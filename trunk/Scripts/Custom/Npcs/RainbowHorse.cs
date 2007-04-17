using System;
using System.Collections;
using Server.Mobiles;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a horse corpse" )]
	[TypeAlias( "Server.Mobiles.RainbowHorse" )]
	public class RainbowHorse : BaseMount
	{
		private static int[] m_IDs = new int[]
			{
				0xC8, 0x3E9F,
				0xE2, 0x3EA0,
				0xE4, 0x3EA1,
				0xCC, 0x3EA2
			};

		[Constructable]
		public RainbowHorse() : this( "a Horse" )
		{
		}

		[Constructable]
		public RainbowHorse( string name ) : base( name, 0xE2, 0x3EA0, AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			int random = Utility.Random( 4 );

			Body = m_IDs[random * 2];
			ItemID = m_IDs[random * 2 + 1];
			BaseSoundID = 0xA8;

			SetStr( 22, 98 );
			SetDex( 56, 75 );
			SetInt( 6, 10 );

			SetHits( 28, 45 );
			SetMana( 0 );

			SetDamage( 3, 4 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 15, 20 );

			SetSkill( SkillName.MagicResist, 25.1, 30.0 );
			SetSkill( SkillName.Tactics, 29.3, 44.0 );
			SetSkill( SkillName.Wrestling, 29.3, 44.0 );

			Fame = 300;
			Karma = 300;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 29.1;
		}

		public override int Meat{ get{ return 3; } }
		public override int Hides{ get{ return 10; } }
		public override FoodType FavoriteFood{ get{ return FoodType.FruitsAndVegies | FoodType.GrainsAndHay; } }

		public RainbowHorse( Serial serial ) : base( serial )
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
		
		public override bool OnDragDrop( Mobile from, Item dropped )
		{          		
         	Mobile m = from;
			PlayerMobile mobile = m as PlayerMobile;

			if ( mobile != null)
			{
				if( dropped is Apple )
         			{
         				dropped.Delete(); 
         				this.Hue = 0x494;
						mobile.SendMessage ( "Your horse enjoys what you've given it" );
					}
				else if( dropped is Lime )
         			{
         				dropped.Delete(); 
         				this.Hue = 0x483;
						mobile.SendMessage ( "Your horse enjoys what you've given it" );
					}
				else if( dropped is Lemon )
         			{
         				dropped.Delete(); 
         				this.Hue = 0x35;
						mobile.SendMessage ( "Your horse enjoys what you've given it" );
					}
				else if( dropped is Grapes )
         			{
         				dropped.Delete(); 
         				this.Hue = 0x490;
						mobile.SendMessage ( "Your horse enjoys what you've given it" );
					}
				else if( dropped is Dates )
         			{
         				dropped.Delete(); 
         				this.Hue = 0x486;
						mobile.SendMessage ( "Your horse enjoys what you've given it" );
					}
				else if( dropped is Pear )
         			{
         				dropped.Delete(); 
         				this.Hue = 0x48F;
						mobile.SendMessage ( "Your horse enjoys what you've given it" );
					}
				else if( dropped is Squash )
         			{
         				dropped.Delete(); 
         				this.Hue = 0x491;
						mobile.SendMessage ( "Your horse enjoys what you've given it" );
					}
				else if( dropped is Cantaloupe )
         			{
         				dropped.Delete(); 
         				this.Hue = 0x499;
						mobile.SendMessage ( "Your horse enjoys what you've given it" );
					}
				else if( dropped is Peach )
         			{
         				dropped.Delete(); 
         				this.Hue = 0x2E;
						mobile.SendMessage ( "Your horse enjoys what you've given it" );
					}
				else if( dropped is Coconut || dropped is SplitCoconut )
         			{
         				dropped.Delete(); 
         				this.Hue = 0x47E;
						mobile.SendMessage ( "Your horse enjoys what you've given it" );
					}
				else if( dropped is Banana || dropped is Bananas )
         			{
         				dropped.Delete(); 
         				this.Hue = 0x38;
						mobile.SendMessage ( "Your horse enjoys what you've given it" );
					}
				else if( dropped is Carrot )
         			{
         				dropped.Delete(); 
         				this.Hue = 0x496;
						mobile.SendMessage ( "Your horse enjoys what you've given it" );
					}
				else if( dropped is Watermelon )
         			{
         				dropped.Delete(); 
         				this.Hue = 0x48E;
						mobile.SendMessage ( "Your horse enjoys what you've given it" );
					}
				else if( dropped is Gold )
         			{
         				dropped.Delete(); 
         				this.Hue = 0x0;
						mobile.SendMessage ( "Your horse enjoys what you've given it" );
					}


         	else
					mobile.SendMessage ( "Your horse doesn't want that item" );
			}
			return false;
		}
	}
}