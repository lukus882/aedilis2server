using System;

namespace Server.Items
{
	public class NightshadeOrb : Item
	{
		[Constructable]
		public NightshadeOrb() : base( 0x186B )
		{
		      Weight = 1.0;
		      Name = "Essence of Nightshade";
            	}

		public NightshadeOrb( Serial serial ) : base( serial )
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