using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class HalloweenBlood : Item
	{
		[Constructable]
		public HalloweenBlood() : base( 4655 )
		{
                  Name = "Blood of the Admins, Halloween 2006";
                 
			Weight = 1.0;
			LootType = LootType.Blessed;
		}

		public HalloweenBlood( Serial serial ) : base( serial )
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