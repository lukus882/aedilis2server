using System;

namespace Server.Items
{
	public class BlackPearlOrb : Item
	{
		[Constructable]
		public BlackPearlOrb() : base( 0x1869 )
		{
		      Weight = 1.0;
		      Name = "Essence of BlackPearl";
            	}

		public BlackPearlOrb( Serial serial ) : base( serial )
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