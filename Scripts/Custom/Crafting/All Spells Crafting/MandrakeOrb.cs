using System;

namespace Server.Items
{
	public class MandrakeOrb : Item
	{
		[Constructable]
		public MandrakeOrb() : base( 0x186D )
		{
		      Weight = 1.0;
		      Name = "Essence of Mandrake Root";
            	}

		public MandrakeOrb( Serial serial ) : base( serial )
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