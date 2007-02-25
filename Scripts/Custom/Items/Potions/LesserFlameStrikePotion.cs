using System;
using Server;

namespace Server.Items
{
	public class LesserFlameStrikePotion : BaseFlameStrikePotion
	{
		public override int MinDamage { get { return 10; } }
		public override int MaxDamage { get { return 15; } }

		[Constructable]
		public LesserFlameStrikePotion() : base( PotionEffect.FlameStrikeLesser )
		{
            Name = "Lesser FlameStrike Potion";
            Hue = 1358;
		}

		public LesserFlameStrikePotion( Serial serial ) : base( serial )
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