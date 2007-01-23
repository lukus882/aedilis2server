using System;
using Server;

namespace Server.Items
{
	public class DeveloperRobe : BaseSuit
	{
		[Constructable]
		public DeveloperRobe() : base( AccessLevel.Developer, 0xC, 0x204F ) // purple hue
		{
		}

		public DeveloperRobe( Serial serial ) : base( serial )
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