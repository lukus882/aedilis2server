using System;
using Server;

namespace Server.Items
{
	public class SkullGiftCandle : Item
	{
		[Constructable]
		public SkullGiftCandle() : base( 6232 )
		{
				
			Name = "a skull candle";
			LootType = LootType.Blessed;
		}

		public SkullGiftCandle( Serial serial ) : base( serial )
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

			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}