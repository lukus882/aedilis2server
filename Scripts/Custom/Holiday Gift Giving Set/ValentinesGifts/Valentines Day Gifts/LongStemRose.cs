using System;
using Server;

namespace Server.Items
{
	public class LongStemRose : Item
	{
		[Constructable]
		public LongStemRose() : base( 6377 )
		{
			if ( Utility.Random( 100 ) < 3 )
				Hue = Utility.RandomList( 1150, 1153, 1157, 1161, 1166 );

			Name = "a long stem rose";
			LootType = LootType.Blessed;
		}

		public LongStemRose( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1060662, "Valentines Day\t2006" );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}