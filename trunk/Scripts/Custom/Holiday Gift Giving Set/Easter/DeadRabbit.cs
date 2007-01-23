using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class DeadRabbit : Food
	{
		[Constructable]
		public DeadRabbit() : base( 15723 )
		{
                  Name = "Corpse of The Easter Bunny";
			Weight = 1.0;
			LootType = LootType.Blessed;
		}

		public DeadRabbit( Serial serial ) : base( serial )
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