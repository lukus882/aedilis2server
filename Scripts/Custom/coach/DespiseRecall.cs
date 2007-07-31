using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class DespiseRecall : RecallRune
	{
		[Constructable]
		public DespiseRecall() : base()
			{
			Weight = 1.0;	
			ItemID = 0x1F14;
			LootType = LootType.Blessed;
			Description = "Despise";
			Hue = 1150;
			Marked = true;
			Target = new Point3D(1298, 1080, 0);
			TargetMap = Map.Felucca;
			}

		public DespiseRecall( Serial serial ) : base( serial )
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