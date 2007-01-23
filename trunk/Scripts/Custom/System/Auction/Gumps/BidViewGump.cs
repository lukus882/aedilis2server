using System;
using System.Collections;

using Server;
using Server.Gumps;

namespace Arya.Auction
{
	/// <summary>
	/// Summary description for BidViewGump.
	/// </summary>
	public class BidViewGump : Gump
	{
		private const int LabelHue = 0x480;
		private const int GreenHue = 0x40;
		private const int RedHue = 0x20;
		private ArrayList m_Buttons;

		private AuctionGumpCallback m_Callback;
		private int m_Page;
		private ArrayList m_Bids;

		public BidViewGump( Mobile m, ArrayList bids, AuctionGumpCallback callback ) : this ( m, bids, callback, 0 )
		{
		}

		public BidViewGump( Mobile m, ArrayList bids, AuctionGumpCallback callback, int page ) : base( 100, 100 )
		{
			m.CloseGump( typeof( BidViewGump ) );
			m_Callback = callback;
			m_Page = page;
			m_Bids = new ArrayList( bids );

			MakeGump();
		}

		private void MakeGump()
		{
			m_Buttons = new ArrayList();

			int numOfPages =  ( m_Bids.Count - 1 ) / 10 + 1;

			if ( m_Bids.Count == 0 )
				numOfPages = 0;

			this.Closable=true;
			m_Buttons.Add( 0 );

			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
			this.AddImageTiled(0, 0, 297, 282, 5174);
			this.AddImageTiled(1, 1, 295, 280, 2702);
			this.AddAlphaRegion(1, 1, 295, 280);
			this.AddLabel(12, 5, RedHue, AuctionSystem.ST[ 86 ] );

			this.AddLabel(160, 5, GreenHue, string.Format( AuctionSystem.ST[ 18 ] , m_Page + 1, numOfPages ) );
			this.AddImageTiled(10, 30, 277, 221, 5174);
			this.AddImageTiled(11, 31, 39, 19, 9274);
			this.AddAlphaRegion(11, 31, 39, 19);
			this.AddImageTiled(51, 31, 104, 19, 9274);
			this.AddAlphaRegion(51, 31, 104, 19);
			this.AddLabel(55, 30, GreenHue, AuctionSystem.ST[ 87 ] );
			this.AddImageTiled(156, 31, 129, 19, 9274);
			this.AddAlphaRegion(156, 31, 129, 19);
			this.AddLabel(160, 30, GreenHue, AuctionSystem.ST[ 88 ] );

			for ( int i = 0; i < 10; i++ )
			{
				this.AddImageTiled(11, 51 + i * 20, 39, 19, 9264);
				this.AddAlphaRegion(11, 51 + i * 20, 39, 19);
				this.AddImageTiled(51, 51 + i * 20, 104, 19, 9264);
				this.AddAlphaRegion(51, 51 + i * 20, 104, 19);
				this.AddImageTiled(156, 51 + i * 20, 129, 19, 9264);
				this.AddAlphaRegion(156, 51 + i * 20, 129, 19);

				if ( m_Page * 10 + i < m_Bids.Count )
				{
					Bid bid = m_Bids[ m_Page * 10 + i ] as Bid;
					this.AddLabel(15, 50 + i * 20, LabelHue, ( m_Page * 10 + i + 1 ).ToString() );
					this.AddLabelCropped( 55, 50 + i * 20, 100, 19, LabelHue, bid.Mobile != null ? bid.Mobile.Name : AuctionSystem.ST[ 78 ] );
					this.AddLabel(160, 50 + i * 20, LabelHue, bid.Amount.ToString() );
				}
			}

			this.AddButton(10, 255, 4011, 4012, 0, GumpButtonType.Reply, 0);
			this.AddLabel(48, 257, LabelHue, AuctionSystem.ST[ 89 ] );
			
			// PREV PAGE: 1
			if ( m_Page > 0 )
			{
				this.AddButton(250, 8, 9706, 9707, 1, GumpButtonType.Reply, 0);
				m_Buttons.Add( 1 );
			}

			// NEXT PAGE: 2
			if ( m_Page < numOfPages - 1 )
			{
				this.AddButton(270, 8, 9702, 9703, 2, GumpButtonType.Reply, 0);
				m_Buttons.Add( 2 );
			}
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
				case 0:

					if ( m_Callback != null )
					{
						try { m_Callback.DynamicInvoke( new object[] { sender.Mobile } ); }
						catch {}
					}
					break;

				case 1:

					sender.Mobile.SendGump( new BidViewGump( sender.Mobile, m_Bids, m_Callback, m_Page - 1 ) );
					break;

				case 2:

					sender.Mobile.SendGump( new BidViewGump( sender.Mobile, m_Bids, m_Callback, m_Page + 1 ) );
					break;
			}
		}
	}
}
