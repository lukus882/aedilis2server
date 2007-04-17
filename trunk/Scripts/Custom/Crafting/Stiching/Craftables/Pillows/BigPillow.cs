using System;

namespace Server.Items
{
	public class BigPillow : Item
	{
		[Constructable]
		public BigPillow() : base(0x163A)
		{
			Weight = 5.0;
			Name = "A Big Pillow";
		}

		public BigPillow(Serial serial) : base(serial)
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