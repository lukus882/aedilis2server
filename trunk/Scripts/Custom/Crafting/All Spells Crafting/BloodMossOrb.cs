using System;

namespace Server.Items
{
	public class BloodMossOrb : Item
	{
		[Constructable]
		public BloodMossOrb() : base( 0x186E )
		{
		      Weight = 1.0;
		      Name = "Essence of BloodMoss";
            	}

		public BloodMossOrb( Serial serial ) : base( serial )
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