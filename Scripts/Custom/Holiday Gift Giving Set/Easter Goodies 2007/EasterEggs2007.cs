using System;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Guilds;
using Server.Gumps;
using Server.Mobiles; 
using Server.Targeting;

namespace Server.Items
{
	public class EasterEggs2007 : Item 
	{
		public override TimeSpan DecayTime{ get { return TimeSpan.FromDays( 2.0 ); } }

		[Constructable]
		public EasterEggs2007() : base( 0x9B5 )
		{
			Weight = 0.1;
			Hue = 3 + (Utility.Random( 20 ) * 5);			
			Name = "Easter Eggs 2007";
			LootType = LootType.Blessed;
		}
		
		public EasterEggs2007( Serial serial ) : base( serial )
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

		public override void OnDoubleClick( Mobile from ) // Override double click of the deed to call our target
		{
			if ( !IsChildOf( from.Backpack ) ) // Make sure its in their pack
			{
				 from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
			else
			{
		 		this.Delete(); 
				from.SendMessage( "An Easter Goodie has been placed in your backpack." );
				switch ( Utility.Random( 8 ) ) //Random choice of gift item 
                { 
			        case 0: from.AddToBackpack( new EasterBonnet2007() ); break;
			        case 1: from.AddToBackpack( new ChocolateEasterBunny2007() ); break;
			        case 2: from.AddToBackpack( new EasterCard2007() ); break;
			        case 3: from.AddToBackpack( new EasterCarrot2007() ); break;
			        case 4: from.AddToBackpack( new BagOfJellyBeans() ); break;
					case 5: from.AddToBackpack( new EasterLily2007() ); break;
					case 6: from.AddToBackpack( new BubbleGumEasterGrass2007() ); break;
					case 7: from.AddToBackpack( new MarshmallowPeep2007() ); break;
                } 
			}
		}	
	}
}