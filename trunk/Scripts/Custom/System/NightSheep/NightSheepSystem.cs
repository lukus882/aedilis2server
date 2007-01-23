using Server;
using System;
using Server.Mobiles;
using Server.Items;
using System.Collections.Generic;
using Server.Spells;
using System.IO;
using System.Xml;
using System.Globalization;

namespace Server.Misc
{

	public class NightSheepSystem 
	{
		public static Dictionary<Mobile, List<BaseCreature>> SheepList; 
		public static string FileName = "Nightsheep.xml";
		
	
		public static void Initialize()
		
		{
			EventSink.Logout += new LogoutEventHandler( EventSink_Logout );
			EventSink.WorldSave += new WorldSaveEventHandler( EventSink_WorldSave );
			CreateSheepList();
			LoadFile();
			
		}
		private static void EventSink_Logout( LogoutEventArgs args )
		{
			Mobile m = args.Mobile;
			if ( SheepList.ContainsKey( m ) )
			{
				List<BaseCreature> list = new List<BaseCreature>();
				if ( SheepList.TryGetValue( m, out list ) && list != null )
				{
					foreach ( BaseCreature bc in list )
					{
						if ( !bc.Deleted ) 
							bc.Delete(); 
					}
					SheepList.Remove( m );
				}
				else
					SheepList.Remove( m );  //Should never happen but...
				
			}
		}
		
		public static void CreateSheepList()
		{
			if ( SheepList == null)
				SheepList = new Dictionary<Mobile, List<BaseCreature>>();
		}
		
		public static void AddSheep( Mobile m, BaseCreature sheep )
		{
			if ( SheepList == null )
				m.SendMessage( "An Error has occured, please contact a GM" );
			else
			{
			
				if ( SheepList.ContainsKey( m ) )
				{
					List<BaseCreature> list;
					if ( SheepList.TryGetValue( m, out list ) )
					{
						if ( list == null )  //Should never happen but crash preventative.
						{
							list = new List<BaseCreature>();
							SheepList[m] = list;
							SheepList[m].Add( sheep );
						}
						else
						{
							SheepList[m].Add( sheep );
						}
					}
				}
				else
				{
					List<BaseCreature> list = new List<BaseCreature>();
					SheepList.Add( m, list );
					SheepList[m].Add( sheep );
				}
			}
			    	
		}
		
		public static void RemoveSheep( Mobile m, BaseCreature sheep )
		{
			List<BaseCreature> list;
			
			if ( SheepList.TryGetValue( m, out list ) )
			{
				if ( list.Contains( sheep ) )
					list.Remove( sheep );
				SheepList[m] = list;
				
			}
		}
		
		
		public static void CheckSander( bool success, BaseCreature creature, Mobile from )
		{
			if ( success )
			{
				if ( .20 > Utility.RandomDouble() ) //20% chance to spawn a sander
				{
					BaseCreature sandy = new Sander();
					sandy.MoveToWorld( creature.Location, creature.Map );
					sandy.FixedParticles( 0x3709, 10, 30, 5052, EffectLayer.LeftFoot );
       				sandy.PlaySound( 0x225 ); 
					sandy.Warmode = true;
					sandy.Combatant = from; 
					
					
				}				
			}
			else
			{
				if ( .10 > Utility.RandomDouble() ) //10% chance to spawn a sander
				{
					BaseCreature sandy = new Sander();
					sandy.MoveToWorld( creature.Location, creature.Map );
					sandy.FixedParticles( 0x3709, 10, 30, 5052, EffectLayer.LeftFoot );
       				sandy.PlaySound( 0x225 ); 
					sandy.Warmode = true;
					sandy.Combatant = from;
					
				}
			}
		}
		
		
		private static void EventSink_WorldSave( WorldSaveEventArgs e )
		{
			using ( StreamWriter sw = new StreamWriter( Path.Combine( Core.BaseDirectory, FileName ) ) )
			{
				XmlTextWriter xml = new XmlTextWriter( sw );

				xml.Formatting = Formatting.Indented;
				xml.IndentChar = '\t';
				xml.Indentation = 1;
				xml.WriteStartDocument( true );
				xml.WriteStartElement( "Herders" );
				xml.WriteAttributeString( "count", SheepList.Count.ToString() );
				
				foreach ( KeyValuePair<Mobile, List<BaseCreature>> kvp in SheepList )
				{
					Mobile m = kvp.Key;
					List<BaseCreature> creat = kvp.Value;
					char[] trimmed = new char[2];
					string excess = "0x";
					trimmed = excess.ToCharArray();
					
					xml.WriteStartElement( "Herder" );
						xml.WriteStartElement( "Name" );
						string name = m.Serial.ToString();
						name = name.TrimStart( trimmed );
						xml.WriteString( name );
						xml.WriteEndElement();
						
						
						foreach ( BaseCreature b in creat )
						{
							xml.WriteStartElement( "Sheep" );
							string sheepname = b.Serial.ToString();
							sheepname = sheepname.TrimStart( trimmed );
							xml.WriteString( sheepname );
							xml.WriteEndElement();
						}
						
						
					xml.WriteEndElement();
					
				}
				
				
				xml.WriteEndElement();
				xml.Close();
			
			}
		}
		
		
		public static string GetInnerText( XmlElement node )
		{
			if ( node == null )
				return "Error";

			return node.InnerText;
		}
		public static void LoadFile()
		{
			if ( !File.Exists( FileName ) )
				return;

			XmlDocument xml = new XmlDocument();
			xml.Load( FileName );

			XmlElement herders = xml["Herders"];

			foreach ( XmlElement herd in herders.GetElementsByTagName( "Herder" ) )
			{
				try
				{
					int mob = Int32.Parse( GetInnerText( herd["Name"] ), NumberStyles.HexNumber  );
					Mobile m = World.FindMobile( mob );
					List<BaseCreature> list = new List<BaseCreature>();
					foreach ( XmlElement sheeps in herd.GetElementsByTagName( "Sheep" ) )
					{
						int basec = Int32.Parse( sheeps.InnerText, NumberStyles.HexNumber );
						BaseCreature bc = World.FindMobile( basec ) as BaseCreature;
						if ( bc != null ) //Shouldnt happen unless some bonehead restarts the shard wrong.
							list.Add( bc );
					}
					
					SheepList.Add( m, list );
				}
				catch 
				{
					Console.WriteLine( "Error loading Nightsheep XML" );
				}
			}
		}
	
	}
	
	public class NightCritterTimer : Timer
	{
			private DateTime m_Expire;
			private Container m_Corpse;
			
			
			public NightCritterTimer( Container corpse ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 1.0 ) )
			{
			   m_Expire = DateTime.Now + TimeSpan.FromSeconds( 3.0 );
			   m_Corpse = corpse;
			
			                   
			}
			                            
			protected override void OnTick()
			{
				if ( DateTime.Now >= m_Expire )				
				{
					Corpse c = (Corpse)m_Corpse;
					Effects.SendLocationParticles( EffectItem.Create( c.Location, c.Map, EffectItem.DefaultDuration ), 0x3709, 10, 30, 3, 0, 5052, 0 );
					Effects.PlaySound( c, c.Map, 0x208 );
         			
         			IPooledEnumerable eable = c.GetMobilesInRange( 4 );
         			foreach ( Mobile m in eable )
         			{
         				if ( m is PlayerMobile || m is NightSheep )
         				{
         					Spells.SpellHelper.Damage( TimeSpan.FromSeconds( 0.5 ), m, c.Owner, (Utility.RandomDouble() * 100), 0, 100, 0, 0, 0 );
         				         					
         				}
         					
         			}
         			c.Delete();
         			eable.Free();
					
					Stop();
				}
			}
		
	}
}
