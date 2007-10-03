using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class HalloweenMug : Item
	{
		[Constructable]
		public HalloweenMug() : base( 0xFFB )
		{
                  Name = "Eerie Skull Mug";
			Weight = 1.0;
			LootType = LootType.Blessed;
		}

		public HalloweenMug( Serial serial ) : base( serial )
		{
		}
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1060662, "Happy Halloween\t2007" );
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