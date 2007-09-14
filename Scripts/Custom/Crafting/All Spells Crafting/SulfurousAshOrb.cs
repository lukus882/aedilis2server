using System;

namespace Server.Items
{
	public class SulfurousAshOrb : Item
	{
		[Constructable]
		public SulfurousAshOrb() : base( 0x1870 )
		{
		      Weight = 1.0;
		      Name = "Essence of Sulfurous";
            	}

		public SulfurousAshOrb( Serial serial ) : base( serial )
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