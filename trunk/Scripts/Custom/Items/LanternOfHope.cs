using System;
using Server;

namespace Server.Items
{
	public class AedilisLantern : BaseEquipableLight
	{
		public override int LitItemID
		{
			get
			{
				if ( ItemID == 0xA15 || ItemID == 0xA17 )
					return ItemID;

				return 0xA22;
			}
		}

		public override int UnlitItemID
		{
			get
			{
				if ( ItemID == 0xA18 )
					return ItemID;

				return 0xA25;
			}
		}

		[Constructable]
		public AedilisLantern() : base( 0xA25 )
		{
			if ( Burnout )
				Duration = TimeSpan.FromMinutes( 60 );
			else
				Duration = TimeSpan.Zero;

			Burning = false;
			Light = LightType.Circle300;
              		LootType = LootType.Blessed;
			Weight = 5.0;
            		Name = "Lantern Of Hope";
			Hue = 311;
		}

		public AedilisLantern( Serial serial ) : base( serial )
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
