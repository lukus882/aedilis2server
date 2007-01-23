using System;
using Server;
using Server.Items;

namespace Server.Misc
{
	public class MondainAnn : GiftGiver
	{
		public static void Initialize()
		{
			GiftGiving.Register( new MondainAnn() );
		}

		public override DateTime Start{ get{ return new DateTime( 2006, 8, 18 ); } }
		public override DateTime Finish{ get{ return new DateTime( 2006, 8, 30 ); } }

		public override void GiveGift( Mobile mob )
		{
			MondainBag bag = new MondainBag();
		

			bag.DropItem( new Jaana() );
			bag.DropItem( new AskAndAnswer() );
			bag.DropItem( new MondainPlate() );
			bag.DropItem( new MondainCoin() );


			


			switch ( GiveGift( mob, bag ) )
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