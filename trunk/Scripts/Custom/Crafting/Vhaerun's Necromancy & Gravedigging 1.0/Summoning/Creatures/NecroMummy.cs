using System;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a mummy corpse" )]
	public class NecroMummy : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.CrushingBlow;
		}

		public override bool IsScaredOfScaryThings{ get{ return false; } }
		public override bool IsScaryToPets{ get{ return true; } }

		public override bool IsBondable{ get{ return false; } }

		[Constructable]
		public NecroMummy() : this( false, 1.0 )
		{
		}

		[Constructable]
		public NecroMummy( bool summoned, double scalar ) : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.4, 0.8 )
		{
			Name = "a mummy";
			Body = 154;
			BaseSoundID = 471;
			Hue = 0x2EF;

			SetStr( (int)(150*scalar), (int)(250*scalar) );
			SetDex( (int)(45*scalar), (int)(70*scalar) );
			SetInt( (int)(65*scalar), (int)(100*scalar) );

			SetHits( (int)(120*scalar), (int)(190*scalar) );

			SetDamage( (int)(10*scalar), (int)(19*scalar) );

			SetDamageType( ResistanceType.Physical, 60 );
			SetDamageType( ResistanceType.Poison, 40 );

			SetResistance( ResistanceType.Physical, (int)(40*scalar), (int)(55*scalar) );

			if ( summoned )
				SetResistance( ResistanceType.Fire, (int)(0*scalar), (int)(5*scalar) );
			else
				SetResistance( ResistanceType.Fire, (int)(0*scalar) );

			SetResistance( ResistanceType.Cold, (int)(20*scalar), (int)(40*scalar) );
			SetResistance( ResistanceType.Poison, (int)(35*scalar), (int)(45*scalar) );

			SetSkill( SkillName.MagicResist, (55.1*scalar), (70.0*scalar) );
			SetSkill( SkillName.Tactics, (55.1*scalar), (90.0*scalar) );
			SetSkill( SkillName.Wrestling, (55.1*scalar), (90.0*scalar) );
			SetSkill( SkillName.Anatomy, (25.1*scalar), (45.0*scalar) );

			if ( summoned )
			{
				Fame = 10;
				Karma = 10;
			}
			else
			{
				Fame = 18000;
				Karma = -18000;
			}

			VirtualArmor = 10;

			ControlSlots = 3;
		}

		public override bool DeleteOnRelease{ get{ return true; } }

		public override bool AutoDispel{ get{ return !Controlled; } }

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

			if ( 0.2 > Utility.RandomDouble() )
				defender.Combatant = null;
		}

		public override void OnDamage( int amount, Mobile from, bool willKill )
		{
			int dam = ( (int)(amount * 0.6) + 1 );

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
		public override Poison HitPoison{ get{ return Poison.Lethal; } }

		public NecroMummy( Serial serial ) : base( serial )
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