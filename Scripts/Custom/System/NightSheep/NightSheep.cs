using System;
using Server.Mobiles;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a nightsheep corpse" )]
	public class NightSheep : BaseCreature
	{
		[Constructable]
		public NightSheep() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a nightsheep";
			Body = 0xD1;
			BaseSoundID = 0xD6;
			Hue = 0xA5E; 

			SetStr( 300, 355 );
			SetDex( 300 );
			SetInt( 90, 100 );

			SetHits( 90, 100 );
			SetMana( 150 );

			SetDamage( 7, 20 );

			SetDamageType( ResistanceType.Physical, 35 );
			SetDamageType( ResistanceType.Fire, 35 );
			SetDamageType( ResistanceType.Cold, 30 );

			SetResistance( ResistanceType.Physical, 40, 75 );
			SetResistance( ResistanceType.Cold, 90, 100 );
			SetResistance( ResistanceType.Fire, 60, 75 );

			SetSkill( SkillName.MagicResist, 85.0 );
			SetSkill( SkillName.Tactics, 90.0 );
			SetSkill( SkillName.Wrestling, 95.0 );

			Fame = 150;
			Karma = -150;

			VirtualArmor = 50;

			Tamable = false;
			ControlSlots = 1;
			MinTameSkill = 11.1;
		}

		
		public override FoodType FavoriteFood{ get{ return FoodType.GrainsAndHay | FoodType.FruitsAndVegies; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		
		public NightSheep(Serial serial) : base(serial)
		{
		}

		
		
		public override void OnDeath( Container c )
		{
			
			if ( this.Controlled )
			NightSheepSystem.RemoveSheep( this.ControlMaster, this );
			
			Timer t = new NightCritterTimer( c );
			t.Start();
			
			base.OnDeath( c );
			
			
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
