using System;
using System.Collections;

using Server;
using Server.Gumps;

namespace Arya.Auction
{
	public class AuctionSortGump : Server.Gumps.Gump
	{
		private const int LabelHue = 0x480;
		private const int GreenHue = 0x40;
		private const int RedHue = 0x20;
		private ArrayList m_Buttons;

		private bool m_Search;
		private bool m_ReturnToAuction;
		private ArrayList m_List;

		public AuctionSortGump( Mobile m, ArrayList items, bool returnToAuction, bool search ) : base( 50, 50 )
		{
			m.CloseGump( typeof( AuctionSortGump ) );

			m_List = items;
			m_ReturnToAuction = returnToAuction;
			m_Search = search;

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
			this.AddImageTiled(49, 34, 402, 312, 3004);
			this.AddImageTiled(50, 35, 400, 310, 2624);
			this.AddAlphaRegion(50, 35, 400, 310);
			this.AddImage(165, 65, 10452);
			this.AddImage(0, 20, 10400);
			this.AddImage(0, 280, 10402);
			this.AddImage(35, 20, 10420);
			this.AddImage(421, 20, 10410);
			this.AddImage(410, 20, 10430);
			this.AddImageTiled(90, 32, 323, 16, 10254);
			this.AddLabel(160, 45, GreenHue, AuctionSystem.ST[ 49 ] );

			this.AddLabel(95, 125, RedHue, AuctionSystem.ST[ 50 ] );
			this.AddImage(75, 125, 2511);
			
			this.AddButton(110, 144, 9702, 9703, 1, GumpButtonType.Reply, 0);
			m_Buttons.Add( 1 );

			this.AddLabel(135, 141, LabelHue, AuctionSystem.ST[ 51 ] );

			this.AddButton(110, 163, 9702, 9703, 2, GumpButtonType.Reply, 0);
			m_Buttons.Add( 2 );

			this.AddLabel(135, 160, LabelHue, AuctionSystem.ST[ 52 ] );

			this.AddImage(420, 280, 10412);
			this.AddLabel(95, 185, RedHue, AuctionSystem.ST[ 53 ] );
			this.AddImage(75, 185, 2511);

			this.AddButton(110, 204, 9702, 9703, 3, GumpButtonType.Reply, 0);
			m_Buttons.Add( 3 );

			this.AddLabel(135, 201, LabelHue, AuctionSystem.ST[ 54 ] );

			this.AddButton(110, 223, 9702, 9703, 4, GumpButtonType.Reply, 0);
			m_Buttons.Add( 4 );

			this.AddLabel(135, 220, LabelHue, AuctionSystem.ST[ 55 ] );

			this.AddLabel(95, 245, RedHue, AuctionSystem.ST[ 56 ] );
			this.AddImage(75, 245, 2511);

			this.AddButton(110, 264, 9702, 9703, 5, GumpButtonType.Reply, 0);
			m_Buttons.Add( 5 );

			this.AddLabel(135, 261, LabelHue, AuctionSystem.ST[ 57 ] );

			this.AddButton(110, 283, 9702, 9703, 6, GumpButtonType.Reply, 0);
			m_Buttons.Add( 6 );

			this.AddLabel(135, 280, LabelHue, AuctionSystem.ST[ 58 ] );

			this.AddLabel(290, 125, RedHue, AuctionSystem.ST[ 59 ] );
			this.AddImage(270, 125, 2511);

			this.AddButton(305, 144, 9702, 9703, 7, GumpButtonType.Reply, 0);
			m_Buttons.Add( 7 );

			this.AddLabel(330, 141, LabelHue, AuctionSystem.ST[ 60 ] );

			this.AddButton(305, 163, 9702, 9703, 8, GumpButtonType.Reply, 0);
			m_Buttons.Add( 8 );

			this.AddLabel(330, 160, LabelHue, AuctionSystem.ST[ 61 ] );

			this.AddLabel(290, 185, RedHue, AuctionSystem.ST[ 62 ] );
			this.AddImage(270, 185, 2511);
			
			this.AddButton(305, 204, 9702, 9703, 9, GumpButtonType.Reply, 0);
			m_Buttons.Add( 9 );

			this.AddLabel(330, 201, LabelHue, AuctionSystem.ST[ 63 ] );

			this.AddButton(305, 223, 9702, 9703, 10, GumpButtonType.Reply, 0);
			m_Buttons.Add( 10 );

			this.AddLabel(330, 220, LabelHue, AuctionSystem.ST[ 64 ] );

			this.AddLabel(290, 245, RedHue, AuctionSystem.ST[ 65 ] );
			this.AddImage(270, 245, 2511);

			this.AddButton(305, 264, 9702, 9703, 11, GumpButtonType.Reply, 0);
			m_Buttons.Add( 11 );

			this.AddLabel(330, 261, LabelHue, AuctionSystem.ST[ 63 ] );

			this.AddButton(305, 283, 9702, 9703, 12, GumpButtonType.Reply, 0);
			m_Buttons.Add( 12 );

			this.AddLabel(330, 280, LabelHue, AuctionSystem.ST[ 64 ] );

			this.AddLabel(120, 315, LabelHue, AuctionSystem.ST[ 66 ] );
			this.AddButton(80, 315, 4017, 4018, 0, GumpButtonType.Reply, 0);
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
				sender.Mobile.SendMessage( AuctionSystem.MessageHue, AuctionSystem.ST[ 15 ]  );
				return;
			}

			AuctionComparer cmp = null;

			switch( info.ButtonID )
			{
				case 1: // Name

					cmp = new AuctionComparer( AuctionSorting.Name, true );
					break;

				case 2:

					cmp = new AuctionComparer( AuctionSorting.Name, false );
					break;

				case 3:

					cmp = new AuctionComparer( AuctionSorting.Date, true );
					break;

				case 4:

					cmp = new AuctionComparer( AuctionSorting.Date, false );
					break;

				case 5:

					cmp = new AuctionComparer( AuctionSorting.TimeLeft, true );
					break;

				case 6:

					cmp = new AuctionComparer( AuctionSorting.TimeLeft, false );
					break;

				case 7:

					cmp = new AuctionComparer( AuctionSorting.Bids, true );
					break;

				case 8:

					cmp = new AuctionComparer( AuctionSorting.Bids, false );
					break;

				case 9:

					cmp = new AuctionComparer( AuctionSorting.MinimumBid, true );
					break;

				case 10:

					cmp = new AuctionComparer( AuctionSorting.MinimumBid, false );
					break;

				case 11:

					cmp = new AuctionComparer( AuctionSorting.HighestBid, true );
					break;

				case 12:

					cmp = new AuctionComparer( AuctionSorting.HighestBid, false );
					break;
			}

			if ( cmp != null )
			{
				m_List.Sort( cmp );
			}

			sender.Mobile.SendGump( new AuctionListing(
				sender.Mobile,
				m_List,
				m_Search,
				m_ReturnToAuction ) );
		}
	}
}