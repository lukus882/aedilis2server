using System;
using System.Collections;

using Server;
using Server.Gumps;

namespace Arya.Auction
{
	/// <summary>
	/// This gump is used to deliver a creature
	/// </summary>
	public class CreatureDeliveryGump : Gump
	{
		private const int LabelHue = 0x480;
		private AuctionCheck m_Check;
		private ArrayList m_Buttons;

		public CreatureDeliveryGump( AuctionCheck check ) : base( 100, 100 )
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
			this.AddImageTiled(18, 37, 263, 310, 2081);
			this.AddImage(21, 347, 2083);
			this.AddLabel(75, 5, 210, AuctionSystem.ST[ 0 ] );
			this.AddLabel(90, 35, 210, AuctionSystem.ST[ 90 ] );

			this.AddImage(45, 60, 2091);
			this.AddImage(45, 100, 2091);

			this.AddLabelCropped( 45, 75, 210, 20, LabelHue, m_Check.ItemName );

			this.AddHtml( 45, 115, 215, 100, m_Check.HtmlDetails, (bool)false, (bool)false);

			// Button 1 : Stable
			this.AddButton(50, 300, 5601, 5605, 1, GumpButtonType.Reply, 0);
			this.AddLabel(70, 298, LabelHue, AuctionSystem.ST[ 91 ] );
			m_Buttons.Add( 1 );

			// Button 0: Close
			this.AddButton(50, 325, 5601, 5605, 0, GumpButtonType.Reply, 0);
			this.AddImage(230, 315, 9004);
			this.AddLabel(70, 323, LabelHue, AuctionSystem.ST[ 7 ] );
			this.AddLabel(45, 220, LabelHue, AuctionSystem.ST[ 92 ] );
			this.AddLabel(45, 240, LabelHue, AuctionSystem.ST[ 93 ] );
			this.AddLabel(45, 255, LabelHue, AuctionSystem.ST[ 94 ] );
			this.AddLabel(45, 275, LabelHue, AuctionSystem.ST[ 95 ] );
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

			if ( info.ButtonID == 1 )
			{
				MobileStatuette ms = m_Check.DeliveredItem as MobileStatuette;

				if ( ms != null )
				{
					ms.Stable( sender.Mobile );
					m_Check.Delete();
				}
			}
		}
	}
}
