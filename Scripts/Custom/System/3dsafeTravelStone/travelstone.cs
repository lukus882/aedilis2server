using System; 
using Server; 
using Server.Gumps; 
using Server.Network; 
using Server.Menus; 
using Server.Menus.Questions; 

namespace Server.Items 
{ 
   public class TravelStone2 : Item 
   { 
      [Constructable] 
      public TravelStone2() : base( 0xED5 ) 
      { 
         Hue = 0x4EC; 
         Movable = false; 
         Name = "Travel Stone"; 
      } 

      public TravelStone2( Serial serial ) : base( serial ) 
      { 
      } 

      public override void OnDoubleClick( Mobile from ) 
      { 
         from.SendGump( new TravelStoneGump( from ) ); 
         from.Frozen = true;
      } 

      public override void Serialize( GenericWriter writer ) 
      { 
         base.Serialize( writer ); 

         writer.Write( (int) 0 ); // version 
      } 

      public override void Deserialize( GenericReader reader ) 
      { 
         base.Deserialize( reader ); 

         int version = reader.ReadInt(); 
      } 
   } 
}