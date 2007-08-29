using System;
using Server.Gumps;
using Server.Network;

namespace Server.Items
{
	public class HauntedMirror : AddonComponent
	{
		private int m_LabelNumber;
		private Mobile m_LastWatcher;
		private static readonly TimeSpan InsultDelay = TimeSpan.FromSeconds( 8.0 );
		private DateTime m_NextInsult;

		public override int LabelNumber{ get{ return m_LabelNumber; } }

		[Constructable]
		public HauntedMirror( int itemID, int labelNumber ) : base( itemID )
		{
			m_LabelNumber = labelNumber;
		}

		public override bool HandlesOnMovement{ get{ return true; } }

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( !(m.Hidden) && Utility.InRange( m.Location, this.Location, 1 ) && (m.Location.Z < this.Location.Z + 4 || m.Location.Z > this.Location.Z - 4) )
			{
				if ( (ItemID == 0x2A7E || ItemID == 0x2A7D) && m.Location.X >= this.Location.X )
				{
					m_LastWatcher = m;
					ItemID = 0x2A7E;
					if ( DateTime.Now > m_NextInsult )
					{
						m_NextInsult = DateTime.Now + InsultDelay;
						Effects.PlaySound( Location, Map, Utility.RandomList( 0x54E, 0x54F, 0x550, 0x551, 0x552, 0x553 ) );
					}
				}
				else if ( (ItemID == 0x2A7C || ItemID == 0x2A7B) && m.Location.Y >= this.Location.Y )
				{
					m_LastWatcher = m;
					ItemID = 0x2A7C;
					if ( DateTime.Now > m_NextInsult )
					{
						m_NextInsult = DateTime.Now + InsultDelay;
						Effects.PlaySound( Location, Map, Utility.RandomList( 0x54E, 0x54F, 0x550, 0x551, 0x552, 0x553 ) );
					}
				}
				else
				{
					if ( ItemID == 0x2A7E )
						ItemID = 0x2A7D;
					else if ( ItemID == 0x2A7C )
						ItemID = 0x2A7B;
				}		
			}
			else if ( Utility.InRange( m.Location, this.Location, 13 ) && m == m_LastWatcher )
			{
				m_LastWatcher = null;
				if ( ItemID == 0x2A7E )
					ItemID = 0x2A7D;
				else if ( ItemID == 0x2A7C )
					ItemID = 0x2A7B;
			}

			base.OnMovement( m, oldLocation );
		}
		public HauntedMirror( Serial serial ) : base( serial )
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
	public class HauntedMirrorAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new HauntedMirrorDeed(); } }

		[Constructable]
		public HauntedMirrorAddon( bool east )
		{
			if( east )
			{
				AddComponent( new HauntedMirror( 0x2A7D, 1074483 ), 0, 0, 0 );
			}
			else
			{
				AddComponent( new HauntedMirror( 0x2A7B, 1074483 ), 0, 0, 0 );
			}
		}

		public HauntedMirrorAddon( Serial serial ) : base( serial )
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

	public class HauntedMirrorDeed : BaseAddonDeed
	{
		private bool m_East;

		public override BaseAddon Addon{ get{ return new HauntedMirrorAddon( m_East ); } }

		[Constructable]
		public HauntedMirrorDeed()
		{
			LootType = LootType.Blessed;
			Name = "A Deed For A Haunted Mirror";
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
			private HauntedMirrorDeed m_Deed;

			public InternalGump( HauntedMirrorDeed deed ) : base( 150, 50 )
			{
				m_Deed = deed;

				AddBackground( 0, 0, 350, 350, 0xA28 );

				AddItem( 90, 52, 0x2A7D );
				AddButton( 70, 35, 0x868, 0x869, 1, GumpButtonType.Reply, 0 ); // HauntedMirrorEast

				AddItem( 220, 52, 0x2A7B );
				AddButton( 185, 35, 0x868, 0x869, 2, GumpButtonType.Reply, 0 ); // HauntedMirrorSouth
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

		public HauntedMirrorDeed( Serial serial ) : base( serial )
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