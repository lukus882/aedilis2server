using System;
using Server;
using Server.Items;

namespace Server.Misc
{
	public class Thanksgiving2006 : GiftGiver
	{
		public static void Initialize()
		{
			GiftGiving.Register( new Thanksgiving2006() );
		}

		public override DateTime Start{ get{ return new DateTime( 2006, 8, 18 ); } }
		public override DateTime Finish{ get{ return new DateTime( 2006, 12, 12 ); } }

		public override void GiveGift( Mobile mob )
		{
			ThanksgivingBasket basket = new ThanksgivingBasket();
		

			basket.DropItem( new ThanksgivingBasket() );
			basket.DropItem( new TurkeyCook() );
			basket.DropItem( new TurkeyIdol() );
			basket.DropItem( new TurkeyStatue() );
                        basket.DropItem( new TheHunter() );
			
			ThanksgivingCard card = new ThanksgivingCard();
			card.Name = "Happy Thanksgiving from " + mob.Name;
			basket.DropItem( card );


			


			switch ( GiveGift( mob, basket ) )
			{
				case GiftResult.Backpack:
					mob.SendMessage( 0x482, "Happy 300th Anniversary from the team!  Gift items have been placed in your backpack." );
					break;
				case GiftResult.BankBox:
					mob.SendMessage( 0x482, "Happy 300th Anniversary from the team!  Gift items have been placed in your bank box." );
					break;
			}
		}
	}
}