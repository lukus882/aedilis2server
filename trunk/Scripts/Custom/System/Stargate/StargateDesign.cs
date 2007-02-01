using System;
using System.Collections;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Accounting;

namespace Server.Stargate
{
	[PropertyObject]
	public class StargateDesign
	{
		public static int DefaultActivateHue{ get{ return 0; } }
		public static int DefaultDeactivateHue{ get{ return 0; } }
		public static int DefaultNormalHue{ get{ return 0; } }
		public static int DefaultGateHue{ get{ return 0; } }

		private string m_Combination;
		private string m_Area;
		private DesignType m_Design;
		private int m_NormalHue, m_ActivateHue, m_DeactivateHue, m_GateHue;
		private uint m_StartingPos;
		private bool m_Reversed;
		private double m_DropChance;
		private StargateAddon m_Gate;

		[CommandProperty( AccessLevel.GameMaster, AccessLevel.Seer )]
		public string Combination
		{ 
			get{ return m_Combination; } 
			set
			{
				if ( Stargate.FindGate( value ) != null )
					return;
				Stargate.RemoveGate( this );
				m_Combination = value;
				Stargate.AddGate( this );
			}
		}
		[CommandProperty( AccessLevel.GameMaster )]
		public string Area{ get{ return m_Area; } set{ m_Area = value; } }
		[CommandProperty( AccessLevel.GameMaster )]
		public string Design
		{ 
			get{ return m_Design == null ? null : m_Design.Name; } 
			set
			{ 
				DesignType design = DesignTypes.FindDesign( value );
				if ( design != null && design != m_Design )
				{
					m_Design = design; 
					UpdateGate( UpdateFlag.Design );
				} 
			}
		}
		public DesignType DesignType{ get{ return m_Design; } set{ m_Design = value; UpdateGate( UpdateFlag.Design ); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int NormalHue{ get{ return m_NormalHue; } set{ m_NormalHue = value; UpdateGate( UpdateFlag.Hue ); } }
		[CommandProperty( AccessLevel.GameMaster )]
		public int ActivateHue{ get{ return m_ActivateHue; } set{ m_ActivateHue = value; } }
		[CommandProperty( AccessLevel.GameMaster )]
		public int DeactivateHue{ get{ return m_DeactivateHue; } set{ m_DeactivateHue = value; } }
		[CommandProperty( AccessLevel.GameMaster )]
		public int GateHue{ get{ return m_GateHue; } set{ m_GateHue = value; } }
		[CommandProperty( AccessLevel.GameMaster )]
		public uint StartingPos{ get{ return m_StartingPos; } set{ m_StartingPos = value; UpdateGate( UpdateFlag.Design ); } }
		[CommandProperty( AccessLevel.GameMaster )]
		public bool Reversed{ get{ return m_Reversed; } set{ m_Reversed = value; UpdateGate( UpdateFlag.Design ); } }
		[CommandProperty( AccessLevel.GameMaster )]
		public double DropChance{ get{ return m_DropChance; } set{ m_DropChance = value; } }

		public StargateAddon Gate{ get{ return m_Gate; } set{ m_Gate = value; } }

		public StargateDesign( string combo ) : this( combo, null )
		{
		}

		public StargateDesign( string combo, StargateAddon gate ) : this( combo, "Unknown Area", gate )
		{
		}

		public StargateDesign( string combo, string design, StargateAddon gate ) : this( combo, "Uknown Area", design, gate )
		{
		}

		public StargateDesign( string combo, string area, string design, StargateAddon gate ) : this( combo, area, design, 0, gate )
		{
		}

		public StargateDesign( string combo, string design, uint startingpos, StargateAddon gate ) : this( combo, "Uknown Area", design, startingpos, gate )
		{
		}

		public StargateDesign( string combo, string area, string design, uint startingpos, StargateAddon gate ) : this( combo, area, design, startingpos, false, gate )
		{
		}

		public StargateDesign( string combo, string area, string design, uint startingpos, bool reversed, StargateAddon gate )
		{
			m_Combination = combo;
			m_Area = area;

			m_ActivateHue = DefaultNormalHue;
			m_NormalHue = DefaultNormalHue;
			m_DeactivateHue = DefaultDeactivateHue;
			m_StartingPos = startingpos;
			m_Reversed = reversed;
			m_Gate = gate;
			Design = design;
		}

		public void UpdateGate( UpdateFlag flag )
		{
			if ( m_Gate != null )
				m_Gate.Update( flag );
			else
				Console.WriteLine( "Warning: orphaned stargate design detected." );
		}

		public override string ToString()
		{
			return m_Combination;
		}

		public void Serialize( GenericWriter writer )
		{
			writer.WriteEncodedInt( (int) 0 ); // version

			writer.Write( Combination );
			writer.Write( m_Area );
			writer.Write( m_Design == null ? (string)null : m_Design.Name );
			writer.Write( m_NormalHue );
			writer.Write( m_ActivateHue );
			writer.Write( m_DeactivateHue );
			writer.Write( (uint)m_StartingPos );
			writer.Write( m_Reversed );
			writer.Write( m_DropChance );
			writer.Write( m_GateHue );
		}

		public void Deserialize( GenericReader reader )
		{
			int version = reader.ReadEncodedInt();

			m_Combination = reader.ReadString();
			m_Area = reader.ReadString();
			m_Design = DesignTypes.FindDesign( reader.ReadString() );
			m_NormalHue = reader.ReadInt();
			m_ActivateHue = reader.ReadInt();
			m_DeactivateHue = reader.ReadInt();
			m_StartingPos = reader.ReadUInt();
			m_Reversed = reader.ReadBool();
			m_DropChance = reader.ReadDouble();
			m_GateHue = reader.ReadInt();
		}

		public StargateDesign( GenericReader reader )
		{
			Deserialize( reader );
		}

		public static string CombinationWord( char letter )
		{
			switch ( Char.ToLower( letter ) )
			{
				default: return "Unknown";
				case 'a': return "Aquarius";
				case 'b': return "Bootes";
				case 'c': return "Cancer";
				case 'd': return "Draco";
				case 'e': return "Eridanus";
				case 'f': return "Fornax";
				case 'g': return "Gemini";
				case 'h': return "Hydra";
				case 'i': return "Indus";
				case 'j': return "Jabbah";
				case 'k': return "Kraz";
				case 'l': return "Libra";
			}
		}
	}

	public class DesignType
	{
		public string Name;
		public int[] DesignOffsets;

		public DesignType( string name )
		{
			Name = name;
			DesignOffsets = new int[24]; //12 symbols, 2 coordinates each
		}
	}

	public class DesignTypes
	{
		private static Dictionary<string, DesignType> m_Designs;
		public static Dictionary<string, DesignType> Designs{ get{ return m_Designs; } }

		public static void Configure()
		{
			LoadDesigns();
		}

		public static void LoadDesigns()
		{
			Console.Write( "Stargate Designs: Loading..." );

			string filePath = Path.Combine( "Data/Stargate", "gatedesigns.xml" );

			if ( !File.Exists( filePath ) )
			{
				m_Designs = new Dictionary<string, DesignType>( 0 );
				return;
			}

			XmlDocument doc = new XmlDocument();
			doc.Load( filePath );

			XmlElement root = doc["designs"];
			m_Designs = new Dictionary<string, DesignType>( Utility.ToInt32( Utility.GetAttribute( root, "count", "0" )) );
			foreach ( XmlElement designxml in root.GetElementsByTagName( "design" ) )
			{
				try
				{
					DesignType design = new DesignType( Utility.GetAttribute( designxml, "name", "Unknown" ) );
					int setcounter = 0;
					foreach( XmlElement designset in designxml.GetElementsByTagName( "set" ) )
					{
						design.DesignOffsets[setcounter++] = Utility.ToInt32( Utility.GetAttribute( designset, "x", "0" ) );
						design.DesignOffsets[setcounter++] = Utility.ToInt32( Utility.GetAttribute( designset, "y", "0" ) );
					}
					m_Designs[design.Name.ToLower()] = design;
				}
				catch
				{
					Console.WriteLine( "Warning: Stargate designs failed to load." );
				}
			}
			Console.WriteLine( "done" );
		}

		public static DesignType FindDesign( string name )
		{
			if ( name == null )
				return null;

			DesignType design;

			m_Designs.TryGetValue( name.ToLower(), out design );

			return design;
		}
	}
}