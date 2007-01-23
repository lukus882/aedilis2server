using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class Switches : Item
	{
		[Constructable]
		public Switches() : base( 0xDE1 )
		{
                  Name = "Switches";
			Weight = 1.0;
			LootType = LootType.Blessed;
		}

		public Switches( Serial serial ) : base( serial )
		{
		}
			public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1060662, "You were Naughty this Year\t2006" );
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