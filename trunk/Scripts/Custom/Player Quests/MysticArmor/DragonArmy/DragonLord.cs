/*
***********************************
*************Created By************
***********************************
**************Broadside ***********
***********************************
****************AKA****************
***********************************
**************Bad Karma************
***********************************
 */
using System; 
using System.Collections; 
using Server.Misc; 
using Server.Items; 
using Server.Mobiles; 

namespace Server.Mobiles 
{ 
	[CorpseName( "a Dragon Lord corpse" )]
	public class DragonLord : BaseCreature
	{
		[Constructable]
		public DragonLord() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Dragon Lord";
			Body = 318;
			BaseSoundID = 0x174;
			Hue = 302;
			SetStr( 2205, 3245 );
			SetDex( 781, 995 );
			SetInt( 2361, 4475 );
			
			SetHits( 10000, 14000 );

			SetDamage( 15, 25 );

			SetDamageType( ResistanceType.Physical, 50, 70 );
			SetDamageType( ResistanceType.Fire, 50, 70 );
			SetDamageType( ResistanceType.Energy, 50, 70 );
			SetDamageType( ResistanceType.Poison, 50, 70 );

			SetResistance( ResistanceType.Physical, 100 );
			SetResistance( ResistanceType.Fire, 30, 50 );
			SetResistance( ResistanceType.Cold, 30, 50 );
			SetResistance( ResistanceType.Poison, 30, 50 );
			SetResistance( ResistanceType.Energy, 30, 50 );
			
			SetSkill( SkillName.Wrestling, 90, 120 );
			SetSkill( SkillName.Tactics, 90, 120 );
			SetSkill( SkillName.Healing, 120, 150 );
			SetSkill( SkillName.SpiritSpeak, 120, 150 );
			SetSkill( SkillName.Anatomy, 90, 120 );
			SetSkill( SkillName.Magery, 90, 120 );
			SetSkill( SkillName.MagicResist, 90, 120 );
			SetSkill( SkillName.Meditation, 90, 120 );
			SetSkill( SkillName.DetectHidden, 20000, 30000 );
			
			Fame = 5000;
			Karma = -5000;
			
			PackGold( 1420, 2690 );
			AddLoot( LootPack.UltraRich, 2 );  // set the loot type to ultra rich
			
			if( Utility.Random( 50 ) < 50 ) 
			switch ( Utility.Random( 50 ))
			{ 
        		case 0:	PackItem( new MysticArms() );
        		break;
        		case 1: PackItem( new MysticGloves() );
        		break;
        		case 2: PackItem( new MysticGorget() );
        		break;
        		case 3: PackItem( new MysticHelm() );
        		break;
        		case 4: PackItem( new MysticLegs() );
        		break;
        		case 5: PackItem( new MysticTunic() );
        		break;
        		
		}

	}

            public override bool AlwaysMurderer { get { return true; } }
            public override bool BardImmune { get { return true; } } 	

		public DragonLord( Serial serial ) : base( serial )
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