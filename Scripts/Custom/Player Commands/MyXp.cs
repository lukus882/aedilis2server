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
	public class MyXP
	{
		public static void Initialize() 
		{ 
			CommandSystem.Register( "MyXP", AccessLevel.Player, new CommandEventHandler( MyXP_OnCommand ) );
			CommandSystem.Register( "MyXPGump", AccessLevel.Player, new CommandEventHandler( MyXPGump_OnCommand ) );
			
		}    
      
		[Usage( "MyXP" )] 
		[Description( "Displays your Xp level." )]
		public static void MyXP_OnCommand( CommandEventArgs e ) 
		{
			Mobile from = e.Mobile;
			if ( from != null )
			{
				from.SendMessage( "Your Current Xp Level is '{0}'",((PlayerMobile)from).PlayerLevel );
				from.SendMessage( "Your Percent Complete is  '{0}'",((PlayerMobile)from).Percent, "%" );
			}
		}

		[Usage( "MyXPGump" )]
		[Description( "Displays your Xp level." )]
		public static void MyXPGump_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if ( from != null )
			{
				from.CloseGump( typeof( MyXPGump ) );
				from.SendGump( new MyXPGump( from ) );
			}
		}
	}
}

namespace Server.Gumps
{
	public class MyXPGump : Gump
	{
		Mobile From;
		public MyXPGump( Mobile m ) : base( 0, 0 )
		{
			From = m;
			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;
			AddPage(0);
			AddBackground(0, 0, 172, 123, 9270);
			AddLabel(35, 10, 1160, @"Current XP Level"); 	
			AddLabel(15, 35, 1149, @"XP: ");
			AddLabel(65, 35, 1149, String.Format("{0}",((PlayerMobile)From).PlayerLevel));
			AddLabel(15, 60, 1149, @"%:");
			AddLabel(65, 60, 1149, String.Format("{0}",((PlayerMobile)From).Percent));
		}
	}
}