using System;
using Server;
using System.Collections; 
using Server.ContextMenus;
using System.Collections.Generic;
using Server.Misc;
using Server.Network;
using Server.Items; 
using Server.Mobiles;
using Server.Gumps;

namespace Server.Mobiles 
{ 
	[CorpseName( "a cook corpse" )]
	public class BulkCook : BaseCreature 
	{ 
		[Constructable] 
		public BulkCook() : base( AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4 ) 
		{  
			Title = "the Chef"; 

			SpeechHue = Utility.RandomDyedHue(); 

			Hue = Utility.RandomSkinHue(); 

			if ( Female = Utility.RandomBool() ) 
			{ 
				Body = 0x191; 
				Name = NameList.RandomName( "female" ); 
			} 
			else 
			{ 
				Body = 0x190; 
				Name = NameList.RandomName( "male" ); 
			} 

			SetStr( 86, 100 );
			SetDex( 81, 95 );
			SetInt( 61, 75 );

			SetDamage( 10, 23 );

			SetSkill( SkillName.Fencing, 66.0, 97.5 );
			SetSkill( SkillName.Macing, 65.0, 87.5 );
			SetSkill( SkillName.MagicResist, 25.0, 47.5 );
			SetSkill( SkillName.Swords, 65.0, 87.5 );
			SetSkill( SkillName.Tactics, 65.0, 87.5 );
			SetSkill( SkillName.Wrestling, 15.0, 37.5 );

			AddItem( new FullApron() );
			AddItem( new LongPants( Utility.RandomRedHue() ) );
			AddItem( new Cap() );
			AddItem( new FancyShirt( Utility.RandomGreenHue() ) );
			AddItem( new Boots() );

		}

		public BulkCook( Serial serial ) : base( serial ) 
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

            	public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{ 
			base.GetContextMenuEntries( from, list );
			list.Add( new BulkFoodEntry( from, this ) ); 
		}

		public class BulkFoodEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public BulkFoodEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
			{
				m_Mobile = from;
				m_Giver = giver;
			}

			public override void OnClick()
			{
				

                          if( !( m_Mobile is PlayerMobile ) )
					return;
				
				PlayerMobile mobile = (PlayerMobile) m_Mobile;

				{
					if ( ! mobile.HasGump( typeof( BulkFoodGump ) ) )
					{
						mobile.SendGump( new BulkFoodGump( mobile ));
						
					} 
				}
			}
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{          		
         		Mobile m = from;
			PlayerMobile mobile = m as PlayerMobile;

			if ( mobile != null)
			{
				if( dropped is CookedBird || dropped is LambLeg || dropped is Ribs || dropped is FishSteak )
         			{
					if( dropped.Amount == 100 )
					{
						dropped.Delete(); 
						SayTo( from, "Ah, my thanks. Here is something for your trouble." );
						mobile.AddToBackpack( new Gold( Utility.RandomMinMax( 300, 500 ) ) );

						if( Utility.RandomDouble() < .25 )
						{
							switch ( Utility.Random( 8 ) )
							{
								case 0: mobile.AddToBackpack( new PegBoardEast() ); break;
								case 1: mobile.AddToBackpack( new PegBoardSouth() ); break;
								case 2: mobile.AddToBackpack( new SturdyBakersBoard() ); break;
								case 3: mobile.AddToBackpack( new SturdyFryingPan() ); break;
								case 4: mobile.AddToBackpack( new SturdyCooksCauldron() ); break;
								case 5: mobile.AddToBackpack( new CooksCap() ); break;
								case 6: mobile.AddToBackpack( new CooksApron() ); break;
								case 7: mobile.AddToBackpack( new WaterTubDeed() ); break;
							}

							from.PlaySound( 0x418 );
						}

						return true;
					}
					else if( dropped.Amount == 200 )
					{
						dropped.Delete(); 
						SayTo( from, "Ah, my thanks. Here is something for your trouble." );
						mobile.AddToBackpack( new Gold( Utility.RandomMinMax( 500, 750 ) ) );

						if( Utility.RandomDouble() < .05 )
						{
							switch ( Utility.Random( 12 ) )
							{
								case 0: mobile.AddToBackpack( new StoneOvenEastDeed() ); break;
								case 1: mobile.AddToBackpack( new StoneOvenSouthDeed() ); break;
								case 2: mobile.AddToBackpack( new ElvenStoveEastDeed() ); break;
								case 3: mobile.AddToBackpack( new ElvenStoveSouthDeed() ); break;
								case 4: mobile.AddToBackpack( new DecoCampfire() ); break;
								case 5: mobile.AddToBackpack( new StoneFirePit() ); break;
								case 6: mobile.AddToBackpack( new WaterBarrelDeed() ); break;
								case 7: mobile.AddToBackpack( new MeatBox() ); break;
								case 8: mobile.AddToBackpack( new FoodPantry() ); break;
								case 9: mobile.AddToBackpack( new HerbJars() ); break;
								case 10: mobile.AddToBackpack( new ChefsCap() ); break;
								case 11: mobile.AddToBackpack( new ChefsApron() ); break;
							}

							from.PlaySound( 0x418 );
						}

						return true;
					}
					else
					{
						SayTo( from, "I need either 100 or 200. No more, no less." );
					}
         			}
         			else
         			{
					SayTo( from, "I don't need this. I need cooked meat." );
     				}
			}

			return false;
		
		}
	}
}