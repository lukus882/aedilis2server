using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class Coal : Item
	{
		[Constructable]
		public Coal() : base( 0x19B9 )
		{
                  Hue = 0x497;
			Weight = 1.0;
			LootType = LootType.Blessed;
		}

		public Coal( Serial serial ) : base( serial )
		{
		}
			public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1060662, "You Were Naughty This Year\t2006" );
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