using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class ChocolateBar : Food
	{
		[Constructable]
		public ChocolateBar() : base( 0x979 )
		{
                  Name = "Bar of Chocolate";
                  Hue = Utility.RandomList(0x1053, 0x156, 0x497, 0x481, 0x71A, 0x21E);
			Weight = 1.0;
			LootType = LootType.Blessed;
		}

		public ChocolateBar( Serial serial ) : base( serial )
		{
		}
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1060662, "Happy Halloween\t2006" );
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