using System;
using Server.Network;
using Server.Targeting;

namespace Server.Items
{
	public class TropicalFish : Item, ICarvable
	{
		public void Carve( Mobile from, Item item )
		{
			base.ScissorHelper( from, new RawFishSteak(), 1 );
		}

		[Constructable]
		public TropicalFish() : base( Utility.Random( 0x3B02, 7 ) )
		{
			Weight = 0.5;
			Name = "Tropical Fish";
		}

		

		public TropicalFish( Serial serial ) : base( serial )
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
