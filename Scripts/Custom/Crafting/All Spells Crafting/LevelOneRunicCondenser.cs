using System;

namespace Server.Items
{
	public class LevelOneRunicCondenser : Item
	{
		[Constructable]
		public LevelOneRunicCondenser() : base( 0xEFC )
		{
		      Weight = 1.0;
		      Name = "a Level One Runic Condenser";
            	}

		public LevelOneRunicCondenser( Serial serial ) : base( serial )
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