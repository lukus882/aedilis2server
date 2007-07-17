using System; 
using System.Collections; 
using Server.Items; 

namespace Server.Mobiles 
{ 
	public class SBMasterFisherman : SBInfo 
	{ 
		private ArrayList m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBMasterFisherman() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override ArrayList BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : ArrayList 
		{ 
			public InternalBuyInfo() 
			{ 
				Add( new GenericBuyInfo( typeof( FishingPole ), 40, 5, 0xDC0, 0 ) );
				Add( new GenericBuyInfo( typeof( ScrimshawKnife ), 15, 5, 0x13F6, 0 ) );
				Add( new GenericBuyInfo( typeof( WhittlingKnife ), 15, 5, 0x13F6, 0 ) );
				Add( new GenericBuyInfo( typeof( FishBones ), 2, 20, 0x3B0C, 0 ) );
				Add( new GenericBuyInfo( typeof( CoilRope ), 10, 20, 0x14F8, 0 ) );
				Add( new GenericBuyInfo( typeof( FishingNet ), 450, 20, 0xDCA, 0 ) );
			} 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{ 
				Add( typeof( RawFishSteak ), 1 );
				Add( typeof( FishingPole ), 16 );
				Add( typeof( PrizedFish ), 4 );
				Add( typeof( WondrousFish ), 4 );
				Add( typeof( TrulyRareFish ), 4 );
				Add( typeof( PeculiarFish ), 5 );
				Add( typeof( BigFish ), 250 );
			} 
		} 
	} 
}