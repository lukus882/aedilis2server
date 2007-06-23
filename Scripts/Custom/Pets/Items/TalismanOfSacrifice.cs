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
   	public class TalismanOfSacrifice : Item 
   	{ 
    
      		[Constructable] 
      		public TalismanOfSacrifice() : base( 0x2f59 ) 
      		{ 
         		Weight = 1.0;  
         		Movable = true;
         		Name="Talisman of Sacrifice";   
      		} 

      		public TalismanOfSacrifice( Serial serial ) : base( serial ) 
      		{ 
      		} 
      		public override void OnDoubleClick( Mobile from ) 
      		{ 

			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
			else if( from.InRange( this.GetWorldLocation(), 1 ) ) 
			{

        			
				else if ( from.Skills[SkillName.AnimalTaming].Value >= 100 )
				{
           				this.SendLocalizedMessageTo(from, 1010086); 
           				from.Target = new TSacrificeTarget( this );
					}
				else
				{
					from.SendMessage( "You must have 100 animal taming to use this talisman." );
				}



			} 
			else 
			{ 
				from.SendLocalizedMessage( 500446 ); // That is too far away. 
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
      		} 


  		private class TSacrificeTarget : Target 
      		{ 
         		private Mobile m_Owner; 
      
         		private TalismanOfSacrifice m_Powder; 

         		public TSacrificeTarget( TalismanOfSacrifice charge ) : base ( 10, false, TargetFlags.None ) 
         		{ 
            			m_Powder=charge; 
         		} 
          
         		protected override void OnTarget( Mobile from, object target ) 
         		{ 

            			if( target == from ) 
				{
               				from.SendMessage( "You cant do that." );
				}
          			else if( target is BaseCreature ) 
          			{ 
            
          				BaseCreature c = (BaseCreature)target;
					if ( c.Controlled == false )
					{
						from.SendMessage( "That Creature is not tamed." );
					}	
					else if ( c.ControlMaster != from )
					{
						from.SendMessage( "This is not your pet." );
					}
					else if ( c.Controlled == true && c.ControlMaster == from && c.IsBonded == true && c.Alive == true)
					{
						c.IsBonded = false;
						c.Kill();						
						from.PlaySound( 503 );
						from.SendMessage( "You have sacrificed your pet" );
						
						from.AddToBackpack( new PetBondingDeed() );
						from.SendMessage( "A new pet bonding deed has been placed in to your pack" );
            					m_Powder.Delete(); 
					}	
  
            			}
				else
				{
					from.SendMessage( "You cant do that." );
				}
         		} 
      		} 
   	} 
} 
