using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class NewbieDisplayCasesouth : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new NewbieDisplayCaseDeed(); } }

		[Constructable]
		public NewbieDisplayCasesouth()
		{
            AddComponent(new AddonComponent(0xb02), 0, 0, 0);
            AddComponent(new AddonComponent(0xb01), 1, 0, 0);
            AddComponent(new AddonComponent(0xb00), 2, 0, 0);
            AddComponent(new AddonComponent(0xaff), 0, 0, 2);
            AddComponent(new AddonComponent(0xafe), 1, 0, 2);
            AddComponent(new AddonComponent(0xafd), 2, 0, 2);
            		}

        public NewbieDisplayCasesouth(Serial serial)
            : base(serial)
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

	public class NewbieDisplayCaseDeed : BaseAddonDeed
	{
        public override BaseAddon Addon { get { return new NewbieDisplayCasesouth(); } }
		
		[Constructable]
		public NewbieDisplayCaseDeed()
		{
		}

        public NewbieDisplayCaseDeed(Serial serial)
            : base(serial)
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
