using System;
using Server.Gumps;
using Server.Network;
using Server.Targeting;

namespace Server.Items
{
	public class SacrificialAltar : AddonComponent
	{
		private int m_LabelNumber;
		public override int LabelNumber{ get{ return m_LabelNumber; } }

		[Constructable]
		public SacrificialAltar( int itemID, int labelNumber ) : base( itemID )
		{
			m_LabelNumber = labelNumber;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( this.GetWorldLocation(), 2 ) )
			{
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
			}
			else
			{
				from.SendLocalizedMessage( 1049485 ); // What would you like to sacrifice?
				from.Target = new InternalTarget( this );
			}
		}

		public SacrificialAltar( Serial serial ) : base( serial )
		{
		}
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version

			writer.Write( (int) m_LabelNumber );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();

			m_LabelNumber = reader.ReadInt();
		}

		private class InternalTarget : Target
		{
			private SacrificialAltar m_Altar;
			private Point3D FixedAltarLocation;
			private Point3D FixedAltarLocation2;

			public InternalTarget( SacrificialAltar altar ) : base( 1, false, TargetFlags.None )
			{
				m_Altar = altar;
			}

			protected override void OnTarget( Mobile from, object targeted ) 
			{
				if ( (targeted is Item) && ((Item)targeted).IsChildOf( from.Backpack ) )
				{

					switch ( m_Altar.ItemID )
					{
						case 0x2A9D:
						{
							FixedAltarLocation = new Point3D( m_Altar.Location.X, m_Altar.Location.Y, m_Altar.Location.Z +8 );
							FixedAltarLocation2 = new Point3D( m_Altar.Location.X, m_Altar.Location.Y +1, m_Altar.Location.Z +8 );
							break;
						}
						case 0x2A9C:
						{
							FixedAltarLocation = new Point3D( m_Altar.Location.X, m_Altar.Location.Y -1, m_Altar.Location.Z +8 );
							FixedAltarLocation2 = new Point3D( m_Altar.Location.X, m_Altar.Location.Y, m_Altar.Location.Z +8 );
							break;
						}
						case 0x2A9B:
						{
							FixedAltarLocation = new Point3D( m_Altar.Location.X +1, m_Altar.Location.Y, m_Altar.Location.Z +8 );
							FixedAltarLocation2 = new Point3D( m_Altar.Location.X, m_Altar.Location.Y, m_Altar.Location.Z +8 );
							break;
						}
						case 0x2A9A:
						{
							FixedAltarLocation = new Point3D( m_Altar.Location.X, m_Altar.Location.Y, m_Altar.Location.Z +8 );
							FixedAltarLocation2 = new Point3D( m_Altar.Location.X -1, m_Altar.Location.Y, m_Altar.Location.Z +8 );
							break;
						}
					}
					Effects.SendLocationEffect( FixedAltarLocation, m_Altar.Map, 0x3709, 30, 10 );
					Effects.SendLocationEffect( FixedAltarLocation2, m_Altar.Map, 0x3709, 30, 10 );
					Effects.PlaySound( FixedAltarLocation, m_Altar.Map, 0x569 );
					((Item)targeted).MoveToWorld( FixedAltarLocation, m_Altar.Map );
					Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), new TimerStateCallback( m_Altar.meh_Callback ), new object[]{ targeted } );
				}
				else
					from.SendLocalizedMessage( 1049486 ); // You can only sacrifice items that are in your backpack!
			}
		}
		public virtual void meh_Callback( object state )
		{
			object[] states = (object[])state;

			object t = (object)states[0];

			((Item)t).Delete();
		}
	}
	public class SacrificialAltarAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new SacrificialAltarDeed(); } }

		[Constructable]
		public SacrificialAltarAddon( bool east )
		{
			if( east )
			{
				AddComponent( new SacrificialAltar( 0x2A9D, 1074818 ), 0, 0, 0 );
				AddComponent( new SacrificialAltar( 0x2A9C, 1074818 ), 0, 1, 0 );
			}
			else
			{
				AddComponent( new SacrificialAltar( 0x2A9B, 1074818 ), 0, 0, 0 );
				AddComponent( new SacrificialAltar( 0x2A9A, 1074818 ), 1, 0, 0 );
			}
		}

		public SacrificialAltarAddon( Serial serial ) : base( serial )
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

	public class SacrificialAltarDeed : BaseAddonDeed
	{
		private bool m_East;

		public override BaseAddon Addon{ get{ return new SacrificialAltarAddon( m_East ); } }

		[Constructable]
		public SacrificialAltarDeed()
		{
			LootType = LootType.Blessed;
			Name = "A Deed For A Sacrificial Altar";
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) )
			{
				from.CloseGump( typeof( InternalGump ) );
				from.SendGump( new InternalGump( this ) );
			}
			else
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
		}

		private void SendTarget( Mobile m )
		{
			base.OnDoubleClick( m );
		}

		private class InternalGump : Gump
		{
			private SacrificialAltarDeed m_Deed;

			public InternalGump( SacrificialAltarDeed deed ) : base( 150, 50 )
			{
				m_Deed = deed;

				AddBackground( 0, 0, 350, 350, 0xA28 );

				AddItem( 125, 50, 0x2A9D );
				AddItem( 107, 84, 0x2A9C );
				AddButton( 70, 35, 0x868, 0x869, 1, GumpButtonType.Reply, 0 ); // SacrificialAltarEast

				AddItem( 210, 50, 0x2A9B );
				AddItem( 231, 83, 0x2A9A );
				AddButton( 185, 35, 0x868, 0x869, 2, GumpButtonType.Reply, 0 ); // SacrificialAltarSouth
			}

			public override void OnResponse( NetState sender, RelayInfo info )
			{
				if ( m_Deed.Deleted || info.ButtonID == 0 || info.ButtonID > 2 )
					return;
				switch ( info.ButtonID )
				{
					case 1:
					{
						m_Deed.m_East = true;
						break;
					}
					case 2:
					{
						m_Deed.m_East = false;
						break;
					}
				}
				m_Deed.SendTarget( sender.Mobile );
			}
		}

		public SacrificialAltarDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}
