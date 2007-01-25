using System;
using Server;
using Server.Engines.Harvest;

namespace Server.Items
{
	public class EnchantedShovel : BaseHarvestTool
	{
		public override int LabelNumber{ get{ return 1045125; } } // sturdy shovel
		public override HarvestSystem HarvestSystem{ get{ return Mining.System; } }

		[Constructable]
		public EnchantedShovel() : this( 1000000 )
		{
		}

		[Constructable]
		public EnchantedShovel( int uses ) : base( uses, 0xF39 )
		{
			Name = "Enchanted Shovel";
			Weight = 5.0;
			Hue = 1581;
		}
		
		public EnchantedShovel( Serial serial ) : base( serial )
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