/*
 created by:Bad Karma aka Broadside

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
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Craft
{
	public class DefPainting : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Inscribe;	}
		}

		public override int GumpTitleNumber
		{
			get { return 0; } // Use String
		}

		public override string GumpTitleString
		{
            get { return "<basefont color=#FFFFFF><CENTER>Bad Karma's Painting Menu</CENTER></basefont>"; } 
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefPainting();

				return m_CraftSystem;
			}
		}

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.5; // 50%
		}

		private DefPainting() : base( 1, 1, 1.25 )// base( 1, 1, 1.5 )
		{
		}

		public override bool RetainsColorFrom( CraftItem item, Type type )
		{
			return false;
		}

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if ( tool.Deleted || tool.UsesRemaining < 0 )
				return 1044038; // You have worn out your tool!
			else if ( !BaseTool.CheckAccessible( tool, from ) )
				return 1044263; // The tool must be on your person to use.

			return 0;
		}

		public override void PlayCraftEffect( Mobile from )
		{
			from.PlaySound( 0x249 );
		}

		public override int PlayEndingEffect( Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item )
		{
			if ( toolBroken )
				from.SendLocalizedMessage( 1044038 ); // You have worn out your tool

			if ( failed )
			{
				if ( lostMaterial )
					return 1044043; // You failed to create the item, and some of your materials are lost.
				else
					return 1044157; // You failed to create the item, but no materials were lost.
			}
			else
			{
				if ( quality == 0 )
					return 502785; // You were barely able to make this item.  It's quality is below average.
				else if ( makersMark && quality == 2 )
					return 1044156; // You create an exceptional quality item and affix your maker's mark.
				else if ( quality == 2 )
					return 1044155; // You create an exceptional quality item.
				else				
					return 1044154; // You create the item.
			}
		}

		public override void InitCraftList()
		{
           			int index = -1;

            #region  //South Portraits 

            index = index = AddCraft(typeof(ManPortrait1South), "South Portraits", "Man Portrait 1", 65.0, 125.0, typeof(Canvas), "Canvas", 1, "You need a canvas.");
            AddRes ( index, typeof ( RedPaint ), "Red Paint" , 1 , "You need more red paint");
            AddRes ( index, typeof ( WhitePaint ), "White Paint", 1, "You need more white paint");
            AddRes ( index, typeof ( OrangePaint ), "Orange Paint", 1, "You need more orange paint");
            AddRes ( index, typeof ( BrownPaint ), "Brown Paint", 1, "You need more brown paint");
            AddRes ( index, typeof ( BlackPaint ), "Black Paint", 1, "You need more black paint");
       

            index = AddCraft(typeof(GreenWomanPortraitSouth), "South Portraits", "Woman Portrait Green", 65.0, 125.0, typeof(Canvas), "Canvas", 1, "You need a canvas.");
            AddRes ( index, typeof ( GreenPaint ), "Green Paint", 1, "You need more green paint");
            AddRes ( index, typeof ( WhitePaint ), "White Paint", 1, "You need more white paint");
            AddRes ( index, typeof ( BrownPaint ), "Brown Paint", 1, "You need more brown paint");
            AddRes ( index, typeof ( BlackPaint ), "Black Paint", 1, "You need more black paint");
            AddRes ( index, typeof ( RedPaint ), "Red Paint", 1, "You need more red paint");
       

            index = AddCraft(typeof(BlueWomanPortraitSouth), "South Portraits", "Woman Portrait Blue", 65.0, 125.0, typeof(Canvas), "Canvas", 1, "You need a canvas.");
            AddRes ( index, typeof ( BluePaint ), "Blue Paint", 1, "You need more blue paint");
            AddRes ( index, typeof ( BrownPaint ), "Brown Paint", 1, "You need more brown paint");
            AddRes ( index, typeof ( YellowPaint ), "Yellow Paint", 1, "You need more yellow paint");
            AddRes ( index, typeof ( BlackPaint ), "Black Paint", 1, "You need more black paint");
            AddRes ( index, typeof ( OrangePaint ), "Orange Paint", 1, "You need more orange paint");
       

            index = AddCraft(typeof(OrangeWomanPortraitSouth), "South Portraits", "Woman Portrait Orange", 65.0, 125.0, typeof(Canvas), "Canvas", 1, "You need a canvas.");
            AddRes ( index, typeof ( GreenPaint ), "Green Paint", 1, "You need more green paint");
            AddRes ( index, typeof ( WhitePaint ), "White Paint", 1, "You need more white paint");
            AddRes ( index, typeof ( OrangePaint ), "Orange Paint", 1, "You need more orange paint");
            AddRes ( index, typeof ( BrownPaint ), "Brown Paint", 1, "You need more brown paint");
            AddRes ( index, typeof ( YellowPaint ), "Yellow Paint", 1, "You need more yellow paint");
       

            index = AddCraft(typeof(RedWomanPortraitSouth), "South Portraits", "Woman Portrait Red", 65.0, 125.0, typeof(Canvas), "Canvas", 1, "You need a canvas.");
            AddRes ( index, typeof ( RedPaint ), "Red Paint", 1, "You need more red paint");
            AddRes ( index, typeof ( WhitePaint ), "White Paint", 1, "You need more white paint");
            AddRes ( index, typeof ( BlackPaint ), "Black Paint", 1, "You need more black paint");
            AddRes ( index, typeof ( BrownPaint ), "Brown Paint", 1, "You need more brown paint");
       

            index = AddCraft(typeof(GreenWomanPortraitSouth), "South Portraits", "Woman Portrait Green", 65.0, 125.0, typeof(Canvas), "Canvas", 1, "You need a canvas.");
            AddRes ( index, typeof ( RedPaint ), "Red Paint", 1, "You need more red paint");
            AddRes ( index, typeof ( WhitePaint ), "White Paint", 1, "You need more white paint");
            AddRes ( index, typeof ( GreenPaint ), "Green Paint", 1, "You need more green paint");
            AddRes ( index, typeof ( BrownPaint ), "Brown Paint", 1, "You need more brown paint");
            AddRes ( index, typeof ( BlackPaint ), "Black Paint", 1, "You need more black paint");
       

            index = AddCraft(typeof(CasualManPortraitSouth), "South Portraits", "Casual Man Portrait", 65.0, 125.0, typeof(Canvas), "Canvas", 1, "You need a canvas.");
            AddRes ( index, typeof ( WhitePaint ), "White Paint", 1, "You need more white paint");
            AddRes ( index, typeof ( GreenPaint ), "Green Paint", 1, "You need more green paint");
            AddRes ( index, typeof ( BrownPaint ), "Brown Paint", 1, "You need more brown paint");
            AddRes ( index, typeof ( YellowPaint ), "Yellow Paint", 1, "You need more yellow paint");
            AddRes ( index, typeof ( BlackPaint ), "Black Paint", 1, "You need more black paint");
       

            #endregion

            #region //East Protraits

            index = AddCraft(typeof(ManPortrait1East), "East Portraits", "Man Portrait 1", 65.0, 125.0, typeof(Canvas), "Canvas", 1, "You need a canvas.");
            AddRes ( index, typeof ( RedPaint ), "Red Paint", 1, "You need more red paint");
            AddRes ( index, typeof ( WhitePaint ), "White Paint", 1, "You need more white paint");
            AddRes ( index, typeof ( OrangePaint ), "Orange Paint", 1, "You need more orange paint");
            AddRes ( index, typeof ( BrownPaint ), "Brown Paint", 1, "You need more brown paint");
            AddRes ( index, typeof ( BlackPaint ), "Black Paint", 1, "You need more black paint");

            index = AddCraft(typeof(GreenWomanPortraitEast), "East Portraits", "Woman Portrait Green", 65.0, 125.0, typeof(Canvas), "Canvas", 1, "You need a canvas.");
            AddRes ( index, typeof ( RedPaint ), "Red Paint", 1, "You need more red paint");
            AddRes ( index, typeof ( WhitePaint ), "White Paint", 1, "You need more white paint");
            AddRes ( index, typeof ( GreenPaint ), "Green Paint", 1, "You need more green paint");
            AddRes ( index, typeof ( BrownPaint ), "Brown Paint", 1, "You need more brown paint");
            AddRes ( index, typeof ( BlackPaint ), "Black Paint", 1, "You need more black paint");

            index = AddCraft(typeof(BlueWomanPortraitEast), "East Portraits", "Woman Portrait Blue", 65.0, 125.0, typeof(Canvas), "Canvas", 1, "You need a canvas.");
            AddRes ( index, typeof ( BluePaint ), "Blue Paint", 1, "You need more blue paint");
            AddRes ( index, typeof ( BrownPaint ), "Brown Paint", 1, "You need more brown paint");
            AddRes ( index, typeof ( YellowPaint ), "Yellow Paint", 1, "You need more yellow paint");
            AddRes ( index, typeof ( BlackPaint ), "Black Paint", 1, "You need more black paint");
            AddRes ( index, typeof ( OrangePaint ), "Orange Paint", 1, "You need more orange paint");

            index = AddCraft(typeof(PurpleWomanPortraitEast), "East Portraits", "Woman Portrait Purple", 65.0, 125.0, typeof(Canvas), "Canvas", 1, "You need a canvas.");
            AddRes ( index, typeof ( PurplePaint ), "Purple Paint", 1, "You need more purple paint");
            AddRes ( index, typeof ( BrownPaint ), "Brown Paint", 1, "You need more brown paint");
            AddRes ( index, typeof ( WhitePaint ), "White Paint", 1, "You need more white paint");
            AddRes ( index, typeof ( BlackPaint ), "Black Paint", 1, "You need more black paint");
            AddRes ( index, typeof ( RedPaint ), "Red Paint", 1, "You need more red paint");

            #endregion

            #region //Large South Portraits 
            index = AddCraft( typeof( LargeWomanPortrait ), "Large South Portraits", "Woman Portrait Large", 80.0, 140.0, typeof( Canvas ), "Canvas", 1, "You need a canvas." );
            AddRes ( index, typeof ( RedPaint ), "Red Paint", 1, "You need more red paint");
            AddRes ( index, typeof ( WhitePaint ), "White Paint", 1, "You need more white paint");
            AddRes ( index, typeof ( OrangePaint ), "Orange Paint", 1, "You need more orange paint");
            AddRes ( index, typeof ( BrownPaint ), "Brown Paint", 1, "You need more brown paint");
            AddRes ( index, typeof ( BlackPaint ), "Black Paint", 1, "You need more black paint");
            AddRes ( index, typeof ( GreenPaint ), "Green Paint", 1, "You need more green paint");

            index = AddCraft(typeof(LargeManPortraitSouth), "Large South Portraits", "Man Portrait Large", 80.0, 140.0, typeof(Canvas), "Canvas", 1, "You need a canvas.");
            AddRes ( index, typeof ( WhitePaint ), "White Paint", 1, "You need more white paint");
            AddRes ( index, typeof ( GreenPaint ), "Green Paint", 2, "You need more green paint");
            AddRes ( index, typeof ( BrownPaint ), "Brown Paint", 1, "You need more brown paint");
            AddRes ( index, typeof ( YellowPaint ), "Yellow Paint", 1, "You need more yellow paint");
            AddRes ( index, typeof ( BlackPaint ), "Black Paint", 1, "You need more black paint");

            index = AddCraft(typeof(FancyLadyPortraitSouth1), "Large South Portraits", "Fancy Lady Portrait South", 80.0, 140.0, typeof(Canvas), "Canvas", 1, "You need a canvas.");
            AddRes ( index, typeof ( WhitePaint ), "White Paint", 1, "You need more white paint");
            AddRes ( index, typeof ( OrangePaint ), "Orange Paint", 1, "You need more orange paint");
            AddRes ( index, typeof ( BrownPaint ), "Brown Paint", 2, "You need more brown paint");
            AddRes ( index, typeof ( YellowPaint ), "Yellow Paint", 1, "You need more yellow paint");
            AddRes ( index, typeof ( BlackPaint ), "Black Paint", 1, "You need more black paint");

            index = AddCraft(typeof(YoungManPortraitSouth), "Large South Portraits", "Young Man Portrait", 80.0, 140.0, typeof(Canvas), "Canvas", 1, "You need a canvas.");
            AddRes ( index, typeof ( WhitePaint ), "White Paint", 1, "You need more white paint");
            AddRes ( index, typeof ( GreenPaint ), "Green Paint", 1, "You need more green paint");
            AddRes ( index, typeof ( BrownPaint ), "Brown Paint", 2, "You need more brown paint");
            AddRes ( index, typeof ( OrangePaint ), "Orange Paint", 1, "You need more orange paint");
            AddRes ( index, typeof ( BlackPaint ), "Black Paint", 1, "You need more black paint");

#endregion

            #region //Large East Portraits

            index = AddCraft(typeof(LargeManPortraitEast), "Large East Portraits", "Man Portrait Large", 80.0, 140.0, typeof(Canvas), "Canvas", 1, "You need a canvas.");
            AddRes ( index, typeof ( WhitePaint ), "White Paint", 1, "You need more white paint");
            AddRes ( index, typeof ( GreenPaint ), "Green Paint", 2, "You need more green paint");
            AddRes ( index, typeof ( BrownPaint ), "Brown Paint", 1, "You need more brown paint");
            AddRes ( index, typeof ( YellowPaint ), "Yellow Paint", 1, "You need more yellow paint");
            AddRes ( index, typeof ( BlackPaint ), "Black Paint", 1, "You need more black paint");

            index = AddCraft(typeof(FancyLadyPortraitEast1), "Large East Portraits", "Fancy Lady Portrait", 80.0, 140.0, typeof(Canvas), "Canvas", 1, "You need a canvas.");
            AddRes ( index, typeof ( WhitePaint ), "White Paint", 1, "You need more white paint");
            AddRes ( index, typeof ( OrangePaint ), "Orange Paint", 1, "You need more orange paint");
            AddRes ( index, typeof ( BrownPaint ), "Brown Paint", 2, "You need more brown paint");
            AddRes ( index, typeof ( YellowPaint ), "Yellow Paint", 1, "You need more yellow paint");
            AddRes ( index, typeof ( BlackPaint ), "Black Paint", 1, "You need more black paint");

            index = AddCraft(typeof(YoungManPortraitEast), "Large East Portraits", "Young Man Portrait", 80.0, 140.0, typeof(Canvas), "Canvas", 1, "You need a canvas.");
            AddRes ( index, typeof ( WhitePaint ), "White Paint", 1, "You need more white paint");
            AddRes ( index, typeof ( GreenPaint ), "Green Paint", 1, "You need more green paint");
            AddRes ( index, typeof ( BrownPaint ), "Brown Paint", 2, "You need more brown paint");
            AddRes ( index, typeof ( OrangePaint ), "Orange Paint", 1, "You need more orange paint");
            AddRes ( index, typeof ( BlackPaint ), "Black Paint", 1, "You need more black paint");
       

            #endregion

            #region //Eastern Paintings 

            index = AddCraft( typeof( RedPaintingSouth ), "Eastern Paintings", "Red Painting (South)", 30.0, 70.0, typeof( Canvas ), "Canvas", 1, "You need a canvas." );
            AddRes ( index, typeof ( WhitePaint ), "White Paint", 1, "You need more white paint");
            AddRes ( index, typeof ( BluePaint ), "Blue Paint", 1, "You need more blue paint");
            AddRes ( index, typeof ( RedPaint ), "Red Paint", 1, "You need more red paint");
       

            index = AddCraft(typeof(RedPaintingEast), "Eastern Paintings", "Red Painting (East)", 30.0, 70.0, typeof(Canvas), "Canvas", 1, "You need a canvas.");
            AddRes ( index, typeof ( WhitePaint ), "White Paint", 1, "You need more white paint");
            AddRes ( index, typeof ( BluePaint ), "Blue Paint", 1, "You need more blue paint");
            AddRes ( index, typeof ( RedPaint ), "Red Paint", 1, "You need more red paint");
       

            index = AddCraft(typeof(TallPaintingSouth), "Eastern Paintings", "Tall Painting (South)", 30.0, 70.0, typeof(Canvas), "Canvas", 1, "You need a canvas.");
            AddRes ( index, typeof ( BrownPaint ), "Brown Paint", 1, "You need more brown paint");
            AddRes ( index, typeof ( WhitePaint ), "White Paint", 2, "You need more white paint");
            AddRes ( index, typeof ( BlackPaint ), "Black Paint", 1, "You need more black paint");
       

            index = AddCraft(typeof(TallPaintingEast), "Eastern Paintings", "Tall Painting (East)", 30.0, 70.0, typeof(Canvas), "Canvas", 1, "You need a canvas.");
            AddRes ( index, typeof ( BrownPaint ), "Brown Paint", 1, "You need more brown paint");
            AddRes ( index, typeof ( WhitePaint ), "White Paint", 2, "You need more white paint");
            AddRes ( index, typeof ( BlackPaint ), "Black Paint", 1, "You need more black paint");
       

            index = AddCraft(typeof(TanPaintingSouth), "Eastern Paintings", "Tan Painting (South)", 30.0, 70.0, typeof(Canvas), "Canvas", 1, "You need a canvas.");
            AddRes ( index, typeof ( OrangePaint ), "Orange Paint", 1, "You need more orange paint");
            AddRes ( index, typeof ( WhitePaint ), "White Paint", 1, "You need more white paint");
            AddRes ( index, typeof ( BlackPaint ), "Black Paint", 1, "You need more black paint");
            AddRes ( index, typeof ( YellowPaint ), "Yellow Paint", 1, "You need more yellow paint");
       

            index = AddCraft(typeof(TanPaintingEast), "Eastern Paintings", "Tan Painting (East)", 30.0, 70.0, typeof(Canvas), "Canvas", 1, "You need a canvas.");
            AddRes ( index, typeof ( OrangePaint ), "Orange Paint", 1, "You need more orange paint");
            AddRes ( index, typeof ( WhitePaint ), "White Paint", 1, "You need more white paint");
            AddRes ( index, typeof ( BlackPaint ), "Black Paint", 1, "You need more black paint");
            AddRes ( index, typeof ( YellowPaint ), "Yellow Paint", 1, "You need more yellow paint");
       

            index = AddCraft(typeof(WarriorPaintingSouth), "Eastern Paintings", "Warrior Painting (South)", 30.0, 70.0, typeof(Canvas), "Canvas", 1, "You need a canvas.");
            AddRes ( index, typeof ( BrownPaint ), "Brown Paint", 2, "You need more brown paint");
            AddRes ( index, typeof ( WhitePaint ), "White Paint", 1, "You need more white paint");
            AddRes ( index, typeof ( BlackPaint ), "Black Paint", 1, "You need more black paint");
            AddRes ( index, typeof ( RedPaint ), "Red Paint", 1, "You need more red paint");
       

            index = AddCraft(typeof(WarriorPaintingEast), "Eastern Paintings", "Warrior Painting (East)", 30.0, 70.0, typeof(Canvas), "Canvas", 1, "You need a canvas.");
            AddRes ( index, typeof ( BrownPaint ), "Brown Paint", 2, "You need more brown paint");
            AddRes ( index, typeof ( WhitePaint ), "White Paint", 1, "You need more white paint");
            AddRes ( index, typeof ( BlackPaint ), "Black Paint", 1, "You need more black paint");
            AddRes ( index, typeof ( RedPaint ), "Red Paint", 1, "You need more red paint");
       

            index = AddCraft(typeof(EasternPaintingSouth1), "Eastern Paintings", "eastern painting (south)", 30.0, 70.0, typeof(Canvas), "Canvas", 1, "You need a canvas.");
            AddRes ( index, typeof ( BlackPaint ), "Black Paint", 1, "You need more black paint");
       

            index = AddCraft(typeof(EasternPaintingEast1), "Eastern Paintings", "eastern painting (east)", 30.0, 70.0, typeof(Canvas), "Canvas", 1, "You need a canvas.");
            AddRes ( index, typeof ( BlackPaint ), "Black Paint", 1, "You need more black paint");
       

            index = AddCraft(typeof(EasternPaintingSouth2), "Eastern Paintings", "eastern painting (south)", 30.0, 70.0, typeof(Canvas), "Canvas", 1, "You need a canvas.");
            AddRes ( index, typeof ( BlackPaint ), "Black Paint", 1, "You need more black paint");
       

            index = AddCraft(typeof(EasternPaintingEast2), "Eastern Paintings", "eastern painting (east)", 30.0, 70.0, typeof(Canvas), "Canvas", 1, "You need a canvas.");
            AddRes ( index, typeof ( BlackPaint ), "Black Paint", 1, "You need more black paint");
       

#endregion

            #region //Murals 
            index = AddCraft( typeof( MuralSouth ), "Murals", "Mural South", 70.0, 125.0, typeof( Canvas ), "Canvas", 1, "You need a canvas." );
            AddRes ( index, typeof ( OrangePaint ), "Orange Paint", 2, "You need more orange paint");
            AddRes ( index, typeof ( BrownPaint ), "Brown Paint", 2, "You need more brown paint");
            AddRes ( index, typeof ( WhitePaint ), "White Paint", 1, "You need more white paint");
            AddRes ( index, typeof ( BlackPaint ), "Black Paint", 1, "You need more black paint");
            AddRes ( index, typeof ( YellowPaint ), "Yellow Paint", 1, "You need more yellow paint");
            AddRes ( index, typeof ( RedPaint ), "Red Paint", 1, "You need more red paint");
       

            index = AddCraft(typeof( MuralEast ), "Murals", "Mural East", 70.0, 125.0, typeof(Canvas), "Canvas", 1, "You need a canvas.");
            AddRes ( index, typeof ( OrangePaint ), "Orange Paint", 2, "You need more orange paint");
            AddRes ( index, typeof ( BrownPaint ), "Brown Paint", 2, "You need more brown paint");
            AddRes ( index, typeof ( WhitePaint ), "White Paint", 1, "You need more white paint");
            AddRes ( index, typeof ( BlackPaint ), "Black Paint", 1, "You need more black paint");
            AddRes ( index, typeof ( YellowPaint ), "Yellow Paint", 1, "You need more yellow paint");
            AddRes ( index, typeof ( RedPaint ), "Red Paint", 1, "You need more red paint");

            #endregion

            MarkOption = true;

		}
	}
}