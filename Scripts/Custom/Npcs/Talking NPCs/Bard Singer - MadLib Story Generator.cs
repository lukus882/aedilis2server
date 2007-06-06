//19SEP2007 Bard Singer by RavonTUS@Yahoo.com
//Play at An Nox, the cure for the UO addiction
//annox.no-ip.com

using System;
using System.Collections;
using Server;
using Server.Items;

namespace Server.Mobiles
{
    public class BardSinger : BaseHealer
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

            #region NPC Welcomes Player
            if (m_Talked == false)
            {
                if (m.InRange(this, 3) && m is PlayerMobile)
                {
                    switch (Utility.Random(5))  //picks one of the following
                    {
                        case 0:
                            {//madlib
                                Sentence = String.Format("The old {0} sang '{1} {2} of {3}'.", dat_noun1, dat_Language1, dat_Language2, dat_Creature1);
                                Sentence = Sentence + String.Format("{0} {1} {2} {3} ", dat_Language1, dat_Language2, dat_Creature1, dat_Language3);
                                Sentence = Sentence + String.Format("{0} {1} {2} {3 }", dat_Language2, dat_Language3, dat_Creature2, dat_Language1);
                                Sentence = Sentence + String.Format("{0} {1} {2} {3}.", dat_Language1, dat_Creature1, dat_Language3, dat_Language2);
                                break;
                            }
                        case 1:
                            {//madlib - Tennis Pirate http://www.writing.com/main/madlibs.php/item_id/1125009?blank_1=NOUN&blank_exists_1=1&blank_2=ADJ&blank_exists_2=1&blank_3=CAR&blank_exists_3=1&blank_4=PLURALNOUN&blank_exists_4=1&blank_5=CAR&blank_exists_5=1&blank_6=PART&blank_exists_6=1&blank_7=VERB-PAST&blank_exists_7=1&item_id=1125009&action=complete
                                // "Grab your NOUN," called my dad. "We're going on a road trip!"
                                //My sister and I were used to our parents' spontaneous,
                                //ADJ decisions to take the family CAR for a drive, so we didn't worry. 
                                //We piled our suitcases and PLURALNOUN into the back of the CAR,
                                //then climbed in ourselves. Our dad revved the PART, shifted into reverse,
                                //and VERB-PAST out of the driveway.
                                Sentence = String.Format("'Grab your {0}', called my {1}.  'We are going on a road trip!' ", dat_Armor, dat_Creature1);
                                Sentence = Sentence + String.Format("My {0} sister and I were used to our parents' spontaneous, ", dat_adj1);
                                Sentence = Sentence + String.Format("{0} decisions to take the family {1} for a drive, so we did not worry. ", dat_adj2, dat_noun1);
                                Sentence = Sentence + String.Format("We pile out bags and {0}s into the back of the {1}, ", dat_noun2, dat_noun1);
                                Sentence = Sentence + String.Format("then climbed in ourselves. Our {0} grabbed the reings, ", dat_Creature1);
                                Sentence = Sentence + String.Format("and we {0} out of the barn.", dat_verbed1);
                                break;
                            }
                        case 2:
                            {//madlib - Puppy - http://www.writing.com/main/madlibs/item_id/1195190#sw
                                //You aint nothin but a ANIMAL2
                                //VERB -ing all the NOUN2.
                                //You aint nothin but a ANIMAL2
                                //VERB -ing all the NOUN2.
                                //Well, you aint never caught a ANIMAL1
                                //And you aint no FRIEND1 of mine.
                                Sentence = String.Format("You aint nothin but a {0}, ", dat_Creature1);
                                Sentence = Sentence + String.Format("{0} all the {1}. ", dat_verbing1, dat_noun2);
                                Sentence = Sentence + String.Format("You ain't nothin' but a {0}, ", dat_Creature1);
                                Sentence = Sentence + String.Format("{0} all the {1}.", dat_verbing1, dat_noun2);
                                Sentence = Sentence + String.Format("Well, you ain't never caught a {0}. ", dat_Creature2);
                                Sentence = Sentence + String.Format("And you ain't no {0} of mine.", dat_noun1);
                                break;

                            }
                        case 3:
                            {//madlib - BRAD BAILEY'S http://members.aol.com/gametown2/games/Madlib.html
                                //One day while I was VERB1 in the ROOM1 a ADJECTIVE1 NOUN1 fell through the roof.
                                //It immediately jumped on the FURNITURE1 and knocked over the NOUN2.
                                //Then it ran out the door into the ROOM2 and VERB2 a NOUN3 off the FURNITURE2.
                                //It then knocked a glass of LIQUID1 off the coffee table. 
                                //After NUMBER1 minutes of chasing the NOUN1 through the house
                                //I finally caught it and put it outside. It quickly climbed the nearest NOUN4. 
                                Sentence = String.Format("One day while I was {0} in the {1} a {2} {3} fell throught the roof. ", dat_verb1, dat_Room1, dat_adj1, dat_noun1);
                                Sentence = Sentence + String.Format("It immediately jumped on the {0} and knocked over the {1} ", dat_Furniture1, dat_noun2);
                                Sentence = Sentence + String.Format("It then knocked a glass of {0} off the coffee table. ", dat_Liquid1);
                                Sentence = Sentence + String.Format("After {0} minutes of chasing the {1} through the house ", dat_Number1, dat_noun1);
                                Sentence = Sentence + String.Format(" I finally caught it and put it outside.  It quickly climbed in the nearest {0}.", dat_noun4);
                                break;
                            }
                        case 4:
                            {//madlib - Borealis Hummingbird http://www.writing.com/main/madlibs.php/item_id/1003828?blank_7=PLACE&blank_exists_7=1&blank_6=PASTVERB&blank_exists_6=1&blank_5=ADJECTIVE&blank_exists_5=1&blank_4=OBJECT&blank_exists_4=1&blank_2=EMOTION&blank_exists_2=1&blank_1=NAME&blank_exists_1=1&blank_3=ANIMAL&blank_exists_3=1&item_id=1003828&action=complete
                                //NAME was a very EMOTION ANIMAL.
                                //NAME had a OBJECT which was ADJECTIVE. 
                                //The ADJECTIVE OBJECT PASTVERB to PLACE, 
                                //and NAME went to look for it. 
                                //NAME finally reached PLACE,
                                //but the OBJECT was nowhere to be found. 
                                //Eventually NAME went home and found the ADJECTIVE OBJECT in the sock drawer.
                                Sentence = Sentence + String.Format("{0} was a very {1} {2}. ", m.Name, dat_adj1, dat_Creature1);
                                Sentence = Sentence + String.Format("{0} has a {1} which was {2}. ", m.Name, dat_Armor, dat_adj1);
                                Sentence = Sentence + String.Format("The {0} {1} {2} to {3}, ", dat_adj1, dat_Armor, dat_verbed1, dat_Other);
                                Sentence = Sentence + String.Format("and {0} went to look for it. ", m.Name);
                                Sentence = Sentence + String.Format("{0} finally reached {1} ", m.Name, dat_Other);
                                Sentence = Sentence + String.Format("but the {0} was nowhere to be found. ", dat_Armor);
                                Sentence = Sentence + String.Format("Eventually {0} went home and found the ", m.Name);
                                Sentence = Sentence + String.Format("{0} {1} in the sock drawer.", dat_adj1, dat_Armor);
                                break;
                            }
                    }
                    m_Talked = true;
                    Say(Sentence, this);
                    this.Move(GetDirectionTo(m.Location));
                    BardSingerTimerTimer t = new BardSingerTimerTimer();
                    t.Start();
                }
            }
            #endregion
        }

        #region BardSingerTimerTimer
        private class BardSingerTimerTimer : Timer
        {
            public BardSingerTimerTimer()
                : base(TimeSpan.FromSeconds(60))
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
        public BardSinger()
        {
            Title = "the bard";

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
        //    SBInfos.Add( new SBBardSingerTimer() );
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

        public BardSinger(Serial serial)
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