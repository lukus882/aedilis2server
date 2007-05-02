using System;
using System.Collections;
using Server;
using Server.Mobiles;
using Server.Misc;
using Server.Gumps;

namespace Fatima.CharacterFlags
{
	public class ViewCharacterFlagGump : Server.Gumps.Gump
	{
		private BaseCharacterFlag Flag;
		private PlayerMobile m_Player;

		public void AddBlackAlpha( int x, int y, int width, int height )
		{
			AddImageTiled( x, y, width, height, 2624 );
			AddAlphaRegion( x, y, width, height );
		}

		public string Color( string text, int color )
		{
			return String.Format( "<BASEFONT COLOR=#{0:X6}>{1}</BASEFONT>", color, text );
		}

		public string Center( string text )
		{
			return String.Format( "<CENTER>{0}</CENTER>", text );
		}

		private const int MainColor = 0x0033FF; //0x32CD32;

		public ViewCharacterFlagGump( PlayerMobile m, BaseCharacterFlag flag) : base(0, 0)
		{
			m_Player = m;
			Flag = flag;

			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;

			AddPage(0);

			AddBackground(57, 66, 411, 343, 9260);
			AddBlackAlpha(71, 79, 384, 312);
			AddLabel(183, 84, 197, "Player Character Flags");
			AddImageTiled(73, 139, 382, 4, 96);
			AddImage(7, 4, 10440);
			AddImage(436, 4, 10441);
			AddHtml( 158, 106, 200, 25, Color( Center(flag.Description), MainColor) , (bool)false, (bool)false);

			AddHtml( 90, 154, 349, 222, flag.ProgressDetails(), (bool)true, (bool)true);

			if ( m != null && m.AccessLevel >= AccessLevel.Administrator )
			{
				AddLabel(419, 108, 62, "Edit");
				AddButton(373, 107, 4011, 4013, (int)Buttons.Edit, GumpButtonType.Reply, 0);
			}
		}
		
		private enum Buttons
		{
			Quit = 0,

			Edit = 1
		}

		public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
		{
			Mobile m = sender.Mobile;
			if ( m == null )
				return;

			switch ( (Buttons)info.ButtonID )
			{
				case Buttons.Quit:
				{
					break;
				}
				case Buttons.Edit:
				{
					if ( m != null && m.AccessLevel >= AccessLevel.Administrator )
					{
						m.SendGump( new FlagEditGump( Flag ) );
					}
					break;
				}
			}
		}
	}
}