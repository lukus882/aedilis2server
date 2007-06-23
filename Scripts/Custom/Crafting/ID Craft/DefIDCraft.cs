using System;
using Server;
using Server.Items;

namespace Server.Engines.Craft
{
	public class DefIDCraft : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.ItemID; }
		}

		public override int GumpTitleNumber
		{
			get { return 0; } // <CENTER>ID Craft MENU</CENTER>
		}

  
                public override string GumpTitleString
		{
			get { return "<basefont color=#FFFFFF><CENTER>ID IDCraftCraft Menu</CENTER></basefont>"; }
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefIDCraft();

				return m_CraftSystem;
			}
		}

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.0; // 0%
		}

		private DefIDCraft() : base( 1, 1, 1.25 )// base( 1, 1, 3.0 )
		{
		}
		
		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if ( tool.Deleted || tool.UsesRemaining < 0 )
				return 1044038; // You have worn out your tool!
			//else if ( !BaseTool.CheckAccessible( tool, from ) )
				//return 1044263; // The tool must be on your person to use.

			return 0;
		}


		public override void PlayCraftEffect( Mobile from )
		{
			from.PlaySound( 0x248 );
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

			// Identification Stuff

			index = AddCraft( typeof( IDCrystal ), "ID Crystal", "An ID Crystal", 90.0, 100.0, typeof( ZoogiFungus ), "Zoogie Fungus", 10, 1044037 );
			AddSkill( index, SkillName.Tinkering, 50.0, 55.0 );
			AddSkill( index, SkillName.Mining, 40.0, 75.0 );

			index = AddCraft( typeof( IdentificationDeed ), "ID Deed", "An ID Deed", 90.0, 100.0, typeof( IDCrystal ), "An Id Crystal", 1, 1044037 );
			AddSkill( index, SkillName.Tinkering, 50.0, 55.0 );
			AddRes( index, typeof( BlankScroll ), "Blank Scrolls", 4, 1044351 );

			index = AddCraft( typeof( CraftedIDWand10 ), "ID Wand 10", "Item ID Wand 10 Charges", 90.0, 100.0, typeof( IronIngot ), "Iron Ingot", 2, 1044037 );
			AddSkill( index, SkillName.Tinkering, 50.0, 55.0 );
			AddRes( index, typeof( Log ), "Log", 2, 1044351 );
			AddRes( index, typeof( IDCrystal ), "ID Cyrstals", 1, 1044351 );
			

			index = AddCraft( typeof( CraftedIDWand100 ), "ID Wand 100", "Item ID Wand 100 Charges", 90.0, 100.0, typeof( IronIngot ), "Iron Ingot", 2, 1044037 );
			AddSkill( index, SkillName.Tinkering, 50.0, 55.0 );
			AddRes( index, typeof( Log ), "Log", 2, 1044351 );
			AddRes( index, typeof( IDCrystal ), "ID Cyrstals", 10, 1044351 );
			
			

			MarkOption = true;
		}
	}
}