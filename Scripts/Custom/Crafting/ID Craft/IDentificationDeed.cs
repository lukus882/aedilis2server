using Server.Targeting; 
using System; 
using Server; 
using Server.Gumps; 
using Server.Network; 
using Server.Menus; 
using Server.Menus.Questions; 
using Server.Mobiles; 
using System.Collections; 

namespace Server.Items 
{ 
   	public class IdentificationDeed : Item 
   	{ 
    
      		[Constructable] 
      		public IdentificationDeed() : base( 0x14F0 ) 
      		{ 
         		Weight = 1.0;  
         		Movable = true;
         		Name = "An Identification Deed";
			Hue = 1103;   
      		} 

      		public IdentificationDeed( Serial serial ) : base( serial ) 
      		{ 
      		} 
      		public override void OnDoubleClick( Mobile from ) 
      		{ 

			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
			else
			{

        			from.SendMessage( "Which Item Would You Like To Identify?" ); 
           			from.Target = new IDTarget( this );

			} 

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
			Name = "An Identification Deed"; 
      		} 


  		private class IDTarget : Target 
      		{ 

			private IdentificationDeed m_Powder; 

         		public IDTarget( IdentificationDeed charge ) : base ( 10, false, TargetFlags.None ) 
         		{ 
            			m_Powder=charge; 
         		} 
         		
          
         		protected override void OnTarget( Mobile from, object target ) 
         		{ 


          			if( target is BaseJewel ) 
          			{ 
            
					((BaseJewel)target).Identified = true;
					 m_Powder.Delete();
					  
            			}
          			else if( target is BaseWeapon ) 
          			{ 
            	
					((BaseWeapon)target).Identified = true;
					 m_Powder.Delete();	
  
            			}
          			else if( target is BaseArmor ) 
          			{ 
            
					((BaseArmor)target).Identified = true;
					 m_Powder.Delete();
              			}
				else
				{
					from.SendMessage( "That Item Can Not Be Identified" );
				}
         		} 
      		} 
   	} 
} 
