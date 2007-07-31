using System;
using Server;

namespace Server.Items
{
	public class SkirtOfTheAmazon : LeatherSkirt
	{
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public SkirtOfTheAmazon()
		{
			Name = "leather skirt of the amazon";
			Hue = 1438;
			Attributes.BonusStr = 8;
			Attributes.WeaponDamage = 15;
			PhysicalBonus = 12;
		}

		public SkirtOfTheAmazon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( Hue == 0x54B )
				Hue = 0x6D1;
		}
	}
}