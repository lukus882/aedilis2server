using System;
using System.IO;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Commands;

namespace Server.Stargate
{
	public class Stargate
	{
		private static Dictionary<string, StargateDesign> m_Gates;
		public static Dictionary<string, StargateDesign> Gates{ get{ return m_Gates; } }

		public static void AddGate( StargateDesign design )
		{
			m_Gates[design.Combination.ToLower()] = design;
		}

		public static void RemoveGate( StargateDesign design )
		{
			m_Gates.Remove( design.Combination.ToLower() );
		}

		public static StargateDesign FindGate( string combo )
		{
			if ( combo == null )
				return null;

			StargateDesign design;

			m_Gates.TryGetValue( combo.ToLower(), out design );

			return design;
		}

		public static DirectoryInfo EnsureDirectory( string dir )
		{
			return Directory.CreateDirectory( dir );			
		}

		public static string DataPath = "Data/Stargate/";

		public static void Configure()
		{
			//EventSink.WorldSave += new WorldSaveEventHandler( EventSink_WorldSave );
			m_Gates = new Dictionary<string, StargateDesign>();
			EventSink.WorldLoad += new WorldLoadEventHandler( EventSink_WorldLoad );
		}
		public static void Initialize()
		{
			CommandSystem.Register( "ImportStargates", AccessLevel.Administrator, new CommandEventHandler( Import_OnCommand ) );
			CommandSystem.Register( "ExportStargates", AccessLevel.Administrator, new CommandEventHandler( Export_OnCommand ) );
		}

		[Usage( "ImportStargates" )]
		[Description( "Imports the stargate system from \\Data\\Stargate\\" )]
		private static void Import_OnCommand( CommandEventArgs e )
		{
			if ( e.ArgString.Length <= 0 )
				e.Mobile.SendMessage( "Format: {0}ImportStargates <filename>", CommandSystem.Prefix );
			else
			{
				Console.Write( "Stargate: Importing {0}.[idx/tdb/bin]...", e.Arguments[0] );
				try
				{
					ImportSystem( e.Arguments[0] );
					Console.WriteLine( "done" );
				}
				catch( Exception exception )
				{
					Console.WriteLine( "failed" );
					Console.WriteLine( exception );
				}
			}
		}

		[Usage( "ExportStargates" )]
		[Description( "Exports the stargate system to \\Data\\Stargate\\" )]
		private static void Export_OnCommand( CommandEventArgs e )
		{
			if ( e.ArgString.Length <= 0 )
				e.Mobile.SendMessage( "Format: {0}ExportStargates <filename>", CommandSystem.Prefix );
			else
			{
				Console.Write( "Stargate: Exporting {0}.[idx/tdb/bin]...", e.Arguments[0] );
				try
				{
					ExportSystem( e.Arguments[0] );
					Console.WriteLine( "done" );
				}
				catch( Exception exception )
				{
					Console.WriteLine( "failed" );
					Console.WriteLine( exception );
				}
			}
		}


		private static void LoadReferences()
		{
			for( int i = 0; i < m_NeedReference.Count; i++ )
				SetDesign( m_NeedReference[i] );
		}

		private static List<StargateDesignReference> m_NeedReference = new List<StargateDesignReference>();
		public static List<StargateDesignReference> NeedReference{ get{ return m_NeedReference; } }

		internal static List<Type> m_SaveTypes = new List<Type>();

		internal static int AddSaveType( Type type )
		{
			m_SaveTypes.Add( type );
			return m_SaveTypes.Count - 1;
		}

		public static void SetDesign( StargateDesignReference reference )
		{
			object obj = reference.Object;

			MethodInfo parseMethod = obj.GetType().GetMethod( "SetDesign", new Type[]{ typeof( StargateDesign ) } );
			if ( parseMethod != null )
				parseMethod.Invoke( obj, new object[]{ FindGate( reference.Combination ) } );
		}

		private static void EventSink_WorldLoad()
		{
			LoadReferences();
		}

		private static void ImportSystem( string filename )
		{
			string folder = Path.Combine( DataPath, "Import/" );

			List<Item> items = new List<Item>();
			List<ItemEntry> entries = new List<ItemEntry>();

			foreach( Item i in World.Items.Values )
				if ( i is StargateAddon )
					items.Add( i );

			foreach( Item i in items )
				i.Delete();

			items.Clear();

			string failedSerial = null;
			Type failedType = null;
			int failedTypeID = 0;
			Exception failed = null;

			Type[] itemctorTypes = new Type[]{ typeof( Serial ) };
			
			int itemCount = 0;

			string idxpath = Path.Combine( folder, filename + ".idx" );
			string tdbpath = Path.Combine( folder, filename + ".tdb" );
			string binpath = Path.Combine( folder, filename + ".bin" );

			EnsureDirectory( Directory.GetParent( binpath ).FullName );

			if ( File.Exists( idxpath ) && File.Exists( tdbpath ) )
			{
				using ( FileStream idx = new FileStream( idxpath, FileMode.Open, FileAccess.Read, FileShare.Read ) )
				{
					BinaryReader idxReader = new BinaryReader( idx );

					using ( FileStream tdb = new FileStream( tdbpath, FileMode.Open, FileAccess.Read, FileShare.Read ) )
					{
						BinaryReader tdbReader = new BinaryReader( tdb );

						int count = tdbReader.ReadInt32();

						ArrayList types = new ArrayList( count );

						for ( int i = 0; i < count; ++i )
						{
							string typeName = tdbReader.ReadString();

							Type t = ScriptCompiler.FindTypeByFullName( typeName );

							if ( t == null )
							{
								Console.WriteLine( "Stargate Import Failure:" );
								Console.WriteLine( "Error: Type '{0}' was not found. Delete all of those types? (y/n)", typeName );

								if ( Console.ReadLine() == "y" )
								{
									types.Add( null );
									Console.Write( "World: Loading..." );
									continue;
								}

								Console.WriteLine( "Types will not be deleted. Import of the system has been halted." );

								return;
							}

							ConstructorInfo itemctor = t.GetConstructor( itemctorTypes );

							if ( itemctor != null )
								types.Add( new object[] {itemctor, typeName } );
							else
							{
								Console.WriteLine( "Type {0} does not have a serialization constructor. Import of the system has been halted.", t );
								return;
							}
						}

						itemCount = idxReader.ReadInt32();

						for ( int i = 0; i < itemCount; ++i )
						{
							int typeID = idxReader.ReadInt32();

							object[] objs = (object[])types[typeID];

							if ( objs == null )
								continue;

							ConstructorInfo ctor = (ConstructorInfo)objs[0];
							string typeName = (string)objs[1];
							Item item = null;
							int serial = idxReader.ReadInt32();
							long pos = idxReader.ReadInt64();
							int length = idxReader.ReadInt32();

							try
							{
								item = (Item)ctor.Invoke( new object[]{ (Serial)serial } );
							}
							catch ( Exception exception )
							{
								Console.WriteLine( exception );
							}

							if ( item != null )
							{
								entries.Add( new ItemEntry( item, typeID, typeName, pos, length ) );
								World.AddItem( item );
							}
						}

						tdbReader.Close();
					}

					idxReader.Close();
				}
			}

			if ( File.Exists( binpath ) )
			{
				using ( FileStream bin = new FileStream( binpath, FileMode.Open, FileAccess.Read, FileShare.Read ) )
				{
					BinaryFileReader reader = new BinaryFileReader( new BinaryReader( bin ) );

					for ( int i = 0; i < entries.Count; ++i )
					{
						ItemEntry entry = entries[i];
						Item item = entry.Item;

						if ( item != null )
						{
							reader.Seek( entry.Position, SeekOrigin.Begin );

							try
							{
								item.Deserialize( reader );
								item.Delta( ItemDelta.Update );

								if ( reader.Position != ( entry.Position + entry.Length ) )
								{
									Console.WriteLine( String.Format( "Error: Bad Stargate import of {0}.  Import of the system has been halted.", item.GetType() ) );
									return;
								}
							}
							catch ( Exception e )
							{
								items.RemoveAt( i );

								failed = e;
								//failedItems = true;
								failedType = item.GetType();
								failedTypeID = entry.TypeID;
								failedSerial = item.Serial.ToString();

								break;
							}
						}
					}

					reader.Close();
				}
			}

			if ( failed != null )
			{
				Console.WriteLine( "Error: Importing a saved object" );

				Console.WriteLine( " - Type: {0}", failedType );
				Console.WriteLine( " - Serial: {0}", failedSerial );

				Console.WriteLine( String.Format( "Load failed (type={0}, serial={1})", failedType, failedSerial ), failed );
			}
		}

		private static void ExportSystem( string filename )
		{
			string folder = Path.Combine( DataPath, "Export/" );

			string idxpath = Path.Combine( folder, filename + ".idx" );
			string tdbpath = Path.Combine( folder, filename + ".tdb" );
			string binpath = Path.Combine( folder, filename + ".bin" );

			EnsureDirectory( Directory.GetParent( binpath ).FullName );

			GenericWriter idx;
			GenericWriter tdb;
			GenericWriter bin;

			if ( World.SaveType == World.SaveOption.Normal )
			{
				idx = new BinaryFileWriter( idxpath, false );
				tdb = new BinaryFileWriter( tdbpath, false );
				bin = new BinaryFileWriter( binpath, true );
			}
			else
			{
				idx = new AsyncWriter( idxpath, false );
				tdb = new AsyncWriter( tdbpath, false );
				bin = new AsyncWriter( binpath, true );
			}

			ArrayList gates = new ArrayList();

			foreach ( Item item in World.Items.Values )
				if ( item is StargateAddon  || item is SymbolComponent )
					gates.Add( item );

			idx.Write( (int)gates.Count );

			foreach ( Item item in gates )
			{
				//Export the Addon
				long start = bin.Position;
				int ourType = m_SaveTypes.IndexOf( item.GetType() );

				if ( ourType == -1 )
					ourType = AddSaveType( item.GetType() );

				idx.Write( (int) ourType );
				idx.Write( (int) item.Serial );
				idx.Write( (long) start );

				item.Serialize( bin );

				idx.Write( (int) ( bin.Position - start ) );
			}

			tdb.Write( (int) m_SaveTypes.Count );
			for ( int i = 0; i < m_SaveTypes.Count; ++i )
				tdb.Write( m_SaveTypes[i].FullName );

			idx.Close();
			tdb.Close();
			bin.Close();
		}

		private interface IEntityEntry
		{
			Serial Serial{ get; }
			int TypeID{ get; }
			long Position{ get; }
			int Length{ get; }
			Item Item{ get; }
		}

		private sealed class ItemEntry : IEntityEntry
		{
			private Item m_Item;
			private int m_TypeID;
			private string m_TypeName;
			private long m_Position;
			private int m_Length;

			public Item Item{ get{ return m_Item; } }
			public Serial Serial{ get{ return m_Item == null ? Serial.MinusOne : m_Item.Serial; } }
			public int TypeID{ get{ return m_TypeID; } }
			public string TypeName{ get{ return m_TypeName; } }
			public long Position{ get{ return m_Position; } }
			public int Length{ get{ return m_Length; } }

			public ItemEntry( Item item, int typeID, string typeName, long pos, int length )
			{
				m_Item = item;
				m_TypeID = typeID;
				m_TypeName = typeName;
				m_Position = pos;
				m_Length = length;
			}
		}
	}

	public class StargateDesignReference
	{
		private object m_Object;
		private string m_Combination;

		public object Object{ get{ return m_Object; } set{ m_Object = value; } }
		public string Combination{ get{ return m_Combination; } set{ m_Combination = value; } }

		public StargateDesignReference( object obj, string combo )
		{
			m_Object = obj;
			m_Combination = combo;
		}
	}
}