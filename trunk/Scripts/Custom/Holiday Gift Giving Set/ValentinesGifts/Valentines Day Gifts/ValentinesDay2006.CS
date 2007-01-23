using System;
using Server;
using Server.Items;

namespace Server.Misc
{
	public class ValentinesDay2006 : GiftGiver
	{
		public static void Initialize()
		{
			GiftGiving.Register( new ValentinesDay2006() );
		}

		public override DateTime Start{ get{ return new DateTime( 2006, 8, 16 ); } }
		public override DateTime Finish{ get{ return new DateTime( 2006, 8, 30 ); } }

		public override void GiveGift( Mobile mob )
		{
			GiftBox box = new GiftBox();
			box.Hue = Utility.RandomList( 38, 1166 );

			box.DropItem( new PinkChampagne() );
			box.DropItem( new ChampagneFlute() );
			box.DropItem( new CandleOfLove() );

			LongStemRose rose = new LongStemRose();
			rose.Name = "a whispering rose from " + mob.Name;
			box.DropItem( rose );

			int random = Utility.Random( 100 );

			if ( random < 30 )
				box.DropItem( new CupidsBow() );
			else
				box.DropItem( new CupidsHarp() );

			if ( random < 50 )
				box.DropItem( new HeartShapedRugEastAddonDeed() );
			else
				box.DropItem( new HeartShapedRugSouthAddonDeed() );

			switch ( GiveGift( mob, box ) )
			{
				case GiftResult.Backpack:
					mob.SendMessage( 0x482, "Happy Valentines Day from the team!  Gift items have been placed in your backpack." );
					break;
				case GiftResult.BankBox:
					mob.SendMessage( 0x482, "Happy Valentines Day from the team!  Gift items have been placed in your bank box." );
					break;
			}
		}
	}
}