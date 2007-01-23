using System;
using System.IO;
using System.Collections;
using Server.Items;
using Server.Gumps;

namespace Server.Mobiles
{
	public class Guide : BaseGuide
	{
		[Constructable]
		public Guide()
		{
			// Properties
			path = "Data/guide.txt";
			Name = "Game Guide";
			Direction = Direction.Left;
			

			// Clothing
			AddItem( new GuideRobe());
			AddItem( new Boots(1175));
			AddItem( new Vandyke(1175));
			AddItem( new ShortHair(1175));
		}

		public Guide( Serial serial ) : base( serial )
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
