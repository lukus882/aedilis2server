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
	public class PlayDeadCommand
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
				
				if ( e.Speech.ToLower().IndexOf( "time to play dead" ) >= 0 ) 
				{
					from.Animate( 0, 6, 22, false, false, 200 );
					from.PlaySound( from.Female ? 791 : 1063 );
				}
			}
		}

	}

}