/*

$Id: //depot/c%23/RunUO Core Scripts/RunUO Core Scripts/Customs/Items/House Teleporter/HouseTeleporterSelectGump.cs#1 $

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation; either version 2 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA

*/

using System;
using Server;
using Server.Gumps;
using Server.Items;

namespace Server.Gumps
{
	public class HouseTeleporterSelectGump : Gump
	{
		private Mobile m_From;
		private HouseTeleporterDeed m_Deed;

		public HouseTeleporterSelectGump(Mobile from, HouseTeleporterDeed deed)
			: base(0, 0)
		{
			m_From = from;
			m_Deed = deed;

			this.Closable = true;
			this.Disposable = true;
			this.Dragable = true;
			this.Resizable = false;
			this.AddPage(0);
			this.AddBackground(80, 48, 267, 261, 9200);
			this.AddItem(120, 88, 6173);
			this.AddButton(95, 99, 5541, 5542, (int)Buttons.Symbol1, GumpButtonType.Reply, 0);
			this.AddItem(120, 136, 6174);
			this.AddButton(95, 147, 5541, 5542, (int)Buttons.Symbol2, GumpButtonType.Reply, 0);
			this.AddItem(120, 184, 6175);
			this.AddButton(95, 195, 5541, 5542, (int)Buttons.Symbol3, GumpButtonType.Reply, 0);
			this.AddItem(120, 232, 6176);
			this.AddButton(95, 243, 5541, 5542, (int)Buttons.Symbol4, GumpButtonType.Reply, 0);
			this.AddItem(200, 88, 6177);
			this.AddButton(175, 99, 5541, 5542, (int)Buttons.Symbol5, GumpButtonType.Reply, 0);
			this.AddItem(200, 136, 6178);
			this.AddButton(175, 147, 5541, 5542, (int)Buttons.Symbol6, GumpButtonType.Reply, 0);
			this.AddItem(200, 184, 6179);
			this.AddButton(175, 195, 5541, 5542, (int)Buttons.Symbol7, GumpButtonType.Reply, 0);
			this.AddItem(200, 232, 6180);
			this.AddButton(175, 243, 5541, 5542, (int)Buttons.Symbol8, GumpButtonType.Reply, 0);
			this.AddItem(288, 88, 6181);
			this.AddButton(263, 99, 5541, 5542, (int)Buttons.Symbol9, GumpButtonType.Reply, 0);
			this.AddItem(288, 136, 6182);
			this.AddButton(263, 147, 5541, 5542, (int)Buttons.Symbol10, GumpButtonType.Reply, 0);
			this.AddItem(288, 184, 6183);
			this.AddButton(263, 195, 5541, 5542, (int)Buttons.Symbol11, GumpButtonType.Reply, 0);
			this.AddItem(288, 232, 6184);
			this.AddButton(263, 243, 5541, 5542, (int)Buttons.Symbol12, GumpButtonType.Reply, 0);
			this.AddLabel(146, 59, 1152, @"Select a Teleporter");
			this.AddButton(182, 280, 242, 241, (int)Buttons.Cancel, GumpButtonType.Reply, 0);
		}

		private enum Buttons
		{
			Cancel,
			Symbol1,
			Symbol2,
			Symbol3,
			Symbol4,
			Symbol5,
			Symbol6,
			Symbol7,
			Symbol8,
			Symbol9,
			Symbol10,
			Symbol11,
			Symbol12,
		}

		public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
		{
			if (sender.Mobile != m_From)
				return;

			Buttons btn = (Buttons)info.ButtonID;

			if (btn == Buttons.Cancel)
			{
				m_From.SendMessage(53, "Placement canceled.");
				m_Deed.Reset();
			}
			else
			{
				m_Deed.PlaceTeleporters(m_From, 6172 + info.ButtonID);
			}
		}
	}
}
