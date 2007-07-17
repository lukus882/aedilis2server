using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a krakens corpse" )]
	public class FishedKraken : BaseCreature
	{
		[Constructable]
		public FishedKraken() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a kraken";
			Body = 77;
			BaseSoundID = 353;

			SetStr( 756, 780 );
			SetDex( 226, 245 );
			SetInt( 26, 40 );

			SetHits( 454, 468 );
			SetHits( 756, 780 );
			SetMana( 0 );

			SetDamage( 19, 33 );

			SetDamageType( ResistanceType.Physical, 70 );
			SetDamageType( ResistanceType.Cold, 30 );

			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			SetSkill( SkillName.MagicResist, 15.1, 20.0 );
			SetSkill( SkillName.Tactics, 45.1, 60.0 );
			SetSkill( SkillName.Wrestling, 45.1, 60.0 );

			Fame = 11000;
			Karma = -11000;

			VirtualArmor = 50;

			Tamable = true;
			ControlSlots = 2;
			MinTameSkill = 79.8;

			CanSwim = true;
			CantWalk = true;

			PackItem( new CoilRope() );
			
			if( Utility.RandomDouble() < .05 )
				PackItem( new MessageInABottle() );
				
			PackItem( new SpecialFishingNet() ); //Confirm?
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
		}

		public override int TreasureMapLevel{ get{ return 4; } }

		public override bool HasBreath{ get{ return true; } }
		public override int BreathEffectSpeed{ get{ return 1; } }
		public override int BreathEffectHue{ get{ return 193; } }
		public override int BreathEffectSound{ get{ return 0x1CC; } }
		public virtual int BreathPhysicalDamage{ get{ return 0; } }
		public virtual int BreathFireDamage{ get{ return 0; } }
		public virtual int BreathColdDamage{ get{ return 100; } }
		public virtual int BreathPoisonDamage{ get{ return 0; } }
		public virtual int BreathEnergyDamage{ get{ return 0; } }

		public FishedKraken( Serial serial ) : base( serial )
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
