using System;
using Server;
using Server.Network;
using Server.Mobiles;
using Server.Accounting;
using System.Collections;
using Server.Scripts.Commands;
using Server.Gumps;
using Server.Commands.Generic;

namespace Server.Gumps
{
	public class AccountInfo : Gump
	{
		private Mobile m_From;

		private int m_PassLength = 3;

		public AccountInfo( Mobile from ) : base( 0, 0 )
		{
			m_From = from;

                        Account acct = (Account)from.Account;
			PlayerMobile pm = (PlayerMobile)from;
			NetState ns = from.NetState;
			ClientVersion v = ns.Version;

			TimeSpan totalTime = (DateTime.Now - acct.Created);

			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;
			AddPage(0);
			AddPage(1);
			AddBackground(39, 29, 417, 276, 5120);
			AddLabel(49, 33, 1160, @"Main Account Screen");
			AddButton(52, 67, 4005, 4006, 1, GumpButtonType.Page, 2);
			AddLabel(85, 68, 1149, @"Change Password");
			AddImageTiled(52, 159, 393, 131, 9254);
			AddAlphaRegion(52, 158, 393, 131);
			AddLabel(56, 136, 1160, @"Account Info:");
			AddLabel(60, 165, 1149, @"User Name:");
			AddLabel(60, 185, 1149, @"Client Version:");
			AddLabel(60, 205, 1149, @"IP Address:");
			AddLabel(60, 225, 1149, @"Account Created On:");
			AddLabel(60, 245, 1149, @"Played Time:");
			AddLabel(60, 265, 1149, @"Account Age:");
			AddImage(384, 39, 5523);
			AddLabel(134, 165, 64, acct.Username.ToString() );
			AddLabel(157, 185, 64, v == null ? "(null)" : v.ToString() );
			AddLabel(135, 205, 64, ns.ToString() );
			AddLabel(187, 225, 64, acct.Created.ToString() );
			
			string gt = pm.GameTime.Days + " Days, " + pm.GameTime.Hours + " Hours, " + pm.GameTime.Minutes + " Minutes, " + pm.GameTime.Seconds + " Seconds.";
			AddLabel(145, 245, 64, gt.ToString() );

			string tt = totalTime.Days + " Days, " + totalTime.Hours + " Hours, " + totalTime.Minutes + " Minutes, " + totalTime.Seconds + " Seconds.";
			AddLabel(150, 265, 64, tt.ToString() );
			AddPage(2);
			AddBackground(39, 29, 262, 240, 5120);
			AddLabel(50, 30, 1160, @"Password Change Menu");
			AddImageTiled(50, 75, 238, 29, 9304);
			AddImageTiled(50, 135, 238, 29, 9304);
			AddImageTiled(50, 195, 238, 29, 9304);
			AddLabel(50, 55, 1149, @"Current Password:");
			AddLabel(50, 115, 1149, @"New Password:");
			AddLabel(50, 175, 1149, @"Confirm Password:");
			AddButton(50, 233, 4023, 4024, 1, GumpButtonType.Reply, 0);
			AddLabel(85, 234, 1160, @"Submit New Password");
			AddTextEntry(50, 75, 238, 29, 0, 1, @"");
			AddTextEntry(50, 135, 238, 29, 0, 2, @"");
			AddTextEntry(50, 195, 238, 29, 0, 3, @"");

		}
      		public override void OnResponse( NetState state, RelayInfo info ) 
      		{ 

        		if ( info.ButtonID == 1 ) // Add Email
         		{ 
                        	Mobile from = state.Mobile;
                        	Account acct = (Account)from.Account; 
            			string cpass = (string)info.GetTextEntry( 1 ).Text;
            			string newpass = (string)info.GetTextEntry( 2 ).Text;
            			string newpass2 = (string)info.GetTextEntry( 3 ).Text;

				if ( acct.CheckPassword( cpass ) )
				{
					if ( newpass == null || newpass2 == null )
					{
						from.SendMessage( 38, "You must type in a new password and confirm it." );
					}
					else if ( newpass.Length <= m_PassLength )
					{
						from.SendMessage( 38, "Your new password must be at least characters {0} long.", m_PassLength );
					}
					else if ( newpass == newpass2 )
					{
						from.SendMessage( "Your password has been changed to {0}.", newpass );
						acct.SetPassword( newpass );
						
					}
					else
					{
						from.SendMessage( 38, "Your new password did not match your confirm password. Please check your spelling and try again." );
						from.SendMessage( 38, "Just a reminder. Passwords are case sensitive." );
					}
				}
				else
				{
					from.SendMessage( 38, "The current password you typed in did not match your current password on record. Please check your spelling and try again." );
					from.SendMessage( 38, "Just a reminder. Passwords are case sensitive." );
				}
			}
        	} 
	}
} 