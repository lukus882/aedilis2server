using System;
using Server;

namespace Server.TimeSystem
{
    #region Enums

    public enum Variable
    {
        None,
        Defaults,

        Logging,
        TimerSpeed,
        LightsEngineTimerSpeed,
        MinutesPerTick,
        UpdateInterval,
        DayLevel,
        NightLevel,
        DarkestHourLevel,
        LightsOnLevel,
        MoonLevelAdjust,
        MinutesPerHour,
        HoursPerDay,
        NightStartHour,
        NightStartMinute,
        DayStartHour,
        DayStartMinute,
        ScaleTimeMinutes,
        Minute,
        Hour,
        Day,
        Month,
        Year,
        UseDarkestHour,
        DarkestHourMinutesAfterNight,
        DarkestHourScaleTimeMinutes,
        DarkestHourLength,
        UseRealTime,
        UseTimeZones,
        TimeZoneXDivisor,
        TimeZoneScaleMinutes,
        UseAutoLighting,
        UseRandomLightOutage,
        UseSeasons,
        UseNightSightDarkestHourOverride,
        UseNightSightOverride,
        UseLightLevelOverride,
        UseMurdererDarkestHourBonus,
        UseEvilSpawners,
        TimeFormat,
        ClockTimeFormat,
        SpyglassFormat
    };

    public enum MoonPhase
    {
        New,
        WaxingCrescent,
        WaxingQuarter,
        WaxingThird,
        WaxingHalf,
        WaxingTwoThirds,
        WaxingThreeQuarters,
        WaxingGibbous,
        Full,
        WaningGibbous,
        WaningThreeQuarters,
        WaningTwoThirds,
        WaningHalf,
        WaningThird,
        WaningQuarter,
        WaningCrescent
    };

    public enum Command
    {
        None,

        Set,
        Get,
        Append,
        RepopLightsList,
        Stop,
        Start,
        Restart,
        Load,
        Save,
        SetTime,
        Query,
        Version,
        ConvertLampPosts,
        AddMonth,
        InsertMonth,
        SetMonth,
        GetMonth,
        RemoveMonth,
        SetMonthProps,
        ClearMonths,
        AddMoon,
        InsertMoon,
        SetMoon,
        GetMoon,
        RemoveMoon,
        SetMoonProps,
        ClearMoons,
        SetFacetAdjust,
        GetFacetAdjust,
        AddEmo,
        SetEmo,
        GetEmo,
        RemoveEmo,
        ToggleEmo,
        AddEemo,
        SetEemo,
        GetEemo,
        RemoveEemo,
        ToggleEemo
    }

    public enum CommandType
    {
        Add,
        Insert,
        Set
    };

    public enum Format
    {
        Time,
        Clock,
        Spyglass
    };

    public enum Season
    {
        None,

        Spring,
        Summer,
        Fall,
        Winter
    };

    public enum EffectsMapType
    {
        None,

        Priority,
        Map,
        X1Y1,
        X2Y2,
        UseLatitude,
        OuterInnerLatitude,
        UseSeasons,
        StaticSeason,
        SpringDate,
        SummerDate,
        FallDate,
        WinterDate,
        UseDarkestHour,
        UseAutoLighting,
        UseRandomLightOutage,
        LightOutageChancePerTick,
        UseNightSightDarkestHourOverride,
        NightSightDarkestHourReduction,
        UseNightSightOverride,
        NightSightLevelReduction,
        UseLightLevelOverride,
        LightLevelOverrideAdjust,
        UseMurdererDarkestHourBonus,
        MurdererDarkestHourLevelBonus,
        UseEvilSpawners
    };

    public enum EffectsExclusionMapType
    {
        None,

        Priority,
        Map,
        X1Y1,
        X2Y2
    };

    #endregion
}