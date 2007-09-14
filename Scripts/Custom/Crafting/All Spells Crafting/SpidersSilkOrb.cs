using System;

namespace Server.Items
{
	public class SpidersSilkOrb : Item
	{
		[Constructable]
		public SpidersSilkOrb() : base( 0x186F )
		{
		      Weight = 1.0;
		      Name = "Essence of Spider's Silk";
            	}

		public SpidersSilkOrb( Serial serial ) : base( serial )
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