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
using Server.Engines.Craft;
using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Craft
{
	public class DefPlantGrinding : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Alchemy;	}
		}

		public override int GumpTitleNumber
		{
			get { return 0; } // Use String
		}

		public override string GumpTitleString
		{
            get { return "<basefont color=#FFFFFF><CENTER>Bad Karma's Plant Grinding Menu</CENTER></basefont>"; } 
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefPlantGrinding();

				return m_CraftSystem;
			}
		}

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.5; // 50%
		}

        private DefPlantGrinding()
            : base(1, 1, 1.25)// base( 1, 1, 1.5 )
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
			from.PlaySound( 0x242 );
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
//			int index = -1;

			// PaintPaste //
			AddCraft(typeof(RedPaste), "PaintPaste", "Red Paste", 65.0, 125.0, typeof( BrightRedLilies ), "Decorative Bright Red Lilies", 1, "You need Bright Red Lilies." );
            AddCraft(typeof(BluePaste), "PaintPaste", "Blue Paste", 65.0, 125.0, typeof(BrightBlueLilies), "Decorative Bright Blue Lilies", 1, "You need Bright Blue Lilies.");
            AddCraft(typeof(YellowPaste), "PaintPaste", "Yellow Paste", 65.0, 125.0, typeof(BrightYellowLilies), "Decorative Bright Yellow Lilies", 1, "You need  Bright Yellow Lilies.");
            AddCraft(typeof(WhitePaste), "PaintPaste", "White Paste", 65.0, 125.0, typeof(WhiteLilies), "Decorative White Lilies", 1, "You need White Lilies.");
            AddCraft(typeof(BlackPaste), "PaintPaste", "Black Paste", 65.0, 125.0, typeof(BlackLilies), "Decorative Black Lilies", 1, "You need Black Lilies.");

			MarkOption = true;

		}
	}
}