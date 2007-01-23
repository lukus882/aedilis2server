// Script: Sort.cs
// Version: 1.1
// Author: Oak (ssalter)
// Servers: RunUO 2.0
// Date: 7/7/2006
// Purpose: 
// Player Command. [sort allows a player to sort items from one container to another
//   Type [sort followed by a keyword, target FROM container and then target TO container
//   Keywords are: gems, wands, regs, scrolls, armor, weapons, clothing, potions, hides, jewelry, help (to get list of keywords)
// History: 
//  Written for RunUO 1.0 shard in March 2005.
//  Modified for RunUO 2.0. Commented out XmlSpawner Questholder check.
// If you have XmlSpawner2, find the if(xx is Questholder) blocks that are commented out and uncomment them or players will use this command to store items in questholder (if you use questholders) and thus have a blessed container.
using System;
using Server;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using Server.Accounting; 
using System.Collections; 
using Server.Network;


namespace Server.Commands 
{ 
  public class Sort
  { 
	 private Type SortType;

	private static Type[] m_SortType = new Type[]
	{
		typeof( BaseArmor ),
		typeof( BaseWeapon ),
		typeof( BaseClothing ),
		typeof( SpellScroll ),
		typeof( BaseReagent ),
		typeof( BaseHides ),
		typeof( BasePotion ),
		typeof( BaseJewel ),
		typeof( Diamond ),
		typeof( BaseWand),
	};

    public static void Initialize() 
    { 
      CommandSystem.Register( "Sort", AccessLevel.Player, new CommandEventHandler( Sort_OnCommand ) ); 
    } 
    public static void Sort_OnCommand( CommandEventArgs e ) 
    { 
		Mobile from = e.Mobile; 
 
		// if we have a command after just the word "sort", as we should
		if ( e.Length != 0 )
		{
			switch( e.GetString(0).ToLower() )
			{
				case "wands" :
					from.LocalOverheadMessage( MessageType.Regular, 0x1150, true, "Select the container you want to sort FROM."); 
					from.Target = new PackFromTarget( from, m_SortType[9] );
					break;
				case "gems" :
					from.LocalOverheadMessage( MessageType.Regular, 0x1150, true, "Select the container you want to sort FROM."); 
					from.Target = new PackFromTarget( from, m_SortType[8] );
					break;
				case "regs" :
					from.LocalOverheadMessage( MessageType.Regular, 0x1150, true, "Select the container you want to sort FROM."); 
					from.Target = new PackFromTarget( from, m_SortType[4] );
					break;
				case "scrolls" :
					from.LocalOverheadMessage( MessageType.Regular, 0x1150, true, "Select the container you want to sort FROM."); 
					from.Target = new PackFromTarget( from, m_SortType[3] );
					break;
				case "armor" :
					from.LocalOverheadMessage( MessageType.Regular, 0x1150, true, "Select the container you want to sort FROM."); 
					from.Target = new PackFromTarget( from, m_SortType[0] );
					break;
				case "clothing" :
					from.LocalOverheadMessage( MessageType.Regular, 0x1150, true, "Select the container you want to sort FROM."); 
					from.Target = new PackFromTarget( from, m_SortType[2] );
					break;
				case "weapons" :
					from.LocalOverheadMessage( MessageType.Regular, 0x1150, true, "Select the container you want to sort FROM."); 
					from.Target = new PackFromTarget( from, m_SortType[1] );
					break;
				case "hides" :
					from.LocalOverheadMessage( MessageType.Regular, 0x1150, true, "Select the container you want to sort FROM."); 
					from.Target = new PackFromTarget( from, m_SortType[5] );
					break;
				case "potions" :
					from.LocalOverheadMessage( MessageType.Regular, 0x1150, true, "Select the container you want to sort FROM."); 
					from.Target = new PackFromTarget( from, m_SortType[6] );
					break;
				case "jewelry" :
					from.LocalOverheadMessage( MessageType.Regular, 0x1150, true, "Select the container you want to sort FROM."); 
					from.Target = new PackFromTarget( from, m_SortType[7] );
					break;
				case "help" :
					from.LocalOverheadMessage( MessageType.Regular, 0x1150, true, "Select the container you want to sort FROM."); 
					from.LocalOverheadMessage( MessageType.Regular, 1150, true, "Usage: [sort and one of the following words: gems, wands, regs, scrolls, armor, weapons, clothing, potions, hides, jewelry"); 
					break;
				default:
					from.LocalOverheadMessage( MessageType.Regular, 1150, true, "Usage: [sort and one of the following words: gems, wands, regs, scrolls, armor, weapons, clothing, potions, hides, jewelry"); 
					break;
			}
		}
		else
		{
			from.LocalOverheadMessage( MessageType.Regular, 1150, true, "Usage: [sort and one of the following words: gems, wands, regs, scrolls, armor, weapons, clothing, potions, hides, jewelry"); 
		}
	}
	private class PackFromTarget : Target
	{
		private Type SortType;
		public PackFromTarget( Mobile from, Type type ) : base( -1, true, TargetFlags.None )
		{
			SortType = type;
		}
			
		protected override void OnTarget( Mobile from, object o )
		{
			if(o is Container)
			{
				Container xx = o as Container;
//				if (xx is QuestHolder)
//				{
//					from.LocalOverheadMessage( MessageType.Regular, 0x22, true, "Sorting from a Questbook is an exploit. Your Account has been flagged."); 
//	                  ((Account)from.Account).SetTag( "QuestExploit", "true"); 
//					return;
//				}

				// Container that is either in a pack
				if ( xx.IsChildOf( from.Backpack) || xx == from.Backpack )
				{
					from.LocalOverheadMessage( MessageType.Regular, 0x33, true, "Select the container you want to sort INTO."); 
					from.Target = new PackToTarget( from, xx, SortType );
				}
				else
				{
					from.LocalOverheadMessage( MessageType.Regular, 0x22, true, "The container to sort FROM must be in your main backpack or BE your main backpack."); 
				}
			}
		}
	}
	private class PackToTarget : Target
	{
		private Container FromCont;
		private Type SortType;

			public PackToTarget( Mobile from, Container cont, Type type ) : base( -1, true, TargetFlags.None )
		{
			SortType = type;
			FromCont = cont;
		}
		
		protected override void OnTarget( Mobile from, object o )
		{
			if( o is Container)
			{
				Container xx = o as Container;

//				if (xx is QuestHolder)
//				{
//					from.LocalOverheadMessage( MessageType.Regular, 0x22, true, "Sorting into a Questbook is an exploit. Your Account has been flagged."); 
//	                  ((Account)from.Account).SetTag( "QuestExploit", "true"); 
//					return;
//				}

				//make sure they aren't targeting same container
				if (xx == FromCont)
				{
					from.LocalOverheadMessage( MessageType.Regular, 0x22, true, "The container to sort INTO must be different from the one you are sorting FROM."); 
					return;
				}

                if (xx == FromCont || xx.IsChildOf(FromCont))
                {
					from.LocalOverheadMessage( MessageType.Regular, 0x22, true, "You cannot sort INTO a subcontainer of the same container you are sorting FROM."); 
					return;
                }

				// gems are a special case 
				if (SortType == typeof(Diamond))
				{
	                Item[] itema =  FromCont.FindItemsByType( typeof(Amber), true );
	                Item[] itemb =  FromCont.FindItemsByType( typeof(Amethyst), true );
	                Item[] itemc =  FromCont.FindItemsByType( typeof(Citrine), true );
	                Item[] itemd =  FromCont.FindItemsByType( typeof(Diamond), true );
	                Item[] iteme =  FromCont.FindItemsByType( typeof(Emerald), true );
	                Item[] itemf =  FromCont.FindItemsByType( typeof(Ruby), true );
	                Item[] itemg =  FromCont.FindItemsByType( typeof(Sapphire), true );
	                Item[] itemh =  FromCont.FindItemsByType( typeof(StarSapphire), true );
	                Item[] itemi =  FromCont.FindItemsByType( typeof(Tourmaline), true );
					foreach (Item item in itema) 
					{
						if(!(xx.TryDropItem( from, item, false )))
							from.SendMessage("That container is too full!");
					}
					foreach (Item item in itema) 
					{
						if(!(xx.TryDropItem( from, item, false )))
							from.SendMessage("That container is too full!");
					}
					foreach (Item item in itemb) 
					{
						if(!(xx.TryDropItem( from, item, false )))
							from.SendMessage("That container is too full!");
					}
					foreach (Item item in itemc) 
					{
						if(!(xx.TryDropItem( from, item, false )))
							from.SendMessage("That container is too full!");
					}
					foreach (Item item in itemd) 
					{
						if(!(xx.TryDropItem( from, item, false )))
							from.SendMessage("That container is too full!");
					}
					foreach (Item item in iteme) 
					{
						if(!(xx.TryDropItem( from, item, false )))
							from.SendMessage("That container is too full!");
					}
					foreach (Item item in itemf) 
					{
						if(!(xx.TryDropItem( from, item, false )))
							from.SendMessage("That container is too full!");
					}
					foreach (Item item in itemg) 
					{
						if(!(xx.TryDropItem( from, item, false )))
							from.SendMessage("That container is too full!");
					}
					foreach (Item item in itemh) 
					{
						if(!(xx.TryDropItem( from, item, false )))
							from.SendMessage("That container is too full!");
					}
					foreach (Item item in itemi) 
					{
						if(!(xx.TryDropItem( from, item, false )))
							from.SendMessage("That container is too full!");
					}
				}
				// all other supported sort items have a base type
				else
				{
	                Item[] items =  FromCont.FindItemsByType( SortType, true );
					foreach (Item item in items) 
					{
	//					Console.WriteLine ("Sorttype=" + SortType + " and item is " + item + "");
						if(!(xx.TryDropItem( from, item, false )))
							from.SendMessage("That container is too full!");
					}
				}
			}
			else
			{
				from.LocalOverheadMessage( MessageType.Regular, 0x22, true, "That is not a container!"); 
			}
		}
	}	
  } 
} 