using System;
using Server.Items;

namespace Server.Items
{
	public class PureWhiteFeather : Item, ICommodity
	{
		string ICommodity.Description
		{
			get
			{
				return String.Format( Amount == 1 ? "{0} pure white feather" : "{0} pure white feathers", Amount );
			}
		}

		[Constructable]
		public PureWhiteFeather() : this( 1 )
		{
		}

		[Constructable]
		public PureWhiteFeather( int amount ) : base( 0x1BD1 )
		{
			Stackable = true;
			Weight = 0.1;
		    Hue = 1153;
			Amount = amount;
		}

		public PureWhiteFeather( Serial serial ) : base( serial )
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
