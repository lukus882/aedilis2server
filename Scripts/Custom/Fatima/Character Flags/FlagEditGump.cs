using System;
using Server;
using Server.Gumps;
using Server.Mobiles;

namespace Fatima.CharacterFlags
{
	public class FlagEditGump : Server.Gumps.Gump
	{
		private const int SERIES_PER_PAGE = 6;

		private const int BIT_ROWS = 13;
		private const int BIT_DELTA_Y = 35; //y => rows
		private const int BIT_DELTA_X = 111; //x => columns

		private const int SERIES_DELTA_Y = 28;

		private const int ON_BUTTON = 10830;
		private const int OFF_BUTTON = 10850;

		private BaseCharacterFlag m_Flag;
		private int m_SeriesPage;
		private int m_SelectedSeries;

		public string Center( string text )
		{
			return String.Format( "<CENTER>{0}</CENTER>", text );
		}

		public string Color( string text, int color )
		{
			return String.Format( "<BASEFONT COLOR=#{0:X6}>{1}</BASEFONT>", color, text );
		}

		public FlagEditGump( BaseCharacterFlag flag ) : this(flag, 0, 0) {}
		public FlagEditGump( BaseCharacterFlag flag, int seriesPage, int selectedSeries ) : base(0, 0)
		{
			m_Flag = flag;
			m_SeriesPage = seriesPage;
			m_SelectedSeries = selectedSeries;

			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;

			AddPage(0);

			AddBackground(191, 16, 593, 552, 9200);
			AddAlphaRegion(202, 47, 567, 500);
			AddLabel(433, 21, 94, "Editing Flag");

			AddBackground(0, 138, 188, 275, 3500);
			AddLabel(53, 153, 94, "SERIES SET");
			AddImage(23, 22, 100);
			AddHtml( 44, 44, 94, 54, Color( Center( flag.HashKey ), 0xFFFFFF ), (bool)false, (bool)false);
			AddLabel(46, 451, 32, "RED => OFF");
			AddLabel(46, 469, 62, "GREEN => ON");


			int loopStart = (seriesPage * SERIES_PER_PAGE);
			int loopRun = flag.FlagLength;

			//Category Page switches.
			AddLabel(73, 358, 94, "Page");
			AddHtml( 66, 380, 42, 19, Center( (seriesPage + 1).ToString() ), (bool)false, (bool)false); //page# for Categories.

			if ( seriesPage > 0 )
			{
				AddButton(48, 383, 9706, 9705, (int)Buttons.LastSeriesPage, GumpButtonType.Reply, 0);
			}
			if ( loopRun - loopStart > SERIES_PER_PAGE ) //implies there are more then SERIES_PER_PAGE entries..
			{
				loopRun = SERIES_PER_PAGE;
				AddButton(112, 383, 9702, 9701, (int)Buttons.NextSeriesPage, GumpButtonType.Reply, 0);
			}

			for( int index = 0; index < loopRun; index++ )
			{
				if ( loopStart + index >= flag.FlagLength ) //we're done.
					break;

				int blockStart = ((loopStart + index) * 64) + 1;
				int blockEnd = ((loopStart + index + 1) * 64);

				//Series entries - 6 Total, per page.
				AddButton(24, 179 + (SERIES_DELTA_Y * index), 4005, 4007, (int)Buttons.SeriesPageStart + index, GumpButtonType.Reply, 0);
				AddLabel(65, 179 + (SERIES_DELTA_Y * index), 367, String.Format("{0}-{1}", blockStart, blockEnd) );
			}


			//grab the value at our position (page/series)
			ulong flagValue = flag.getValue( (seriesPage + selectedSeries ) * 64 );

			int xShift = 0;
			int yShift = 0;

			for( byte i=1; i<=64; i++)
			{
				bool flagOn = HasFlagAtBit( (byte)(i - 1), flagValue );

				AddButton(216 + xShift, 62 + yShift, flagOn ? ON_BUTTON : OFF_BUTTON, flagOn ? OFF_BUTTON : ON_BUTTON, (int)Buttons.BitStart + i, GumpButtonType.Reply, 0);
				AddLabel(260 + xShift, 66 + yShift, 208, String.Format("Bit {0}", i) );

				if ( i % BIT_ROWS == 0 )
				{
					xShift += BIT_DELTA_X;
					yShift = 0;
				}
				else
				{
					yShift += BIT_DELTA_Y;
				}
			}
		}

		private bool HasFlagAtBit( byte bitPosition, ulong flagValue )
		{
			ulong flag = (ulong)Math.Pow( 2, bitPosition );

			return (flagValue & flag) != 0;
		}
		
		public enum Buttons
		{
			Quit = 0,

			LastSeriesPage,
			NextSeriesPage,

			SeriesPageStart = 10,

			BitStart = 100,
		}

		public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
		{
			int val = info.ButtonID;

			if ( val <= 0 )
				return;

			Mobile from = sender.Mobile;

			if ( from == null || !(from is PlayerMobile) )
				return;

			if ( from.AccessLevel < AccessLevel.Administrator )
				return;

			PlayerMobile m = from as PlayerMobile;

			switch ( (Buttons)info.ButtonID )
			{
				case Buttons.NextSeriesPage:
				{
					m.SendGump( new FlagEditGump( m_Flag, ++m_SeriesPage, 0 ) );
					break;
				}
				case Buttons.LastSeriesPage:
				{
					m.SendGump( new FlagEditGump( m_Flag, --m_SeriesPage, 0 ) );
					break;
				}
			}

			if (info.ButtonID >= (int)Buttons.SeriesPageStart && info.ButtonID < (int)Buttons.SeriesPageStart + SERIES_PER_PAGE )
			{
				int newIndex = info.ButtonID - (int)Buttons.SeriesPageStart;
				if ( newIndex == m_SelectedSeries ) //no change
				{
					m.SendGump( this );
				}
				else
					m.SendGump( new FlagEditGump( m_Flag, m_SeriesPage, newIndex ) );
			}

			if (info.ButtonID >= (int)Buttons.BitStart && info.ButtonID <= (int)Buttons.BitStart + 64 )
			{
				int index = info.ButtonID - (int)Buttons.BitStart; //0-63
				int position = (m_SeriesPage + m_SelectedSeries) * 64 + index - 1;

				if ( m_Flag == null )
					from.SendMessage("The flag has gone null. Something is wrong.. perhaps it was removed while editing?");
				else
				{
					m_Flag.SetFlag( position, !m_Flag.HasFlag( position ) );
					from.SendMessage( String.Format("Bit {0} ({1}) has been toggled.", (index + 1).ToString(), (1 + position).ToString()) );
					from.SendMessage( String.Format("Flag Value : {0}", m_Flag.getValue( position )) );
					m.SendGump( new FlagEditGump( m_Flag, m_SeriesPage, m_SelectedSeries ) );
				}
			}
		}
	}
}