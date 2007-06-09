
using System; 
using Server; 


//--Magery Full Spellbook Start-----------------------------------------------------------

namespace Server.Items 
{ 
   public class HorseRaceSpellbook : Spellbook
   { 

      [Constructable] 
      public HorseRaceSpellbook()
      {
          
            this.Content = ulong.MaxValue; 
	    this.Hue = 48;
	    this.Name = "First Horse Race Champ Spellbook";
      } 

      public HorseRaceSpellbook( Serial serial ) : base( serial ) 
      { 
      } 

      public override void Serialize( GenericWriter writer ) 
      { 
         base.Serialize( writer ); 
         writer.Write( (int) 0 ); 
      } 

      public override void Deserialize( GenericReader reader ) 
      { 
         base.Deserialize( reader ); 
         int version = reader.ReadInt(); 
      } 
   } 
}

