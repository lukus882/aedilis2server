using System;
using Server.Gumps;
using Server.Network;

namespace Server.Items
{
	public enum EvilFurnitureType
	{
		ChairEast,
		ChairSouth,
		CouchEast,
		CouchSouth,
		Desk
	}
	public class EvilFurniture : AddonComponent
	{
		private int m_LabelNumber;

		public override int LabelNumber{ get{ return m_LabelNumber; } }

		[Constructable]
		public EvilFurniture( int itemID, int labelNumber ) : base( itemID )
		{
			m_LabelNumber = labelNumber;
		}

		public override bool OnMoveOver( Mobile from )
		{
			if ( from.Alive && !(from.AccessLevel > AccessLevel.Player && from.Hidden) )
				Effects.PlaySound( from.Location, from.Map, Utility.RandomList( 0x545, 0x548, 0x54D, 0x54B, 0x54C ) );

			return base.OnMoveOver( from );
		}

		public EvilFurniture( Serial serial ) : base( serial )
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

	}
	public class EvilFurnitureAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new EvilFurnitureDeed(); } }

		[Constructable]
		public EvilFurnitureAddon( EvilFurnitureType evilfurnituretype )
		{
			switch ( evilfurnituretype )
			{
				case EvilFurnitureType.ChairEast:
				{
					AddComponent( new EvilFurniture( 0x2A59, 1074476 ), 0, 0, 0 );
					break;
				}
				case EvilFurnitureType.ChairSouth:
				{
					AddComponent( new EvilFurniture( 0x2A58, 1074476 ), 0, 0, 0 );
					break;
				}
				case EvilFurnitureType.CouchEast:
				{
					AddComponent( new EvilFurniture( 0x2A80, 1074477 ), 0, 0, 0 );
					AddComponent( new EvilFurniture( 0x2A7F, 1074477 ), 0, 1, 0 );
					break;
				}
				case EvilFurnitureType.CouchSouth:
				{
					AddComponent( new EvilFurniture( 0x2A5B, 1074477 ), 0, 0, 0 );
					AddComponent( new EvilFurniture( 0x2A5A, 1074477 ), 1, 0, 0 );
					break;
				}
				case EvilFurnitureType.Desk:
				{
					AddComponent( new EvilFurniture( 0x2A5C, 1074478 ), 0, 0, 0 ); // yes you could stand on the desk on OSITestshard
					break;
				}
			}
		}

		public EvilFurnitureAddon( Serial serial ) : base( serial )
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

	public class EvilFurnitureDeed : BaseAddonDeed
	{
		private EvilFurnitureType m_EvilFurnitureType;

		public override BaseAddon Addon{ get{ return new EvilFurnitureAddon( m_EvilFurnitureType ); } }

		[Constructable]
		public EvilFurnitureDeed()
		{
			LootType = LootType.Blessed;
			Name = "A Deed For An Evil Furniture";
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
			private EvilFurnitureDeed m_Deed;

			public InternalGump( EvilFurnitureDeed deed ) : base( 150, 50 )
			{
				m_Deed = deed;

				AddBackground( 0, 0, 350, 400, 0xA28 );

				AddItem( 90, 52, 0x2A59 );
				AddButton( 70, 35, 0x868, 0x869, 1, GumpButtonType.Reply, 0 ); // ChairEast

				AddItem( 220, 52, 0x2A58 );
				AddButton( 185, 35, 0x868, 0x869, 2, GumpButtonType.Reply, 0 ); // ChairSouth

				AddItem( 90, 160, 0x2A80 );
				AddItem( 75, 175, 0x2A7F );
				AddButton( 70, 143, 0x868, 0x869, 3, GumpButtonType.Reply, 0 ); // CouchEast

				AddItem( 220, 160, 0x2A5B );
				AddItem( 240, 185, 0x2A5A );
				AddButton( 185, 143, 0x868, 0x869, 4, GumpButtonType.Reply, 0 ); // CouchSouth

				AddItem( 90, 285, 0x2A5C );
				AddButton( 70, 268, 0x868, 0x869, 5, GumpButtonType.Reply, 0 ); // Desk
			}

			public override void OnResponse( NetState sender, RelayInfo info )
			{
				if ( m_Deed.Deleted || info.ButtonID == 0 || info.ButtonID > 5 )
					return;
				switch ( info.ButtonID )
				{
					case 1:
					{
						m_Deed.m_EvilFurnitureType = EvilFurnitureType.ChairEast;
						break;
					}
					case 2:
					{
						m_Deed.m_EvilFurnitureType = EvilFurnitureType.ChairSouth;
						break;
					}
					case 3:
					{
						m_Deed.m_EvilFurnitureType = EvilFurnitureType.CouchEast;
						break;
					}
					case 4:
					{
						m_Deed.m_EvilFurnitureType = EvilFurnitureType.CouchSouth;
						break;
					}
					case 5:
					{
						m_Deed.m_EvilFurnitureType = EvilFurnitureType.Desk;
						break;
					}
				}
				m_Deed.SendTarget( sender.Mobile );
			}
		}

		public EvilFurnitureDeed( Serial serial ) : base( serial )
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
