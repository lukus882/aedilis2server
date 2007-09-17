using System;
using Server;
using Server.Items;

namespace Server.Engines.Craft
{
	public class DefAllSpellCrafting : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Alchemy; }
		}

		public override int GumpTitleNumber
		{
			get { return 0; }
		}

  
                public override string GumpTitleString
		{
			get { return "<basefont color=#FFFFFF><CENTER>All Spell Crafting</CENTER></basefont>"; }
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefAllSpellCrafting();

				return m_CraftSystem;
			}
		}

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.0; // 0%
		}

		private DefAllSpellCrafting() : base( 1, 1, 1.25 )// base( 1, 1, 3.0 )
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

			index = AddCraft( typeof( GarlicOrb ), "Mage Essences", "Essence of Garlic", 90.0, 100.0, typeof( Garlic ), "Garlic", 10, "You do not have enough garlic" );
			index = AddCraft( typeof( SpidersSilkOrb ), "Mage Essences", "Essence Of Spider's Silk", 90.0, 100.0, typeof( SpidersSilk ), "Spider's Silk", 100, "You don't have enough Spider's Silk."  );
			index = AddCraft( typeof( SulfurousAshOrb ), "Mage Essences", "Essence Of  Sulfurous Ash", 90.0, 100.0, typeof( SulfurousAsh ), "Sulfurous Ash", 100, "You don't have enough Sulfurous Ash."  );
			index = AddCraft( typeof( GinsengOrb ), "Mage Essences", "Essence Of  Ginseng", 90.0, 100.0, typeof( Ginseng ), "Ginseng", 100, "You don't have enough Ginseng."  );
			index = AddCraft( typeof( BlackPearlOrb ), "Mage Essences", "Essence Of  Black Pearl", 90.0, 100.0, typeof( BlackPearl ), "Black Pearl", 100, "You don't have enough Black Pearl."  );
			index = AddCraft( typeof( MandrakeOrb ), "Mage Essences", "Essence Of  Mandrake", 90.0, 100.0, typeof( MandrakeRoot ), "Mandrake Root", 100, "You don't have enough Mandrake Root."  );
			index = AddCraft( typeof( BloodMossOrb ), "Mage Essences", "Essence Of  Blood Moss", 90.0, 100.0, typeof( Bloodmoss ), "Bloodmoss", 100, "You don't have enough Blood Moss."  );
			index = AddCraft( typeof( NightshadeOrb ), "Mage Essences", "Essence Of  Night Shade", 90.0, 100.0, typeof( Nightshade ), "Nightshade", 100, "You don't have enough Nightshade."  );
			
			index = AddCraft( typeof( LevelOneRunicCondenser ), "Runic Condensers", "Level One Runic Condenser", 90.0, 100.0, typeof( GarlicOrb ), "Essence Of Garlic", 1, "You don't have enough essence." );
			AddRes( index, typeof(BloodMossOrb), "Essence Of Blood Moss", 1, "You don't have enough essence.");
			AddRes( index, typeof(SpidersSilkOrb), "Essence Of Spider Silk", 1, "You don't have enough essence.");
			AddRes( index, typeof(SulfurousAshOrb), "Essence Of Sulfurous Ash", 1, "You don't have enough essence.");

			index = AddCraft( typeof( LevelTwoRunicCondenser ), "Runic Condensers", "Level Two Runic Condenser", 90.0, 100.0, typeof( GinsengOrb ), "Essence Of Ginseng", 1, "You don't have enough essence." );
			AddRes( index, typeof(BlackPearlOrb), "Essence Of Black Pearl", 1, "You don't have enough essence.");
			AddRes( index, typeof(MandrakeOrb), "Essence Of Mandrake Root", 1, "You don't have enough essence.");
			AddRes( index, typeof(NightshadeOrb), "Essence Of Night Shade", 1, "You don't have enough essence.");

			index = AddCraft( typeof( RunicInk ), "All Spells", "Runic Ink", 90.0, 100.0, typeof( LevelOneRunicCondenser ), "Level One Runic Condenser", 1, "You don't have a Runic Condenser." );
			AddRes( index, typeof(LevelTwoRunicCondenser), "Level Two Runic Condenser", 1, "You don't have a Runic Condenser." );
			AddSkill( index, SkillName.Inscribe, 90.0, 100.0 );
		

			
			

			MarkOption = true;
		}
	}
}