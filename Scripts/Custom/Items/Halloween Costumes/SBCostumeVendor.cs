///////////////////////////////////////////////////
///// Created By Bad Karma aka Broadside///////////
///////////////////////////////////////////////////
using System; 
using System.Collections; 
using Server.Items; 

namespace Server.Mobiles 
{ 
	public class SBCostumeVendor : SBInfo 
	{ 
		private ArrayList m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBCostumeVendor() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override ArrayList BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : ArrayList 
		{ 
			public InternalBuyInfo() 
			{ 
				Add( new GenericBuyInfo( typeof( SkeletonCostume ), 5000, 50, 9860, 0 ) );
				Add( new GenericBuyInfo( typeof( BlackthorneCostume ), 5000, 50, 9860, 0 ) );
				Add( new GenericBuyInfo( typeof( LizardmanCostume ), 5000, 50, 9860, 0 ) );
				Add( new GenericBuyInfo( typeof( FanDancerCostume ), 5000, 50, 9860, 0 ) );
				Add( new GenericBuyInfo( typeof( FairyCostume ), 5000, 50, 9860, 0 ) );
				Add( new GenericBuyInfo( typeof( GargoyleCostume ), 5000, 50, 9860, 0 ) );
				Add( new GenericBuyInfo( typeof( MinionCostume ), 5000, 50, 9860, 0 ) );
				Add( new GenericBuyInfo( typeof( ImpCostume ), 5000, 50, 9860, 0 ) );
				Add( new GenericBuyInfo( typeof( KappaCostume ), 5000, 50, 9860, 0 ) );
				Add( new GenericBuyInfo( typeof( MongbatCostume ), 5000, 50, 9860, 0 ) );
				Add( new GenericBuyInfo( typeof( CentaurCostume ), 5000, 50, 9860, 0 ) );
				//Add( new GenericBuyInfo( typeof( ArcaneDemonCostume ), 5000, 50, 9860, 0 ) );
				Add( new GenericBuyInfo( typeof( CyclopsCostume ), 5000, 50, 9860, 0 ) );
				Add( new GenericBuyInfo( typeof( EarthElementalCostume ), 5000, 50, 9860, 0 ) );
				Add( new GenericBuyInfo( typeof( EttinCostume ), 5000, 20, 9860, 0 ) );
			} 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{ 
				Add( typeof( SkeletonCostume ), 2500 );
				Add( typeof( BlackthorneCostume ), 2500 );
				Add( typeof( LizardmanCostume ), 2500 );
				Add( typeof( FanDancerCostume ), 2500 );
				Add( typeof( FairyCostume ), 2500 );
				Add( typeof( GargoyleCostume ), 2500 );
				Add( typeof( MinionCostume ), 2500 );
				Add( typeof( ImpCostume ), 2500 );
				Add( typeof( KappaCostume ), 2500 );
				Add( typeof( MongbatCostume ), 2500 );
				Add( typeof( CentaurCostume ), 2500 );
				//Add( typeof( ArcaneDemonCostume ), 2500 );
				Add( typeof( CyclopsCostume ), 2500 );
				Add( typeof( EarthElementalCostume ), 2500 );
				Add( typeof( EttinCostume ), 2500 );
			} 
		} 
	} 
}
