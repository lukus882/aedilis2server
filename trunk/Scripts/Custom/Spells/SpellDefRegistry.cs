using System;

namespace Server.Spells
{
	public class SpellDefRegistry
	{
		private static string[][] m_SDefs = new string[1000][]; //This should match your SpellRegistry.m_Types size

		public static string[][] SDefs{ get{ return m_SDefs; } }

		public static void Register( int spellID, string name, string des, string regs, string inf )
		{
			if( spellID < 0 || spellID >= m_SDefs.Length )
				return;

			string[] def = new string[4];
			def[0] = name;
			def[1] = des;
			def[2] = regs;
			def[3] = inf;
			m_SDefs[spellID] = def;
		}

		public static string[] GetDefFor( int spellID )
		{
			if( spellID < 0 || spellID >= m_SDefs.Length )
				return null;

			string[] s = m_SDefs[spellID];

			return s;
		}
	}
}