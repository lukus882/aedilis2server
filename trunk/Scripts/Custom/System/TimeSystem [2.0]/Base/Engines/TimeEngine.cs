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

        public static bool IsNightTime(object o)
        {
            int x = Support.GetXAxis(o);

            int minute, hour;

            GetTimeMinHour(o, x, out minute, out hour);

            return IsNightTime(minute, hour);
        }

        public static bool IsDarkestHour(object o)
        {
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
            return GetFacetAdjustment(o) + GetTimeZoneAdjustment(x, false);
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
                int amountOver = (int)((double)minute / (double)Data.MinutesPerHour);
                int leftOver = amountOver * Data.MinutesPerHour;

                minute -= leftOver;
                hour += amountOver;
            }

            if (hour >= Data.HoursPerDay)
            {
                int amountOver = (int)((double)hour / (double)Data.HoursPerDay);
                int leftOver = amountOver * Data.HoursPerDay;

                hour -= leftOver;
                day += amountOver;
            }

            if (day > totalDays)
            {
                int amountOver = (int)((double)day / (double)totalDays);
                int leftOver = amountOver * totalDays;

                day -= leftOver;
                month += amountOver;
            }

            if (month > totalMonths)
            {
                int amountOver = (int)((double)month / (double)totalMonths);
                int leftOver = amountOver * totalMonths;

                month -= leftOver;
                year += amountOver;
            }

            if (checkMoonPhaseDay)
            {
                if (hour == Data.NightStartHour && minute == Data.NightStartMinute)
                {
                    MoonEngine.IncrementMoonPhaseDay(1, day);
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
            if (LightCycle.LevelOverride > int.MinValue)
            {
                return LightCycle.LevelOverride;
            }
            else if (!Data.Enabled)
            {
                return Data.DayLevel;
            }

            int x = Support.GetXAxis(o);

            int minute = Data.Minute + GetAdjustments(o, false);
            int hour = Data.Hour;
            int day = Data.Day;
            int month = Data.Month;
            int year = Data.Year;

            if (o != null)
            {
                CheckTime(ref minute, ref hour, ref day, ref month, ref year, false);
            }

            MobileObject mo = null;

            if (o is Mobile)
            {
                mo = Support.GetMobileObject((Mobile)o);

                if (DateTime.Now - Data.UpdateTimeStamp < TimeSpan.FromMilliseconds(Data.UpdateInterval))
                {
                    return mo.LightLevel;
                }

                if (mo != null)
                {
                    mo.IsDarkestHour = false;
                }
            }

            int currentLevel = Data.BaseLightLevel;
            int nightLevelAdjust = MoonEngine.GetNightLevelAdjust();

            if (IsNightTime(minute, hour)) // Night time.
            {
                bool isDarkestHour = IsDarkestHour(minute, hour);

                if (o is Mobile && mo != null)
                {
                    mo.IsDarkestHour = isDarkestHour;
                }

                int minutesAfterNight = GetMinutesAfterNight(minute, hour);

                if (Data.ScaleTimeMinutes - minutesAfterNight >= 0)
                {
                    currentLevel = (int)(nightLevelAdjust * ((double)minutesAfterNight / (double)Data.ScaleTimeMinutes));
                }
                else if (Data.UseDarkestHour && isDarkestHour)
                {
                    if (o is BaseLight)
                    {
                        LightsEngine.CalculateLightOutage((BaseLight)o);
                    }

                    int minutesAfterDarkestHour = minutesAfterNight - Data.DarkestHourStartMinutes;

                    if (minutesAfterDarkestHour <= Data.DarkestHourScaleTimeMinutes)
                    {
                        currentLevel = (int)(nightLevelAdjust + ((Data.DarkestHourLevel - nightLevelAdjust) * ((double)minutesAfterDarkestHour / (double)Data.DarkestHourScaleTimeMinutes)));
                    }
                    else if (minutesAfterDarkestHour > Data.DarkestHourScaleTimeMinutes && minutesAfterDarkestHour <= (Data.DarkestHourLength + Data.DarkestHourScaleTimeMinutes))
                    {
                        currentLevel = Data.DarkestHourLevel;
                    }
                    else if (minutesAfterDarkestHour > (Data.DarkestHourLength + Data.DarkestHourScaleTimeMinutes) && minutesAfterDarkestHour <= Data.DarkestHourTotalMinutes)
                    {
                        currentLevel = (int)(nightLevelAdjust + ((Data.DarkestHourLevel - nightLevelAdjust) * ((double)(Data.DarkestHourTotalMinutes - minutesAfterDarkestHour) / (double)Data.DarkestHourScaleTimeMinutes)));
                    }
                }
                else
                {
                    currentLevel = nightLevelAdjust;
                }

                if (!isDarkestHour && Data.UseAutoLighting)
                {
                    if (o is BaseLight)
                    {
                        LightsEngine.UpdateManagedLight((BaseLight)o, currentLevel);
                    }
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

                if (Data.UseAutoLighting)
                {
                    if (o is BaseLight)
                    {
                        LightsEngine.UpdateManagedLight((BaseLight)o, currentLevel);
                    }
                }
            }

            if (o == null)
            {
                Data.BaseLightLevel = currentLevel;
            }
            else if (o is Mobile)
            {
                if (mo != null)
                {
                    Data.UpdateTimeStamp = DateTime.Now;

                    mo.LightLevel = currentLevel;

                    EffectsEngine.CheckEffects(o, true, true);
                }
            }

            return currentLevel;
        }

        #endregion
    }
}