using System;
using Server.Network;

namespace Server.Items
{
	public class Seaweed : Item
	{
		[Constructable]
		public Seaweed() : base( Utility.Random( 0xDBA, 2 ) )
		{
			Name = "Seaweed";
		      	Weight = 1.0;
			Movable = true;
            	}

		public Seaweed( Serial serial ) : base( serial )
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

	public class Kelp : Item
	{
		[Constructable]
		public Kelp() : base( Utility.Random( 0xCAC, 9 ) )
		{
			Name = "Kelp";
		      	Weight = 1.0;
			Hue = 0x314;
			Movable = true;
            	}

		public Kelp( Serial serial ) : base( serial )
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

	public class NetPiece : Item
	{
		[Constructable]
		public NetPiece() : base( 0xDD5 )
		{
			Name = "Fishing Net Piece";
		      	Weight = 1.0;
			Movable = true;
            	}

		public NetPiece( Serial serial ) : base( serial )
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