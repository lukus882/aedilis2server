using System;
using System.Collections;

using Server;
using Server.Gumps;

namespace Arya.Auction
{
	/// <summary>
	/// Provides the message notice for messages from the auction system
	/// </summary>
	public class AuctionNoticeGump : Gump
	{
		private AuctionMessageGump m_Message;
		private ArrayList m_Buttons;

		public AuctionNoticeGump( AuctionMessageGump msg ) : base ( 25, 25 )
		{
			m_Message = msg;
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
			this.AddImageTiled(0, 0, 75, 75, 3004);
			this.AddImageTiled(1, 1, 73, 73, 2624);
			this.AddAlphaRegion(1, 1, 73, 73);
			this.AddButton(7, 7, 5573, 5574, 1, GumpButtonType.Reply, 0);
			m_Buttons.Add( 1 );
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

			if ( info.ButtonID == 1 )
			{
				if ( m_Message != null )
				{
					m_Message.SendTo( sender.Mobile );
				}
			}
		}
	}
}
