using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class FruitCake : Food
	{
		[Constructable]
		public FruitCake() : base( 0x1044 )
		{
                  Name = "Fruitcake";
                  Hue = 0x485;
			Weight = 1.0;
			LootType = LootType.Blessed;
		}

		public FruitCake( Serial serial ) : base( serial )
		{
		}
			public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1060662, "Seasons Greetings\t2006" );
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

	public class Spam : Food
	{
		[Constructable]
		public Spam() : base( 0x1044 )
		{
                  Name = "Spam";
			Weight = 1.0;
			LootType = LootType.Blessed;
		}

		public Spam( Serial serial ) : base( serial )
		{
		}
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1060662, "You Were Naughty This Year\t2006" );
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