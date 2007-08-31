/*

$Id: //depot/c%23/RunUO Core Scripts/RunUO Core Scripts/Customs/Items/House Teleporter/AddonHouseTeleporter.cs#1 $

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
using Server.Items;
using Server.Multis;

namespace Server.Items
{
	public class AddonHouseTeleporter : HouseTeleporter, IAddon, IChopable
	{
		public AddonHouseTeleporter(int itemID)
			: base(itemID)
		{
		}

		public AddonHouseTeleporter(int itemID, Item target)
			: base(itemID, target)
		{
		}

		public override bool OnMoveOver(Mobile m)
		{
			if (Target != null || !Target.Deleted)
				return base.OnMoveOver(m);
			else
			{
				m.SendMessage(32, "The other side of the teleporter has been destroyed, the magic in this teleporter fades as you step on it.");
				BaseHouse house = BaseHouse.FindHouseAt(this);
				if (house != null)
					house.Addons.Remove(this);
				Delete();
				return true;
			}
		}

		#region Serialization
		public AddonHouseTeleporter(Serial serial)
			: base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.WriteEncodedInt(0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadEncodedInt();
		}
		#endregion

		#region IAddon Members
		public Item Deed { get { return null; } }

		public bool CouldFit(IPoint3D p, Map map)
		{
			HouseTeleporterDeed deed = new HouseTeleporterDeed();
			bool res = deed.CouldFit(p, map);
			deed.Delete();
			return res;
		}
		#endregion

		#region IChopable Members
		public void OnChop(Mobile from)
		{
			BaseHouse house = BaseHouse.FindHouseAt(from);

			if (house != null && house.IsOwner(from))
			{
				if (Target != null)
				{
					BaseHouse house2 = BaseHouse.FindHouseAt(Target);
					if (house2 != null)
						house2.Addons.Remove(Target);
					Target.Delete();
				}
				house.Addons.Remove(this);
				Delete();

				from.SendMessage("You destroy the teleporter.");
			}
		}
		#endregion
	}
}