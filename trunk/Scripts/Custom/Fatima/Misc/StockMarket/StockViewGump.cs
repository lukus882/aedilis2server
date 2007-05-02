using System;
using System.Collections.Generic;
using Server;
using Server.Gumps;
using Fatima.Misc;

namespace Fatima.Gumps
{
	public class StockViewGump : Server.Gumps.Gump
	{
		private enum StockSort
		{
			CommodityABC,
			CommodityZYX,

			PriceUp,
			PriceDown,

			ChangeUp,
			ChangeDown,
		};

		private class MarketSorter : System.Collections.IComparer 
		{
			private StockSort m_SortType;

			private bool Ascending
			{
				get{ return (int)m_SortType % 2 == 0; }
			}
			
			public MarketSorter() : this( StockSort.CommodityABC ) {}
			public MarketSorter( StockSort sortType )
			{
				m_SortType = sortType;
			}
			
			int System.Collections.IComparer.Compare(object x, object y) 
			{
				StockMarketItem stock1 = (StockMarketItem) x;
				StockMarketItem stock2 = (StockMarketItem) y;

				if ( stock1 == null && stock2 == null )
					return 0;
				else if (stock1 == null && stock2 != null)
					return Ascending ? -1 : 1;
				else if (stock1 != null && stock2 == null)
					return Ascending ? 1 : -1;

				switch (m_SortType)
				{
					case StockSort.CommodityABC:
						goto case StockSort.CommodityZYX;
					case StockSort.CommodityZYX:
					{
						return Ascending ? stock1.ResName.CompareTo(stock2.ResName) : stock2.ResName.CompareTo(stock1.ResName);
					}
					case StockSort.PriceUp:
						goto case StockSort.PriceDown;
					case StockSort.PriceDown:
					{
						return Ascending ? stock1.NewPrice.CompareTo(stock2.NewPrice) : stock2.NewPrice.CompareTo(stock1.NewPrice);
					}
					case StockSort.ChangeUp:
						goto case StockSort.ChangeDown;
					case StockSort.ChangeDown:
					{
						return Ascending ? stock1.PriceChange.CompareTo(stock2.PriceChange) : stock2.PriceChange.CompareTo(stock1.PriceChange);
					}
				}

				return 0;
			}
		}

		public void AddBlackAlpha( int x, int y, int width, int height )
		{
			AddImageTiled( x, y, width, height, 9204 ); //3604
			//AddAlphaRegion( x, y, width, height );
		}

		public void AddWhiteAlpha( int x, int y, int width, int height )
		{
			AddImageTiled( x, y, width, height, 3604 );
			//AddAlphaRegion( x, y, width, height );
		}

		public string Center( string text )
		{
			return String.Format( "<CENTER>{0}</CENTER>", text );
		}

		public string Color( string text, int color )
		{
			return String.Format( "<BASEFONT COLOR=#{0:X6}>{1}</BASEFONT>", color, text );
		}

		private const int DELTA_Y = 30;
		private const int MAX_PER_PAGE = 7;
		private StockSort m_Sort;
		private int m_Page;
		private System.Collections.ArrayList m_Market;

		private const int COLOR32_RED = 0xCC0000;
		private const int COLOR32_GREEN = 0x007700;
		private const int COLOR32_BLUE = 0x0000FF;
		private const int COLOR32_ORANGE = 0x993300;
 

		private bool IsGoodBuy( StockMarketItem item )
		{
			double pct = (double)item.NewPrice / (double)item.BasePrice;

			return pct <= (double)2.00;
		}

		public StockViewGump( Mobile from ) : this( from, StockSort.CommodityABC, 0 ) {}
		private StockViewGump( Mobile from, StockSort sort, int page ) : base(0, 0)
		{
			from.CloseGump( typeof( ResourceBoxGump ) );

			Closable = true;

			m_Sort = sort;
			m_Page = page;

			AddPage(0);

			AddBackground(62, 48, 349, 299, 9270);
			AddAlphaRegion(74, 99, 325, 235);

			AddLabel(160, 64, 143, "Latest Commodity Prices");
			AddImageTiled(75, 93, 324, 4, 2700);
			AddLabel(103, 105, 75, "Commodity");
			AddLabel(244, 105, 75, "Price");
			AddLabel(339, 105, 75, "Change");
			AddAlphaRegion(75, 128, 324, 29);

			m_Market = VendorStockMarket.Market;

			m_Market.Sort( new MarketSorter( sort ) );

			int loopStart = page * MAX_PER_PAGE;
			int loopCount = GetIndexEnd( m_Market.Count, page ) - loopStart;

			for( int index = 0; index< loopCount; index++ )
			{
				StockMarketItem item = m_Market[loopStart + index] as StockMarketItem;

				if ( index % 2 == 0 )
					AddBlackAlpha(74, 128 + (index*DELTA_Y), 325, 29);
				else
					AddWhiteAlpha(74, 128 + (index*DELTA_Y), 325, 29);

				//AddHtml( 82, 130 + (index*DELTA_Y), 121, 23, Color( item.ResName ,COLOR32_BLUE), (bool)false, (bool)false); //COMMODITY NAME
				AddLabel(82, 130 + (index*DELTA_Y), 87, item.ResName );
				AddHtml( 223, 130 + (index*DELTA_Y), 70, 23, Color(Center( String.Format("{0}g", item.NewPrice) ), IsGoodBuy(item) ? COLOR32_GREEN : COLOR32_RED), (bool)false, (bool)false); //PRICE
				AddHtml( 322, 130 + (index*DELTA_Y), 67, 23, Color(Center( String.Format("{0}{1}g", item.PriceChange >= 0 ? "+" : String.Empty, item.PriceChange.ToString("N2") )), item.PriceChange >= 0 ? COLOR32_GREEN : COLOR32_RED), (bool)false, (bool)false); //CHANGE
			}

			/*****
			AddHtml( 82, 160, 121, 23, "Commodity2", (bool)false, (bool)false);
			AddHtml( 322, 160, 67, 23, "-5g", (bool)false, (bool)false);
			AddHtml( 227, 160, 70, 23, "65g", (bool)false, (bool)false);
			******/

			if ( m_Market.Count > loopStart + loopCount )
				AddButton(371, 63, 9904, 9901, (int)Buttons.NextPage, GumpButtonType.Reply, 0);
			if ( page > 0 )
				AddButton(78, 63, 9910, 9907, (int)Buttons.LastPage, GumpButtonType.Reply, 0);

			AddButton(84, 110, 2103, 2104, (int)Buttons.SortCommodity, GumpButtonType.Reply, 0);
			AddButton(228, 110, 2103, 2104, (int)Buttons.SortPrice, GumpButtonType.Reply, 0);
			AddButton(324, 110, 2103, 2104, (int)Buttons.SortChange, GumpButtonType.Reply, 0);
		}
		
		public enum Buttons
		{
			Quit,

			SortCommodity,
			SortPrice,
			SortChange,
			NextPage,
			LastPage,
		}

		private static int GetIndexEnd( int arrayCount, int curPage )
		{
			int totalIndexed = curPage * MAX_PER_PAGE;
			if ( arrayCount > totalIndexed )
			{
				if ( arrayCount - totalIndexed >= MAX_PER_PAGE)
					return totalIndexed + MAX_PER_PAGE;
				else
					return totalIndexed + (arrayCount - totalIndexed);
			}

			return 0;
		}

		public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;
			int buttonID = info.ButtonID;

			switch ( (Buttons)buttonID )
			{
				case Buttons.SortCommodity:
				{
					if ( m_Sort == StockSort.CommodityABC )
						from.SendGump( new StockViewGump( from, StockSort.CommodityZYX, m_Page ) );
					else
						from.SendGump( new StockViewGump( from, StockSort.CommodityABC, m_Page ) );

					break;
				}
				case Buttons.SortPrice:
				{
					if ( m_Sort == StockSort.PriceUp )
						from.SendGump( new StockViewGump( from, StockSort.PriceDown, m_Page ) );
					else
						from.SendGump( new StockViewGump( from, StockSort.PriceUp, m_Page ) );

					break;
				}
				case Buttons.SortChange:
				{
					if ( m_Sort == StockSort.ChangeUp )
						from.SendGump( new StockViewGump( from, StockSort.ChangeDown, m_Page ) );
					else
						from.SendGump( new StockViewGump( from, StockSort.ChangeUp, m_Page ) );

					break;
				}
				case Buttons.NextPage:
				{
					if ( m_Market.Count > m_Page * MAX_PER_PAGE )
					{
						from.SendGump( new StockViewGump( from, m_Sort, m_Page + 1 ) );
					}
					break;
				}
				case Buttons.LastPage:
				{
					if (m_Page - 1 >= 0 )
					{
						from.SendGump( new StockViewGump( from, m_Sort, m_Page - 1 ) );
					}

					break;
				}
			}
		}
	}
}