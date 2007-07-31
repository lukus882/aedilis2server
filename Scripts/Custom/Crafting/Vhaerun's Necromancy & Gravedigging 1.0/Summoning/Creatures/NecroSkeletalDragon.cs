using System;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a skeletal dragon corpse" )]
	public class NecroSkeletalDragon : BaseCreature
	{
		public override bool IsScaredOfScaryThings{ get{ return false; } }
		public override bool IsScaryToPets{ get{ return true; } }

		public override bool IsBondable{ get{ return false; } }

		[Constructable]
		public NecroSkeletalDragon() : this( false, 1.0 )
		{
		}

		[Constructable]
		public NecroSkeletalDragon( bool summoned, double scalar ) : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.4, 0.8 )
		{
			Name = "a skeletal dragon";
			Body = 104;
			BaseSoundID = 0x488;
			Hue = 0x2EF;

			SetStr( (int)(450*scalar), (int)(850*scalar) );
			SetDex( (int)(55*scalar), (int)(100*scalar) );
			SetInt( (int)(320*scalar), (int)(460*scalar) );

			SetHits( (int)(375*scalar), (int)(480*scalar) );

			SetDamage( (int)(18*scalar), (int)(26*scalar) );

			SetDamageType( ResistanceType.Physical, 80 );
			SetDamageType( ResistanceType.Fire, 15 );
			SetDamageType( ResistanceType.Energy, 5 );

			SetResistance( ResistanceType.Physical, (int)(50*scalar), (int)(65*scalar) );

			if ( summoned )
				SetResistance( ResistanceType.Fire, (int)(25*scalar), (int)(45*scalar) );
			else
				SetResistance( ResistanceType.Fire, (int)(25*scalar) );

			SetResistance( ResistanceType.Cold, (int)(35*scalar), (int)(40*scalar) );
			SetResistance( ResistanceType.Poison, (int)(35*scalar), (int)(40*scalar) );
			SetResistance( ResistanceType.Energy, (int)(35*scalar), (int)(40*scalar) );

			SetSkill( SkillName.MagicResist, (75.1*scalar), (90.0*scalar) );
			SetSkill( SkillName.Tactics, (55.1*scalar), (70.0*scalar) );
			SetSkill( SkillName.Wrestling, (45.1*scalar), (80.0*scalar) );
			SetSkill( SkillName.Anatomy, (25.1*scalar), (50.0*scalar) );
			SetSkill( SkillName.EvalInt, (40.1*scalar), (80.0*scalar) );
			SetSkill( SkillName.Magery, (70.1*scalar), (100.0*scalar) );

			if ( summoned )
			{
				Fame = 10;
				Karma = 10;
			}
			else
			{
				Fame = 20000;
				Karma = -20000;
			}

			VirtualArmor = 10;

			ControlSlots = 4;
		}

		public override bool DeleteOnRelease{ get{ return true; } }

		public override bool AutoDispel{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } } // fire breath enabled
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 50; } }
		public override int BreathEnergyDamage{ get{ return 50; } }
		public override int BreathEffectHue{ get{ return 0x480; } }

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

			if ( 0.2 > Utility.RandomDouble() )
				defender.Combatant = null;
		}

		public override void OnDamage( int amount, Mobile from, bool willKill )
		{
			int dam = ( (int)(amount * 0.8) + 1 );

			if ( Controlled || Summoned )
			{
				Mobile master = ( this.ControlMaster );

				if ( master == null )
					master = this.SummonMaster;

				if ( master != null && master.Player && master.Map == this.Map && master.InRange( Location, 20 ) )
				{
					if ( master.Mana >= dam )
					{
						master.Mana -= dam;
					}
					else
					{
						dam -= master.Mana;
						master.Mana = 0;
						master.Damage( dam );
					}
				}
			}

			base.OnDamage( amount, from, willKill );
		}

		public override bool BardImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		public NecroSkeletalDragon( Serial serial ) : base( serial )
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