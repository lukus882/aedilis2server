using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class NaughtyBag : Container
	{
		[Constructable]
		public NaughtyBag() : base( 0xE76 )
		{
                  Name = "You were naughty this year - 2006";
                  Hue = Utility.RandomList(0x25, 0x43, 0x3C1);
			Weight = 1.0;
			LootType = LootType.Blessed;
		}

		public NaughtyBag( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}