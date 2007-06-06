//11APR2007 HighCardBetty - High Card by RavonTUS@Yahoo.com

using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Multis;

namespace Server.Mobiles
{
    public class HighCardBetty : BaseHealer
    {
        private static bool m_Talked;
        private static bool m_ItemHasBeenReturned;
        private static int PlayerCard;
        private static int DealerCard;

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
                    string Sentence = "";
                    string playername = m.Name;

                    //The Welcome
                    switch (Utility.Random(3))  //picks one of the following
                    {
                        case 0:
                            {
                                //m.Name = char.ToUpper(m.Name[0]) + m.Name.Substring(1);
                                Sentence = String.Format("Welcome to High Card Betty's House, want to try {0} {1}.", dat_adj1, playername);
                                break;
                            }
                        case 1:
                            {
                                m.Name = char.ToUpper(m.Name[0]) + m.Name.Substring(1);
                                Sentence = String.Format("{0}, I have a {1} card game for you.", playername, dat_adj1);
                                break;
                            }
                        case 2:
                            {
                                Sentence = String.Format("I'm glad you left {0} and came to see {1} old High Card Betty.", dat_TownRegion, dat_verb1);
                                break;
                            }
                    }
                    m_Talked = true;
                    Say(Sentence, this);
                    this.Move(GetDirectionTo(m.Location));
                    HighCardBettyTimer t = new HighCardBettyTimer();
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
                            { Say(String.Format("{0} {1}, do you want to play your favorite game?", dat_Greeting, playername, dat_verb1, dat_Creature1)); break; }
                        case 1:
                            { Say(String.Format("Would you like to play {0} cards?", dat_adj1)); break; }
                        case 2:
                            { Say(String.Format("People come from {0} to play cards here.", dat_TownRegion)); break; }
                        case 3:
                            { Say(String.Format("{0}, {1} want to play?", playername, dat_Greeting)); break; }
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
                                    Say(String.Format("{0} draw a card and let me see it.", playername));
                                    break;
                                }
                            case 1:
                                {
                                    dat_article1 = char.ToUpper(dat_article1[0]) + dat_article1.Substring(1);
                                    Say(String.Format("{0} my {1} card vs your {2} card..", playername, dat_adj1, dat_adj1));
                                    break;
                                }
                            case 2:
                                {
                                    dat_article1 = char.ToUpper(dat_article1[0]) + dat_article1.Substring(1);
                                    Say(String.Format("Place your card back into my deck and let's see if you a winner.", dat_adj1));
                                    break;
                                }
                            case 3:
                                {
                                    dat_article1 = char.ToUpper(dat_article1[0]) + dat_article1.Substring(1);
                                    Say(String.Format("Be a good {0} and deal the cards.", dat_Creature1, dat_adj1));
                                    break;
                                }
                        }


                        //Choose Item Location
                        int x = Location.X - 1;
                        int y = Location.Y;
                        int z = Map.GetAverageZ(x, y);

                        if (Map.CanFit(x, y, Location.Z, 1))
                        {
                            Item item = new PlayingCards();
                            item.Name = dat_PlayingCards;
                            item.MoveToWorld(new Point3D(x, y, Location.Z), Map);
                            m_ItemHasBeenReturned = false;
                            HighCardBettyItemTimer t = new HighCardBettyItemTimer();
                            t.Start();
                        }
                        else if (Map.CanFit(x, y, z, 1))
                        {
                            Item item = new PlayingCards();
                            item.Name = dat_PlayingCards;
                            item.MoveToWorld(new Point3D(x, y, z), Map);
                            m_ItemHasBeenReturned = false;
                            HighCardBettyItemTimer t = new HighCardBettyItemTimer();
                            t.Start();
                        }
                    }
                    else
                    {
                        Say(String.Format("Let me see the card, {0} or {1}.  Hurry any card will due!", dat_adj1, dat_adj2));
                    }
                }
            #endregion
        }

        public override bool OnDragDrop(Mobile from, Item item)
        {
            #region Player dropped Quest Item
            if (item is PlayingCards)
            {
                switch (item.Name)
                {
                    case "1": { PlayerCard = 1; break; }
                    case "2": { PlayerCard = 2; break; }
                    case "3": { PlayerCard = 3; break; }
                    case "4": { PlayerCard = 4; break; }
                    case "5": { PlayerCard = 5; break; }
                    case "6": { PlayerCard = 6; break; }
                    case "7": { PlayerCard = 7; break; }
                    case "8": { PlayerCard = 8; break; }
                    case "9": { PlayerCard = 9; break; }
                    case "10": { PlayerCard = 10; break; }
                    case "J": { PlayerCard = 11; break; }
                    case "Q": { PlayerCard = 12; break; }
                    case "K": { PlayerCard = 13; break; }
                    case "A": { PlayerCard = 14; break; }
                    default: { PlayerCard = 1; break; }
                }

                DealerCard = Utility.RandomMinMax(1, 14);

                if (PlayerCard > DealerCard)
                {
                    Say(String.Format("Player Wins! {0} take {1}", item.Name, DealerCard));

                    from.Backpack.AddItem(new Gold(1));
                    m_ItemHasBeenReturned = true;
                    return true;
                }

                if (PlayerCard < DealerCard)
                {
                    Say(String.Format("Dealer Wins! {0} take {1}", DealerCard, item.Name));

                    Item item2 = from.Backpack.FindItemByType(typeof(Gold));
                    if (item2 != null)// if is item is found
                    {
                        if (item2.Amount > 1)
                        { item2.Amount--; }
                        else
                        { item2.Delete(); }

                        m_ItemHasBeenReturned = true;
                        return true;
                    }
                }

                if (PlayerCard == DealerCard)
                {
                    Say(String.Format("Draw! {0} ties {1}", item.Name, DealerCard));
                    //Say(String.Format("Draw!"));
                    m_ItemHasBeenReturned = true;
                    return true;
                }

                return false;
            }
            return false;
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
                "hit",
                "me",
                "hitme",
                "card",
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

        #region HighCardBettyTimer
        private class HighCardBettyTimer : Timer
        {
            public HighCardBettyTimer()
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

        #region HighCardBettyItemTimer
        private class HighCardBettyItemTimer : Timer
        {
            public HighCardBettyItemTimer()
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
        public HighCardBetty()
        {
            Title = "the High Card Dealer";
            m_ItemHasBeenReturned = true;

            SetSkill(SkillName.Anatomy, 85.0, 100.0);
            SetSkill(SkillName.Healing, 90.0, 100.0);
            SetSkill(SkillName.Forensics, 75.0, 98.0);
            SetSkill(SkillName.SpiritSpeak, 65.0, 88.0);

            Body = 0x191;
            CantWalk = true;
        }


        public override bool IsActiveVendor { get { return false; } }
        public override bool IsInvulnerable { get { return false; } }

        //public override void InitSBInfo()
        //{
        //    SBInfos.Add( new SBMage() );
        //    SBInfos.Add( new SBHighCardBetty() );
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

        public HighCardBetty(Serial serial)
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