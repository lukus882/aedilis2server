using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	public class StichingKit : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefStiching.CraftSystem; } }

		[Constructable]
		public StichingKit() : base( 0xDF6 )
		{
			Weight = 2.0;
			Name = "Stiching Kit";
		}

        public StichingKit(Serial serial) : base(serial)
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