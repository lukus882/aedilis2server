using System; 
using Server; 
using Server.Gumps; 
using Server.Network;

using Server.Commands;

namespace Server.Gumps 
{ 
   public class TravelStoneGump : Gump 
   { 
      public static void Initialize() 
      { 
         CommandSystem.Register( "TravelStoneGump", AccessLevel.GameMaster, new CommandEventHandler( TravelStoneGump_OnCommand ) ); 
      } 

      private static void TravelStoneGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new TravelStoneGump( e.Mobile ) ); 
      } 

      public TravelStoneGump( Mobile owner ) : base( 50,50 ) 
      { 
        
         AddPage( 0 ); 
         AddBackground( 0, 0, 500, 320, 2600 ); 
         AddImage( 320, 40, 60468, 0x34 ); 
         AddImage( 320, 40, 120 ); 
         AddImage( 320, 40, 60441 ); 
         AddImage( 320, 40, 60999, 0x34 ); 
         AddImage( 320, 40, 60476, 0x134 ); 
         AddImage( 320, 40, 50937, 0x134 ); 
         AddImage( 320, 40, 60438, 0x134 ); 
         AddImage( 320, 40, 60439, 0x134 ); 
         AddImage( 320, 40, 60440, 0x134 ); 
         AddButton( 395, 275, 0xFB1, 0xFB3, 0, GumpButtonType.Reply, 0 ); 
         AddLabel( 425, 275, 0x34, "Exit" ); 
        
         AddPage( 1 ); 
          AddHtml( 20, 20, 460, 27,"          Felucca Towns", true, false ); 
          AddButton( 285, 260, 0x1196, 0x1196, 2, GumpButtonType.Page, 2 ); 
          
         AddButton( 35, 63, 0x2623, 0x2622, 1, GumpButtonType.Reply, 0 ); 
         AddLabel( 55, 60, 0x34, "Britian" ); 

         AddButton( 35, 93, 0x2623, 0x2622, 2, GumpButtonType.Reply, 0 ); 
         AddLabel( 55, 90, 0x34, "Delucia" ); 

         AddButton( 35, 123, 0x2623, 0x2622, 3, GumpButtonType.Reply, 0 ); 
         AddLabel( 55, 120, 0x34, "Ocllo" ); 

         AddButton( 35, 153, 0x2623, 0x2622, 4, GumpButtonType.Reply, 0 ); 
         AddLabel( 55, 150, 0x34, "Vesper" ); 

         AddButton( 35, 183, 0x2623, 0x2622, 5, GumpButtonType.Reply, 0 ); 
         AddLabel( 55, 180, 0x34, "Cove" ); 

         AddButton( 35, 213, 0x2623, 0x2622, 7, GumpButtonType.Reply, 0 ); 
         AddLabel( 55, 210, 0x34, "Jhelom" ); 

         AddButton( 35, 243, 0x2623, 0x2622, 8, GumpButtonType.Reply, 0 ); 
         AddLabel( 55, 240, 0x34, "Minoc" ); 

         AddButton( 135, 63, 0x2623, 0x2622, 9, GumpButtonType.Reply, 0 ); 
         AddLabel( 155, 60, 0x34, "Moonglow " ); 

         AddButton( 135, 93, 0x2623, 0x2622, 10, GumpButtonType.Reply, 0 ); 
         AddLabel( 155, 90, 0x34, "Nujel'm " ); 

         AddButton( 135, 123, 0x2623, 0x2622, 11, GumpButtonType.Reply, 0 ); 
         AddLabel( 155, 120, 0x34, "Serp's hold" ); 

         AddButton( 135, 153, 0x2623, 0x2622, 12, GumpButtonType.Reply, 0 ); 
         AddLabel( 155, 150, 0x34, "Skara Brae" ); 

         AddButton( 135, 183, 0x2623, 0x2622, 13, GumpButtonType.Reply, 0 ); 
         AddLabel( 155, 180, 0x34, "Trinsic" ); 

         AddButton( 135, 213, 0x2623, 0x2622, 14, GumpButtonType.Reply, 0 ); 
         AddLabel( 155, 210, 0x34, "Wind" ); 

         AddButton( 135, 243, 0x2623, 0x2622, 15, GumpButtonType.Reply, 0 ); 
         AddLabel( 155, 240, 0x34, "Yew" ); 
          
          AddButton( 235, 63,0x2623, 0x2622, 16, GumpButtonType.Reply, 0 ); 
         AddLabel( 255, 60, 0x34, "Papua" ); 
          
         AddButton( 235, 93, 0x2623, 0x2622, 17, GumpButtonType.Reply, 0 ); 
         AddLabel( 255, 90, 0x34, "Magincia" ); 
          
         AddButton( 235, 123, 0x2623, 0x2622, 18, GumpButtonType.Reply, 0 ); 
         AddLabel( 255, 120, 0x234, "Buc's Den" ); 


         AddPage( 2 ); 
          AddHtml( 20, 20, 460, 27,"           Dungeons Felucca", true, false ); 
          
          AddButton( 165, 260, 0x119a, 0x119a, 1, GumpButtonType.Page, 1 ); 
          AddButton( 285, 260, 0x1196, 0x1196, 3, GumpButtonType.Page, 3 ); 
        
         AddButton( 35, 63, 0x2623, 0x2622, 23, GumpButtonType.Reply, 0 ); 
         AddLabel( 55, 60, 0x34, "Covetous" ); 

         AddButton( 35, 93, 0x2623, 0x2622, 28, GumpButtonType.Reply, 0 ); 
         AddLabel( 55, 90, 0x34, "Deciet" ); 

         AddButton( 35, 123, 0x2623, 0x2622, 32, GumpButtonType.Reply, 0 ); 
         AddLabel( 55, 120, 0x34, "Despise" ); 

         AddButton( 35, 153, 0x2623, 0x2622, 36, GumpButtonType.Reply, 0 ); 
         AddLabel( 55, 150, 0x34, "Destard" ); 

         AddButton( 35, 183, 0x2623, 0x2622, 39, GumpButtonType.Reply, 0 ); 
         AddLabel( 55,  180, 0x34, "Hyloth" ); 

         AddButton( 35, 213, 0x2623, 0x2622, 43, GumpButtonType.Reply, 0 ); 
         AddLabel( 55, 210, 0x34, "Shame" ); 

         AddButton( 35, 243, 0x2623, 0x2622, 48, GumpButtonType.Reply, 0 ); 
         AddLabel( 55, 240, 0x34, "Wrong" );  
          
  } 

      public override void OnResponse( NetState state, RelayInfo info ) //Function for GumpButtonType.Reply Buttons 
      { 
         Mobile from = state.Mobile; 

         switch ( info.ButtonID ) 
         { 
            case 0: //Case uses the ActionIDs defenied above. Case 0 defenies the actions for the button with the action id 0 
            { 
               //Cancel 
                from.SendMessage( "You choose to stay put" ); 
               from.Frozen = false;
               break; 
            } 
            case 1: //Case uses the ActionIDs defenied above. Case 0 defenies the actions for the button with the action id 0 
            { 
               //Britian 
              from.Map = Map.Felucca; 
               from.Location = new Point3D(1430,1703,9); 
               from.Frozen = false;
               break;  
            } 
            case 2: //Same as above 
            { 
               //Delucia 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(5272,3995,37); 
               from.Frozen = false;
               break;  
            } 
            case 3: 
            { 
               //Ocllo 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(3685,2523,0); 
               from.Frozen = false;
               break;  
            } 
            case 4: 
            { 
               //Vesper 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(2899,676,0); 
               from.Frozen = false;
               break; 
            } 
            case 5: 
            { 
               //Cove 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(2237,1196,0); 
               from.Frozen = false;
               break;  
            } 
            case 7: 
            { 
               //Jhelom 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(1324,3778,0); 
               from.Frozen = false;
               break; 
            } 
            case 8: 
            { 
               //Minoc 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(2503,563,0); 
               from.Frozen = false;
               break; 
            } 
            case 9: 
            { 
               //Moonglow 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(4469,1180,0); 
               from.Frozen = false;
               break; 
            } 
            case 10: 
            { 
               //Nujel'm 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(3770,1308,0); 
               from.Frozen = false;
               break; 
            } 
            case 11: 
            { 
               //Serp's hold 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(2895,3479,15); 
               from.Frozen = false;
               break; 
            } 
            case 12: 
            { 
               //Skara Brae 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(594,2155,0); 
               from.Frozen = false;
               break; 
            } 
            case 13: 
            { 
               //Trinsic 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(1823,2826,0); 
               from.Frozen = false;
               break; 
            } 
            case 14: 
            { 
               //Wind 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(1361,895,0); 
               from.Frozen = false;
               break; 
            } 
            case 15: 
            { 
               //Yew 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(659,813,0); 
               from.Frozen = false;
               break; 
            } 
            case 16: 
            { 
               //Papua 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(5675,3142,12); 
               from.Frozen = false;
               break; 
            } 
            case 17: 
            { 
               //Magincia 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(3728,2164,20 ); 
               from.Frozen = false;
               break; 
            } 
            case 18: 
            { 
               //Buc's Den 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(2724,2192,0); 
               from.Frozen = false;
               break;  
            } 
            case 23: 
            { 
               //Covetous 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(2499,921,0); 
               from.Frozen = false;
               break; 
            } 
            case 24: 
            { 
               //Covetous Lvl2 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(2388,836,0); 
               from.Frozen = false;
               break; 
            } 
            case 25: 
            { 
               //Covetous Lvl3 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(2546,857,0); 
               from.Frozen = false;
               break; 
            } 
            case 26: 
            { 
               //Covetous Lvl4 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(5593,1844,0); 
               from.Frozen = false;
               break; 
            } 
            case 27: 
            { 
               //Covetous Lvl5 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(5560,1825,0); 
               from.Frozen = false;
               break; 
            } 
            case 28: 
            { 
               //Deciet 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(4111,434,5); 
               from.Frozen = false;
               break; 
            } 
            case 29: 
            { 
               //Deciet Lvl2 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(5218,582,0); 
               from.Frozen = false;
               break; 
            } 
            case 30: 
            { 
               //Deciet Lvl3 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(5342,578,0); 
               from.Frozen = false;
               break; 
            } 
            case 31: 
            { 
               //Deciet Lvl4 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(5219,757,-20); 
               from.Frozen = false;
               break; 
            } 
            case 32: 
            { 
               //Despise 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(1300,1080,0); 
               from.Frozen = false;
               break; 
            } 
            case 33: 
            { 
               //Despise Lvl1 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(5498,570,59); 
               from.Frozen = false;
               break;  
            } 
            case 34: 
            { 
               //Despise Lvl2 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(5517,673,20); 
               from.Frozen = false;
               break; 
            } 
            case 35: 
            { 
               //Despise Lvl3 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(5392,756,5); 
               from.Frozen = false;
               break; 
            } 
            case 36: 
            { 
               //Destard 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(1176,2639,0); 
               from.Frozen = false;
               break; 
            } 
            case 37: 
            { 
               //Destard Lvl2 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(5131,901,0); 
               from.Frozen = false;
               break; 
            } 
            case 38: 
            { 
               //Destard Lvl3 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(5146,809,0); 
               from.Frozen = false;
               break; 
            } 
            case 39: 
            { 
               //Hythloth 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(4722,3824,0); 
               from.Frozen = false;
               break;  
            } 
            case 40: 
            {  
               //Hythloth Lvl2 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(5906,100,0); 
               from.Frozen = false;
               break;  
            } 
            case 41: 
            {  
               //Hythloth Lvl3 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(5917,169,22); 
               from.Frozen = false;
               break; 
            } 
            case 42: 
            {  
               //Hythloth Lvl4 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(6036,193,22); 
               from.Frozen = false;
               break; 
            } 
            case 43: 
            {  
               //Shame 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(513,1562,0); 
               from.Frozen = false;
               break; 
            } 
            case 44: 
            {  
               //Shame Lvl2 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(5485,21,-30); 
               from.Frozen = false;
               break; 
            } 
            case 45: 
            {  
               //Shame Lvl3 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(5604,98,0); 
               from.Frozen = false;
               break; 
            } 
            case 46: 
            {  
               //Shame Lvl4 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(5542,170,0); 
               from.Frozen = false;
               break; 
            } 
            case 47: 
            {  
               //Shame Lvl5 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(5507,166,0); 
               from.Frozen = false;
               break; 
            } 
            case 48: 
            {  
               //Wrong 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(2042,221,14); 
               from.Frozen = false;
               break; 
            } 
            case 49: 
            {  
               //Wrong Lvl2 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(5826,596,0); 
               from.Frozen = false;
               break; 
            } 
            case 50: 
            {  
               //Wrong Lvl3 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(5826,596,0); 
               from.Frozen = false;
               break;  
            } 
            case 51: 
            {  
               //Wrong Lvl4 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(5701,659,0); 
               from.Frozen = false;
               break; 
            } 
            case 52: 
            {  
               //Fire 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(2923,3409,8); 
               from.Frozen = false;
               break; 
            } 
            case 53: 
            {  
               //Fire Lvl1 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(5787,1341,0); 
               from.Frozen = false;
               break; 
            } 
            case 54: 
            {  
               //Ice 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(1999,81,4); 
               from.Frozen = false;
               break; 
            } 
            case 55: 
            {  
               //Ice Lvl2 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(5710,146,-43); 
               from.Frozen = false;
               break; 
            } 
            case 64: 
            {  
               //Britian 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(1334,2005,0); 
               from.Frozen = false;
               break; 
            } 
            case 65: 
            {  
               //MoonGlow 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(4465,1288,0); 
               from.Frozen = false;
               break; 
            } 
            case 66: 
            {  
               //Jhelom 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(1329,3777,0); 
               from.Frozen = false;
               break; 
            } 
            case 67: 
            {  
               //Yew 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(659,813,0); 
               from.Frozen = false;
               break; 
            } 
            case 68: 
            {  
               //Minoc 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(2701,697,0); 
               from.Frozen = false;
               break; 
            } 
            case 69: 
            {  
               //Trinsic 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(1828,2952,-25); 
               from.Frozen = false;
               break; 
            } 
            case 70: 
            {  
               //Skara Brae 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(643,2072,0); 
               from.Frozen = false;
               break; 
            } 
            case 71: 
            {  
               //Magincia 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(3564,2145,33); 
               from.Frozen = false;
               break; 
            } 
            case 72: 
            {  
               //Buc's Den 
               from.Map = Map.Felucca; 
               from.Location = new Point3D(2724,2192,0); 
               from.Frozen = false;
               break; 
            } 
         } 
      } 
   } 
}