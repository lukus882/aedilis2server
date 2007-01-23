using System; 
using Server; 
using Server.Targeting; 
using Server.Items; 
using Server.Guilds;

namespace Server.Items 
{ 

   [FlipableAttribute( 0xF5E, 0xF5F )] 
   public class HMR : BaseOuterTorso
{
   private int DefSerUss = 0; 
   private DateTime DefSerTimeLimit = DateTime.Now; 

      [Constructable] 
      public HMR() : base(7939)
      { 
               Weight = 6.0; 
               Hue = 0; 
               DefSerUss = 0; 
               LootType = LootType.Blessed;
               Name = "Guild Robe";

               
      } 

      public HMR( Serial serial ) : base( serial ) 
      { 
      } 
     

      public override bool OnEquip( Mobile from ) 
      { 
      Guild g = from.Guild as Guild;
      if(DefSerUss == 0 && g != null) 
      { 
      DefSerUss = from.Serial; 
      from.Emote( "" + from.Name + ", the robe is now it your personal item!" );  
      this.Name =  from.Name +"'s "  +  this.Name +" [" + g.Abbreviation + "]";
      base.OnEquip( from ); 
      return true;
      }
      else if (g == null )
      {
      this.Name = "Robe";
      this.ItemID = 7939;
      this.Hue = 0;
      this.LootType = LootType.Regular;
      base.OnEquip( from ); 
      return true;
      }
     
            else if(DefSerUss == from.Serial) 
      { 
      from.SendMessage( "Guild Form " + from.Name + " was dressed" ); 
      

   base.OnEquip( from ); 
    
   return true; 
    

      } 
      else 
      { 
      from.Emote( "*It is not your Guild item...!*" ); 
      return false; 
} 
} 


     public override void Serialize( GenericWriter writer ) 
      { 
         base.Serialize( writer ); 
         writer.Write( (int) 0 ); // version 
         writer.Write( (int) DefSerUss ); 
         writer.Write( (DateTime) DefSerTimeLimit ); 
        


      } 

      public override void Deserialize( GenericReader reader ) 
      { 
         base.Deserialize( reader ); 
         int version = reader.ReadInt(); 
         DefSerUss = reader.ReadInt(); 
         DefSerTimeLimit = reader.ReadDateTime(); 
      } 
   } 
}