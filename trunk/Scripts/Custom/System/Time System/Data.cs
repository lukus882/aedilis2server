using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Server;
using Server.Mobiles;
using Server.Network;
using Server.Items;

namespace Server.TimeSystem
{
    public class Data
    {
        #region Constant Variables

        public const string Version = "2.0.5"; // Current version of the Time System.
        public const int BinaryVersion = 5; // The version number used in the data file.

        public static readonly bool ForceScriptSettings = false; // Set to true to have settings configured by script only.  The settings can no longer be configured in-game.

        public static readonly string DataDirectory = Path.Combine(Core.BaseDirectory, @"Data\Custom\Time System");
        public static readonly string LogDirectory = Path.Combine(Core.BaseDirectory, @"Data\Custom\Time System");

        public const string DataFileName = "Time System.dat";
        public const string LogFileName = "Time System.log";

        public static readonly string DataFile = Path.Combine(DataDirectory, DataFileName);
        public static readonly string LogFile = Path.Combine(DataDirectory, LogFileName);

        //public const char ParseChar = ' '; // Default is a null char.

        public const int MinLightLevel = 0; // Minimum light level.  Lower = brighter.  UO only supports 0-30.
        public const int MaxLightLevel = 30; // Maximum light level.  Higher = darker.

        public const int MinLightLevelDifference = 8; // Minimum light level difference allowed between day and night.
        public const int MinDarkestHourNightLevelDifference = 4; // Minimum light level different between night and darkest hour.

        public const int MinDayNightHoursDifference = 4; // Minimum hours difference allowed between starting day hour and starting night hour.

        public const double MinTimerValue = 0.5; // Minimum seconds allowed per timer tick.

        public const int MinUpdateInterval = 100; // Minimum milliseconds allowed for update interval lag reduction.
        public const int MaxUpdateInterval = 60000; // Maximum milliseconds allowed for update interval lag reduction.

        public const int MinTimeZoneXDivisor = 16; // Minimum X per m_MinutesPerTick for Time Zones.
        public const int MaxTimeZoneXDivisor = 512; // Maximum X per m_MinutesPerTick for Time Zones.

        public const int MinTimeZoneScaleMinutes = 15; // Minimum minutes allowed for time zone differences. (i.e. 60 minutes is our traditional time zone difference)
        public const int MaxTimeZoneScaleMinutes = 360; // Maximum minutes allowed for time zone differences.

        public const int MinMinutesPerHour = 10; // Minimum minutes allowed per hour.
        public const int MaxMinutesPerHour = 100; // Maximum minutes allowed per hour.

        public const int MinHoursPerDay = 10; // Minimum hours allowed per day.
        public const int MaxHoursPerDay = 100; // Maximum hours allowed per day.

        public static readonly int NumberOfFacets = Map.GetMapNames().Length - 1; // Get total number of facets minus internal map.

        public const int StreamSendLimit = 6; // When displaying command and variables, only display this many per output to screen at a time.

        public const string DataFileInUseMessage = "The Time System is currently processing it's data file!  Command cancelled... please retry in a few seconds!";

        public static readonly Type[] ItemLightTypes = // BaseLight item types that will be toggled on/off for day/night.
        {
            typeof(LampPost1), typeof(LampPost2), typeof(LampPost3),
            typeof(TSLampPost1), typeof(TSLampPost2), typeof(TSLampPost3)
        };

        public const string TimeFormatMoonPhase = "The moon is $mp-1$.";
        public const string TimeFormatPreset1 = "The time is $hr-ap$:$mn-2$ $ap$ on day $da$ of month $mo$ of year $yr$.";
        public const string TimeFormatPreset2 = "$mo$/$da$/$yr$ $hr-ap$:$mn-2$ $ap$.";
        public const string TimeFormatPreset3 = "$da$/$mo$/$yr$ $hr-ap$:$mn-2$ $ap$.";
        public const string TimeFormatPreset4 = "$mo-0$ $da$, $yr$ $hr-ap$:$mn-2$ $ap$.";
        public const string TimeFormatPreset5 = "$mo-3$ $da$, $yr$ $hr-ap$:$mn-2$ $ap$.";
        public const string TimeFormatPreset6 = "It is $hr-ap$:$mn-2$ $ap$ on the $da$$nth-d$ of $mo-0$, $yr$.";
        public const string SpyglassFormatPreset1 = "You peer into the sky and see the moon is $mp-1$.";

        #endregion

        #region Private Variables

        // Do NOT set variable values in this declaration.  This is merely for declaration only and will NOT be the
        // default values for these variables.  To set the default values, you must edit the Config.cs and make
        // your changes to the Set Variables region.

        private static bool m_Enabled;
        private static bool m_Logging = true;

        private static double m_TimerSpeed; // How many seconds between ticks on master timer.
        private static int m_MinutesPerTick; // Minutes added per tick of timer.

        private static double m_LightsEngineTimerSpeed; // How many seconds between ticks on lights engine timer.

        private static int m_UpdateInterval; // How many milliseconds are allowed between updates.  Reduces lag.

        private static int m_DayLevel;
        private static int m_NightLevel;
        private static int m_DarkestHourLevel;
        private static int m_LightsOnLevel;
        private static int m_MoonLevelAdjust;

        private static int m_MinutesPerHour;
        private static int m_HoursPerDay;

        private static int m_NightStartHour;
        private static int m_NightStartMinute;
        private static int m_DayStartHour;
        private static int m_DayStartMinute;
        private static int m_ScaleTimeMinutes; // Minutes it takes to scale from day/night or night/day

        private static int m_Minute;
        private static int m_Hour;
        private static int m_Day;
        private static int m_Month;
        private static int m_Year;

        private static bool m_UseDarkestHour;
        private static int m_DarkestHourMinutesAfterNight; // How many minutes after nightfall until the darkest hour
        private static int m_DarkestHourScaleTimeMinutes; // Minutes it takes to scale from night/darkest hour or darkest hour/night
        private static int m_DarkestHourLength; // How many minutes will darkest hour last

        private static bool m_UseRealTime;

        private static bool m_UseTimeZones;
        private static int m_TimeZoneXDivisor;
        private static int m_TimeZoneScaleMinutes;

        private static bool m_UseAutoLighting; // If true, specific lampposts will turn on/off when light level >= m_LightsOnLevel.
        private static bool m_UseRandomLightOutage; // If true, random lights turn on/off during the darkest hour.

        private static bool m_UseSeasons;

        private static bool m_UseNightSightDarkestHourOverride;

        private static bool m_UseNightSightOverride;

        private static bool m_UseLightLevelOverride;

        private static bool m_UseMurdererDarkestHourBonus;

        private static bool m_UseEvilSpawners;

        private static string m_TimeFormat;
        private static string m_ClockTimeFormat;
        private static string m_SpyglassFormat;

        private static List<MonthPropsObject> m_MonthsArray;
        private static List<MoonPropsObject> m_MoonsArray;
        private static List<FacetPropsObject> m_FacetArray;
        private static List<EffectsMapObject> m_EffectsMapArray;
        private static List<EffectsExclusionMapObject> m_EffectsExclusionMapArray;

        // Unsaved Variables

        private static bool m_Loading;
        private static bool m_DataFileInUse;

        private static StreamWriter m_LogWriter;

        private static int m_BaseLightLevel;

        private static Dictionary<Mobile, MobileObject> m_MobilesTable = new Dictionary<Mobile, MobileObject>();

        private static List<BaseLight> m_LightsList;
        private static List<EvilSpawner> m_EvilSpawnersList = new List<EvilSpawner>();

        #endregion

        #region Public Variables

        public static bool Enabled { get { return m_Enabled; } set { m_Enabled = value; } }
        public static bool Logging
        {
            get { return m_Logging; }
            set
            {
                if (!m_Logging && value)
                {
                    Support.OpenLogFile();
                }
                else if (m_Logging && !value)
                {
                    Support.ConsoleWriteLine("Time System: Logging is disabled.");
                }

                m_Logging = value;
            }
        }

        public static double TimerSpeed { get { return m_TimerSpeed; } set { m_TimerSpeed = value; } }
        public static int MinutesPerTick { get { return m_MinutesPerTick; } set { m_MinutesPerTick = value; } }

        public static double LightsEngineTimerSpeed { get { return m_LightsEngineTimerSpeed; } set { m_LightsEngineTimerSpeed = value; } }

        public static int UpdateInterval { get { return m_UpdateInterval; } set { m_UpdateInterval = value; } }

        public static int DayLevel { get { return m_DayLevel; } set { m_DayLevel = value; } }
        public static int NightLevel { get { return m_NightLevel; } set { m_NightLevel = value; } }
        public static int DarkestHourLevel { get { return m_DarkestHourLevel; } set { m_DarkestHourLevel = value; } }
        public static int LightsOnLevel { get { return m_LightsOnLevel; } set { m_LightsOnLevel = value; } }
        public static int MoonLevelAdjust { get { return m_MoonLevelAdjust; } set { m_MoonLevelAdjust = value; } }

        public static int MinutesPerHour { get { return m_MinutesPerHour; } set { m_MinutesPerHour = value; } }
        public static int HoursPerDay { get { return m_HoursPerDay; } set { m_HoursPerDay = value; } }

        public static int NightStartHour { get { return m_NightStartHour; } set { m_NightStartHour = value; } }
        public static int NightStartMinute { get { return m_NightStartMinute; } set { m_NightStartMinute = value; } }
        public static int DayStartHour { get { return m_DayStartHour; } set { m_DayStartHour = value; } }
        public static int DayStartMinute { get { return m_DayStartMinute; } set { m_DayStartMinute = value; } }
        public static int ScaleTimeMinutes { get { return m_ScaleTimeMinutes; } set { m_ScaleTimeMinutes = value; } }

        public static int Minute { get { return m_Minute; } set { m_Minute = value; } }
        public static int Hour { get { return m_Hour; } set { m_Hour = value; } }
        public static int Day { get { return m_Day; } set { m_Day = value; } }
        public static int Month { get { return m_Month; } set { m_Month = value; } }
        public static int Year { get { return m_Year; } set { m_Year = value; } }

        public static bool UseDarkestHour { get { return m_UseDarkestHour; } set { m_UseDarkestHour = value; } }
        public static int DarkestHourMinutesAfterNight { get { return m_DarkestHourMinutesAfterNight; } set { m_DarkestHourMinutesAfterNight = value; } }
        public static int DarkestHourScaleTimeMinutes { get { return m_DarkestHourScaleTimeMinutes; } set { m_DarkestHourScaleTimeMinutes = value; } }
        public static int DarkestHourLength { get { return m_DarkestHourLength; } set { m_DarkestHourLength = value; } }

        public static bool UseRealTime { get { return m_UseRealTime; } set { m_UseRealTime = value; } }

        public static bool UseTimeZones { get { return m_UseTimeZones; } set { m_UseTimeZones = value; } }
        public static int TimeZoneXDivisor { get { return m_TimeZoneXDivisor; } set { m_TimeZoneXDivisor = value; } }
        public static int TimeZoneScaleMinutes { get { return m_TimeZoneScaleMinutes; } set { m_TimeZoneScaleMinutes = value; } }

        public static bool UseAutoLighting { get { return m_UseAutoLighting; } set { m_UseAutoLighting = value; } }
        public static bool UseRandomLightOutage { get { return m_UseRandomLightOutage; } set { m_UseRandomLightOutage = value; } }

        public static bool UseSeasons { get { return m_UseSeasons; } set { m_UseSeasons = value; } }

        public static bool UseNightSightDarkestHourOverride { get { return m_UseNightSightDarkestHourOverride; } set { m_UseNightSightDarkestHourOverride = value; } }

        public static bool UseNightSightOverride { get { return m_UseNightSightOverride; } set { m_UseNightSightOverride = value; } }

        public static bool UseLightLevelOverride { get { return m_UseLightLevelOverride; } set { m_UseLightLevelOverride = value; } }

        public static bool UseMurdererDarkestHourBonus { get { return m_UseMurdererDarkestHourBonus; } set { m_UseMurdererDarkestHourBonus = value; } }

        public static bool UseEvilSpawners { get { return m_UseEvilSpawners; } set { m_UseEvilSpawners = value; } }

        public static string TimeFormat { get { return m_TimeFormat; } set { m_TimeFormat = value; } }
        public static string ClockTimeFormat { get { return m_ClockTimeFormat; } set { m_ClockTimeFormat = value; } }
        public static string SpyglassFormat { get { return m_SpyglassFormat; } set { m_SpyglassFormat = value; } }

        public static List<MonthPropsObject> MonthsArray { get { return m_MonthsArray; } set { m_MonthsArray = value; } }
        public static List<MoonPropsObject> MoonsArray { get { return m_MoonsArray; } set { m_MoonsArray = value; } }
        public static List<FacetPropsObject> FacetArray { get { return m_FacetArray; } set { m_FacetArray = value; } }
        public static List<EffectsMapObject> EffectsMapArray { get { return m_EffectsMapArray; } set { m_EffectsMapArray = value; } }
        public static List<EffectsExclusionMapObject> EffectsExclusionMapArray { get { return m_EffectsExclusionMapArray; } set { m_EffectsExclusionMapArray = value; } }

        // Unsaved Variables

        public static bool Loading { get { return m_Loading; } set { m_Loading = value; } }
        public static bool DataFileInUse { get { return m_DataFileInUse; } set { m_DataFileInUse = value; } }

        public static StreamWriter LogWriter { get { return m_LogWriter; } set { m_LogWriter = value; } }

        public static int BaseLightLevel { get { return m_BaseLightLevel; } set { m_BaseLightLevel = value; } }

        public static Dictionary<Mobile, MobileObject> MobilesTable { get { return m_MobilesTable; } set { m_MobilesTable = value; } }

        public static List<BaseLight> LightsList { get { return m_LightsList; } set { m_LightsList = value; } }
        public static List<EvilSpawner> EvilSpawnersList { get { return m_EvilSpawnersList; } set { m_EvilSpawnersList = value; } }

        #endregion

        #region Calculated Variables

        public static int NightHours
        {
            get
            {
                if (m_NightStartHour > m_DayStartHour)
                {
                    return (m_HoursPerDay - m_NightStartHour) + m_DayStartHour;
                }
                else
                {
                    return m_DayStartHour - m_NightStartHour;
                }
            }
        }

        public static int NightMinutes
        {
            get
            {
                if (m_NightStartHour > m_DayStartHour)
                {
                    return ((m_HoursPerDay - m_NightStartHour) + m_DayStartHour) * m_MinutesPerHour - m_NightStartMinute + m_DayStartMinute;
                }
                else
                {
                    return ((m_DayStartHour - m_NightStartHour) * m_MinutesPerHour) - m_DayStartMinute + m_NightStartMinute;
                }
            }
        }

        public static int DayHours
        {
            get
            {
                if (m_DayStartHour > m_NightStartHour)
                {
                    return (m_HoursPerDay - m_DayStartHour) + m_NightStartHour;
                }
                else
                {
                    return m_NightStartHour - m_DayStartHour;
                }
            }
        }

        public static int DayMinutes
        {
            get
            {
                if (m_DayStartHour > m_NightStartHour)
                {
                    return ((m_HoursPerDay - m_DayStartHour) + m_NightStartHour) * m_MinutesPerHour - m_DayStartMinute + m_NightStartMinute;
                }
                else
                {
                    return ((m_NightStartHour - m_DayStartHour) * m_MinutesPerHour) - m_NightStartMinute + m_DayStartMinute;
                }
            }
        }

        public static int DarkestHourStartMinutes
        {
            get { return m_ScaleTimeMinutes + m_DarkestHourMinutesAfterNight; }
        }

        public static int DarkestHourTotalMinutes
        {
            get { return (m_DarkestHourLength + (m_DarkestHourScaleTimeMinutes * 2)); }
        }

        #endregion

        #region Loading

        public static bool Load()
        {
            if (!Support.CheckDataPath())
            {
                Support.ConsoleWriteLine(String.Format("Time System: \"{0}\" not found!  Creating a new file using the current settings.", DataFileName));

                Config.SetDefaults(true);

                Engine.Restart();

                Save();

                return false;
            }
            else
            {
                using (BinaryReader reader = new BinaryReader(File.Open(DataFile, FileMode.Open)))
                {
                    try
                    {
                        m_DataFileInUse = true;

                        Support.WipeAllArrays();

                        Config.SetDefaultSettings(true);

                        int version = reader.ReadInt32();

                        if (version < 3)
                        {
                            return LegacySupport.Load(reader, version);
                        }

                        switch (version)
                        {
                            case 5:
                                {
                                    m_LightsEngineTimerSpeed = reader.ReadDouble();

                                    m_UseEvilSpawners = reader.ReadBoolean();

                                    goto case 4;
                                }
                            case 4:
                                {
                                    m_Logging = reader.ReadBoolean();

                                    m_UseLightLevelOverride = reader.ReadBoolean();

                                    m_UseMurdererDarkestHourBonus = reader.ReadBoolean();

                                    goto case 3;
                                }
                            case 3:
                                {
                                    m_Enabled = reader.ReadBoolean();

                                    m_TimerSpeed = reader.ReadDouble();
                                    m_MinutesPerTick = reader.ReadInt32();

                                    m_UpdateInterval = reader.ReadInt32();

                                    m_DayLevel = reader.ReadInt32();
                                    m_NightLevel = reader.ReadInt32();
                                    m_DarkestHourLevel = reader.ReadInt32();
                                    m_LightsOnLevel = reader.ReadInt32();
                                    m_MoonLevelAdjust = reader.ReadInt32();

                                    m_MinutesPerHour = reader.ReadInt32();
                                    m_HoursPerDay = reader.ReadInt32();

                                    m_NightStartHour = reader.ReadInt32();
                                    m_NightStartMinute = reader.ReadInt32();
                                    m_DayStartHour = reader.ReadInt32();
                                    m_DayStartMinute = reader.ReadInt32();
                                    m_ScaleTimeMinutes = reader.ReadInt32();

                                    m_Minute = reader.ReadInt32();
                                    m_Hour = reader.ReadInt32();
                                    m_Day = reader.ReadInt32();
                                    m_Month = reader.ReadInt32();
                                    m_Year = reader.ReadInt32();

                                    m_UseDarkestHour = reader.ReadBoolean();
                                    m_DarkestHourMinutesAfterNight = reader.ReadInt32();
                                    m_DarkestHourScaleTimeMinutes = reader.ReadInt32();
                                    m_DarkestHourLength = reader.ReadInt32();

                                    m_UseRealTime = reader.ReadBoolean();

                                    m_UseTimeZones = reader.ReadBoolean();
                                    m_TimeZoneXDivisor = reader.ReadInt32();
                                    m_TimeZoneScaleMinutes = reader.ReadInt32();

                                    m_UseAutoLighting = reader.ReadBoolean();
                                    m_UseRandomLightOutage = reader.ReadBoolean();

                                    if (version < 5)
                                    {
                                        reader.ReadInt32();
                                    }

                                    m_UseSeasons = reader.ReadBoolean();

                                    m_UseNightSightDarkestHourOverride = reader.ReadBoolean();

                                    m_UseNightSightOverride = reader.ReadBoolean();

                                    m_TimeFormat = reader.ReadString();
                                    m_ClockTimeFormat = reader.ReadString();
                                    m_SpyglassFormat = reader.ReadString();

                                    // Custom Months

                                    lock (m_MonthsArray)
                                    {
                                        int recordCount = reader.ReadInt32();

                                        for (int i = 0; i < recordCount; i++)
                                        {
                                            MonthPropsObject mpo = new MonthPropsObject();

                                            mpo.Name = reader.ReadString();
                                            mpo.TotalDays = reader.ReadInt32();

                                            m_MonthsArray.Add(mpo);
                                        }
                                    }

                                    // Custom Moons

                                    lock (m_MoonsArray)
                                    {
                                        int recordCount = reader.ReadInt32();

                                        for (int i = 0; i < recordCount; i++)
                                        {
                                            MoonPropsObject mpo = new MoonPropsObject();

                                            mpo.Name = reader.ReadString();

                                            mpo.TotalDays = reader.ReadInt32();
                                            mpo.CurrentDay = reader.ReadInt32();

                                            mpo.LastUpdateDay = reader.ReadInt32();

                                            m_MoonsArray.Add(mpo);
                                        }
                                    }

                                    // Facet Adjustments

                                    lock (m_FacetArray)
                                    {
                                        int recordCount = reader.ReadInt32();

                                        for (int i = 0; i < recordCount; i++)
                                        {
                                            FacetPropsObject fpo = new FacetPropsObject();

                                            fpo.Map = Support.GetMapFromName(reader.ReadString(), false);

                                            fpo.Adjustment = reader.ReadInt32();

                                            Data.FacetArray.Add(fpo);
                                        }
                                    }

                                    // Effects Maps

                                    lock (m_EffectsMapArray)
                                    {
                                        int recordCount = reader.ReadInt32();

                                        for (int i = 0; i < recordCount; i++)
                                        {
                                            int priority = reader.ReadInt32();

                                            Map map = Support.GetMapFromName(reader.ReadString(), false);

                                            int x1 = reader.ReadInt32();
                                            int y1 = reader.ReadInt32();
                                            int x2 = reader.ReadInt32();
                                            int y2 = reader.ReadInt32();

                                            EffectsMapObject emo = Config.SetDefaultEffectsValues(new EffectsMapObject(map, x1, y1, x2, y2));

                                            emo.Priority = priority;

                                            if (version > 4)
                                            {
                                                emo.Index = reader.ReadInt32();

                                                emo.Enabled = reader.ReadBoolean();
                                            }

                                            emo.UseLatitude = reader.ReadBoolean();

                                            emo.OuterLatitudePercent = reader.ReadDouble();
                                            emo.InnerLatitudePercent = reader.ReadDouble();

                                            if (version < 5)
                                            {
                                                emo.Index = reader.ReadInt32();
                                            }

                                            emo.UseSeasons = reader.ReadBoolean();

                                            emo.StaticSeason = (Season)reader.ReadInt32();

                                            emo.SpringDate.Month = reader.ReadInt32();
                                            emo.SpringDate.Day = reader.ReadInt32();

                                            emo.SummerDate.Month = reader.ReadInt32();
                                            emo.SummerDate.Day = reader.ReadInt32();

                                            emo.FallDate.Month = reader.ReadInt32();
                                            emo.FallDate.Day = reader.ReadInt32();

                                            emo.WinterDate.Month = reader.ReadInt32();
                                            emo.WinterDate.Day = reader.ReadInt32();

                                            if (version > 4)
                                            {
                                                emo.UseDarkestHour = reader.ReadBoolean();

                                                emo.UseAutoLighting = reader.ReadBoolean();
                                                emo.UseRandomLightOutage = reader.ReadBoolean();
                                                emo.LightOutageChancePerTick = reader.ReadInt32();
                                            }

                                            emo.UseNightSightDarkestHourOverride = reader.ReadBoolean();

                                            if (version > 3)
                                            {
                                                emo.NightSightDarkestHourReduction = reader.ReadInt32();
                                            }

                                            emo.UseNightSightOverride = reader.ReadBoolean();
                                            emo.NightSightLevelReduction = reader.ReadInt32();

                                            if (version > 3)
                                            {
                                                emo.UseLightLevelOverride = reader.ReadBoolean();
                                                emo.LightLevelOverrideAdjust = reader.ReadInt32();

                                                emo.UseMurdererDarkestHourBonus = reader.ReadBoolean();
                                                emo.MurdererDarkestHourLevelBonus = reader.ReadInt32();
                                            }

                                            if (version > 4)
                                            {
                                                emo.UseEvilSpawners = reader.ReadBoolean();
                                            }

                                            Data.EffectsMapArray.Add(emo);
                                        }
                                    }

                                    // Effects Exclusion Maps

                                    lock (m_EffectsExclusionMapArray)
                                    {
                                        int recordCount = reader.ReadInt32();

                                        for (int i = 0; i < recordCount; i++)
                                        {
                                            int priority = reader.ReadInt32();

                                            Map map = Support.GetMapFromName(reader.ReadString(), false);

                                            int x1 = reader.ReadInt32();
                                            int y1 = reader.ReadInt32();
                                            int x2 = reader.ReadInt32();
                                            int y2 = reader.ReadInt32();

                                            EffectsExclusionMapObject eemo = Config.SetDefaultEffectsExclusionValues(new EffectsExclusionMapObject(map, x1, y1, x2, y2));

                                            eemo.Priority = priority;

                                            if (version > 4)
                                            {
                                                eemo.Index = reader.ReadInt32();

                                                eemo.Enabled = reader.ReadBoolean();
                                            }

                                            if (version < 4)
                                            {
                                                reader.ReadBoolean();

                                                reader.ReadDouble();
                                                reader.ReadDouble();
                                            }

                                            if (version < 5)
                                            {
                                                eemo.Index = reader.ReadInt32();
                                            }

                                            Data.EffectsExclusionMapArray.Add(eemo);
                                        }
                                    }

                                    break;
                                }
                        }

                        reader.Close();

                        Support.ConsoleWriteLine("Time System: Loading complete.");

                        m_DataFileInUse = false;

                        Support.ReIndexArray(EffectsMapArray);
                        Support.ReIndexArray(EffectsExclusionMapArray);

                        return true;
                    }
                    catch (EndOfStreamException e)
                    {
                        m_DataFileInUse = false;

                        reader.Close();

                        Support.ConsoleWriteLine(String.Format("Time System: \"{0}\" is corrupt!  Creating a new file using the current settings.\r\n\r\nException:\r\n\r\n{1}\r\n", DataFileName, e.ToString()));

                        Config.SetDefaults(true);

                        Engine.Restart();

                        Save();

                        string goAwayWarning = e.ToString(); // Stop compile warning.

                        return false;
                    }
                    catch (Exception e)
                    {
                        m_DataFileInUse = false;

                        reader.Close();

                        Support.ConsoleWriteLine(String.Format("Time System: Unable to load data from file \"{0}\"!  Creating a new file using the current settings.\r\n\r\nException:\r\n\r\n{1}\r\n", DataFileName, e.ToString()));

                        Config.SetDefaults(true);

                        Engine.Restart();

                        Save();

                        return false;
                    }
                }
            }
        }

        #endregion

        #region Saving

        public static bool Save()
        {
            Support.CheckDataPath();

            using (BinaryWriter writer = new BinaryWriter(File.Open(DataFile, FileMode.Create)))
            {
                try
                {
                    m_DataFileInUse = true;

                    writer.Write(BinaryVersion);

                    writer.Write(m_LightsEngineTimerSpeed);

                    writer.Write(m_UseEvilSpawners);

                    writer.Write(m_Logging);

                    writer.Write(m_UseLightLevelOverride);

                    writer.Write(m_UseMurdererDarkestHourBonus);

                    writer.Write(m_Enabled);

                    writer.Write(m_TimerSpeed);
                    writer.Write(m_MinutesPerTick);

                    writer.Write(m_UpdateInterval);

                    writer.Write(m_DayLevel);
                    writer.Write(m_NightLevel);
                    writer.Write(m_DarkestHourLevel);
                    writer.Write(m_LightsOnLevel);
                    writer.Write(m_MoonLevelAdjust);

                    writer.Write(m_MinutesPerHour);
                    writer.Write(m_HoursPerDay);

                    writer.Write(m_NightStartHour);
                    writer.Write(m_NightStartMinute);
                    writer.Write(m_DayStartHour);
                    writer.Write(m_DayStartMinute);
                    writer.Write(m_ScaleTimeMinutes);

                    writer.Write(m_Minute);
                    writer.Write(m_Hour);
                    writer.Write(m_Day);
                    writer.Write(m_Month);
                    writer.Write(m_Year);

                    writer.Write(m_UseDarkestHour);
                    writer.Write(m_DarkestHourMinutesAfterNight);
                    writer.Write(m_DarkestHourScaleTimeMinutes);
                    writer.Write(m_DarkestHourLength);

                    writer.Write(m_UseRealTime);

                    writer.Write(m_UseTimeZones);
                    writer.Write(m_TimeZoneXDivisor);
                    writer.Write(m_TimeZoneScaleMinutes);

                    writer.Write(m_UseAutoLighting);
                    writer.Write(m_UseRandomLightOutage);

                    writer.Write(m_UseSeasons);

                    writer.Write(m_UseNightSightDarkestHourOverride);

                    writer.Write(m_UseNightSightOverride);

                    writer.Write(m_TimeFormat);
                    writer.Write(m_ClockTimeFormat);
                    writer.Write(m_SpyglassFormat);

                    // Custom Months

                    lock (m_MonthsArray)
                    {
                        writer.Write(m_MonthsArray.Count);

                        for (int i = 0; i < m_MonthsArray.Count; i++)
                        {
                            writer.Write(m_MonthsArray[i].Name);
                            writer.Write(m_MonthsArray[i].TotalDays);
                        }
                    }

                    // Custom Moons

                    lock (m_MoonsArray)
                    {
                        writer.Write(m_MoonsArray.Count);

                        for (int i = 0; i < m_MoonsArray.Count; i++)
                        {
                            writer.Write(m_MoonsArray[i].Name);

                            writer.Write(m_MoonsArray[i].TotalDays);
                            writer.Write(m_MoonsArray[i].CurrentDay);

                            writer.Write(m_MoonsArray[i].LastUpdateDay);
                        }
                    }

                    // Facet Adjustment

                    lock (m_FacetArray)
                    {
                        writer.Write(m_FacetArray.Count);

                        for (int i = 0; i < m_FacetArray.Count; i++)
                        {
                            writer.Write(m_FacetArray[i].Map.Name);

                            writer.Write(m_FacetArray[i].Adjustment);
                        }
                    }

                    // Effects Maps

                    lock (m_EffectsMapArray)
                    {
                        writer.Write(m_EffectsMapArray.Count);

                        for (int i = 0; i < m_EffectsMapArray.Count; i++)
                        {
                            writer.Write(m_EffectsMapArray[i].Priority);

                            writer.Write(m_EffectsMapArray[i].Map.Name);

                            writer.Write(m_EffectsMapArray[i].X1);
                            writer.Write(m_EffectsMapArray[i].Y1);
                            writer.Write(m_EffectsMapArray[i].X2);
                            writer.Write(m_EffectsMapArray[i].Y2);

                            writer.Write(m_EffectsMapArray[i].Index);

                            writer.Write(m_EffectsMapArray[i].Enabled);

                            writer.Write(m_EffectsMapArray[i].UseLatitude);

                            writer.Write(m_EffectsMapArray[i].OuterLatitudePercent);
                            writer.Write(m_EffectsMapArray[i].InnerLatitudePercent);

                            writer.Write(m_EffectsMapArray[i].UseSeasons);

                            writer.Write((int)m_EffectsMapArray[i].StaticSeason);

                            writer.Write(m_EffectsMapArray[i].SpringDate.Month);
                            writer.Write(m_EffectsMapArray[i].SpringDate.Day);

                            writer.Write(m_EffectsMapArray[i].SummerDate.Month);
                            writer.Write(m_EffectsMapArray[i].SummerDate.Day);

                            writer.Write(m_EffectsMapArray[i].FallDate.Month);
                            writer.Write(m_EffectsMapArray[i].FallDate.Day);

                            writer.Write(m_EffectsMapArray[i].WinterDate.Month);
                            writer.Write(m_EffectsMapArray[i].WinterDate.Day);

                            writer.Write(m_EffectsMapArray[i].UseDarkestHour);

                            writer.Write(m_EffectsMapArray[i].UseAutoLighting);
                            writer.Write(m_EffectsMapArray[i].UseRandomLightOutage);
                            writer.Write(m_EffectsMapArray[i].LightOutageChancePerTick);

                            writer.Write(m_EffectsMapArray[i].UseNightSightDarkestHourOverride);
                            writer.Write(m_EffectsMapArray[i].NightSightDarkestHourReduction);

                            writer.Write(m_EffectsMapArray[i].UseNightSightOverride);
                            writer.Write(m_EffectsMapArray[i].NightSightLevelReduction);

                            writer.Write(m_EffectsMapArray[i].UseLightLevelOverride);
                            writer.Write(m_EffectsMapArray[i].LightLevelOverrideAdjust);

                            writer.Write(m_EffectsMapArray[i].UseMurdererDarkestHourBonus);
                            writer.Write(m_EffectsMapArray[i].MurdererDarkestHourLevelBonus);

                            writer.Write(m_EffectsMapArray[i].UseEvilSpawners);
                        }
                    }

                    // Effects Exclusion Maps

                    lock (m_EffectsExclusionMapArray)
                    {
                        writer.Write(m_EffectsExclusionMapArray.Count);

                        for (int i = 0; i < m_EffectsExclusionMapArray.Count; i++)
                        {
                            writer.Write(m_EffectsExclusionMapArray[i].Priority);

                            writer.Write(m_EffectsExclusionMapArray[i].Map.Name);

                            writer.Write(m_EffectsExclusionMapArray[i].X1);
                            writer.Write(m_EffectsExclusionMapArray[i].Y1);
                            writer.Write(m_EffectsExclusionMapArray[i].X2);
                            writer.Write(m_EffectsExclusionMapArray[i].Y2);

                            writer.Write(m_EffectsExclusionMapArray[i].Index);

                            writer.Write(m_EffectsExclusionMapArray[i].Enabled);
                        }
                    }

                    writer.Flush();
                    writer.Close();

                    Support.ConsoleWriteLine("Time System: Saving complete.");

                    m_DataFileInUse = false;

                    return true;
                }
                catch (Exception e)
                {
                    m_DataFileInUse = false;

                    writer.Close();

                    Support.ConsoleWriteLine(String.Format("Time System: Unable to save data to file \"{0}\"!\r\n{1}", DataFileName, e.ToString()));

                    return false;
                }
            }
        }

        #endregion
    }
}