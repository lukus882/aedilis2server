using System;
using System.Collections;
using Server;
using Server.Network;

namespace Server.Items
{
	public class CandyHeart : Food
	{

		[Constructable]
		public CandyHeart() : this( 1 )
		{
		}
		
		[Constructable]
		public CandyHeart( int amount ) : base( amount, 0xF8F  )
		{
			this.Stackable = true;
			this.Name = "a candy heart";
			this.ItemID = 587;
			this.Amount = amount;
			this.Weight = 1;
			this.FillFactor = 1;
		}

		public CandyHeart( Serial serial ) : base( serial )
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