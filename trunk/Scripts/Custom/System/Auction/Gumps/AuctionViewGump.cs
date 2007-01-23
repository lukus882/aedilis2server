using System;
using System.Collections;

using Server;
using Server.Gumps;

namespace Arya.Auction
{
	/// <summary>
	/// This gump displays the general information about an auction
	/// </summary>
	public class AuctionViewGump : Gump
	{
		private const int LabelHue = 0x480;
		private const int GreenHue = 0x40;
		private const int RedHue = 0x20;
		private ArrayList m_Buttons;

		private Mobile m_User;
		private AuctionItem m_Auction;
		private int m_Page = 0;
		private AuctionGumpCallback m_Callback;

		public AuctionViewGump( Mobile user, AuctionItem auction ) : this( user, auction, null )
		{
		}

		public AuctionViewGump( Mobile user, AuctionItem auction, AuctionGumpCallback callback ) : this( user, auction, callback, 0 )
		{
		}

		public AuctionViewGump( Mobile user, AuctionItem auction, AuctionGumpCallback callback, int page ) : base( 50, 50 )
		{
			m_Page = page;
			m_User = user;
			m_Auction = auction;
			m_Callback = callback;

			MakeGump();
		}

		private void MakeGump()
		{
			m_Buttons = new ArrayList();

			AuctionItem.ItemInfo item = m_Auction[ m_Page ];

			if ( item == null )
				return;

			this.Closable=true;
			m_Buttons.Add( 0 );

			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			this.AddPage(0);
			this.AddBackground( 0, 0, 501, 370, 9270 );
			this.AddImageTiled(4, 4, 492, 362, 2524);
			this.AddImageTiled(5, 5, 490, 360, 2624);
			this.AddAlphaRegion(5, 5, 490, 360);

			// Item image goes here
			if ( item.Item != null )
				this.AddItem( 15, 15, item.Item.ItemID, 0 );

			this.AddImageTiled(5, 169, 255, 20, 2524);
			this.AddImageTiled(5, 170, 155, 195, 2624);
			this.AddImageTiled(159, 5, 20, 184, 2524);
			this.AddImageTiled(160, 5, 335, 360, 2624);
			
			this.AddAlphaRegion(160, 5, 335, 360);
			this.AddAlphaRegion(5, 170, 155, 195);
			this.AddLabel(170, 10, GreenHue, m_Auction.ItemName );
			this.AddImageTiled(169, 29, 317, 142, 2524);
			this.AddImageTiled(170, 30, 315, 140, 2624);
			this.AddAlphaRegion(170, 30, 315, 140);
			this.AddLabel(210, 30, LabelHue, string.Format( AuctionSystem.ST[ 67 ] , m_Auction.ItemCount ) );
			this.AddImageTiled(209, 49, 237, 122, 2524);

			// Prev Item button: 1
			if ( m_Page > 0 )
			{
				this.AddButton(175, 35, 4014, 4015, 1, GumpButtonType.Reply, 0);
				m_Buttons.Add( 1 );
			}

			// Next Item button: 2
			if ( m_Page < m_Auction.ItemCount - 1 )
			{
				this.AddButton(450, 35, 4005, 4006, 2, GumpButtonType.Reply, 0);
				m_Buttons.Add( 2 );
			}

			this.AddImageTiled(210, 50, 235, 120, 2624);
			this.AddAlphaRegion(210, 50, 235, 120);
			
			// Properties html
			this.AddHtml( 210, 50, 235, 120, m_Auction[ m_Page ].Properties, (bool)false, (bool)true);
			this.AddImageTiled(4, 169, 156, 197, 2524);
			this.AddImageTiled(5, 170, 154, 195, 2624);
			this.AddAlphaRegion(5, 170, 154, 195);

			// Auction info
			this.AddLabel(10, 175, LabelHue, AuctionSystem.ST[ 68 ] );
			this.AddLabel( 45, 190, GreenHue, m_Auction.MinBid.ToString() );

			this.AddLabel(10, 280, LabelHue, AuctionSystem.ST[ 69 ] );
			this.AddLabel(75, 280, m_Auction.ReserveMet ? GreenHue : RedHue, m_Auction.ReserveMet ? "Met" : "Not Met" );

			this.AddLabel(10, 210, LabelHue, AuctionSystem.ST[ 70 ] );

			if ( m_Auction.HasBids )
				this.AddLabel(45, 225, m_Auction.ReserveMet ? GreenHue : RedHue, m_Auction.HighestBid.Amount.ToString() );
			else
				this.AddLabel(45, 225, RedHue, AuctionSystem.ST[ 71 ]  );

			// Web link button: 3
			if ( m_Auction.WebLink != null && m_Auction.WebLink.Length > 0 )
			{
				this.AddLabel(10, 300, LabelHue, AuctionSystem.ST[ 72 ] );
				this.AddButton(75, 302, 5601, 5605, 3, GumpButtonType.Reply, 0);
				m_Buttons.Add( 3 );
			}

			this.AddLabel( 10, 245, LabelHue, AuctionSystem.ST[ 56 ] );
			string timeleft = null;

			if ( ! m_Auction.Expired )
			{
				if ( m_Auction.TimeLeft >= TimeSpan.FromDays( 1 ) )
					timeleft = string.Format( AuctionSystem.ST[ 73 ] , m_Auction.TimeLeft.Days, m_Auction.TimeLeft.Hours );
				else if ( m_Auction.TimeLeft >= TimeSpan.FromMinutes( 60 ) )
					timeleft = string.Format( AuctionSystem.ST[ 74 ] , m_Auction.TimeLeft.Hours );
				else if ( m_Auction.TimeLeft >= TimeSpan.FromSeconds( 60 ) )
					timeleft = string.Format( AuctionSystem.ST[ 75 ] , m_Auction.TimeLeft.Minutes );
				else
					timeleft = string.Format( AuctionSystem.ST[ 76 ] , m_Auction.TimeLeft.Seconds );
			}
			else if ( m_Auction.Pending )
			{
				timeleft = AuctionSystem.ST[ 77 ] ;
			}
			else
			{
				timeleft = AuctionSystem.ST[ 78 ] ;
			}

			this.AddLabel( 45, 260, GreenHue, timeleft );
			
			if ( m_Auction.CanBid( m_User ) && ! m_Auction.Expired )
			{
				this.AddLabel(10, 320, LabelHue, AuctionSystem.ST[ 79 ] );
				this.AddImageTiled(9, 339, 112, 22, 2524);
				this.AddImageTiled(10, 340, 110, 20, 2624);
				this.AddAlphaRegion(10, 340, 110, 20);
				
				// Bid text: 0
				this.AddTextEntry(10, 340, 110, 20, GreenHue, 0, @"");

				// Bid button: 4
				this.AddButton(125, 340, 4011, 4012, 4, GumpButtonType.Reply, 0);
				m_Buttons.Add( 4 );
			}
			else if ( m_Auction.IsOwner( m_User ) )
			{
				// View bids: button 5
				this.AddLabel( 10, 340, LabelHue, AuctionSystem.ST[ 80 ]  );
				this.AddButton( 125, 340, 4011, 4012, 5, GumpButtonType.Reply, 0 );
				m_Buttons.Add( 5 );
			}

			// Owner description
			this.AddImageTiled(169, 194, 317, 112, 2524);
			this.AddLabel(210, 175, LabelHue, AuctionSystem.ST[ 81 ] );
			this.AddImageTiled(170, 195, 315, 110, 2624);
			this.AddAlphaRegion(170, 195, 315, 110);
			this.AddHtml( 170, 195, 315, 110, string.Format( "<basefont color=#FFFFFF>{0}", m_Auction.Description ), (bool)false, (bool)true);

			// Buy now : Button 8
			if ( m_Auction.AllowBuyNow && m_Auction.CanBid( m_User ) && !m_Auction.Expired )
			{
				this.AddButton( 170, 310, 4029, 4030, 8, GumpButtonType.Reply, 0 );
				m_Buttons.Add( 8 );
				this.AddLabel( 205, 312, GreenHue, string.Format( AuctionSystem.ST[ 210 ], m_Auction.BuyNow ) );
			}

			// Hue preview image
			if ( item.Item != null )
			{
				this.AddImageTiled( 40, 145, 100, 20, 3004 );
				this.AddImageTiled( 41, 146, 98, 18, 2624 );
				this.AddAlphaRegion( 41, 146, 98, 18 );
				this.AddLabel( 55, 145, LabelHue, AuctionSystem.ST[ 82 ]  );

				int hue = 0;

				switch ( item.Item.Hue )
				{
					case 0 : hue = 0;
						break;
						
					case 1 : hue = AuctionSystem.BlackHue - 1;
						break;

					default: hue = item.Item.Hue - 1;
						break;
				}

				// Some hues are | 0x8000 for some reason, but it leads to the same hue
				if ( hue >= 3000 )
				{
					hue -= 32768;
				}

				// Validate in case the hue was shifted by some other value
				if ( hue < 0 || hue >= 3000 )
					hue = 0;

				// For some reason gumps hues are offset by one
				this.AddImage(115, 60, 5536, hue );
			}

			if ( m_User.AccessLevel < AuctionSystem.AuctionAdminAcessLevel )
			{
				// 3D clients SUCK!
				this.AddLabel( 170, 342, GreenHue, AuctionSystem.ST[ 83 ] );
			}
			else
			{
				// Button 6 : Auction Panel
				this.AddButton( 170, 340, 4011, 4012, 6, GumpButtonType.Reply, 0 );
				m_Buttons.Add( 6 );
				this.AddLabel( 205, 342, LabelHue, AuctionSystem.ST[ 227 ] );
			}

			// Close button: 0
			this.AddButton(455, 340, 4017, 4018, 0, GumpButtonType.Reply, 0);
			this.AddLabel(415, 342, LabelHue, AuctionSystem.ST[ 7 ] );
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

			if ( !AuctionSystem.Auctions.Contains( m_Auction ) )
			{
				sender.Mobile.SendMessage( AuctionSystem.MessageHue, AuctionSystem.ST[ 207 ] );
				
				if ( m_Callback != null )
				{
					try { m_Callback.DynamicInvoke( new object[] { m_User } ) ; }
					catch {}
				}

				return;
			}

			switch ( info.ButtonID )
			{
				case 0: // Close

					if ( m_Callback != null )
					{
						try { m_Callback.DynamicInvoke( new object[] { m_User } ); }
						catch {}
					}
					break;

				case 1: // Prev item

					m_User.SendGump( new AuctionViewGump( m_User, m_Auction, m_Callback, m_Page - 1 ) );
					break;

				case 2: // Next item

					m_User.SendGump( new AuctionViewGump( m_User, m_Auction, m_Callback, m_Page + 1 ) );
					break;

				case 3: // Web link

					m_User.SendGump( new AuctionViewGump( m_User, m_Auction, m_Callback, m_Page ) );
					m_Auction.SendLinkTo( m_User );
					break;

				case 4: // Bid

					uint bid = 0;

					try { bid = uint.Parse( info.TextEntries[ 0 ].Text ); }
					catch {}

					if ( m_Auction.Expired )
					{
						m_User.SendMessage( AuctionSystem.MessageHue, AuctionSystem.ST[ 84 ] );
					}
					else if ( bid == 0 )
					{
						m_User.SendMessage( AuctionSystem.MessageHue, AuctionSystem.ST[ 85 ]  );
					}
					else
					{
						if ( m_Auction.AllowBuyNow && bid >= m_Auction.BuyNow )
						{
							// Do buy now instead
							goto case 8;
						}
						else
						{
							m_Auction.PlaceBid( m_User, (int) bid );
						}
					}

					m_User.SendGump( new AuctionViewGump( m_User, m_Auction, m_Callback, m_Page ) );
					break;

				case 5: // View bids

					m_User.SendGump( new BidViewGump( m_User, m_Auction.Bids, new AuctionGumpCallback( BidViewCallback ) ) );
					break;

				case 6: // Staff Panel

					m_User.SendGump( new AuctionControlGump( m_User, m_Auction, this ) );
					break;

				case 8: // Buy Now

					if ( m_Auction.DoBuyNow( sender.Mobile ) )
					{
						goto case 0; // Close the gump
					}
					else
					{
						sender.Mobile.SendGump( new AuctionViewGump( sender.Mobile, m_Auction, m_Callback, m_Page ) );
					}
					break;
			}
		}

		private void BidViewCallback( Mobile m )
		{
			m.SendGump( new AuctionViewGump( m, m_Auction, m_Callback, m_Page ) );
		}
	}
}