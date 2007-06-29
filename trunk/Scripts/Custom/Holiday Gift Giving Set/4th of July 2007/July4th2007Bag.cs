using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class July4th2007Bag : Container
	{
		[Constructable]
		public July4th2007Bag() : base( 0xE76 )
		{
                  Name = "4th of July Gift Bag";
                  Hue = 38;
	          Weight = 1.0;
		}

		public July4th2007Bag( Serial serial ) : base( serial )
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