using System; 
using Server; 

namespace Server.Items 
{ 

   	public class EnchantedShovelHead : Item 
   	{ 
       
      		[Constructable] 
      		public EnchantedShovelHead() 
      		{ 
      		
				Name = "The Head Of An Enchanted Shovel";          
         		ItemID=3971;
         		Weight= 10.0;
         		Hue = 1581;
       	
       
      		} 

      		public override bool ForceShowProperties{ get{ return ObjectPropertyList.Enabled; } }

     		public EnchantedShovelHead( Serial serial ) : base( serial ) 
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