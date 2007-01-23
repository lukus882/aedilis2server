using System;
using System.Collections;
using Server;
using Server.Targeting;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;
using Server.Prompts;
using System.IO;
using Server.Multis;

namespace Server.Items
{
	public class BagRenaming2 : Item
	{
		[Constructable]
		public BagRenaming2() : base( 0x1F14 )
		{
			Weight = 1.0;
			Name = "an In House Container Renaming Rune";
			Hue = 0x486;
		}

		public BagRenaming2( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
                        BaseHouse house = BaseHouse.FindHouseAt( from );
			if ( !( house == null || !house.IsCoOwner( from ) ) )
			{
				from.Target=  new RenameBagTarget2(this);
				from.SendMessage( "What container do you wish to rename?" );
			}
			else
			{
                            from.SendMessage( "You can only rename containers in houses that you own!" );
			}
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class RenameBagPrompt2 : Prompt
	{
		private Container m_bag;

		public RenameBagPrompt2( Container bag )
		{
			m_bag=bag;
		}

		public override void OnResponse( Mobile from, string text )
		{
			do
			{
				if (text == null)
				{
					from.SendMessage("You MUST enter a name for the new container!");
				}
				else
				{
					if (text != null)
					{
						m_bag.Name = text;
						from.SendMessage("The container has been renamed to : " + text);
						break;
					}
				}
			}while (text == null);
		}
	}


	public class RenameBagTarget2 : Target
	{
		private Item m_Item;

		public RenameBagTarget2( Item item ) : base( 10, false, TargetFlags.None )
		{
			m_Item = item;
		}

		protected override void OnTarget( Mobile from, object targeted)
		{
			if (targeted is Container)
			{
				if (targeted is Item)
				{
					Container bag = targeted as Container;
                                        BaseHouse house = BaseHouse.FindHouseAt( from );
					if ( !( house == null || !house.IsCoOwner( from ) ) )
					{
						from.SendMessage("Please enter the new name for the container.");
						from.Prompt = new RenameBagPrompt2( bag );
						m_Item.Delete();
					}
					else
					{
                                        from.SendMessage( "You can only rename containers in houses that you own!" );
					}
				}
			}
			else
			{
				from.SendMessage("You can only rename containers!");
			}
		}
	}
}