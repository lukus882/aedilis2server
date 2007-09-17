using System; 
using Server; 
using Server.Gumps; 
using Server.Network; 
using Server.Menus; 
using Server.Menus.Questions; 
using Server.Mobiles; 
using System.Collections; 
using Server.Targeting; 

namespace Server.Items 
{ 
   	public class RunicInk : Item 
   	{ 
    
      		[Constructable] 
      		public RunicInk() : base( 0x2D61 ) 
      		{ 
         		Weight = 1.0;  
         		Movable = true;
         		Name="Runic Ink"; 
			Hue = 1153;  
      		} 

      		public RunicInk( Serial serial ) : base( serial ) 
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

          			this.SendLocalizedMessageTo(from, 1010086); 
           			from.Target = new AllSpellTarget(this);

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

		private class AllSpellTarget : Target 
      		{ 

			private Mobile m_Owner; 
      
         		private RunicInk m_Powder; 

         		public AllSpellTarget( RunicInk charge ) : base ( 10, false, TargetFlags.None ) 
         		{ 
            			m_Powder=charge; 
         		} 
          
         		protected override void OnTarget( Mobile from, object target ) 
         		{ 

          			if( target != null && target is Spellbook ) 
          			{ 

				          
          				Spellbook c = (Spellbook)target;

					if ( c.ItemID == 0xE3B )
					{
						c.Content = ulong.MaxValue;
						from.SendMessage( "You Invoke The Power Locked Inside The Ink and Add Every Known Magery Spell To Your Book" );
						m_Powder.Delete();
					}
					else
					{
						from.SendMessage( "That is not a Magery Spellbook" );
					}
	
  
            			}
				else
				{
					from.SendMessage( "That is not a Magery Spellbook" );
				}
         		} 
      		} 


   	} 
} 
