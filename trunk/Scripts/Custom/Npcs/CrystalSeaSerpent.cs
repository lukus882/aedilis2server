using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a crystal sea serpent's corpse" )]
	public class CrystalSeaSerpent : BaseCreature
	{
		[Constructable]
		public CrystalSeaSerpent() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a crystal sea serpent";
			Body = 150;
			BaseSoundID = 447;
			Hue = 1151;

			SetStr( 259, 411 );
			SetDex( 103, 141 );
			SetInt( 96, 151 );

			SetHits( 231, 323 );

			SetDamage( 7, 13 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 51, 64 );
			SetResistance( ResistanceType.Cold, 72, 90 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 65, 80 );

			SetSkill( SkillName.MagicResist, 60.1, 75.0 );
			SetSkill( SkillName.Tactics, 60.8, 70.0 );
			SetSkill( SkillName.Wrestling, 60.6, 70.0 );

			Fame = 6000;
			Karma = -6000;

			VirtualArmor = 30;
			CanSwim = true;
			CantWalk = true;

			if ( Utility.RandomBool() )
				PackItem( new SulfurousAsh( 4 ) );
			else
				PackItem( new BlackPearl( 4 ) );

			PackItem( new RawFishSteak() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}

		public override bool HasBreath{ get{ return true; } }
		public override int BreathEffectSpeed{ get{ return 1; } }
		public override int BreathEffectHue{ get{ return 193; } }
		public override int BreathEffectSound{ get{ return 0x1CC; } }
		public virtual int BreathPhysicalDamage{ get{ return 0; } }
		public virtual int BreathFireDamage{ get{ return 0; } }
		public virtual int BreathColdDamage{ get{ return 100; } }
		public virtual int BreathPoisonDamage{ get{ return 0; } }
		public virtual int BreathEnergyDamage{ get{ return 0; } }

		public override int TreasureMapLevel{ get{ return 2; } }

		public override int Hides{ get{ return 10; } }
		public override HideType HideType{ get{ return HideType.Horned; } }
		public override int Scales{ get{ return 8; } }
		public override ScaleType ScaleType{ get{ return ScaleType.Blue; } }

		public CrystalSeaSerpent( Serial serial ) : base( serial )
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