using System;
using Server;

namespace Server.Items
{
	public class PumpkinBombS : BaseExplosionPotion
	{
		public override int MinDamage { get { return 20; } }
		public override int MaxDamage { get { return 40; } }

		[Constructable]
		public PumpkinBombS() : base( PotionEffect.ExplosionGreater )
		{
			Name = "Unstable Pumpkin";
			ItemID = 0xC6C;
		}

		public PumpkinBombS( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class PumpkinBombM : BaseExplosionPotion
	{
		public override int MinDamage { get { return 25; } }
		public override int MaxDamage { get { return 50; } }

		[Constructable]
		public PumpkinBombM() : base( PotionEffect.ExplosionGreater )
		{
			Name = "Unstable Pumpkin";
			ItemID = 0xC6A;
		}

		public PumpkinBombM( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class PumpkinBombL : BaseExplosionPotion
	{
		public override int MinDamage { get { return 30; } }
		public override int MaxDamage { get { return 60; } }

		[Constructable]
		public PumpkinBombL() : base( PotionEffect.ExplosionGreater )
		{
			Name = "Unstable Pumpkin";
			ItemID = 0x3C10;
		}

		public PumpkinBombL( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
}