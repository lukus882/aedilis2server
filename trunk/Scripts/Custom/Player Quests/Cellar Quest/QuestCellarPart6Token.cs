using System; 
using Server; 
 

namespace Server.Items 
{ 
   	public class QuestCellarPart6Token : Item 
   	{ 
    
      		[Constructable] 
      		public QuestCellarPart6Token() : base( 0x14ED ) 
      		{ 
         		Weight = 1.0;  
         		Movable = true;
         		Name="You have completed part 6/7 of the home cellar quest [Quest Item]";   
      		} 

      		public QuestCellarPart6Token( Serial serial ) : base( serial ) 
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
