using System;
using Server;
using Server.Gumps;

namespace Server.Gumps
{
	public class NameGump : Gump
	{
		public NameGump()
			: base( 200, 200 )
		{
			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(12, 86, 475, 60, 9200);
			this.AddBackground(53, 66, 94, 33, 9200);
			this.AddLabel(59, 73, 1359, @"Invalid Name");
			this.AddLabel(33, 120, 1359, @"You can choose a new name by typing it into the main game window now");
			this.AddLabel(108, 99, 1359, @"The name you have chosen is already in use");

		}
		

	}
}
