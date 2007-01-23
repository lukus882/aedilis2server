using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Gumps;

namespace Server.Gumps
{
	public class GuildForm : Gump
	{
		public GuildForm()
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
			this.AddLabel(96, 115, 1281, "Male");
			this.AddLabel(349, 115, 1281, "Male");
			this.AddImage(24, 86, 12);
			this.AddImage(24, 87, 50469);
			this.AddImage(267, 86, 14);
			this.AddImage(267, 86, 50898);
			this.AddButton(337, 394, 1806, 1807, (int)Buttons.Button1, GumpButtonType.Reply, 0);
			this.AddImage(24, 86, 50700);
			this.AddButton(100, 322, 1154, 1155, (int)Buttons.Button2, GumpButtonType.Reply, 0);
			this.AddButton(337, 322, 1154, 1155, (int)Buttons.Button3, GumpButtonType.Reply, 0);

		}
		
		public enum Buttons
		{
			Button1,
			Button2,
			Button3,

		}


                public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;

			switch( (Buttons) info.ButtonID )
			{


                                case Buttons.Button1:
                                {
                                from.SendGump(new GuildFormt());
                                break;
                                }


				case Buttons.Button2:
                                {
                                from.AddToBackpack( new HMR() );
                                from.SendMessage( "A New Guild Robe Appears In Your BackPack" );
                                break;
                                }

                                
                                case Buttons.Button3:
                                {
                                from.AddToBackpack( new EMR() );
                                from.SendMessage( "A New Guild Robe Appears In Your BackPack" );
                                break;
                                }



                            }
                     
                   }

	}
}