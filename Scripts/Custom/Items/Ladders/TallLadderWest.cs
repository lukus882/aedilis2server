using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class LadderWestAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get	{	return new LadderWestAddonDeed(); }
		}

		[ Constructable ]
		public LadderWestAddon()
		{
			
			AddComponent( new AddonComponent( 0x2FDE ), 0, 0, 0 );
		
		}

		public LadderWestAddon( Serial serial ) : base( serial )
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

	public class LadderWestAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get	{	return new LadderWestAddon();	}
		}

		[Constructable]
		public LadderWestAddonDeed()
		{
			Name = "Tall Ladder West";
			Hue = 2425;
			LootType = LootType.Blessed;
		}

		public LadderWestAddonDeed( Serial serial ) : base( serial )
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
