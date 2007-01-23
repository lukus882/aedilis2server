using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Gumps;

namespace Server.Gumps
{
	public class GuildFormt : Gump
	{
		public GuildFormt()
			: base( 50, 50 )
		{
			this.Closable=false;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(51, 26, 383, 404, 9270);
			this.AddLabel(183, 47, 1193, "Select Guild Robe");
			this.AddLabel(93, 80, 1254, "Human");
			this.AddLabel(346, 80, 1270, "Elven");
			this.AddLabel(96, 115, 1281, "Female");
			this.AddLabel(349, 115, 1281, "Female");
			this.AddImage(24, 86, 13);
			this.AddImage(267, 86, 15);
			this.AddImage(24, 85, 50448);
			this.AddImage(267, 88, 50899);
			this.AddImage(24, 87, 50706);
			this.AddImage(267, 86, 50892);
			this.AddButton(67, 394, 1809, 1810, (int)Buttons.Button4, GumpButtonType.Reply, 0);
			this.AddButton(100, 322, 1154, 1155, (int)Buttons.Button5, GumpButtonType.Reply, 0);
			this.AddButton(337, 322, 1154, 1155, (int)Buttons.Button6, GumpButtonType.Reply, 0);

		}
		
		public enum Buttons
		{

			Button4,
			Button5,
			Button6,
		}


                public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;

			switch( (Buttons) info.ButtonID )
			{


                                case Buttons.Button4:
                                {
                                from.SendGump(new GuildForm());
                                break;
                                }



                                case Buttons.Button5:
                                {
                                from.AddToBackpack( new HFR() );
                                from.SendMessage( "A New Guild Robe Appears In Your BackPack" );
                                break;
                                }



                                case Buttons.Button6:
                                {
                                from.AddToBackpack( new EFR() );
                                from.SendMessage( "A New Guild Robe Appears In Your BackPack" );
                                break;
                                }
                            }
                     
                   }

	}
}