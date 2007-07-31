using System;
using Server.Items;

namespace Server.Engines.Craft
{
	public class DefNecroSummon : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Necromancy;	}
		}

		public override int GumpTitleNumber
		{
			get { return 0; } // Use String
		}

		public override string GumpTitleString
		{
			get { return "<basefont color=#FFFFFF><CENTER>NECROMANTIC SUMMONING MENU</CENTER></basefont>"; } 
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefNecroSummon();

				return m_CraftSystem;
			}
		}

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.5; // 50%
		}

		private DefNecroSummon() : base( 1, 1, 1.25 )// base( 1, 1, 3.0 )
		{
		}

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if ( tool.Deleted || tool.UsesRemaining < 0 )
				return 1044038; // You have worn out your tool!

			return 0;
		}

		public override void PlayCraftEffect( Mobile from )
		{
			// no animation
			//if ( from.Body.Type == BodyType.Human && !from.Mounted )
			//	from.Animate( 9, 5, 1, true, false, 0 );

			from.PlaySound( 0xF9 );
			from.PlaySound( 0x101 );
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
				else if ( quality == 2 )
					return 1044155; // You create an exceptional quality item.
				else				
					return 1044154; // You create the item.
			}
		}

		public override void InitCraftList()
		{
			int index = -1;

			index = AddCraft( typeof( NecroSkeletonEssence ), "Essences", "Skeleton Essence", 50.0, 80.0, typeof( Diamond ), "Diamonds", 1, "You need more diamonds." );
			AddRes( index, typeof( SoulGem ), "Soul Gem", 1, "You need a soul gem." );
			AddRes( index, typeof( DaemonBlood ), "Daemon Blood", 5, "You need more daemon blood." );

			index = AddCraft( typeof( NecroFleshGolemEssence ), "Essences", "Flesh Golem Essence", 60.0, 90.0, typeof( Diamond ), "Diamonds", 2, "You need more diamonds." );
			AddRes( index, typeof( SoulGem ), "Soul Gem", 1, "You need a soul gem." );
			AddRes( index, typeof( DaemonBlood ), "Daemon Blood", 10, "You need more daemon blood." );

			index = AddCraft( typeof( NecroBogleEssence ), "Essences", "Bogle Essence", 60.0, 90.0, typeof( Diamond ), "Diamonds", 2, "You need more diamonds." );
			AddRes( index, typeof( SoulGem ), "Soul Gem", 1, "You need a soul gem." );
			AddRes( index, typeof( DaemonBlood ), "Daemon Blood", 10, "You need more daemon blood." );

			index = AddCraft( typeof( NecroGreaterSkeletonEssence ), "Essences", "Greater Skeleton Essence", 70.0, 100.0, typeof( Diamond ), "Diamonds", 3, "You need more diamonds." );
			AddRes( index, typeof( SoulGem ), "Soul Gem", 1, "You need a soul gem." );
			AddRes( index, typeof( DaemonBlood ), "Daemon Blood", 10, "You need more daemon blood." );

			index = AddCraft( typeof( NecroMummyEssence ), "Essences", "Mummy Essence", 80.0, 110.0, typeof( Diamond ), "Diamonds", 4, "You need more diamonds." );
			AddRes( index, typeof( SoulGem ), "Soul Gem", 1, "You need a soul gem." );
			AddRes( index, typeof( DaemonBlood ), "Daemon Blood", 15, "You need more daemon blood." );

			index = AddCraft( typeof( NecroSkeletalDragonEssence ), "Essences", "Skeletal Dragon Essence", 90.0, 120.0, typeof( Diamond ), "Diamonds", 5, "You need more diamonds." );
			AddRes( index, typeof( SoulGem ), "Soul Gem", 1, "You need a soul gem." );
			AddRes( index, typeof( DaemonBlood ), "Daemon Blood", 20, "You need more daemon blood." );

			Repair = Core.AOS;
		}

}	}
