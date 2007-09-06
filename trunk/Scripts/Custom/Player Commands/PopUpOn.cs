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
	public class PopUpOn
	{
		public static void Initialize()
		{
			CommandSystem.Register( "PopUpOn", AccessLevel.Player, new CommandEventHandler( PopUpOn_OnCommand ) );
		}

		private static void PopUpOn_OnCommand( CommandEventArgs e )
		{
			Mobile m = e.Mobile;
			PlayerMobile pm = (PlayerMobile)m;
			if( pm is PlayerMobile )
			{

				pm.PopUpToggle = true;
				pm.SendMessage("Pop Up Messages Have Been Turned On");


			}
		}
	}
}