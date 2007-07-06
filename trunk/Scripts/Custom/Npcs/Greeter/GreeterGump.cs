using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{ 
   public class GreeterGump : Gump 
   { 
      public static void Initialize() 
      { 
         CommandSystem.Register( "GreeterGump", AccessLevel.GameMaster, new CommandEventHandler( GreeterGump_OnCommand ) ); 
      } 

      private static void GreeterGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new GreeterGump( e.Mobile ) ); 
      } 

      public GreeterGump( Mobile owner ) : base( 50,50 ) 
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
			AddLabel( 140, 60, 0x34, "Greeter" );
			

			AddHtml( 107, 140, 300, 230, "<BODY>" +
//----------------------/----------------------------------------------/
"<BASEFONT COLOR=White><I>The Greeter Welcomes You To Aedilis</I><br><br>" +
"<BASEFONT Color=White>If this is your first time visiting please take some time to visit our website.<br><br>" + 
"<BASEFONT COLOR=White>You can get there by using the welcome book in your pack or by going to www.aedilis.us<br><br>" +
"<BASEFONT COLOR=White>You can chat with us using the [chat command for global or [irc command for our chat room<br><br>" +
"<BASEFONT COLOR=White>Just say [chat blah blah or [irc blah blah<br><br>" +
"<BASEFONT COLOR=White>Thanks for stopping by and I hope you enjoy your stay with us. - Stormwolf<br><br>" +
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
               from.SendMessage( "Greeter Says Thank You For Joining The Shard Of The World Of Goddess" );
               break; 
            } 

         }
      }
   }
}
