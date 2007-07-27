using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class CovetousRecall : RecallRune
	{
		[Constructable]
		public CovetousRecall() : base()
			{
			Weight = 1.0;	
			ItemID = 0x1F14;
			LootType = LootType.Blessed;
			Description = "Covetous";
			Hue = 1150;
			Marked = true;
			Target = new Point3D(2499, 919, 0);
			TargetMap = Map.Felucca;
			}

		public CovetousRecall( Serial serial ) : base( serial )
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