using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class Eggnog : Item
	{
		[Constructable]
		public Eggnog() : base( 0x99F )
		{
                  Name = "A Bottle Of Eggnog";
			Weight = 1.0;
			LootType = LootType.Blessed;
		}

		public Eggnog( Serial serial ) : base( serial )
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

	public class Champagne : Item
	{
		[Constructable]
		public Champagne() : base( 0x99B )
		{
                  Name = "A Bottle Of Champagne";
			Weight = 1.0;
			LootType = LootType.Blessed;
		}

		public Champagne( Serial serial ) : base( serial )
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