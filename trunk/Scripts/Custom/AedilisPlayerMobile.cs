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
	public partial class PlayerMobile
	{

		#region PopUp Toggle -- Storm Septmber '07

			public bool m_PopUpToggle;

			[CommandProperty( AccessLevel.GameMaster )]
			public bool PopUpToggle
			{
				get{ return m_PopUpToggle; }
				set
				{
					m_PopUpToggle = value;
				}
			}

		#endregion

		#region Travel Mages -- April 28th '07
			private int m_TravelTickets;

			[CommandProperty( AccessLevel.Administrator )]
			public int TravelTickets
			{
				get{ return m_TravelTickets; }
				set
				{
					m_TravelTickets = value;
				}
			}

		#endregion

		public void ExtendedDeserialize( GenericReader reader, int parentVerison )
		{
			int version = reader.ReadInt();

			switch (version)
			{

                                 case 3:
                                 {
					m_PopUpToggle = reader.ReadBool();
                                        goto case 2;
                                 }
                                 case 2:
                                 {
				 	Percent = reader.ReadDouble();
                                 	m_LevelTitle = reader.ReadString();
                                        m_PlayerLevel = reader.ReadInt();
                                        goto case 1;
                                 }

				case 1:
				{
					m_Settings = new MobileSettings(this);
					m_Settings.Deserialize(reader);
					goto case 0;
				}
				case 0:
				{
					m_TravelTickets = reader.ReadInt();
					break;
				}
			}
		}

		public void ExtendedSerialize( GenericWriter writer )
		{
			writer.Write( (int)3 ); //version

			//Alteration 3 // PopUpToggle
			writer.Write( (bool)m_PopUpToggle );

			//Alteration 2 Player Levels
			writer.Write( (double)Percent );
                        writer.Write( (String)m_LevelTitle );
                        writer.Write( m_PlayerLevel );

			//Alteration 1
                        m_Settings.Serialize(writer);

			//Alteration 0
			writer.Write( (int)m_TravelTickets );
		}
	}
}