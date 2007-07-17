using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x8A6, 0x8A5 )]
	public class RopeLadder : Item
	{

		[Constructable]
		public RopeLadder() : base( 0x8A6 )
		{
			Weight = 5.0;
		}

		public RopeLadder( Serial serial ) : base( serial )
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