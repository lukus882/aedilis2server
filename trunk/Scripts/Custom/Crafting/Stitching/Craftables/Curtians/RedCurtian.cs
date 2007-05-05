using System;

namespace Server.Items
{
	public class RedCurtian : Item
	{
		[Constructable]
		public RedCurtian() : base(0x1557)
		{
			Weight = 5.0;
			Name = "Curtian";
		}

		public RedCurtian(Serial serial) : base(serial)
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