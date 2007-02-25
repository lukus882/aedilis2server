using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class HeavenlyLightScroll : SpellScroll
	{

		[Constructable]
		public HeavenlyLightScroll() : this( 1 )
		{
		}

		[Constructable]
		public HeavenlyLightScroll( int amount ) : base( 212, 8815, amount )
		{
			Name = "Heavenly Light";
			Hue = 1150;
		}

		public HeavenlyLightScroll( Serial ser ) : base(ser)
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