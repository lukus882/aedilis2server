using System;
using Server;
using System.Collections.Generic;
using Server.Commands; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;

namespace Server.Gumps
{ 
   public class RalphGump : Gump 
   { 
      public static void Initialize() 
      { 
         CommandSystem.Register( "RalphGump", AccessLevel.GameMaster, new CommandEventHandler( RalphGump_OnCommand ) ); 
      } 

      public static void RalphGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new RalphGump( e.Mobile ) ); 
      } 

      public RalphGump( Mobile owner ) : base( 50,50 ) 
      { 
//----------------------------------------------------------------------------------------------------

				AddPage( 0 );
			AddImageTiled(  54, 33, 369, 400, 2624 );
			AddAlphaRegion( 54, 33, 369, 400 );

			AddImageTiled( 416, 39, 44, 389, 203 );
//--------------------------------------Window size bar--------------------------------------------
			
			AddImage( 97, 49, 9005 );
			AddImageTiled( 58, 39, 29, 390, 10460 );
			AddImageTiled( 412, 37, 31, 389, 10460 );
			AddLabel( 140, 60, 0x34, "Ralph's Banner Quest" );
			

			AddHtml( 107, 140, 300, 230, "<BODY>" +
//----------------------/----------------------------------------------/
"<BASEFONT COLOR=WHITE>*Ralph glances up with a worried look in his eyes*<BR><BR>Please kind Traveler,  could you assist me?<BR>" +
"<BASEFONT COLOR=WHITE>I am nothing but a poor banner crafter for Lord British and I have run out of Worm Silk to finish making his banners.<BR>" +
"<BASEFONT COLOR=WHITE>If you  bring me 10 Worm Silk,  I will reward you with one of my banners.<BR>" +
"<BASEFONT COLOR=WHITE>You can find these Silk Worms around some of the farm areas, as they are good for plants, but if you attack them, they are a little pesky!<BR> "+
"<BASEFONT COLOR=WHITE>When you kill the Silk Worm, use a knife and carve into the worm goo to get the Worm Silk." +
 "</BODY>", false, true);
			

			AddImage( 430, 9, 10441);
			AddImageTiled( 40, 38, 17, 391, 9263 );
			AddImage( 6, 25, 10421 );
			AddImage( 34, 12, 10420 );
			AddImageTiled( 94, 25, 342, 15, 10304 );
			AddImageTiled( 40, 427, 415, 16, 10304 );
			AddImage( -10, 314, 10402 );
			AddImage( 56, 150, 10411 );
			AddImage( 155, 120, 2103 );
			AddImage( 136, 84, 96 );

			AddButton( 225, 390, 0xF7, 0xF8, 0, GumpButtonType.Reply, 0 ); 

//--------------------------------------------------------------------------------------------------------------
      } 

      public override void OnResponse( NetState state, RelayInfo info ) //Function for GumpButtonType.Reply Buttons 
      { 
         Mobile from = state.Mobile; 

         switch ( info.ButtonID ) 
         { 
            case 0: //Case uses the ActionIDs defenied above. Case 0 defenies the actions for the button with the action id 0 
            { 
               //Cancel 
               from.SendMessage( "Thank you most kindly stranger!" );
               break; 
            } 

         }
      }
   }
}
