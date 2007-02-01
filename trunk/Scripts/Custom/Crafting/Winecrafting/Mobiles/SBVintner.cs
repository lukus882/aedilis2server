//================================================//
// Created by dracana				  //
// Desc: Vintner npc buy/sell list.               //
//================================================//
using System; 
using System.Collections; 
using Server.Items; 
using Server.Items.Crops;

namespace Server.Mobiles 
{ 
	public class SBVintner : SBInfo 
	{ 
		private ArrayList m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBVintner() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override ArrayList BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : ArrayList 
		{ 
			public InternalBuyInfo() 
			{ 
				Add( new GenericBuyInfo( typeof( BottleOfWine ), 30, 20, 0x9C7, 0 ) );
			} 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{ 
				Add( typeof( BottleOfWine ), 20 );
			} 
		} 
	} 
}
