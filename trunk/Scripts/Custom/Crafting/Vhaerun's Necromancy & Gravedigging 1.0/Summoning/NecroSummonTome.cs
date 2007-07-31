using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{	
	public class NecroSummonTome : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefNecroSummon.CraftSystem; } }

		[Constructable]
		public NecroSummonTome() : base( 0x2253 )
		{
			Weight = 2.0;
			Hue = 1107;
			Name = "Necromantic Summoning Tome";
		}

		[Constructable]
		public NecroSummonTome( int uses ) : base( uses, 0x2253 )
		{
			Weight = 2.0;
			Name = "Necromantic Summoning Tome";
		}

		public NecroSummonTome( Serial serial ) : base( serial )
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