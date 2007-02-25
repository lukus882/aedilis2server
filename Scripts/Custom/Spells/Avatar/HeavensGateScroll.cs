using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class HeavensGateScroll : SpellScroll
	{
		[Constructable]
		public HeavensGateScroll() : this( 1 )
		{
		}

		[Constructable]
		public HeavensGateScroll( int amount ) : base( 210, 8815, amount )
		{
			Name = "Heavens Gate";
			Hue = 1150;
		}

		public HeavensGateScroll( Serial serial ) : base( serial )
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
