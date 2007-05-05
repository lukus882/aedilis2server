using System;

namespace Server.Items
{
	[Flipable( 0x1638, 0x1639 )]
	public class MiniRedCurtian : Item
	{
		[Constructable]
		public MiniRedCurtian() : base(0x1638)
		{
			Weight = 5.0;
			Name = "Mini Red Curtian";
		}

		public MiniRedCurtian(Serial serial) : base(serial)
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