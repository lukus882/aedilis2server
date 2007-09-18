using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a fire drake corpse" )]
	public class FireDrake : BaseCreature
	{
		[Constructable]
		public FireDrake () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
            		int i_Resource = 0;
            		i_Resource = Utility.RandomMinMax(1, 25);

			Body = 49;
			Body = Utility.RandomList( 60, 61 );
			BaseSoundID = 362;
            		Hue = 0x489;
                        Name = "Fire Drake";

			SetStr( 401, 430 );
			SetDex( 133, 152 );
			SetInt( 101, 140 );

			SetHits( 241, 258 );

			SetDamage( 11, 17 );

			SetDamageType( ResistanceType.Physical, 20 );
			SetDamageType( ResistanceType.Fire, 80 );

			SetResistance( ResistanceType.Physical, 45, 50 );
			SetResistance( ResistanceType.Fire, 80, 90 );
			SetResistance( ResistanceType.Cold, 5, 5 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.MagicResist, 65.1, 80.0 );
			SetSkill( SkillName.Tactics, 65.1, 90.0 );
			SetSkill( SkillName.Wrestling, 65.1, 80.0 );



			Fame = 8000;
			Karma = -800;

			VirtualArmor = 64;

			Tamable = false;
			ControlSlots = 3;
			MinTameSkill = 96.3;

			AddLoot( LootPack.Gems, Utility.Random( 1, 5 ) );

            		if (i_Resource > 24) PackItem(new fdeggs());
		}

		public override int TreasureMapLevel{ get{ return 4; } }
		public override int Meat{ get{ return 19; } }
		public override int Hides{ get{ return 20; } }
		public override HideType HideType{ get{ return HideType.Barbed; } }
		public override int Scales{ get{ return 9; } }
		public override ScaleType ScaleType{ get{ return ScaleType.White; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat | FoodType.Gold; } }

		public FireDrake( Serial serial ) : base( serial )
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