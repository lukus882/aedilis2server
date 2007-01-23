using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	[Flipable( 0x232A, 0x232B )]
	public class XmasPackageRare : XmasBag
	{
		[Constructable]
		public XmasPackageRare()
		{
			DropItem( new SeasonsGreetings() );
			DropItem( new WristWatch() );
			DropItem( new BunchOfDates() );
			DropItem( new FruitCake() );
                  DropItem( new Eggnog() );
                  DropItem( new RedChampagneGlass() );
                  DropItem( new GreenChampagneGlass() );
                  DropItem( new FireworksWand() );

		}

		public XmasPackageRare( Serial serial ) : base( serial )
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