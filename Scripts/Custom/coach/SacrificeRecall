using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class SacrificeRecall : RecallRune
	{
		[Constructable]
		public SacrificeRecall() : base()
			{
			Weight = 1.0;	
			ItemID = 0x1F14;
			LootType = LootType.Blessed;
			Description = "Sacrifice";
			Hue = 1150;
			Marked = true;
			Target = new Point3D(3355, 290, 4);
			TargetMap = Map.Felucca;
			}

		public SacrificeRecall( Serial serial ) : base( serial )
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