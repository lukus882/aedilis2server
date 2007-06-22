using System;
using System.Collections;
using Server.Items;

namespace Server.Mobiles
{
	public class SBGardener : SBInfo
	{
		private ArrayList m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBGardener()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override ArrayList BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : ArrayList
		{
			public InternalBuyInfo()
			{

					Add( new GenericBuyInfo( typeof( BambooCluster ), 150, 100, 9324, 0 ) );
					Add( new GenericBuyInfo( typeof( BambooEast ), 150, 100, 9327, 0 ) );
					Add( new GenericBuyInfo( typeof( BambooNorth ), 150, 100, 9325, 0 ) );
					Add( new GenericBuyInfo( typeof( BambooSouth ), 150, 100, 9326, 0 ) );
					Add( new GenericBuyInfo( typeof( BambooWest ), 150, 100, 9328, 0 ) );
					Add( new GenericBuyInfo( typeof( BladePlant ), 150, 100, 3219, 0 ) );
					Add( new GenericBuyInfo( typeof( BramblesLarge ), 150, 100, 3391, 0 ) );
					Add( new GenericBuyInfo( typeof( BramblesSmall ), 150, 100, 3392, 0 ) );
					Add( new GenericBuyInfo( typeof( Bulrushes ), 150, 100, 3220, 0 ) );
					Add( new GenericBuyInfo( typeof( Cactus1 ), 150, 100, 3365, 0 ) );
					Add( new GenericBuyInfo( typeof( Cactus2 ), 150, 100, 3366, 0 ) );
					Add( new GenericBuyInfo( typeof( Cactus3 ), 150, 100, 3367, 0 ) );
					Add( new GenericBuyInfo( typeof( Cactus4 ), 150, 100, 3368, 0 ) );
					Add( new GenericBuyInfo( typeof( Cactus5 ), 150, 100, 3370, 0 ) );
					Add( new GenericBuyInfo( typeof( Cactus6 ), 150, 100, 3372, 0 ) );
					Add( new GenericBuyInfo( typeof( Cactus7 ), 150, 100, 3374, 0 ) );
					Add( new GenericBuyInfo( typeof( CatTails1 ), 150, 100, 3255, 0 ) );
					Add( new GenericBuyInfo( typeof( CatTails2 ), 150, 100, 3256, 0 ) );
					Add( new GenericBuyInfo( typeof( ElephantEar ), 150, 100, 3223, 0 ) );
					Add( new GenericBuyInfo( typeof( FanPlant ), 150, 100, 3224, 0 ) );
					Add( new GenericBuyInfo( typeof( Fern1 ), 150, 100, 3231, 0 ) );
					Add( new GenericBuyInfo( typeof( Fern2 ), 150, 100, 3232, 0 ) );
					Add( new GenericBuyInfo( typeof( Fern3 ), 150, 100, 3234, 0 ) );
					Add( new GenericBuyInfo( typeof( Fern4 ), 150, 100, 3235, 0 ) );
					Add( new GenericBuyInfo( typeof( Fern5 ), 150, 100, 3236, 0 ) );
					Add( new GenericBuyInfo( typeof( JuniperBush ), 150, 100, 3272, 0 ) );
					Add( new GenericBuyInfo( typeof( LargeFern ), 150, 100, 3233, 0 ) );
					Add( new GenericBuyInfo( typeof( Mushrooms1 ), 150, 100, 3347, 0 ) );
					Add( new GenericBuyInfo( typeof( PampasGrass1 ), 150, 100, 3237, 0 ) );
					Add( new GenericBuyInfo( typeof( PampasGrass2 ), 150, 100, 3268, 0 ) );
					Add( new GenericBuyInfo( typeof( Rushes ), 150, 100, 3239, 0 ) );
					Add( new GenericBuyInfo( typeof( SnakePlant ), 150, 100, 3241, 0 ) );
					Add( new GenericBuyInfo( typeof( TangledBramblesE ), 150, 100, 12322, 0 ) );
					Add( new GenericBuyInfo( typeof( TangledBramblesN ), 150, 100, 12320, 0 ) );
					Add( new GenericBuyInfo( typeof( TangledBramblesS ), 150, 100, 12321, 0 ) );
					Add( new GenericBuyInfo( typeof( TangledBramblesW ), 150, 100, 12323, 0 ) );
					Add( new GenericBuyInfo( typeof( TrimmedHedge1 ), 150, 100, 3215, 0 ) );


			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{

					Add( typeof( FertileDirt ), 2 );

			}
		}
	}
}
