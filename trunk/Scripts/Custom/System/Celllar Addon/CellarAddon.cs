using System;
using Server;
using Server.Network;
using System.Collections;
using Server.Multis;

namespace Server.Items
{
	public class CellarAddon : BaseAddon, IChopable
	{
		public override BaseAddonDeed Deed{ get{ return new CellarDeed(); } }

		[Constructable]
		public CellarAddon()
		{
                        AddComponent( new AddonComponent( 0x31F4 ), -1, -1, -40 );
			AddComponent( new AddonComponent( 0x31F4 ),  0, -1, -40 );
			AddComponent( new AddonComponent( 0x31F4 ),  1, -1, -40 );
			AddComponent( new AddonComponent( 0x31F4 ), -1,  0, -40 );
			AddComponent( new AddonComponent( 0x31F4 ),  0,  0, -40 );
			AddComponent( new AddonComponent( 0x31F4 ),  1,  0, -40 );
			AddComponent( new AddonComponent( 0x31F4 ), -1,  1, -40 );
			AddComponent( new AddonComponent( 0x31F4 ),  0,  1, -40 );
			AddComponent( new AddonComponent( 0x31F4 ),  1,  1, -40 );
                        AddComponent( new AddonComponent( 0x31F4 ), -2, -2, -40 );
			AddComponent( new AddonComponent( 0x31F4 ),  0, -2, -40 );
			AddComponent( new AddonComponent( 0x31F4 ),  2, -2, -40 );
			AddComponent( new AddonComponent( 0x31F4 ), -2,  0, -40 );
                        AddComponent( new AddonComponent( 0x31F4 ),  2,  0, -40 );
			AddComponent( new AddonComponent( 0x31F4 ), -2,  2, -40 );
			AddComponent( new AddonComponent( 0x31F4 ),  0,  2, -40 );
			AddComponent( new AddonComponent( 0x31F4 ),  2,  2, -40 );
                        AddComponent( new AddonComponent( 0x31F4 ), 1, 2, -40 );
			AddComponent( new AddonComponent( 0x31F4 ), 2, 1, -40 );
                        AddComponent( new AddonComponent( 0x31F4 ), -1, 2, -40 );
			AddComponent( new AddonComponent( 0x31F4 ), -2, 1, -40 );
                        AddComponent( new AddonComponent( 0x31F4 ), 1, -2, -40 );
			AddComponent( new AddonComponent( 0x31F4 ), 2, -1, -40 );
                        AddComponent( new AddonComponent( 0x31F4 ), -1, -2, -40 );
			AddComponent( new AddonComponent( 0x31F4 ), -2, -1, -40 );
   
			AddComponent( new AddonComponent( 0x31F4 ), 0, 3, -40 );
			AddComponent( new AddonComponent( 0x31F4 ), 3, 0, -40 );
			AddComponent( new AddonComponent( 0x31F4 ), 1, 3, -40 );
			AddComponent( new AddonComponent( 0x31F4 ), 3, 1, -40 );
                        AddComponent( new AddonComponent( 0x31F4 ), -1, 3, -40 );
			AddComponent( new AddonComponent( 0x31F4 ), -3, 1, -40 );
                        AddComponent( new AddonComponent( 0x31F4 ), 1, -3, -40 );
			AddComponent( new AddonComponent( 0x31F4 ), 3, -1, -40 );
                        AddComponent( new AddonComponent( 0x31F4 ), -1, -3, -40 );
			AddComponent( new AddonComponent( 0x31F4 ), -3, -1, -40 );
			AddComponent( new AddonComponent( 0x31F4 ), 2, 3, -40 );
			AddComponent( new AddonComponent( 0x31F4 ), 3, 2, -40 );
                        AddComponent( new AddonComponent( 0x31F4 ), -2, 3, -40 );
			AddComponent( new AddonComponent( 0x31F4 ), -3, 2, -40 );
                        AddComponent( new AddonComponent( 0x31F4 ), 2, -3, -40 );
			AddComponent( new AddonComponent( 0x31F4 ), 3, -2, -40 );
                        AddComponent( new AddonComponent( 0x31F4 ), -2, -3, -40 );
			AddComponent( new AddonComponent( 0x31F4 ), -3, -2, -40 );
                        AddComponent( new AddonComponent( 0x31F4 ), -3, -3, -40 );
			AddComponent( new AddonComponent( 0x31F4 ), -3, 3, -40 );
                        AddComponent( new AddonComponent( 0x31F4 ), 3, -3, -40 );
			AddComponent( new AddonComponent( 0x31F4 ), 3, 3, -40 );
                        AddComponent( new AddonComponent( 0x31F4 ), 0, -3, -40 );
			AddComponent( new AddonComponent( 0x31F4 ), -3, 0, -40 );
   
                        AddComponent( new CellarTeleporter(), 0, 0, -39 );
                        AddComponent( new CellarTeleporter1(), 0, 0, 0 );
		}

		public CellarAddon( Serial serial ) : base( serial )
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

	public class CellarDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new CellarAddon(); } }
	

		[Constructable]
		public CellarDeed()
		{
                 Name = "Cellar Deed";
		 Weight = 5.0;
                 Hue = 46;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if (from.AccessLevel > AccessLevel.Player)
			{
				from.SendMessage( "You require the assistance of a Game Master to install this add-on" );
				return;
			}
			else if ( from.Z <= -60 )
			{
				from.SendMessage( "You can not make a cellar here - you are to deep" );
				return;
			}
		
			base.OnDoubleClick( from );
				
		}

		public CellarDeed( Serial serial ) : base( serial )
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
 
        public class CellarTeleporter1 : AddonComponent
	{
		[Constructable]
		public CellarTeleporter1() : this( false )
		{
		}

		[Constructable]
		public CellarTeleporter1( bool creatures ) : base( 0xAFA )
		{
			Movable = false;
			Visible = true;
                        Hue = 336;
		}

                public override void OnDoubleClick( Mobile from )
		{

			if (!from.InRange( this.GetWorldLocation(), 1 ) )
			{
                               from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
			}
			else
			{
				from.Z -= 40;
			}
		}

		public CellarTeleporter1( Serial serial ) : base( serial )
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
	public class CellarTeleporter : AddonComponent
	{

		[Constructable]
		public CellarTeleporter() : this( false )
		{
		}

		[Constructable]
		public CellarTeleporter( bool creatures ) : base( 0x71F )
		{
			Movable = false;
			Visible = true;
			
                }
                
                public override bool OnMoveOver( Mobile m )
		{
                       {
                        m.Z += 40;
                        
                        return false;
                        }
                   return true;
                 }
                 
		public CellarTeleporter( Serial serial ) : base( serial )
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
