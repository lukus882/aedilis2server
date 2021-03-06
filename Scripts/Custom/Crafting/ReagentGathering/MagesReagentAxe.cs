//Created by Lord Greywolf

using System;
using Server.Items;
using Server.Network;
using Server.Engines.Harvest;

namespace Server.Items
{
	[FlipableAttribute(0x13E4, 0x13E3)]
	public class MageReagentAxe : BaseAxe
	{
		public override HarvestSystem HarvestSystem { get { return MageReagentGathering.System; } }

		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Slash1H; } }

		[Constructable]
		public MageReagentAxe() : base(0x143c)
		{
			Name = "Mage Reagent Gathering Axe";
			Hue = 64;
			Weight = 3.0;
			UsesRemaining = 50;
			ShowUsesRemaining = true;
		}

		public MageReagentAxe(Serial serial) : base(serial)
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
			ShowUsesRemaining = true;
		}
	}
}