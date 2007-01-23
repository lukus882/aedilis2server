using System;
using Server;
using Server.Items;

namespace Server.Misc
{
	public class Halloween2006 : GiftGiver
	{
		public static void Initialize()
		{
			GiftGiving.Register( new Halloween2006() );
		}

		public override DateTime Start{ get{ return new DateTime( 2006, 8, 16 ); } }
		public override DateTime Finish{ get{ return new DateTime( 2006, 8, 30 ); } }

		public override void GiveGift( Mobile mob )
		{
			HalloweenBag bag = new HalloweenBag();
		

			bag.DropItem( new ChocolateBar() );
			bag.DropItem( new HalloweenMug() );
			bag.DropItem( new HalloweenScarecrow() );
			bag.DropItem( new HalloweenSkull() );
			bag.DropItem( new JackOLantern() );
			bag.DropItem( new HalloweenBlood() );

			SkullGiftCandle candle = new SkullGiftCandle();
			candle.Name = "Happy Halloween from " + mob.Name;
			bag.DropItem( candle );


			switch ( GiveGift( mob, bag ) )
			{
				case GiftResult.Backpack:
					mob.SendMessage( 0x482, "Happy Halloween from the team!  Gift items have been placed in your backpack." );
					break;
				case GiftResult.BankBox:
					mob.SendMessage( 0x482, "Happy Halloween from the team!  Gift items have been placed in your bank box." );
					break;
			}
		}
	}
}