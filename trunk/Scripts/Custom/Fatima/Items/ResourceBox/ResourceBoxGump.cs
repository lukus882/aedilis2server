using System;
using System.Collections.Generic;
using Server;
using Server.Gumps;
using Fatima.Items;

namespace Fatima.Gumps
{
	public class ResBoxGumpInfo
	{
		private int m_CatSelected;
		private int m_CatPage;
		private int m_ResPage;
	
		public int CatSelected{ get{ return m_CatSelected; } }
		public int CatPage{ get{ return m_CatPage; } }
		public int ResPage{ get{ return m_ResPage; } }
	
		public ResBoxGumpInfo( int category, int catPage, int resPage )
		{
			m_CatSelected = category;
			m_CatPage = catPage;
			m_ResPage = resPage;
		}
	}

	public class ResourceBoxGump : Gump
	{
		private const byte DELTA_Y = 31; //spacing between buttons going downward.
		private const byte MAX_PER_PAGE = 7; //Doesn't adjust gump size. Don't touch!

		private const int CAT_UNSELECTED_COLOR = 62;
		private const int CAT_SELECTED_COLOR = 32;

		private const int RES_ITEM_COLOR = 143;
		private const int RES_AMOUNT_COLOR32 = 0x8080FF;

		private ResourceBox m_Box;
		private ResourceBoxContainer m_BoxContents;
		private int m_CatSelected;
		private int m_CatPage;
		private int m_ResPage;
		private String[] m_Cats;
		private Type[] m_ResValid;

		private ResBoxGumpInfo GetInfo
		{
			get
			{
				return new ResBoxGumpInfo(m_CatSelected, m_CatPage, m_ResPage);
			}
		}

		public string Center( string text )
		{
			return String.Format( "<CENTER>{0}</CENTER>", text );
		}

		public string Color( string text, int color )
		{
			return String.Format( "<BASEFONT COLOR=#{0:X6}>{1}</BASEFONT>", color, text );
		}

		public ResourceBoxGump( Mobile from, ResourceBox box, ResourceBoxContainer contents ) : this(from, box, contents, 0, 0, 0) {}
		public ResourceBoxGump( Mobile from, ResourceBox box, ResourceBoxContainer contents, int category, int catPage, int resPage ) : base(0, 0)
		{
			from.CloseGump( typeof( ResourceBoxGump ) );

			if (box == null)
				return;

			m_Box = box;
			m_CatSelected = category;
			m_BoxContents = contents;
			m_CatPage = catPage;
			m_ResPage = resPage;

			AddPage(0);
			AddBackground(31, 127, 198, 336, 2600);
			AddBackground(244, 127, 271, 336, 9270);
			AddAlphaRegion(260, 141, 243, 310);

			//Top Block - Wooden Board & Label
			AddImage(183, 14, 103);
			AddLabel(212, 40, 55, "Resource Box");
			AddLabel(225, 67, 55, "Manifest");


			//Category Window
			AddLabel(91, 145, 462, "Categories");

			//Populate the Category Window
			Dictionary<string, List<Type>> categories = ResourceBox.GetCategorizedTypes(box);
			m_Cats = new List<string>(categories.Keys).ToArray();

			int loopStart = catPage * MAX_PER_PAGE;
			int loopCount = GetIndexEnd( m_Cats.Length, catPage ) - loopStart;

			for( int index = 0; index< loopCount; index++ )
			{
				AddButton(56, 181 + (index*DELTA_Y), 2152, 2154, index + (int)Buttons.Category1, GumpButtonType.Reply, 0);
				AddLabel(93, 184 + (index*DELTA_Y), category == index ? CAT_SELECTED_COLOR : CAT_UNSELECTED_COLOR, m_Cats[loopStart + index]);
			}

			if ( m_Cats.Length > loopCount )
				AddButton(156, 422, 5541, 5542, (int)Buttons.NextCategory, GumpButtonType.Reply, 0);
			if ( catPage > 0 )
				AddButton(84, 422, 5538, 5539, (int)Buttons.LastCategory, GumpButtonType.Reply, 0);
				

			//Change CategoryWindow Pages
			AddLabel(112, 397, 92, "Page");
			AddHtml( 111, 422, 35, 20, Color(Center( (catPage + 1).ToString()),RES_AMOUNT_COLOR32), (bool)false, (bool)false);

			AddLabel(314, 145, 462, "Resource");
			AddLabel(443, 145, 432, "Amount");


			m_ResValid = categories[(new List<string>(categories.Keys).ToArray())[category]].ToArray();
			//Populate the Resource Window for the Category

			loopStart = resPage * MAX_PER_PAGE;
			loopCount = GetIndexEnd( m_ResValid.Length, resPage ) - loopStart;

			for( int index = 0; index< loopCount; index++ )
			{

				AddButton(272, 181 + (index*DELTA_Y), 4005, 4006, index + (int)Buttons.Inventory1, GumpButtonType.Reply, 0);
				AddLabel(315, 181 + (index*DELTA_Y), RES_ITEM_COLOR, ResourceBox.GetName(box, m_ResValid[loopStart + index]) );
				AddHtml( 435, 181 + (index*DELTA_Y), 62, 18, Color(Center(contents.GetAmount(m_ResValid[loopStart + index]).ToString()),RES_AMOUNT_COLOR32), (bool)false, (bool)false);
			}

			//Change Resource Window Pages
			AddLabel(367, 395, 92, "Page");
			AddHtml( 366, 420, 35, 20, Color(Center( (resPage + 1).ToString()),RES_AMOUNT_COLOR32), (bool)false, (bool)false);

			if ( m_ResValid.Length > loopStart + loopCount )
				AddButton(411, 420, 5541, 5542, (int)Buttons.NextResource, GumpButtonType.Reply, 0);
			if ( resPage > 0 )
				AddButton(339, 420, 5538, 5539, (int)Buttons.LastResource, GumpButtonType.Reply, 0);


			//AddLabel(93, 215, 32, "Tailoring");
			//AddLabel(281, 145, 1152, "*");
			//AddLabel(316, 212, 143, "Bronze Ingots");
			//AddHtml( 436, 212, 62, 18, "090000", (bool)false, (bool)false);
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
		
		public enum Buttons
		{
			Close = 0,

			NextCategory,
			LastCategory,

			NextResource,
			LastResource,

			Category1 = 100,
			Category2,
			Category3,
			Category4,
			Category5,
			Category6,
			Category7,

			Inventory1 = 200,
			Inventory2,
			Inventory3,
			Inventory4,
			Inventory5,
			Inventory6,
			Inventory7,
			InventoryEnd = 300,
		}

		public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;
			int buttonID = info.ButtonID;

			switch ( (Buttons)buttonID)
			{
				case Buttons.NextCategory:
				{
					if (m_Cats.Length > m_CatPage + 1)
					{
						from.SendGump( new ResourceBoxGump( from, m_Box, m_BoxContents, 0, m_CatPage + 1, 0 ) );
					}
					break;
				}
				case Buttons.LastCategory:
				{
					if ( m_CatPage - 1 >= 0)
					{
						from.SendGump( new ResourceBoxGump( from, m_Box, m_BoxContents, 0, m_CatPage - 1, 0 ) );
					}

					break;
				}
				case Buttons.NextResource:
				{
					if ( m_ResValid.Length > m_ResPage * MAX_PER_PAGE )
					{
						from.SendGump( new ResourceBoxGump( from, m_Box, m_BoxContents, m_CatSelected, m_CatPage, m_ResPage + 1 ) );
					}

					break;
				}
				case Buttons.LastResource:
				{
					if (m_ResPage - 1 >= 0 )
					{
						from.SendGump( new ResourceBoxGump( from, m_Box, m_BoxContents, m_CatSelected, m_CatPage, m_ResPage - 1 ) );
					}

					break;
				}
			}

			//Select a new category.
			if (buttonID >= (int)Buttons.Category1 && buttonID < (int)Buttons.Inventory1 )
			{
				int indexer = buttonID - (int)Buttons.Category1;

				if (m_Cats.Length > indexer)
					from.SendGump( new ResourceBoxGump( from, m_Box, m_BoxContents, indexer, m_CatPage, m_ResPage ) );
				else
					from.SendMessage("No such Category exists..");
			}
			
			//Get items out of inventory
			if (buttonID >= (int)Buttons.Inventory1 && buttonID < (int)Buttons.InventoryEnd )
			{
				int indexer = buttonID - (int)Buttons.Inventory1;
				int blockStart = m_ResPage * MAX_PER_PAGE;

				//Console.WriteLine("DEBUG-RES BOX::Withdrawl:: Indexer = {0}, blockStart = {1}, m_ResValid.Length = {2}", indexer, blockStart, m_ResValid.Length);
				if ( m_ResValid.Length > indexer + blockStart )
				{
					m_Box.Withdrawl( from, m_ResValid[indexer + blockStart], GetInfo );
					from.SendGump( new ResourceBoxGump( from, m_Box, m_BoxContents, m_CatSelected, m_CatPage, m_ResPage ) );
				}
			}

		}
	}
}