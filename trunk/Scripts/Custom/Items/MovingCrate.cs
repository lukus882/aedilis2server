using System;
using System.Collections;
using Server;
using Server.Mobiles;
using Server.Multis;
using Server.Network;
using Server.Accounting; 
using Server.Commands.Generic;
using Server.Commands;

namespace Server.Items
{

		[Furniture]
	[Flipable( 0x2813, 0x2814 )]
	public class MovingBox : Container
	{
		public static void Initialize() 
		{ 
			CommandSystem.Register( "SummonCrate", AccessLevel.Player, new CommandEventHandler( MovingBox_OnCommand ) );	
			CommandSystem.Register( "DismissCrate", AccessLevel.Player, new CommandEventHandler( DismissBox_OnCommand ) );
			CommandSystem.Register( "GetBox", AccessLevel.GameMaster, new CommandEventHandler( GetBox_OnCommand ) );
			CommandSystem.Register( "CountBoxes", AccessLevel.GameMaster, new CommandEventHandler( CountBox_OnCommand ) );
		} 
		
		[Usage( "CountBoxes" )]
		[Description( "Counts the amount of Moving Crates in the Internal Map" )]
		public static void CountBox_OnCommand( CommandEventArgs e )
		{
			ArrayList list = new ArrayList( World.Items.Values );
			int count = 0;
						foreach ( Item item in list )
						{
							if ( item is MovingBox )
							{
								MovingBox box = (MovingBox)item;
								if ( box.Map == Map.Internal )
								{
									count+= 1;
									continue;
								}
								
							}
						}
						Mobile from = e.Mobile;
						from.SendMessage( "There are {0} boxes on the Internal Map", count );
		}
		
		
		
		[Usage( "SummonCrate" )]
		[Description( " Summons a Moving Crate " )] 
		public static void MovingBox_OnCommand( CommandEventArgs e ) 
		{
			Mobile from = e.Mobile;
			SummonBox( from, true );
		}
		
		[Usage( "DismissCrate" )] 
		[Description( " Dismisses a Moving Crate " )] 
		public static void DismissBox_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			DismissBox( from );
		}
		
		[Usage( "GetBox <Serial>" )]
		[Description( "Allows a GM or Admin to retrieve a Moving Box" )]
		public static void GetBox_OnCommand( CommandEventArgs e ) 
		{
			Mobile from = e.Mobile;
			if ( e.Length == 0 )
				from.SendMessage( "You must include a serial" );
			else
			{
				int ser = e.GetInt32( 0 );
				ArrayList list = new ArrayList( World.Items.Values );
						foreach ( Item item in list )
						{
							if ( item is MovingBox )
							{
								MovingBox box = (MovingBox)item;
								if ( box.Serial == ser )
								{
									box.Map = from.Map;
									box.Location = from.Location;
									break;
								}
							}
						}
			}
		}
		
		private bool m_used;
		private Mobile m_owner;
		private bool m_inworld;
		private DateTime m_end;
		private InternalTimer m_Timer;
		
		public override int DefaultMaxWeight
		{
			get	{ return 0; }
		
		}
		
		public DateTime End
		{
			get { return m_end; }
			set { m_end = value; }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		 public bool Used
		 {
		 	get { return m_used; }
		 	set { m_used = value; }
		 }
		 
		 [CommandProperty( AccessLevel.GameMaster )]
		  public Mobile Owner
		  {
		  	get { return m_owner; }
		  	set { m_owner = value; }
		  }
		  
		  [CommandProperty( AccessLevel.GameMaster )]
		   public bool InWorld
		   {
		   	get { return m_inworld; }
		   	set { m_inworld = value; }
		   }
		   
		   [CommandProperty( AccessLevel.GameMaster )]
		   public TimeSpan ToDelete
		   {
		   	get { return m_end - DateTime.Now; }
		   	set { DoTimer( value, m_owner ); }
		   }
		   
		   	
		public override int DefaultGumpID{ get{ return 0x10D; } }
		public override int DefaultDropSound{ get{ return 0x42; } }

		public override Rectangle2D Bounds
		{
			get{ return new Rectangle2D( 18, 105, 144, 73 ); }
		}

		public static void SummonBox( Mobile from, bool exist )
		{
			BaseHouse house = BaseHouse.FindHouseAt( from );
			bool found = false;
			if ( house == null )
				from.SendMessage( "You must be in your house to do this!" );
			else
				if ( !house.IsOwner( from ) )
					from.SendMessage( "You may only do this in your own house!" );
				else
					if ( !exist ) 
						{
						Account acct = (Account)from.Account;
						Point3D targ = new Point3D( from.X + 1,from.Y + 1, from.Z );
						Effects.SendLocationEffect( targ, from.Map, 0x376A, 50 );
						from.PlaySound( 0x5C3 );
						MovingBox box = new MovingBox( from );
						box.Map = from.Map;
						box.Location = targ;
						box.Owner = from;
						box.InWorld = true;
						box.End = DateTime.Now + TimeSpan.FromDays( 2.0 );
						acct.SetTag( "Crate", box.Serial.ToString() );
						from.SendMessage( "You have summoned a moving chest, you must complete your move within 2 days!" );
				
						}
					else
					{
						ArrayList list = new ArrayList( World.Items.Values );
						foreach ( Item item in list )
						{
							if ( item is MovingBox )
							{
								MovingBox box = (MovingBox)item;
								if ( !box.InWorld && box.Map == Map.Internal && box.Owner == from )
								{
									Point3D targ = new Point3D( from.X + 1,from.Y + 1, from.Z );
									Effects.SendLocationEffect( targ, from.Map, 0x376A, 50 );
									from.PlaySound( 0x5C3 );
									box.Map = from.Map;
									box.Location = targ;
									box.InWorld = true;
									found = true;
									TimeSpan time = box.End - DateTime.Now;
									from.SendMessage( "You have summoned your moving chest, time left {0} Days, {1} Hours, {2} Minutes, {3} Seconds", time.Days, time.Hours, time.Minutes, time.Seconds  );
									break;
								}
								
							}
							
						}
						 
						if ( !found )
						from.SendMessage( "You either have no moving chest or it has decayed!" );
					}
					
		}
		
		public static void DismissBox( Mobile from )
		{
			bool found = false;
			BaseHouse house = BaseHouse.FindHouseAt( from );
			if ( house == null )
				from.SendMessage( "You must be in your house to do this!" );
			else
				if ( !house.IsOwner( from ) )
					from.SendMessage( "You may only do this in your own house!" );
				else
				{
					ArrayList list = new ArrayList( World.Items.Values );
					foreach ( Item item in list )
					{
						if ( item is MovingBox )
						{
							MovingBox box = (MovingBox)item;
							if ( box.Owner == from && box.InWorld && box.Map == from.Map )
								{
									Effects.SendLocationEffect( box.Location, from.Map, 0x3789, 100 );
									from.PlaySound( 0x5C7 );		
									box.Map = Map.Internal;
					 				box.InWorld = false;
					 				found = true;
					 				break;
					 				
					 			}
						}
					}
					if ( !found )
					from.SendMessage( "You do not have any moving crates summoned." );
				}
		}
		
		public override bool OnDragLift( Mobile from )
		{
			return false;
		}
		
		[Constructable]
		public MovingBox( Mobile from ) : base( 0x2813 )
		{
			Weight = 15.0; // TODO: Real weight
			Name = "A Moving Crate";
			DoTimer( TimeSpan.FromDays( 2.0 ), from );
		}

		public override bool CheckHold( Mobile m, Item item, bool message, bool checkItems, int plusItems, int plusWeight )
		{
			return true;

		}
		
				
		public override void OnDoubleClick( Mobile from )
		{
			if ( this.Owner != from && from.AccessLevel < AccessLevel.GameMaster )
			{
				from.SendMessage( "Only the owner may use this box!" );
				return;
			}
		
			base.OnDoubleClick( from );
				
		}
		public void DoTimer( TimeSpan delay, Mobile owner )
		{
			
			
			if ( m_Timer != null )
				m_Timer.Stop();
			
			m_end = DateTime.Now + delay;

			m_Timer = new InternalTimer( delay, this, owner );
			m_Timer.Start();
		}

		private class InternalTimer : Timer
		{
			private MovingBox BOX;
			private DateTime m_Expire;
			private Mobile m_owner;

			public InternalTimer( TimeSpan delay, MovingBox box, Mobile from ) : base( delay )
			{
				Priority = TimerPriority.FiveSeconds;
				BOX = box;
				m_Expire = DateTime.Now + delay;
				m_owner = from;
			}

			protected override void OnTick()
			{
				if ( DateTime.Now >= m_Expire || BOX.ToDelete < TimeSpan.Zero )
				{
					Account acct = (Account)m_owner.Account;
					BOX.Delete();
					acct.SetTag( "Crate", "null" );
					Stop();
				}
			}
		}
		
		public MovingBox( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			writer.Write( (bool) m_used );
			writer.Write( (Mobile) m_owner );
			writer.Write( (bool) m_inworld );
			writer.Write( (DateTime) m_end );
			
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			m_used = reader.ReadBool();
			m_owner = reader.ReadMobile();
			m_inworld = reader.ReadBool();
			m_end = reader.ReadDateTime();
			TimeSpan delay = m_end - DateTime.Now;
			DoTimer( delay, m_owner );
		}
	}


	public class MovingBoxDeed : Item
	{
		

		[Constructable]
		public MovingBoxDeed() : base( 0x14F0 )
		{
			Hue = 0x488;
			Weight = 1.0;
			Name = "a Moving Box Deed";
		}

		public MovingBoxDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			
		}

		public bool ValidatePlacement( Mobile from, Point3D loc )
		{
			if ( from.AccessLevel >= AccessLevel.GameMaster )
				return true;

			if ( !from.InRange( this.GetWorldLocation(), 1 ) )
			{
				from.SendLocalizedMessage( 500446 ); // That is too far away.
				return false;
			}

			
			Map map = from.Map;

			if ( map == null )
				return false;

			BaseHouse house = BaseHouse.FindHouseAt( loc, map, 20 );

			if ( house == null || !house.IsOwner( from ) )
			{
				from.SendMessage( "You may only do this in your house!" );
				return false;
			}

			if ( !map.CanFit( loc, 3 ) )
			{
				from.SendLocalizedMessage( 500269 ); // You cannot build that there.
				return false;
			}

			return true;
		}

		public override void OnDoubleClick( Mobile from )
		{
			Point3D targ = new Point3D( from.X + 1,from.Y + 1, from.Z );
			BaseHouse house = BaseHouse.FindHouseAt( from );
			ArrayList list = new ArrayList( World.Items.Values );
			bool owned = false;
			foreach ( Item item in list )
			{
				if ( item is MovingBox )
					{
						MovingBox box = (MovingBox)item;
						if ( box.Owner == from )
						{
							owned = true;
							break;
						}			
					}
			}
			if ( owned )
			{
				from.SendMessage( "You may only have 1 crate at a time! You have lost your deed!" );
				this.Delete();
			}
			else
			if ( house == null )
				from.SendMessage( "You must be in your house to do this!" );
			else
				if ( !house.IsOwner( from ) )
					from.SendMessage( "You may only do this in your own house!" );	
				else				
					if ( ValidatePlacement( from, targ ) )
				    	{
				    		MovingBox.SummonBox( from, false );
				    		this.Delete();
				    	}
					else
						from.SendMessage( "You can't summon a box there, try standing in an open area of your house." );
		}
	}
	
	

}
