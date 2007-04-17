using System;

namespace Server.Items
{
	[Flipable( 0xA94, 0xA95 )]
	public class RichFoldedSheet : Item
	{
		[Constructable]
		public RichFoldedSheet() : base(0xA94)
		{
			Weight = 5.0;
			Name = "Rich Folded Sheet";
		}

		public RichFoldedSheet(Serial serial) : base(serial)
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