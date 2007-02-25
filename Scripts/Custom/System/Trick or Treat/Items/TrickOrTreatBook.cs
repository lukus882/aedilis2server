using System;
using Server;

namespace Server.Items
{
	public class TrickOrTreatBook : RedBook
	{
		[Constructable]
		public TrickOrTreatBook() : base( "Trick or Treat", "Staff", 3, false )
		{
			Hue = 0x4EA;

			Pages[0].Lines = new string[]
				{
					"   Happy Halloween!  ",
					" ",
					"  Here is a trick or",
					"treat goodie bag.",
					"  You can use this",
					"bag to go trick or",
					"treating at the",
					"town vendors."					
				};

			Pages[1].Lines = new string[]
				{
					"  To do this, simply.",
					"walk up to any vendor",
					"in any town and say",
					"'trick or treat'.  The",
					"vendor's will give you",
					"goodies and you may",
					"even receive a special",
					"halloween novelty item."
				};

			Pages[2].Lines = new string[]
				{
					"  Have Fun and Good",
					"Luck!"
				};
		}

		public TrickOrTreatBook( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}