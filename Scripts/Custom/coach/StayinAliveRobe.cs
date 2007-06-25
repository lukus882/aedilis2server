using System; 
using Server; 
using Server.Targeting; 
using Server.Items; 

namespace Server.Items 
{ 

   [FlipableAttribute( 0xF5E, 0xF5F )] 
   public class StayinAliveRobe : BaseOuterTorso
{
      [Constructable] 
      public StayinAliveRobe() : base(7939)
      { 
               Weight = 6.0; 
               Hue = 48; 
               Name = "I stayed alive the longest against Coach's Crazy Creatures!";

               
      } 

      public StayinAliveRobe( Serial serial ) : base( serial ) 
      { 
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