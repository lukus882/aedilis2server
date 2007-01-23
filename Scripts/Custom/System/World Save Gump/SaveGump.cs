using System;
using Server;
using Server.Gumps;

namespace Server.Gumps
{
	public class SaveGump : Gump
	{
		public SaveGump()
			: base( 200, 200 )
		{
			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(0, 0, 353, 118, 9270);
			this.AddItem(297, 38, 4168);
			this.AddLabel(118, 15, 1149, @"World Saving");
			this.AddLabel(48, 71, 255, @"take a few minutes. Please be patient");
			this.AddLabel(48, 55, 255, @"Due to the large world data base, this can");
			this.AddLabel(48, 39, 255, @"Please wait while the world saves");
			this.AddItem(12, 38, 4171);

		}
		

	}
}