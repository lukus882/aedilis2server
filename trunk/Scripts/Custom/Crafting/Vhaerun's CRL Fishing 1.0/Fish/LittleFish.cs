using System;
using Server.Network;
using Server.Targeting;

namespace Server.Items
{
	public class LittleFish : Item, ICarvable
	{
		public void Carve( Mobile from, Item item )
		{
			base.ScissorHelper( from, new RawFishSteak(), 1 );
		}

		[Constructable]
		public LittleFish() : base( 0x3B03 )
		{
			Name = "little fish";
			Weight = 0.2;
			Hue = 0x31C;
		}

		

		public LittleFish( Serial serial ) : base( serial )
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
