using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class HalloweenScarecrow : Item
	{
		[Constructable]
		public HalloweenScarecrow() : base( 0x1E35 )
		{
                  Name = "Scarecrow";
			Weight = 1.0;
			LootType = LootType.Blessed;
		}

		public HalloweenScarecrow( Serial serial ) : base( serial )
		{
		}
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1060662, "Happy Halloween\t2006" );
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