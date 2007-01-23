using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class WoodWorkerBenchEastAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new WoodWorkerBenchEastAddonDeed();
			}
		}

		[ Constructable ]
		public WoodWorkerBenchEastAddon()
		{
			AddComponent( new AddonComponent( 6642 ), 0, 0, 0 );
			AddComponent( new AddonComponent( 6641 ), 0, 1, 0 );
			AddComponent( new AddonComponent( 6643 ), 0, -1, 0 );

		}

		public WoodWorkerBenchEastAddon( Serial serial ) : base( serial )
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

	public class WoodWorkerBenchEastAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new WoodWorkerBenchEastAddon();
			}
		}

		[Constructable]
		public WoodWorkerBenchEastAddonDeed()
		{
			Name = "woodworkers bench deed (east)";
		}

		public WoodWorkerBenchEastAddonDeed( Serial serial ) : base( serial )
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