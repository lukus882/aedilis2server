using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	[Flipable( 0x232A, 0x232B )]
	public class EasterPackageRare : EasterBasket
	{
		[Constructable]
		public EasterPackageRare()
		{
			DropItem( new EasterCard() );
			DropItem( new ChocolateEasterBunny() );
			DropItem( new EasterCarrot() );
			DropItem( new EasterEggs() );
                  DropItem( new EasterEggs() );

		}

		public EasterPackageRare( Serial serial ) : base( serial )
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