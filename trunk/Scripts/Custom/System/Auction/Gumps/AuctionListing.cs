using System;
using System.Collections;

using Server;
using Server.Gumps;

namespace Arya.Auction
{
	/// <summary>
	/// Lists auction items
	/// </summary>
	public class AuctionListing : Gump
	{
		private const int LabelHue = 0x480;
		private const int GreenHue = 0x40;
		private const int RedHue = 0x20;
		private ArrayList m_Buttons;

		private bool m_EnableSearch;
		private int m_Page;
		private ArrayList m_List;
		private bool m_ReturnToAuction;

		public AuctionListing( Mobile m, ArrayList items, bool searchEnabled, bool returnToAuction, int page ) : base( 50, 50 )
		{
			m.CloseGump( typeof( AuctionListing ) );
			m_EnableSearch = searchEnabled;
			m_Page = page;
			m_List = new ArrayList( items );
			m_ReturnToAuction = returnToAuction;

			MakeGump();
		}

		public AuctionListing( Mobile m, ArrayList items, bool searchEnabled, bool returnToAuction ) : this( m, items, searchEnabled, returnToAuction, 0 )
		{
		}

		private void MakeGump()
		{
			m_Buttons = new ArrayList();

			this.Closable=true;
			m_Buttons.Add( 0 );

			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			this.AddPage(0);

			this.AddImageTiled(49, 39, 402, 352, 3004);
			this.AddImageTiled(50, 40, 400, 350, 2624);
			this.AddAlphaRegion(50, 40, 400, 350);
			this.AddImage(165, 65, 10452);
			this.AddImage(0, 20, 10400);
			this.AddImage(0, 330, 10402);
			this.AddImage(35, 20, 10420);
			this.AddImage(421, 20, 10410);
			this.AddImage(410, 20, 10430);
			this.AddImageTiled(90, 32, 323, 16, 10254);
			this.AddLabel(160, 45, GreenHue, AuctionSystem.ST[ 8 ] );
			this.AddImage(420, 330, 10412);
			this.AddImage(420, 175, 10411);
			this.AddImage(0, 175, 10401);

			// Search: BUTTON 1
			if ( m_EnableSearch )
			{
				this.AddLabel(305, 120, LabelHue, AuctionSystem.ST[ 16 ] );
				this.AddButton(270, 120, 4005, 4006, 1, GumpButtonType.Reply, 0);
				m_Buttons.Add( 1 );
			}

			// Sort: BUTTON 2
			this.AddLabel(395, 120, LabelHue, AuctionSystem.ST[ 17 ] );
			this.AddButton(360, 120, 4005, 4006, 2, GumpButtonType.Reply, 0);
			m_Buttons.Add( 2 );

			while ( m_Page * 10 >= m_List.Count )
				m_Page--;

			if ( m_List.Count > 0 )
			{
				// Display the page number
				this.AddLabel( 360, 95, RedHue, string.Format( AuctionSystem.ST[ 18 ] , m_Page + 1, ( m_List.Count - 1 ) / 10 + 1 ) );

				this.AddLabel(70, 120, RedHue, string.Format( AuctionSystem.ST[ 19 ] , m_List.Count ) );
			}
			else
				this.AddLabel( 70, 120, RedHue, AuctionSystem.ST[ 20 ] );

			// Display items: BUTTONS 10 + i

			int lower = m_Page * 10;

			if ( m_List.Count > 0 )
			{
				for ( int i = 0; i < 10 && ( m_Page * 10 + i ) < m_List.Count; i++ )
				{
					AuctionItem item = m_List[ m_Page * 10 + i ] as AuctionItem;

					this.AddButton(115, 153 + i * 20, 5601, 5605, 10 + i, GumpButtonType.Reply, 0);
					m_Buttons.Add( 10 + i );
					this.AddLabelCropped( 140, 150 + i * 20, 260, 20, LabelHue, item.ItemName );
				}
			}

			// Next page: BUTTON 3
			if ( ( m_Page + 1 ) * 10 < m_List.Count )
			{
				this.AddLabel(355, 360, LabelHue, AuctionSystem.ST[ 22 ] );
				this.AddButton(315, 360, 4005, 4006, 3, GumpButtonType.Reply, 0);
				m_Buttons.Add( 3 );
			}

			// Previous page: BUTTON 4
			if ( m_Page > 0 )
			{
				this.AddLabel(180, 360, LabelHue, AuctionSystem.ST[ 21 ] );
				this.AddButton(280, 360, 4014, 4015, 4, GumpButtonType.Reply, 0);
				m_Buttons.Add( 4 );
			}
			
			// Close: BUTTON 0
			this.AddLabel(115, 360, LabelHue, AuctionSystem.ST[ 7 ] );
			this.AddButton(75, 360, 4017, 4018, 0, GumpButtonType.Reply, 0);
		}

		public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
		{
			if ( ! m_Buttons.Contains( info.ButtonID ) )
			{
				Console.WriteLine( @"The auction system located a potential exploit. 
					Player {0} (Acc. {1}) tried to press an unregistered button in a gump of type: {2}",
					sender.Mobile != null ? sender.Mobile.ToString() : "Unkown",
					sender.Mobile != null && sender.Mobile.Account != null ? ( sender.Mobile.Account as Server.Accounting.Account ).Username : "Unkown",
					this.GetType().Name );

				return;
			}

			if ( ! AuctionSystem.Running )
			{
				sender.Mobile.SendMessage( AuctionSystem.MessageHue, AuctionSystem.ST[ 15 ] );
				return;
			}

			switch( info.ButtonID )
			{
				case 0: // Exit

					if ( m_ReturnToAuction )
						sender.Mobile.SendGump( new AuctionGump( sender.Mobile ) );
					else
						sender.Mobile.SendGump( new MyAuctionGump( sender.Mobile, null ) );
					break;

				case 1: // Search

					sender.Mobile.SendGump( new AuctionSearchGump(
						sender.Mobile,
						m_List,
						m_ReturnToAuction ) );
					break;

				case 2: // Sort

					sender.Mobile.SendGump( new AuctionSortGump(
						sender.Mobile,
						m_List,
						m_ReturnToAuction,
						m_EnableSearch ) );
					break;

				case 3: // Next page

					sender.Mobile.SendGump( new AuctionListing( sender.Mobile, m_List, m_EnableSearch, m_ReturnToAuction, m_Page +1 ) );
					break;

				case 4: // Previous page

					sender.Mobile.SendGump( new AuctionListing( sender.Mobile, m_List, m_EnableSearch, m_ReturnToAuction, m_Page - 1 ) );
					break;

				default:

					int index = m_Page * 10 + info.ButtonID - 10;

					if ( index < 0 || index >= m_List.Count )
					{
						// Apparently in some cases this can go out of bounds, investigating.

						sender.Mobile.SendMessage( AuctionSystem.MessageHue, AuctionSystem.ST[ 23 ] );

						if ( m_ReturnToAuction )
							sender.Mobile.SendGump( new AuctionGump( sender.Mobile ) );
						else
							sender.Mobile.SendGump( new MyAuctionGump( sender.Mobile, null ) );
						
						return;
					}

					AuctionItem item = m_List[ index ] as AuctionItem;

					if ( item != null )
					{
						if ( ( ! item.Expired || item.Pending ) && ( AuctionSystem.Auctions.Contains( item ) || AuctionSystem.Pending.Contains( item ) ) )
						{
							sender.Mobile.SendGump( new AuctionViewGump( sender.Mobile, item, new AuctionGumpCallback( ViewCallback ) ) );
						}
						else
						{
							sender.Mobile.SendMessage( AuctionSystem.MessageHue, AuctionSystem.ST[ 24 ] );
							sender.Mobile.SendGump( new AuctionListing( sender.Mobile, m_List, m_EnableSearch, m_ReturnToAuction, m_Page ) );
						}
					}

					break;
			}
		}

		private void ViewCallback( Mobile user )
		{
			user.SendGump( new AuctionListing( user, m_List, m_EnableSearch, m_ReturnToAuction, m_Page ) );
		}
	}
}