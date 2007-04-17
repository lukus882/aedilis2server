using System;
using Server.Network;

namespace Server.Items
{
 	public class MarshmallowPeep2007 : Food
	{
		[Constructable]
		public MarshmallowPeep2007() : base( 0x20d1 )
		{
			Name = "Marshmallow Peep 2007";
            Stackable = false;
			Weight = 1.0;
			FillFactor = 1;
            Hue = 2028;
            ItemID = 8474;
			LootType = LootType.Blessed;
		}

		public MarshmallowPeep2007( Serial serial ) : base( serial )
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