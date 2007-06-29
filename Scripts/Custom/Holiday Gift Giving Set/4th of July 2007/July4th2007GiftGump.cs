using System;
using System.Net;
using Server;
using Server.Accounting;
using Server.Gumps;
using Server.Items;
using Server.Network;
using Server.Scripts.Commands;


namespace Server.Items

{
  public class July4th2007GiftGump : Gump
   {
      private Mobile m_Mobile;

      public July4th2007GiftGump( Mobile from ) : base( 30, 20 )
      {
         m_Mobile = from;

         AddPage( 0 );

         AddBackground( 0, 0, 300, 370, 5054 );
         AddBackground( 8, 8, 284, 354, 3000 );

         AddLabel( 60, 12, 0, "Choose a Holiday Gift" );

         Account a = from.Account as Account;

         AddLabel( 52, 60, 0, "Blessed Statue" );
         AddButton( 12, 60, 4005, 4007, 1, GumpButtonType.Reply, 1 );
         AddLabel( 52, 80, 0, "Carved Wooden Screen" );
         AddButton( 12, 80, 4005, 4007, 2, GumpButtonType.Reply, 2 );
         AddLabel( 52, 100, 0, "Dragon Brazier" );
         AddButton( 12, 100, 4005, 4007, 3, GumpButtonType.Reply, 3 );
         AddLabel( 52, 120, 0, "Mongbat Dart Board" );
         AddButton( 12, 120, 4005, 4007, 4, GumpButtonType.Reply, 4 );
         AddLabel( 52, 140, 0, "A Sprinkler" );
         AddButton( 12, 140, 4005, 4007, 5, GumpButtonType.Reply, 5 );
         AddLabel( 52, 180, 0, "Close" );
         AddButton( 12, 180, 4005, 4007, 0, GumpButtonType.Reply, 6 );
          }
      public override void OnResponse( NetState state, RelayInfo info )
      {
         Mobile from = state.Mobile;

         switch ( info.ButtonID )
         {
            case 0: //Close Gump
            {
               from.AddToBackpack( new July4th2007GiftDeed() );
               from.CloseGump( typeof( July4th2007GiftGump ) );
               break;
            }
            case 1:
            {
               from.AddToBackpack( new BlessedStatue() );
               from.CloseGump( typeof( July4th2007GiftGump ) );
               break;
            }
            case 2: 
            {
               from.AddToBackpack( new CarvedWoodenScreen() );
               from.CloseGump( typeof( July4th2007GiftGump ) );
               break;
            }
            case 3: 
            {
                from.AddToBackpack( new DragonBrazier() );
                from.CloseGump( typeof( July4th2007GiftGump ) );
                break;
            }
            case 4: 
            {
               from.AddToBackpack( new MongbatDartboard() );
               from.CloseGump( typeof( July4th2007GiftGump ) );
               break;
            }
            case 5: 
            {
               from.AddToBackpack( new Sprinkler() );
	       from.AddToBackpack( new SprinklerContainer() );
               from.CloseGump( typeof( July4th2007GiftGump ) );
               break;
            }
         }
      }
   }
}
