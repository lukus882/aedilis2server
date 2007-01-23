using System;
using System.Collections;

using Server;
using Server.Gumps;

namespace Arya.Auction
{
	/// <summary>
	/// Summary description for AuctionMessageGump.
	/// </summary>
	public class AuctionMessageGump : Gump
	{
		private const int LabelHue = 0x480;
		private const int GreenHue = 0x40;
		private const int RedHue = 0x20;
		private ArrayList m_Buttons;

		/// <summary>
		/// Sets the message displayed by the gump in the details area
		/// </summary>
		public string Message
		{
			set
			{
				m_HtmlMessage = string.Format( "<basefont color=#111111>{0}", value );
			}
		}

		/// <summary>
		/// Sets the text associated with the OK button
		/// </summary>
		public string OkText
		{
			set { m_OkText = value; }
		}

		/// <summary>
		/// Sets the text associated with the Cancel button
		/// </summary>
		public string CancelText
		{
			set { m_CancelText = value; }
		}

		/// <summary>
		/// Specifies whether this gump carries just information. If true, the gump will only have an OK button.
		/// If false the gump will have both OK and Cancel buttons.
		/// </summary>
		public bool InformationMode
		{
			set { m_InformationMode = value; }
		}

		/// <summary>
		/// Gets or sets the auction referenced by this message
		/// </summary>
		public AuctionItem Auction
		{
			get
			{
				return AuctionSystem.Find( m_ID );
			}
			set
			{
				m_ID = value.ID;
			}
		}

		/// <summary>
		/// Specifies if this message is targeted at the auction owner, rather than at bidder
		/// </summary>
		public bool OwnerTarget
		{
			set { m_OwnerTarget = value; }
		}

		/// <summary>
		/// Specifies whether this message should validate the answer with the auction
		/// </summary>
		public bool VerifyAuction
		{
			set { m_VerifyAuction = value; }
		}

		/// <summary>
		/// Specifies whether to show the expiration notice at the bottom of the message
		/// </summary>
		public bool ShowExpiration
		{
			set { m_ShowExpiration = value; }
		}

		private Guid m_ID;
		private string m_HtmlMessage;
		private string m_OkText;
		private string m_CancelText;
		private bool m_InformationMode;
		private bool m_OwnerTarget;
		private bool m_VerifyAuction;
		private bool m_ShowExpiration = true;

		public AuctionMessageGump( AuctionItem auction, bool informationMode, bool ownerTarget, bool verifyAuction ) : base( 50, 50 )
		{
			Auction = auction;
			m_InformationMode = informationMode;
			m_OwnerTarget = ownerTarget;
			m_VerifyAuction = verifyAuction;
		}

		public void SendTo( Mobile m )
		{
			MakeGump();
			m.SendGump( this );
		}

		private void MakeGump()
		{
			m_Buttons = new ArrayList();

			this.Closable=false;
			this.Disposable=true;
			this.Dragable=false;
			this.Resizable=false;
			this.AddPage(0);
			this.AddImage(0, 0, 9380);
			this.AddImageTiled(114, 0, 335, 140, 9381);
			this.AddImage(449, 0, 9382);
			this.AddImageTiled(0, 140, 115, 153, 9383);
			this.AddImageTiled(114, 140, 336, 217, 9384);
			this.AddImageTiled(449, 140, 102, 217, 9385);
			this.AddImage(0, 290, 9386);
			this.AddImageTiled(114, 290, 353, 140, 9387);
			this.AddImage(450, 290, 9388);
			this.AddLabel(200, 38, 76, AuctionSystem.ST[ 25 ] );
			this.AddImageTiled(65, 65, 438, 11, 2091);

			this.AddLabel(65, 85, 0, AuctionSystem.ST[ 26 ] );

			AuctionItem auction = this.Auction;

			// BUTTON 0: View auction details
			if ( auction != null )
			{
				this.AddLabel(125, 85, RedHue, auction.ItemName );

				this.AddButton(65, 112, 9762, 9763, 0, GumpButtonType.Reply, 0);
				m_Buttons.Add( 0 );
				this.AddLabel(85, 110, LabelHue, AuctionSystem.ST[ 27 ] );
			}				
			else
			{
				this.AddLabel( 125, 85, RedHue, AuctionSystem.ST[ 28 ] );
			}
			
			this.AddHtml( 75, 170, 413, 120, m_HtmlMessage, (bool)true, (bool)false);
			this.AddLabel(75, 150, LabelHue, AuctionSystem.ST[ 29 ] );

			// BUTTON 1: OK
			// BUTTON 2: CANCEL

			if ( m_InformationMode )
			{
				// Information mode: show only OK button at bottom with the OK text
				this.AddButton( 45, 345, 1147, 1149, 1, GumpButtonType.Reply, 0 );
				m_Buttons.Add( 1 );
				this.AddLabel( 125, 355, LabelHue, m_OkText );
			}
			else
			{
				this.AddButton(45, 300, 1147, 1149, 1, GumpButtonType.Reply, 0);
				this.AddLabel(125, 310, LabelHue, m_OkText);
				this.AddButton(45, 345, 1144, 1146, 2, GumpButtonType.Reply, 0);
				m_Buttons.Add( 2 );
				this.AddLabel(125, 355, LabelHue, m_CancelText);
			}

			if ( m_ShowExpiration && auction != null )
			{
				this.AddLabel( 55, 405, RedHue, 
					string.Format( AuctionSystem.ST[ 30 ] ,
					auction.PendingTimeLeft.Days, auction.PendingTimeLeft.Hours ) );
			}
		}

		private void ResendMessage( Mobile m )
		{
			m.SendGump( this );
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

			AuctionItem auction = Auction;

			if ( auction == null )
			{
				sender.Mobile.SendMessage( AuctionSystem.MessageHue, AuctionSystem.ST[ 31 ] );
				return;
			}

			switch ( info.ButtonID )
			{
				case 0: // View auction details

					if ( m_InformationMode && ! m_VerifyAuction )
						// This is an outbid message, no need to return after visiting the auction
						sender.Mobile.SendGump( new AuctionViewGump( sender.Mobile, Auction ) );
					else
						sender.Mobile.SendGump( new AuctionViewGump( sender.Mobile, Auction, new AuctionGumpCallback( ResendMessage ) ) );
					break;

				case 1: // OK

					if ( m_InformationMode )
					{
						if ( m_VerifyAuction )
						{
							auction.ConfirmInformationMessage( m_OwnerTarget );
						}
					}
					else
					{
						auction.ConfirmResponseMessage( m_OwnerTarget, true );
					}
					break;

				case 2: // Cancel

					auction.ConfirmResponseMessage( m_OwnerTarget, false );
					break;
			}
		}
	}
}
