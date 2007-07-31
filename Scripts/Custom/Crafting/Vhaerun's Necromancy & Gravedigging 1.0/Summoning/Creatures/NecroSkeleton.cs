using System;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a skeletal corpse" )]
	public class NecroSkeleton : BaseCreature
	{
		public override bool IsScaredOfScaryThings{ get{ return false; } }
		public override bool IsScaryToPets{ get{ return true; } }

		public override bool IsBondable{ get{ return false; } }

		[Constructable]
		public NecroSkeleton() : this( false, 1.0 )
		{
		}

		[Constructable]
		public NecroSkeleton( bool summoned, double scalar ) : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.4, 0.8 )
		{
			Name = "a skeleton";
			Body = Utility.RandomList( 50, 56 );
			BaseSoundID = 0x48D;
			Hue = 0x2EF;

			SetStr( (int)(20*scalar), (int)(30*scalar) );
			SetDex( (int)(20*scalar), (int)(30*scalar) );
			SetInt( (int)(20*scalar), (int)(30*scalar) );

			SetHits( (int)(25*scalar), (int)(40*scalar) );

			SetDamage( (int)(3*scalar), (int)(6*scalar) );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, (int)(10*scalar), (int)(20*scalar) );

			if ( summoned )
				SetResistance( ResistanceType.Fire, (int)(10*scalar), (int)(15*scalar) );
			else
				SetResistance( ResistanceType.Fire, (int)(0*scalar) );

			SetResistance( ResistanceType.Cold, (int)(15*scalar), (int)(20*scalar) );
			SetResistance( ResistanceType.Poison, (int)(20*scalar), (int)(30*scalar) );

			SetSkill( SkillName.MagicResist, (25.1*scalar), (35.0*scalar) );
			SetSkill( SkillName.Tactics, (10.1*scalar), (15.0*scalar) );
			SetSkill( SkillName.Wrestling, (25.1*scalar), (40.0*scalar) );

			if ( summoned )
			{
				Fame = 10;
				Karma = 10;
			}
			else
			{
				Fame = 600;
				Karma = -600;
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

		public NecroSkeleton( Serial serial ) : base( serial )
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