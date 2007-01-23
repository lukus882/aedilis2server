using System;

using Server;
using Server.Gumps;

namespace Arya.Auction
{
	/// <summary>
	/// Summary description for DeleteSystemGump.
	/// </summary>
	public class DeleteAuctionGump : Gump
	{
		private const int GreenHue = 0x40;
		private const int RedHue = 0x20;

		public DeleteAuctionGump( Mobile m ) : base( 100, 100 )
		{
			m.CloseGump( typeof( DeleteAuctionGump ) );
			MakeGump();
		}

		private void MakeGump()
		{
			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			this.AddPage(0);
			this.AddImageTiled(0, 0, 350, 250, 5174);
			this.AddImageTiled(1, 1, 348, 248, 2702);
			this.AddAlphaRegion(1, 1, 348, 248);
			this.AddLabel(70, 15, RedHue, AuctionSystem.ST[ 96 ] );
			this.AddHtml( 30, 45, 285, 130, AuctionSystem.ST[ 97 ] , (bool)false, (bool)false);

			// Close: Button 1
			this.AddButton(30, 185, 4017, 4017, 1, GumpButtonType.Reply, 0);
			this.AddLabel(70, 185, RedHue, AuctionSystem.ST[ 98 ] );
			this.AddButton(30, 215, 4014, 4015, 0, GumpButtonType.Reply, 0);
			this.AddLabel(70, 215, GreenHue, AuctionSystem.ST[ 99 ] );
		}

		public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
		{
			if ( info.ButtonID == 1 )
			{
				AuctionSystem.ForceDelete( sender.Mobile );
			}
		}
	}
}
