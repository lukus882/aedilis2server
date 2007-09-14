using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	public class RunicPowerTube : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefAllSpellCrafting.CraftSystem; } }

		[Constructable]
		public RunicPowerTube() : base( 0x194B )
		{
			Name = "A Runic Power Tube";
			Weight = 1.0;
			Hue = 1153;  
		}

		[Constructable]
		public RunicPowerTube( int uses ) : base( uses, 0x194B )
		{
			Weight = 1.0;
		}

		public RunicPowerTube( Serial serial ) : base( serial )
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