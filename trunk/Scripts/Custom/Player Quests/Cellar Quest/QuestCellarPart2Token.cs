using System; 
using Server; 
 

namespace Server.Items 
{ 
   	public class QuestCellarPart2Token : Item 
   	{ 
    
      		[Constructable] 
      		public QuestCellarPart2Token() : base( 0x14ED ) 
      		{ 
         		Weight = 1.0;  
         		Movable = true;
         		Name="You have completed part 2/7 of the home cellar quest [Quest Item]";   
      		} 

      		public QuestCellarPart2Token( Serial serial ) : base( serial ) 
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
