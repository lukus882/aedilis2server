using System;

namespace Server.Items
{
	public class GarlicOrb : Item
	{
		[Constructable]
		public GarlicOrb() : base( 0x186C )
		{
		      Weight = 1.0;
		      Name = "Essence of Garlic";
            	}

		public GarlicOrb( Serial serial ) : base( serial )
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