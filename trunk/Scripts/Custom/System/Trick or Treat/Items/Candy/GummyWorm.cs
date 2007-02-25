using System;
using System.Collections;
using Server;
using Server.Network;

namespace Server.Items
{
	public class GummyWorm : Food
	{

		[Constructable]
		public GummyWorm() : this( 1 )
		{
		}
		
		[Constructable]
		public GummyWorm( int amount ) : base( 0xF8F, amount )
		{
			this.Stackable = true;
			this.Hue = 1271;
			this.Name = "a gummy worm";
			this.ItemID = 8446;
			this.Amount = amount;
			this.Weight = 1;
			this.FillFactor = 1;
		}

		public GummyWorm( Serial serial ) : base( serial )
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