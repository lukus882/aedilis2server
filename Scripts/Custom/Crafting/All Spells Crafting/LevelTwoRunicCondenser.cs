using System;

namespace Server.Items
{
	public class LevelTwoRunicCondenser : Item
	{
		[Constructable]
		public LevelTwoRunicCondenser() : base( 0xEFB )
		{
		      Weight = 1.0;
		      Name = "a Level Two Runic Condenser";
            	}

		public LevelTwoRunicCondenser( Serial serial ) : base( serial )
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