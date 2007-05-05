using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	public class StitchingKit : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefStitching.CraftSystem; } }

		[Constructable]
		public StitchingKit() : base( 0xDF6 )
		{
			Weight = 2.0;
			Name = "Stitching Kit";
		}

        public StitchingKit(Serial serial) : base(serial)
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