using System;
using System.Collections;
using Server;
using Server.Network;

namespace Server.Items
{
	public class ChocolateSkeleton : Food
	{

		[Constructable]
		public ChocolateSkeleton() : this( 1 )
		{
		}
		
		[Constructable]
		public ChocolateSkeleton( int amount ) : base( 0xF8F, amount )
		{
			this.Stackable = true;
			this.Hue = 542;
			this.Name = "a chocolate skeleton";
			this.ItemID = 8423;
			this.Amount = amount;
			this.Weight = 1;
			this.FillFactor = 1;
		}

		public ChocolateSkeleton( Serial serial ) : base( serial )
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