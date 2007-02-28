using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Misc;
using Server.Items;
using Server.Gumps;
using Server.Multis;
using Server.Engines.Help;
using Server.ContextMenus;
using Server.Network;
using Server.Spells;
using Server.Spells.Fifth;
using Server.Spells.Sixth;
using Server.Spells.Seventh;
using Server.Spells.Necromancy;
using Server.Spells.Ninjitsu;
using Server.Spells.Bushido;
using Server.Targeting;
using Server.Engines.Quests;
using Server.Factions;
using Server.Regions;
using Server.Accounting;
using Server.Engines.CannedEvil;
using Server.Engines.Craft;

namespace Server.Mobiles
{
	public class AedilisPlayerMobile : PlayerMobile
	{
		public AedilisPlayerMobile() : base()
		{
		}

		public AedilisPlayerMobile( Serial s ) : base( s )
		{
		}
		

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			

			switch ( version )
			{

				case 1:
                		{
                 		     m_Settings = new MobileSettings(this);
               			     m_Settings.Deserialize(reader);
               			     goto case 0;
               		        }
				case 0:
				{
					break;
				}
				
			}

		if (version < 1) m_Settings = new MobileSettings(this);

			
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 );

			m_Settings.Serialize(writer);
		}
	}
}