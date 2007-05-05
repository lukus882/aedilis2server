using System;
using System.Collections;
using Server.Items;

namespace Server.Mobiles
{
	public class SBStitchingMistress: SBInfo
	{
		private ArrayList m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBStitchingMistress()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override ArrayList BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : ArrayList
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( StitchingKit ), 10000, 20, 0xDF6, 0 ) ); 
				Add( new GenericBuyInfo( typeof( Dyes ), 8, 20, 0xFA9, 0 ) ); 
				Add( new GenericBuyInfo( typeof( DyeTub ), 9, 20, 0xFAB, 0 ) ); 
				Add( new GenericBuyInfo( typeof( BoltOfCloth ), 120, 20, 0xf95, 0 ) ); 
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( BoltOfCloth ), 60 );
			}
		}
	}
}