using System;
using System.Collections;

using Server;
using Server.Gumps;
using Server.Items;

namespace Arya.Auction
{
	/// <summary>
	/// Summary description for AuctionSearchGump.
	/// </summary>
	public class AuctionSearchGump : Gump
	{
		private const int LabelHue = 0x480;
		private const int GreenHue = 0x40;
		private const int RedHue = 0x20;

		private ArrayList m_List;
		private bool m_ReturnToAuction;
		private ArrayList m_Buttons;

		public AuctionSearchGump( Mobile m, ArrayList items, bool returnToAuction ) : base( 50, 50 )
		{
			m.CloseGump( typeof( AuctionSearchGump ) );

			m_List = items;
			m_ReturnToAuction = returnToAuction;

			MakeGump();
		}

		private void MakeGump()
		{
			m_Buttons = new ArrayList();

			this.Closable=true;
			m_Buttons.Add( 0 );

			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
			this.AddImageTiled(49, 34, 402, 347, 3004);
			this.AddImageTiled(50, 35, 400, 345, 2624);
			this.AddAlphaRegion(50, 35, 400, 345);
			this.AddImage(165, 65, 10452);
			this.AddImage(0, 20, 10400);
			this.AddImage(0, 320, 10402);
			this.AddImage(35, 20, 10420);
			this.AddImage(421, 20, 10410);
			this.AddImage(410, 20, 10430);
			this.AddImageTiled(90, 32, 323, 16, 10254);
			this.AddLabel(185, 45, GreenHue, AuctionSystem.ST[ 32 ] );
			this.AddImage(420, 320, 10412);
			this.AddImage(0, 170, 10401);
			this.AddImage(420, 170, 10411);

			// TEXT 0 : Search text
			this.AddLabel(70, 115, LabelHue, AuctionSystem.ST[ 33 ] );
			this.AddImageTiled(145, 135, 200, 20, 3004);
			this.AddImageTiled(146, 136, 198, 18, 2624);
			this.AddAlphaRegion(146, 136, 198, 18);
			this.AddTextEntry(146, 135, 198, 20, RedHue, 0, @"");

			this.AddLabel(70, 160, LabelHue, AuctionSystem.ST[ 35 ] );

			this.AddCheck(260, 221, 2510, 2511, false, 1);
			this.AddLabel(280, 220, LabelHue, AuctionSystem.ST[ 35 ] );

			if ( Core.AOS )
			{
				this.AddCheck(260, 261, 2510, 2511, false, 9);
				this.AddLabel(280, 260, LabelHue, AuctionSystem.ST[ 36 ] );

				this.AddCheck(260, 241, 2510, 2511, false, 4);
				this.AddLabel(280, 240, LabelHue, AuctionSystem.ST[ 37 ] );
			}

			this.AddCheck(260, 201, 2510, 2511, false, 4);
			this.AddLabel(280, 200, LabelHue, AuctionSystem.ST[ 38 ] );

			this.AddCheck(260, 181, 2510, 2511, false, 5);
			this.AddLabel(280, 180, LabelHue, AuctionSystem.ST[ 39 ] );

			this.AddCheck(90, 181, 2510, 2511, false, 6);
			this.AddLabel(110, 180, LabelHue, AuctionSystem.ST[ 40 ] );

			this.AddCheck(90, 201, 2510, 2511, false, 7);
			this.AddLabel(110, 200, LabelHue, AuctionSystem.ST[ 41 ] );

			this.AddCheck(90, 221, 2510, 2511, false, 8);
			this.AddLabel(110, 220, LabelHue, AuctionSystem.ST[ 42 ] );

			this.AddCheck(90, 241, 2510, 2511, false, 2);
			this.AddLabel(110, 240, LabelHue, AuctionSystem.ST[ 43 ] );

			this.AddCheck(90, 261, 2510, 2511, false, 12);
			this.AddLabel(110, 260, LabelHue, AuctionSystem.ST[ 44 ] );

			if ( Core.AOS )
			{
				this.AddCheck(90, 280, 2510, 2511, false, 11);
				this.AddLabel(110, 279, LabelHue, AuctionSystem.ST[ 45 ] );

				this.AddCheck(260, 280, 2510, 2511, false, 10);
				this.AddLabel(280, 279, LabelHue, AuctionSystem.ST[ 46 ] );
			}

			// BUTTON 1 : Search
			this.AddButton(255, 350, 4005, 4006, 1, GumpButtonType.Reply, 0);
			this.AddLabel(295, 350, LabelHue, AuctionSystem.ST[ 16 ] );
			m_Buttons.Add( 1 );

			// BUTTON 0 : Cancel
			this.AddButton(85, 350, 4017, 4018, 0, GumpButtonType.Reply, 0);
			this.AddLabel(125, 350, LabelHue, AuctionSystem.ST[ 47 ] );
			m_Buttons.Add( 0 );
			
			// CHECK 0: Search withing existing results
			this.AddCheck(80, 310, 9721, 9724, false, 0);
			this.AddLabel(115, 312, LabelHue, AuctionSystem.ST[ 48 ] );
		}

		public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
		{
			if ( ! m_Buttons.Contains( info.ButtonID ) )
			{
				Console.WriteLine( @"The auction system located a potential exploit. 
					Player {0} (Acc. {1}) tried to press an unregistered button in a gump of type: {2}",
					sender.Mobile != null ? sender.Mobile.ToString() : "Unkown",
					sender.Mobile != null && sender.Mobile.Account != null ? ( sender.Mobile.Account as Server.Accounting.Account ).Username : "Unkown",
					this.GetType().Name );

				return;
			}

			if ( ! AuctionSystem.Running )
			{
				sender.Mobile.SendMessage( AuctionSystem.MessageHue, AuctionSystem.ST[ 15 ] );
				return;
			}

			if ( info.ButtonID == 0 )
			{
				// Cancel
				sender.Mobile.SendGump( new AuctionListing( sender.Mobile, m_List, true, m_ReturnToAuction ) );
				return;
			}

			bool searchExisting = false;
			bool artifacts = false;
			bool commodity = false;
			ArrayList types = new ArrayList();
			string text = null;

			if ( info.TextEntries[ 0 ].Text != null && info.TextEntries[ 0 ].Text.Length > 0 )
			{
				text = info.TextEntries[ 0 ].Text;
			}

			foreach( int check in info.Switches )
			{
				switch ( check )
				{
					case 0 : searchExisting = true;
						break;

					case 1: types.Add( typeof( MapItem ) );
						break;

					case 2: types.Add( typeof( BaseReagent ) );
						break;

					case 3: commodity = true;
						break;

					case 4:
						types.Add( typeof( StatCapScroll ) );
						types.Add( typeof( PowerScroll ) );
						break;

					case 5: types.Add( typeof( BaseJewel ) );
						break;

					case 6: types.Add( typeof( BaseWeapon ) );
						break;

					case 7: types.Add( typeof( BaseArmor ) );
						break;

					case 8: types.Add( typeof( BaseShield ) );
						break;

					case 9: artifacts = true;
						break;

					case 10: types.Add( typeof( Server.Engines.BulkOrders.SmallBOD ) );
						break;

					case 11: types.Add( typeof( Server.Engines.BulkOrders.LargeBOD ) );
						break;

					case 12:
						types.Add( typeof( BasePotion ) );
						types.Add( typeof( PotionKeg ) );
						break;
				}
			}

			ArrayList source = null;

			if ( searchExisting )
			{
				source = new ArrayList( m_List );
			}
			else
			{
				source = new ArrayList( AuctionSystem.Auctions );
			}

			ArrayList typeSearch = null;
			ArrayList commoditySearch = null;
			ArrayList artifactsSearch = null;

			if ( types.Count > 0 )
			{
				typeSearch = AuctionSearch.ForTypes( source, types );
			}

			if ( commodity )
			{
				commoditySearch = AuctionSearch.ForCommodities( source );
			}

			if ( artifacts )
			{
				artifactsSearch = AuctionSearch.ForArtifacts( source );
			}

			ArrayList results = new ArrayList();

			if ( typeSearch == null && artifactsSearch == null && commoditySearch == null )
			{
				results.AddRange( source );
			}
			else
			{
				if ( typeSearch != null )
					results.AddRange( typeSearch );

				if ( commoditySearch != null )
					results = AuctionSearch.Merge( results, commoditySearch );

				if ( artifactsSearch != null )
					results = AuctionSearch.Merge( results, artifactsSearch );
			}

			// Perform search
			if ( text != null )
			{
				results = AuctionSearch.SearchForText( results, text );
			}
			
			sender.Mobile.SendGump( new AuctionListing(
				sender.Mobile,
				results,
				true,
				m_ReturnToAuction ) );
		}
	}
}