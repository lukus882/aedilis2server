using System;

namespace Server.Items
{
	[Flipable( 0xA6E, 0xA6F )]
	public class LightFoldedBlanket : Item
	{
		[Constructable]
		public LightFoldedBlanket() : base(0xA6E)
		{
			Weight = 5.0;
			Name = "Light Folded Blanket";
		}

		public LightFoldedBlanket(Serial serial) : base(serial)
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