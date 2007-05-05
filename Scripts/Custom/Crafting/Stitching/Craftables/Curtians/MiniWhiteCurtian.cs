using System;

namespace Server.Items
{
	[Flipable( 0x15F6, 0x15F7 )]
	public class MiniWhiteCurtian : Item
	{
		[Constructable]
		public MiniWhiteCurtian() : base(0x15F6)
		{
			Weight = 5.0;
			Name = "Mini White Curtian";
		}

		public MiniWhiteCurtian(Serial serial) : base(serial)
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