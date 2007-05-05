using System;

namespace Server.Items
{
	public class SmallPillow : Item
	{
		[Constructable]
		public SmallPillow() : base(0x1915)
		{
			Weight = 5.0;
			Name = "A Small Pillow";
		}

		public SmallPillow(Serial serial) : base(serial)
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