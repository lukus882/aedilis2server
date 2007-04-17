using System;

namespace Server.Items
{
	public class RedCurtianEast : Item
	{
		[Constructable]
		public RedCurtianEast() : base(0x154F)
		{
			Weight = 5.0;
			Name = "Curtian";
		}

		public RedCurtianEast(Serial serial) : base(serial)
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