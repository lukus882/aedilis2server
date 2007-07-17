using System;
using Server.Items;
using Server.Network;
using Server.Targeting;

namespace Server.Items
{
	public class FishBones : Item
	{

		[Constructable]
		public FishBones() : this( 1 )
		{
		}

		[Constructable]
		public FishBones( int amount ) : base( 0x3B0C )
		{
			Stackable = true;
			Weight = 0.2;
			Amount = amount;
			Name = "Fish Bones";
			Hue = 0x47E;
		}

		

		public FishBones( Serial serial ) : base( serial )
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
