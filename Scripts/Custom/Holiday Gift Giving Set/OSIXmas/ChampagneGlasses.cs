using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class RedChampagneGlass : Item
	{
		[Constructable]
		public RedChampagneGlass() : base( 0x99A )
		{
                  Name = "Champagne Glass";
                  Hue = 0x43;
			Weight = 1.0;
			LootType = LootType.Blessed;
		}

		public RedChampagneGlass( Serial serial ) : base( serial )
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

	public class GreenChampagneGlass : Item
	{
		[Constructable]
		public GreenChampagneGlass() : base( 0x9CB )
		{
                  Name = "Champagne Glass";
                  Hue = 0x25;
			Weight = 1.0;
			LootType = LootType.Blessed;
		}

		public GreenChampagneGlass( Serial serial ) : base( serial )
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
}