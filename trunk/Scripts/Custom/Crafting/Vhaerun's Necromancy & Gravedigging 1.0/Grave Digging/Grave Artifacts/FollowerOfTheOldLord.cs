using System;
using Server;

namespace Server.Items
{
	public class FollowerOfTheOldLord : BodySash
	{
		[Constructable]
		public FollowerOfTheOldLord()
		{
			Name = "follower of the old lord";
			Hue = 1150;
			Attributes.LowerManaCost = 8;
			Attributes.AttackChance = 15;
			Resistances.Fire = 12;
			Resistances.Energy = 9;
		}

		public FollowerOfTheOldLord( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}