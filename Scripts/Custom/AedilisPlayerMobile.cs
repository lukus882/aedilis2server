using System;
using Server;

namespace Server.Mobiles
{
	public class AedilisPlayerMobile : PlayerMobile
	{
		public AedilisPlayerMobile() : base()
		{
		}

		public AedilisPlayerMobile( Serial s ) : base( s )
		{
		}
		

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
	}
}