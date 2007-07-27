using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class WrongRecall : RecallRune
	{
		[Constructable]
		public WrongRecall() : base()
			{
			Weight = 1.0;	
			ItemID = 0x1F14;
			LootType = LootType.Blessed;
			Description = "Wrong";
			Hue = 1150;
			Marked = true;
			Target = new Point3D(2043, 238, 0);
			TargetMap = Map.Felucca;
			}

		public WrongRecall( Serial serial ) : base( serial )
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