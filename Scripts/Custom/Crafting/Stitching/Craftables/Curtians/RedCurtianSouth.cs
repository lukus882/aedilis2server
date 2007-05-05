using System;

namespace Server.Items
{
	public class RedCurtianSouth : Item
	{
		[Constructable]
		public RedCurtianSouth() : base(0x154E)
		{
			Weight = 5.0;
			Name = "Curtian";
		}

		public RedCurtianSouth(Serial serial) : base(serial)
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