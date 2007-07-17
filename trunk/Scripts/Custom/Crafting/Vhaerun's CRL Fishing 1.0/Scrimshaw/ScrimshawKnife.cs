using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	[FlipableAttribute( 0x13F6, 0x13F7 )]
	public class ScrimshawKnife : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefScrimshaw.CraftSystem; } }

		[Constructable]
		public ScrimshawKnife() : base( 0x13F6 )
		{
			Weight = 2.0;
			Layer = Layer.OneHanded;
			Name = "scrimshaw knife";
		}

		[Constructable]
		public ScrimshawKnife( int uses ) : base( uses, 0x13F6 )
		{
			Weight = 2.0;
			Layer = Layer.OneHanded;
			Name = "scrimshaw knife";
		}

		public ScrimshawKnife( Serial serial ) : base( serial )
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