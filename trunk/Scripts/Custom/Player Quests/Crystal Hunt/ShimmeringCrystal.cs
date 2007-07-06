using System; 
using Server; 
 

namespace Server.Items 
{ 
   	public class QuestShimeringCrystal : Item 
   	{ 
    
      		[Constructable] 
      		public QuestShimeringCrystal() : base( 0x35DA ) 
      		{ 
         		Weight = 1.0;  
         		Movable = true;
         		Name="A Shimmering Crystal [Quest Item]";   
      		} 

      		public QuestShimeringCrystal( Serial serial ) : base( serial ) 
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
