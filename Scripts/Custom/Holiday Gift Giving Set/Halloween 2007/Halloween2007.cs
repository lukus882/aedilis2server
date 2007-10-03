// Original Author Unknown
// Updated to be halloween 2007 by GreyWolf

using System;
using Server;
using Server.Items;

namespace Server.Misc
{
	public class Halloween2007 : GiftGiver
	{
		public static void Initialize()
		{
			GiftGiving.Register( new Halloween2007() );
		}

		public override DateTime Start{ get{ return new DateTime( 2007, 10, 20 ); } }
		public override DateTime Finish{ get{ return new DateTime( 2007, 11, 2 ); } }

		public override void GiveGift( Mobile mob )
		{
			HalloweenBag2007 bag = new HalloweenBag2007();

			bag.DropItem( new TrickOrTreatBag() );
			bag.DropItem( new TrickOrTreatBag() );
			bag.DropItem( new HalloweenOuiJaBoard2007() );
			bag.DropItem( new SkullGiftCandle() );
			bag.DropItem( new HalloweenMug() );

			bag.DropItem( new PumpkinBombS() );
			bag.DropItem( new PumpkinBombS() );
			bag.DropItem( new PumpkinBombS() );

			bag.DropItem( new PumpkinBombM() );
			bag.DropItem( new PumpkinBombM() );
			bag.DropItem( new PumpkinBombM() );

			bag.DropItem( new PumpkinBombL() );
			bag.DropItem( new PumpkinBombL() );
			bag.DropItem( new PumpkinBombL() );

			bag.DropItem( new SmellyPumpkinBombS() );
			bag.DropItem( new SmellyPumpkinBombS() );
			bag.DropItem( new SmellyPumpkinBombS() );

			bag.DropItem( new SmellyPumpkinBombM() );
			bag.DropItem( new SmellyPumpkinBombM() );
			bag.DropItem( new SmellyPumpkinBombM() );

			bag.DropItem( new SmellyPumpkinBombL() );
			bag.DropItem( new SmellyPumpkinBombL() );
			bag.DropItem( new SmellyPumpkinBombL() );

			switch ( Utility.Random( 4 ) )
			{      	
				case 0: bag.DropItem(new HalloweenCloak2007()); break;
				case 1: bag.DropItem(new HalloweenTunic2007()); break;
				case 2: bag.DropItem(new HalloweenDoublet2007()); break;
				case 3: bag.DropItem(new HalloweenBoots2007()); break;
			}


			switch ( GiveGift( mob, bag ) )
			{
				case GiftResult.Backpack:
					mob.SendMessage( 0x482, "Happy Halloween! Gift items have been placed in your backpack." );
					break;
				case GiftResult.BankBox:
					mob.SendMessage( 0x482, "Happy Halloween! Gift items have been placed in your bank box." );
					break;
			}
		}
	}
}
