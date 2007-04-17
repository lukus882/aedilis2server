using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class EasterCard2007 : Item
	{
		[Constructable]
		public EasterCard2007() : base( 0x14EF )
		{
            Name = "Happy Easter 2007";
            Hue = Utility.RandomList(0x36, 0x17, 0x120);
			Weight = 1.0;
			LootType = LootType.Blessed;
		}

		public EasterCard2007( Serial serial ) : base( serial )
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