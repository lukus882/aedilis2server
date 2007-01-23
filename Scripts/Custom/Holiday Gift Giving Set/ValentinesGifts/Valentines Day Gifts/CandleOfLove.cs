using System;
using Server;

namespace Server.Items
{
	public class CandleOfLove : Item
	{
		[Constructable]
		public CandleOfLove() : base( 7188 )
		{
			LootType = LootType.Blessed;
			Light = LightType.Circle150;
		}

		public CandleOfLove( Serial serial ) : base( serial )
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