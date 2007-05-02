using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using Server;
using Server.Commands;
using Server.Mobiles;
using Server.Items;

namespace Fatima.Misc
{
	public class StockMarketItem
	{
		private static double m_LOG3 = Math.Log( 3 ); //store it, so we dont have to calculate it a million times.

		//Max values we can ever obtain, OF the base cost
		const double COST_PCT_MAX = 5.0; //500% max value
		const double COST_PCT_MIN = 0.0; //0% min value (base value)

		const double COST_MAX_ADJUST_POS = 0.25; //25% base value
		const double COST_MIN_ADJUST_POS = 0.05; //5% base value

		const double COST_MAX_ADJUST_NEG = 0.40; //40% base value -- rates drop faster then they grow. But, they tend to drop less often.
		const double COST_MIN_ADJUST_NEG = 0.15; //15% base value

		const double DEMAND_MAX_VALUE = 100.0;
		const double DEMAND_MIN_VALUE = -100.0;

		private Type m_ResType;
		private int m_BasePrice;
		private int m_MaxPrice;
		private string m_ResName;

		public Type ResType{ get{ return m_ResType; } }
		public int BasePrice{ get{ return m_BasePrice; } }
		public int MaxPrice{ get{ return m_MaxPrice; } }
		public string ResName { get { return m_ResName; } }

		private double m_Demand;
		public double Demand
		{ 
			get{ return m_Demand; } 
			set
			{
				if (value > DEMAND_MAX_VALUE)
					m_Demand = DEMAND_MAX_VALUE;
				else if (value < DEMAND_MIN_VALUE)
					m_Demand = DEMAND_MIN_VALUE;
				else
					m_Demand = value;
			}
		}

		private double m_PriceChange = 0.0;
		public double PriceChange
		{
			get
			{
				return m_PriceChange;
			}
		}

		private double m_PriceAdjust = 0;
		public double PriceAdjust
		{
			get
			{
				return m_PriceAdjust;
			}
			set { m_PriceAdjust = value > 0.0 ? value : 0.0; }
		}

		public int NewPrice
		{
			get
			{
				int price = m_BasePrice + (int)Math.Ceiling(m_PriceAdjust);

				if (m_MaxPrice > 0)
					return price > m_MaxPrice ? m_MaxPrice : price;
				else
					return price;
			}
		}

		public StockMarketItem( Type resType, string resName, int basePrice ) : this( resType, resName, basePrice, 0 ) {}
		public StockMarketItem( Type resType, string resName, int basePrice, int maxPrice )
		{
			m_ResType = resType;
			m_ResName = resName;
			m_BasePrice = basePrice;
			m_MaxPrice = maxPrice;
		}

		public static bool CriticalDrop( double demand )
		{
			if (demand > DEMAND_MAX_VALUE)
				demand = DEMAND_MAX_VALUE;
			else if (demand < DEMAND_MIN_VALUE)
				demand = DEMAND_MIN_VALUE;


			return ( (demand / 150) > Utility.RandomDouble() );
		}

		public static bool CriticalJump( double demand )
		{
			if (demand > DEMAND_MAX_VALUE)
				demand = DEMAND_MAX_VALUE;
			else if (demand < DEMAND_MIN_VALUE)
				demand = DEMAND_MIN_VALUE;


			return ( (demand / 250) > Utility.RandomDouble() );
		}

		public static bool IncreaseItemCost( double demand )
		{
			if (demand > DEMAND_MAX_VALUE)
				demand = DEMAND_MAX_VALUE;
			else if (demand < DEMAND_MIN_VALUE)
				demand = DEMAND_MIN_VALUE;


			return ( (demand / 250) + 0.25 > Utility.RandomDouble() );
		}

		private static double GetDemandChange( double demand, int boughtAmount )
		{
			for ( int index = 0; index < boughtAmount; index++ )
			{
				demand += demandFunction( demand );

				if (demand > DEMAND_MAX_VALUE)
					return DEMAND_MAX_VALUE;
				else if (demand < DEMAND_MIN_VALUE)
					return DEMAND_MIN_VALUE;
			}

			return demand;
		}

		private static double demandFunction( double demand )
		{
			if (demand < 0)
				return (1 / ( ( 1 + Math.Abs(demand / 10) ) * 10 * m_LOG3 )) * 5.0; //5 times greater demand below 0.

			return 1 / ( ( 1 + Math.Abs(demand / 10) ) * 10 * m_LOG3 );
		}

		public static double GetDemandChangeMod( double demand )
		{
			if ( demand <= 0 )
				return 0.3;

			return 1.0;
		}

		public static double GetRandomChange( int demand )
		{
			if ( demand <= 0 )
				return 0.3;

			return 1.0;
		}

		public double GetMaxPctChange( double inflation, bool increase )
		{
			double maxPCT = increase ? COST_MAX_ADJUST_POS : COST_MAX_ADJUST_NEG;

			if ( increase && m_MaxPrice > 0 )
			{
				double maxCostPct = 1 - ( ((double)m_BasePrice + m_PriceAdjust ) / m_MaxPrice );
				if (maxCostPct <= 0)
					return maxPCT = 0.0;
				else if ( maxPCT > maxCostPct )
					maxPCT = maxCostPct;
			} 

			if ( COST_PCT_MAX - inflation  > maxPCT ) //almost always..
				return maxPCT;
			else if ( COST_PCT_MAX - inflation <= 0 )
				return 0.0;
			else
				return Utility.RandomDouble() * maxPCT;
		}

		//PROVEN TRUE.
		private static double RandomDoubleBetween( double one, double two )
		{
			if ( one == two || two < one )
				return 0.0;

			double rnd = Utility.RandomDouble() * ( two - one );

			return (rnd + one);
		}

		public static double GetDemandDecayAmount( double demand )
		{
			return Math.Exp( (demand / 100) - 1 );
		}

		public void OnPlayerBought( int amount )
		{
			m_Demand = GetDemandChange( m_Demand, amount );
		}

		public void Flux()
		{
			bool increase = IncreaseItemCost( m_Demand );

			double inflation = m_PriceAdjust / (double)m_BasePrice;

			double maxChange = GetMaxPctChange( inflation, increase );
			double change = RandomDoubleBetween( increase ? COST_MIN_ADJUST_POS : COST_MIN_ADJUST_NEG, maxChange );

			bool crit = increase ? CriticalJump( m_Demand ) : CriticalDrop( m_Demand );
			if (crit)
				change *= 3;

			//ensure it belongs in the proper adjustment range.
			if (change > COST_PCT_MAX)
				change = COST_PCT_MAX;
			else if (COST_PCT_MIN > change)
				change = COST_PCT_MIN;

			if ( increase && m_PriceAdjust >= ( m_BasePrice * COST_PCT_MAX ) )
			{
				m_PriceChange = 0.0;
				m_PriceAdjust = m_BasePrice * COST_PCT_MAX;

				return; //price is capped out.
			}
			else if ( !increase && m_PriceAdjust < 0 )
			{
				m_PriceChange = 0.0;
				m_PriceAdjust = m_BasePrice;

/*
				Fatima.Misc.LogWriter.WriteLine("StockMarket","Debug", 
								String.Format(	"Fluxing {0}:" + 
									      	"\n		inflation: {1}" +
									      	"\n		maxChange: {2}" +
									      	"\n		change: {3}" +
									      	"\n		increase?: {4}" +
									      	"\n		m_PriceAdjust: {5}" +
									      	"\n		NewPrice: {6}" +
									      	"\n		Crit: {7}",
										m_ResType.Name, inflation.ToString(),
										maxChange.ToString(), change.ToString(),
										increase ? "Yes" : "No",
										m_PriceAdjust.ToString(),
										NewPrice.ToString(),
										crit ? "Yes" : "No"
									     )
								);
*/
				return; //price is minned out.
			}

			//round up & set to negative if needed.
			//PriceAdjust += Math.Ceiling(m_BasePrice * change ) * (increase ? 1 : -1);
			PriceAdjust += m_BasePrice * change * (increase ? 1 : -1);
			m_PriceChange = m_BasePrice * change  * (increase ? 1 : -1);
		}

		public void Decay()
		{
			if (m_Demand <= DEMAND_MIN_VALUE)
				return;

			Demand -= GetDemandDecayAmount( m_Demand );
		}

		public void SliceXML( XmlElement stock )
		{
			//type is hardcoded. No need to parse it out.
			Double.TryParse( stock.GetAttribute("demand"), out m_Demand );
			Double.TryParse( stock.GetAttribute("priceChange"), out m_PriceChange );
			Double.TryParse( stock.GetAttribute("priceAdjust"), out m_PriceAdjust );
		}

		public void SaveXML( XmlTextWriter xml )
		{
			xml.WriteStartElement( "Stock" );
				xml.WriteAttributeString( "type", m_ResType.FullName );
				xml.WriteAttributeString( "demand", m_Demand.ToString() );
				xml.WriteAttributeString( "priceChange", m_PriceChange.ToString() );
				xml.WriteAttributeString( "priceAdjust", m_PriceAdjust.ToString() );
			xml.WriteEndElement();
		}
	}

	public class VendorStockMarket
	{
		private static string m_SavePath = Path.Combine( Core.BaseDirectory, "Data\\StockMarket.xml" );

		private static Dictionary<Type,StockMarketItem> m_MarketItems = new Dictionary<Type,StockMarketItem>();

		private static TimeSpan DECAY_PERIOD = TimeSpan.FromHours(10);
		private static TimeSpan FLUX_PERIOD = TimeSpan.FromMinutes(30);
		private static DateTime m_LastDecay;
		private static DateTime m_LastFlux;

		static VendorStockMarket()
		{

			//Alchemy Region
			m_MarketItems.Add( typeof( Bottle ), 		new StockMarketItem(	typeof( Bottle ), 	"Bottle", 	5 ) );

			m_MarketItems.Add( typeof( BlackPearl ), 	new StockMarketItem(	typeof( BlackPearl ), 	"Black Pearls", 6 ) );
			m_MarketItems.Add( typeof( Bloodmoss ), 	new StockMarketItem(	typeof( Bloodmoss ), 	"Blood Moss", 	5 ) );
			m_MarketItems.Add( typeof( Garlic ), 		new StockMarketItem(	typeof( Garlic ), 	"Garlic", 	3 ) );
			m_MarketItems.Add( typeof( Ginseng ), 		new StockMarketItem(	typeof( Ginseng ), 	"Ginseng", 	3 ) );
			m_MarketItems.Add( typeof( MandrakeRoot ),	new StockMarketItem(	typeof( MandrakeRoot ), "Mandrake Roots", 3 ) );
			m_MarketItems.Add( typeof( Nightshade ), 	new StockMarketItem(	typeof( Nightshade ), 	"Nightshade", 	3 ) );
			m_MarketItems.Add( typeof( SpidersSilk ), 	new StockMarketItem(	typeof( SpidersSilk ), 	"Spiders Silk", 	3 ) );
			m_MarketItems.Add( typeof( SulfurousAsh ), 	new StockMarketItem(	typeof( SulfurousAsh ), "Sulfurous Ash", 3 ) );

			m_MarketItems.Add( typeof( BatWing ), 		new StockMarketItem(	typeof( BatWing ), 	"Bat Wings", 3 ) );
			m_MarketItems.Add( typeof( DaemonBlood ), 	new StockMarketItem(	typeof( DaemonBlood ), 	"Daemon Blood", 6 ) );
			m_MarketItems.Add( typeof( PigIron ), 		new StockMarketItem(	typeof( PigIron ), 	"Pig Iron", 5 ) );
			m_MarketItems.Add( typeof( NoxCrystal ), 	new StockMarketItem(	typeof( NoxCrystal ), 	"Nox Crystal", 6 ) );
			m_MarketItems.Add( typeof( GraveDust ), 	new StockMarketItem(	typeof( NoxCrystal ), 	"Grave Dust", 3 ) );
			//End Alchemy Region

			//Tailor Items
			m_MarketItems.Add( typeof( BoltOfCloth ), 	new StockMarketItem(	typeof( BoltOfCloth ), "Bolt Of Cloth", 120 ) );
			m_MarketItems.Add( typeof( Cloth ), 	new StockMarketItem(	typeof( Cloth ), "Cloth", 2 ) );
			m_MarketItems.Add( typeof( UncutCloth ), 	new StockMarketItem(	typeof( UncutCloth ), "Uncut Cloth", 2 ) );

			//Misc
			m_MarketItems.Add( typeof( Bandage ), 	new StockMarketItem(	typeof( Bandage ), 	"Bandages", 2 ) );
			m_MarketItems.Add( typeof( Log ), 	new StockMarketItem(	typeof( Log ), 		"Logs", 6 ) );
			m_MarketItems.Add( typeof( Board ), 	new StockMarketItem(	typeof( Board ), 	"Boards", 6 ) );
			m_MarketItems.Add( typeof( Arrow ), 	new StockMarketItem(	typeof( Arrow ), 	"Arrows", 4 ) );
			m_MarketItems.Add( typeof( IronOre ), 	new StockMarketItem(	typeof( IronOre ), 	"Iron Ore", 6 ) );
			m_MarketItems.Add( typeof( IronIngot ), new StockMarketItem(	typeof( IronIngot ), 	"Iron Ingots", 8 ) );
		}

		public static System.Collections.ArrayList Market
		{
			get
			{
				return new System.Collections.ArrayList(m_MarketItems.Values);
			}
		}

		public static void Initialize()
		{
			m_LastDecay = DateTime.Now;

			//CommandSystem.Register( "AddDemand", AccessLevel.Administrator, new CommandEventHandler( AddDemand_OnCommand ) );
			//CommandSystem.Register( "Flux", AccessLevel.Administrator, new CommandEventHandler( Flux_OnCommand ) );

			if ( LoadXML() )
				Console.WriteLine("\nStockMarket: Loaded StockMarket Data");

			EventSink.WorldSave += new WorldSaveEventHandler( OnWorldSave );
		}

		public static void OnWorldSave( WorldSaveEventArgs e )
		{
			Console.WriteLine("\nStockMarket: Saved StockMarket Data");
			SaveXML();

			if ( DateTime.Now - m_LastDecay >= DECAY_PERIOD ) //decay all.
			{
				MarketDecay();
				m_LastDecay = DateTime.Now;
			}
			if ( DateTime.Now - m_LastFlux >= FLUX_PERIOD ) //flux all.
			{
				MarketFlux();
				m_LastFlux = DateTime.Now;
			}
		}

		private static void AddDemand_OnCommand( CommandEventArgs e )
		{
			foreach ( StockMarketItem marketEntry in m_MarketItems.Values )
			{
				marketEntry.Demand = 75.0;
				for( int index=0; index< 20; index++ )
					marketEntry.Flux();
			}
		}

		private static void Flux_OnCommand( CommandEventArgs e )
		{
			MarketFlux();
		}

		public static void SaveXML()
		{
			using ( StreamWriter op = new StreamWriter( m_SavePath ) )
			{
				XmlTextWriter xml = new XmlTextWriter( op );
	
				xml.Formatting = Formatting.Indented;
				xml.IndentChar = '\t';
				xml.Indentation = 1;
	
				xml.WriteStartElement( "StockData" );
					foreach( StockMarketItem marketEntry in m_MarketItems.Values )
					{
						marketEntry.SaveXML( xml );
					}
				xml.WriteEndElement();
			}
		}

		public static bool LoadXML()
		{
			System.IO.FileInfo info = new System.IO.FileInfo( m_SavePath );
			if ( !info.Exists )
				return false;

			XmlDocument xml = new XmlDocument();
			xml.Load( m_SavePath );

			XmlElement root = xml["StockData"];

			foreach ( XmlElement element in root.GetElementsByTagName( "Stock" ) )
			{
				Type resType = Type.GetType( element.GetAttribute("type") );

				if (resType != null) //we found the type in question..
				{
					if (m_MarketItems.ContainsKey(resType))
					{
						m_MarketItems[resType].SliceXML(element);
						//m_Data.Add( new ID3XMLData( element.GetAttribute("artist"), element.GetAttribute("title"), element.GetAttribute("album")) );
					}
				}
			}

			return true;
		}


		public static void MarketDecay()
		{
			try
			{
				foreach ( StockMarketItem marketEntry in m_MarketItems.Values )
				{
					marketEntry.Decay();
				}
			} catch{ Console.WriteLine("Error!! Market was unable to Decay Demand of the stock market!"); }
		}

		public static void MarketFlux()
		{
			try
			{
				foreach ( StockMarketItem marketEntry in m_MarketItems.Values )
				{
					marketEntry.Flux();
				}
			} catch{ Console.WriteLine("Error!! Market was unable to flux the stock market!"); }
		}

		public static void OnVendorPurchase( BaseVendor vendor, Type res, int amount )
		{
			if (m_MarketItems.ContainsKey( res ) )
			{
				m_MarketItems[res].OnPlayerBought( amount );
			}
		}

		public static void UpdateVendor( BaseVendor vendor )
		{
			IBuyItemInfo[] itemsToBuy = vendor.GetBuyInfo();
			foreach ( IBuyItemInfo iBuyInterface in itemsToBuy )
			{
				if ( iBuyInterface is GenericBuyInfo )
				{
					GenericBuyInfo buyInfo = (GenericBuyInfo)iBuyInterface;
					if ( m_MarketItems.ContainsKey( buyInfo.Type ) )
					{
						StockMarketItem stockEntry = m_MarketItems[buyInfo.Type];
						buyInfo.Price = stockEntry.NewPrice;
						//vendor.Say( String.Format("Demand on {0} is {1}", buyInfo.Type.Name, stockEntry.Demand) );
					}
				}
			}

		}
	}
}