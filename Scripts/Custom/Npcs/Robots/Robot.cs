using System;
using Server.Items;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a broken machine" )]
	public class Robot : BaseCreature
	{
		private bool m_Stunning;
        private bool m_FieldActive;
        public bool FieldActive { get { return m_FieldActive; } }
        public bool CanUseField { get { return Hits >= HitsMax * 9 / 10; } } // TODO: an OSI bug prevents to verify this

		public override bool IsScaredOfScaryThings{ get{ return false; } }
		public override bool IsScaryToPets{ get{ return true; } }

		public override bool IsBondable{ get{ return true; } }

		[Constructable]
		public Robot() : this( false, 1.0 )
		{
		}

		[Constructable]
		public Robot( bool summoned, double scalar ) : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.4, 0.8 )
		{
			Name = "a robot";
			Body = 0x2F5;

			if ( summoned )
				Hue = 1109;

			SetStr( (int)(500*scalar), (int)(600*scalar) );
			SetDex( (int)(200*scalar), (int)(300*scalar) );
			SetInt( (int)(250*scalar), (int)(350*scalar) );

			SetHits( (int)(600*scalar), (int)(800*scalar) );

			SetDamage( (int)(20*scalar), (int)(30*scalar) );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, (int)(70*scalar), (int)(100*scalar) );

			if ( summoned )
				SetResistance( ResistanceType.Fire, (int)(40*scalar), (int)(60*scalar) );
			else
				SetResistance( ResistanceType.Fire, (int)(100*scalar) );

			SetResistance( ResistanceType.Cold, (int)(50*scalar), (int)(70*scalar) );
			SetResistance( ResistanceType.Poison, (int)(50*scalar), (int)(70*scalar) );
			SetResistance( ResistanceType.Energy, (int)(10*scalar), (int)(20*scalar) );

			SetSkill( SkillName.MagicResist, (150.1*scalar), (190.0*scalar) );
			SetSkill( SkillName.Tactics, (60.1*scalar), (100.0*scalar) );
			SetSkill( SkillName.Wrestling, (60.1*scalar), (100.0*scalar) );

			if ( summoned )
			{
				Fame = 10;
				Karma = 10;
			}
			else
			{
				Fame = 3500;
				Karma = -3500;
			}

			if ( !summoned )
			{
				PackItem( new IronIngot( Utility.RandomMinMax( 13, 21 ) ) );

				if ( 0.5 > Utility.RandomDouble() )
					PackItem( new PowerCrystal() );

				if ( 0.1 > Utility.RandomDouble() )
                    PackItem(new AdvancedRobotInstructions());

				if ( 0.10 > Utility.RandomDouble() )
					PackItem( new ArcaneGem() );

				if ( 0.30 > Utility.RandomDouble() )
					PackItem( new Gears() );
			}
            m_FieldActive = CanUseField;
			ControlSlots = 5;
		}

		public override bool DeleteOnRelease{ get{ return true; } }

		public override int GetAngerSound()
		{
			return 0x26C;
		}

		public override int GetIdleSound()
		{
			if ( !Controlled )
				return 0x218;

			return base.GetIdleSound();
		}

		public override int GetDeathSound()
		{
			if ( !Controlled )
				return 0x211;

			return base.GetDeathSound();
		}

		public override int GetAttackSound()
		{
			return 0x232;
		}

		public override int GetHurtSound()
		{
			if ( Controlled )
				return 0x140;

			return base.GetHurtSound();
		}
        public override void AlterMeleeDamageFrom( Mobile from, ref int damage )
		{
			if ( m_FieldActive )
				damage = 0; // no melee damage when the field is up
		}

		public override void AlterSpellDamageFrom( Mobile from, ref int damage )
		{
			if ( !m_FieldActive )
				damage = 0; // no spell damage when the field is down
		}

		public override void OnDamagedBySpell( Mobile from )
		{
			if( from != null && from.Alive && 0.4 > Utility.RandomDouble() )
			{
				SendEBolt( from );
			}

			if ( !m_FieldActive )
			{
				// should there be an effect when spells nullifying is on?
				this.FixedParticles( 0, 10, 0, 0x2522, EffectLayer.Waist );
			}
			else if ( m_FieldActive && !CanUseField )
			{
				m_FieldActive = false;

				// TODO: message and effect when field turns down; cannot be verified on OSI due to a bug
				this.FixedParticles( 0x3735, 1, 30, 0x251F, EffectLayer.Waist );
			}
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( m_FieldActive )
			{
				this.FixedParticles( 0x376A, 20, 10, 0x2530, EffectLayer.Waist );

				PlaySound( 0x2F4 );

				attacker.SendAsciiMessage( "Your weapon cannot penetrate the creature's magical barrier" );
			}

			if( attacker != null && attacker.Alive && attacker.Weapon is BaseRanged && 0.4 > Utility.RandomDouble() )
			{
				SendEBolt( attacker );
			}
		}

		public override void OnThink()
		{
			base.OnThink();

			// TODO: an OSI bug prevents to verify if the field can regenerate or not
			if ( !m_FieldActive && !IsHurt() )
				m_FieldActive = true;
		}

		public override bool Move( Direction d )
		{
			bool move = base.Move( d );

			if ( move && m_FieldActive && this.Combatant != null )
				this.FixedParticles( 0, 10, 0, 0x2530, EffectLayer.Waist );

			return move;
		}

        public void SendEBolt(Mobile to)
        {
            this.MovingParticles(to, 0x379F, 7, 0, false, true, 0xBE3, 0xFCB, 0x211);
            to.PlaySound(0x229);
            this.DoHarmful(to);
            AOS.Damage(to, this, 50, 0, 0, 0, 0, 100);
        }

		public override bool AutoDispel{ get{ return !Controlled; } }
		public override bool BleedImmune{ get{ return true; } }

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

			if ( !m_Stunning && 0.5 > Utility.RandomDouble() )
			{
				m_Stunning = true;

				defender.Animate( 21, 6, 1, true, false, 0 );
				this.PlaySound( 0xEE );
				defender.LocalOverheadMessage( MessageType.Regular, 0x3B2, false, "You have been stunned by a colossal blow!" );

				BaseWeapon weapon = this.Weapon as BaseWeapon;
				if ( weapon != null )
					weapon.OnHit( this, defender );

				if ( defender.Alive )
				{
					defender.Frozen = true;
					Timer.DelayCall( TimeSpan.FromSeconds( 10.0 ), new TimerStateCallback( Recover_Callback ), defender );
				}
			}
		}

		private void Recover_Callback( object state )
		{
			Mobile defender = state as Mobile;

			if ( defender != null )
			{
				defender.Frozen = false;
				defender.Combatant = null;
				defender.LocalOverheadMessage( MessageType.Regular, 0x3B2, false, "You recover your senses." );
			}

			m_Stunning = false;
		}

		public override void OnDamage( int amount, Mobile from, bool willKill )
		{
			if ( Controlled || Summoned )
			{
				Mobile master = ( this.ControlMaster );

				if ( master == null )
					master = this.SummonMaster;

				if ( master != null && master.Player && master.Map == this.Map && master.InRange( Location, 20 ) )
				{
					if ( master.Mana >= amount )
					{
						master.Mana -= amount;
					}
					else
					{
						amount -= master.Mana;
						master.Mana = 0;
						master.Damage( amount );
					}
				}
			}

			base.OnDamage( amount, from, willKill );
		}

		public override bool BardImmune{ get{ return !Core.AOS || Controlled; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		public Robot( Serial serial ) : base( serial )
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