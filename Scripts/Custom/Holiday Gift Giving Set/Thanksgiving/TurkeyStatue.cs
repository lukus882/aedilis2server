using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class TurkeyStatue : Food
	{
		[Constructable]
		public TurkeyStatue() : base( 8430 )
		{
                  Name = "Turkey Statue";
			Weight = 1.0;
			LootType = LootType.Blessed;
		}

		public TurkeyStatue( Serial serial ) : base( serial )
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