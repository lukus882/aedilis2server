using System;
using Server;

namespace Server.Items
{
	public class PinkChampagne : BeverageBottle
	{
		[Constructable]
		public PinkChampagne() : base( BeverageType.Ale )
		{
			Hue = Utility.RandomList( 35, 38, 1166 );
			Name = "pink champagne";
			LootType = LootType.Blessed;
		}

		public PinkChampagne( Serial serial ) : base( serial )
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