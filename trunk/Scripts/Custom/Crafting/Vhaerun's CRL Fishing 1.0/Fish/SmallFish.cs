using System;
using Server.Network;
using Server.Targeting;

namespace Server.Items
{
	public class Bluegill : Item, ICarvable
	{
		public void Carve( Mobile from, Item item )
		{
			base.ScissorHelper( from, new RawFishSteak(), 1 );
		}

		[Constructable]
		public Bluegill() : base( 0x3AFE )
		{
			Name = "Bluegill";
			Weight = 0.5;
		}

		

		public Bluegill( Serial serial ) : base( serial )
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

	public class Perch : Item, ICarvable
	{
		public void Carve( Mobile from, Item item )
		{
			base.ScissorHelper( from, new RawFishSteak(), 1 );
		}

		[Constructable]
		public Perch() : base( 0x3B01 )
		{
			Name = "Perch";
			Weight = 0.5;
		}

		

		public Perch( Serial serial ) : base( serial )
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

	public class Carp : Item, ICarvable
	{
		public void Carve( Mobile from, Item item )
		{
			base.ScissorHelper( from, new RawFishSteak(), 1 );
		}

		[Constructable]
		public Carp( int amount ) : base( 0x3B09 )
		{
			Name = "Carp";
			Weight = 0.5;
		}

		

		public Carp( Serial serial ) : base( serial )
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

	public class Catfish : Item, ICarvable
	{
		public void Carve( Mobile from, Item item )
		{
			base.ScissorHelper( from, new RawFishSteak(), 1 );
		}

		[Constructable]
		public Catfish( int amount ) : base( 0x3B09 )
		{
			Name = "Catfish";
			Weight = 0.5;
			Hue = 0x34B;
		}

		

		public Catfish( Serial serial ) : base( serial )
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
