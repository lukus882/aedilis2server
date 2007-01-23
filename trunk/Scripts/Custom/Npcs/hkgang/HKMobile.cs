using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Misc;
using Server.Mobiles;

namespace Server.Engines.HunterKiller
{
	public abstract class HKMobile : BaseCreature
	{
		public HKMobile( AIType aiType ) : this( aiType, FightMode.Closest )
		{
		}

		public HKMobile( AIType aiType, FightMode mode ) : base( aiType, mode, 18, 1, 0.1, 0.2 )
		{
			Title = "the murderer";

			Hue = Utility.RandomSkinHue();

			if ( this.Female = Utility.RandomBool() )
			{
				Body = 0x191;
				Name = NameList.RandomName( "female" );
			}
			else
			{
				Body = 0x190;
				Name = NameList.RandomName( "male" );
			}

            AddItem( new Shirt( Utility.RandomNeutralHue() )); 

			Item hair = new Item( Utility.RandomList( 0x203B, 0x2049, 0x2048, 0x204A ) );

			hair.Hue = Utility.RandomNondyedHue();
			hair.Layer = Layer.Hair;
			hair.Movable = false;

			AddItem( hair );

			if (Utility.RandomBool())
			{
				AddItem( new Spear() );
			}
			else
			{
				switch ( Utility.Random( 3 )) 
				{
				case 0: AddItem( new Longsword() ); break; 
				case 1: AddItem( new Broadsword() ); break; 
				case 2: AddItem( new VikingSword() ); break; 
				}

				switch ( Utility.Random( 8 )) 
				{ 
				case 0: AddItem( new BronzeShield() ); break; 
				case 1: AddItem( new HeaterShield() ); break; 
				case 2: AddItem( new MetalKiteShield() ); break; 
				case 3: AddItem( new MetalShield() ); break; 
				case 4: AddItem( new WoodenKiteShield() ); break; 
				case 5: AddItem( new WoodenShield() ); break; 
				case 6: AddItem( new OrderShield() ); break; 
				case 7: AddItem( new ChaosShield() ); break; 
				} 
			}

			AddItem( new CloseHelm() );
            AddItem( new PlateChest() );
            AddItem( new PlateLegs() );
            AddItem( new PlateArms() );
            AddItem( new LeatherGorget() );

            PackGold( 350, 550 ); 
		}

		public HKMobile( Serial serial ) : base( serial )
		{
		}

		public override bool AlwaysMurderer{ get{ return true; } }

		public override bool CanRummageCorpses{ get{ return false; } }

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