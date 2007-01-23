using System; 
using Server; 
using Server.Mobiles; 
using Server.Gumps;
using System.Data;
using System.IO;
using Server.Items;
using Server.Network;
using Server.Targeting;
using System.Collections;
using Server.Regions;
using Server.Prompts;
using System.Net;
using System.Text;
using System.Diagnostics;
using Server.Accounting;

namespace Server.Commands
{
	public class Played
	{
		public static void Initialize()
		{
			CommandSystem.Register( "played", AccessLevel.Player, new CommandEventHandler( Played_OnCommand ) );
		}

		private static void Played_OnCommand( CommandEventArgs e )
		{
			Mobile m = e.Mobile;
			if( m.Player && m is PlayerMobile )
			{
				PlayerMobile from = m as PlayerMobile;

				Account from_account = (from.Account) as Account;
				String createdon = from_account.Created.ToString();

				//m.SendMessage("Total Playtime  : " + from.GameTime);
                                m.SendMessage("Total Playtime : " + from.GameTime.Days + "d " + from.GameTime.Hours + "h " + from.GameTime.Minutes + "m " + from.GameTime.Seconds + "s");
				m.SendMessage("Account Created : " + createdon);
			}
		}
	}
}