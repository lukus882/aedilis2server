using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class TurkeyCook : Food
	{
		[Constructable]
		public TurkeyCook() : base( 7819 )
		{
                  Name = "Turkey Good Enough to Eat";
			Weight = 1.0;
			LootType = LootType.Blessed;
		}

		public TurkeyCook( Serial serial ) : base( serial )
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