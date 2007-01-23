using System;
using System.Collections;

using Server;
using Server.Gumps;

namespace Arya.Auction
{
	/// <summary>
	/// The main gump for the auction house
	/// </summary>
	public class AuctionGump : Gump
	{
		private const int LabelHue = 0x480;
		private ArrayList m_Buttons;

		public AuctionGump( Mobile user ) : base( 50, 50 )
		{
			user.CloseGump( typeof( AuctionGump ) );
			MakeGump();
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
			this.AddImageTiled(49, 39, 402, 197, 3004);
			this.AddImageTiled(50, 40, 400, 195, 2624);
			this.AddAlphaRegion(50, 40, 400, 195);
			this.AddImage(165, 65, 10452);
			this.AddImage(-1, 20, 10400);
			this.AddImage(-1, 185, 10402);
			this.AddImage(35, 20, 10420);
			this.AddImage(421, 20, 10410);
			this.AddImage(410, 20, 10430);
			this.AddImageTiled(90, 32, 323, 16, 10254);
			this.AddImage(420, 185, 10412);

			this.AddLabel(160, 45, 151, AuctionSystem.ST[ 8 ] );

			// Create new auction: B1
			this.AddLabel(100, 130, LabelHue, AuctionSystem.ST[ 9 ] );
			this.AddButton(60, 130, 4005, 4006, 1, GumpButtonType.Reply, 0);
			m_Buttons.Add( 1 );

			// View all auctions: B2
			this.AddLabel(285, 130, LabelHue, AuctionSystem.ST[ 10 ] );
			this.AddButton(245, 130, 4005, 4006, 2, GumpButtonType.Reply, 0);
			m_Buttons.Add( 2 );

			// View your auctions: B3
			this.AddLabel(100, 165, LabelHue, AuctionSystem.ST[ 11 ] );
			this.AddButton(60, 165, 4005, 4006, 3, GumpButtonType.Reply, 0);
			m_Buttons.Add( 3 );

			// View your bids: B4
			this.AddLabel(285, 165, LabelHue, AuctionSystem.ST[ 12 ] );
			this.AddButton(245, 165, 4005, 4006, 4, GumpButtonType.Reply, 0);
			m_Buttons.Add( 4 );

			// View pendencies: B5
			this.AddButton( 60, 200, 4005, 4006, 5, GumpButtonType.Reply, 0 );
			this.AddLabel( 100, 200, LabelHue, AuctionSystem.ST[ 13 ] );
			m_Buttons.Add( 5 );

			// Exit: B0
			this.AddLabel(285, 205, LabelHue, AuctionSystem.ST[ 14 ] );
			this.AddButton(245, 200, 4017, 4018, 0, GumpButtonType.Reply, 0);
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

			switch ( info.ButtonID )
			{
				case 0: // Exit

					break;

				case 1: // Create auction

					AuctionSystem.AuctionRequest( sender.Mobile );

					break;

				case 2: // View all auctions

					sender.Mobile.SendGump( new AuctionListing(
						sender.Mobile,
						AuctionSystem.Auctions,
						true,
						true ) );

					break;

				case 3: // View your auctions

					sender.Mobile.SendGump( new AuctionListing(
						sender.Mobile, AuctionSystem.GetAuctions( sender.Mobile ), true, true ) );
					break;

				case 4: // View your bids

					sender.Mobile.SendGump( new AuctionListing(
						sender.Mobile, AuctionSystem.GetBids( sender.Mobile ), true, true ) );
					break;

				case 5: // View pendencies

					sender.Mobile.SendGump( new AuctionListing(
						sender.Mobile, AuctionSystem.GetPendencies( sender.Mobile ), true, true ) );
					break;
			}
		}

	}
}