using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "an ettins corpse" )]
	public class GateEttin : BaseCreature
	{
		[Constructable]
		public GateEttin() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an ettin";
			Body = 18;
			BaseSoundID = 367;

			SetStr( 136, 165 );
			SetDex( 56, 75 );
			SetInt( 31, 55 );

			SetHits( 82, 99 );

			SetDamage( 7, 17 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 40 );
			SetResistance( ResistanceType.Fire, 15, 25 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 15, 25 );
			SetResistance( ResistanceType.Energy, 15, 25 );

			SetSkill( SkillName.MagicResist, 40.1, 55.0 );
			SetSkill( SkillName.Tactics, 50.1, 70.0 );
			SetSkill( SkillName.Wrestling, 50.1, 60.0 );

			Fame = 3000;
			Karma = -3000;

			VirtualArmor = 38;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Potions );
		}

		public class MobileDeleteTime : Timer
		{
			private Item mob;

			public MobileDeleteTime( Item m ) : base( TimeSpan.FromSeconds( 10 ) )
			{
				mob = m;
				Priority = TimerPriority.OneSecond;
			}

			protected override void OnTick()
			{
			 if( mob == null || mob.Deleted )
				{
				Stop();
				return;
				}

				mob.Delete();
			}
		}

		public override bool OnBeforeDeath()
		{

			// spawn the item
			Item item = (Item)Activator.CreateInstance( typeof(Moongate) );
			Moongate moon = (Moongate)item;

			moon.TargetMap = Map.Felucca; //or map
			moon.Target = new Point3D( 5394, 1111, 0 ); //where the gate goes 
			moon.Hue = 29;

			Point3D pnt = GetSpawnLocation();

			moon.MoveToWorld(pnt,this.Map);

			Timer m_timer = new MobileDeleteTime( item ); 
			m_timer.Start();

			return true;
		}
		
		//from champspawn.cs
		public Point3D GetSpawnLocation()
		{
			int m_SpawnRange = 2;
			Map map = Map;

			if ( map == null )
			return Location;

			// Try 20 times to find a spawnable location.
			for ( int i = 0; i < 20; i++ )
			{
				int x = Location.X + (Utility.Random( (m_SpawnRange * 2) + 1 ) - m_SpawnRange);
				int y = Location.Y + (Utility.Random( (m_SpawnRange * 2) + 1 ) - m_SpawnRange);
				int z = Map.GetAverageZ( 5394, 1116 ); // where the gate spawns

				if ( Map.CanSpawnMobile( new Point2D( 5394, 1116 ), 0 ) ) // where the gate spawns
				return new Point3D( 5394, 1116, 0 ); // where the gate spawns
			}

			return Location;
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 1; } }
		public override int Meat{ get{ return 4; } }

		public GateEttin( Serial serial ) : base( serial )
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