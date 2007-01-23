using System;
using System.Collections;

using Server;
using Server.Gumps;

namespace Arya.Auction
{
	/// <summary>
	/// Summary description for MyAuctionGump.
	/// </summary>
	public class MyAuctionGump : Gump
	{
		private const int LabelHue = 0x480;
		private const int GreenHue = 0x40;
		private const int RedHue = 0x20;
		private ArrayList m_Buttons;

		private AuctionGumpCallback m_Callback;

		public MyAuctionGump( Mobile m, AuctionGumpCallback callback ) : base( 50, 50 )
		{
			m_Callback = callback;
			m.CloseGump( typeof( MyAuctionGump ) );
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
			this.AddImage(0, 20, 10400);
			this.AddImage(0, 185, 10402);
			this.AddImage(35, 20, 10420);
			this.AddImage(421, 20, 10410);
			this.AddImage(410, 20, 10430);
			this.AddImageTiled(90, 32, 323, 16, 10254);
			this.AddLabel(160, 45, GreenHue, AuctionSystem.ST[ 8 ] );
			this.AddLabel(100, 130, LabelHue, AuctionSystem.ST[ 11 ] );
			this.AddLabel(285, 130, LabelHue, AuctionSystem.ST[ 12 ] );
			this.AddLabel(100, 165, LabelHue, AuctionSystem.ST[ 13 ] );

			this.AddButton(60, 130, 4005, 4006, 1, GumpButtonType.Reply, 0);
			m_Buttons.Add( 1 );
			this.AddButton(245, 130, 4005, 4006, 2, GumpButtonType.Reply, 0);
			m_Buttons.Add( 2 );
			this.AddButton(60, 165, 4005, 4006, 3, GumpButtonType.Reply, 0);
			m_Buttons.Add( 3 );
			this.AddButton(60, 205, 4017, 4018, 0, GumpButtonType.Reply, 0);
			this.AddLabel(100, 205, LabelHue, AuctionSystem.ST[ 14 ] );
			this.AddImage(420, 185, 10412);
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
				case 0:

					if ( m_Callback != null )
					{
						try { m_Callback.DynamicInvoke( new object[] { sender.Mobile } ); }
						catch {}
					}
					break;

				case 1: // View your auctions

					sender.Mobile.SendGump( new AuctionListing(
						sender.Mobile, AuctionSystem.GetAuctions( sender.Mobile ), false, false ) );
					break;

				case 2: // View your bids

					sender.Mobile.SendGump( new AuctionListing(
						sender.Mobile, AuctionSystem.GetBids( sender.Mobile ), false, false ) );
					break;

				case 3: // View your pendencies

					sender.Mobile.SendGump( new AuctionListing(
						sender.Mobile, AuctionSystem.GetPendencies( sender.Mobile ), false, false ) );
					break;
			}
		}
	}
}
