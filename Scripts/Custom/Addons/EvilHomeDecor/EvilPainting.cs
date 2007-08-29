using System;
using Server.Gumps;
using Server.Network;

namespace Server.Items
{
	public enum EvilPaintingType
	{
		LycanthropyEast,
		TheWatcherEast,
		WitheringBeautyEast,
		LycanthropySouth,
		TheWatcherSouth,
		WitheringBeautySouth
	}
	public class EvilPainting : AddonComponent
	{
		private int m_LabelNumber;
		private bool m_UpDown;
		private static readonly TimeSpan ChangeDelay = TimeSpan.FromSeconds( 8.0 );
		private DateTime m_NextChange;

		public override int LabelNumber{ get{ return m_LabelNumber; } }

		[Constructable]
		public EvilPainting( int itemID, int labelNumber ) : base( itemID )
		{
			m_LabelNumber = labelNumber;
			m_UpDown = true;
		}

		public override bool HandlesOnMovement{ get{ return true; } }

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( !(m.AccessLevel > AccessLevel.Player && m.Hidden) && Utility.InRange( m.Location, this.Location, 18 ) )
			{
				if ( m_LabelNumber == 1074480 )
				{
					if ( ItemID == 0x2A67 && m.Location.Y < this.Location.Y )
						NextImage( m );
					else if ( ItemID == 0x2A65 && m.Location.X < this.Location.X )
						NextImage( m );
					else if ( ItemID == 0x2A66 && m.Location.X > this.Location.X )
						NextImage( m );
					else if ( ItemID == 0x2A68 && m.Location.Y > this.Location.Y )
						NextImage( m );
				}
				else if ( DateTime.Now > m_NextChange && !(m.Hidden) )
				    NextImage( m );
			}
			base.OnMovement( m, oldLocation );
		}

		public void NextImage( Mobile m )
		{
			m_NextChange = DateTime.Now + ChangeDelay;

			if ( m_UpDown )
			{
				switch ( m_LabelNumber )
				{
					case 1074479:
					{
						++ItemID;
						if ( ItemID >= 0x2A64 || ItemID == 0x2A60 )
						{
							Effects.PlaySound( Location, Map, 0x569 );
							m_UpDown = false;
						}
						break;
					}
					case 1074480:
					{
						++ItemID;
						if ( m.Female )
							m.PlaySound( 0x568 );
						else
							m.PlaySound( 0x566 );
						m_UpDown = false;
						break;
					}
					case 1074481:
					{
						++ItemID;
						if ( ItemID >= 0x2A70 || ItemID == 0x2A6C )
						{
							Effects.PlaySound( Location, Map, 0x567 );
							m_UpDown = false;
						}
						break;
					}
				}
			}
			else
			{
				switch ( m_LabelNumber )
				{
					case 1074479:
					{
						--ItemID;
						if ( ItemID == 0x2A61 || ItemID <= 0x2A5D )
							m_UpDown = true;
						break;
					}
					case 1074480:
					{
						--ItemID;
						m_UpDown = true;
						break;
					}
					case 1074481:
					{
						--ItemID;
						if ( ItemID == 0x2A6D || ItemID <= 0x2A69 )
							m_UpDown = true;
						break;
					}
				}
			}
		}

		public EvilPainting( Serial serial ) : base( serial )
		{
		}
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
			
			writer.Write( (int) m_LabelNumber );
			writer.Write( (bool) m_UpDown );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
			
			m_LabelNumber = reader.ReadInt();
			m_UpDown = reader.ReadBool();
		}

	}
	public class EvilPaintingAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new EvilPaintingDeed(); } }

		[Constructable]
		public EvilPaintingAddon( EvilPaintingType evilpaintingtype )
		{
			switch ( evilpaintingtype )
			{
				case EvilPaintingType.LycanthropyEast:
				{
					AddComponent( new EvilPainting( 0x2A61, 1074479 ), 0, 0, 0 );
					break;
				}
				case EvilPaintingType.TheWatcherEast:
				{
					AddComponent( new EvilPainting( 0x2A67, 1074480 ), 0, 0, 0 );
					break;
				}
				case EvilPaintingType.WitheringBeautyEast:
				{
					AddComponent( new EvilPainting( 0x2A6D, 1074481 ), 0, 0, 0 );
					break;
				}
				case EvilPaintingType.LycanthropySouth:
				{
					AddComponent( new EvilPainting( 0x2A5D, 1074479 ), 0, 0, 0 );
					break;
				}
				case EvilPaintingType.TheWatcherSouth:
				{
					AddComponent( new EvilPainting( 0x2A65, 1074480 ), 0, 0, 0 );
					break;
				}
				case EvilPaintingType.WitheringBeautySouth:
				{
					AddComponent( new EvilPainting( 0x2A69, 1074481 ), 0, 0, 0 );
					break;
				}
			}
		}

		public EvilPaintingAddon( Serial serial ) : base( serial )
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

	public class EvilPaintingDeed : BaseAddonDeed
	{
		private EvilPaintingType m_EvilPaintingType;

		public override BaseAddon Addon{ get{ return new EvilPaintingAddon( m_EvilPaintingType ); } }

		[Constructable]
		public EvilPaintingDeed()
		{
			LootType = LootType.Blessed;
			Name = "A Deed For An Evil Painting";
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
			private EvilPaintingDeed m_Deed;

			public InternalGump( EvilPaintingDeed deed ) : base( 150, 50 )
			{
				m_Deed = deed;

				AddBackground( 0, 0, 350, 400, 0xA28 );

				AddItem( 90, 52, 0x2A61 );
				AddButton( 70, 35, 0x868, 0x869, 1, GumpButtonType.Reply, 0 ); // Lycanthropy East

				AddItem( 90, 152, 0x2A67 );
				AddButton( 70, 135, 0x868, 0x869, 2, GumpButtonType.Reply, 0 ); // The Watcher East

				AddItem( 90, 252, 0x2A6D );
				AddButton( 70, 235, 0x868, 0x869, 3, GumpButtonType.Reply, 0 ); // Withering Beauty East

				AddItem( 220, 52, 0x2A5D );
				AddButton( 185, 35, 0x868, 0x869, 4, GumpButtonType.Reply, 0 ); // Lycanthropy South

				AddItem( 220, 152, 0x2A65 );
				AddButton( 185, 135, 0x868, 0x869, 5, GumpButtonType.Reply, 0 ); // The Watcher South

				AddItem( 220, 252, 0x2A69 );
				AddButton( 185, 235, 0x868, 0x869, 6, GumpButtonType.Reply, 0 ); // Withering Beauty South
			}

			public override void OnResponse( NetState sender, RelayInfo info )
			{
				if ( m_Deed.Deleted || info.ButtonID == 0 || info.ButtonID > 6 )
					return;
				switch ( info.ButtonID )
				{
					case 1:
					{
						m_Deed.m_EvilPaintingType = EvilPaintingType.LycanthropyEast;
						break;
					}
					case 2:
					{
						m_Deed.m_EvilPaintingType = EvilPaintingType.TheWatcherEast;
						break;
					}
					case 3:
					{
						m_Deed.m_EvilPaintingType = EvilPaintingType.WitheringBeautyEast;
						break;
					}
					case 4:
					{
						m_Deed.m_EvilPaintingType = EvilPaintingType.LycanthropySouth;
						break;
					}
					case 5:
					{
						m_Deed.m_EvilPaintingType = EvilPaintingType.TheWatcherSouth;
						break;
					}
					case 6:
					{
						m_Deed.m_EvilPaintingType = EvilPaintingType.WitheringBeautySouth;
						break;
					}
				}
				m_Deed.SendTarget( sender.Mobile );
			}
		}

		public EvilPaintingDeed( Serial serial ) : base( serial )
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
