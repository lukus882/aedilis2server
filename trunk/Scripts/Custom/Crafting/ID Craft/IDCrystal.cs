using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Items
{
	public class IDCrystal : Item
	{
		[Constructable]
		public IDCrystal() : base( 0x23B )
		{
			Name = "an Identification Crystal";
			Weight = 0.5; 
			Movable = true;
			Hue = 1196;
		}

		public IDCrystal( Serial serial ) : base( serial )
		{
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
