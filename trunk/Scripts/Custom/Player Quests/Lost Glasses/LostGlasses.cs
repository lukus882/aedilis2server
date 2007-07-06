using System; 
using Server; 
 

namespace Server.Items 
{ 
   	public class QuestGlasses : Item 
   	{ 
    
      		[Constructable] 
      		public QuestGlasses() : base( 0x2FB8 ) 
      		{ 
         		Weight = 1.0;  
         		Movable = true;
         		Name="A Lost Pair of Glasses [Quest Item]";   
      		} 

      		public QuestGlasses( Serial serial ) : base( serial ) 
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
