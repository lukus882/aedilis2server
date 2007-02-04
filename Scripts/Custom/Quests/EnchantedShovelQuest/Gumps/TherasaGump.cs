using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{ 
   public class TherasaGump : Gump 
   { 
      public static void Initialize() 
      {
          CommandSystem.Register("TherasaGump", AccessLevel.GameMaster, new CommandEventHandler(TherasaGump_OnCommand)); 
      } 

      private static void TherasaGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new TherasaGump( e.Mobile ) ); 
      } 

      public TherasaGump( Mobile owner ) : base( 50,50 ) 
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
			AddLabel( 140, 60, 0x34, "The Enchanted Shovel" );
			

			AddHtml( 107, 140, 300, 230, "<BODY>" +
//----------------------/----------------------------------------------/
"<BASEFONT COLOR=YELLOW>Therasa looks at you with hope in her eyes.<BR><BR>My late Husband was a miner, and his brother was a Blacksmith for the Mages Guild.<BR>" +
"<BASEFONT COLOR=YELLOW>Quite a few years ago, they started work on a shovel that was very strong.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>But my dear Husband was killed in a rock slide before his work was finished.<BR>" +
"<BASEFONT COLOR=YELLOW>I know that he had 3 peices on his corpse, but he was looted quickly after his death<BR><BR>I need to find these peices and finish my Husbands work.<BR>" +
"<BASEFONT COLOR=YELLOW>Will you help me finish my work? If you can find it in your heart to help me, I reward you with a duplicate of the shovel.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>You must take this Magical Connection Box that my Brother in Law has made, this will re-join all the peices.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>The first piece I know is somewhere in the dungeon called shame.<BR><BR> There are quite a few monsters there, so you will have to be brave and kill them all.<BR>The Second is somewhere near Papua...<BR><BR>" +
"<BASEFONT COLOR=YELLOW>The Third and Final piece can be found near the entrance to the dungeon called destard<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Bring me back the shovel and I will be forever in your debt.<BR><BR>" +
						     "</BODY>", false, true);
			
//			<BASEFONT COLOR=#7B6D20>			

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
               from.SendMessage( "please bring me back the shovel" );
               break; 
            } 

         }
      }
   }
}