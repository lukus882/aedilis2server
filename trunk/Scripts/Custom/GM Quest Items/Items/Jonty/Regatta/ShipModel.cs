using System;

namespace Server.Items
{
	[FlipableAttribute(0x14F3, 0x14F4)]
	public class ShipModel : Item
	{
		[Constructable]
		public ShipModel() : base(0x14F3)
		{
			Weight = 3;
			Hue = 0x4EB;
			Name = "Word Regatta Trophy";
		}

		public ShipModel(Serial serial) : base(serial)
		{
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