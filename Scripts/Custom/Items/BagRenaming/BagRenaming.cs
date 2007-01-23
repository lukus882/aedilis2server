using System;
using System.Collections;
using Server;
using Server.Targeting;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;
using Server.Prompts;
using System.IO;

namespace Server.Items
{
	public class BagRenaming : Item
	{
		[Constructable]
		public BagRenaming() : base( 0x1F14 )
		{
			Weight = 1.0;
			Name = "an In Bag Container Renaming Rune";
			Hue = 0x486;
		}

		public BagRenaming( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
			else
			{
				from.Target=  new RenameBagTarget(this);
				from.SendMessage( "What container do you wish to rename?" );
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

	public class RenameBagPrompt : Prompt
	{
		private Container m_bag;

		public RenameBagPrompt( Container bag )
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


	public class RenameBagTarget : Target
	{
		private Item m_Item;

		public RenameBagTarget( Item item ) : base( 10, false, TargetFlags.None )
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
					if ( !bag.IsChildOf( from.Backpack ) )
					{
						from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
					}
					else
					{
						from.SendMessage("Please enter the new name for the container.");
						from.Prompt = new RenameBagPrompt( bag );
						m_Item.Delete();
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