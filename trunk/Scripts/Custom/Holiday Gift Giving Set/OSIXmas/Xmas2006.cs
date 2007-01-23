using System;
using Server;
using Server.Items;

namespace Server.Misc
{
	public class Xmas2006 : GiftGiver
	{
		public static void Initialize()
		{
			GiftGiving.Register( new Xmas2006() );
		}

		public override DateTime Start{ get{ return new DateTime( 2006, 8, 16 ); } }
		public override DateTime Finish{ get{ return new DateTime( 2006, 8, 30 ); } }

		public override void GiveGift( Mobile mob )
		{
			XmasBag bag = new XmasBag();
		

			bag.DropItem( new Eggnog() );
			bag.DropItem( new Champagne() );
			bag.DropItem( new BunchOfDates() );
			bag.DropItem( new FruitCake() );
			bag.DropItem( new PileSnow() );
			bag.DropItem( new RedChampagneGlass() );
			bag.DropItem( new GreenChampagneGlass() );
			bag.DropItem( new WristWatch() );

			SeasonsGreetings greetings = new SeasonsGreetings();
			greetings.Name = "Seasons Greetings from " + mob.Name;
			bag.DropItem( greetings );

			switch ( GiveGift( mob, bag ) )
			{
				case GiftResult.Backpack:
					mob.SendMessage( 0x482, "Seasons Greetings from the team!  Gift items have been placed in your backpack." );
					break;
				case GiftResult.BankBox:
					mob.SendMessage( 0x482, "Seasons Greetings from the team!  Gift items have been placed in your bank box." );
					break;
			}
		}
	}
}