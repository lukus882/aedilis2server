using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class MarkOfGodsScroll : SpellScroll
	{
		[Constructable]
		public MarkOfGodsScroll() : this( 1 )
		{
		}

		[Constructable]
		public MarkOfGodsScroll( int amount ) : base( 211, 8815, amount )
		{
			Name="Mark Of Gods";
			Hue = 1150;
		}

		public MarkOfGodsScroll( Serial serial ) : base( serial )
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
