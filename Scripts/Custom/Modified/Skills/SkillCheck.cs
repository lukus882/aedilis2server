using System;
using Server;
using Server.Mobiles;

namespace Server.Misc
{
	public class SkillCheck
	{
		private static readonly bool AntiMacroCode = false;		//Change this to false to disable anti-macro code

		public static TimeSpan AntiMacroExpire = TimeSpan.FromMinutes( 5.0 ); //How long do we remember targets/locations?
		public const int Allowance = 3;	//How many times may we use the same location/target for gain
		private const int LocationSize = 5; //The size of eeach location, make this smaller so players dont have to move as far
		private static bool[] UseAntiMacro = new bool[]
		{
			// true if this skill uses the anti-macro code, false if it does not
			false,// Alchemy = 0,
			true,// Anatomy = 1,
			true,// AnimalLore = 2,
			true,// ItemID = 3,
			true,// ArmsLore = 4,
			false,// Parry = 5,
			true,// Begging = 6,
			false,// Blacksmith = 7,
			false,// Fletching = 8,
			true,// Peacemaking = 9,
			true,// Camping = 10,
			false,// Carpentry = 11,
			false,// Cartography = 12,
			false,// Cooking = 13,
			true,// DetectHidden = 14,
			true,// Discordance = 15,
			true,// EvalInt = 16,
			true,// Healing = 17,
			true,// Fishing = 18,
			true,// Forensics = 19,
			true,// Herding = 20,
			true,// Hiding = 21,
			true,// Provocation = 22,
			false,// Inscribe = 23,
			true,// Lockpicking = 24,
			true,// Magery = 25,
			true,// MagicResist = 26,
			false,// Tactics = 27,
			true,// Snooping = 28,
			true,// Musicianship = 29,
			true,// Poisoning = 30,
			false,// Archery = 31,
			true,// SpiritSpeak = 32,
			true,// Stealing = 33,
			false,// Tailoring = 34,
			true,// AnimalTaming = 35,
			true,// TasteID = 36,
			false,// Tinkering = 37,
			true,// Tracking = 38,
			true,// Veterinary = 39,
			false,// Swords = 40,
			false,// Macing = 41,
			false,// Fencing = 42,
			false,// Wrestling = 43,
			true,// Lumberjacking = 44,
			true,// Mining = 45,
			true,// Meditation = 46,
			true,// Stealth = 47,
			true,// RemoveTrap = 48,
			true,// Necromancy = 49,
			false,// Focus = 50,
			true,// Chivalry = 51
			true,// Bushido = 52
			true,// Ninjitsu = 53
			true // Spellweaving = 54
		};

		public static void Initialize()
		{
			// Begin mod to enable XmlSpawner skill triggering
			Mobile.SkillCheckLocationHandler = new SkillCheckLocationHandler( XmlSpawnerSkillCheck.Mobile_SkillCheckLocation );
			Mobile.SkillCheckDirectLocationHandler = new SkillCheckDirectLocationHandler( XmlSpawnerSkillCheck.Mobile_SkillCheckDirectLocation );
			
			Mobile.SkillCheckTargetHandler = new SkillCheckTargetHandler( XmlSpawnerSkillCheck.Mobile_SkillCheckTarget );
			Mobile.SkillCheckDirectTargetHandler = new SkillCheckDirectTargetHandler( XmlSpawnerSkillCheck.Mobile_SkillCheckDirectTarget );
			// End mod to enable XmlSpawner skill triggering

//*********EDIT THESE LINES TO SET THE BASE PERCENT CHANCE TO GAIN FOR EACH INDIVIDUAL SKILL***********************//
			//This Table used to set the base chance to gain skill between 40 and 70
         //for instance .15 would be a 15% chance to gain in the particular skill

         SkillInfo.Table[0].GainFactor = .10;// Alchemy = 0, 
         SkillInfo.Table[1].GainFactor = .12;// Anatomy = 1, 
         SkillInfo.Table[2].GainFactor = .12;// AnimalLore = 2, 
         SkillInfo.Table[3].GainFactor = .12;// ItemID = 3, 
         SkillInfo.Table[4].GainFactor = .12;// ArmsLore = 4, 
         SkillInfo.Table[5].GainFactor = .12;// Parry = 5, 
         SkillInfo.Table[6].GainFactor = .10;// Begging = 6, 
         SkillInfo.Table[7].GainFactor = .10;// Blacksmith = 7, 
         SkillInfo.Table[8].GainFactor = .10;// Fletching = 8, 
         SkillInfo.Table[9].GainFactor = .10;// Peacemaking = 9, 
         SkillInfo.Table[10].GainFactor = .10;// Camping = 10, 
         SkillInfo.Table[11].GainFactor = .12;// Carpentry = 11, 
         SkillInfo.Table[12].GainFactor = .12;// Cartography = 12, 
         SkillInfo.Table[13].GainFactor = .12;// Cooking = 13, 
         SkillInfo.Table[14].GainFactor = .12;// DetectHidden = 14, 
         SkillInfo.Table[15].GainFactor = .12;// Discordance = 15, 
         SkillInfo.Table[16].GainFactor = .12;// EvalInt = 16, 
         SkillInfo.Table[17].GainFactor = .12;// Healing = 17, 
         SkillInfo.Table[18].GainFactor = .12;// Fishing = 18, 
         SkillInfo.Table[19].GainFactor = .12;// Forensics = 19, 
         SkillInfo.Table[20].GainFactor = .11;// Herding = 20, 
         SkillInfo.Table[21].GainFactor = .12;// Hiding = 21, 
         SkillInfo.Table[22].GainFactor = .12;// Provocation = 22, 
         SkillInfo.Table[23].GainFactor = .14;// Inscribe = 23, 
         SkillInfo.Table[24].GainFactor = .15;// Lockpicking = 24, 
         SkillInfo.Table[25].GainFactor = .12;// Magery = 25, 
         SkillInfo.Table[26].GainFactor = .12;// MagicResist = 26, 
         SkillInfo.Table[27].GainFactor = .12;// Tactics = 27, 
         SkillInfo.Table[28].GainFactor = .12;// Snooping = 28, 
         SkillInfo.Table[29].GainFactor = .11;// Musicianship = 29, 
         SkillInfo.Table[30].GainFactor = .15;// Poisoning = 30 
         SkillInfo.Table[31].GainFactor = .12;// Archery = 31 
         SkillInfo.Table[32].GainFactor = .10;// SpiritSpeak = 32 
         SkillInfo.Table[33].GainFactor = .12;// Stealing = 33 
         SkillInfo.Table[34].GainFactor = .12;// Tailoring = 34 
         SkillInfo.Table[35].GainFactor = .12;// AnimalTaming = 35 
         SkillInfo.Table[36].GainFactor = .12;// TasteID = 36 
         SkillInfo.Table[37].GainFactor = .12;// Tinkering = 37 
         SkillInfo.Table[38].GainFactor = .15;// Tracking = 38 
         SkillInfo.Table[39].GainFactor = .12;// Veterinary = 39 
         SkillInfo.Table[40].GainFactor = .10;// Swords = 40 
         SkillInfo.Table[41].GainFactor = .12;// Macing = 41 
         SkillInfo.Table[42].GainFactor = .12;// Fencing = 42 
         SkillInfo.Table[43].GainFactor = .12;// Wrestling = 43 
         SkillInfo.Table[44].GainFactor = .10;// Lumberjacking = 44 
         SkillInfo.Table[45].GainFactor = .10;// Mining = 45 
         SkillInfo.Table[46].GainFactor = .12;// Meditation = 46 
         SkillInfo.Table[47].GainFactor = .12;// Stealth = 47 
         SkillInfo.Table[48].GainFactor = .14;// RemoveTrap = 48 
         SkillInfo.Table[49].GainFactor = .11;// Necromancy = 49 
         SkillInfo.Table[50].GainFactor = .12;// Focus = 50 
         SkillInfo.Table[51].GainFactor = .12;// Chivalry = 51
         SkillInfo.Table[52].GainFactor = .10;// Bushido = 52
         SkillInfo.Table[53].GainFactor = .10;// Ninjitsu = 53
         SkillInfo.Table[54].GainFactor = .10;// Spellweaving = 54

		}

        public static bool Mobile_SkillCheckLocation(Mobile from, SkillName skillName, double minSkill, double maxSkill)
        {
            Skill skill = from.Skills[skillName];

            if (skill == null)
                return false;

            double value = skill.Value;

            if (value < minSkill)
                return false; // Too difficult
			else if ( value >= maxSkill )
                return true; // No challenge

            double chance = (value - minSkill) / (maxSkill - minSkill);

            Point2D loc = new Point2D(from.Location.X / LocationSize, from.Location.Y / LocationSize);
			return CheckSkill( from, skill, loc, chance );
        }

		public static bool Mobile_SkillCheckDirectLocation( Mobile from, SkillName skillName, double chance )
		{
			Skill skill = from.Skills[skillName];

			if ( skill == null )
				return false;

			if ( chance < 0.0 )
				return false; // Too difficult
			else if ( chance >= 1.0 )
				return true; // No challenge

			Point2D loc = new Point2D( from.Location.X / LocationSize, from.Location.Y / LocationSize );
			return CheckSkill( from, skill, loc, chance );
		}

        public static bool CheckSkill(Mobile from, Skill skill, object amObj, double chance)
        {
            if (from.Skills.Cap == 0)
				return false;

			bool success = ( chance >= Utility.RandomDouble() );

//**********EDIT THESE LINES FOR MODIFICATIONS TO BASE PERCENTAGE AT DIFFERENT SKILL LEVELS************************//
			double LowSkillMod = .25; // percent added to skill gain for skill between 0 - 40
			double BaseSkillGain = skill.Info.GainFactor;  // base percent to gain at skill between 40 - 70 set in table above.
			double HighSkillMod = .05; // percent subtracted from base gain for skill between 70 - 100
			double AboveGmSkillMod = .07; // percent subtracted from base gain for skill over 100
			//Example: Player Skill is 85.3 - We would take the 15% base chance and subtract 8% - Player now has a 7% chance to gain.
            //*****************************************************************************************************************//
           
			double ComputedSkillMod = 0; // holds the percent chance to gain after checking players skill

			if ( skill.Base < 40.1 ) 
                ComputedSkillMod = BaseSkillGain + LowSkillMod;
			else if ( skill.Base < 70.1 ) 
				ComputedSkillMod = BaseSkillGain;                 
			else if ( skill.Base < 100.1 ) 
				ComputedSkillMod = BaseSkillGain - HighSkillMod;                
			else if ( skill.Base > 100.0 ) 
				ComputedSkillMod = BaseSkillGain - AboveGmSkillMod;  

			if ( ComputedSkillMod < 0.01 ) 
				ComputedSkillMod = 0.01;

            //following line used to see chance to gain ingame
            if (from.AccessLevel > AccessLevel.Player)
	    from.SendMessage( "Your chance to gain {0} is {1}",skill.Name, ComputedSkillMod );
            if (from is BaseCreature && ((BaseCreature)from).Controlled)
                ComputedSkillMod *= 2;

            if (from.Alive && ((ComputedSkillMod >= Utility.RandomDouble() && AllowGain(from, skill, amObj)) || skill.Base < 10.0))
                Gain(from, skill);

            return success;
        }

		public static bool Mobile_SkillCheckTarget( Mobile from, SkillName skillName, object target, double minSkill, double maxSkill )
		{
			Skill skill = from.Skills[skillName];

			if ( skill == null )
				return false;

			double value = skill.Value;

			if ( value < minSkill )
				return false; // Too difficult
			else if ( value >= maxSkill )
				return true; // No challenge

			double chance = (value - minSkill) / (maxSkill - minSkill);

			return CheckSkill( from, skill, target, chance );
		}

		public static bool Mobile_SkillCheckDirectTarget( Mobile from, SkillName skillName, object target, double chance )
		{
			Skill skill = from.Skills[skillName];

			if ( skill == null )
				return false;

			if ( chance < 0.0 )
				return false; // Too difficult
			else if ( chance >= 1.0 )
				return true; // No challenge

			return CheckSkill( from, skill, target, chance );
		}

		private static bool AllowGain( Mobile from, Skill skill, object obj )
		{
			if ( from is PlayerMobile && AntiMacroCode && UseAntiMacro[skill.Info.SkillID] )
				return ((PlayerMobile)from).AntiMacroCheck( skill, obj );
			else
				return true;
		}

		public enum Stat { Str, Dex, Int }

		public static void Gain( Mobile from, Skill skill )
		{
			if ( from.Region.IsPartOf( typeof( Regions.Jail ) ) )
				return;

			if ( from is BaseCreature && ((BaseCreature)from).IsDeadPet )
				return;

			if ( skill.SkillName == SkillName.Focus && from is BaseCreature )
				return;

            if (skill.Base < skill.Cap && skill.Lock == SkillLock.Up)
            {
                int toGain = 1;

				if ( skill.Base <= 10.0 )
					toGain = Utility.Random( 4 ) + 1;

                Skills skills = from.Skills;
                if ((skills.Total / skills.Cap) >= Utility.RandomDouble())//( skills.Total >= skills.Cap )
                {
                    for (int i = 0; i < skills.Length; ++i)
                    {
                        Skill toLower = skills[i];
                        if (toLower != skill && toLower.Lock == SkillLock.Down && toLower.BaseFixedPoint >= toGain)
                        {
                            toLower.BaseFixedPoint -= toGain;
                            break;
                        }
                    }
                }

                if ((skills.Total + toGain) <= skills.Cap)
                {
                    skill.BaseFixedPoint += toGain;
                }
            }

            if (skill.Lock == SkillLock.Up)
            {
                SkillInfo info = skill.Info;

//*****EDIT THIS LINE TO MAKE STATS GAIN FASTER, NUMBER MUST BE BETWEEN 0.00 and 1.00, THIS IS ADDED TO THE CHANCE TO GAIN IN A STAT********//
				double StatGainBonus = .15; //Extra chance to gain in stats. Left at 0 would be default runuo gains.
//**************************************************  **************************************************  **************************************//

				if ( from.StrLock == StatLockType.Up && ((info.StrGain / 33.3) + StatGainBonus) > Utility.RandomDouble() )
                {
                    if (info.StrGain != 0)
                        GainStat(from, Stat.Str);
                }
				else if ( from.DexLock == StatLockType.Up && ((info.DexGain / 33.3) + StatGainBonus) > Utility.RandomDouble() )
                {
                    if (info.DexGain != 0)
                        GainStat(from, Stat.Dex);
                }
				else if ( from.IntLock == StatLockType.Up && ((info.IntGain / 33.3) + StatGainBonus) > Utility.RandomDouble() )
                {
                    if (info.IntGain != 0)
                        GainStat(from, Stat.Int);
                }
                //following line used to show chance to gain stats ingame
if (from.AccessLevel > AccessLevel.Player)
from.SendMessage( "Str: {0} Dex: {1} Int: {2}",((info.StrGain / 33.3) + StatGainBonus),((info.DexGain / 33.3) + StatGainBonus),((info.IntGain / 33.3) + StatGainBonus) );
            }
        }

		public static bool CanLower( Mobile from, Stat stat )
		{
			switch ( stat )
			{
				case Stat.Str: return ( from.StrLock == StatLockType.Down && from.RawStr > 10 );
				case Stat.Dex: return ( from.DexLock == StatLockType.Down && from.RawDex > 10 );
				case Stat.Int: return ( from.IntLock == StatLockType.Down && from.RawInt > 10 );
			}

			return false;
		}

		public static bool CanRaise( Mobile from, Stat stat )
		{
			if ( !(from is BaseCreature && ((BaseCreature)from).Controlled) )
			{
				if ( from.RawStatTotal >= from.StatCap )
					return false;
			}

			switch ( stat )
			{
				case Stat.Str: return ( from.StrLock == StatLockType.Up && from.RawStr < from.StatCap );
				case Stat.Dex: return ( from.DexLock == StatLockType.Up && from.RawDex < from.StatCap );
				case Stat.Int: return ( from.IntLock == StatLockType.Up && from.RawInt < from.StatCap );
			}

			return false;
		}

		public static void IncreaseStat( Mobile from, Stat stat, bool atrophy )
		{
			atrophy = atrophy || (from.RawStatTotal >= from.StatCap);

			switch ( stat )
			{
				case Stat.Str:
				{
					if ( atrophy )
					{
						if ( CanLower( from, Stat.Dex ) && (from.RawDex < from.RawInt || !CanLower( from, Stat.Int )) )
							--from.RawDex;
						else if ( CanLower( from, Stat.Int ) )
							--from.RawInt;
					}

					if ( CanRaise( from, Stat.Str ) )
						++from.RawStr;

					break;
				}
				case Stat.Dex:
				{
					if ( atrophy )
					{
						if ( CanLower( from, Stat.Str ) && (from.RawStr < from.RawInt || !CanLower( from, Stat.Int )) )
							--from.RawStr;
						else if ( CanLower( from, Stat.Int ) )
							--from.RawInt;
					}

					if ( CanRaise( from, Stat.Dex ) )
						++from.RawDex;

					break;
				}
				case Stat.Int:
				{
					if ( atrophy )
					{
						if ( CanLower( from, Stat.Str ) && (from.RawStr < from.RawDex || !CanLower( from, Stat.Dex )) )
							--from.RawStr;
						else if ( CanLower( from, Stat.Dex ) )
							--from.RawDex;
					}

					if ( CanRaise( from, Stat.Int ) )
						++from.RawInt;

					break;
				}
			}
		}

		private static TimeSpan m_StatGainDelay = TimeSpan.FromMinutes( 10.0 );

		public static void GainStat( Mobile from, Stat stat )
		{
			if ( (from.LastStatGain + m_StatGainDelay) >= DateTime.Now )
						return;

			from.LastStatGain = DateTime.Now;

			bool atrophy = ( (from.RawStatTotal / (double)from.StatCap) >= Utility.RandomDouble() );

			IncreaseStat( from, stat, atrophy );
		}
	}
}