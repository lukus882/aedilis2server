using System;
using System.Collections;
using Server;
using Server.Mobiles;
using Server.Misc;
using Server.Gumps;

namespace Fatima.CharacterFlags
{
	public class ViewCharacterFlagsGump : Server.Gumps.Gump
	{
		private PlayerMobile FlaggedPerson;
		private ArrayList Keys;

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

		public ViewCharacterFlagsGump(PlayerMobile owner) : base(0, 0)
		{
			FlaggedPerson = owner;

			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;

			AddPage(0);

			AddBackground(57, 66, 411, 343, 9260);
			AddBlackAlpha(71, 79, 384, 312);
			AddLabel(187, 91, 197, "Player Character Flags");
			AddImageTiled(73, 135, 382, 4, 96);

			AddImage(7, 4, 10440);
			AddImage(436, 4, 10441);

			if (owner.CharFlags == null)
				return;

			Keys = new ArrayList(owner.CharFlags.Keys);

			int skipped = 0;
			for(int i=0;i<Keys.Count;i++)
			{
				string key = (string)Keys[i];

				BaseCharacterFlag bcFlag = owner.CharFlags[key] as BaseCharacterFlag;

				if (bcFlag.CanViewFlag || owner.AccessLevel >= AccessLevel.GameMaster )
				{
					//AddHtml( 120, 158 + (35*(i-skipped)), 291, 21, Color(bcFlag.Description, MainColor), (bool)false, (bool)false);
					AddLabel(120, 158 + (35*(i-skipped)), 167, bcFlag.Description); //Green text (167)
					AddButton(82, 158 + (35*(i-skipped)), 4011, 4012, i + 1, GumpButtonType.Reply, 0);
				}
				else
					skipped++;
			}
		}
		

		public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
		{
			if (info.ButtonID == 0 || FlaggedPerson == null || Keys.Count <= 0)
				return; //Bye

			Mobile m = sender.Mobile; //The person who requested the gump view..
			m.SendGump( new ViewCharacterFlagGump( (PlayerMobile)m, (BaseCharacterFlag)( FlaggedPerson.CharFlags[(string)Keys[info.ButtonID - 1]] )) );
		}
	}
}