using System;
using System.Collections;
using Server;
using Server.Mobiles;
using Server.Targeting;
using Server.Network;
using Server.Commands;
using Server.Engines.XmlSpawner2;

namespace Server.Misc
{
  public class SiegeInfoCommand
	{
		public static void Initialize()
			{
	      		CommandSystem.Register( "SiegeInfo", AccessLevel.Player, new CommandEventHandler( SiegeInfo_OnCommand ) );
			}
			
		[Usage( "SiegeInfo" )]
		[Description( "Tells you about the siege info related to your target" )]
		public static void SiegeInfo_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			e.Mobile.BeginTarget( -1, false, TargetFlags.None, new TargetCallback( SiegeInfo_OnTarget ) );
			e.Mobile.SendMessage("Target structure you want to get siege information about");
		}	
		
		
		public static void SiegeInfo_OnTarget( Mobile from, object targeted )	
		{	

			int hits = XmlSiege.GetHits(targeted);
			int maxhits = XmlSiege.GetHitsMax(targeted);


						if( hits > 0 )
							{
								from.SendMessage( "The Current HP of this siege object is '{0}'", ( hits ) );
								from.SendMessage( "The Max HP of this siege object is '{0}'", ( maxhits ) );
							}
						else	
							{
								from.SendMessage("There is no siege information available about that object" );
							}		
							
	
			
		}			

	}
}