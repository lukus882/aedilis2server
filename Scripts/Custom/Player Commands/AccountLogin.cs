using System; 
using Server; 
using System.IO; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Accounting; 
using Server.Commands.Generic;
using Server.Commands;

namespace Server.Gumps
{ 
   	public class AccountLogin : Gump 
   	{ 
      		public static void Initialize() 
      		{ 
         		CommandSystem.Register( "AccountLogin", AccessLevel.Player, new CommandEventHandler( AccountLogin_OnCommand ) ); 
      		} 

      		private static void AccountLogin_OnCommand( CommandEventArgs e ) 
      		{ 
         		e.Mobile.SendGump( new AccountLogin( e.Mobile ) ); 
      		}

      		private Mobile m_From; 

      		public AccountLogin( Mobile owner ) : base( 25,25 ) 
      		{ 
			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;
			AddPage(0);
			AddBackground(13, 12, 333, 193, 5120);
			AddImageTiled(28, 81, 212, 25, 9304);
			AddImageTiled(28, 135, 212, 25, 9304);
			AddLabel(30, 59, 1160, @"Username:");
			AddLabel(30, 114, 1160, @"Password:");
			AddImage(255, 53, 5504);
			AddButton(250, 136, 4023, 4024, 1, GumpButtonType.Reply, 0);
			AddLabel(286, 138, 1149, @"Submit");
			AddTextEntry(28, 81, 212, 25, 0, 1, @"");
			AddTextEntry(28, 135, 212, 25, 0, 2, @"");
			AddLabel(28, 166, 1149, @"For your security please enter your username and");
			AddLabel(28, 178, 1149, @"password to proceed");
			AddLabel(25, 16, 1160, @"Account Login Screen");

		}
      		public override void OnResponse( NetState state, RelayInfo info ) 
      		{ 

        		if ( info.ButtonID == 1 ) // Add Email
         		{ 
                        	Mobile from = state.Mobile;
                        	Account acct = (Account)from.Account; 
            			string user = (string)info.GetTextEntry( 1 ).Text;
            			string pass = (string)info.GetTextEntry( 2 ).Text;

				if ( user == acct.Username && acct.CheckPassword( pass ) )
				{
					from.SendMessage( 64, "Login Confirmed." );
					from.SendGump( new AccountInfo( from ) );
				}
				else
				{
					from.SendMessage( 38, "Either the username or password you entered was incorrect, Please recheck your spelling and remember that passwords and usernames are case sensitive. Please try again." );
				}
			}
        	} 
	}
} 