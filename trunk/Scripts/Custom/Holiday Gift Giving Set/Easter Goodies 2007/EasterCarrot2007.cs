using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class EasterCarrot2007 : Food
	{
		[Constructable]
		public EasterCarrot2007() : base( 0xC78 )
		{
            Name = "Easter Bunny's Carrot 2007";
			Weight = 1.0;
			LootType = LootType.Blessed;
		}

		public EasterCarrot2007( Serial serial ) : base( serial )
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