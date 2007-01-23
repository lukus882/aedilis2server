using System; 
using Server; 
using Server.Mobiles; 
using Server.Gumps;
using System.Data;
using System.IO;
using Server.Items;
using Server.Network;
using Server.Targeting;
using System.Collections;
using Server.Regions;
using Server.Prompts;

namespace Server.Commands
{
	public class Massbag
	{
		public static void Initialize()
		{
			CommandSystem.Register( "bag", AccessLevel.Player, new CommandEventHandler( MassBag_OnCommand ) );
		}

		private static void MassBag_OnCommand( CommandEventArgs e )
		{
			Mobile m = e.Mobile;
			if( m.Player && m is PlayerMobile )
			{
				if (m.Alive)
				{
					m.SendMessage("Which item type would you like bagged?");
					m.Target = new TargetItemType();
				}
			}
		}

		private class TargetItemType : Target
		{
			public TargetItemType() : base( 15, false, TargetFlags.None )
			{

			}

			protected override void OnTarget( Mobile from, object targ )
			{
				if (targ is Object)
				{
					if (targ is Container)
					{
						from.SendMessage("You cannot add containers to containers. Sorry!");
					}
					else
					{
						if (targ is Item)
						{
							Item itemtobag = targ as Item;
							if ( itemtobag.IsChildOf( from.Backpack ) ) // Make sure its in their pack
							{
								from.SendMessage("Select the Container you wish to toss the items in.");
								from.Target = new TargetBag(itemtobag);
							}
							else
							{
								from.SendMessage("Item must be in your backpack in order to initiate Mass Bagging!");
							}
						}
						else
						{
							from.SendMessage("You can only bag items!");
						}
					}
				}
				else
				{
					from.SendMessage("You cannot bag that!");
				}
			}
		}

		private class TargetBag : Target
		{
			private Item m_item;
			//private Item m_type;

			public TargetBag(Item item) : base( 15, false, TargetFlags.None )
			{
				m_item=item;
				
			}

			protected override void OnTarget( Mobile from, object targ )
			{
				Item itemcheck;
				int countitems=0;

				if (targ is Container)
				{
					if (m_item == targ)
					{
						from.SendMessage("You cannot add this onto itself.");
					}
					else
					{
						Container bag = targ as Container;
						if ( bag.IsChildOf( from.Backpack ) ) // Make sure its in their pack
						{
							ArrayList BagContents = new ArrayList();
							//from.SendMessage("#" + BagContents.Count.ToString());
							//int count = BagContents.Count;
							foreach ( Item item in from.Backpack.Items )
							{
								if (item.GetType() == m_item.GetType())
								{
									BagContents.Add(item);
								}
							}
	
							for (int i=0;i < BagContents.Count ;i++)
							{
								itemcheck = BagContents[i] as Item;
								bag.DropItem(itemcheck);
								countitems++;
							}
							from.SendMessage(countitems.ToString() + " item(s) have been moved into the container successfully.");
						}
						else
						{
							from.SendMessage("The container must be in your backpack in order to initiate Mass Bagging!");
						}
					}
				}
				else
				{
					from.SendMessage("You cannot bag that!");
				}
			}
		}
	}
}