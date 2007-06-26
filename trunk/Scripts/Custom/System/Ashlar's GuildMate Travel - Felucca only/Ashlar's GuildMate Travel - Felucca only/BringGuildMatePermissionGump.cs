using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Mobiles;
using Server.Targeting;
using Server.Prompts;
using Server.Regions;
using Server.Spells;

namespace Server.Guilds
{
	public class BringGuildMatePermissionGump : BaseGuildGump
	{
		PlayerMobile m_Member;
		PlayerMobile m_pm;

		public BringGuildMatePermissionGump( PlayerMobile member, Guild g, PlayerMobile pm ) : base( pm, g, 10, 40 )
		{
			m_Member = (PlayerMobile)member;
			m_pm = (PlayerMobile)pm;
			PopulateGump();
		}

		public override void PopulateGump()
		{
			AddPage( 0 );

			AddBackground( 0, 0, 350, 195, 0x242C );
			AddHtmlLocalized( 20, 15, 310, 26, 1063018, 0x0, false, false ); // <div align=center><i>Will you goto this guildmate?</i></div>
			AddImageTiled( 20, 40, 225, 2, 0x2711 );
			
			AddHtmlLocalized( 20, 50, 150, 26, 1062955, 0x0, true, false ); // <i>Name</i>
			AddHtml( 180, 53, 150, 26, m_pm.Name, false, false );
			
			AddHtmlLocalized( 20, 80, 150, 26, 1062956, 0x0, true, false ); // <i>Rank</i>
			AddHtmlLocalized( 180, 83, 150, 26, m_pm.GuildRank.Name, 0x0, false, false );
			
			AddHtmlLocalized( 20, 110, 150, 26, 1062953, 0x0, true, false ); // <i>Guild Title</i>
			AddHtml( 180, 113, 150, 26, m_pm.GuildTitle, false, false );
			AddImageTiled( 20, 142, 310, 2, 0x2711 );

			AddBackground( 20, 150, 150, 26, 0x2486 );
			AddButton( 25, 155, 0x845, 0x846, 1, GumpButtonType.Reply, 0 );
			AddHtml( 50, 153, 110, 26, "Take me there.", false, false );
			
			AddBackground( 180, 150, 150, 26, 0x2486 );
			AddButton( 185, 155, 0x845, 0x846, 2, GumpButtonType.Reply, 0 );
			AddHtml( 210, 153, 110, 26, "Leave me here.", false, false );
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			PlayerMobile m_Member = sender.Mobile as PlayerMobile;
			
			if( m_pm == null || m_Member == null || !IsMember( m_pm, guild ) || !IsMember( m_Member, guild ) )
				return; // If either of the two mobiles are null then exit.

			switch( info.ButtonID )
			{
				case 1:	//Yes
				{
					if ( Server.Spells.SpellHelper.CheckCombat( m_Member ) )
					{
					m_pm.SendMessage( "You cannot travel while fighting. 1 " );
					m_Member.SendMessage( "Your guildmate is in combat and can not travel at the moment.2 " );
					}

					else if ( Server.Misc.WeightOverloading.IsOverloaded( m_Member ))
					{ 
					m_Member.SendMessage("You are overloaded and can not travel until you lose some weight. 1");
					m_pm.SendMessage( "Your guildmate is overloaded and can not travel until they lose some weight. 2" );
					}

					else if ( m_pm.Criminal )
					{
					m_pm.SendMessage( "You can not use this system when one of the members is a criminal. 1 " );
					m_Member.SendMessage( "You can not use this system when one of the members is a criminal. 2 " );
					}

					else if ( m_Member.Criminal )
					{
					m_pm.SendMessage( "You can not use this system when one of the members is a criminal. 1 " );
					m_Member.SendMessage( "You can not use this system when one of the members is a criminal. 2 " );
					}

					else if ( Server.Spells.SpellHelper.CheckCombat( m_pm ) )
					{
					m_pm.SendMessage( "You can not use this system while somebody is in combat. 1 " );
					m_Member.SendMessage( "You can not use this system while somebody is in combat. 2 " );
					}

					else if ( Server.Spells.SpellHelper.CheckCombat( m_member ) )
					{
					m_pm.SendMessage( "You can not use this system while somebody is in combat. 1 " );
					m_Member.SendMessage( "You can not use this system while somebody is in combat. 2 " );
					}

					else if (!m_pm.Alive)  
					{ 
					m_pm.SendMessage("You are dead and lack the power to summon your guildmate at this time 1");
					m_Member.SendMessage( "Your Guildmate is dead and can not use his powers to summon you at this time. 2" );
					}

					else if (m_pm.Region is DungeonRegion)  
					{ 
					m_Member.SendMessage("Your guildmate is in a dungeon and you can not transport there at this time. 1 ");
					m_pm.SendMessage( "The mystical powers of this dungeon prevent your guildmate from joining you. 2 ");
					} 

					else if (m_Member.Region is DungeonRegion)  
					{ 
					m_Member.SendMessage("The mystical powers of this dungeon prevent you from joining your guildmate.1 ");
					m_pm.SendMessage( "Your guildmate is in a dungeon and is blocked from joining you at this time. 2 " );
					}

					else
					{

						
                   				if ( m_pm.Map == Map.Felucca && m_Member.Map == Map.Felucca )
						{
							

							m_pm.RevealingAction();
							m_Member.RevealingAction();
							Map map = m_pm.Map;
							Point3D loc = m_pm.Location;

							m_Member.MoveToWorld( loc, map );
						}
						else
						{
							m_Member.SendMessage( m_pm.Name +" is in "+ m_pm.Map as string +"." );
                        				m_Member.SendMessage( "You are in "+ m_Member.Map as string +"." );
                        				m_Member.SendMessage( "You both need to be in Felucca to travel." );
							m_pm.SendMessage( m_Member.Name +" is in "+ m_Member.Map as string +"." );
                       					m_pm.SendMessage( "You are in "+ m_pm.Map as string +"." );
                        				m_pm.SendMessage( "You both need to be in Felucca to travel." );
						}
					}
					
					break;
				}
				case 2:	//No
				{
					m_Member.SendMessage( m_pm.Name +" has been informed that you are not available." );
					m_pm.SendMessage( m_Member.Name +" does not wish to travel to you at this time." );
					break;
				}
			}
		}
	}
}