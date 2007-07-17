using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x1EA3, 0x1EA4 )]
	public class SmallDecorativeNet : Item
	{

		[Constructable]
		public SmallDecorativeNet() : base( 0x1EA3 )
		{
			Weight = 7.0;
			Name = "a small decorative net";
		}

		public SmallDecorativeNet( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0x1EA6, 0x1EA5 )]
	public class LargeDecorativeNet : Item
	{

		[Constructable]
		public LargeDecorativeNet() : base( 0x1EA6 )
		{
			Weight = 10.0;
			Name = "a large decorative net";
		}

		public LargeDecorativeNet( Serial serial ) : base( serial )
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