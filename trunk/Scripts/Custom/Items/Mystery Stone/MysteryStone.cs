using System; 
using Server; 
using Server.Items;
using Server.Gumps; 
using Server.Network; 
using Server.Menus; 
using Server.Mobiles;
using Server.Menus.Questions;

namespace Server.Items
{ 
	public class MysteryStone : Item 
	{ 

		[Constructable] 
		public MysteryStone() : base( 3800 ) 
		{ 
			Movable = false;  
			Name = "a gravestone"; 
		} 

		public override void OnDoubleClick( Mobile from ) 
		{ 
			if ( from is PlayerMobile )
			{
				if ( from.InRange( GetWorldLocation(), 3 ) )
				{ 
            	      			from.CloseGump( typeof( MysteryStoneGump ) ); 
            	      			from.SendGump( new MysteryStoneGump() ); 
            	      			Effects.PlaySound( from.Location, from.Map, 0x5C7 );
				}
				else
				{
            	      			from.SendLocalizedMessage( 500446 ); // That is too far away.
				}
			}
     	 	} 

		public MysteryStone( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 

			writer.Write( (int) 0 ); // version 
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 

			int version = reader.ReadInt(); 
		} 
	} 
} 