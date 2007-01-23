
using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class HeartShapedRugSouthAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new HeartShapedRugSouthAddonDeed();
			}
		}

		[ Constructable ]
		public HeartShapedRugSouthAddon()
		{
			AddonComponent ac = null;
			ac = new AddonComponent( 6013 );
			ac.Hue = 1166;
			AddComponent( ac, -2, -2, 0 );
			ac = new AddonComponent( 6013 );
			ac.Hue = 1166;
			AddComponent( ac, -1, -2, 0 );
			ac = new AddonComponent( 6013 );
			ac.Hue = 1166;
			AddComponent( ac, 1, -2, 0 );
			ac = new AddonComponent( 6013 );
			ac.Hue = 1166;
			AddComponent( ac, 2, -2, 0 );
			ac = new AddonComponent( 6022 );
			ac.Hue = 1166;
			AddComponent( ac, 0, -2, 0 );
			ac = new AddonComponent( 6025 );
			ac.Hue = 1166;
			AddComponent( ac, 0, -2, 0 );
			ac = new AddonComponent( 6025 );
			ac.Hue = 1166;
			AddComponent( ac, -2, -3, 0 );
			ac = new AddonComponent( 6025 );
			ac.Hue = 1166;
			AddComponent( ac, 1, -3, 0 );
			ac = new AddonComponent( 6022 );
			ac.Hue = 1166;
			AddComponent( ac, -1, -3, 0 );
			ac = new AddonComponent( 6022 );
			ac.Hue = 1166;
			AddComponent( ac, 2, -3, 0 );
			ac = new AddonComponent( 6025 );
			ac.Hue = 1166;
			AddComponent( ac, -3, -2, 0 );
			ac = new AddonComponent( 6019 );
			ac.Hue = 1166;
			AddComponent( ac, -4, -1, 0 );
			ac = new AddonComponent( 6013 );
			ac.Hue = 1166;
			AddComponent( ac, -3, -1, 0 );
			ac = new AddonComponent( 6013 );
			ac.Hue = 1166;
			AddComponent( ac, -2, -1, 0 );
			ac = new AddonComponent( 6013 );
			ac.Hue = 1166;
			AddComponent( ac, -1, -1, 0 );
			ac = new AddonComponent( 6013 );
			ac.Hue = 1166;
			AddComponent( ac, 0, -1, 0 );
			ac = new AddonComponent( 6013 );
			ac.Hue = 1166;
			AddComponent( ac, 1, -1, 0 );
			ac = new AddonComponent( 6013 );
			ac.Hue = 1166;
			AddComponent( ac, 2, -1, 0 );
			ac = new AddonComponent( 6013 );
			ac.Hue = 1166;
			AddComponent( ac, -2, 0, 0 );
			ac = new AddonComponent( 6013 );
			ac.Hue = 1166;
			AddComponent( ac, -1, 0, 0 );
			ac = new AddonComponent( 6013 );
			ac.Hue = 1166;
			AddComponent( ac, 0, 0, 0 );
			ac = new AddonComponent( 6013 );
			ac.Hue = 1166;
			AddComponent( ac, 1, 0, 0 );
			ac = new AddonComponent( 6013 );
			ac.Hue = 1166;
			AddComponent( ac, 2, 0, 0 );
			ac = new AddonComponent( 6013 );
			ac.Hue = 1166;
			AddComponent( ac, -1, 1, 0 );
			ac = new AddonComponent( 6013 );
			ac.Hue = 1166;
			AddComponent( ac, 0, 1, 0 );
			ac = new AddonComponent( 6013 );
			ac.Hue = 1166;
			AddComponent( ac, 1, 1, 0 );
			ac = new AddonComponent( 6013 );
			ac.Hue = 1166;
			AddComponent( ac, 0, 2, 0 );
			ac = new AddonComponent( 6020 );
			ac.Hue = 1166;
			AddComponent( ac, 0, 3, 0 );
			ac = new AddonComponent( 6023 );
			ac.Hue = 1166;
			AddComponent( ac, 2, 1, 0 );
			ac = new AddonComponent( 6023 );
			ac.Hue = 1166;
			AddComponent( ac, 1, 2, 0 );
			ac = new AddonComponent( 6024 );
			ac.Hue = 1166;
			AddComponent( ac, -3, 0, 0 );
			ac = new AddonComponent( 6024 );
			ac.Hue = 1166;
			AddComponent( ac, -2, 1, 0 );
			ac = new AddonComponent( 6024 );
			ac.Hue = 1166;
			AddComponent( ac, -1, 2, 0 );
			ac = new AddonComponent( 6022 );
			ac.Hue = 1166;
			AddComponent( ac, 3, -2, 0 );
			ac = new AddonComponent( 6021 );
			ac.Hue = 1166;
			AddComponent( ac, 4, -1, 0 );
			ac = new AddonComponent( 6013 );
			ac.Hue = 1166;
			AddComponent( ac, 3, -1, 0 );
			ac = new AddonComponent( 6023 );
			ac.Hue = 1166;
			AddComponent( ac, 3, 0, 0 );

		}

		public HeartShapedRugSouthAddon( Serial serial ) : base( serial )
		{
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

	public class HeartShapedRugSouthAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new HeartShapedRugSouthAddon();
			}
		}

		[Constructable]
		public HeartShapedRugSouthAddonDeed()
		{
			Name = "a deed for a heart shaped shag rug (South)";
			Hue = 1166;
			LootType = LootType.Blessed;
		}

		public HeartShapedRugSouthAddonDeed( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1060662, "Valentines Day\t2006" );
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