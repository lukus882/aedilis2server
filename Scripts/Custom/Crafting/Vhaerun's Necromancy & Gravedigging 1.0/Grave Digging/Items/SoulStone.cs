using System;

namespace Server.Items
{
	public class SoulGem : Item
	{
		[Constructable]
		public SoulGem() : base( 0x1EA7 )
		{
		      	Weight = 0.5;
			Name = "soul gem";
            	}

		public SoulGem( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
            	}
	}
}