//Created by Lord Greywolf

using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Spells;

namespace Server.Mobiles
{
	[CorpseName( "a beholder corpse" )]
	public class BeholderRegular : BaseCreature
	{
		int damagebonus = 0; // curent level of "spell" damage bonus, hits (*10) bonus, str bonus, resist (/2) bonus
		int poisonlevelnumber = 1; //1 to 5 DO NOT GO OVER 5 HERE and is used for damage inc, additional hides, higher stun time & higher mana drain
		int gasspore = Utility.RandomMinMax( 0, 9 ); //1 in 10 of being a gass spore (in chance - 1 is number to use)

		[Constructable]
		public BeholderRegular() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a Beholder";
			Body = 22;
			Hue = 0;

			SetStr( 50 + damagebonus, 100 + damagebonus );
			SetDex( 100, 120 );
			SetInt( 50, 60 );

			SetHits( 100 + (damagebonus * 10) , 120 + (damagebonus * 10)  );

			SetDamage( 5 + poisonlevelnumber, 10 + poisonlevelnumber );

			SetDamageType( ResistanceType.Physical, 100 );

			int resistmod = (int)(damagebonus/2);
			SetResistance( ResistanceType.Physical, 10 + resistmod, 20 + resistmod );
			SetResistance( ResistanceType.Fire, 5 + resistmod, 10 + resistmod );
			SetResistance( ResistanceType.Cold, 5 + resistmod, 10 + resistmod );
			SetResistance( ResistanceType.Poison, 5 + resistmod, 10 + resistmod );
			SetResistance( ResistanceType.Energy, 5 + resistmod, 10 + resistmod );

			SetSkill( SkillName.MagicResist, 75.0 + resistmod );
			SetSkill( SkillName.Tactics, 50.0 + resistmod );
			SetSkill( SkillName.Wrestling, 50.0 + resistmod );

			Fame = (5000 + (damagebonus * 10));
			Karma = -(5000 + (damagebonus * 10));

			VirtualArmor = 5 + resistmod;
			
			AddLoot( LootPack.FilthyRich, (poisonlevelnumber) );

		}
		public override int Hides{ get{ return 10 + poisonlevelnumber; } }
		public override bool AutoDispel{ get{ return true; } }

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			if (gasspore == 0)
				BeholderGas();

			else
				BeholderEye();
		}

		public override void OnDamagedBySpell( Mobile caster )
		{
			if ( caster == this )
				return;

			if (gasspore == 0)
				BeholderGas();

			else
				BeholderDrain();
		}

		public void BeholderGas()
		{
			Map map = this.Map;

			if ( map == null )
				return;

			ArrayList targets = new ArrayList();

			foreach ( Mobile m in this.GetMobilesInRange( 8 ) )
			{
				if ( m == this || !CanBeHarmful( m ) )
					continue;

				if ( m is BaseCreature && (((BaseCreature)m).Controlled || ((BaseCreature)m).Summoned || ((BaseCreature)m).Team != this.Team) )
					targets.Add( m );
				else if ( m.Player )
					targets.Add( m );
			}

			for ( int i = 0; i < targets.Count; ++i )
			{
				Mobile m = (Mobile)targets[i];
				double damage = 15 + Utility.RandomMinMax( 5, 10 ) + damagebonus;
				DoHarmful( m );
				AOS.Damage( m, this, (int)damage, 0, 0, 0, 100, 0 );
				m.SendMessage("You were hit by its Poison Ray");
				if ( m.Alive && m.Body.IsHuman && !m.Mounted )
					m.Animate( 20, 7, 1, true, false, 0 ); // take hit
				m.ApplyPoison( this, Poison.Lethal);
				m.SendMessage("This was realy a Gas Spore and you were just Poisoned");
			}
			this.Kill();
		}

		public void BeholderEye()
		{
			Map map = this.Map;

			if ( map == null )
				return;

			ArrayList targets = new ArrayList();

			foreach ( Mobile mm in this.GetMobilesInRange( 8 ) )
			{
				if ( mm == this || !CanBeHarmful( mm ) )
					continue;

				if ( mm is BaseCreature && (((BaseCreature)mm).Controlled || ((BaseCreature)mm).Summoned || ((BaseCreature)mm).Team != this.Team) )
					targets.Add( mm );
				else if ( mm.Player )
					targets.Add( mm );
			}

			for ( int i = 0; i < targets.Count; ++i )
			{
				Mobile m = (Mobile)targets[i];
				switch ( Utility.Random( 10 ) ) 
				{
					case 0: //physical ray
					{
						double damage = 25 + Utility.RandomMinMax( 15, 30 ) + damagebonus;
						DoHarmful( m );
						AOS.Damage( m, this, (int)damage, 100, 0, 0, 0, 0 );
						m.SendMessage("You were hit by its Physical Ray");
						if ( m.Alive && m.Body.IsHuman && !m.Mounted )
							m.Animate( 20, 7, 1, true, false, 0 ); // take hit
						int toDrain = (int)(damage/2);
						Hits += toDrain;	
					} break;

					case 1: //fire ray
					{
						double damage = 25 + Utility.RandomMinMax( 15, 30 ) + damagebonus;
						DoHarmful( m );
						AOS.Damage( m, this, (int)damage, 0, 100, 0, 0, 0 );
						m.SendMessage("You were hit by its Fire Ray");
						if ( m.Alive && m.Body.IsHuman && !m.Mounted )
							m.Animate( 20, 7, 1, true, false, 0 ); // take hit

						int toDrain = (int)(damage/2);
						Hits += toDrain;	
					} break;

					case 2: //cold ray
					{
						double damage = 25 + Utility.RandomMinMax( 15, 30 ) + damagebonus;
						DoHarmful( m );
						AOS.Damage( m, this, (int)damage, 0, 0, 100, 0, 0 );
						m.SendMessage("You were hit by its Cold Ray");
						if ( m.Alive && m.Body.IsHuman && !m.Mounted )
							m.Animate( 20, 7, 1, true, false, 0 ); // take hit

						int toDrain = (int)(damage/2);
						Hits += toDrain;
					} break;

					case 3: //energy ray
					{
						double damage = 25 + Utility.RandomMinMax( 15, 30 ) + damagebonus;
						DoHarmful( m );
						AOS.Damage( m, this, (int)damage, 0, 0, 0, 0, 100 );
						m.SendMessage("You were hit by its Energy Ray");
						if ( m.Alive && m.Body.IsHuman && !m.Mounted )
							m.Animate( 20, 7, 1, true, false, 0 ); // take hit

						int toDrain = (int)(damage/2);
						Hits += toDrain;
					} break;

					case 4: //spectrum ray
					{
						double damage = 25 + Utility.RandomMinMax( 15, 30 ) + damagebonus;
						DoHarmful( m );
						AOS.Damage( m, this, (int)damage, 20, 20, 20, 20, 20 );
						m.SendMessage("You were hit by its Spectrum Ray");
						if ( m.Alive && m.Body.IsHuman && !m.Mounted )
							m.Animate( 20, 7, 1, true, false, 0 ); // take hit

						int toDrain = (int)(damage/2);
						Hits += toDrain;
					} break;

					case 5: //poison ray
					{
						double damage = 15 + Utility.RandomMinMax( 5, 10 ) + damagebonus;
						DoHarmful( m );
						AOS.Damage( m, this, (int)damage, 0, 0, 0, 100, 0 );
						m.SendMessage("You were hit by its Poison Ray");
						if ( m.Alive && m.Body.IsHuman && !m.Mounted )
							m.Animate( 20, 7, 1, true, false, 0 ); // take hit

						switch ( poisonlevelnumber )
						{
							case 1: default: m.ApplyPoison( this, Poison.Lesser); break;
							case 2: m.ApplyPoison( this, Poison.Regular); break;
							case 3: m.ApplyPoison( this, Poison.Greater); break;
							case 4: m.ApplyPoison( this, Poison.Deadly); break;
							case 5: m.ApplyPoison( this, Poison.Lethal); break;
						}
						int toDrain = (int)(damage/2);
						Hits += toDrain;
					} break;

					case 6: //life drain ray
					{
						double damage = (int)(((30 + damagebonus)/100) * m.Hits);
						DoHarmful( m );
						m.SendMessage("You were hit by its Life Drain Ray");
						if ( m.Alive && m.Body.IsHuman && !m.Mounted )
							m.Animate( 20, 7, 1, true, false, 0 ); // take hit

						int toDrain = (int)(damage);
						Hits += toDrain;
						m.Hits -=  toDrain;
					} break;

					case 7: //elemental ray
					{
						double damage = 25 + Utility.RandomMinMax( 15, 30 ) + damagebonus;
						DoHarmful( m );
						AOS.Damage( m, this, (int)damage, 0, 25, 25, 25, 25 );
						m.SendMessage("You were hit by its Elemental Ray");
						if ( m.Alive && m.Body.IsHuman && !m.Mounted )
							m.Animate( 20, 7, 1, true, false, 0 ); // take hit

						int toDrain = (int)(damage/2);
						Hits += toDrain;
					} break;

					case 8: //stun ray
					{
						DoHarmful( m );
						m.SendMessage("You were hit by its Stun Ray");
						if ( m.Alive && m.Body.IsHuman && !m.Mounted )
							m.Animate( 20, 7, 1, true, false, 0 ); // take hit

						m.SendLocalizedMessage( 1004014 ); // You have been stunned!
						int stuntime = 4 + poisonlevelnumber;
						m.Freeze( TimeSpan.FromSeconds( stuntime ) );
					} break;

					case 9: //teleportion ray
					{
						DoHarmful( m );
						m.SendMessage("You were hit by its Teleportion Ray and are a little confused");
						if ( m.Alive && m.Body.IsHuman && !m.Mounted )
							m.Animate( 20, 7, 1, true, false, 0 ); // take hit
						int x = this.X;
						int y = this.Y;
						int z = this.Z;
						m.X = x;
						m.Y = y;
						m.Z = z;
						m.Freeze( TimeSpan.FromSeconds( 1.0 ) );
					} break;
				}
			}
		}

		public void BeholderDrain()
		{
			Map map = this.Map;

			if ( map == null )
				return;

			ArrayList targets = new ArrayList();

			foreach ( Mobile m in this.GetMobilesInRange( 8 ) )
			{
				if ( m == this || !CanBeHarmful( m ) )
					continue;

				if ( m is BaseCreature && (((BaseCreature)m).Controlled || ((BaseCreature)m).Summoned || ((BaseCreature)m).Team != this.Team) )
					targets.Add( m );
				else if ( m.Player )
					targets.Add( m );
			}

			for ( int i = 0; i < targets.Count; ++i )
			{
				Mobile m = (Mobile)targets[i];
				m.SendMessage("The Beholder has used its Anti-Magic Ray");
				int percentlost = (50 + (poisonlevelnumber * 10));
				int amountlost = (int)(m.Mana * percentlost);
				m.Mana -= amountlost;
			}
		}

		public BeholderRegular( Serial serial ) : base( serial )
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