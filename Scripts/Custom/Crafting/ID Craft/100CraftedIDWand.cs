using System;
using Server;
using Server.Targeting;

namespace Server.Items
{
	public class CraftedIDWand100 : BaseWand
	{
		public override TimeSpan GetUseDelay{ get{ return TimeSpan.Zero; } }

		[Constructable]
		public CraftedIDWand100() : base( WandEffect.Identification, 25, 175 )
		{
			Charges = 100;
		}

		public CraftedIDWand100( Serial serial ) : base( serial )
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

		public override bool OnWandTarget( Mobile from, object o )
		{

						//if ( o is BaseClothing )
						//	((BaseClothing)o).Identified = true;
						if ( o is BaseJewel )
							((BaseJewel)o).Identified = true;
						else if ( o is BaseWeapon )
							((BaseWeapon)o).Identified = true;
						else if ( o is BaseArmor )
							((BaseArmor)o).Identified = true;

						if ( !Core.AOS )
							((Item)o).OnSingleClick( from );

			return ( o is Item );
		}
	}
}