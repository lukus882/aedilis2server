using System;
using Server;

namespace Server.Items
{
	public class CupidsHarp : LapHarp
	{
		public override int InitMinUses{ get{ return 1600; } }
		public override int InitMaxUses{ get{ return 1600; } }

		[Constructable]
		public CupidsHarp()
		{
			Name = "cupids's harp";
			Hue = Utility.RandomList( 38, 1166 );
			Slayer = SlayerName.Silver;
			Slayer2 = SlayerName.Exorcism;
			LootType = LootType.Blessed;
		}

		public CupidsHarp( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1060662, "Valentines Day\t2006" );
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