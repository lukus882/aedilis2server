using System;
using System.Collections;

using Server;
using Server.Gumps;

namespace Arya.Auction
{
	/// <summary>
	/// The admin gump for the auction system
	/// </summary>
	public class AuctionAdminGump : Gump
	{
		private const int LabelHue = 0x480;
		private const int GreenHue = 0x40;
		private const int RedHue = 0x20;

		private ArrayList m_Buttons;

		public AuctionAdminGump( Mobile m ) : base ( 100, 100 )
		{
			m.CloseGump( typeof( AuctionAdminGump ) );
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
			this.AddBackground(0, 0, 270, 270, 9300);
			this.AddAlphaRegion(0, 0, 270, 270);
			this.AddLabel(36, 5, RedHue, @"Auction System Administration");
			this.AddImageTiled(16, 30, 238, 1, 9274);

			this.AddLabel(15, 65, LabelHue, string.Format( @"Deadline: {0} at {1}", AuctionScheduler.Deadline.ToShortDateString(), AuctionScheduler.Deadline.ToShortTimeString() ) );
			this.AddLabel(15, 40, GreenHue, string.Format( @"{0} Auctions, {1} Pending", AuctionSystem.Auctions.Count, AuctionSystem.Pending.Count ) );

			// B 1 : Validate
			this.AddButton(15, 100, 4005, 4006, 1, GumpButtonType.Reply, 0);
			this.AddLabel(55, 100, LabelHue, @"Force Verification");
			m_Buttons.Add( 1 );

			// B 2 : Profile
			this.AddButton(15, 130, 4005, 4006, 2, GumpButtonType.Reply, 0);
			this.AddLabel(55, 130, LabelHue, @"Profile the System");
			m_Buttons.Add( 2 );

			// B 3 : Temporary Shutdown
			this.AddButton(15, 160, 4005, 4006, 3, GumpButtonType.Reply, 0);
			this.AddLabel(55, 160, LabelHue, @"Temporarily Shut Down");
			m_Buttons.Add( 3 );

			// B 4 : Delete
			this.AddButton(15, 190, 4005, 4006, 4, GumpButtonType.Reply, 0);
			this.AddLabel(55, 190, LabelHue, @"Permanently Shut Down");
			m_Buttons.Add( 4 );

			// B 0 : Close
			this.AddButton(15, 230, 4023, 4024, 0, GumpButtonType.Reply, 0);
			this.AddLabel(55, 230, LabelHue, @"Exit");
			m_Buttons.Add( 0 );
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
				case 1: // Validate

					AuctionSystem.VerifyAuctions();
					AuctionSystem.VerifyPendencies();

					sender.Mobile.SendGump( new AuctionAdminGump( sender.Mobile ) );
					break;

				case 2: // Profile

					AuctionSystem.ProfileAuctions();

					sender.Mobile.SendGump( new AuctionAdminGump( sender.Mobile ) );
					break;

				case 3: // Disable

					AuctionSystem.Disable();
					sender.Mobile.SendMessage( AuctionSystem.MessageHue, "The system has been stopped. It will be restored with the next reboot." );
					break;

				case 4: // Delete

					sender.Mobile.SendGump( new DeleteAuctionGump( sender.Mobile ) );
					break;
			}
		}

	}
}
