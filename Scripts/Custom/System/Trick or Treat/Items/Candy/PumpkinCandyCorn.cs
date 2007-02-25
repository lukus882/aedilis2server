using System;
using System.Collections;
using Server;
using Server.Network;

namespace Server.Items
{
	public class PumpkinCandyCorn : Food
	{

		[Constructable]
		public PumpkinCandyCorn() : this( 1 )
		{
		}
		
		[Constructable]
		public PumpkinCandyCorn( int amount ) : base( 0xF8F, amount )
		{
			this.Stackable = true;
			this.Name = "a pumpkin candycorn";
			this.ItemID = 3180;
			this.Amount = amount;
			this.Weight = 1;
			this.FillFactor = 1;
		}

		public PumpkinCandyCorn( Serial serial ) : base( serial )
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