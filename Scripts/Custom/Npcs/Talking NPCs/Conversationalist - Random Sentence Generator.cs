//19APR2007 Converstationlist by RavonTUS@Yahoo.com *** START ***

using System;
using System.Collections;
using Server;
using Server.Items;

namespace Server.Mobiles
{
    public class Conversationalist : BaseHealer
    {
        private static bool m_Talked;
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
                    string Sentence = "";

                    switch (Utility.Random(3))  //picks one of the following
                    {
                        case 0:
                            {
                                //Sentence = Welcome Player
                                m.Name = char.ToUpper(m.Name[0]) + m.Name.Substring(1);
                                Sentence = String.Format(" {0} {1}", dat_Greeting, m.Name);
                                break;
                            }
                        case 1:
                            {
                                //Sentence = Player do you know where Shrine is?
                                Sentence = String.Format("{0} do you know where {1} is?", m.Name, dat_Shrine);
                                break;
                            }
                        case 2:
                            {
                                //Sentence = Player have you visisted Town?
                                Sentence = String.Format("{0} have you visisted {1}?", m.Name, dat_noun1);
                                break;
                            }
                    }
                    m_Talked = true;
                    Say(Sentence, this);
                    this.Move(GetDirectionTo(m.Location));
                    ConversationalistTimer t = new ConversationalistTimer();
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

            //Mobile pm = (Mobile)e;
            string speech = e.Speech.ToLower();
            string ArmorName = "";
            int x;
            int y;
            int z;

            #region Player says "Hi"
            for (int i = 0; i < Greetings.Length; i++)
                if (speech == Greetings[i])
                {
                    switch (Utility.Random(6))  //picks one of the following
                    {
                        case 0:
                            { Say(String.Format("{0} do you know where {1} is?", e.Mobile.Name, dat_Shrine)); break; }
                        case 1:
                            { Say(String.Format("{0} do you know where {1}s are?", e.Mobile.Name, dat_noun1)); break; }
                        case 2:
                            { Say(String.Format("{0} have you visisted {1}?", e.Mobile.Name, dat_noun1)); break; }
                        case 3:
                            { Say(String.Format("{0} do you know where I can find {0}?", e.Mobile.Name, dat_Armor)); break; }
                        case 4:
                            { Say(String.Format("{0} do you need some armor?", e.Mobile.Name, dat_Armor)); break; }
                    }
                }
            #endregion

            #region Play says "Yes"
            if (speech == "yes")
            {
                switch (Utility.Random(3))  //picks one of the following
                {
                    case 0:
                        //Player did you go to Yew Cave and find fuzzy orcs?
                        { Say(String.Format("{0} did you go to {1} and find {2} {3}?", e.Mobile.Name, dat_Other, dat_article1, dat_noun1)); break; }
                    case 1:
                        //Player when you go to Yew did you see any fuzzy orcs.
                        { Say(String.Format("{0} when you go to {1}, did you see any {2} {3}", e.Mobile.Name, dat_TownRegion, dat_article1, dat_noun1)); break; }
                    case 2:
                        //Do you think fuzzy orcs live near Britain Graveyard
                        { Say(String.Format("Do you think {0} {1}s and {2}s live near {3}'?", dat_adj1, dat_noun1, dat_noun2, dat_DungeonRegion)); break; }
                }
            }
            #endregion

            #region Play says "No"
            if (speech == "no")
            {
                switch (Utility.Random(3))  //picks one of the following
                {
                    case 0:
                        //Player watch out for fuzzy orcs saying "Hi".
                        { Say(String.Format("{0}, watch out for {1} {2} saying, '{3}'.", e.Mobile.Name, dat_adj1, dat_noun1, dat_Greeting)); break; }
                    case 1:
                        { Say(String.Format("Can you help me find {0} near {1}?", dat_Armor, dat_DungeonRegion)); break; }
                    case 2:
                        { Say(String.Format("Have you been able to {0} a {1}?", dat_verb1, dat_Creature1)); break; }
                }
            }
            #endregion

            #region Player says "What"
            for (int i = 0; i < What.Length; i++)
                if (speech == What[i])
                {
                    switch (Utility.Random(3))  //picks one of the following
                    {
                        case 0:
                            {//Sentence = Player is a little ocr who lives in Minoc
                                dat_article1 = char.ToUpper(dat_article1[0]) + dat_article1.Substring(1);
                                Say(String.Format("{0} is a {1} {2} who lives in {3}.", e.Mobile.Name, dat_adj1, dat_noun1, dat_TownRegion));
                                break;
                            }
                        case 1:
                            {//Sentence = A orc could beat player
                                dat_article1 = char.ToUpper(dat_article1[0]) + dat_article1.Substring(1);
                                Say(String.Format("{0} {1} could {2} {3}.", dat_article1, dat_noun1, dat_verb1, e.Mobile.Name));
                                break;
                            }
                        case 2:
                            {//Sentence = I over heard Player say, "asdf asdf asd"  to a orc
                                dat_article1 = char.ToUpper(dat_article1[0]) + dat_article1.Substring(1);
                                Say(String.Format("I over heard {0} say, '{1} {2} {3}' to a {4}.", e.Mobile.Name, dat_Language1, dat_Language2, dat_Language3, dat_Creature1));
                                break;
                            }
                    }
                }
            #endregion

            //#region Player says "Armor"
            //for (int i = 0; i < Armor.Length; i++)
            //    if (speech == Armor[i])
            //    {
            //        switch (Utility.Random(7))  //picks one of the following
            //        {
            //            //Leather Chest,Leather Gloves,Leather Gorget,Leather Legs,Leather Shorts,Leather Skirt,Leather Cap
            //            case 0: { pm.DropItem(new LeatherChest()); ArmorName = "Leather Chest"; break; }
            //            case 1: { Item item = new LeatherGloves(); ArmorName = "Leather Gloves"; break; }
            //            case 2: { Item item = new LeatherGloves(); ArmorName = "Leather Gorget"; break; }
            //            case 3: { Item item = new LeatherLegs(); ArmorName = "Leather Legs"; break; }
            //            case 4: { Item item = new LeatherShorts(); ArmorName = "Leather Shorts"; break; }
            //            case 5: { Item item = new LeatherSkirt(); ArmorName = "Leather Skirt"; break; }
            //            case 6: { Item item = new LeatherCap(); ArmorName = "Leather Cap"; break; }
            //        }

            //        switch (Utility.Random(4))  //picks one of the following
            //        {
            //            case 0:
            //                { Say(String.Format("Try this {0}, it will {1} a {2} {3}.", ArmorName, dat_verb1, dat_adj1, dat_Creature1)); break; }
            //            case 1:
            //                { Say(String.Format("Let my find a {0} piece of leather.", dat_adj1)); break; }
            //            case 2:
            //                { Say(String.Format("{0} is the best I have {1}", ArmorName, e.Mobile.Name)); break; }
            //            case 3:
            //                { Say(String.Format("Take my {0}, it will help against {1} and {2}.", ArmorName, dat_Creature1, dat_Creature2)); break; }
            //        }
            //    }
            //#endregion
        }

        #region List of Response Words
        public static string[] Greetings = new string[]
            {	"hello",
	            "hey",
	            "hi",
	            "sup",
	            "howdy",
	            "bonjour"
            };

        public static string[] What = new string[]
            {	"who",
	            "what",
	            "when",
	            "where",
	            "how",
	            "understand"
            };

        public static string[] Armor = new string[]
            {	"armor",
	            "armour",
	            "arms"
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

        #region ConversationalistTimer
        private class ConversationalistTimer : Timer
        {
            public ConversationalistTimer()
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
        public Conversationalist()
        {
            Title = "the conversationalist";

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
        //    SBInfos.Add( new SBConversationalist() );
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

        public Conversationalist(Serial serial)
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