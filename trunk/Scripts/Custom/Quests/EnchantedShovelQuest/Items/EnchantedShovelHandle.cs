using System; 
using Server; 

namespace Server.Items 
{ 

   	public class EnchantedShovelHandle : Item 
   	{ 
       
      		[Constructable] 
      		public EnchantedShovelHandle() 
      		{ 
      		
				Name = "The Handle Of An Enchanted Shovel";          
         		ItemID=6202;
         		Weight= 10.0;
         		Hue = 1581;
       	
       
      		} 

      		public override bool ForceShowProperties{ get{ return ObjectPropertyList.Enabled; } }

     		public EnchantedShovelHandle( Serial serial ) : base( serial ) 
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