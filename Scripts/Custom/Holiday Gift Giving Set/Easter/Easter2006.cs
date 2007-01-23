using System;
using Server;
using Server.Items;

namespace Server.Misc
{
	public class Easter2006 : GiftGiver
	{
		public static void Initialize()
		{
			GiftGiving.Register( new Easter2006() );
		}

		public override DateTime Start{ get{ return new DateTime( 2006, 8, 18 ); } }
		public override DateTime Finish{ get{ return new DateTime( 2006, 8, 30 ); } }

		public override void GiveGift( Mobile mob )
		{
			EasterBasket basket = new EasterBasket();
		

			basket.DropItem( new EasterCarrot() );
			basket.DropItem( new ChocolateEasterBunny() );
			basket.DropItem( new BunnyIdol() );
			basket.DropItem( new DeadRabbit() );
			
			EasterCard card = new EasterCard();
			card.Name = "Happy Easter from " + mob.Name;
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