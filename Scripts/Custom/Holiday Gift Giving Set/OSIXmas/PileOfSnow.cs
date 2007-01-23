using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class PileSnow : Item
	{
		[Constructable]
		public PileSnow() : base( 0x913 )
		{
                  Name = "A Pile Of Snow";
                  Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Blessed;
		}

		public PileSnow( Serial serial ) : base( serial )
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