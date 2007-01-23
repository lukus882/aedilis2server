using System;
using System.Collections;

using Server;
using Server.Gumps;

namespace Arya.Auction
{
	/// <summary>
	/// This gump is used to deliver the auction checks
	/// </summary>
	public class AuctionDeliveryGump : Gump
	{
		private const int LabelHue = 0x480;
		private ArrayList m_Buttons;

		private AuctionCheck m_Check;

		public AuctionDeliveryGump( AuctionCheck check ) : base( 100, 100 )
		{
			m_Check = check;

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
			this.AddImage(0, 0, 2080);
			this.AddImageTiled(18, 37, 263, 245, 2081);
			this.AddImage(20, 280, 2083);
			this.AddLabel(75, 5, 210, AuctionSystem.ST[ 0 ] );
			this.AddLabel(45, 35, 0, AuctionSystem.ST[ 1 ] );

			int goldHue = 0;
			int itemHue = 0;

			if ( m_Check is AuctionGoldCheck )
			{
				// Delivering gold
				goldHue = 143;
				itemHue = 730;
				this.AddImage(200, 39, 2530);
				this.AddLabel(70, 220, LabelHue, AuctionSystem.ST[ 2 ] );
			}
			else
			{
				// Delivering an item
				goldHue = 730;
				itemHue = 143;
				this.AddImage(135, 39, 2530);
				this.AddLabel(70, 220, LabelHue, AuctionSystem.ST[ 3 ] );
			}

			this.AddLabel(145, 35, itemHue, AuctionSystem.ST[ 4 ] );
			this.AddLabel(210, 35, goldHue, AuctionSystem.ST[ 5 ] );

			this.AddImage(45, 60, 2091);
			this.AddImage(45, 100, 2091);

			// Item name
			this.AddLabelCropped( 55, 75, 200, 20, LabelHue, m_Check.ItemName );

			this.AddHtml( 45, 115, 215, 100, m_Check.HtmlDetails, (bool)false, (bool)false);

			// Button 1 : Place in bank
			this.AddButton(45, 223, 5601, 5605, 1, GumpButtonType.Reply, 0);
			m_Buttons.Add( 1 );

			// Button 2 : View Auction
			if ( m_Check.Auction != null )
			{
				this.AddButton(45, 243, 5601, 5605, 2, GumpButtonType.Reply, 0);
				m_Buttons.Add( 2 );
				this.AddLabel(70, 240, LabelHue, AuctionSystem.ST[ 6 ] );
			}

			// Button 0 : Close
			this.AddButton(45, 263, 5601, 5605, 0, GumpButtonType.Reply, 0);
			this.AddLabel(70, 260, LabelHue, AuctionSystem.ST[ 7 ] );

			this.AddImage(225, 240, 9004);
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

			switch ( info.ButtonID )
			{
				case 1: // Place in bank

					if ( ! m_Check.Deliver( sender.Mobile ) )
					{
						sender.Mobile.SendGump( new AuctionDeliveryGump( m_Check ) );
					}
					break;

				case 2: // View auction

					if ( m_Check.Auction != null )
					{
						sender.Mobile.SendGump( new AuctionViewGump( sender.Mobile, m_Check.Auction, null ) );
					}
					else
					{
						sender.Mobile.SendGump( new AuctionDeliveryGump( m_Check ) );
					}

					break;
			}
		}
	}
}