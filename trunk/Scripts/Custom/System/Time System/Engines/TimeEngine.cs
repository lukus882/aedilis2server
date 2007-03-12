using System;
using Server;
using Server.Items;

namespace Server.TimeSystem
{
    public class TimeEngine
    {
        #region Get Methods

        private static int GetMinutesAfterNight(int minute, int hour)
        {
            if (hour >= Data.NightStartHour)
            {
                return (hour - Data.NightStartHour) * Data.MinutesPerHour + minute - Data.NightStartMinute;
            }
            else
            {
                return ((Data.HoursPerDay - Data.NightStartHour) + hour) * Data.MinutesPerHour + minute - Data.NightStartMinute;
            }
        }

        private static int GetMinutesAfterDay(int minute, int hour)
        {
            if (hour >= Data.DayStartHour)
            {
                return (hour - Data.DayStartHour) * Data.MinutesPerHour + minute - Data.DayStartMinute;
            }
            else
            {
                return ((Data.HoursPerDay - Data.DayStartHour) + hour) * Data.MinutesPerHour + minute - Data.DayStartMinute;
            }
        }

        private static bool IsNightTime(int minute, int hour)
        {
            if (hour == Data.NightStartHour && minute >= Data.NightStartMinute)
            {
                return true;
            }
            else if (hour > Data.NightStartHour || hour < Data.DayStartHour)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsNightTime(object o)
        {
            int x = Support.GetXAxis(o);

            int minute, hour;

            GetTimeMinHour(o, x, out minute, out hour);

            return IsNightTime(minute, hour);
        }

        private static bool IsDarkestHour(int minute, int hour)
        {
            int minutesAfterNight = GetMinutesAfterNight(minute, hour);

            if (minutesAfterNight - Data.DarkestHourStartMinutes >= 0)
            {
                return ((minutesAfterNight - Data.DarkestHourStartMinutes) <= Data.DarkestHourTotalMinutes);
            }
            else
            {
                return false;
            }
        }

        private static bool IsDarkestHour(int minute, int hour, EffectsObject eo)
        {
            if (!Data.UseDarkestHour || eo == null || eo.EffectsMap == null || !eo.EffectsMap.UseDarkestHour)
            {
                return false;
            }

            return IsDarkestHour(minute, hour);
        }

        public static bool IsDarkestHour(object o, EffectsObject eo)
        {
            if (!Data.UseDarkestHour || eo == null || eo.EffectsMap == null || !eo.EffectsMap.UseDarkestHour)
            {
                return false;
            }

            int x = Support.GetXAxis(o);

            int minute, hour;

            GetTimeMinHour(o, x, out minute, out hour);

            return IsDarkestHour(minute, hour);
        }

        public static int GetAdjustments(object o, bool useTimeZoneScaling)
        {
            int x = Support.GetXAxis(o);

            if (x == -1)
            {
                x = 0;
            }

            return GetFacetAdjustment(o) + GetTimeZoneAdjustment(x, useTimeZoneScaling);
        }

        public static int GetAdjustments(object o, int x)
        {
            return GetFacetAdjustment(o) + GetTimeZoneAdjustment(x, Data.UseTimeZones);
        }

        public static int GetFacetAdjustment(object o)
        {
            int index = Support.GetMapIndex(o);

            if (index > -1)
            {
                FacetPropsObject fpo = Data.FacetArray[index];

                return fpo.Adjustment;
            }
            else
            {
                return 0;
            }
        }

        public static int GetTimeZoneAdjustment(int x, bool useTimeZoneScaling)
        {
            if (Data.UseTimeZones)
            {
                if (useTimeZoneScaling)
                {
                    return Support.GetWholeNumber(Support.GetWholeNumber(x, Data.TimeZoneXDivisor, true), Data.TimeZoneScaleMinutes, true) * Data.TimeZoneScaleMinutes;
                }
                else
                {
                    return Support.GetWholeNumber(x, Data.TimeZoneXDivisor, true);
                }
            }
            else
            {
                return 0;
            }
        }

        public static void GetTimeMinHour(object o, int x, out int minute, out int hour)
        {
            minute = Data.Minute + GetAdjustments(o, x);
            hour = Data.Hour;
            int day = Data.Day;
            int month = Data.Month;
            int year = Data.Year;

            CheckTime(ref minute, ref hour, ref day, ref month, ref year, false);
        }

        public static void GetTimeMonthDay(object o, int x, out int month, out int day)
        {
            int minute = Data.Minute + GetAdjustments(o, x);
            int hour = Data.Hour;
            day = Data.Day;
            month = Data.Month;
            int year = Data.Year;

            CheckTime(ref minute, ref hour, ref day, ref month, ref year, false);
        }

        #endregion

        #region Check Methods

        public static void CheckTime(ref int minute, ref int hour, ref int day, ref int month, ref int year, bool checkMoonPhaseDay)
        {
            int totalMonths = Data.MonthsArray.Count;

            MonthPropsObject mpo = Data.MonthsArray[month - 1];

            int totalDays = mpo.TotalDays;

            if (minute >= Data.MinutesPerHour)
            {
                int amountOver = Support.GetWholeNumber(minute, Data.MinutesPerHour, true);
                int leftOver = amountOver * Data.MinutesPerHour;

                minute -= leftOver;
                hour += amountOver;
            }

            if (hour >= Data.HoursPerDay)
            {
                int amountOver = Support.GetWholeNumber(hour, Data.HoursPerDay, true);
                int leftOver = amountOver * Data.HoursPerDay;

                hour -= leftOver;
                day += amountOver;
            }

            while (day > totalDays)
            {
                day -= totalDays;

                month++;

                if (month > totalMonths)
                {
                    month -= totalMonths;

                    year++;
                }

                mpo = Data.MonthsArray[month - 1];

                totalDays = mpo.TotalDays;
            }

            if (checkMoonPhaseDay)
            {
                if (hour == Data.NightStartHour && minute == Data.NightStartMinute)
                {
                    MoonEngine.IncrementMoonDay(1, day);
                }
            }
        }

        #endregion

        #region Calculation Methods

        public static void CalculateBaseTime()
        {
            if (Data.UseRealTime)
            {
                Data.Minute = DateTime.Now.Minute;
                Data.Hour = DateTime.Now.Hour;
                Data.Day = DateTime.Now.Day;
                Data.Month = DateTime.Now.Month;
                Data.Year = DateTime.Now.Year;
            }
            else
            {
                int minute = Data.Minute;
                int hour = Data.Hour;
                int day = Data.Day;
                int month = Data.Month;
                int year = Data.Year;

                minute += Data.MinutesPerTick;

                CheckTime(ref minute, ref hour, ref day, ref month, ref year, true);

                Data.Minute = minute;
                Data.Hour = hour;
                Data.Day = day;
                Data.Month = month;
                Data.Year = year;
            }
        }

        public static int CalculateLightLevel(object o)
        {
            return CalculateLightLevel(o, null);
        }

        public static int CalculateLightLevel(object o, EffectsObject eo)
        {
            if (!Data.Enabled)
            {
                return Data.DayLevel;
            }

            MobileObject mo = null;

            if (o is Mobile)
            {
                mo = Support.GetMobileObject((Mobile)o);

                if (mo != null)
                {
                    if (DateTime.Now - mo.UpdateTimeStamp < TimeSpan.FromMilliseconds(Data.UpdateInterval))
                    {
                        return mo.LightLevel;
                    }

                    mo.IsDarkestHour = false;
                }
                else
                {
                    return Data.DayLevel;
                }
            }

            if (o != null && eo == null)
            {
                eo = EffectsEngine.GetEffects(o, false);
            }

            int currentLevel = Data.BaseLightLevel;

            if (Data.UseLightLevelOverride && eo != null && eo.EffectsMap != null && eo.EffectsMap.UseLightLevelOverride)
            {
                currentLevel = eo.EffectsMap.LightLevelOverrideAdjust;
            }
            else
            {
                int nightLevelAdjust = MoonEngine.GetNightLevelAdjust();

                int x = Support.GetXAxis(o);

                int minute = Data.Minute + GetAdjustments(o, Data.UseTimeZones);
                int hour = Data.Hour;
                int day = Data.Day;
                int month = Data.Month;
                int year = Data.Year;

                if (o != null)
                {
                    CheckTime(ref minute, ref hour, ref day, ref month, ref year, false);
                }

                if (IsNightTime(minute, hour)) // Night time.
                {
                    bool isDarkestHour = IsDarkestHour(minute, hour, eo);

                    if (mo != null)
                    {
                        mo.IsDarkestHour = isDarkestHour;
                    }

                    int minutesAfterNight = GetMinutesAfterNight(minute, hour);

                    if (Data.ScaleTimeMinutes - minutesAfterNight >= 0)
                    {
                        currentLevel = (int)(nightLevelAdjust * ((double)minutesAfterNight / (double)Data.ScaleTimeMinutes));
                    }
                    else if (isDarkestHour)
                    {
                        if (o is BaseLight)
                        {
                            LightsEngine.CalculateLightOutage((BaseLight)o, eo);
                        }

                        int bonus = 0;

                        if (mo != null && eo != null && eo.EffectsMap != null)
                        {
                            if (Data.UseMurdererDarkestHourBonus && eo.EffectsMap.UseMurdererDarkestHourBonus && mo.IsMurderer)
                            {
                                bonus = eo.EffectsMap.MurdererDarkestHourLevelBonus;
                            }
                        }

                        int minutesAfterDarkestHour = minutesAfterNight - Data.DarkestHourStartMinutes;

                        if (minutesAfterDarkestHour <= Data.DarkestHourScaleTimeMinutes)
                        {
                            currentLevel = (int)(nightLevelAdjust + ((Data.DarkestHourLevel - bonus - nightLevelAdjust) * ((double)minutesAfterDarkestHour / (double)Data.DarkestHourScaleTimeMinutes)));
                        }
                        else if (minutesAfterDarkestHour > Data.DarkestHourScaleTimeMinutes && minutesAfterDarkestHour <= (Data.DarkestHourLength + Data.DarkestHourScaleTimeMinutes))
                        {
                            currentLevel = Data.DarkestHourLevel - bonus;
                        }
                        else if (minutesAfterDarkestHour > (Data.DarkestHourLength + Data.DarkestHourScaleTimeMinutes) && minutesAfterDarkestHour <= Data.DarkestHourTotalMinutes)
                        {
                            currentLevel = (int)(nightLevelAdjust + ((Data.DarkestHourLevel - bonus - nightLevelAdjust) * ((double)(Data.DarkestHourTotalMinutes - minutesAfterDarkestHour) / (double)Data.DarkestHourScaleTimeMinutes)));
                        }
                    }
                    else
                    {
                        currentLevel = nightLevelAdjust;
                    }

                    if (o is BaseLight && !isDarkestHour)
                    {
                        LightsEngine.UpdateManagedLight((BaseLight)o, currentLevel);
                    }
                }
                else // Day time.
                {
                    int minutesAfterDay = GetMinutesAfterDay(minute, hour);

                    if (Data.ScaleTimeMinutes - minutesAfterDay >= 0)
                    {
                        currentLevel = (int)((nightLevelAdjust - Data.DayLevel) - ((nightLevelAdjust - Data.DayLevel) * ((double)minutesAfterDay / (double)Data.ScaleTimeMinutes)));
                    }
                    else
                    {
                        currentLevel = Data.DayLevel;
                    }

                    if (o is BaseLight)
                    {
                        LightsEngine.UpdateManagedLight((BaseLight)o, currentLevel);
                    }
                }
            }

            if(currentLevel > Data.MaxLightLevel)
            {
                currentLevel = Data.MaxLightLevel;
            }
            else if(currentLevel < Data.MinLightLevel)
            {
                currentLevel = Data.MinLightLevel;
            }

            if (o == null)
            {
                Data.BaseLightLevel = currentLevel;
            }
            else if (o is Mobile)
            {
                if (mo != null)
                {
                    mo.UpdateTimeStamp = DateTime.Now;

                    mo.LightLevel = currentLevel;

                    EffectsEngine.CheckEffects(mo, eo, true, true);
                }
            }

            return currentLevel;
        }

        #endregion
    }
}