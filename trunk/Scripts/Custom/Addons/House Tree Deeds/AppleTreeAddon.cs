using System;
using Server;

namespace Server.Items
{
	public class AppleTreeAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new AppleTreeDeed(); } }

		[Constructable]
		public AppleTreeAddon()
		{
			AddComponent( new AddonComponent( 0xD94 ), 0, 0, 0 );
			AddComponent( new AddonComponent( 0xD96 ), 0, 0, 0 );
		}

		public AppleTreeAddon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	public class AppleTreeDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new AppleTreeAddon(); } }
		//public override int LabelNumber{ get{ return 1076269; } } // Apple Tree

		[Constructable]
		public AppleTreeDeed() 
		{
			Name = "Apple Tree Deed";
			Hue = 32;
		}

		public AppleTreeDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}
