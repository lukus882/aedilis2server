using System;
using Server;

namespace Server.Items
{
	public class WaterBarrelAddon : BaseAddon, IWaterSource
	{
		public override BaseAddonDeed Deed{ get{ return new WaterBarrelDeed(); } }

		[Constructable]
		public WaterBarrelAddon()
		{
			AddComponent( new AddonComponent( 0x154D ), 0, 0, 0 );
		}

		public WaterBarrelAddon( Serial serial ) : base( serial )
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
			get{ return 350; }
			set{}
		}
	}

	public class WaterBarrelDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new WaterBarrelAddon(); } }

		[Constructable]
		public WaterBarrelDeed()
		{
			Name = "Water Barrel Deed";
		}

		public WaterBarrelDeed( Serial serial ) : base( serial )
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