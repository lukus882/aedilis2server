//19APR2007 Professor - a random quest generator by RavonTUS@Yahoo.com
//Play at An Nox, the cure for the UO addiction
//annox.no-ip.com

using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Multis;

namespace Server.Mobiles
{
    public class Professor : BaseHealer
    {
        private static bool m_Talked;
        private static bool m_ItemHasBeenReturned;
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

            #region NPC welcomes Player
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
                                Sentence = String.Format("What would happen if I mixed {0} with {1}?", dat_noun1, dat_noun2);
                                break;
                            }
                        case 1:
                            {
                                m.Name = char.ToUpper(m.Name[0]) + m.Name.Substring(1);
                                Sentence = String.Format("This {0} potions will {1} the {2}", dat_adj1, dat_verb1, dat_DungeonRegion);
                                break;
                            }
                        case 2:
                            {
                                Sentence = String.Format("After {0} my microscope, I can {2} the {2}.", dat_verb1, dat_verb2, dat_Other);
                                break;
                            }
                    }
                    m_Talked = true;
                    Say(Sentence, this);
                    this.Move(GetDirectionTo(m.Location));
                    ProfessorTimer t = new ProfessorTimer();
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
                            { Say(String.Format("{0} {1}, I am {2} a {3} do you want to assist?", dat_Greeting, playername, dat_verb1, dat_Creature1)); break; }
                        case 1:
                            { Say(String.Format("That {0} bottle. Oh, {1} {2}!", dat_adj1, dat_Greeting, playername)); break; }
                        case 2:
                            { Say(String.Format("{0}{1} are so..ah, uhm. {2} {3}", dat_adj1, dat_Creature1, dat_Greeting, playername)); break; }
                        case 3:
                            { Say(String.Format("{0} {1} {2}, AGH! My spell! {3} you are so distracting.", dat_Language1, dat_Language2, dat_Language3, playername)); break; }
                    }
                }
            #endregion

            #region Player says "Yes" or some other response
            for (int i = 0; i < Response.Length; i++)
                if (speech == Response[i])
                {
                    if (m_ItemHasBeenReturned == true)
                    {
                        switch (Utility.Random(4))  //picks one of the following
                        {
                            case 0:
                                {
                                    Say(String.Format("{0} help me find my {1} bottle.  It should be around here somewhere, just hand it to me.", playername, dat_noun1));
                                    break;
                                }
                            case 1:
                                {
                                    dat_article1 = char.ToUpper(dat_article1[0]) + dat_article1.Substring(1);
                                    Say(String.Format("{0} somewhere around here I have a {1} bottle.", playername, dat_noun1));
                                    break;
                                }
                            case 2:
                                {
                                    dat_article1 = char.ToUpper(dat_article1[0]) + dat_article1.Substring(1);
                                    Say(String.Format("I am {0} a {1} potion and need my {2} bottle.", dat_verb1, dat_adj1, dat_noun1));
                                    break;
                                }
                            case 3:
                                {
                                    dat_article1 = char.ToUpper(dat_article1[0]) + dat_article1.Substring(1);
                                    Say(String.Format("Maybe some blood of {0} would work.  No I need a bottle of {1}.", dat_Creature1, dat_noun1));
                                    break;
                                }
                        }


                        //Choose Item Location
                        int x = Location.X + Utility.RandomMinMax(-5, 5);
                        int y = Location.Y + Utility.RandomMinMax(-5, 5);
                        int z = Map.GetAverageZ(x, y);

                        if (Map.CanFit(x, y, Location.Z, 1))
                        {
                            Item item = new Bottle();
                            item.Name = String.Format("{0} bottle", dat_noun1);
                            item.MoveToWorld(new Point3D(x, y, Location.Z), Map);
                            m_ItemHasBeenReturned = false;
                            ProfessorItemTimer t = new ProfessorItemTimer();
                            t.Start();
                        }
                        else if (Map.CanFit(x, y, z, 1))
                        {
                            Item item = new Bottle();
                            item.Name = String.Format("{0} bottle", dat_noun1);
                            item.MoveToWorld(new Point3D(x, y, z), Map);
                            m_ItemHasBeenReturned = false;
                            ProfessorItemTimer t = new ProfessorItemTimer();
                            t.Start();
                        }
                    }
                    else
                    {
                        Say(String.Format("A bottle, {0} or {1}.  Hurry any bottle will due!", dat_adj1, dat_noun1));
                    }
                }
            #endregion
        }

        public override bool OnDragDrop(Mobile from, Item item)
        {
            #region Player dropped Quest Item
            if (item is Bottle)
            {
                Say(String.Format("Thank you."));
                m_ItemHasBeenReturned = true;
                return true;
            }

            return base.OnDragDrop(from, item);
            #endregion
        }

        #region List of Response Words
        public static string[] Greetings = new string[]
            {	"hello",
	            "hey",
	            "hi",
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
                "bottle",
                "quest",
                "look",
                "assist",
                "bottle",
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

        #region ProfessorTimer
        private class ProfessorTimer : Timer
        {
            public ProfessorTimer()
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

        #region ProfessorItemTimer
        private class ProfessorItemTimer : Timer
        {
            public ProfessorItemTimer()
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
        public Professor()
        {
            Title = "the Professor";
            m_ItemHasBeenReturned = true;

            SetSkill(SkillName.Anatomy, 85.0, 100.0);
            SetSkill(SkillName.Healing, 90.0, 100.0);
            SetSkill(SkillName.Forensics, 75.0, 98.0);
            SetSkill(SkillName.SpiritSpeak, 65.0, 88.0);

            
        }

        public override bool IsActiveVendor { get { return false; } }
        public override bool IsInvulnerable { get { return false; } }

        //public override void InitSBInfo()
        //{
        //    SBInfos.Add( new SBMage() );
        //    SBInfos.Add( new SBProfessor() );
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

        public Professor(Serial serial)
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