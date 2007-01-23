// Script: Status Command
// Version: 1.1
// Author: Oak (ssalter)
// Servers: RunUO 2.0
// Date: 7/7/2006
// Purpose: 
// Player Command. Allows players to view online time, when they joined, fame, karma.
//  all they need to do is say "what is my status"  to run the command. A [  is not necessary.
// History: 
// I saw a few 'status' types commands on Runuo.com, but do not remember which was my inspiration
//  Written for RunUO 1.0 shard in February 2005.
//  Modified for RunUO 2.0.
using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Guilds;
using Server.Gumps;
using Server.Network;
using Server.Mobiles;
using Server.Accounting;
namespace Server.Misc
{
	public class StatusCommand
	{		

		public static void Initialize()
		{
			EventSink.Speech += new SpeechEventHandler( Name ); 
		}

		public static void Name( SpeechEventArgs e )
		{
			Mobile m = e.Mobile; 
	
			if( m is PlayerMobile )
			{
				PlayerMobile from = (PlayerMobile)m; 
				
				if ( e.Speech.ToLower().IndexOf( "what is my status" ) >= 0 ) 
				{
					from.Animate( 0, 6, 22, false, false, 200 );
					from.PlaySound( from.Female ? 791 : 1063 );
					from.CloseGump( typeof( TermGump ) );
					from.SendGump ( new TermGump( from ) );
				}
			}
		}
		public class TermGump : Gump
		{
			public TermGump( Mobile from ) : base( (640 - 360) / 2, (480 - 150) / 2 )
			{

				Mobile m_from = from;
				string tera = m_from.Name;
				int terd = m_from.Fame;
				int tere = m_from.Karma;
				PlayerMobile pm_from = (PlayerMobile)from;
				int terf = pm_from.GameTime.Hours + (pm_from.GameTime.Days * 24);
				TimeSpan ctime=DateTime.Now - pm_from.SessionStart;
				string curr = ctime.Hours.ToString() + ":" + ctime.Minutes.ToString() + ":" + ctime.Seconds.ToString();
				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;
				this.AddPage(0);
				this.AddBackground(0, 0, 460, 150, 9270);
				this.AddAlphaRegion( 0, 0, 460, 150 );
				this.AddLabel(48, 16, 1150, tera );
				this.AddLabel(48, 32, 255, "Creation Date:" );
				this.AddLabel(48, 48, 255, "Online Time");
				this.AddLabel(48, 64, 255, "Current Session");
				this.AddLabel(48, 80, 255, "Fame");
				this.AddLabel(48, 96, 255, "Karma");
				
				Account xx = ((Mobile)pm_from).Account as Account;

				this.AddLabel(198, 32, 255, ": " + xx.Created.ToString()  );
				this.AddLabel(198, 48, 255, ": " + terf.ToString() + " hours." );
				this.AddLabel(198, 64, 255, ": " + curr );
				this.AddLabel(198, 80, 255, ": " + terd.ToString() );
				this.AddLabel(198, 96, 255, ": " + tere.ToString() );
			}
		}

	}

}