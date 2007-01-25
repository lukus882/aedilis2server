using System; 
using Server; 

namespace Server.Items 
{ 

   	public class EnchantedShovelArm : Item 
   	{ 
       
      		[Constructable] 
      		public EnchantedShovelArm() 
      		{ 
      		
				Name = "The Arm Of An Enchanted Shovel";          
         		ItemID=3721;
         		Weight= 10.0;
         		Hue = 1581;
       	
       
      		} 

      		public override bool ForceShowProperties{ get{ return ObjectPropertyList.Enabled; } }

     		public EnchantedShovelArm( Serial serial ) : base( serial ) 
     	 	{ 
      		} 

      		public override void Serialize( GenericWriter writer ) 
      		{ 
         		base.Serialize( writer ); 

         		writer.Write( (int) 0 ); 
      		} 
       
      		public override void Deserialize(GenericReader reader) 
      		{ 
         		base.Deserialize( reader ); 

         		int version = reader.ReadInt(); 
      		} 
   	}     
} 