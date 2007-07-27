using System;
using Server.Items;

namespace Server.Items
{

	public class CorpseCrystalBall : Item
	{
		public CorpseCrystalBall() : base( 0xE30 )
		{
			Movable = true;
			Weight = 8.0;
		}

		public CorpseCrystalBall( Serial serial ) : base( serial )
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