using System;
using Server.Items;

namespace Server.Engines.Craft
{
    public class DefScrimshaw : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Fishing;	}
		}

        public override int GumpTitleNumber
        {
            get { return 0; }
        }

        public override string GumpTitleString
        {
            get { return "<basefont color=#FFFFFF><CENTER>SCRIMSHAW MENU</CENTER></basefont>"; }
        }

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
                    m_CraftSystem = new DefScrimshaw();

				return m_CraftSystem;
			}
		}

        public override bool RetainsColorFrom(CraftItem item, Type type)
        {
            return true;
        }

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.5; // 50%
		}

        private DefScrimshaw() : base(1, 1, 1.25)// base( 1, 1, 3.0 )
		{
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
			// no animation
			//if ( from.Body.Type == BodyType.Human && !from.Mounted )
			//	from.Animate( 9, 5, 1, true, false, 0 );

			from.PlaySound( 0x241 );
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


		index = AddCraft( typeof( ScrimDogStatue ), "Statues", "Dog Statue", 75.0, 100.0, typeof( FishBones ), "Fish Bones", 3, "You don't have enough fish bones." );
		AddSkill( index, SkillName.Tinkering, 40.0, 45.0 );
		index = AddCraft( typeof( ScrimCatStatue ), "Statues", "Cat Statue", 75.0, 100.0, typeof( FishBones ), "Fish Bones", 3, "You don't have enough fish bones." );
		AddSkill( index, SkillName.Tinkering, 40.0, 45.0 );
		index = AddCraft( typeof( ScrimRabbitStatue ), "Statues", "Rabbit Statue", 75.0, 100.0, typeof( FishBones ), "Fish Bones", 3, "You don't have enough fish bones." );
		AddSkill( index, SkillName.Tinkering, 40.0, 45.0 );
		index = AddCraft( typeof( ScrimBirdStatue ), "Statues", "Bird Statue", 75.0, 100.0, typeof( FishBones ), "Fish Bones", 3, "You don't have enough fish bones." );
		AddSkill( index, SkillName.Tinkering, 40.0, 45.0 );
		index = AddCraft( typeof( ScrimFishStatue ), "Statues", "Fish Statue", 75.0, 100.0, typeof( FishBones ), "Fish Bones", 3, "You don't have enough fish bones." );
		AddSkill( index, SkillName.Tinkering, 40.0, 45.0 );

		index = AddCraft( typeof( ScrimCraneStatue ), "Statues", "Crane Statue", 80.0, 105.0, typeof( FishBones), "Fish Bones", 3, "You don't have enough fish bones." );
		AddSkill( index, SkillName.Tinkering, 45.0, 50.0 );
		index = AddCraft( typeof( ScrimGoatStatue ), "Statues", "Goat Statue", 80.0, 105.0, typeof( FishBones ), "Fish Bones", 3, "You don't have enough fish bones." );
		AddSkill( index, SkillName.Tinkering, 45.0, 50.0 );
		index = AddCraft( typeof( ScrimLizardStatue ), "Statues", "Lizard Statue", 80.0, 105.0, typeof( FishBones ), "Fish Bones", 5, "You don't have enough fish bones." );
		AddSkill( index, SkillName.Tinkering, 45.0, 50.0 );
		index = AddCraft( typeof( ScrimFerretStatue ), "Statues", "Ferret Statue", 80.0, 105.0, typeof( FishBones ), "Fish Bones", 5, "You don't have enough fish bones." );
		AddSkill( index, SkillName.Tinkering, 45.0, 50.0 );

		index = AddCraft( typeof( ScrimCougarStatue ), "Statues", "Cougar Statue", 90.0, 115.0, typeof( FishBones ), "Fish Bones", 8, "You don't have enough fish bones." );
		AddSkill( index, SkillName.Tinkering, 45.0, 50.0 );
		index = AddCraft( typeof( ScrimWolfStatue ), "Statues", "Wolf Statue", 90.0, 115.0, typeof( FishBones ), "Fish Bones", 8, "You don't have enough fish bones." );
		AddSkill( index, SkillName.Tinkering, 45.0, 50.0 );
		index = AddCraft( typeof( ScrimBearStatue ), "Statues", "Bear Statue", 90.0, 115.0, typeof( FishBones ), "Fish Bones", 8, "You don't have enough fish bones." );
		AddSkill( index, SkillName.Tinkering, 45.0, 50.0 );
		index = AddCraft( typeof( ScrimTreeStatue ), "Statues", "Tree Statue", 90.0, 115.0, typeof( FishBones ), "Fish Bones", 8, "You don't have enough fish bones." );
		AddSkill( index, SkillName.Tinkering, 45.0, 50.0 );

		index = AddCraft( typeof( ScrimFaerieStatue ), "Statues", "Faerie Statue", 100.0, 125.0, typeof( FishBones ), "Fish Bones", 8, "You don't have enough fish bones." );
		AddSkill( index, SkillName.Tinkering, 50.0, 55.0 );
		index = AddCraft( typeof( ScrimMageStatue ), "Statues", "Mage Statue", 100.0, 125.0, typeof( FishBones ), "Fish Bones", 8, "You don't have enough fish bones." );
		AddSkill( index, SkillName.Tinkering, 50.0, 55.0 );
		index = AddCraft( typeof( ScrimPhoenixStatue ), "Statues", "Phoenix Statue", 100.0, 125.0, typeof( FishBones ), "Fish Bones", 8, "You don't have enough fish bones." );
		AddSkill( index, SkillName.Tinkering, 50.0, 55.0 );

		index = AddCraft( typeof( SmallDecorativeNet ), "Nets", "Small Decorative Net", 65.0, 90.0, typeof( CoilRope ), "Coils of Rope", 3, "You don't have enough coils of rope." );
		AddSkill( index, SkillName.Tailoring, 40.0, 45.0 );

		index = AddCraft( typeof( LargeDecorativeNet ), "Nets", "Large Decorative Net", 70.0, 95.0, typeof( CoilRope ), "Coils of Rope", 5, "You don't have enough coils of rope." );
		AddSkill( index, SkillName.Tailoring, 50.0, 55.0 );

		index = AddCraft( typeof( FishingNet ), "Nets", "Repaired Fishing Net", 75.0, 95.0, typeof( NetPiece ), "Fishing Net Piece", 3, "You don't have enough net pieces." );
		AddRes( index, typeof( CoilRope ), "Coil of Rope", 1, "You don't have a coil of rope." );

		index = AddCraft( typeof( FishingNet ), "Nets", "Fishing Net", 80.0, 105.0, typeof( CoilRope ), "Coils of Rope", 3, "You don't have enough coils of rope." );
		AddSkill( index, SkillName.Tailoring, 50.0, 55.0 );

		//index = AddCraft( typeof( RopeLadder ), "Other", "Rope Ladder", 65.0, 90.0, typeof( CoilRope ), "Coils of Rope", 2, "You don't have enough coils of rope." );
		//AddSkill( index, SkillName.Tailoring, 40.0, 45.0 );

            // END OF MENU

			MarkOption = true;

		}
	}
}