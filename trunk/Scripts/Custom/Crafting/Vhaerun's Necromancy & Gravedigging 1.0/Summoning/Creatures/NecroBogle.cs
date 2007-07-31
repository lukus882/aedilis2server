using System;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a bogle corpse" )]
	public class NecroBogle : BaseCreature
	{
		public override bool IsScaredOfScaryThings{ get{ return false; } }
		public override bool IsScaryToPets{ get{ return true; } }

		public override bool IsBondable{ get{ return false; } }

		[Constructable]
		public NecroBogle() : this( false, 1.0 )
		{
		}

		[Constructable]
		public NecroBogle( bool summoned, double scalar ) : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.4, 0.8 )
		{
			Name = "a bogle";
			Body = 153;
			BaseSoundID = 0x482;
			Hue = 0x2EF;

			SetStr( (int)(40*scalar), (int)(70*scalar) );
			SetDex( (int)(30*scalar), (int)(70*scalar) );
			SetInt( (int)(40*scalar), (int)(70*scalar) );

			SetHits( (int)(40*scalar), (int)(60*scalar) );

			SetDamage( (int)(5*scalar), (int)(8*scalar) );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, (int)(10*scalar), (int)(20*scalar) );

			if ( summoned )
				SetResistance( ResistanceType.Fire, (int)(10*scalar), (int)(15*scalar) );
			else
				SetResistance( ResistanceType.Fire, (int)(0*scalar) );

			SetResistance( ResistanceType.Cold, (int)(15*scalar), (int)(20*scalar) );
			SetResistance( ResistanceType.Poison, (int)(20*scalar), (int)(30*scalar) );

			SetSkill( SkillName.EvalInt, (35.1*scalar), (55.0*scalar ) );
			SetSkill( SkillName.MagicResist, (35.1*scalar), (55.0*scalar) );
			SetSkill( SkillName.Magery, (35.1*scalar), (55.0*scalar) );
			SetSkill( SkillName.Tactics, (35.1*scalar), (45.0*scalar) );
			SetSkill( SkillName.Wrestling, (35.1*scalar), (45.0*scalar) );

			if ( summoned )
			{
				Fame = 10;
				Karma = 10;
			}
			else
			{
				Fame = 2000;
				Karma = -2000;
			}

			VirtualArmor = 10;

			ControlSlots = 1;
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
			int dam = ( (int)(amount * 0.2) + 1 );

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

		public NecroBogle( Serial serial ) : base( serial )
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