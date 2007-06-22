using System;
using Server;

namespace Server.Items
{
	public class PeachTreeAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new PeachTreeDeed(); } }

		[Constructable]
		public PeachTreeAddon()
		{
			AddComponent( new AddonComponent( 0xD9C ), 0, 0, 0 );
			AddComponent( new AddonComponent( 0xD9E ), 0, 0, 0 );
		}

		public PeachTreeAddon( Serial serial ) : base( serial )
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

	public class PeachTreeDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new PeachTreeAddon(); } }
		//public override int LabelNumber{ get{ return 1076268; } } // Peach Tree

		[Constructable]
		public PeachTreeDeed() 
		{
			Name = "Peach Tree Deed";
			Hue = 49;
		}

		public PeachTreeDeed( Serial serial ) : base( serial )
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
