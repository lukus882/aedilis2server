using System;
using Server;

namespace Server.Items
{
	public class GreaterFlameStrikePotion : BaseFlameStrikePotion
	{
		public override int MinDamage { get { return 30; } }
		public override int MaxDamage { get { return 45; } }

		[Constructable]
		public GreaterFlameStrikePotion() : base( PotionEffect.FlameStrike )
		{
            Name = "Greater FlameStrike Potion";
            Hue = 1358;
		}

		public GreaterFlameStrikePotion( Serial serial ) : base( serial )
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