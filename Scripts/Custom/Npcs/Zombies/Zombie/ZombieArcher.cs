using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles 
{ 
	[CorpseName( "a Zombie Archer's corpse" )] 
	public class ZombieArcher : BaseCreature 
	{ 
		[Constructable] 
		public ZombieArcher() : base( AIType.AI_Archer, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{ 	
			Title = "A Zombie Archer";
			Body = 400;
			Hue = 1425; 

			SetStr( 200, 300 );
			SetDex( 191, 215 );
			SetInt( 196, 220 );

			SetHits( 520, 525 );

			SetDamage( 10, 15 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 0, 01 );
			SetResistance( ResistanceType.Fire, 0, 01 );
			SetResistance( ResistanceType.Poison, 0, 01 );
			SetResistance( ResistanceType.Energy, 0, 01 );

			SetSkill( SkillName.Archery, 75.1, 100.0 );
			SetSkill( SkillName.Tactics, 75.1, 100.0 );
			SetSkill( SkillName.MagicResist, 75.0, 97.5 );
			SetSkill( SkillName.Tactics, 65.0, 87.5 );
			SetSkill( SkillName.Wrestling, 20.2, 60.0 );

			Fame = 2500;
			Karma = -2500;

			VirtualArmor = 50;

			Item hair = new Item( Utility.RandomList( 0x203B, 0x2049, 0x2048, 0x204A ) );
			hair.Hue = 1429;
			hair.Layer = Layer.Hair;
			hair.Movable = false;
			AddItem( hair );
			
			Item beard = new Item( Utility.RandomList( 0x203E, 0x203F, 0x2040, 0x2041, 0x204B, 0x204C, 0x204D ) );

				beard.Hue = hair.Hue;
				beard.Layer = Layer.FacialHair;
				beard.Movable = false;
				AddItem( beard );
			
				//switch ( Utility.Random( 10 ))
					if (Utility.Random(10) == 1 ) 
   					AddItem( new Bow() ); 
					else 
				    AddItem( new CompositeBow());

			
			ZombieChest chest = new ZombieChest();
			chest.Movable = false;
   			AddItem(chest);
   			
   			ZombieHands gloves = new ZombieHands();
   			gloves.Movable = false;
  			AddItem(gloves);
  			
  			ZombieNeck gorget = new ZombieNeck();
  			gorget.Movable = false;
 			AddItem(gorget);
 			
 			ZombieLegs legs = new ZombieLegs();
 			legs.Movable = false;
			AddItem(legs);

			ZombieRobe robe = new ZombieRobe();
			robe.Movable = false;
			AddItem(robe);

			Sandals sandals = new Sandals();
			sandals.Hue = 1425;
			AddItem( sandals );

		}

		public override void GenerateLoot()
		{
			switch ( Utility.Random( 15 ))
			{
				case 0: PackItem( new ZombieChest() ); break;
				case 1: PackItem( new ZombieHands() ); break;
				case 2: PackItem( new ZombieNeck() ); break;
				case 3: PackItem( new ZombieLegs() ); break;
				case 4: PackItem( new ZombieArms() ); break;
				case 5: PackItem( new ZombieRobe() ); break;
			}

			switch ( Utility.Random( 20 ))
			{
				case 0: PackItem( new ZombieFace() ); break;
			}

			AddLoot( LootPack.FilthyRich, 30 );
			AddLoot( LootPack.MedScrolls, 10 );
			AddLoot( LootPack.Gems, 10 ); 
			AddItem( new Arrow(250) );
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override bool AlwaysMurderer{ get{ return true; } }

		public ZombieArcher( Serial serial ) : base( serial )
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
