//11APR2007 QuestGiver - hide the thimble by RavonTUS@Yahoo.com *** START ***

using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Multis;
using Server.Gumps;

namespace Server.Mobiles
{
    public class QuestGiver : BaseHealer
    {
        private static bool m_Talked;
        private static bool m_ItemHasBeenReturned;
        private static int x;
        private static int y;
        private static int z;
        private string Sentence = "";

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            #region Here is the list of Random Words you can use
            string dat_Facet = TalkingNPCsXMLReader.RandomName("dat_Facet");
            string dat_TownRegion = TalkingNPCsXMLReader.RandomName("dat_TownRegion");
            string dat_DungeonRegion = TalkingNPCsXMLReader.RandomName("dat_DungeonRegion");
            string dat_NoHousingRegion = TalkingNPCsXMLReader.RandomName("dat_NoHousingRegion");
            string dat_Other = TalkingNPCsXMLReader.RandomName("dat_Other");
            string dat_Shrine = TalkingNPCsXMLReader.RandomName("dat_Shrine");
            string dat_article1 = TalkingNPCsXMLReader.RandomName("dat_article");
            string dat_article2 = TalkingNPCsXMLReader.RandomName("dat_article");
            string dat_noun1 = TalkingNPCsXMLReader.RandomName("dat_noun");
            string dat_noun2 = TalkingNPCsXMLReader.RandomName("dat_noun");
            string dat_noun3 = TalkingNPCsXMLReader.RandomName("dat_noun");
            string dat_noun4 = TalkingNPCsXMLReader.RandomName("dat_noun");
            string dat_verb1 = TalkingNPCsXMLReader.RandomName("dat_verb");
            string dat_verb2 = TalkingNPCsXMLReader.RandomName("dat_verb");
            string dat_verbing1 = TalkingNPCsXMLReader.RandomName("dat_verbing");
            string dat_verbing2 = TalkingNPCsXMLReader.RandomName("dat_verbing");
            string dat_verb3rd1 = TalkingNPCsXMLReader.RandomName("dat_verb3rd");
            string dat_verb3rd2 = TalkingNPCsXMLReader.RandomName("dat_verb3rd");
            string dat_verbed1 = TalkingNPCsXMLReader.RandomName("dat_verbed");
            string dat_verbed2 = TalkingNPCsXMLReader.RandomName("dat_verbed");
            string dat_preposition1 = TalkingNPCsXMLReader.RandomName("dat_preposition");
            string dat_preposition2 = TalkingNPCsXMLReader.RandomName("dat_preposition");
            string dat_adj1 = TalkingNPCsXMLReader.RandomName("dat_adj");
            string dat_adj2 = TalkingNPCsXMLReader.RandomName("dat_adj");
            string dat_Greeting = TalkingNPCsXMLReader.RandomName("dat_Greeting");
            string dat_Language1 = TalkingNPCsXMLReader.RandomName("dat_Language");
            string dat_Language2 = TalkingNPCsXMLReader.RandomName("dat_Language");
            string dat_Language3 = TalkingNPCsXMLReader.RandomName("dat_Language");
            string dat_Armor = TalkingNPCsXMLReader.RandomName("dat_Armor");
            string dat_Creature1 = TalkingNPCsXMLReader.RandomName("dat_Creature");
            string dat_Creature2 = TalkingNPCsXMLReader.RandomName("dat_Creature");
            string dat_Room1 = TalkingNPCsXMLReader.RandomName("dat_Room");
            string dat_Room2 = TalkingNPCsXMLReader.RandomName("dat_Room");
            string dat_Furniture1 = TalkingNPCsXMLReader.RandomName("dat_Furniture");
            string dat_Furniture2 = TalkingNPCsXMLReader.RandomName("dat_Furniture");
            string dat_Liquid1 = TalkingNPCsXMLReader.RandomName("dat_Liquid");
            string dat_Number1 = TalkingNPCsXMLReader.RandomName("dat_Number");
            string dat_PlayingCards = TalkingNPCsXMLReader.RandomName("dat_PlayingCards");
            string dat_MinocShop = TalkingNPCsXMLReader.RandomName("dat_MinocShop");
            string dat_MinocShopQuestItem = TalkingNPCsXMLReader.RandomName("dat_MinocShopQuestItem");
            #endregion

            #region NPC Welcomes Player
            if (m_Talked == false)
            {
                if (m.InRange(this, 3) && m is PlayerMobile)
                {
                    //The Welcome
                    switch (Utility.Random(3))  //picks one of the following
                    {
                        case 0:
                            {
                                //m.Name = char.ToUpper(m.Name[0]) + m.Name.Substring(1);
                                Sentence = String.Format("We have the {0} quest for you.", dat_adj1);
                                break;
                            }
                        case 1:
                            {
                                m.Name = char.ToUpper(m.Name[0]) + m.Name.Substring(1);
                                Sentence = String.Format("{0} the {1} found a {2}.", dat_Creature1, dat_noun1, dat_Armor);
                                break;
                            }
                        case 2:
                            {
                                Sentence = String.Format("{0} was missing from {1} and found near {2}", dat_Armor, dat_TownRegion, dat_DungeonRegion);
                                break;
                            }
                    }
                    m_Talked = true;
                    Say(Sentence, this);
                    this.Move(GetDirectionTo(m.Location));
                    QuestGiverTimer t = new QuestGiverTimer();
                    t.Start();
                }
            }
            #endregion
        }

        public override void OnSpeech(SpeechEventArgs e)
        {
            #region Here is the list of Random Words you can use
            string dat_Facet = TalkingNPCsXMLReader.RandomName("dat_Facet");
            string dat_TownRegion = TalkingNPCsXMLReader.RandomName("dat_TownRegion");
            string dat_DungeonRegion = TalkingNPCsXMLReader.RandomName("dat_DungeonRegion");
            string dat_NoHousingRegion = TalkingNPCsXMLReader.RandomName("dat_NoHousingRegion");
            string dat_Other = TalkingNPCsXMLReader.RandomName("dat_Other");
            string dat_Shrine = TalkingNPCsXMLReader.RandomName("dat_Shrine");
            string dat_article1 = TalkingNPCsXMLReader.RandomName("dat_article");
            string dat_article2 = TalkingNPCsXMLReader.RandomName("dat_article");
            string dat_noun1 = TalkingNPCsXMLReader.RandomName("dat_noun");
            string dat_noun2 = TalkingNPCsXMLReader.RandomName("dat_noun");
            string dat_noun3 = TalkingNPCsXMLReader.RandomName("dat_noun");
            string dat_noun4 = TalkingNPCsXMLReader.RandomName("dat_noun");
            string dat_verb1 = TalkingNPCsXMLReader.RandomName("dat_verb");
            string dat_verb2 = TalkingNPCsXMLReader.RandomName("dat_verb");
            string dat_verbing1 = TalkingNPCsXMLReader.RandomName("dat_verbing");
            string dat_verbing2 = TalkingNPCsXMLReader.RandomName("dat_verbing");
            string dat_verb3rd1 = TalkingNPCsXMLReader.RandomName("dat_verb3rd");
            string dat_verb3rd2 = TalkingNPCsXMLReader.RandomName("dat_verb3rd");
            string dat_verbed1 = TalkingNPCsXMLReader.RandomName("dat_verbed");
            string dat_verbed2 = TalkingNPCsXMLReader.RandomName("dat_verbed");
            string dat_preposition1 = TalkingNPCsXMLReader.RandomName("dat_preposition");
            string dat_preposition2 = TalkingNPCsXMLReader.RandomName("dat_preposition");
            string dat_adj1 = TalkingNPCsXMLReader.RandomName("dat_adj");
            string dat_adj2 = TalkingNPCsXMLReader.RandomName("dat_adj");
            string dat_Greeting = TalkingNPCsXMLReader.RandomName("dat_Greeting");
            string dat_Language1 = TalkingNPCsXMLReader.RandomName("dat_Language");
            string dat_Language2 = TalkingNPCsXMLReader.RandomName("dat_Language");
            string dat_Language3 = TalkingNPCsXMLReader.RandomName("dat_Language");
            string dat_Armor = TalkingNPCsXMLReader.RandomName("dat_Armor");
            string dat_Creature1 = TalkingNPCsXMLReader.RandomName("dat_Creature");
            string dat_Creature2 = TalkingNPCsXMLReader.RandomName("dat_Creature");
            string dat_Room1 = TalkingNPCsXMLReader.RandomName("dat_Room");
            string dat_Room2 = TalkingNPCsXMLReader.RandomName("dat_Room");
            string dat_Furniture1 = TalkingNPCsXMLReader.RandomName("dat_Furniture");
            string dat_Furniture2 = TalkingNPCsXMLReader.RandomName("dat_Furniture");
            string dat_Liquid1 = TalkingNPCsXMLReader.RandomName("dat_Liquid");
            string dat_Number1 = TalkingNPCsXMLReader.RandomName("dat_Number");
            string dat_PlayingCards = TalkingNPCsXMLReader.RandomName("dat_PlayingCards");
            string dat_MinocShop = TalkingNPCsXMLReader.RandomName("dat_MinocShop");
            string dat_MinocShopQuestItem = TalkingNPCsXMLReader.RandomName("dat_MinocShopQuestItem");
            #endregion

            string playername = e.Mobile.Name;
            string speech = e.Speech.ToLower();

            #region Player says "Hi"
            for (int i = 0; i < Greetings.Length; i++)
                if (speech == Greetings[i])
                {
                    switch (Utility.Random(4))  //picks one of the following
                    {
                        case 0:
                            { Say(String.Format("{0}, {1} found {2}, would you like to look for something too?", dat_Greeting, dat_Creature1, dat_Armor)); break; }
                        case 1:
                            { Say(String.Format("A quest for {0} {1}?  Just say 'YES'!", dat_adj1, playername)); break; }
                        case 2:
                            { Say(String.Format("{0} I have may quest for you, do you wish to join?", playername)); break; }
                        case 3:
                            { Say(String.Format("{0}, there are so many quest, just ask for one.", playername)); break; }
                    }
                }
            #endregion

            #region Play says "Yes" or some other response
            for (int i = 0; i < Response.Length; i++)
                if (speech == Response[i])
                {
                    if (m_ItemHasBeenReturned == true)
                    {
                        switch (Utility.Random(4))  //picks one of the following
                        {
                            case 0:
                                {
                                    playername = char.ToUpper(playername[0]) + playername.Substring(1);
                                    string TheQuest = String.Format("QUEST: (1 Hour Limit) - {0} is searching for a {1} near the {2} in Minoc.", playername, dat_MinocShopQuestItem, dat_MinocShop);
                                    World.Broadcast(0x35, true, TheQuest);
                                    e.Mobile.SendGump(new QuestTemplate(dat_MinocShopQuestItem, TheQuest));
                                    break;
                                }
                            case 1:
                                {
                                    string TheQuest = String.Format("QUEST: (1 Hour Limit) - Find the {0} near the {1} in Minoc and return it to {2}.", dat_MinocShopQuestItem, dat_MinocShop, playername);
                                    World.Broadcast(0x35, true, TheQuest);
                                    e.Mobile.SendGump(new QuestTemplate(dat_MinocShopQuestItem, TheQuest));
                                    break;
                                }
                            case 2:
                                {
                                    string TheQuest = String.Format("QUEST: (1 Hour Limit) - Help {0} in Minoc find the {1} near the {2}.", playername, dat_MinocShopQuestItem, dat_MinocShop);
                                    World.Broadcast(0x35, true, TheQuest);
                                    e.Mobile.SendGump(new QuestTemplate(dat_MinocShopQuestItem, TheQuest));
                                    break;
                                }
                            case 3:
                                {
                                    string TheQuest = String.Format("QUEST: (1 Hour Limit) - Lost in Minoc, is a {0}.  Said to be near the {1}, seek out {2} for more information.", dat_MinocShopQuestItem, dat_MinocShop, playername);
                                    World.Broadcast(0x35, true, TheQuest);
                                    e.Mobile.SendGump(new QuestTemplate(dat_MinocShopQuestItem, TheQuest));
                                    break;
                                }
                        }

                        //Choose Item Location
                        #region Randomly Pick Location
                        switch (dat_MinocShop)  //picks one of the following
                        {
                            case "Warrior's Battle Gear":
                                x = 2525;
                                y = 578;
                                z = 0;
                                break;

                            case "Healing Hand":
                                x = 2576;
                                y = 603;
                                z = 0;
                                break;

                            case "Bank of Minoc":
                                x = 2505;
                                y = 560;
                                z = 0;
                                break;

                            case "The Forgery":
                                x = 2470;
                                y = 569;
                                z = 5;
                                break;

                            case "The Slaughtered Cow":
                                x = 2472;
                                y = 458;
                                z = 15;
                                break;

                            case "Gears and Gadgets":
                                x = 2472;
                                y = 458;
                                z = 15;
                                break;

                            case "The Oak Throne":
                                x = 2510;
                                y = 482;
                                z = 15;
                                break;

                            case "The Stretched Hide":
                                x = 2523;
                                y = 536;
                                z = 0;
                                break;

                            case "The Barnacle":
                                x = 2470;
                                y = 413;
                                z = 15;
                                break;

                            case "Minoc Town Hall":
                                x = 2428;
                                y = 536;
                                z = 0;
                                break;

                            case "The Mystical Lute":
                                x = 2432;
                                y = 555;
                                z = 0;
                                break;

                            case "The New World Order":
                                x = 2482;
                                y = 440;
                                z = 15;
                                break;

                            case "The Old Miners' Supplies":
                                x = 2459;
                                y = 432;
                                z = 15;
                                break;

                            case "The Golden Pick Axe":
                                x = 2500;
                                y = 440;
                                z = 0;
                                break;

                            case "The Survival Shop":
                                x = 2534;
                                y = 560;
                                z = 0;
                                break;

                            case "The Matewan":
                                x = 2459;
                                y = 492;
                                z = 15;
                                break;

                            case "Stables":
                                x = 2521;
                                y = 379;
                                z = 23;
                                break;

                            case "Counselor's Guild":
                                x = 2436;
                                y = 443;
                                z = 15;
                                break;

                            default:  //if all else fails drop item near by
                                {
                                    x = Location.X + Utility.RandomMinMax(-5, 5);
                                    y = Location.Y + Utility.RandomMinMax(-5, 5);
                                    z = Map.GetAverageZ(x, y);
                                    break;
                                }
                        #endregion

                        }

                        x = x + Utility.RandomMinMax(-5, 5);
                        y = y + Utility.RandomMinMax(-5, 5);

                        if (Map.CanFit(x, y, Location.Z, 1))
                        {
                            Item item = new OrnateAxe();
                            item.Name = dat_MinocShopQuestItem;
                            item.MoveToWorld(new Point3D(x, y, Location.Z), Map);
                            m_ItemHasBeenReturned = false;
                            QuestGiverItemTimer t = new QuestGiverItemTimer();
                            t.Start();
                        }
                        else if (Map.CanFit(x, y, z, 1))
                        {
                            Item item = new OrnateAxe();
                            item.Name = dat_MinocShopQuestItem;
                            item.MoveToWorld(new Point3D(x, y, z), Map);
                            m_ItemHasBeenReturned = false;
                            QuestGiverItemTimer t = new QuestGiverItemTimer();
                            t.Start();
                        }
                    }
                }
            #endregion
        }

        public override bool OnDragDrop(Mobile from, Item item)
        {
            #region Player dropped Quest Item
            if (m_ItemHasBeenReturned == false)
            {
                if (item is OrnateAxe)
                {
                    string TheQuest = String.Format("{0} has found and returned the {1}.", from.Name, item.Name);
                    World.Broadcast(0x35, true, TheQuest);
                    m_ItemHasBeenReturned = true;
                    from.Backpack.AddItem(new Gold(150));
                    return true;
                }
            }
            Say(String.Format("Sorry, I am not looking for that."));
            return false;
            #endregion
        }

        #region List of Response Words
        public static string[] Greetings = new string[]
            {	"hello",
	            "hey",
	            "hi",
                "hiya",
	            "sup",
	            "howdy",
                "greetings",
	            "bonjour"
            };

        public static string[] Response = new string[]
            {	"yes",
	            "ya",
	            "y",
            	"no",
	            "na",
	            "n",
            	"who",
	            "what",
	            "when",
	            "where",
	            "how", 
                "find",
                "quest",
	            "understand"
            };
        #endregion

        #region Setup for OnSpeech
        public override bool HandlesOnSpeech(Mobile from)
        {
            if (from.InRange(this.Location, 3))
                return true;

            return base.HandlesOnSpeech(from);
        }
        #endregion

        #region QuestGiverTimer
        private class QuestGiverTimer : Timer
        {
            public QuestGiverTimer()
                : base(TimeSpan.FromSeconds(30))
            {
                Priority = TimerPriority.OneMinute;
            }

            protected override void OnTick()
            {
                m_Talked = false;
            }
        }
        #endregion

        #region QuestGiverItemTimer
        private class QuestGiverItemTimer : Timer
        {
            public QuestGiverItemTimer()
                : base(TimeSpan.FromMinutes(5))
            {
                Priority = TimerPriority.OneMinute;
            }

            protected override void OnTick()
            {
                m_ItemHasBeenReturned = true;
            }
        }
        #endregion

        #region Build the NPC
        public override bool CanTeach { get { return false; } }

        //public override bool CheckTeach( SkillName skill, Mobile from )
        //{
        //    if ( !base.CheckTeach( skill, from ) )
        //        return false;

        //    return ( skill == SkillName.Anatomy )
        //        || ( skill == SkillName.Healing )
        //        || ( skill == SkillName.Forensics )
        //        || ( skill == SkillName.SpiritSpeak );
        //}

        [Constructable]
        public QuestGiver()
        {
            Title = "the Quest Giver";
            m_ItemHasBeenReturned = true;

            SetSkill(SkillName.Anatomy, 85.0, 100.0);
            SetSkill(SkillName.Healing, 90.0, 100.0);
            SetSkill(SkillName.Forensics, 75.0, 98.0);
            SetSkill(SkillName.SpiritSpeak, 65.0, 88.0);

            Body = 0x191;
            
        }

        public override bool IsActiveVendor { get { return false; } }
        public override bool IsInvulnerable { get { return false; } }

        //public override void InitSBInfo()
        //{
        //    SBInfos.Add( new SBMage() );
        //    SBInfos.Add( new SBQuestGiver() );
        //}

        public override int GetRobeColor()
        {
            return RandomBrightHue();
        }

        public override void InitOutfit()
        {
            base.InitOutfit();

            switch (Utility.Random(3))
            {
                case 0: AddItem(new SkullCap(RandomBrightHue())); break;
                case 1: AddItem(new WizardsHat(RandomBrightHue())); break;
                case 2: AddItem(new Bandana(RandomBrightHue())); break;
            }

            AddItem(new Spellbook());
        }

        public QuestGiver(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
        #endregion
}