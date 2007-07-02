using System;
using Server;
using Server.Items;

namespace Server.Misc
{
	public class July4th2007Gifter : GiftGiver
	{
		public static void Initialize()
		{
			GiftGiving.Register( new July4th2007Gifter() );
		}

		public override DateTime Start{ get{ return new DateTime( 2007, 7, 3 ); } }
		public override DateTime Finish{ get{ return new DateTime( 2007, 7, 10 ); } }

		public override void GiveGift( Mobile mob )
		{
			

			July4th2007Bag bag = new July4th2007Bag();
		
			bag.DropItem( new FireworksWand() );
			bag.DropItem( new LargeFireworksStand() );
			bag.DropItem( new LargeFireworksStand() );
			bag.DropItem( new SmallFireworksStand() );
			bag.DropItem( new SmallFireworksStand() );
			bag.DropItem( new July4th2007GiftDeed() );
			


			switch ( GiveGift( mob, bag ) )
			{
				case GiftResult.Backpack:
					mob.SendMessage( 0x482, "Happy 4th of July From The Aedilis Staff!  Gift items have been placed in your backpack." );
					break;
				case GiftResult.BankBox:
					mob.SendMessage( 0x482, "Happy 4th of July From The Aedilis Staff!  Gift items have been placed in your bank box." );
					break;
			}
		}
	}
}