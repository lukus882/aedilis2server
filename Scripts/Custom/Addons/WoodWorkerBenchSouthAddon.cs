using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class WoodWorkerBenchSouthAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new WoodWorkerBenchSouthAddonDeed();
			}
		}

		[ Constructable ]
		public WoodWorkerBenchSouthAddon()
		{
			AddComponent( new AddonComponent( 6647 ), -1, 0, 0 );
			AddComponent( new AddonComponent( 6645 ), 1, 0, 0 );
			AddComponent( new AddonComponent( 6646 ), 0, 0, 0 );

		}

		public WoodWorkerBenchSouthAddon( Serial serial ) : base( serial )
		{
			Name="woodworkers bench";
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class WoodWorkerBenchSouthAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new WoodWorkerBenchSouthAddon();
			}
		}

		[Constructable]
		public WoodWorkerBenchSouthAddonDeed()
		{
			Name = "woodworkers bench deed (south)";
		}

		public WoodWorkerBenchSouthAddonDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void	Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}