using System;
using Server.Network;
using Server.Targeting;

namespace Server.Items
{
	public class CoilRope : Item
	{

		[Constructable]
		public CoilRope() : this( 1 )
		{
		}

		[Constructable]
		public CoilRope( int amount ) : base( 0x14F8 )
		{
			Stackable = true;
			Weight = 1.0;
			Amount = amount;
			Name = "Coil of Rope";
		}

		

		public CoilRope( Serial serial ) : base( serial )
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
