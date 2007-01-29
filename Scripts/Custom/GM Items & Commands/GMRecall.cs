using Server;
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Misc;
using Server.Targeting;
using Server.Commands;

namespace Server.Commands
{
   public class GMRecall
   {
      public static void Initialize()
      {
         Register();
      }

      public static void Register()
      {
        CommandSystem.Register( "Recall", AccessLevel.Counselor, new 
CommandEventHandler( GMRecall_OnCommand ) );
      }

      private class RecallTarget : Target
      {
         public RecallTarget( Mobile m ) : base( 1, false, 
TargetFlags.None )
         {
         }

         protected override void OnTarget( Mobile from, object target )
         {
            if ( target is RecallRune )
            {
               RecallRune t = ( RecallRune )target;

               if ( t.Marked == true )
               {
                  from.Location = t.Target;
                  from.Map = t.TargetMap;
               }
	       else
		  from.SendLocalizedMessage( 502354 ); // Target is not marked.
            }
            else
	    if ( target is Runebook )
	    {
		RunebookEntry e = ((Runebook)target).Default;

		if ( e != null )
		{
		   from.Location = e.Location;
		   from.Map = e.Map;
		}
		else
		   from.SendLocalizedMessage( 502354 ); // Target is not marked.
            }
            else
            {
               from.SendMessage( "That can not be done,!" );
            }
         }
      }

      [Usage( "Recall" )]
      [Description( "Recall on rune." )]
      private static void GMRecall_OnCommand( CommandEventArgs e )
      {
         e.Mobile.SendMessage( "Target A Default Runebook" );
         e.Mobile.Target = new RecallTarget( e.Mobile );
      }
   }
}
