using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class TheHunter : Food
	{
		[Constructable]
		public TheHunter() : base( 10096 )
		{
                  Name = "Milkman the Turkey Eater!";
                  Hue = Utility.RandomList(0x156, 0x21E, 0x71A);
			Weight = 1.0;
			LootType = LootType.Blessed;
		}

		public TheHunter( Serial serial ) : base( serial )
		{
		}
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1060662, "Happy Thanksgiving\t2006" );
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