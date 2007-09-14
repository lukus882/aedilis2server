using System;

namespace Server.Items
{
	public class GinsengOrb : Item
	{
		[Constructable]
		public GinsengOrb() : base( 0x186A )
		{
		      Weight = 1.0;
		      Name = "Essence of Ginseng";
            	}

		public GinsengOrb( Serial serial ) : base( serial )
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