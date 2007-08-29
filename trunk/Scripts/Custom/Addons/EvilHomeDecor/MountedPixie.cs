using System;
using Server.Gumps;
using Server.Network;

namespace Server.Items
{
	public enum MountedPixieType
	{
		PixieGreenEast,
		PixieGreenSouth,
		PixieOrangeEast,
		PixieOrangeSouth,
		PixieBlueEast,
		PixieBlueSouth,
		PixieBrightGreenEast,
		PixieBrightGreenSouth,
		PixieWhiteEast,
		PixieWhiteSouth
	}
	public class MountedPixie : AddonComponent
	{
		private int m_LabelNumber;
		[CommandProperty( AccessLevel.GameMaster )]
		public int Number
		{
			get{ return m_LabelNumber; }
			set{ m_LabelNumber = value; InvalidateProperties(); }
		}

		public override int LabelNumber{ get{ return m_LabelNumber; } }

		[Constructable]
		public MountedPixie( int itemID, int labelNumber ) : base( itemID )
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
				switch ( ItemID )
				{
					case 0x2A71:
					case 0x2A72:
					{
						Effects.PlaySound( Location, Map, Utility.RandomList( 0x554, 0x555, 0x556 ) );
						break;
					}
					case 0x2A73:
					case 0x2A74:
					{
						Effects.PlaySound( Location, Map, Utility.RandomList( 0x557, 0x558, 0x559 ) );
						break;
					}
					case 0x2A75:
					case 0x2A76:
					{
						Effects.PlaySound( Location, Map, Utility.RandomList( 0x55A, 0x55B, 0x55C ) );
						break;
					}
					case 0x2A77:
					case 0x2A78:
					{
						Effects.PlaySound( Location, Map, Utility.RandomList( 0x55D, 0x55E, 0x55F ) );
						break;
					}
					case 0x2A79:
					case 0x2A7A:
					{
						Effects.PlaySound( Location, Map, Utility.RandomList( 0x560, 0x561, 0x562 ) );
						break;
					}
				}
			}
		}

		public MountedPixie( Serial serial ) : base( serial )
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
	public class MountedPixieAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new MountedPixieDeed(); } }

		[Constructable]
		public MountedPixieAddon( MountedPixieType mountedpixietype )
		{
			switch ( mountedpixietype )
			{
				case MountedPixieType.PixieGreenEast:
				{
					AddComponent( new MountedPixie( 0x2A71, 1074482 ), 0, 0, 0 );
					break;
				}
				case MountedPixieType.PixieGreenSouth:
				{
					AddComponent( new MountedPixie( 0x2A72, 1074482 ), 0, 0, 0 );
					break;
				}
				case MountedPixieType.PixieOrangeEast:
				{
					AddComponent( new MountedPixie( 0x2A73, 1074482 ), 0, 0, 0 );
					break;
				}
				case MountedPixieType.PixieOrangeSouth:
				{
					AddComponent( new MountedPixie( 0x2A74, 1074482 ), 0, 0, 0 );
					break;
				}
				case MountedPixieType.PixieBlueEast:
				{
					AddComponent( new MountedPixie( 0x2A75, 1074482 ), 0, 0, 0 );
					break;
				}
				case MountedPixieType.PixieBlueSouth:
				{
					AddComponent( new MountedPixie( 0x2A76, 1074482 ), 0, 0, 0 );
					break;
				}
				case MountedPixieType.PixieBrightGreenEast:
				{
					AddComponent( new MountedPixie( 0x2A77, 1074482 ), 0, 0, 0 );
					break;
				}
				case MountedPixieType.PixieBrightGreenSouth:
				{
					AddComponent( new MountedPixie( 0x2A78, 1074482 ), 0, 0, 0 );
					break;
				}
				case MountedPixieType.PixieWhiteEast:
				{
					AddComponent( new MountedPixie( 0x2A79, 1074482 ), 0, 0, 0 );
					break;
				}
				case MountedPixieType.PixieWhiteSouth:
				{
					AddComponent( new MountedPixie( 0x2A7A, 1074482 ), 0, 0, 0 );
					break;
				}
			}
		}

		public MountedPixieAddon( Serial serial ) : base( serial )
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

	public class MountedPixieDeed : BaseAddonDeed
	{
		private MountedPixieType m_MountedPixieType;

		public override BaseAddon Addon{ get{ return new MountedPixieAddon( m_MountedPixieType ); } }

		[Constructable]
		public MountedPixieDeed()
		{
			LootType = LootType.Blessed;
			Name = "A Deed For A Mounted Pixie";
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
			private MountedPixieDeed m_Deed;

			public InternalGump( MountedPixieDeed deed ) : base( 150, 50 )
			{
				m_Deed = deed;

				AddBackground( 0, 0, 350, 500, 0xA28 );

				AddItem( 90, 52, 0x2A71 );
				AddButton( 70, 35, 0x868, 0x869, 1, GumpButtonType.Reply, 0 ); // PixieGreenEast

				AddItem( 220, 52, 0x2A72 );
				AddButton( 185, 35, 0x868, 0x869, 2, GumpButtonType.Reply, 0 ); // PixieGreenSouth

				AddItem( 90, 135, 0x2A73 );
				AddButton( 70, 118, 0x868, 0x869, 3, GumpButtonType.Reply, 0 ); // PixieOrangeEast

				AddItem( 220, 135, 0x2A74 );
				AddButton( 185, 118, 0x868, 0x869, 4, GumpButtonType.Reply, 0 ); // PixieOrangeSouth

				AddItem( 90, 243, 0x2A75 );
				AddButton( 70, 226, 0x868, 0x869, 5, GumpButtonType.Reply, 0 ); // PixieBlueEast

				AddItem( 220, 243, 0x2A76 );
				AddButton( 185, 226, 0x868, 0x869, 6, GumpButtonType.Reply, 0 ); // PixieBlueSouth

				AddItem( 90, 326, 0x2A77 );
				AddButton( 70, 309, 0x868, 0x869, 7, GumpButtonType.Reply, 0 ); // PixieBrightGreenEast

				AddItem( 220, 326, 0x2A78 );
				AddButton( 185, 309, 0x868, 0x869, 8, GumpButtonType.Reply, 0 ); //	PixieBrightGreenSouth

				AddItem( 90, 409, 0x2A79 );
				AddButton( 70, 392, 0x868, 0x869, 9, GumpButtonType.Reply, 0 ); // PixieWhiteEast

				AddItem( 220, 409, 0x2A7A );
				AddButton( 185, 392, 0x868, 0x869, 10, GumpButtonType.Reply, 0 ); // PixieWhiteSouth
			}

			public override void OnResponse( NetState sender, RelayInfo info )
			{
				if ( m_Deed.Deleted || info.ButtonID == 0 || info.ButtonID > 10 )
					return;
				switch ( info.ButtonID )
				{
					case 1:
					{
						m_Deed.m_MountedPixieType = MountedPixieType.PixieGreenEast;
						break;
					}
					case 2:
					{
						m_Deed.m_MountedPixieType = MountedPixieType.PixieGreenSouth;
						break;
					}
					case 3:
					{
						m_Deed.m_MountedPixieType = MountedPixieType.PixieOrangeEast;
						break;
					}
					case 4:
					{
						m_Deed.m_MountedPixieType = MountedPixieType.PixieOrangeSouth;
						break;
					}
					case 5:
					{
						m_Deed.m_MountedPixieType = MountedPixieType.PixieBlueEast;
						break;
					}
					case 6:
					{
						m_Deed.m_MountedPixieType = MountedPixieType.PixieBlueSouth;
						break;
					}
					case 7:
					{
						m_Deed.m_MountedPixieType = MountedPixieType.PixieBrightGreenEast;
						break;
					}
					case 8:
					{
						m_Deed.m_MountedPixieType = MountedPixieType.PixieBrightGreenSouth;
						break;
					}
					case 9:
					{
						m_Deed.m_MountedPixieType = MountedPixieType.PixieWhiteEast;
						break;
					}
					case 10:
					{
						m_Deed.m_MountedPixieType = MountedPixieType.PixieWhiteSouth;
						break;
					}
				}
				m_Deed.SendTarget( sender.Mobile );
			}
		}

		public MountedPixieDeed( Serial serial ) : base( serial )
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
