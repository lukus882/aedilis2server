using System;
using Server.Gumps;
using Server.Network;
using Server.Mobiles;

namespace Server.Items
{
	public class BedOfNails : AddonComponent
	{
		public override int LabelNumber{ get{ return 1074484; } }

		public BedOfNails( int itemID ) : base( itemID )
		{
		}

		public override bool OnMoveOver( Mobile from )
		{
			if ( base.OnMoveOver( from ) )
			{
				if ( from.Alive && !(from.AccessLevel > AccessLevel.Player && from.Hidden) )
				{
					int hurtSound;
					if ( from is BaseCreature )
						hurtSound = ((BaseCreature)from).GetHurtSound();
					else if ( from.Female )
						hurtSound = Utility.RandomBool() ? 1339 : 1341;
					else
						hurtSound = Utility.RandomBool() ? 1342 : 1346;

					from.PlaySound( hurtSound );
					new Blood( Utility.RandomList( 0x122A, 0x122B, 0x122C, 0x122D, 0x122E, 0x1645 ) ).MoveToWorld( from.Location, from.Map );
					new Blood( Utility.RandomList( 0x122A, 0x122B, 0x122C, 0x122D, 0x122E, 0x1645 ) ).MoveToWorld( new Point3D( X, Y, Z + 2 ), Map );
				}

				return true;
			}

			return false;
		}

		public BedOfNails( Serial serial ) : base( serial )
		{
		}
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}

	}
	public class BedOfNailsAddon : BaseAddon
	{
		private bool m_UpDown;
		private DateTime m_NextChange;

		public override BaseAddonDeed Deed{ get{ return new BedOfNailsDeed(); } }

		[Constructable]
		public BedOfNailsAddon() : this( true )
		{
		}

		[Constructable]
		public BedOfNailsAddon( bool east )
		{
			if( east )
			{
				AddComponent( new BedOfNails( 0x2A82 ), 0, 0, 0 );
				AddComponent( new BedOfNails( 0x2A81 ), 0, 1, 0 );
			}
			else
			{
				AddComponent( new BedOfNails( 0x2A8A ), 0, 0, 0 );
				AddComponent( new BedOfNails( 0x2A89 ), 1, 0, 0 );
			}
		}

		public override bool HandlesOnMovement{ get{ return true; } }

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( DateTime.Now < m_NextChange )
				return;

			if ( !m.Hidden && Utility.InRange( m.Location, Location, 19 ) )
			{
				if ( m_UpDown )
				{
					if ( Components[1].ItemID >= 0x2A8F || Components[1].ItemID == 0x2A88 || Components[1].ItemID == 0x2A87 )
					{
						m_UpDown = false;
						Components[0].ItemID -= 2;
						Components[1].ItemID -= 2;
					}
					else
					{
						Components[0].ItemID += 2;
						Components[1].ItemID += 2;
					}
				}
				else
				{
					if ( Components[1].ItemID <= 0x2A82 || Components[1].ItemID == 0x2A89 || Components[1].ItemID == 0x2A8A )
					{
						m_UpDown = true;
						Components[0].ItemID += 2;
						Components[1].ItemID += 2;
					}
					else
					{
						Components[0].ItemID -= 2;
						Components[1].ItemID -= 2;
					}
				}

				m_NextChange = DateTime.Now + TimeSpan.FromSeconds( 1.0 );
			}

			base.OnMovement( m, oldLocation );
		}

		public BedOfNailsAddon( Serial serial ) : base( serial )
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

	public class BedOfNailsDeed : BaseAddonDeed
	{
		private bool m_East;

		public override BaseAddon Addon{ get{ return new BedOfNailsAddon( m_East ); } }

		[Constructable]
		public BedOfNailsDeed()
		{
			LootType = LootType.Blessed;
			Name = "A Deed For A Bed Of Nails";
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
			private BedOfNailsDeed m_Deed;

			public InternalGump( BedOfNailsDeed deed ) : base( 150, 50 )
			{
				m_Deed = deed;

				AddBackground( 0, 0, 350, 350, 0xA28 );

				AddItem( 119, 50, 0x2A82 );
				AddItem( 95, 89, 0x2A81 );
				AddButton( 70, 35, 0x868, 0x869, 1, GumpButtonType.Reply, 0 ); // BedOfNailsEast

				AddItem( 210, 50, 0x2A8A );
				AddItem( 228, 86, 0x2A89 );
				AddButton( 185, 35, 0x868, 0x869, 2, GumpButtonType.Reply, 0 ); // BedOfNailsSouth
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

		public BedOfNailsDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}
