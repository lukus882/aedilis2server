using System;
using Server.Network;
using Server.Targeting;

namespace Server.Items
{
	public class Shrimp : Item
	{

		[Constructable]
		public Shrimp() : this( 1 )
		{
		}

		[Constructable]
		public Shrimp( int amount ) : base( 0x3B14 )
		{
			Name = "shrimp";
			Stackable = true;
			Weight = 0.1;
			Amount = amount;
		}

		public Shrimp( Serial serial ) : base( serial )
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

	public class DecoSeaHorse : Item
	{
		[Constructable]
		public DecoSeaHorse() : base( 0x3B10 )
		{
		      	Weight = 0.5;
                  	Name = "sea horse";

            	}

		public DecoSeaHorse( Serial serial ) : base( serial )
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
		      
                  if ( Weight == 4.0 )
				Weight = 1.0;

            	}
	}

	public class SeaCrab : Item
	{
		[Constructable]
		public SeaCrab() : base( 0x262F )
		{
		      	Weight = 0.5;
                  	Name = "sea crab";
			Hue = 0x66D;

            	}

		public SeaCrab( Serial serial ) : base( serial )
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
		      
                  if ( Weight == 4.0 )
				Weight = 1.0;

            	}
	}

	public class Jellyfish : Item
	{
		[Constructable]
		public Jellyfish() : base( 0x3B0E )
		{
		      	Weight = 0.5;
                  	Name = "jellyfish";

            	}

		public Jellyfish( Serial serial ) : base( serial )
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
		      
                  if ( Weight == 4.0 )
				Weight = 1.0;

            	}
	}

	public class BrineShrimp : Item
	{
		[Constructable]
		public BrineShrimp() : this( 1 )
		{
		}

		[Constructable]
		public BrineShrimp( int amount ) : base( 0x3B11 )
		{
			Name = "brine shrimp";
			Stackable = true;
			Weight = 0.2;
			Amount = amount;
		}

		public BrineShrimp( Serial serial ) : base( serial )
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
		      
                  if ( Weight == 4.0 )
				Weight = 1.0;

            	}
	}
}
