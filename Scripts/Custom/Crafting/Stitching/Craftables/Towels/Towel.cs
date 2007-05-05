using System;

namespace Server.Items
{
	public class Towel : Item
	{
		[Constructable]
		public Towel() : base(0x1914)
		{
			Weight = 5.0;
			Name = "A Towel";
		}

		public Towel(Serial serial) : base(serial)
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