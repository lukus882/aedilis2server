using System;
using Server;

namespace Server.Items
{
	public class WaterTubAddon : BaseAddon, IWaterSource
	{
		public override BaseAddonDeed Deed{ get{ return new WaterTubDeed(); } }

		[Constructable]
		public WaterTubAddon()
		{
			AddComponent( new AddonComponent( 0xE7B ), 0, 0, 0 );
		}

		public WaterTubAddon( Serial serial ) : base( serial )
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

		public int Quantity
		{
			get{ return 175; }
			set{}
		}
	}

	public class WaterTubDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new WaterTubAddon(); } }

		[Constructable]
		public WaterTubDeed()
		{
			Name = "Water Tub Deed";
		}

		public WaterTubDeed( Serial serial ) : base( serial )
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