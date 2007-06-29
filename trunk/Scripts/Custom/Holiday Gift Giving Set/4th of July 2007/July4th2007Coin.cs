using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class July4th2007Coin : Item
	{
		[Constructable]
		public July4th2007Coin() : base( 0x186F )
		{
                  Name = "In Commemoration : Aedilis 4th of July 2007";
                  Hue = 28;
		  Weight = 1.0;
		  LootType = LootType.Blessed;
		}

		public July4th2007Coin( Serial serial ) : base( serial )
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