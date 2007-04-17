using System;

namespace Server.Items
{
	[Flipable( 0x159E, 0x159F )]
	public class SingleWhiteCurtian : Item
	{
		[Constructable]
		public SingleWhiteCurtian() : base(0x159E)
		{
			Weight = 5.0;
			Name = "White Curtian";
		}

		public SingleWhiteCurtian(Serial serial) : base(serial)
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