using System;
using System.Collections;

using Server;
using Server.Items;
using Server.Gumps;

namespace Arya.Auction
{
	/// <summary>
	/// Configuration for a new auction
	/// </summary>
	public class NewAuctionGump : Gump
	{
		private const int LabelHue = 0x480;
		private const int GreenHue = 0x40;
		private const int RedHue = 0x20;
		private ArrayList m_Buttons;

		private Mobile m_User;
		private AuctionItem m_Auction;

		public NewAuctionGump( Mobile user, AuctionItem auction ) : base( 100, 100 )
		{
			user.CloseGump( typeof( NewAuctionGump ) );

			m_User = user;
			m_Auction = auction;

			MakeGump();
		}

		private void MakeGump()
		{
			m_Buttons = new ArrayList();
			
			this.Closable=false;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			this.AddPage(0);

			this.AddBackground( 0, 0, 501, 370, 9270 );
			this.AddImageTiled(4, 4, 492, 362, 2524);
			this.AddImageTiled(5, 5, 490, 360, 2624);
			this.AddAlphaRegion(5, 5, 490, 360);

			// Auction item goes here
			this.AddItem( 15, 15, m_Auction.Item.ItemID, 0 );

			this.AddImageTiled(159, 5, 20, 184, 2524);
			this.AddImageTiled(5, 169, 255, 20, 2524);
			this.AddImageTiled(160, 5, 335, 355, 2624);
			this.AddImageTiled(5, 170, 155, 195, 2624);
			this.AddAlphaRegion(160, 5, 335, 360);
			this.AddAlphaRegion(5, 170, 155, 195);

			this.AddLabel(250, 10, RedHue, AuctionSystem.ST[ 100 ] );

			// Starting bid: text 0
			this.AddLabel(170, 35, LabelHue, AuctionSystem.ST[ 68 ] );
			this.AddImageTiled(254, 34, 72, 22, 2524);
			this.AddImageTiled(255, 35, 70, 20, 2624);
			this.AddAlphaRegion(255, 35, 70, 20);
			this.AddTextEntry(255, 35, 70, 20, GreenHue, 0, m_Auction.MinBid.ToString() );

			// Reserve: text 1
			this.AddLabel(345, 35, LabelHue, AuctionSystem.ST[ 69 ] );
			this.AddImageTiled(414, 34, 72, 22, 2524);
			this.AddImageTiled(415, 35, 70, 20, 2624);
			this.AddAlphaRegion(415, 35, 70, 20);
			this.AddTextEntry(415, 35, 70, 20, GreenHue, 1, m_Auction.Reserve.ToString() );

			// Days duration: text 2
			this.AddLabel(170, 60, LabelHue, AuctionSystem.ST[ 101 ] );
			this.AddImageTiled(254, 59, 32, 22, 2524);
			this.AddImageTiled(255, 60, 30, 20, 2624);
			this.AddAlphaRegion(255, 60, 30, 20);
			this.AddTextEntry(255, 60, 30, 20, GreenHue, 2, m_Auction.Duration.TotalDays.ToString() );
			this.AddLabel(290, 60, LabelHue, AuctionSystem.ST[ 102 ] );

			// Item name: text 3
			this.AddLabel(170, 85, LabelHue, AuctionSystem.ST[ 50 ] );
			this.AddImageTiled(254, 84, 232, 22, 2524);
			this.AddImageTiled(255, 85, 230, 20, 2624);
			this.AddAlphaRegion(255, 85, 230, 20);
			this.AddTextEntry(255, 85, 230, 20, GreenHue, 3, m_Auction.ItemName );

			// Buy now: Check 0, Text 6
			this.AddCheck( 165, 110, 2152, 2153, false, 0 );
			this.AddLabel( 200, 115, LabelHue, AuctionSystem.ST[ 208 ] );
			this.AddImageTiled( 329, 114, 157, 22, 2524 );
			this.AddImageTiled( 330, 115, 155, 20, 2624 );
			this.AddAlphaRegion( 330, 115, 155, 20 );
			this.AddTextEntry( 330, 115, 155, 20, GreenHue, 6, "" );

			// Description: text 4
			this.AddLabel(170, 140, LabelHue, AuctionSystem.ST[ 103 ] );
			this.AddImageTiled(169, 159, 317, 92, 2524);
			this.AddImageTiled(170, 160, 315, 90, 2624);
			this.AddAlphaRegion(170, 160, 315, 90);
			this.AddTextEntry(170, 160, 315, 90, GreenHue, 4, m_Auction.Description);

			// Web link: text 5
			this.AddLabel(170, 255, LabelHue, AuctionSystem.ST[ 104 ] );
			this.AddImageTiled(224, 274, 262, 22, 2524);
			this.AddLabel(170, 275, LabelHue, @"http://");
			this.AddImageTiled(225, 275, 260, 20, 2624);
			this.AddAlphaRegion(225, 275, 260, 20);
			this.AddTextEntry(225, 275, 260, 20, GreenHue, 5, m_Auction.WebLink );
			
			// Help area
			this.AddImageTiled(9, 174, 152, 187, 2524);
			this.AddImageTiled(10, 175, 150, 185, 2624);
			this.AddAlphaRegion(10, 175, 150, 185);
			this.AddHtml( 10, 175, 150, 185, AuctionSystem.ST[ 105 ] , (bool)false, (bool)true);

			// OK Button: button 1
			this.AddButton(170, 305, 4023, 4024, 1, GumpButtonType.Reply, 0);
			m_Buttons.Add( 1 );
			this.AddLabel(210, 300, RedHue, AuctionSystem.ST[ 106 ] );
			this.AddLabel(210, 315, RedHue, AuctionSystem.ST[ 107 ] );

			// Cancel button: button 0
			this.AddButton(170, 335, 4020, 4020, 0, GumpButtonType.Reply, 0);
			m_Buttons.Add( 0 );
			this.AddLabel(210, 335, LabelHue, AuctionSystem.ST[ 108 ] );
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

				m_Auction.Cancel();

				return;
			}

			bool allowBuyNow = info.Switches.Length > 0; // Just one switch

			switch ( info.ButtonID )
			{
				case 0: // Cancel the auction

					m_Auction.Cancel();
					m_User.SendGump( new AuctionGump( m_User ) );
					break;

				case 1: // Commit the auction

					// Collect information
					int minbid = 0; // text 0
					int reserve = 0; // text 1
					int days = 0; // text 2
					string name = ""; // text 3
					string description = ""; // text 4
					string weblink = ""; // text 5
					int buynow = 0; // text 6

					// The 3D client sucks

					string[] tr = new string[ 7 ];

					foreach( TextRelay t in info.TextEntries )
					{
						tr[ t.EntryID ] = t.Text;
					}

					try { minbid = (int) uint.Parse( tr[ 0 ] ); } 
					catch {}

					try { reserve = (int) uint.Parse( tr[ 1 ] ); }
					catch {}

					try { days = (int) uint.Parse( tr[ 2 ] ); }
					catch {}

					try { buynow = (int) uint.Parse( tr[ 6 ] ); }
					catch {}

					if ( tr[ 3 ] != null )
					{
						name = tr[ 3 ];
					}

					if ( tr[ 4 ] != null )
					{
						description = tr[ 4 ];
					}

					if ( tr[ 5 ] != null )
					{
						weblink = tr[ 5 ];
					}

					bool ok = true;

					if ( minbid < 1 )
					{
						m_User.SendMessage( AuctionSystem.MessageHue, AuctionSystem.ST[ 109 ] );
						ok = false;
					}

					if ( reserve < 1 || reserve < minbid )
					{
						m_User.SendMessage( AuctionSystem.MessageHue, AuctionSystem.ST[ 110 ] );
						ok = false;
					}

					if ( days < AuctionSystem.MinAuctionDays && m_User.AccessLevel < AccessLevel.GameMaster || days < 1 )
					{
						m_User.SendMessage( AuctionSystem.MessageHue, AuctionSystem.ST[ 111 ] , AuctionSystem.MinAuctionDays );
						ok = false;
					}

					if ( days > AuctionSystem.MaxAuctionDays && m_User.AccessLevel < AccessLevel.GameMaster )
					{
						m_User.SendMessage( AuctionSystem.MessageHue, AuctionSystem.ST[ 112 ] , AuctionSystem.MaxAuctionDays );
						ok = false;
					}

					if ( name.Length == 0 )
					{
						m_User.SendMessage( AuctionSystem.MessageHue, AuctionSystem.ST[ 113 ] );
						ok = false;
					}

					if ( minbid * AuctionSystem.MaxReserveMultiplier < reserve && m_User.AccessLevel < AccessLevel.GameMaster )
					{
						m_User.SendMessage( AuctionSystem.MessageHue, AuctionSystem.ST[ 114 ] );
						ok = false;
					}

					if ( allowBuyNow && buynow <= reserve )
					{
						m_User.SendMessage( AuctionSystem.MessageHue, AuctionSystem.ST[ 209 ] );
						ok = false;
					}

					m_Auction.MinBid = minbid;
					m_Auction.Reserve = reserve;
					m_Auction.ItemName = name;
					m_Auction.Duration = TimeSpan.FromDays( days );					
					m_Auction.Description = description;
					m_Auction.WebLink = weblink;
					m_Auction.BuyNow = allowBuyNow ? buynow : 0;

					if ( ok && AuctionSystem.Running )
					{
						m_Auction.Confirm();
						m_User.SendGump( new AuctionViewGump( m_User, m_Auction, new AuctionGumpCallback( AuctionCallback ) ) );
					}
					else if ( AuctionSystem.Running )
					{
						m_User.SendGump( new NewAuctionGump( m_User, m_Auction ) );
					}
					else
					{
						m_User.SendMessage( AuctionSystem.MessageHue, AuctionSystem.ST[ 115 ] );
					}

					break;
			}
		}

		private static void AuctionCallback( Mobile user )
		{
			user.SendGump( new AuctionGump( user ) );
		}
	}
}
