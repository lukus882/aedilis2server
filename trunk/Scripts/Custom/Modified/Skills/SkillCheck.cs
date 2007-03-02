using System;
using Server;
using Server.Mobiles;
using Server.RateDef;

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

         //here is gainrates 4 each skill
         //for instance .15 would be a 15% chance to gain in the particular skill

         SkillInfo.Table[0].GainFactor = .12;// Alchemy = 0, 
         SkillInfo.Table[1].GainFactor = .12;// Anatomy = 1, 
         SkillInfo.Table[2].GainFactor = .12;// AnimalLore = 2, 
         SkillInfo.Table[3].GainFactor = .12;// ItemID = 3, 
         SkillInfo.Table[4].GainFactor = .12;// ArmsLore = 4, 
         SkillInfo.Table[5].GainFactor = .12;// Parry = 5, 
         SkillInfo.Table[6].GainFactor = .12;// Begging = 6, 
         SkillInfo.Table[7].GainFactor = .12;// Blacksmith = 7, 
         SkillInfo.Table[8].GainFactor = .12;// Fletching = 8, 
         SkillInfo.Table[9].GainFactor = .12;// Peacemaking = 9, 
         SkillInfo.Table[10].GainFactor = .12;// Camping = 10, 
         SkillInfo.Table[11].GainFactor = .12;// Carpentry = 11, 
         SkillInfo.Table[12].GainFactor = .12;// Cartography = 12, 
         SkillInfo.Table[13].GainFactor = .12;// Cooking = 13, 
         SkillInfo.Table[14].GainFactor = .12;// DetectHidden = 14, 
         SkillInfo.Table[15].GainFactor = .12;// Discordance = 15, 
         SkillInfo.Table[16].GainFactor = .12;// EvalInt = 16, 
         SkillInfo.Table[17].GainFactor = .12;// Healing = 17, 
         SkillInfo.Table[18].GainFactor = .12;// Fishing = 18, 
         SkillInfo.Table[19].GainFactor = .12;// Forensics = 19, 
         SkillInfo.Table[20].GainFactor = .15;// Herding = 20, 
         SkillInfo.Table[21].GainFactor = .15;// Hiding = 21, 
         SkillInfo.Table[22].GainFactor = .12;// Provocation = 22, 
         SkillInfo.Table[23].GainFactor = .15;// Inscribe = 23, 
         SkillInfo.Table[24].GainFactor = .15;// Lockpicking = 24, 
         SkillInfo.Table[25].GainFactor = .12;// Magery = 25, 
         SkillInfo.Table[26].GainFactor = .15;// MagicResist = 26, 
         SkillInfo.Table[27].GainFactor = .12;// Tactics = 27, 
         SkillInfo.Table[28].GainFactor = .12;// Snooping = 28, 
         SkillInfo.Table[29].GainFactor = .11;// Musicianship = 29, 
         SkillInfo.Table[30].GainFactor = .15;// Poisoning = 30 
         SkillInfo.Table[31].GainFactor = .12;// Archery = 31 
         SkillInfo.Table[32].GainFactor = .15;// SpiritSpeak = 32 
         SkillInfo.Table[33].GainFactor = .12;// Stealing = 33 
         SkillInfo.Table[34].GainFactor = .12;// Tailoring = 34 
         SkillInfo.Table[35].GainFactor = .15;// AnimalTaming = 35 
         SkillInfo.Table[36].GainFactor = .12;// TasteID = 36 
         SkillInfo.Table[37].GainFactor = .12;// Tinkering = 37 
         SkillInfo.Table[38].GainFactor = .15;// Tracking = 38 
         SkillInfo.Table[39].GainFactor = .15;// Veterinary = 39 
         SkillInfo.Table[40].GainFactor = .12;// Swords = 40 
         SkillInfo.Table[41].GainFactor = .12;// Macing = 41 
         SkillInfo.Table[42].GainFactor = .12;// Fencing = 42 
         SkillInfo.Table[43].GainFactor = .12;// Wrestling = 43 
         SkillInfo.Table[44].GainFactor = .12;// Lumberjacking = 44 
         SkillInfo.Table[45].GainFactor = .12;// Mining = 45 
         SkillInfo.Table[46].GainFactor = .15;// Meditation = 46 
         SkillInfo.Table[47].GainFactor = .15;// Stealth = 47 
         SkillInfo.Table[48].GainFactor = .13;// RemoveTrap = 48 
         SkillInfo.Table[49].GainFactor = .10;// Necromancy = 49 
         SkillInfo.Table[50].GainFactor = .15;// Focus = 50 
         SkillInfo.Table[51].GainFactor = .12;// Chivalry = 51
         SkillInfo.Table[52].GainFactor = .12;// Bushido = 52
         SkillInfo.Table[53].GainFactor = .12;// Ninjitsu = 53
         SkillInfo.Table[54].GainFactor = .12;// Spellweaving = 54

		}

        public static bool Mobile_SkillCheckLocation(Mobile from, SkillName skillName, double minSkill, double maxSkill)
        {
            return Mobile_SkillCheckLocation(from, skillName, minSkill, maxSkill, maxSkill);
        }

        public static bool Mobile_SkillCheckLocation(Mobile from, SkillName skillName, double minSkill, double maxSkill, double maxSkillGain)
        {
            Skill skill = from.Skills[skillName];

            if (skill == null)
                return false;

            double value = skill.Value;

            if (value < minSkill)
                return false; // Too difficult
            else if (value >= maxSkill && maxSkill >= maxSkillGain)
                return true; // No challenge

            double chance = (value - minSkill) / (maxSkill - minSkill);
            double chanceG = (value - minSkill) / (maxSkillGain - minSkill);

            Point2D loc = new Point2D(from.Location.X / LocationSize, from.Location.Y / LocationSize);

            CheckSkill(from, skill, loc, chanceG);

            return chance >= Utility.RandomDouble();
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
            bool success = (chance >= Utility.RandomDouble());

            //here is modifications 2 skillgains
            double BaseSkillGain = skill.Info.GainFactor;
            double LowSkillMod = .10;
            double aSkillMod = .03;
            double bSkillMod = .05;
            double cSkillMod = .06;
            double dSkillMod = .07;
            double eSkillMod = .08;
            double fSkillMod = .085;
            double gSkillMod = .09;
            double hSkillMod = .095;
            double iSkillMod = .096;
            double jSkillMod = .097;
            double kSkillMod = .098;

            //Example: Player Skill is 85.3 - We would take the 12% base chance and subtract 8% - Player now has a 4% chance to gain.
            //*****************************************************************************************************************//
            double ComputedSkillMod = 0; // holds the percent chance to gain after checking players skill

            //here is modding gainrates. i.c. under lv 20% lowskillmod added 2 gain rate. from 20 to 40 askillmod substracted from it

            if (skill.Base < 20.0)
                ComputedSkillMod = BaseSkillGain + LowSkillMod;
            else if (skill.Base < 40.0)
                ComputedSkillMod = BaseSkillGain - aSkillMod;
            else if (skill.Base < 80.0)
                ComputedSkillMod = BaseSkillGain - bSkillMod;
            else if (skill.Base < 90.0)
                ComputedSkillMod = BaseSkillGain - cSkillMod;
            else if (skill.Base < 100.0)
                ComputedSkillMod = BaseSkillGain - dSkillMod;
            else if (skill.Base < 110.0)
                ComputedSkillMod = BaseSkillGain - eSkillMod;
            else if (skill.Base < 120.0)
                ComputedSkillMod = BaseSkillGain - fSkillMod;
            else if (skill.Base < 130.0)
                ComputedSkillMod = BaseSkillGain - gSkillMod;
            else if (skill.Base < 140.0)
                ComputedSkillMod = BaseSkillGain - hSkillMod;
            else if (skill.Base < 160.0)
                ComputedSkillMod = BaseSkillGain - iSkillMod;
            else if (skill.Base < 180.0)
                ComputedSkillMod = BaseSkillGain - jSkillMod;
            else if (skill.Base >= 200.0)
                ComputedSkillMod = BaseSkillGain - kSkillMod;

            //if u make substraction larger than skill gain rate
            if (ComputedSkillMod < 0.001)
                ComputedSkillMod = 0.001;

            //following line used to see chance to gain ingame
            if (from.AccessLevel > AccessLevel.Player)
	    from.SendMessage( "Your chance to gain {0} is {1}",skill.Name, ComputedSkillMod );
            if (from is BaseCreature && ((BaseCreature)from).Controlled)
                ComputedSkillMod *= 2;
            ComputedSkillMod *= RateDefinitions.ratebonus;
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
                if (skill.Base < 20.0)
                    toGain = Utility.Random(8) + 7;
                else if (skill.Base < 40.0)
                    toGain = Utility.Random(7) + 6;
                else if (skill.Base < 60.0)
                    toGain = Utility.Random(6) + 5;
                else if (skill.Base < 80.0)
                    toGain = Utility.Random(5) + 4;
                else if (skill.Base < 100.0)
                    toGain = Utility.Random(4) + 3;
                else if (skill.Base < 120.0)
                    toGain = Utility.Random(3) + 2;
                else if (skill.Base < 150.0)
                    toGain = Utility.Random(2) + 1;
                else if (skill.Base < 200.0)
                    toGain = Utility.Random(1) + 1;
                toGain *= RateDefinitions.gainbonus;
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
                if (from.StrLock == StatLockType.Up && ((info.StrGain / 10.0) + RateDefinitions.StatGainBonus) > Utility.RandomDouble())
                {
                    if (info.StrGain != 0)
                        GainStat(from, Stat.Str);
                }
                else if (from.DexLock == StatLockType.Up && ((info.DexGain / 8.0) + RateDefinitions.StatGainBonus) > Utility.RandomDouble())
                {
                    if (info.DexGain != 0)
                        GainStat(from, Stat.Dex);
                }
                else if (from.IntLock == StatLockType.Up && ((info.IntGain / 13.0) + RateDefinitions.StatGainBonus) > Utility.RandomDouble())
                {
                    if (info.IntGain != 0)
                        GainStat(from, Stat.Int);
                }
                //following line used to show chance to gain stats ingame
                //from.SendMessage( "Str: {0} Dex: {1} Int: {2}",((info.StrGain / 33.3) + RateDefinitions.StatGainBonus),((info.DexGain / 33.3) + RateDefinitions.StatGainBonus),((info.IntGain / 33.3) + RateDefinitions.StatGainBonus) );
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

		private static TimeSpan m_StatGainDelay = TimeSpan.FromMinutes( 5.0 );

		public static void GainStat( Mobile from, Stat stat )
		{
			switch( stat )
			{
				case Stat.Str:
				{
					if( (from.LastStrGain + m_StatGainDelay) >= DateTime.Now )
						return;

					from.LastStrGain = DateTime.Now;
					break;
				}
				case Stat.Dex:
				{
					if( (from.LastDexGain + m_StatGainDelay) >= DateTime.Now )
						return;

					from.LastDexGain = DateTime.Now;
					break;
				}
				case Stat.Int:
				{
					if( (from.LastIntGain + m_StatGainDelay) >= DateTime.Now )
						return;

					from.LastIntGain = DateTime.Now;
					break;
				}
			}

			bool atrophy = ( (from.RawStatTotal / (double)from.StatCap) >= Utility.RandomDouble() );

			IncreaseStat( from, stat, atrophy );
		}
	}
}