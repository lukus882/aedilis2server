/***************************************
 * Created By Broadside Aka Bad Karma **
 *************************************** 
 * Special thanks to Lord_Greywolf for *
 * his help with the random sounds *****
 ***************************************
 ***************************************
 */
using System;

namespace Server.Items 
{ 
      public class BellOfCourage : Item 
      { 
          
             int Sound = Utility.RandomList(245, 246, 247, 248, 249, 250, 251, 252, 253, 254, 255, 256, 257, 258, 259, 260, 261, 262);

             [Constructable] 
             public BellOfCourage() : base( 0x1c12 ) 
             { 
		     Stackable = false; 
		     Weight = 1.0; 
		     Name = "Bell Of Courage";
	             Hue = Utility.RandomList ( 1, 91, 4, 39, 11, 46, 487, 567, 53, 502, 21, 68, 206, 1253, 1153, 32, 12); 
		     LootType = LootType.Blessed;
             }
            
             public BellOfCourage( Serial serial ) : base( serial ) 
             {         
    	     }

             public override void OnDoubleClick( Mobile from ) 
             { 
             	Effects.PlaySound( from, from.Map, (Sound) );
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
