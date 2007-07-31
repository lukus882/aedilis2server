using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class VesperRecall : RecallRune
	{
		[Constructable]
		public VesperRecall() : base()
			{
			Weight = 1.0;	
			ItemID = 0x1F14;
			LootType = LootType.Blessed;
			Description = "Vesper";
			Hue = 1150;
			Marked = true;
			Target = new Point3D(2882, 788, 0);
			TargetMap = Map.Felucca;
			}

		public VesperRecall( Serial serial ) : base( serial )
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