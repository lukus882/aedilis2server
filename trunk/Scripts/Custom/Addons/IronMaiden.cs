using System;
using Server;
using Server.Items;
using Server.Network;

namespace Server.Items
{
    public class IronMaiden : AddonComponent
	{
		private InternalTimer m_Timer;
		public static TimeSpan AnimDelay = TimeSpan.FromSeconds( 8.0 ); //the delay between animation is 8 seconds
		public DateTime m_NextAnim;

		[Constructable]
		public IronMaiden() : base( 0x124B )
		{
		}

		public IronMaiden( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile m )
		{
			if ( m.InRange( this, 3 ) ) 
			{
				switch ( ItemID ) 
				{ 
					//do swap or animation here 
					case 0x124B: //open
						Effects.PlaySound( m.Location, m.Map, 0x2D );
						this.ItemID=0x124A; break;
					case 0x124C: //open
						Effects.PlaySound( m.Location, m.Map, 0x2D );
						this.ItemID=0x124A; break;
					case 0x124D: //clean
						if (m.Female)
						{
							Effects.PlaySound( m.Location, m.Map, 0x32D );
						}
						else
						{
							Effects.PlaySound( m.Location, m.Map, 0x43F );
						}
						this.ItemID=0x124B; //open

						m.PrivateOverheadMessage( MessageType.Regular, 1153, false, "You quickly clean up the blood!", m.NetState );

						break; 
					case 0x124A: //close 
						this.ItemID=0x124B;
						Effects.PlaySound( m.Location, m.Map, 0x2C );
						new InternalTimer( this, m ).Start();
						break;
					default: break; 
				}
			}
			else
			{
				m.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that
			}
		}

		public override bool HandlesOnMovement{ get{ return true; } }

		public override void OnMovement( Mobile m, Point3D oldLocation ) 
		{ 
			if ( DateTime.Now >= m_NextAnim && m.InRange( this, 4 ) ) // check if it's time to animate & mobile in range & in los.
			{
				m_NextAnim = DateTime.Now + AnimDelay; // set next animation time

				switch ( ItemID ) 
				{ 
					//do swap or animation here 
					case 0x124A: //close 
						this.ItemID=0x124B;
						Effects.PlaySound( m.Location, m.Map, 0x2C );
						new InternalTimer( this, m ).Start();
						break;
					case 0x124B: //open
						Effects.PlaySound( m.Location, m.Map, 0x2D );
						this.ItemID=0x124A;
						break;
					case 0x124D:
						Effects.PlaySound( m.Location, m.Map, 0x2D );
						this.ItemID=0x124A;
						break;
					default: break; 
				}
			}
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 );
		}
        
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}

		public class InternalTimer : Timer
		{
			private int m_Count = 4;
			private IronMaiden m_IronMaiden;
			private Mobile m_From;
	
			public InternalTimer( IronMaiden ironmaiden, Mobile from ) : base( TimeSpan.FromSeconds( 1.0 ), TimeSpan.FromSeconds( 1.0 ) )
			{
				m_IronMaiden = ironmaiden;
				m_From = from;
			}

			protected override void OnTick() 
			{
				m_Count--;
	
				if ( m_Count == ( 3 ) )
				{
					Effects.PlaySound( m_From.Location, m_From.Map, Utility.RandomList( 0x317, 0x316, 0x424, 0x440) );
				}
				if ( m_Count == ( 2 ) )
				{
					m_IronMaiden.ItemID=0x124C;
				}
				if ( m_Count == ( 1 ) )
				{
					m_IronMaiden.ItemID=0x124D; 
				}
				if ( m_Count == 0 )
				{
					Stop();
				}

				if ( m_From.NetState == null )
				{
					Stop();
				}
			}
		}

	}
			
	public class IronMaidenAddon : BaseAddon
	{
	    public override BaseAddonDeed Deed{ get{ return new IronMaidenDeed(); } }
		
		[Constructable]
		public IronMaidenAddon()
		{
		    Name = "Iron Maiden";
			Weight = 2.0;
			
			AddComponent( new IronMaiden(), 0, 0, 0 );
		}

		public IronMaidenAddon( Serial serial ) : base( serial )
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
	}

	public class IronMaidenDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new IronMaidenAddon(); } }

		[Constructable]
		public IronMaidenDeed()
		{
		    Name = "Iron Maiden Deed";
		}

		public IronMaidenDeed( Serial serial ) : base( serial )
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
    }
	
}