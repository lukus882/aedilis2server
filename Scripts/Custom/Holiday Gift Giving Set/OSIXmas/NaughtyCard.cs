using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class NaughtyCard : Item
	{
		[Constructable]
		public NaughtyCard() : base( 0x14EF )
		{
                  Name = "Maybe you will get a nicer gift next year";
                  Hue = Utility.RandomList(0x25, 0x43, 0x3C1);
			Weight = 1.0;
			LootType = LootType.Blessed;
		}

		public NaughtyCard( Serial serial ) : base( serial )
		{
		}
			public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1060662, "Seasons Greetings\t2006" );
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