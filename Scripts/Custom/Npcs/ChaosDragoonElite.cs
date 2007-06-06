//Created By -Cybella-

using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a chaos dragoon elite corpse" )]
	public class ChaosDragoonElite : BaseCreature
	{
		[Constructable]
		public ChaosDragoonElite() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a chaos dragoon elite";
			Body = 185;

			SetStr( 276, 350 );
			SetDex( 66, 90 );
			SetInt( 126, 150 );

			SetDamage( 29, 34 );

			SetDamageType( ResistanceType.Physical, 55 );
			SetDamageType( ResistanceType.Fire, 25 );
			SetDamageType( ResistanceType.Cold, 50 );
			SetDamageType( ResistanceType.Poison, 35 );
			SetDamageType( ResistanceType.Energy, 35 );

			SetSkill( SkillName.Tactics, 80.1, 100.0 );
			SetSkill( SkillName.MagicResist, 100.1, 110.0 );
			SetSkill( SkillName.Anatomy, 80.1, 100.0 );
			SetSkill( SkillName.Magery, 85.1, 100.0 );
			SetSkill( SkillName.EvalInt, 85.1, 100.0 );
			SetSkill( SkillName.MagicResist, 72.5, 95.0 );
			SetSkill( SkillName.Swords, 72.5, 95.0 );
			SetSkill( SkillName.Tactics, 72.5, 95.0 );

			Fame = 8000;
			Karma = -8000;

			switch ( Utility.Random( 6 ) )
			{
				case 0: PackItem( new RedScales( Utility.RandomMinMax( 1, 3 ) ) ); break;
				case 1: PackItem( new YellowScales( Utility.RandomMinMax( 1, 3 ) ) ); break;
				case 2: PackItem( new BlackScales( Utility.RandomMinMax( 1, 3 ) ) ); break;
				case 3: PackItem( new GreenScales( Utility.RandomMinMax( 1, 3 ) ) ); break;
				case 4: PackItem( new WhiteScales( Utility.RandomMinMax( 1, 3 ) ) ); break;
				case 5: PackItem( new BlueScales( Utility.RandomMinMax( 1, 3 ) ) ); break;
			}

			DragonChest Tunic = new DragonChest();
			Tunic.Quality = ArmorQuality.Exceptional;
			Tunic.Movable = false;
			AddItem( Tunic );

			DragonLegs Legs = new DragonLegs();
			Legs.Quality = ArmorQuality.Exceptional;
			Legs.Movable = false;
			AddItem( Legs );

			DragonArms Arms = new DragonArms();
			Arms.Quality = ArmorQuality.Exceptional;
			Arms.Movable = false;
			AddItem( Arms );

			DragonGloves Gloves = new DragonGloves();
			Gloves.Quality = ArmorQuality.Exceptional;
			Gloves.Movable = false;
			AddItem( Gloves );

			DragonHelm Helm = new DragonHelm();
			Helm.Quality = ArmorQuality.Exceptional;
			Helm.Movable = false;
			AddItem( Helm );

			EquipItem( Loot.RandomWeapon() );
			AddItem( new Boots( 0x455 ) );
			AddItem( new Shirt( Utility.RandomMetalHue() ) );
			EquipItem( new ChaosShield() );

			new ScaledSwampDragon().Rider = this;
		}

		public override int GetIdleSound()
		{
			return 0x2CE;
		}

		public override int GetDeathSound()
		{
			return 0x2CC;
		}

		public override int GetHurtSound()
		{
			return 0x2D1;
		}

		public override int GetAttackSound()
		{
			return 0x2C8;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Gems );	
		}

		public override bool HasBreath{ get{ return true; } }
		public override bool AutoDispel{ get{ return true; } }
		public override bool BardImmune{ get{ return !Core.AOS; } }
		public override bool CanRummageCorpses{ get{ return true; } }
		public override bool AlwaysMurderer{ get{ return true; } }
		public override bool ShowFameTitle{ get{ return false; } }

		public override bool OnBeforeDeath()
		{
			IMount mount = this.Mount;

			if ( mount != null )
				mount.Rider = null;

			if ( mount is Mobile )
				((Mobile)mount).Delete();

			return base.OnBeforeDeath();
		}

		public override void AlterMeleeDamageTo( Mobile to, ref int damage )
		{
			if ( to is Dragon || to is WhiteWyrm || to is SwampDragon || to is Drake || to is Nightmare || to is Hiryu || to is LesserHiryu || to is Daemon )
				damage *= 3;
		}

		public ChaosDragoonElite( Serial serial ) : base( serial )
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