using System;
using Server;
using Server.Spells;

namespace Server.Spells.Avatar
{
	public class AvatarInitializer
	{
		public static void Initialize()
		{
			if( Core.AOS )
			{
				Register( 210, typeof( Avatar.HeavensGateSpell ) );
				Register( 211, typeof( Avatar.MarkOfGodsSpell ) );
				Register( 212, typeof( Avatar.HeavenlyLightSpell ) );

				//RegDef( spellID, "Name", "Description", "Reagent1; Reagent2; Reagentn", "Skill; Mana; Tithe; Etc" );
				RegDef( 210, "Heaven's Gate",  "Allows the Paladin to open a heavenly portal to another location.",      null, "Tithe: 30; Skill: 80; Mana: 40" );
				RegDef( 211, "Mark Of Gods",   "Casts down a powerful bolt of lightning to mark a rune for the Palaidn", null, "Tithe: 10; Skill: 20; Mana: 10" );
				RegDef( 212, "Heavenly Light", "Heaven lights the Paladin's way.",                                       null, "Tithe: 10; Skill: 20; Mana: 10" );
			}
		}
		public static void Register( int spellID, Type type )
		{
			SpellRegistry.Register( spellID, type );
		}
		public static void RegDef( int spellID, string name, string des, string regs, string inf )
		{
			SpellDefRegistry.Register( spellID, name, des, regs, inf );
		}
	}
}