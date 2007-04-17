using System;

namespace Server.Items
{
	public class IslandRock : Item
	{
		[Constructable]
		public IslandRock() : base( 0x3B0F )
		{
		      Weight = 0.0;
                  LootType = LootType.Blessed;

            }

		public IslandRock( Serial serial ) : base( serial )
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
		      
                  if ( Weight == 4.0 )
				Weight = 1.0;

            }
	}
}