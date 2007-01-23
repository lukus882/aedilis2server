using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	[Flipable( 0x232A, 0x232B )]
	public class NaughtyPackageRare : NaughtyBag
	{
		[Constructable]
		public NaughtyPackageRare()
		{
			DropItem( new NaughtyCard() );
			DropItem( new Switches() );
			DropItem( new Coal() );
			DropItem( new Spam() );

		}

		public NaughtyPackageRare( Serial serial ) : base( serial )
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