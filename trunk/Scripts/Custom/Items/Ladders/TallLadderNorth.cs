using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class LadderNorthAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get	{	return new LadderNorthAddonDeed(); }
		}

		[ Constructable ]
		public LadderNorthAddon()
		{
			
			AddComponent( new AddonComponent( 0x2FDF ), 0, 0, 0 );
		
		}

		public LadderNorthAddon( Serial serial ) : base( serial )
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

	public class LadderNorthAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get	{	return new LadderNorthAddon();	}
		}

		[Constructable]
		public LadderNorthAddonDeed()
		{
			Name = "Tall Ladder North";
			Hue = 2425;
			LootType = LootType.Blessed;
		}

		public LadderNorthAddonDeed( Serial serial ) : base( serial )
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
