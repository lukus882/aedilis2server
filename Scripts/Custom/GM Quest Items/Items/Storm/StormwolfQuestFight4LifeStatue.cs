using System; 
using Server; 
 

namespace Server.Items 
{ 
   	public class StormwolfQuestFight4LifeStatue : Item 
   	{ 
    
      		[Constructable] 
      		public StormwolfQuestFight4LifeStatue() : base( 0x2848 ) 
      		{ 
         		Weight = 1.0;  
         		Movable = true;
         		Name="I Won The fight For Your Life Event";   
      		} 

      		public StormwolfQuestFight4LifeStatue( Serial serial ) : base( serial ) 
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
