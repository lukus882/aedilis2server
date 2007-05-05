using System;

namespace Server.Items
{
	[Flipable( 0xA92, 0xA93 )]
	public class FoldedSheet : Item
	{
		[Constructable]
		public FoldedSheet() : base(0xA92)
		{
			Weight = 5.0;
			Name = "Folded Sheet";
		}

		public FoldedSheet(Serial serial) : base(serial)
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