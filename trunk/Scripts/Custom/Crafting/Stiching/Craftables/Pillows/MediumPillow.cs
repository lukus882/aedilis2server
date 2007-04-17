using System;

namespace Server.Items
{
	public class MediumPillow : Item
	{
		[Constructable]
		public MediumPillow() : base(0x163B)
		{
			Weight = 5.0;
			Name = "A Medium Pillow";
		}

		public MediumPillow(Serial serial) : base(serial)
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