using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class WormSilk : Item
	{
		
		[Constructable]
		public WormSilk() : this( 1 )
		{
		}

		[Constructable]
		public WormSilk( int amount ) : base( 0xF8D)
		{
			Name ="Worm Silk";
			Weight = 1.0;
			Stackable = true;
			Amount = amount;
			Hue = 1810;
		}

		public WormSilk( Serial serial ) : base( serial )
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
