///////////////////////////////////////////
// C# Exporter Generated: 2/18/2007 12:56:42 PM
//
// Designed by Ravenal of OrBSydia
// Version: 2.1
//
// Script: SkillTitlesGump.cs
///////////////////////////////////////////

using System;
using System.Collections.Generic;
using Server;
using Server.Network;
using Server.Accounting;
using Server.Mobiles;
using Server.Misc;
using Server.HuePickers;
using Server.Gumps;

namespace Server
{
    public class SkillTitles
    {
        public static void Initialize()
		{
			Commands.CommandSystem.Register( "SkillTitles", AccessLevel.Player, new Commands.CommandEventHandler( OnSkillTitles ) );
		}

        public static bool Enabled = true;

        public const double MininumSkill = 100.0;
        public const int DefaultLegendaryHue = 37;
        public const int DefaultElderHue = 5;
        public const int DefaultGrandmasterHue = 70;
        public const int DefaultNormalHue = 1071;

        public const double LegendarySkill = 120.0;
        public const double ElderSkill = 110.0;
        public const double GrandmasterSkill = 100.0;

        private static void OnSkillTitles(Commands.CommandEventArgs e)
        {
            Mobile m = e.Mobile;
            if (m is PlayerMobile)
            {
                m.SendGump(new SkillTitlesGump((PlayerMobile)m, (int)SkillTitlesGump.Page.Titles));
            }
        }
    }
}

namespace Server.Gumps
{
    public class SkillTitlesGump : Gump
    {
        public enum Page
        {
            Options = 9,
            Titles = 10
        }

        private class InternalPicker : HuePicker
		{
            private TimerStateCallback m_Callback;

			public InternalPicker( TimerStateCallback callback ) : base( 0x1018 )
			{
                m_Callback = callback;
			}

			public override void OnResponse( int hue )
			{
                m_Callback.Invoke(hue);
			}
		}

        protected Dictionary<int, Skill[]> BuildList(PlayerMobile m)
        {
            Dictionary<int, Skill[]> list = new Dictionary<int, Skill[]>();

            int page = 1;
            int i = 0;
            int index = 0;

            List<Skill> skills = new List<Skill>();
            while( index < m.Skills.Length )
            {                
                Skill skill = m.Skills[index];                

                if (skill != null && skill.BaseFixedPoint >= 300)
                {
                    skills.Add(skill);
                    i++;
                }

                if (i == (page * 7))
                {                  
                    List<Skill> finalize = new List<Skill>();

                    foreach (Skill s in skills)
                        finalize.Add(s);

                    list[(page - 1) + (int)Page.Titles] = finalize.ToArray();

                    page++;
                    skills.Clear();
                }
                
                if ( (index + 1) == m.Skills.Length )
                {
                    if (skills.Count != 0)
                        list[(page - 1) + (int)Page.Titles] = skills.ToArray();
                }

                index++;
            }

            return list;
        }

        private static Skill GetHighestSkill( Mobile m )
		{
			Skills skills = m.Skills;

			if ( !Core.AOS )
				return skills.Highest;

			Skill highest = null;

			for ( int i = 0; i < m.Skills.Length; ++i )
			{
				Skill check = m.Skills[i];

				if ( highest == null || check.BaseFixedPoint > highest.BaseFixedPoint )
					highest = check;
				else if ( highest != null && highest.Lock != SkillLock.Up && check.Lock == SkillLock.Up && check.BaseFixedPoint == highest.BaseFixedPoint )
					highest = check;
			}

			return highest;
		}

        private Skill[] x_CurrentSkillsOnPage;

        protected void PopulateSkills(PlayerMobile m, int page)
        {
            // the skills that is stored by pages and higher than 300
            Dictionary<int, Skill[]> list = BuildList(m);


            int y = 0;

            Skill currentskill = m.Skills[m.Settings.SkillTitle];
            string skillLevel = "";
            string skillTitle = "No Skill Achieved";

            if (list.Count != 0)
            {
                if (currentskill != null && currentskill.BaseFixedPoint < 300)
                {
                    currentskill = GetHighestSkill(m);
                }

                skillLevel = Titles.GetSkillLevel(currentskill);
                skillTitle = currentskill.Info.Title;

                if ( m.Female && skillTitle.EndsWith( "man" ) )
			        skillTitle = skillTitle.Substring( 0, skillTitle.Length - 3 ) + "woman";
            }

            if (list.Count == 0)
            {
                // Since no data is found then we just set the page 1
                AddButton(25, 320, 4026, 4027, (int)Page.Options, GumpButtonType.Reply, 0); // Previous Page
            
                AddLabel(20, 98, 1071, @"Current Title - "); // Current Title - 
                AddImageTiled(19, 123, 325, 2, 96); 
                AddLabel(119, 98, 70, String.Format("{0} {1}", skillLevel, skillTitle) ); // Skill Title

                AddLabel(65, 320, m.Settings.LegendaryHue, @"Legendary"); // Legendary
                AddLabel(155, 320, m.Settings.ElderHue, @"Elder"); // Elder
                AddLabel(210, 320, m.Settings.GrandmasterHue, @"Grandmaster"); // Grandmaster
            
                AddLabel(55, 130 + y, 1071, @"There are currently no Skills to choose."); // - Legendary Necromancer
            }
            else
            {
                if (list.ContainsKey(page))
                {
                    Skill[] skills = list[page];
                    x_CurrentSkillsOnPage = list[page];

                    AddLabel(20, 98, 1071, @"Current Title - "); // Current Title - 
                    AddImageTiled(19, 123, 325, 2, 96);
                    AddLabel(119, 98, 70, String.Format("{0} {1}", skillLevel, skillTitle)); // Skill Title

                    AddLabel(65, 320, m.Settings.LegendaryHue, @"Legendary"); // Legendary
                    AddLabel(155, 320, m.Settings.ElderHue, @"Elder"); // Elder
                    AddLabel(210, 320, m.Settings.GrandmasterHue, @"Grandmaster"); // Grandmaster

                    if( list.Count > ((page+1)-(int)Page.Titles) )
                        AddButton(305, 320, 4005, 4006, page + 1, GumpButtonType.Reply, 0); // Next Page

                    AddButton(25, 320, (page == (int)Page.Titles) ? 4026 : 4014, (page == (int)Page.Titles) ? 4027 : 4015, (page == (int)Page.Options) ? (int)Page.Options : page - 1, GumpButtonType.Reply, 0); // Previous Page
                                      
                    for (int i = 0; i < skills.Length; i++)
                    {
                        Skill skill = skills[i];

                        if (skill.Base >= SkillTitles.MininumSkill)
                        {
                            // Skill Title
                            string slevel = Titles.GetSkillLevel(skill);
                            string stitle = skill.Info.Title;

                            if (m.Female && stitle.EndsWith("man"))
                                stitle = stitle.Substring(0, stitle.Length - 3) + "woman";

                            AddLabel(55, 130 + y, 1071, String.Format("- {0} {1}", slevel, stitle)); // - Legendary Necromancer
                            AddButton(20, 129 + y, 4005, 4006, 100 + i, GumpButtonType.Reply, 0); // Select Title

                            int hue = 0;

                            if (skill.Value >= SkillTitles.LegendarySkill)
                                hue = m.Settings.LegendaryHue;
                            else if (skill.Value >= SkillTitles.ElderSkill)
                                hue = m.Settings.ElderHue;
                            else if (skill.Value >= SkillTitles.GrandmasterSkill)
                                hue = m.Settings.GrandmasterHue;
                            else
                                hue = m.Settings.NormalHue;

                            AddLabel(255, 130 + y, hue, String.Format("{0:F1}", skill.Base)); // Base Value Skill  

                            hue = 0;


                            if (skill.Cap >= SkillTitles.LegendarySkill)
                                hue = m.Settings.LegendaryHue;
                            else if (skill.Cap >= SkillTitles.ElderSkill)
                                hue = m.Settings.ElderHue;
                            else if (skill.Cap >= SkillTitles.GrandmasterSkill)
                                hue = m.Settings.GrandmasterHue;
                            else
                                hue = m.Settings.NormalHue;

                            AddLabel(305, 130 + y, hue, String.Format("{0:F1}", skill.Cap)); // Cap Skill
                            AddLabel(290, 130 + y, 1071, @"/"); // '/'

                            y += 25;
                        }
                    }
                }
            }
        }

        private PlayerMobile m_Player;

        public SkillTitlesGump( PlayerMobile m, int page )
            : base(50, 50)
        {
            m.CloseGump(typeof(SkillTitlesGump));
            m_Player = m;

            Closable = true;
            Disposable = true;
            Dragable = true;

            AddPage(0);
            AddBackground(5, 5, 350, 350, 83); // Background
            AddLabel(145, 18, 1071, @"Skill Titles"); // Skill Titles
            AddImageTiled(18, 90, 325, 2, 96); // Tiled Image 1            
            AddImage(20, 20, 5547); // Image 1
            AddImage(280, 20, 5561); // Copy of Image 1
            AddImage(90, 35, 10452); // Image 2
            AddImageTiled(16, 315, 325, 2, 96); // Copy of Copy of Tiled Image 1 

            if( page == (int)Page.Options )
            {
                int leghue = m.Settings.LegendaryHue;
                int eldhue = m.Settings.ElderHue;
                int gmhue = m.Settings.GrandmasterHue;
                int norhue = m.Settings.NormalHue;

                AddLabel(132, 92, 5, @"Option Settings"); // Option Settings
                AddLabel(63, 115, leghue, @"Legendary"); // Legendary
                AddImageTiled(20, 114, 325, 2, 96); // Copy of Tiled Image 1

                AddButton(100, 320, 246, 244, 1, GumpButtonType.Reply, 0); // Default Set
                AddButton(200, 320, 238, 239, 2, GumpButtonType.Reply, 0); // Apply Options

                AddBackground(37, 133, 125, 24, 9350); // Background 226
                AddTextEntry(40, 135, 118, 20, leghue, 0, leghue.ToString()); // Text Entry 1
                AddLabel(78, 160, eldhue, @"Elder"); // Elder
                AddBackground(37, 178, 125, 24, 9350); // Copy of Background 226
                AddTextEntry(40, 180, 118, 20, eldhue, 1, eldhue.ToString()); // Copy of Text Entry 1
                AddLabel(214, 113, gmhue, @"Grandmaster"); // Grandmaster
                AddBackground(198, 131, 125, 24, 9350); // Copy of Copy of Background 226
                AddTextEntry(201, 133, 118, 20, gmhue, 2, gmhue.ToString()); // Copy of Copy of Text Entry 1
                AddLabel(233, 160, norhue, @"Normal"); // Normal
               
                AddBackground(199, 178, 125, 24, 9350); // Copy of Copy of Copy of Background 226
                AddTextEntry(202, 180, 118, 20, norhue, 3, norhue.ToString()); // Copy of Copy of Copy of Text Entry 1
                
                AddLabel(69, 249, 1071, @"Display the skill Title before others"); // Display the skill Title before others
                AddCheck(33, 245, 2151, 2154, m.Settings.DisplaySkillTitles, 0); // Radio Button 2

                AddButton(305, 320, 4005, 4006, (int)Page.Titles, GumpButtonType.Reply, 0); // Next Page                

                AddButton(170, 135, 5603, 5607, 3, GumpButtonType.Reply, 0); // Legendary Hue Change
                AddButton(170, 185, 5603, 5607, 4, GumpButtonType.Reply, 0); // Elder Hue Change
                AddButton(330, 135, 5603, 5607, 5, GumpButtonType.Reply, 0); // Grandmaster Hue Change
                AddButton(330, 185, 5603, 5607, 6, GumpButtonType.Reply, 0); // Normal Hue Change
            }   
            else if( page >= (int)Page.Titles )
            {
                PopulateSkills(m, page); 
            }              
		}

        public override void OnResponse( NetState sender, RelayInfo info )
        {
            int button = info.ButtonID;

            Mobile m = sender.Mobile;
            if (m is PlayerMobile)
            {
                PlayerMobile pm = (PlayerMobile)m;  

                if (button >= (int)Page.Titles && button < 100 )
                {
                    // if buttons are higher than Page.Titles (10) and below 100 then we do
                    // GoToPage( pm, button ); where button = page + 1 or page - 1, if it is 
                    // page - 1 = 9 then it'll skip to the else, but since its not above 100, then
                    // it'll go to the switch case
                    GoToPage(pm, button);
                    return;
                }
                else if (button >= 100)
                {
                    Skill skill = x_CurrentSkillsOnPage[button - 100];

                    if (skill != null)
                    {
                        pm.SendMessage("You have selected, {0} as your title!", skill.Name);
                        pm.Settings.SkillTitle = skill.SkillName;
                    }
                }

                switch( button )
                {
		            // Set Default
		            case 1:                        
                        pm.Settings.DisplaySkillTitles = true;
                        pm.Settings.LegendaryHue = SkillTitles.DefaultLegendaryHue;
                        pm.Settings.ElderHue = SkillTitles.DefaultElderHue;
                        pm.Settings.GrandmasterHue = SkillTitles.DefaultGrandmasterHue;
                        pm.Settings.NormalHue = SkillTitles.DefaultNormalHue;

                        pm.SendAsciiMessage(0x35, "Default Settings for the Skill Titles was reset.");
                        GoToPage(pm, (int)Page.Options);
                    break;
		            // Apply Options
                    case 2:      
                        pm.Settings.DisplaySkillTitles = info.IsSwitched( 0 );                        
                        pm.Settings.LegendaryHue = Utility.ToInt32( info.GetTextEntry(0).Text );
                        pm.Settings.ElderHue = Utility.ToInt32( info.GetTextEntry(1).Text );
                        pm.Settings.GrandmasterHue = Utility.ToInt32( info.GetTextEntry(2).Text );
                        pm.Settings.NormalHue = Utility.ToInt32( info.GetTextEntry(3).Text );

                        pm.SendAsciiMessage(0x35, "Settings we're applied.");    
                        GoToPage(pm, (int)Page.Options);
                    break;  
                    case 3:
                        pm.SendHuePicker(new InternalPicker(new TimerStateCallback(LegendaryHue)));
                    break;
                    case 4:
                        pm.SendHuePicker(new InternalPicker(new TimerStateCallback(ElderHue)));
                     break;
                    case 5:
                        pm.SendHuePicker(new InternalPicker(new TimerStateCallback(GrandmasterHue)));
                    break;
                    case 6:
                        pm.SendHuePicker(new InternalPicker(new TimerStateCallback(NormalHue)));
                     break;
                    // Options Page, This will tell that the Page we're going to is Page 9, IE Options
                    case 9:
                        GoToPage(pm, (int)Page.Options);     				
		            break;                
                }
            } 
        }

        private void LegendaryHue( object obj )
		{
			if ( !(obj is int) )
				return;

			m_Player.Settings.LegendaryHue = (int)obj;

            GoToPage(m_Player, (int)Page.Options);
		}

        private void ElderHue( object obj )
		{
			if ( !(obj is int) )
				return;

			m_Player.Settings.ElderHue = (int)obj;

            GoToPage(m_Player, (int)Page.Options);
		}

        private void GrandmasterHue( object obj )
		{
			if ( !(obj is int) )
				return;

			m_Player.Settings.GrandmasterHue = (int)obj;

            GoToPage(m_Player, (int)Page.Options);
		}

        private void NormalHue( object obj )
		{
			if ( !(obj is int) )
				return;

			m_Player.Settings.NormalHue = (int)obj;

            GoToPage(m_Player, (int)Page.Options);
		}

        protected void GoToPage(PlayerMobile m, int page)
        {
            m.SendGump(new SkillTitlesGump(m, page));
        }
    }
}
