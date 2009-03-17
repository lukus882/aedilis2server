using System;
using System.Collections;
using Server;
using Server.Mobiles;
using Server.Targeting;
using Server.Network;
using Server.Commands;

namespace Server.Misc
{
  public class BodInfoCommand
	{
		public static void Initialize()
			{
	      CommandSystem.Register( "BodInfo", AccessLevel.Player, new CommandEventHandler( BodInfo_OnCommand ) );
			}
			
		[Usage( "BodInfo" )]
		[Description( "Tells you how much time remaining until your next BOD is available." )]
		public static void BodInfo_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			
			string SmithBodInfo = string.Format("Your next smith BOD is available in {0} hours, {1} minutes and {2} seconds!", ((PlayerMobile)from).NextSmithBulkOrder.Hours, 
			((PlayerMobile)from).NextSmithBulkOrder.Minutes, ((PlayerMobile)from).NextSmithBulkOrder.Seconds   ); 
			
			string TailorBodInfo = string.Format("Your next tailor BOD is available in {0} hours, {1} minutes and {2} seconds!", ((PlayerMobile)from).NextTailorBulkOrder.Hours, 
			((PlayerMobile)from).NextTailorBulkOrder.Minutes, ((PlayerMobile)from).NextTailorBulkOrder.Seconds   );
			
			if( ((PlayerMobile)from).NextSmithBulkOrder == TimeSpan.Zero)
					{
						((PlayerMobile)from).SendMessage( "You may pick up a new smith BOD now!");
					}
					else
						{
							((PlayerMobile)from).SendMessage ( SmithBodInfo );
						}
							
			if( ((PlayerMobile)from).NextTailorBulkOrder == TimeSpan.Zero)
				{
					((PlayerMobile)from).SendMessage( "You may pick up a new tailor BOD now!");
				}					
			else
			{
				((PlayerMobile)from).SendMessage ( TailorBodInfo );
			}		
		}
 	}
}