using System;
using Server;

namespace Server.Items
{
	public class MGCenterAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new MGCenterDeed(); } }

		[Constructable]
		public MGCenterAddon()
		{
			AddComponent( new AddonComponent( 0x750 ),  0, +2, 0 );
			AddComponent( new AddonComponent( 0x750 ),  0, +1, 0 );
			AddComponent( new AddonComponent( 0x750 ),  0,  0, 0 );
			AddComponent( new AddonComponent( 0x750 ),  0, -1, 0 );
			AddComponent( new AddonComponent( 0x750 ),  0, -2, 0 );

			AddComponent( new AddonComponent( 0x750 ),  0, +2, 5 );
			AddComponent( new AddonComponent( 0x750 ),  0, +1, 5 );
			AddComponent( new AddonComponent( 0x750 ),  0,  0, 5 );
			AddComponent( new AddonComponent( 0x750 ),  0, -1, 5 );
			AddComponent( new AddonComponent( 0x750 ),  0, -2, 5 );

			AddComponent( new AddonComponent( 0x750 ),  0, +2, 10 );
			AddComponent( new AddonComponent( 0x750 ),  0, +1, 10 );
			AddComponent( new AddonComponent( 0x750 ),  0,  0, 10 );
			AddComponent( new AddonComponent( 0x750 ),  0, -1, 10 );
			AddComponent( new AddonComponent( 0x750 ),  0, -2, 10 );

			AddComponent( new AddonComponent( 0x750 ),  0, +2, 15 );
			AddComponent( new AddonComponent( 0x750 ),  0, +1, 15 );
			AddComponent( new AddonComponent( 0x750 ),  0,  0, 15 );
			AddComponent( new AddonComponent( 0x750 ),  0, -1, 15 );
			AddComponent( new AddonComponent( 0x750 ),  0, -2, 15 );

			AddComponent( new AddonComponent( 0x750 ),  0, +2, 20 );
			AddComponent( new AddonComponent( 0x750 ),  0, +1, 20 );
			AddComponent( new AddonComponent( 0x750 ),  0,  0, 20 );
			AddComponent( new AddonComponent( 0x750 ),  0, -1, 20 );
			AddComponent( new AddonComponent( 0x750 ),  0, -2, 20 );

			AddComponent( new AddonComponent( 0x750 ),  0, +2, 25 );
			AddComponent( new AddonComponent( 0x750 ),  0, +1, 25 );
			AddComponent( new AddonComponent( 0x750 ),  0,  0, 25 );
			AddComponent( new AddonComponent( 0x750 ),  0, -1, 25 );
			AddComponent( new AddonComponent( 0x750 ),  0, -2, 25 );
///////////////////////

			AddComponent( new AddonComponent( 0x750 ),  +1, +2, 0 );
			AddComponent( new AddonComponent( 0x750 ),  +1, +1, 0 );
			AddComponent( new AddonComponent( 0x750 ),  +1,  0, 0 );
			AddComponent( new AddonComponent( 0x750 ),  +1, -1, 0 );
			AddComponent( new AddonComponent( 0x750 ),  +1, -2, 0 );

			AddComponent( new AddonComponent( 0x751 ),  +2, +2, 0 );
			AddComponent( new AddonComponent( 0x753 ),  +2, -2, 0 );

			AddComponent( new AddonComponent( 0x750 ),  +1, +2, 5 );
			AddComponent( new AddonComponent( 0x750 ),  +1, -2, 5 );

			AddComponent( new AddonComponent( 0x750 ),  +1, +2, 10 );
			AddComponent( new AddonComponent( 0x750 ),  +1, -2, 10 );

			AddComponent( new AddonComponent( 0x750 ),  +1, +2, 15 );
			AddComponent( new AddonComponent( 0x750 ),  +1, -2, 15 );

			AddComponent( new AddonComponent( 0x750 ),  +1, +2, 20 );
			AddComponent( new AddonComponent( 0x750 ),  +1, -2, 20 );

			AddComponent( new AddonComponent( 0x750 ),  +1, +2, 25 );
			AddComponent( new AddonComponent( 0x750 ),  +1, -2, 25 );

			AddComponent( new AddonComponent( 0x750 ),  +1, +1, 25 );
			AddComponent( new AddonComponent( 0x750 ),  +1,  0, 25 );
			AddComponent( new AddonComponent( 0x750 ),  +1, -1, 25 );

			AddComponent( new AddonComponent( 0x750 ),  +2, +1, 0 );
			AddComponent( new AddonComponent( 0x750 ),  +2,  0, 0 );
			AddComponent( new AddonComponent( 0x750 ),  +2, -1, 0 );

			AddComponent( new AddonComponent( 0x756 ),  +3, +2, 0 );
			AddComponent( new AddonComponent( 0x752 ),  +3, +1, 0 );
			AddComponent( new AddonComponent( 0x752 ),  +3,  0, 0 );
			AddComponent( new AddonComponent( 0x752 ),  +3, -1, 0 );
			AddComponent( new AddonComponent( 0x757 ),  +3, -2, 0 );
/////////////////////////////
			AddComponent( new AddonComponent( 0x750 ),  0, +1, 30 );
			AddComponent( new AddonComponent( 0x750 ),  0,  0, 30 );
			AddComponent( new AddonComponent( 0x750 ),  0, -1, 30 );

			AddComponent( new AddonComponent( 0x750 ),  +1, +1, 30 );
			AddComponent( new AddonComponent( 0x750 ),  +1,  0, 30 );
			AddComponent( new AddonComponent( 0x750 ),  +1, -1, 30 );

			AddComponent( new AddonComponent( 0x750 ),  0,  0, 35 );

			AddComponent( new AddonComponent( 0x750 ),  +1,  0, 35 );
////////////////////////////////////////////////////
			AddComponent( new AddonComponent( 0x751 ),  +1, +1, 35 );
			AddComponent( new AddonComponent( 0x753 ),  +1, -1, 35 );

			AddComponent( new AddonComponent( 0x751 ),  0, +1, 35 );
			AddComponent( new AddonComponent( 0x753 ),  0, -1, 35 );

			AddComponent( new AddonComponent( 0x751 ),  0, +2, 30 );
			AddComponent( new AddonComponent( 0x753 ),  0, -2, 30 );

			AddComponent( new AddonComponent( 0x751 ),  +1, +2, 30 );
			AddComponent( new AddonComponent( 0x753 ),  +1, -2, 30 );
		}

		public MGCenterAddon( Serial serial ) : base( serial )
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

	public class MGCenterDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new MGCenterAddon(); } }

		[Constructable]
		public MGCenterDeed()
		{
			Name = "Travel Center Deed";
		}

		public MGCenterDeed( Serial serial ) : base( serial )
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