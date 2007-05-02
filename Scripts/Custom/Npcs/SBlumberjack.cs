using System;
using System.Collections;
using Server.Items;

namespace Server.Mobiles
{
	public class SBLumberJack: SBInfo
	{
		private ArrayList m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBLumberJack()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override ArrayList BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : ArrayList
		{
			public InternalBuyInfo()
			{

				Add( new GenericBuyInfo( "In Pack Container Renaming Rune", typeof( BagRenaming ), 600, 20, 0x1F14, 0 ) );
                                Add( new GenericBuyInfo( "In House Container Renaming Rune", typeof( BagRenaming2 ), 800, 20, 0x1F14, 0 ) );
                                Add( new GenericBuyInfo( "Seed Box", typeof( SeedBox ), 5000, 20, 0xE41, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{

Add( typeof( Board ), 3 );
Add( typeof( Log ), 6 );



			}
		}
	}
}