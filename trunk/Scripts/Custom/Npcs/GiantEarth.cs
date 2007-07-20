using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "an earth elemental corpse" )]
	public class GiantEarthElemental : BaseCreature
	{
		public override double DispelDifficulty{ get{ return 117.5; } }
		public override double DispelFocus{ get{ return 45.0; } }

		[Constructable]
		public GiantEarthElemental() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a giant earth elemental";
			Body = 752;
			BaseSoundID = 268;
			Hue = 46;

			SetStr( 1200, 1400 );
			SetDex( 100, 200 );
			SetInt( 71, 92 );

			SetHits( 1200, 1400 );

			SetDamage( 45, 50 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 30, 35 );
			SetResistance( ResistanceType.Fire, 10, 20 );
			SetResistance( ResistanceType.Cold, 10, 20 );
			SetResistance( ResistanceType.Poison, 15, 25 );
			SetResistance( ResistanceType.Energy, 15, 25 );

			SetSkill( SkillName.MagicResist, 50.1, 95.0 );
			SetSkill( SkillName.Tactics, 60.1, 100.0 );
			SetSkill( SkillName.Wrestling, 60.1, 100.0 );

			Fame = 3500;
			Karma = -3500;

			VirtualArmor = 34;
			ControlSlots = 2;

			PackItem( new FertileDirt( Utility.RandomMinMax( 1, 4 ) ) );
			PackItem( new IronOre( 3 ) ); // TODO: Five small iron ore
			PackItem( new MandrakeRoot() );
			if ( 0.10 > Utility.RandomDouble() )
			                    PackItem( new PhoenixLantern() );	
			
			  if ( 0.05 > Utility.RandomDouble() )
		  PackItem( new Sandals( Utility.RandomDyedHue() ) );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Meager );
			AddLoot( LootPack.Gems );			
		}

		private DateTime m_NextQuake;

		public override void OnThink()
		{
		
			base.OnThink();

			if ( DateTime.Now < m_NextQuake)
				return;		
					
			m_NextQuake = DateTime.Now + TimeSpan.FromSeconds(40.0 * Utility.RandomDouble() );			
			
			Timer.DelayCall( TimeSpan.FromSeconds( 40.0 ), new TimerCallback( Quake ) );			
		}

		private void Quake()
		{			
			
			ArrayList list = new ArrayList();

			foreach ( Mobile m in this.GetMobilesInRange( 15 ) )
			{
				if ( m == this || m == null )
					continue;				
				{					
					if ( m.Player && m.AccessLevel == AccessLevel.Player && m.Alive)
						list.Add( m );					
				}
				
			}
			for ( int i = 0; i < list.Count; ++i )
			{
				Mobile m = (Mobile)list[i];
				m.Hits -= Utility.Random( 25, 60);					
				m.SendMessage ("an earthquake shakes the dungeon and almost knocks you off your feet.");
				this.Say("*"+m.Name+ " I will send you through the floor of this dungeon!");
				m.PlaySound(0x101);	
				m.Animate( 22, 5, 1, true, false, 0 );
				return;
			}
		
				
		}
			  public class PhoenixLantern : Lantern
			{
		public override int LabelNumber{ get{ return 1061618; } } 

		[Constructable]
		public PhoenixLantern()
		{
                       		 Name = "a lantern";
			Hue = (Utility.RandomDyedHue());
		}

		public PhoenixLantern( Serial serial ) : base( serial )
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
		
		

		public override bool BleedImmune{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 1; } }
		public virtual void OnBeforeDeath()
      		  {	
		this.Say("I WILL RISE AGAIN AND CRUSH YOU HUMANS");
            		return;
        		}

		public GiantEarthElemental( Serial serial ) : base( serial )
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