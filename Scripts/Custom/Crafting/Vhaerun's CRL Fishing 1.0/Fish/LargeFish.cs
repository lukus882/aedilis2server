using System;
using Server.Network;
using Server.Targeting;

namespace Server.Items
{
	public class Tuna : Item, ICarvable
	{
		public void Carve( Mobile from, Item item )
		{
			base.ScissorHelper( from, new RawFishSteak(), 4 );
		}

		[Constructable]
		public Tuna() : this( 1 )
		{
		}

		[Constructable]
		public Tuna( int amount ) : base( 0x09CC )
		{
			Name = "Tuna";
			Stackable = true;
			Weight = 1.0;
			Amount = amount;
		}

		

		public Tuna( Serial serial ) : base( serial )
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

	public class Cod : Item, ICarvable
	{
		public void Carve( Mobile from, Item item )
		{
			base.ScissorHelper( from, new RawFishSteak(), 4 );
		}

		[Constructable]
		public Cod() : this( 1 )
		{
		}

		[Constructable]
		public Cod( int amount ) : base( 0x09CD )
		{
			Name = "Cod";
			Stackable = true;
			Weight = 1.0;
			Amount = amount;
		}

		

		public Cod( Serial serial ) : base( serial )
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

	public class Sturgeon : Item, ICarvable
	{
		public void Carve( Mobile from, Item item )
		{
			base.ScissorHelper( from, new RawFishSteak(), 3 );
		}

		[Constructable]
		public Sturgeon() : this( 1 )
		{
		}

		[Constructable]
		public Sturgeon( int amount ) : base( 0x09CF )
		{
			Name = "Sturgeon";
			Stackable = true;
			Weight = 1.0;
			Amount = amount;
		}

		

		public Sturgeon( Serial serial ) : base( serial )
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

	public class Swordfish : Item, ICarvable
	{
		public void Carve( Mobile from, Item item )
		{
			base.ScissorHelper( from, new RawFishSteak(), 2 );
		}

		[Constructable]
		public Swordfish() : this( 1 )
		{
		}

		[Constructable]
		public Swordfish( int amount ) : base( 0x09CE )
		{
			Name = "Swordfish";
			Stackable = true;
			Weight = 1.0;
			Amount = amount;
		}

		

		public Swordfish( Serial serial ) : base( serial )
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
