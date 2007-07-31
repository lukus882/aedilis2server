using System;
using Server;

namespace Server.Items
{
	public class HolyHammerOfExorcism : HammerPick
	{
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public HolyHammerOfExorcism()
		{
			Name = "holy hammer of exorcism";
			Hue = 1153;

			if ( Utility.Random( 100 ) < 50 )
				Slayer = SlayerName.DaemonDismissal;
			else
				Slayer = SlayerName.Silver;

			Attributes.WeaponDamage = 50;
			Attributes.WeaponSpeed = 35;
		}

		public HolyHammerOfExorcism( Serial serial ) : base( serial )
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

			if ( Slayer == SlayerName.None )
				Slayer = SlayerName.DaemonDismissal;
		}
	}
}