using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	[Flipable( 0x232A, 0x232B )]
	public class HalloweenPackageRare : HalloweenBag
	{
		[Constructable]
		public HalloweenPackageRare()
		{
			DropItem( new ChocolateBar() );
			DropItem( new JackOLantern() );
			DropItem( new HalloweenMug() );
                  DropItem( new HalloweenSkull() );
                  DropItem( new HalloweenScarecrow() );
                 
		}

		public HalloweenPackageRare( Serial serial ) : base( serial )
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