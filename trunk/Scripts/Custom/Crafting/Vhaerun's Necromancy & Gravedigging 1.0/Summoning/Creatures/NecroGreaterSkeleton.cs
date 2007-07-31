using System;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a skeletal corpse" )]
	public class NecroGreaterSkeleton : BaseCreature
	{
		public override bool IsScaredOfScaryThings{ get{ return false; } }
		public override bool IsScaryToPets{ get{ return true; } }

		public override bool IsBondable{ get{ return false; } }

		[Constructable]
		public NecroGreaterSkeleton() : this( false, 1.0 )
		{
		}

		[Constructable]
		public NecroGreaterSkeleton( bool summoned, double scalar ) : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.4, 0.8 )
		{
			Name = "a greater skeleton";
			Body = 309;
			BaseSoundID = 0x48D;
			Hue = 0x2EF;

			SetStr( (int)(60*scalar), (int)(80*scalar) );
			SetDex( (int)(30*scalar), (int)(55*scalar) );
			SetInt( (int)(30*scalar), (int)(55*scalar) );

			SetHits( (int)(75*scalar), (int)(120*scalar) );

			SetDamage( (int)(6*scalar), (int)(12*scalar) );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, (int)(25*scalar), (int)(35*scalar) );

			if ( summoned )
				SetResistance( ResistanceType.Fire, (int)(0*scalar), (int)(5*scalar) );
			else
				SetResistance( ResistanceType.Fire, (int)(0*scalar) );

			SetResistance( ResistanceType.Cold, (int)(25*scalar), (int)(30*scalar) );
			SetResistance( ResistanceType.Poison, (int)(20*scalar), (int)(30*scalar) );

			SetSkill( SkillName.MagicResist, (45.1*scalar), (65.0*scalar) );
			SetSkill( SkillName.Tactics, (30.1*scalar), (55.0*scalar) );
			SetSkill( SkillName.Wrestling, (55.1*scalar), (70.0*scalar) );

			if ( summoned )
			{
				Fame = 10;
				Karma = 10;
			}
			else
			{
				Fame = 4000;
				Karma = -4000;
			}

			VirtualArmor = 10;

			ControlSlots = 2;
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
			int dam = ( (int)(amount * 0.4) + 1 );

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

		public NecroGreaterSkeleton( Serial serial ) : base( serial )
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