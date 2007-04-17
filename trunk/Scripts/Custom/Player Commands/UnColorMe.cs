using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;
using Server.Commands;
using System.Collections;
using System.Collections.Generic;


namespace Server.LucidNagual
{
	public class UnColorCommand
	{
		public static void Initialize()
		{
			CommandSystem.Register( "UnColorMe", AccessLevel.Player, new CommandEventHandler( UnColorMe_OnCommand ) );
		}
			
		[Usage( "UnColorMe" )]
		[Description( "UnHues all items that are equipped." )]
		public static void UnColorMe_OnCommand( CommandEventArgs e )
		{
			PlayerMobile pm = e.Mobile as PlayerMobile;
			
			List<Item> playerItems = new List<Item> ( pm.Items );
			List<Item> wornitems = new List<Item>();			
			
			foreach ( Item item in playerItems )
			{
				if ( ( item.Layer != Layer.Bank ) && ( item.Layer != Layer.Backpack ) && ( item.Layer != Layer.Hair ) && ( item.Layer != Layer.FacialHair ) )
				{
					if ( item != null )
						wornitems.Add( item );
				}
			}		
			
			if ( wornitems.Count > 0 )
			{
				for ( int i = 0; i < wornitems.Count; i++ )
				{
					Item item = wornitems[i];
					
					item.Hue = 0;
				}
			}			
		}		
	}
}
