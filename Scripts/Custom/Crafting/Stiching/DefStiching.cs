using System;
using Server;
using Server.Items;

namespace Server.Engines.Craft
{
	public class DefStiching : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Tailoring; }
		}

		public override int GumpTitleNumber
		{
			get { return 0; } // <CENTER>STICHING MENU</CENTER>
		}

  
                public override string GumpTitleString
		{
			get { return "<basefont color=#FFFFFF><CENTER>Stiching Menu</CENTER></basefont>"; }
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefStiching();

				return m_CraftSystem;
			}
		}

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.5; // 50%
		}

		private DefStiching() : base( 1, 1, 1.25 )// base( 1, 1, 3.0 )
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

			// Curtians

			index = AddCraft( typeof( RedCurtianSouth ), "Curtians", "red curtian south", 100.0, 120.0, typeof( Cloth ), 1044286, 150, 1044287 );
			index = AddCraft( typeof( RedCurtianEast ), "Curtians", "red curtian east", 100.0, 120.0, typeof( Cloth ), 1044286, 150, 1044287 );
			index = AddCraft( typeof( RedCurtian ), "Curtians", "red curtian", 100.0, 120.0, typeof( Cloth ), 1044286, 150, 1044287 );
			index = AddCraft( typeof( SingleWhiteCurtian ), "Curtians", "white curtian", 100.0, 120.0, typeof( Cloth ), 1044286, 50, 1044287 );
			index = AddCraft( typeof( MiniRedCurtian ), "Curtians", "mini red curtian", 100.0, 120.0, typeof( Cloth ), 1044286, 25, 1044287 );
			index = AddCraft( typeof( MiniWhiteCurtian ), "Curtians", "mini white curtian", 100.0, 120.0, typeof( Cloth ), 1044286, 25, 1044287 );

			// Bedding
			index = AddCraft( typeof( DarkFoldedBlanket ), "Bedding", "Dark Folded Blanket", 100.0, 120.0, typeof( Cloth ), 1044286, 75, 1044287 );
			index = AddCraft( typeof( LightFoldedBlanket ), "Bedding", "Light Folded Blanket", 100.0, 120.0, typeof( Cloth ), 1044286, 75, 1044287 );
			index = AddCraft( typeof( FoldedSheet ), "Bedding", "Folded Sheet", 100.0, 120.0, typeof( Cloth ), 1044286, 75, 1044287 );
			index = AddCraft( typeof( RichFoldedSheet ), "Bedding", "Rich Folded Sheet", 100.0, 120.0, typeof( Cloth ), 1044286, 75, 1044287 );

			// Pillows
			index = AddCraft( typeof( BigPillow ), "Pillows", "Big Pillow", 100.0, 120.0, typeof( Cloth ), 1044286, 85, 1044287 );
			index = AddCraft( typeof( MediumPillow ), "Pillows", "Medium Pillow", 100.0, 120.0, typeof( Cloth ), 1044286, 75, 1044287 );
			index = AddCraft( typeof( SmallPillow ), "Pillows", "Small Pillow", 100.0, 120.0, typeof( Cloth ), 1044286, 65, 1044287 );

			// Towels
			index = AddCraft( typeof( Towel ), "Towels", "Towel", 100.0, 120.0, typeof( Cloth ), 1044286, 55, 1044287 );

			MarkOption = true;
		}
	}
}