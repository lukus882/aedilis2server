/*
 Modified by:Bad Karma aka Broadside

  			        BBBBB        A      DDDDD  
  			       BB  BB      AAA     DDDDDD
  			      BB  BB     AA AA    DD   DD
 			     BBBBBB	   AA   AA   DD   DD
  		    	BBBBB     AAAAAAA   DD   DD
   		       BB  BB    AAAAAAA   DD   DD
   		      BB  BB    AA   AA   DDDDDD
		     BBBBBB    AA   AA   DDDDD

			        KK   KK       A      RRRRRR    MM   MM      A
			       KK  KK       AAA     RR   RR   MMM MMM     AAA
			      KK KK       AA AA    RR   RR   MMMMMMM    AA AA
			     KKKK       AA   AA   RRRRRR    MM M MM   AA   AA
			    KKKK       AAAAAAA   RRRR      MM   MM   AAAAAAA
			   KK KK      AAAAAAA   RR RR     MM   MM   AAAAAAA
			  KK   KK    AA   AA   RR  RR    MM   MM   AA   AA
			 KK     KK  AA   AA   RR   RR   MM   MM   AA   AA
 */
using System;
using Server;
using Server.Mobiles;
using Server.Gumps;
using Server.Items;
using Server.Network;

namespace Server.Engines.Plants
{
    public class SetToDecorativeGump : Gump
    {
        private PlantItem m_Plant;

        public SetToDecorativeGump(PlantItem plant)
            : base(20, 20)
        {
            m_Plant = plant;

            DrawBackground();

            AddLabel(115, 85, 0x44, "Set plant");
            AddLabel(82, 105, 0x44, "to decorative mode?");

            AddButton(98, 140, 0x47E, 0x480, 1, GumpButtonType.Reply, 0); // Cancel

            AddButton(138, 141, 0xD2, 0xD2, 2, GumpButtonType.Reply, 0); // Help
            AddLabel(143, 141, 0x835, "?");

            AddButton(168, 140, 0x481, 0x483, 3, GumpButtonType.Reply, 0); // Ok
        }

        private void DrawBackground()
        {
            AddBackground(50, 50, 200, 150, 0xE10);

            AddItem(25, 45, 0xCEB);
            AddItem(25, 118, 0xCEC);

            AddItem(227, 45, 0xCEF);
            AddItem(227, 118, 0xCF0);
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;


            PlayerMobile m = from as PlayerMobile;

            if (info.ButtonID == 0 || m_Plant.Deleted || m_Plant.PlantStatus != PlantStatus.Stage9 || !from.InRange(m_Plant.GetWorldLocation(), 3))
                return;

            if (!m_Plant.IsUsableBy(from))
            {
                m_Plant.LabelTo(from, 1061856); // You must have the item in your backpack or locked down in order to use it.
                return;
            }

            switch (info.ButtonID)
            {
                case 1: // Cancel
                    {
                        from.SendGump(new ReproductionGump(m_Plant));


                    }
                    break;
                case 2: // Help
                    {
                        from.Send(new DisplayHelpTopic(70, true)); // DECORATIVE MODE

                        from.SendGump(new SetToDecorativeGump(m_Plant));


                    }
                    break;
                case 3: // Ok
                    {
                        string planthue = Convert.ToString(m_Plant.PlantHue);
                        string planttype = Convert.ToString(m_Plant.PlantType);
                        #region //Campions
                        if (planttype == "CampionFlowers")
                        {
                            if (planthue == "BrightBlue")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightBlueCampionFlowers());
                            }

                            else if (planthue == "Black")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BlackCampionFlowers());
                            }

                            else if (planthue == "White")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new WhiteCampionFlowers());
                            }

                            else if (planthue == "Pink")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new PinkCampionFlowers());
                            }

                            else if (planthue == "Magenta")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new MagentaCampionFlowers());
                            }

                            else if (planthue == "Aqua")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new AquaCampionFlowers());
                            }

                            else if (planthue == "FireRed")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new FireRedCampionFlowers());
                            }

                            else if (planthue == "Plain")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new CampionFlowers());
                            }

                            else if (planthue == "Red")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new RedCampionFlowers());
                            }

                            else if (planthue == "Blue")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BlueCampionFlowers());
                            }

                            else if (planthue == "Purple")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new PurpleCampionFlowers());
                            }

                            else if (planthue == "Yellow")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new YellowCampionFlowers());
                            }

                            else if (planthue == "Orange")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new OrangeCampionFlowers());
                            }

                            else if (planthue == "Green")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new GreenCampionFlowers());
                            }

                            else if (planthue == "BrightRed")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightRedCampionFlowers());
                            }

                            else if (planthue == "BrightPurple")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightPurpleCampionFlowers());
                            }

                            else if (planthue == "BrightYellow")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightYellowCampionFlowers());
                            }

                            else if (planthue == "BrightOrange")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightOrangeCampionFlowers());
                            }

                            else if (planthue == "BrightGreen")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightGreenCampionFlowers());
                            }
                        }

                        #endregion

                        #region //Poppies
                        if (planttype == "Poppies")
                        {
                            if (planthue == "BrightBlue")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightBluePoppies());
                            }

                            else if (planthue == "Black")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BlackPoppies());
                            }

                            else if (planthue == "White")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new WhitePoppies());
                            }

                            else if (planthue == "Pink")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new PinkPoppies());
                            }

                            else if (planthue == "Magenta")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new MagentaPoppies());
                            }

                            else if (planthue == "Aqua")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new AquaPoppies());
                            }

                            else if (planthue == "FireRed")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new FireRedPoppies());
                            }

                            else if (planthue == "Plain")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new Poppies());
                            }

                            else if (planthue == "Red")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new RedPoppies());
                            }

                            else if (planthue == "Blue")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BluePoppies());
                            }

                            else if (planthue == "Purple")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new PurplePoppies());
                            }

                            else if (planthue == "Yellow")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new YellowPoppies());
                            }

                            else if (planthue == "Orange")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new OrangePoppies());
                            }

                            else if (planthue == "Green")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new GreenPoppies());
                            }

                            else if (planthue == "BrightRed")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightRedPoppies());
                            }

                            else if (planthue == "BrightPurple")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightPurplePoppies());
                            }

                            else if (planthue == "BrightYellow")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightYellowPoppies());
                            }

                            else if (planthue == "BrightOrange")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightOrangePoppies());
                            }

                            else if (planthue == "BrightGreen")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightGreenPoppies());
                            }
                        }

                        #endregion

                        #region //Snowdrops
                        if (planttype == "Snowdrops")
                        {
                            if (planthue == "BrightBlue")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightBlueSnowdrops());
                            }

                            else if (planthue == "Black")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BlackSnowdrops());
                            }

                            else if (planthue == "White")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new WhiteSnowdrops());
                            }

                            else if (planthue == "Pink")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new PinkSnowdrops());
                            }

                            else if (planthue == "Magenta")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new MagentaSnowdrops());
                            }

                            else if (planthue == "Aqua")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new AquaSnowdrops());
                            }

                            else if (planthue == "FireRed")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new FireRedSnowdrops());
                            }

                            else if (planthue == "Plain")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new Snowdrops());
                            }

                            else if (planthue == "Red")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new RedSnowdrops());
                            }

                            else if (planthue == "Blue")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BlueSnowdrops());
                            }

                            else if (planthue == "Purple")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new PurpleSnowdrops());
                            }

                            else if (planthue == "Yellow")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new YellowSnowdrops());
                            }

                            else if (planthue == "Orange")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new OrangeSnowdrops());
                            }

                            else if (planthue == "Green")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new GreenSnowdrops());
                            }

                            else if (planthue == "BrightRed")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightRedSnowdrops());
                            }

                            else if (planthue == "BrightPurple")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightPurpleSnowdrops());
                            }

                            else if (planthue == "BrightYellow")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightYellowSnowdrops());
                            }

                            else if (planthue == "BrightOrange")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightOrangeSnowdrops());
                            }

                            else if (planthue == "BrightGreen")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightGreenSnowdrops());
                            }
                        }

                        #endregion

                        #region //Bulrushes
                        if (planttype == "Bulrushes")
                        {
                            if (planthue == "BrightBlue")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightBlueBulrushes());
                            }

                            else if (planthue == "Black")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BlackBulrushes());
                            }

                            else if (planthue == "White")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new WhiteBulrushes());
                            }

                            else if (planthue == "Pink")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new PinkBulrushes());
                            }

                            else if (planthue == "Magenta")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new MagentaBulrushes());
                            }

                            else if (planthue == "Aqua")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new AquaBulrushes());
                            }

                            else if (planthue == "FireRed")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new FireRedBulrushes());
                            }

                            else if (planthue == "Plain")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new Bulrushes());
                            }

                            else if (planthue == "Red")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new RedBulrushes());
                            }

                            else if (planthue == "Blue")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BlueBulrushes());
                            }

                            else if (planthue == "Purple")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new PurpleBulrushes());
                            }

                            else if (planthue == "Yellow")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new YellowBulrushes());
                            }

                            else if (planthue == "Orange")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new OrangeBulrushes());
                            }

                            else if (planthue == "Green")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new GreenBulrushes());
                            }

                            else if (planthue == "BrightRed")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightRedBulrushes());
                            }

                            else if (planthue == "BrightPurple")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightPurpleBulrushes());
                            }

                            else if (planthue == "BrightYellow")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightYellowBulrushes());
                            }

                            else if (planthue == "BrightOrange")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightOrangeBulrushes());
                            }

                            else if (planthue == "BrightGreen")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightGreenBulrushes());
                            }
                        }

                        #endregion

                        #region //Lilies
                        if (planttype == "Lilies")
                        {
                            if (planthue == "BrightBlue")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightBlueLilies());
                            }

                            else if (planthue == "Black")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BlackLilies());
                            }

                            else if (planthue == "White")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new WhiteLilies());
                            }

                            else if (planthue == "Pink")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new PinkLilies());
                            }

                            else if (planthue == "Magenta")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new MagentaLilies());
                            }

                            else if (planthue == "Aqua")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new AquaLilies());
                            }

                            else if (planthue == "FireRed")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new FireRedLilies());
                            }

                            else if (planthue == "Plain")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new Lilies());
                            }

                            else if (planthue == "Red")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new RedLilies());
                            }

                            else if (planthue == "Blue")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BlueLilies());
                            }

                            else if (planthue == "Purple")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new PurpleLilies());
                            }

                            else if (planthue == "Yellow")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new YellowLilies());
                            }

                            else if (planthue == "Orange")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new OrangeLilies());
                            }

                            else if (planthue == "Green")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new GreenLilies());
                            }

                            else if (planthue == "BrightRed")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightRedLilies());
                            }

                            else if (planthue == "BrightPurple")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightPurpleLilies());
                            }

                            else if (planthue == "BrightYellow")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightYellowLilies());
                            }

                            else if (planthue == "BrightOrange")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightOrangeLilies());
                            }

                            else if (planthue == "BrightGreen")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightGreenLilies());
                            }
                        }

                        #endregion

                        #region //PampasGrass
                        if (planttype == "PampasGrass")
                        {
                            if (planthue == "BrightBlue")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightBluePampasGrass());
                            }

                            else if (planthue == "Black")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BlackPampasGrass());
                            }

                            else if (planthue == "White")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new WhitePampasGrass());
                            }

                            else if (planthue == "Pink")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new PinkPampasGrass());
                            }

                            else if (planthue == "Magenta")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new MagentaPampasGrass());
                            }

                            else if (planthue == "Aqua")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new AquaPampasGrass());
                            }

                            else if (planthue == "FireRed")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new FireRedPampasGrass());
                            }

                            else if (planthue == "Plain")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new PampasGrass());
                            }

                            else if (planthue == "Red")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new RedPampasGrass());
                            }

                            else if (planthue == "Blue")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BluePampasGrass());
                            }

                            else if (planthue == "Purple")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new PurplePampasGrass());
                            }

                            else if (planthue == "Yellow")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new YellowPampasGrass());
                            }

                            else if (planthue == "Orange")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new OrangePampasGrass());
                            }

                            else if (planthue == "Green")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new GreenPampasGrass());
                            }

                            else if (planthue == "BrightRed")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightRedPampasGrass());
                            }

                            else if (planthue == "BrightPurple")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightPurplePampasGrass());
                            }

                            else if (planthue == "BrightYellow")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightYellowPampasGrass());
                            }

                            else if (planthue == "BrightOrange")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightOrangePampasGrass());
                            }

                            else if (planthue == "BrightGreen")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightGreenPampasGrass());
                            }
                        }

                        #endregion

                        #region //Rushes
                        if (planttype == "Rushes")
                        {
                            if (planthue == "BrightBlue")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightBlueRushes());
                            }

                            else if (planthue == "Black")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BlackRushes());
                            }

                            else if (planthue == "White")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new WhiteRushes());
                            }

                            else if (planthue == "Pink")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new PinkRushes());
                            }

                            else if (planthue == "Magenta")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new MagentaRushes());
                            }

                            else if (planthue == "Aqua")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new AquaRushes());
                            }

                            else if (planthue == "FireRed")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new FireRedRushes());
                            }

                            else if (planthue == "Plain")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new Rushes());
                            }

                            else if (planthue == "Red")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new RedRushes());
                            }

                            else if (planthue == "Blue")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BlueRushes());
                            }

                            else if (planthue == "Purple")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new PurpleRushes());
                            }

                            else if (planthue == "Yellow")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new YellowRushes());
                            }

                            else if (planthue == "Orange")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new OrangeRushes());
                            }

                            else if (planthue == "Green")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new GreenRushes());
                            }

                            else if (planthue == "BrightRed")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightRedRushes());
                            }

                            else if (planthue == "BrightPurple")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightPurpleRushes());
                            }

                            else if (planthue == "BrightYellow")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightYellowRushes());
                            }

                            else if (planthue == "BrightOrange")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightOrangeRushes());
                            }

                            else if (planthue == "BrightGreen")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightGreenRushes());
                            }
                        }

                        #endregion

                        #region //ElephantEarPlant
                        if (planttype == "ElephantEarPlant")
                        {
                            if (planthue == "BrightBlue")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightBlueElephantEarPlant());
                            }

                            else if (planthue == "Black")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BlackElephantEarPlant());
                            }

                            else if (planthue == "White")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new WhiteElephantEarPlant());
                            }

                            else if (planthue == "Pink")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new PinkElephantEarPlant());
                            }

                            else if (planthue == "Magenta")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new MagentaElephantEarPlant());
                            }

                            else if (planthue == "Aqua")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new AquaElephantEarPlant());
                            }

                            else if (planthue == "FireRed")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new FireRedElephantEarPlant());
                            }

                            else if (planthue == "Plain")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new ElephantEarPlant());
                            }

                            else if (planthue == "Red")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new RedElephantEarPlant());
                            }

                            else if (planthue == "Blue")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BlueElephantEarPlant());
                            }

                            else if (planthue == "Purple")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new PurpleElephantEarPlant());
                            }

                            else if (planthue == "Yellow")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new YellowElephantEarPlant());
                            }

                            else if (planthue == "Orange")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new OrangeElephantEarPlant());
                            }

                            else if (planthue == "Green")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new GreenElephantEarPlant());
                            }

                            else if (planthue == "BrightRed")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightRedElephantEarPlant());
                            }

                            else if (planthue == "BrightPurple")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightPurpleElephantEarPlant());
                            }

                            else if (planthue == "BrightYellow")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightYellowElephantEarPlant());
                            }

                            else if (planthue == "BrightOrange")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightOrangeElephantEarPlant());
                            }

                            else if (planthue == "BrightGreen")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightGreenElephantEarPlant());
                            }
                        }

                        #endregion

                        #region //Fern
                        if (planttype == "Fern")
                        {
                            if (planthue == "BrightBlue")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightBlueFern());
                            }

                            else if (planthue == "Black")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BlackFern());
                            }

                            else if (planthue == "White")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new WhiteFern());
                            }

                            else if (planthue == "Pink")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new PinkFern());
                            }

                            else if (planthue == "Magenta")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new MagentaFern());
                            }

                            else if (planthue == "Aqua")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new AquaFern());
                            }

                            else if (planthue == "FireRed")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new FireRedFern());
                            }

                            else if (planthue == "Plain")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new Fern());
                            }

                            else if (planthue == "Red")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new RedFern());
                            }

                            else if (planthue == "Blue")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BlueFern());
                            }

                            else if (planthue == "Purple")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new PurpleFern());
                            }

                            else if (planthue == "Yellow")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new YellowFern());
                            }

                            else if (planthue == "Orange")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new OrangeFern());
                            }

                            else if (planthue == "Green")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new GreenFern());
                            }

                            else if (planthue == "BrightRed")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightRedFern());
                            }

                            else if (planthue == "BrightPurple")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightPurpleFern());
                            }

                            else if (planthue == "BrightYellow")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightYellowFern());
                            }

                            else if (planthue == "BrightOrange")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightOrangeFern());
                            }

                            else if (planthue == "BrightGreen")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightGreenFern());
                            }
                        }

                        #endregion

                        #region //PonytailPalm
                        if (planttype == "PonytailPalm")
                        {
                            if (planthue == "BrightBlue")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightBluePonytailPalm());
                            }

                            else if (planthue == "Black")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BlackPonytailPalm());
                            }

                            else if (planthue == "White")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new WhitePonytailPalm());
                            }

                            else if (planthue == "Pink")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new PinkPonytailPalm());
                            }

                            else if (planthue == "Magenta")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new MagentaPonytailPalm());
                            }

                            else if (planthue == "Aqua")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new AquaPonytailPalm());
                            }

                            else if (planthue == "FireRed")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new FireRedPonytailPalm());
                            }

                            else if (planthue == "Plain")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new PonytailPalm());
                            }

                            else if (planthue == "Red")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new RedPonytailPalm());
                            }

                            else if (planthue == "Blue")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BluePonytailPalm());
                            }

                            else if (planthue == "Purple")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new PurplePonytailPalm());
                            }

                            else if (planthue == "Yellow")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new YellowPonytailPalm());
                            }

                            else if (planthue == "Orange")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new OrangePonytailPalm());
                            }

                            else if (planthue == "Green")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new GreenPonytailPalm());
                            }

                            else if (planthue == "BrightRed")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightRedPonytailPalm());
                            }

                            else if (planthue == "BrightPurple")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightPurplePonytailPalm());
                            }

                            else if (planthue == "BrightYellow")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightYellowPonytailPalm());
                            }

                            else if (planthue == "BrightOrange")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightOrangePonytailPalm());
                            }

                            else if (planthue == "BrightGreen")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightGreenPonytailPalm());
                            }
                        }

                        #endregion

                        #region //SmallPalm
                        if (planttype == "SmallPalm")
                        {
                            if (planthue == "BrightBlue")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightBlueSmallPalm());
                            }

                            else if (planthue == "Black")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BlackSmallPalm());
                            }

                            else if (planthue == "White")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new WhiteSmallPalm());
                            }

                            else if (planthue == "Pink")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new PinkSmallPalm());
                            }

                            else if (planthue == "Magenta")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new MagentaSmallPalm());
                            }

                            else if (planthue == "Aqua")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new AquaSmallPalm());
                            }

                            else if (planthue == "FireRed")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new FireRedSmallPalm());
                            }

                            else if (planthue == "Plain")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new SmallPalm());
                            }

                            else if (planthue == "Red")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new RedSmallPalm());
                            }

                            else if (planthue == "Blue")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BlueSmallPalm());
                            }

                            else if (planthue == "Purple")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new PurpleSmallPalm());
                            }

                            else if (planthue == "Yellow")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new YellowSmallPalm());
                            }

                            else if (planthue == "Orange")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new OrangeSmallPalm());
                            }

                            else if (planthue == "Green")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new GreenSmallPalm());
                            }

                            else if (planthue == "BrightRed")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightRedSmallPalm());
                            }

                            else if (planthue == "BrightPurple")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightPurpleSmallPalm());
                            }

                            else if (planthue == "BrightYellow")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightYellowSmallPalm());
                            }

                            else if (planthue == "BrightOrange")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightOrangeSmallPalm());
                            }

                            else if (planthue == "BrightGreen")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightGreenSmallPalm());
                            }
                        }

                        #endregion

                        #region //CenturyPlant
                        if (planttype == "CenturyPlant")
                        {
                            if (planthue == "BrightBlue")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightBlueCenturyPlant());
                            }

                            else if (planthue == "Black")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BlackCenturyPlant());
                            }

                            else if (planthue == "White")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new WhiteCenturyPlant());
                            }

                            else if (planthue == "Pink")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new PinkCenturyPlant());
                            }

                            else if (planthue == "Magenta")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new MagentaCenturyPlant());
                            }

                            else if (planthue == "Aqua")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new AquaCenturyPlant());
                            }

                            else if (planthue == "FireRed")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new FireRedCenturyPlant());
                            }

                            else if (planthue == "Plain")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new CenturyPlant());
                            }

                            else if (planthue == "Red")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new RedCenturyPlant());
                            }

                            else if (planthue == "Blue")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BlueCenturyPlant());
                            }

                            else if (planthue == "Purple")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new PurpleCenturyPlant());
                            }

                            else if (planthue == "Yellow")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new YellowCenturyPlant());
                            }

                            else if (planthue == "Orange")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new OrangeCenturyPlant());
                            }

                            else if (planthue == "Green")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new GreenCenturyPlant());
                            }

                            else if (planthue == "BrightRed")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightRedCenturyPlant());
                            }

                            else if (planthue == "BrightPurple")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightPurpleCenturyPlant());
                            }

                            else if (planthue == "BrightYellow")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightYellowCenturyPlant());
                            }

                            else if (planthue == "BrightOrange")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightOrangeCenturyPlant());
                            }

                            else if (planthue == "BrightGreen")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightGreenCenturyPlant());
                            }
                        }

                        #endregion

                        #region //WaterPlant
                        if (planttype == "WaterPlant")
                        {
                            if (planthue == "BrightBlue")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightBlueWaterPlant());
                            }

                            else if (planthue == "Black")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BlackWaterPlant());
                            }

                            else if (planthue == "White")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new WhiteWaterPlant());
                            }

                            else if (planthue == "Pink")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new PinkWaterPlant());
                            }

                            else if (planthue == "Magenta")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new MagentaWaterPlant());
                            }

                            else if (planthue == "Aqua")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new AquaWaterPlant());
                            }

                            else if (planthue == "FireRed")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new FireRedWaterPlant());
                            }

                            else if (planthue == "Plain")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new WaterPlant());
                            }

                            else if (planthue == "Red")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new RedWaterPlant());
                            }

                            else if (planthue == "Blue")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BlueWaterPlant());
                            }

                            else if (planthue == "Purple")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new PurpleWaterPlant());
                            }

                            else if (planthue == "Yellow")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new YellowWaterPlant());
                            }

                            else if (planthue == "Orange")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new OrangeWaterPlant());
                            }

                            else if (planthue == "Green")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new GreenWaterPlant());
                            }

                            else if (planthue == "BrightRed")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightRedWaterPlant());
                            }

                            else if (planthue == "BrightPurple")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightPurpleWaterPlant());
                            }

                            else if (planthue == "BrightYellow")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightYellowWaterPlant());
                            }

                            else if (planthue == "BrightOrange")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightOrangeWaterPlant());
                            }

                            else if (planthue == "BrightGreen")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightGreenWaterPlant());
                            }
                        }

                        #endregion

                        #region //SnakePlant
                        if (planttype == "SnakePlant")
                        {
                            if (planthue == "BrightBlue")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightBlueSnakePlant());
                            }

                            else if (planthue == "Black")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BlackSnakePlant());
                            }

                            else if (planthue == "White")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new WhiteSnakePlant());
                            }

                            else if (planthue == "Pink")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new PinkSnakePlant());
                            }

                            else if (planthue == "Magenta")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new MagentaSnakePlant());
                            }

                            else if (planthue == "Aqua")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new AquaSnakePlant());
                            }

                            else if (planthue == "FireRed")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new FireRedSnakePlant());
                            }

                            else if (planthue == "Plain")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new SnakePlant());
                            }

                            else if (planthue == "Red")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new RedSnakePlant());
                            }

                            else if (planthue == "Blue")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BlueSnakePlant());
                            }

                            else if (planthue == "Purple")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new PurpleSnakePlant());
                            }

                            else if (planthue == "Yellow")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new YellowSnakePlant());
                            }

                            else if (planthue == "Orange")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new OrangeSnakePlant());
                            }

                            else if (planthue == "Green")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new GreenSnakePlant());
                            }

                            else if (planthue == "BrightRed")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightRedSnakePlant());
                            }

                            else if (planthue == "BrightPurple")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightPurpleSnakePlant());
                            }

                            else if (planthue == "BrightYellow")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightYellowSnakePlant());
                            }

                            else if (planthue == "BrightOrange")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightOrangeSnakePlant());
                            }

                            else if (planthue == "BrightGreen")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightGreenSnakePlant());
                            }
                        }

                        #endregion

                        #region //PricklyPearCactus
                        if (planttype == "PricklyPearCactus")
                        {
                            if (planthue == "BrightBlue")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightBluePricklyPearCactus());
                            }

                            else if (planthue == "Black")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BlackPricklyPearCactus());
                            }

                            else if (planthue == "White")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new WhitePricklyPearCactus());
                            }

                            else if (planthue == "Pink")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new PinkPricklyPearCactus());
                            }

                            else if (planthue == "Magenta")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new MagentaPricklyPearCactus());
                            }

                            else if (planthue == "Aqua")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new AquaPricklyPearCactus());
                            }

                            else if (planthue == "FireRed")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new FireRedPricklyPearCactus());
                            }

                            else if (planthue == "Plain")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new PricklyPearCactus());
                            }

                            else if (planthue == "Red")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new RedPricklyPearCactus());
                            }

                            else if (planthue == "Blue")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BluePricklyPearCactus());
                            }

                            else if (planthue == "Purple")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new PurplePricklyPearCactus());
                            }

                            else if (planthue == "Yellow")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new YellowPricklyPearCactus());
                            }

                            else if (planthue == "Orange")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new OrangePricklyPearCactus());
                            }

                            else if (planthue == "Green")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new GreenPricklyPearCactus());
                            }

                            else if (planthue == "BrightRed")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightRedPricklyPearCactus());
                            }

                            else if (planthue == "BrightPurple")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightPurplePricklyPearCactus());
                            }

                            else if (planthue == "BrightYellow")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightYellowPricklyPearCactus());
                            }

                            else if (planthue == "BrightOrange")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightOrangePricklyPearCactus());
                            }

                            else if (planthue == "BrightGreen")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightGreenPricklyPearCactus());
                            }
                        }

                        #endregion

                        #region //BarrelCactus
                        if (planttype == "BarrelCactus")
                        {
                            if (planthue == "BrightBlue")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightBlueBarrelCactus());
                            }

                            else if (planthue == "Black")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BlackBarrelCactus());
                            }

                            else if (planthue == "White")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new WhiteBarrelCactus());
                            }

                            else if (planthue == "Pink")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new PinkBarrelCactus());
                            }

                            else if (planthue == "Magenta")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new MagentaBarrelCactus());
                            }

                            else if (planthue == "Aqua")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new AquaBarrelCactus());
                            }

                            else if (planthue == "FireRed")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new FireRedBarrelCactus());
                            }

                            else if (planthue == "Plain")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BarrelCactus());
                            }

                            else if (planthue == "Red")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new RedBarrelCactus());
                            }

                            else if (planthue == "Blue")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BlueBarrelCactus());
                            }

                            else if (planthue == "Purple")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new PurpleBarrelCactus());
                            }

                            else if (planthue == "Yellow")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new YellowBarrelCactus());
                            }

                            else if (planthue == "Orange")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new OrangeBarrelCactus());
                            }

                            else if (planthue == "Green")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new GreenBarrelCactus());
                            }

                            else if (planthue == "BrightRed")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightRedBarrelCactus());
                            }

                            else if (planthue == "BrightPurple")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightPurpleBarrelCactus());
                            }

                            else if (planthue == "BrightYellow")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightYellowBarrelCactus());
                            }

                            else if (planthue == "BrightOrange")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightOrangeBarrelCactus());
                            }

                            else if (planthue == "BrightGreen")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightGreenBarrelCactus());
                            }
                        }

                        #endregion

                        #region //TribarrelCactus
                        if (planttype == "TribarrelCactus")
                        {
                            if (planthue == "BrightBlue")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightBlueTribarrelCactus());
                            }

                            else if (planthue == "Black")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BlackTribarrelCactus());
                            }

                            else if (planthue == "White")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new WhiteTribarrelCactus());
                            }

                            else if (planthue == "Pink")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new PinkTribarrelCactus());
                            }

                            else if (planthue == "Magenta")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new MagentaTribarrelCactus());
                            }

                            else if (planthue == "Aqua")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new AquaTribarrelCactus());
                            }

                            else if (planthue == "FireRed")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new FireRedTribarrelCactus());
                            }

                            else if (planthue == "Plain")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new TribarrelCactus());
                            }

                            else if (planthue == "Red")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new RedTribarrelCactus());
                            }

                            else if (planthue == "Blue")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BlueTribarrelCactus());
                            }

                            else if (planthue == "Purple")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new PurpleTribarrelCactus());
                            }

                            else if (planthue == "Yellow")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new YellowTribarrelCactus());
                            }

                            else if (planthue == "Orange")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new OrangeTribarrelCactus());
                            }

                            else if (planthue == "Green")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new GreenTribarrelCactus());
                            }

                            else if (planthue == "BrightRed")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightRedTribarrelCactus());
                            }

                            else if (planthue == "BrightPurple")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightPurpleTribarrelCactus());
                            }

                            else if (planthue == "BrightYellow")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightYellowTribarrelCactus());
                            }

                            else if (planthue == "BrightOrange")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightOrangeTribarrelCactus());
                            }

                            else if (planthue == "BrightGreen")
                            {
                                m_Plant.Delete();
                                m.AddToBackpack(new BrightGreenTribarrelCactus());
                            }
                        }

                        #endregion

                        #region //Bonsai
                        else if (planttype == "CommonGreenBonsai" && planthue == "None")
                        {
                            m_Plant.Delete();
                            m.AddToBackpack(new CommonGreenBonsai());
                        }



                        else if (planttype == "CommonPinkBonsai" && planthue == "None")
                        {
                            m_Plant.Delete();
                            m.AddToBackpack(new CommonPinkBonsai());
                        }



                        else if (planttype == "UncommonGreenBonsai" && planthue == "None")
                        {
                            m_Plant.Delete();
                            m.AddToBackpack(new UncommonGreenBonsai());
                        }



                        else if (planttype == "UncommonPinkBonsai" && planthue == "None")
                        {
                            m_Plant.Delete();
                            m.AddToBackpack(new UncommonPinkBonsai());
                        }



                        else if (planttype == "RareGreenBonsai" && planthue == "None")
                        {
                            m_Plant.Delete();
                            m.AddToBackpack(new RareGreenBonsai());
                        }



                        else if (planttype == "RarePinkBonsai" && planthue == "None")
                        {
                            m_Plant.Delete();
                            m.AddToBackpack(new RarePinkBonsai());
                        }



                        else if (planttype == "ExceptionalBonsai" && planthue == "None")
                        {
                            m_Plant.Delete();
                            m.AddToBackpack(new ExceptionalBonsai());
                        }



                        else if (planttype == "ExoticBonsai" && planthue == "None")
                        {
                            m_Plant.Delete();
                            m.AddToBackpack(new ExoticBonsai());
                        }
                    }

                    break;
                        #endregion

            }
        }
    }
}                                          
               