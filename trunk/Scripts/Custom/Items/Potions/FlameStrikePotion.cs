using System;
using Server;

namespace Server.Items
{
	public class FlameStrikePotion : BaseFlameStrikePotion
	{
		public override int MinDamage { get { return 20; } }
		public override int MaxDamage { get { return 30; } }

		[Constructable]
		public FlameStrikePotion() : base( PotionEffect.FlameStrike )
		{
            Name = "FlameStrike Potion";
            Hue = 1358;
		}

		public FlameStrikePotion( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}