using System;
using Server.Mobiles;
using Server.Items;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a sander corpse" )]
	public class Sander : BaseCreature
	{
		[Constructable]
		public Sander() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a sander";
			Body = 14;
			BaseSoundID = 0x4CF;
			Hue = 1357;

			SetStr( 100, 115 );
			SetDex( 500 );
			SetInt( 10, 20 );

			SetHits( 90, 100 );
			SetMana( 0 );

			SetDamage( 4,12 );

			SetDamageType( ResistanceType.Energy, 100 );
			

			SetResistance( ResistanceType.Physical, 98, 100 );
			SetResistance( ResistanceType.Energy, 90, 100 );
			

			SetSkill( SkillName.MagicResist, 35.0 );
			SetSkill( SkillName.Tactics, 90.0 );
			SetSkill( SkillName.Wrestling, 120.0 );
			SetSkill( SkillName.Anatomy, 120.0 );

			Fame = 25;
			Karma = -150;

			VirtualArmor = 38;

			Tamable = false;
			Timer t = new SanderTimer( this );
			t.Start();
			
		}

		
		public Sander(Serial serial) : base(serial)
		{
		}

		public override void OnDeath( Container c )
		{
			
			Timer t = new NightCritterTimer( c );
			t.Start();
			
			base.OnDeath( c );
			
			
		}		
		
		private class SanderTimer : Timer
		{
			private DateTime m_Expire;
			private BaseCreature m_sander;
			
			public SanderTimer( BaseCreature sand ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 1.5 ) )
			{
			   m_Expire = DateTime.Now + TimeSpan.FromMinutes( 10.0 );
			   m_sander = sand;
			                   
			}
			                            
			protected override void OnTick()
			{
				if ( DateTime.Now >= m_Expire )
				{
					m_sander.Delete();
					Stop();
				}
			}
		}
		
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}
