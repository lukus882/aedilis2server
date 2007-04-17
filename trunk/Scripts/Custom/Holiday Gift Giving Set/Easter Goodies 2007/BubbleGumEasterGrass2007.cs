using System;
using Server.Network;

namespace Server.Items
{
 	public class BubbleGumEasterGrass2007 : Food
	{
		[Constructable]
		public BubbleGumEasterGrass2007() : base( 0x171f )
		{
			Name = "BubbleGum Easter Grass 2007";
            Stackable = false;
			Weight = 1.0;
			FillFactor = 1;
            Hue = Utility.RandomList( 1272, 1372, 68, 1160, 1162, 1173 );
            ItemID = 3378;
			LootType = LootType.Blessed;
		}

		public BubbleGumEasterGrass2007( Serial serial ) : base( serial )
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