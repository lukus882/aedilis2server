using Server;
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Misc;
using Server.Targeting;
using Server.Commands;

namespace Server.Commands
{
   public class GMMark
   {
      public static void Initialize()
      {
         Register();
      }

      public static void Register()
      {
        CommandSystem.Register( "Mark", AccessLevel.Counselor, new CommandEventHandler( GMMark_OnCommand ) );
      }

      private class MarkTarget : Target
      {
         public MarkTarget( Mobile m ) : base( 1, false, TargetFlags.None )
         {
         }

         protected override void OnTarget( Mobile from, object target )
         {


            if ( target is RecallRune )
            {
                RecallRune t = ( RecallRune )target;

                t.Mark( from );

		from.PlaySound( 0x1FA );
		Effects.SendLocationEffect( from, from.Map, 14201, 16 );
	    }
	   else
	   {
	    from.Send( new MessageLocalized( from.Serial, from.Body, MessageType.Regular, 0x3B2, 3, 501797, from.Name, "" ) ); // I cannot mark that object.
	   }
            
         }
      }

      [Usage( "Mark" )]
      [Description( "Mark a rune." )]
      private static void GMMark_OnCommand( CommandEventArgs e )
      {
         e.Mobile.SendMessage( "Target A Recall Rune" );
         e.Mobile.Target = new MarkTarget( e.Mobile );
      }
   }
}
