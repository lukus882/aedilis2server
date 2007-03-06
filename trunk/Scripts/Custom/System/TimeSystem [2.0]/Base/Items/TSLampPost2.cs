using System;
using Server;

namespace Server.Items
{
	public class TSLampPost2 : TSBaseLight
	{
		public override int LitItemID{ get { return 0xB22; } }
		public override int UnlitItemID{ get { return 0xB23; } }
		
		[Constructable]
		public TSLampPost2() : base( 0xB23 )
		{
			Movable = false;
			Duration = TimeSpan.Zero; // Never burnt out
			Burning = false;
			Light = LightType.Circle300;
			Weight = 40.0;
            UseRandomLightOutage = true;
		}

		public TSLampPost2( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}