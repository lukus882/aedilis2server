using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	public class IDCraftKit : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefIDCraft.CraftSystem; } }

		[Constructable]
		public IDCraftKit() : base( 0xE2D )
		{
			Weight = 2.0;
			Name = "ID IDCraftCraft Kit";
		}

        public IDCraftKit(Serial serial) : base(serial)
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