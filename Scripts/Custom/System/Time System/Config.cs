using System;
using System.Collections.Generic;
using System.Text;
using Server;
using Server.Commands;

namespace Server.TimeSystem
{
    public class Config
    {
        #region Constant Variables

        public const bool ForceScriptSettings = false; // Set to true to have settings configured by script only.  The settings can no longer be configured in-game with exception to setting current date and time.

        #endregion

        #region Set Defaults

        public static void SetDefaults(bool forceUpdate)
        {
            SetDefaultSettings(forceUpdate);

            SetDefaultMonths();
            SetDefaultMoons();
            SetDefaultFacetAdjustments();
            SetDefaultEffects();
            SetDefaultEffectsExclusions();
            SetDefaultSeasons();
        }

        public static void SetDefaultSettings(bool forceUpdate)
        {
            Data.Enabled = true;

            if (!Data.Loading)
            {
                Data.Logging = true;
            }

            Data.TimerSpeed = 5.0;
            Data.MinutesPerTick = 1;

            Data.LightsEngineTimerSpeed = 5.0;

            Data.UpdateInterval = 1000;

            Data.DayLevel = 8;
            Data.NightLevel = 24;
            Data.DarkestHourLevel = 28;
            Data.LightsOnLevel = 10;
            Data.MoonLevelAdjust = 15;

            Data.MinutesPerHour = 60;
            Data.HoursPerDay = 24;

            Data.NightStartHour = 16;
            Data.NightStartMinute = 0;
            Data.DayStartHour = 8;
            Data.DayStartMinute = 0;
            Data.ScaleTimeMinutes = 60;

            if (forceUpdate || !Config.ForceScriptSettings)
            {
                Data.Year = 2007;
                Data.Month = 3;
                Data.Day = 12;
                Data.Hour = 0;
                Data.Minute = 0;
            }

            Data.UseDarkestHour = true;
            Data.DarkestHourMinutesAfterNight = 120;
            Data.DarkestHourScaleTimeMinutes = 30;
            Data.DarkestHourLength = 180;

            Data.UseRealTime = true;

            Data.UseTimeZones = false;
            Data.TimeZoneXDivisor = 16;
            Data.TimeZoneScaleMinutes = 60;

            Data.UseAutoLighting = true;
            Data.UseRandomLightOutage = true;

            Data.UseSeasons = true;

            Data.UseNightSightDarkestHourOverride = true;

            Data.UseNightSightOverride = false;

            Data.UseLightLevelOverride = true;

            Data.UseMurdererDarkestHourBonus = false;

            Data.UseEvilSpawners = true;

            Data.TimeFormat = String.Format("{0} {1}", Data.TimeFormatPreset6, Data.TimeFormatMoonPhase);
            Data.ClockTimeFormat = Data.TimeFormatPreset1;
            Data.SpyglassFormat = Data.SpyglassFormatPreset1;
        }

        public static void SetDefaultMonths()
        {
            Data.MonthsArray = new List<MonthPropsObject>();

            lock (Data.MonthsArray)
            {
                Data.MonthsArray.Add(new MonthPropsObject("January", 31));
                Data.MonthsArray.Add(new MonthPropsObject("February", 28));
                Data.MonthsArray.Add(new MonthPropsObject("March", 31));
                Data.MonthsArray.Add(new MonthPropsObject("April", 30));
                Data.MonthsArray.Add(new MonthPropsObject("May", 31));
                Data.MonthsArray.Add(new MonthPropsObject("June", 30));
                Data.MonthsArray.Add(new MonthPropsObject("July", 31));
                Data.MonthsArray.Add(new MonthPropsObject("August", 31));
                Data.MonthsArray.Add(new MonthPropsObject("September", 30));
                Data.MonthsArray.Add(new MonthPropsObject("October", 31));
                Data.MonthsArray.Add(new MonthPropsObject("November", 30));
                Data.MonthsArray.Add(new MonthPropsObject("December", 31));
            }
        }

        public static void SetDefaultMoons()
        {
            Data.MoonsArray = new List<MoonPropsObject>();

            lock (Data.MoonsArray)
            {
                Data.MoonsArray.Add(new MoonPropsObject("Moon", 28));
            }
        }

        public static void SetDefaultFacetAdjustments()
        {
            Data.FacetArray = new List<FacetPropsObject>();

            lock (Data.FacetArray)
            {
                // Adjustments in minutes from BaseTime.

                Data.FacetArray.Add(new FacetPropsObject(Map.Felucca, 0));
                Data.FacetArray.Add(new FacetPropsObject(Map.Trammel, 0));
                Data.FacetArray.Add(new FacetPropsObject(Map.Ilshenar, 0));
                Data.FacetArray.Add(new FacetPropsObject(Map.Malas, 0));
                Data.FacetArray.Add(new FacetPropsObject(Map.Tokuno, 0));
            }
        }

        public static void SetDefaultEffects()
        {
            
        }

        public static void SetDefaultEffectsExclusions()
        {
        }

        public static void SetDefaultSeasons()
        {
            lock (Data.EffectsMapArray)
            {
                for (int i = 0; i < Data.EffectsMapArray.Count; i++)
                {
                    EffectsMapObject emo = SetDefaultSeasonsValues(Data.EffectsMapArray[i]);

                    Data.EffectsMapArray[i] = emo;
                }
            }
        }

        public static EffectsMapObject SetDefaultEffectsValues(EffectsMapObject emo)
        {
            emo.Priority = 0;

            emo.Enabled = true;

            emo.UseLatitude = true;
            emo.OuterLatitudePercent = 0.10;
            emo.InnerLatitudePercent = 0.10;

            emo.UseDarkestHour = true;

            emo.UseAutoLighting = true;
            emo.UseRandomLightOutage = true;
            emo.LightOutageChancePerTick = 10;

            emo.UseNightSightDarkestHourOverride = true;
            emo.NightSightDarkestHourReduction = 100;

            emo.UseNightSightOverride = false;
            emo.NightSightLevelReduction = 0;

            emo.UseLightLevelOverride = false;
            emo.LightLevelOverrideAdjust = 0;

            emo.UseMurdererDarkestHourBonus = false;
            emo.MurdererDarkestHourLevelBonus = 0;

            emo.UseEvilSpawners = true;

            return emo;
        }

        public static EffectsExclusionMapObject SetDefaultEffectsExclusionValues(EffectsExclusionMapObject eemo)
        {
            eemo.Priority = 0;

            eemo.Enabled = true;

            return eemo;
        }

        public static EffectsMapObject SetDefaultSeasonsValues(EffectsMapObject emo)
        {
            emo.UseSeasons = true;

            emo.StaticSeason = Season.None;
            emo.SetSpringDate(3, 21);
            emo.SetSummerDate(6, 21);
            emo.SetFallDate(9, 21);
            emo.SetWinterDate(12, 21);

            return emo;
        }

        #endregion

        #region Set Variables

        public static void SetVariable(Mobile mobile, CommandEventArgs e, bool append)
        {
            if (e.Length == 1)
            {
                Support.SendVariableNames(mobile);
            }
            else
            {
                string variableName = e.GetString(1);
                string value = null;

                if (e.Length > 2)
                {
                    StringBuilder sb = new StringBuilder();

                    for (int i = 2; i < e.Length; i++)
                    {
                        if (i == 2)
                        {
                            sb.Append(e.GetString(i));
                        }
                        else
                        {
                            sb.Append(String.Format(" {0}", e.GetString(i)));
                        }
                    }

                    value = sb.ToString();
                }

                VariableObject vo = SetVariable(variableName, value, true, append);

                mobile.SendMessage(vo.Message);
            }
        }

        public static VariableObject SetVariable(string variableName, string variableValue, bool restart, bool append)
        {
            VariableObject vo = new VariableObject();
            Variable variable = Support.GetVariableFromName(variableName);

            if (Data.ForceScriptSettings)
            {
                bool allowed = true;

                switch (variable)
                {
                    case Variable.Defaults: { break; }
                    case Variable.Year: { break; }
                    case Variable.Month: { break; }
                    case Variable.Day: { break; }
                    case Variable.Hour: { break; }
                    case Variable.Minute: { break; }
                    default: { allowed = false; break; }
                }

                if (!allowed && Support.CheckForceScriptSettings(ref vo, variable.ToString()))
                {
                    return vo;
                }
            }

            if (Data.DataFileInUse)
            {
                vo.Success = false;
                vo.Message = Data.DataFileInUseMessage;

                return vo;
            }

            object o = Support.GetValue(variableValue);

            bool success = false;
            string minValue = null;
            string maxValue = null;
            string message = null;

            switch (variable)
            {
                case Variable.Defaults:
                    {
                        SetDefaults(true);

                        vo.Success = true;
                        vo.Message = "The time system has been reset to its default configuration.";

                        return vo;
                    }
                case Variable.Logging:
                    {
                        if (CheckVariable(o, variable, false, true, typeof(bool), ref success, ref message))
                        {
                            Data.Logging = (bool)o;
                        }

                        break;
                    }
                case Variable.TimerSpeed:
                    {
                        if (o is int)
                        {
                            o = Convert.ToDouble(o);
                        }

                        if (CheckVariable(o, variable, (double)Data.MinTimerValue, (double)Data.MinutesPerHour, typeof(double), ref success, ref message))
                        {
                            Data.TimerSpeed = (double)o;
                        }

                        break;
                    }
                case Variable.MinutesPerTick:
                    {
                        if (CheckVariable(o, variable, 1, Data.MinutesPerHour, typeof(int), ref success, ref message))
                        {
                            Data.MinutesPerTick = (int)o;
                        }

                        break;
                    }
                case Variable.LightsEngineTimerSpeed:
                    {
                        if (o is int)
                        {
                            o = Convert.ToDouble(o);
                        }

                        if (CheckVariable(o, variable, (double)Data.MinTimerValue, (double)Data.MinutesPerHour, typeof(double), ref success, ref message))
                        {
                            Data.LightsEngineTimerSpeed = (double)o;
                        }

                        break;
                    }
                case Variable.UpdateInterval:
                    {
                        if (CheckVariable(o, variable, Data.MinUpdateInterval, Data.MaxUpdateInterval, typeof(int), ref success, ref message))
                        {
                            Data.UpdateInterval = (int)o;
                        }

                        break;
                    }
                case Variable.DayLevel:
                    {
                        if (CheckVariable(o, variable, Data.MinLightLevel, Data.NightLevel - Data.MinLightLevelDifference, typeof(int), ref success, ref message))
                        {
                            Data.DayLevel = (int)o;
                        }

                        break;
                    }
                case Variable.NightLevel:
                    {
                        int highValue = Data.MaxLightLevel;

                        if (Data.UseDarkestHour)
                        {
                            highValue = Data.DarkestHourLevel - Data.MinDarkestHourNightLevelDifference;
                        }

                        if (CheckVariable(o, variable, Data.DayLevel + Data.MinLightLevelDifference, highValue, typeof(int), ref success, ref message))
                        {
                            Data.NightLevel = (int)o;
                        }

                        break;
                    }
                case Variable.DarkestHourLevel:
                    {
                        if (CheckVariable(o, variable, Data.NightLevel + Data.MinDarkestHourNightLevelDifference, Data.MaxLightLevel, typeof(int), ref success, ref message))
                        {
                            Data.DarkestHourLevel = (int)o;
                        }

                        break;
                    }
                case Variable.LightsOnLevel:
                    {
                        if (CheckVariable(o, variable, Data.MinLightLevel, Data.MaxLightLevel, typeof(int), ref success, ref message))
                        {
                            Data.LightsOnLevel = (int)o;
                        }

                        break;
                    }
                case Variable.MoonLevelAdjust:
                    {
                        if (CheckVariable(o, variable, Data.MinLightLevel, Data.NightLevel, typeof(int), ref success, ref message))
                        {
                            Data.MoonLevelAdjust = (int)o;
                        }

                        break;
                    }
                case Variable.MinutesPerHour:
                    {
                        if (CheckVariable(o, variable, Data.MinMinutesPerHour, Data.MaxMinutesPerHour, typeof(int), ref success, ref message))
                        {
                            Data.MinutesPerHour = (int)o;
                        }

                        break;
                    }
                case Variable.HoursPerDay:
                    {
                        if (CheckVariable(o, variable, Data.MinHoursPerDay, Data.MaxHoursPerDay, typeof(int), ref success, ref message))
                        {
                            Data.HoursPerDay = (int)o;

                            if (Data.NightStartHour > Data.HoursPerDay)
                            {
                                Data.NightStartHour = 0;
                            }

                            if (Data.DayStartHour > Data.HoursPerDay)
                            {
                                Data.DayStartHour = 4;
                            }
                        }

                        break;
                    }
                case Variable.NightStartHour:
                    {
                        Type typeExpected = typeof(int);

                        int lowValue = 0;
                        int highValue = Data.HoursPerDay - 1;

                        minValue = Convert.ToString(lowValue);
                        maxValue = Convert.ToString(highValue);

                        if (o is int)
                        {
                            int value = (int)o;

                            if (value >= lowValue && value <= highValue)
                            {
                                bool overNight = false;

                                int beforeDayStartHour = Data.DayStartHour - Data.MinDayNightHoursDifference;
                                int afterDayStartHour = Data.DayStartHour + Data.MinDayNightHoursDifference;

                                if (Data.NightStartMinute < Data.DayStartMinute)
                                {
                                    afterDayStartHour++;
                                }
                                else if (Data.DayStartMinute < Data.NightStartMinute)
                                {
                                    beforeDayStartHour--;
                                }

                                if (beforeDayStartHour < 0)
                                {
                                    overNight = true;

                                    beforeDayStartHour += Data.HoursPerDay;
                                }

                                if (afterDayStartHour >= Data.HoursPerDay)
                                {
                                    overNight = true;

                                    afterDayStartHour -= Data.HoursPerDay;
                                }

                                if (overNight)
                                {
                                    if (beforeDayStartHour > afterDayStartHour)
                                    {
                                        lowValue = afterDayStartHour;
                                        highValue = beforeDayStartHour;
                                    }
                                    else
                                    {
                                        lowValue = beforeDayStartHour;
                                        highValue = afterDayStartHour;
                                    }

                                    if (value >= lowValue && value <= highValue)
                                    {
                                        success = true;

                                        Data.NightStartHour = value;
                                    }
                                    else
                                    {
                                        message = Formatting.ErrorMessageFormatter(variable.ToString(), value, lowValue.ToString(), highValue.ToString());
                                    }
                                }
                                else
                                {
                                    if (value <= beforeDayStartHour || value >= afterDayStartHour)
                                    {
                                        success = true;

                                        Data.NightStartHour = value;
                                    }
                                    else
                                    {
                                        message = Formatting.ErrorMessageFormatter(variable.ToString(), value, "0", beforeDayStartHour.ToString(), afterDayStartHour.ToString(), Data.HoursPerDay.ToString());
                                    }
                                }
                            }
                            else
                            {
                                message = Formatting.ErrorMessageFormatter(variable.ToString(), value, minValue, maxValue);
                            }
                        }
                        else
                        {
                            message = Formatting.ErrorMessageFormatter(variable.ToString(), o, minValue, maxValue, typeExpected);
                        }

                        break;
                    }
                case Variable.NightStartMinute:
                    {
                        int lowValue = 0;
                        int highValue = Data.MinutesPerHour - 1;

                        if (Data.NightHours == Data.MinDayNightHoursDifference)
                        {
                            highValue = Data.DayStartMinute;
                        }
                        else if (Data.DayHours == Data.MinDayNightHoursDifference)
                        {
                            lowValue = Data.DayStartMinute;
                        }

                        int nightMinutesMinusDarkestHour = Data.NightMinutes - (Data.DarkestHourStartMinutes + Data.DarkestHourTotalMinutes);

                        if (highValue > nightMinutesMinusDarkestHour)
                        {
                            highValue = nightMinutesMinusDarkestHour;
                        }

                        if (CheckVariable(o, variable, lowValue, highValue, typeof(int), ref success, ref message))
                        {
                            Data.NightStartMinute = (int)o;
                        }

                        break;
                    }
                case Variable.DayStartHour:
                    {
                        Type typeExpected = typeof(int);

                        int lowValue = 0;
                        int highValue = Data.HoursPerDay - 1;

                        minValue = Convert.ToString(lowValue);
                        maxValue = Convert.ToString(highValue);

                        if (o is int)
                        {
                            int value = (int)o;

                            if (value >= lowValue && value <= highValue)
                            {
                                bool overNight = false;

                                int beforeNightStartHour = Data.NightStartHour - Data.MinDayNightHoursDifference;
                                int afterNightStartHour = Data.NightStartHour + Data.MinDayNightHoursDifference;

                                if (Data.DayStartMinute < Data.NightStartMinute)
                                {
                                    afterNightStartHour++;
                                }
                                else if (Data.NightStartMinute < Data.DayStartMinute)
                                {
                                    beforeNightStartHour--;
                                }

                                if (beforeNightStartHour < 0)
                                {
                                    overNight = true;

                                    beforeNightStartHour += Data.HoursPerDay;
                                }

                                if (afterNightStartHour >= Data.HoursPerDay)
                                {
                                    overNight = true;

                                    afterNightStartHour -= Data.HoursPerDay;
                                }

                                if (overNight)
                                {
                                    if (beforeNightStartHour > afterNightStartHour)
                                    {
                                        lowValue = afterNightStartHour;
                                        highValue = beforeNightStartHour;
                                    }
                                    else
                                    {
                                        lowValue = beforeNightStartHour;
                                        highValue = afterNightStartHour;
                                    }

                                    if (value >= lowValue && value <= highValue)
                                    {
                                        success = true;

                                        Data.DayStartHour = value;
                                    }
                                    else
                                    {
                                        message = Formatting.ErrorMessageFormatter(variable.ToString(), value, lowValue.ToString(), highValue.ToString());
                                    }
                                }
                                else
                                {
                                    if (value <= beforeNightStartHour || value >= afterNightStartHour)
                                    {
                                        success = true;

                                        Data.DayStartHour = value;
                                    }
                                    else
                                    {
                                        message = Formatting.ErrorMessageFormatter(variable.ToString(), value, "0", beforeNightStartHour.ToString(), afterNightStartHour.ToString(), Data.HoursPerDay.ToString());
                                    }
                                }
                            }
                            else
                            {
                                message = Formatting.ErrorMessageFormatter(variable.ToString(), value, minValue, maxValue);
                            }
                        }
                        else
                        {
                            message = Formatting.ErrorMessageFormatter(variable.ToString(), o, minValue, maxValue, typeExpected);
                        }

                        break;
                    }
                case Variable.DayStartMinute:
                    {
                        int lowValue = 0;
                        int highValue = Data.MinutesPerHour - 1;

                        if (Data.NightHours == Data.MinDayNightHoursDifference)
                        {
                            lowValue = Data.NightStartMinute;
                        }
                        else if (Data.DayHours == Data.MinDayNightHoursDifference)
                        {
                            highValue = Data.NightStartMinute;
                        }

                        if (CheckVariable(o, variable, lowValue, highValue, typeof(int), ref success, ref message))
                        {
                            Data.DayStartMinute = (int)o;
                        }

                        break;
                    }
                case Variable.ScaleTimeMinutes:
                    {
                        int highValue = Data.MinutesPerHour * Data.HoursPerDay;

                        if (Data.NightMinutes <= Data.DayMinutes)
                        {
                            highValue = Data.DayMinutes - Data.NightMinutes;
                        }
                        else
                        {
                            highValue = Data.NightMinutes - Data.DayMinutes;
                        }

                        if (CheckVariable(o, variable, 0, highValue, typeof(int), ref success, ref message))
                        {
                            Data.ScaleTimeMinutes = (int)o;
                        }

                        break;
                    }
                case Variable.Minute:
                    {
                        if (CheckVariable(o, variable, 0, Data.MinutesPerHour - 1, typeof(int), ref success, ref message))
                        {
                            Data.Minute = (int)o;
                        }

                        break;
                    }
                case Variable.Hour:
                    {
                        if (CheckVariable(o, variable, 0, Data.HoursPerDay - 1, typeof(int), ref success, ref message))
                        {
                            Data.Hour = (int)o;
                        }

                        break;
                    }
                case Variable.Day:
                    {
                        MonthPropsObject mpo = Data.MonthsArray[Data.Month - 1];

                        if (CheckVariable(o, variable, 1, mpo.TotalDays, typeof(int), ref success, ref message))
                        {
                            Data.Day = (int)o;
                        }

                        break;
                    }
                case Variable.Month:
                    {
                        if (CheckVariable(o, variable, 1, Data.MonthsArray.Count, typeof(int), ref success, ref message))
                        {
                            Data.Month = (int)o;
                        }

                        break;
                    }
                case Variable.Year:
                    {
                        if (CheckVariable(o, variable, 0, int.MaxValue, typeof(int), ref success, ref message))
                        {
                            Data.Year = (int)o;
                        }

                        break;
                    }
                case Variable.UseDarkestHour:
                    {
                        if (CheckVariable(o, variable, false, true, typeof(bool), ref success, ref message))
                        {
                            Data.UseDarkestHour = (bool)o;
                        }

                        break;
                    }
                case Variable.DarkestHourMinutesAfterNight:
                    {
                        if (CheckVariable(o, variable, 0, Data.NightMinutes - Data.DarkestHourTotalMinutes, typeof(int), ref success, ref message))
                        {
                            Data.DarkestHourMinutesAfterNight = (int)o;
                        }

                        break;
                    }
                case Variable.DarkestHourScaleTimeMinutes:
                    {
                        int highValue = Convert.ToInt32((((double)Data.NightMinutes - Data.DarkestHourLength) / 2.0) - Data.DarkestHourMinutesAfterNight);

                        if (CheckVariable(o, variable, 0, highValue, typeof(int), ref success, ref message))
                        {
                            Data.DarkestHourScaleTimeMinutes = (int)o;
                        }

                        break;
                    }
                case Variable.DarkestHourLength:
                    {
                        int highValue = Data.NightMinutes - (Data.ScaleTimeMinutes * 2) - Data.DarkestHourMinutesAfterNight;

                        if (CheckVariable(o, variable, 0, highValue, typeof(int), ref success, ref message))
                        {
                            Data.DarkestHourLength = (int)o;
                        }

                        break;
                    }
                case Variable.UseRealTime:
                    {
                        if (CheckVariable(o, variable, false, true, typeof(bool), ref success, ref message))
                        {
                            Data.UseRealTime = (bool)o;
                        }

                        break;
                    }
                case Variable.UseTimeZones:
                    {
                        if (CheckVariable(o, variable, false, true, typeof(bool), ref success, ref message))
                        {
                            Data.UseTimeZones = (bool)o;
                        }

                        break;
                    }
                case Variable.TimeZoneXDivisor:
                    {
                        if (CheckVariable(o, variable, Data.MinTimeZoneXDivisor, Data.MaxTimeZoneXDivisor, typeof(int), ref success, ref message))
                        {
                            Data.TimeZoneXDivisor = (int)o;
                        }

                        break;
                    }
                case Variable.TimeZoneScaleMinutes:
                    {
                        if (CheckVariable(o, variable, Data.MinTimeZoneScaleMinutes, Data.MaxTimeZoneScaleMinutes, typeof(int), ref success, ref message))
                        {
                            Data.TimeZoneScaleMinutes = (int)o;
                        }

                        break;
                    }
                case Variable.UseAutoLighting:
                    {
                        if (CheckVariable(o, variable, false, true, typeof(bool), ref success, ref message))
                        {
                            Data.UseAutoLighting = (bool)o;
                        }

                        break;
                    }
                case Variable.UseRandomLightOutage:
                    {
                        if (CheckVariable(o, variable, false, true, typeof(bool), ref success, ref message))
                        {
                            Data.UseRandomLightOutage = (bool)o;
                        }

                        break;
                    }
                case Variable.UseSeasons:
                    {
                        if (CheckVariable(o, variable, false, true, typeof(bool), ref success, ref message))
                        {
                            Data.UseSeasons = (bool)o;
                        }

                        break;
                    }
                case Variable.UseNightSightDarkestHourOverride:
                    {
                        if (CheckVariable(o, variable, false, true, typeof(bool), ref success, ref message))
                        {
                            Data.UseNightSightDarkestHourOverride = (bool)o;
                        }

                        break;
                    }
                case Variable.UseNightSightOverride:
                    {
                        if (CheckVariable(o, variable, false, true, typeof(bool), ref success, ref message))
                        {
                            Data.UseNightSightOverride = (bool)o;
                        }

                        break;
                    }
                case Variable.UseLightLevelOverride:
                    {
                        if (CheckVariable(o, variable, false, true, typeof(bool), ref success, ref message))
                        {
                            Data.UseLightLevelOverride = (bool)o;
                        }

                        break;
                    }
                case Variable.UseMurdererDarkestHourBonus:
                    {
                        if (CheckVariable(o, variable, false, true, typeof(bool), ref success, ref message))
                        {
                            Data.UseMurdererDarkestHourBonus = (bool)o;
                        }

                        break;
                    }
                case Variable.UseEvilSpawners:
                    {
                        if (CheckVariable(o, variable, false, true, typeof(bool), ref success, ref message))
                        {
                            bool enabled = (bool)o;

                            if (!enabled && Data.UseEvilSpawners)
                            {
                                EffectsEngine.TurnOffAllEvilSpawners();
                            }

                            Data.UseEvilSpawners = enabled;
                        }

                        break;
                    }
                case Variable.TimeFormat:
                    {
                        Type typeExpected = typeof(string);

                        if (variableValue != null)
                        {
                            switch (variableValue.ToLower())
                            {
                                case "preset1": { variableValue = String.Format("{0} {1}", Data.TimeFormatPreset1, Data.TimeFormatMoonPhase); break; }
                                case "preset2": { variableValue = String.Format("{0} {1}", Data.TimeFormatPreset2, Data.TimeFormatMoonPhase); break; }
                                case "preset3": { variableValue = String.Format("{0} {1}", Data.TimeFormatPreset3, Data.TimeFormatMoonPhase); break; }
                                case "preset4": { variableValue = String.Format("{0} {1}", Data.TimeFormatPreset4, Data.TimeFormatMoonPhase); break; }
                                case "preset5": { variableValue = String.Format("{0} {1}", Data.TimeFormatPreset5, Data.TimeFormatMoonPhase); break; }
                                case "preset6": { variableValue = String.Format("{0} {1}", Data.TimeFormatPreset6, Data.TimeFormatMoonPhase); break; }
                            }

                            variableValue = variableValue.Replace('$', Formatting.CodeChar);

                            if (append)
                            {
                                Data.TimeFormat = String.Format("{0}{1}", Data.TimeFormat, variableValue);
                            }
                            else
                            {
                                Data.TimeFormat = variableValue;
                            }

                            success = true;
                        }
                        else
                        {
                            message = Formatting.ErrorMessageFormatter(variable.ToString().ToString(), o, typeExpected);
                        }

                        break;
                    }
                case Variable.ClockTimeFormat:
                    {
                        Type typeExpected = typeof(string);

                        if (variableValue != null)
                        {
                            switch (variableValue.ToLower())
                            {
                                case "preset1": { variableValue = Data.TimeFormatPreset1; break; }
                                case "preset2": { variableValue = Data.TimeFormatPreset2; break; }
                                case "preset3": { variableValue = Data.TimeFormatPreset3; break; }
                                case "preset4": { variableValue = Data.TimeFormatPreset4; break; }
                                case "preset5": { variableValue = Data.TimeFormatPreset5; break; }
                                case "preset6": { variableValue = Data.TimeFormatPreset6; break; }
                            }

                            variableValue = variableValue.Replace('$', Formatting.CodeChar);

                            if (append)
                            {
                                Data.ClockTimeFormat = String.Format("{0}{1}", Data.ClockTimeFormat, variableValue);
                            }
                            else
                            {
                                Data.ClockTimeFormat = variableValue;
                            }

                            success = true;
                        }
                        else
                        {
                            message = Formatting.ErrorMessageFormatter(variable.ToString().ToString(), o, typeExpected);
                        }

                        break;
                    }
                case Variable.SpyglassFormat:
                    {
                        Type typeExpected = typeof(string);

                        if (variableValue != null)
                        {
                            switch (variableValue.ToLower())
                            {
                                case "preset1": { variableValue = Data.SpyglassFormatPreset1; break; }
                            }

                            variableValue = variableValue.Replace('$', Formatting.CodeChar);

                            if (append)
                            {
                                Data.SpyglassFormat = String.Format("{0}{1}", Data.SpyglassFormat, variableValue);
                            }
                            else
                            {
                                Data.SpyglassFormat = variableValue;
                            }

                            success = true;
                        }
                        else
                        {
                            message = Formatting.ErrorMessageFormatter(variable.ToString().ToString(), o, typeExpected);
                        }

                        break;
                    }
                default:
                    {
                        message = "That variable type does not exist!";

                        break;
                    }
            }

            if (success)
            {
                message = Formatting.VariableMessageFormatter(variable.ToString(), variableValue, append);

                if (restart)
                {
                    Engine.Restart();
                }
            }

            vo.Success = success;
            vo.Message = message;

            return vo;
        }

        #endregion

        #region Get Variables

        public static VariableObject GetVariable(string variableName)
        {
            Variable variable = Support.GetVariableFromName(variableName);

            VariableObject vo = new VariableObject();

            bool success = false;
            string message = null;

            switch (variable)
            {
                case Variable.TimerSpeed:
                    {
                        message = Formatting.VariableMessageFormatter(variable.ToString(), Data.TimerSpeed.ToString());

                        success = true;

                        break;
                    }
                case Variable.MinutesPerTick:
                    {
                        message = Formatting.VariableMessageFormatter(variable.ToString(), Data.MinutesPerTick.ToString());

                        success = true;

                        break;
                    }
                case Variable.LightsEngineTimerSpeed:
                    {
                        message = Formatting.VariableMessageFormatter(variable.ToString(), Data.LightsEngineTimerSpeed.ToString());

                        success = true;

                        break;
                    }
                case Variable.UpdateInterval:
                    {
                        message = Formatting.VariableMessageFormatter(variable.ToString(), Data.UpdateInterval.ToString());

                        success = true;

                        break;
                    }
                case Variable.DayLevel:
                    {
                        message = Formatting.VariableMessageFormatter(variable.ToString(), Data.DayLevel.ToString());

                        success = true;

                        break;
                    }
                case Variable.NightLevel:
                    {
                        message = Formatting.VariableMessageFormatter(variable.ToString(), Data.NightLevel.ToString());

                        success = true;

                        break;
                    }
                case Variable.DarkestHourLevel:
                    {
                        message = Formatting.VariableMessageFormatter(variable.ToString(), Data.DarkestHourLevel.ToString());

                        success = true;

                        break;
                    }
                case Variable.LightsOnLevel:
                    {
                        message = Formatting.VariableMessageFormatter(variable.ToString(), Data.LightsOnLevel.ToString());

                        success = true;

                        break;
                    }
                case Variable.MoonLevelAdjust:
                    {
                        message = Formatting.VariableMessageFormatter(variable.ToString(), Data.MoonLevelAdjust.ToString());

                        success = true;

                        break;
                    }
                case Variable.MinutesPerHour:
                    {
                        message = Formatting.VariableMessageFormatter(variable.ToString(), Data.MinutesPerHour.ToString());

                        success = true;

                        break;
                    }
                case Variable.HoursPerDay:
                    {
                        message = Formatting.VariableMessageFormatter(variable.ToString(), Data.HoursPerDay.ToString());

                        success = true;

                        break;
                    }
                case Variable.NightStartHour:
                    {
                        message = Formatting.VariableMessageFormatter(variable.ToString(), Data.NightStartHour.ToString());

                        success = true;

                        break;
                    }
                case Variable.NightStartMinute:
                    {
                        message = Formatting.VariableMessageFormatter(variable.ToString(), Data.NightStartMinute.ToString());

                        success = true;

                        break;
                    }
                case Variable.DayStartHour:
                    {
                        message = Formatting.VariableMessageFormatter(variable.ToString(), Data.DayStartHour.ToString());

                        success = true;

                        break;
                    }
                case Variable.DayStartMinute:
                    {
                        message = Formatting.VariableMessageFormatter(variable.ToString(), Data.DayStartMinute.ToString());

                        success = true;

                        break;
                    }
                case Variable.ScaleTimeMinutes:
                    {
                        message = Formatting.VariableMessageFormatter(variable.ToString(), Data.ScaleTimeMinutes.ToString());

                        success = true;

                        break;
                    }
                case Variable.Minute:
                    {
                        message = Formatting.VariableMessageFormatter(variable.ToString(), Data.Minute.ToString());

                        success = true;

                        break;
                    }
                case Variable.Hour:
                    {
                        message = Formatting.VariableMessageFormatter(variable.ToString(), Data.Hour.ToString());

                        success = true;

                        break;
                    }
                case Variable.Day:
                    {
                        message = Formatting.VariableMessageFormatter(variable.ToString(), Data.Day.ToString());

                        success = true;

                        break;
                    }
                case Variable.Month:
                    {
                        message = Formatting.VariableMessageFormatter(variable.ToString(), Data.Month.ToString());

                        success = true;

                        break;
                    }
                case Variable.Year:
                    {
                        message = Formatting.VariableMessageFormatter(variable.ToString(), Data.Year.ToString());

                        success = true;

                        break;
                    }
                case Variable.UseDarkestHour:
                    {
                        message = Formatting.VariableMessageFormatter(variable.ToString(), Data.UseDarkestHour.ToString());

                        success = true;

                        break;
                    }
                case Variable.DarkestHourMinutesAfterNight:
                    {
                        message = Formatting.VariableMessageFormatter(variable.ToString(), Data.DarkestHourMinutesAfterNight.ToString());

                        success = true;

                        break;
                    }
                case Variable.DarkestHourScaleTimeMinutes:
                    {
                        message = Formatting.VariableMessageFormatter(variable.ToString(), Data.DarkestHourScaleTimeMinutes.ToString());

                        success = true;

                        break;
                    }
                case Variable.DarkestHourLength:
                    {
                        message = Formatting.VariableMessageFormatter(variable.ToString(), Data.DarkestHourLength.ToString());

                        success = true;

                        break;
                    }
                case Variable.UseRealTime:
                    {
                        message = Formatting.VariableMessageFormatter(variable.ToString(), Data.UseRealTime.ToString());

                        success = true;

                        break;
                    }
                case Variable.UseTimeZones:
                    {
                        message = Formatting.VariableMessageFormatter(variable.ToString(), Data.UseTimeZones.ToString());

                        success = true;

                        break;
                    }
                case Variable.TimeZoneXDivisor:
                    {
                        message = Formatting.VariableMessageFormatter(variable.ToString(), Data.TimeZoneXDivisor.ToString());

                        success = true;

                        break;
                    }
                case Variable.TimeZoneScaleMinutes:
                    {
                        message = Formatting.VariableMessageFormatter(variable.ToString(), Data.TimeZoneScaleMinutes.ToString());

                        success = true;

                        break;
                    }
                case Variable.UseAutoLighting:
                    {
                        message = Formatting.VariableMessageFormatter(variable.ToString(), Data.UseAutoLighting.ToString());

                        success = true;

                        break;
                    }
                case Variable.UseRandomLightOutage:
                    {
                        message = Formatting.VariableMessageFormatter(variable.ToString(), Data.UseRandomLightOutage.ToString());

                        success = true;

                        break;
                    }
                case Variable.UseSeasons:
                    {
                        message = Formatting.VariableMessageFormatter(variable.ToString(), Data.UseSeasons.ToString());

                        success = true;

                        break;
                    }
                case Variable.UseNightSightDarkestHourOverride:
                    {
                        message = Formatting.VariableMessageFormatter(variable.ToString(), Data.UseNightSightDarkestHourOverride.ToString());

                        success = true;

                        break;
                    }
                case Variable.UseNightSightOverride:
                    {
                        message = Formatting.VariableMessageFormatter(variable.ToString(), Data.UseNightSightOverride.ToString());

                        success = true;

                        break;
                    }
                case Variable.UseLightLevelOverride:
                    {
                        message = Formatting.VariableMessageFormatter(variable.ToString(), Data.UseLightLevelOverride.ToString());

                        success = true;

                        break;
                    }
                case Variable.UseMurdererDarkestHourBonus:
                    {
                        message = Formatting.VariableMessageFormatter(variable.ToString(), Data.UseMurdererDarkestHourBonus.ToString());

                        success = true;

                        break;
                    }
                case Variable.UseEvilSpawners:
                    {
                        message = Formatting.VariableMessageFormatter(variable.ToString(), Data.UseEvilSpawners.ToString());

                        success = true;

                        break;
                    }
                case Variable.TimeFormat:
                    {
                        message = Formatting.VariableMessageFormatter(variable.ToString(), Data.TimeFormat);

                        success = true;

                        break;
                    }
                case Variable.ClockTimeFormat:
                    {
                        message = Formatting.VariableMessageFormatter(variable.ToString(), Data.ClockTimeFormat);

                        success = true;

                        break;
                    }
                case Variable.SpyglassFormat:
                    {
                        message = Formatting.VariableMessageFormatter(variable.ToString(), Data.SpyglassFormat);

                        success = true;

                        break;
                    }
                default:
                    {
                        message = "That variable type does not exist!";

                        break;
                    }
            }

            vo.Success = success;
            vo.Message = message;

            return vo;
        }

        #endregion

        #region Check Methods

        public static bool CheckVariable(object o, object var, object lowValue, object highValue, Type typeExpected, ref bool success, ref string message)
        {
            bool wrongType = true;

            success = false;

            if (o != null && o.GetType().Name == typeExpected.Name)
            {
                wrongType = false;

                if (o is int && (int)o >= (int)lowValue && (int)o <= (int)highValue)
                {
                    success = true;
                }
                else if (o is double && (double)o >= (double)lowValue && (double)o <= (double)highValue)
                {
                    success = true;
                }
                else if (o is bool)
                {
                    success = true;
                }
            }

            string variable = String.Empty;

            if (var is Variable)
            {
                variable = ((Variable)var).ToString();
            }
            else if (var is string)
            {
                variable = (string)var;
            }

            if (wrongType)
            {
                message = Formatting.ErrorMessageFormatter(variable, o, lowValue.ToString(), highValue.ToString(), typeExpected);
            }
            else if (!success)
            {
                message = Formatting.ErrorMessageFormatter(variable, o, lowValue.ToString(), highValue.ToString());
            }

            return success;
        }

        #endregion
    }
}