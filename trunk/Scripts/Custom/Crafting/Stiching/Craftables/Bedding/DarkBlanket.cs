using System;

namespace Server.Items
{
	[Flipable( 0xA6C, 0xA6D )]
	public class DarkFoldedBlanket : Item
	{
		[Constructable]
		public DarkFoldedBlanket() : base(0xA6C)
		{
			Weight = 5.0;
			Name = "Dark Folded Blanket";
		}

		public DarkFoldedBlanket(Serial serial) : base(serial)
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