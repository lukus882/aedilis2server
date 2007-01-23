using System;
using Server;
using Server.Items;

namespace Server.Misc
{
	public class Xmas22006 : GiftGiver
	{
		public static void Initialize()
		{
			GiftGiving.Register( new Xmas22006() );
		}

		public override DateTime Start{ get{ return new DateTime( 2006, 8, 16 ); } }
		public override DateTime Finish{ get{ return new DateTime( 2006, 8, 30 ); } }

		public override void GiveGift( Mobile mob )
		{
			NaughtyBag bag = new NaughtyBag();

			bag.DropItem( new Spam() );
			bag.DropItem( new Coal() );
			bag.DropItem( new Switches() );

			NaughtyCard card = new NaughtyCard();
			card.Name = "You were naughty this year from " + mob.Name;
			bag.DropItem( card );

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