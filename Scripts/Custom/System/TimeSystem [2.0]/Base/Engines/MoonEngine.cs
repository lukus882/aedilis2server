using System;
using System.Text;
using Server;

namespace Server.TimeSystem
{
    public class MoonEngine
    {
        #region Get Methods

        public static int GetNightLevelAdjust()
        {
            int currentDay = 0, totalDays = 0;

            if (Data.MoonsArray.Count > 0)
            {
                int totalMoonPhases = Enum.GetNames(typeof(MoonPhase)).Length;

                int difference = int.MaxValue;

                int newCurrentDay = 0, newTotalDays = 0;

                for (int i = 0; i < Data.MoonsArray.Count; i++)
                {
                    MoonPropsObject mpo = Data.MoonsArray[i];

                    newCurrentDay = mpo.CurrentDay;
                    newTotalDays = mpo.TotalDays;

                    MoonPhase mp = GetMoonPhase(newCurrentDay, newTotalDays);

                    int moonPhaseIndex = (int)mp;
                    int moonPhaseDivisor = Convert.ToInt32(((double)totalMoonPhases / 2.0));

                    int moonPhaseDifference = 0;

                    if (moonPhaseIndex <= moonPhaseDivisor)
                    {
                        moonPhaseDifference = moonPhaseDivisor - moonPhaseIndex;
                    }
                    else
                    {
                        moonPhaseDifference = moonPhaseIndex - moonPhaseDivisor;
                    }

                    if (moonPhaseDifference < difference)
                    {
                        difference = moonPhaseDifference;

                        currentDay = newCurrentDay;
                        totalDays = newTotalDays;
                    }
                }
            }
            else
            {
                return Data.NightLevel;
            }

            double moonPhaseMultiplier = ((double)currentDay - 1.0) / ((double)totalDays / 2.0);

            if (moonPhaseMultiplier > 1.0)
            {
                moonPhaseMultiplier = 2.0 - moonPhaseMultiplier;
            }

            return Data.NightLevel - Convert.ToInt32((double)Data.MoonLevelAdjust * moonPhaseMultiplier);
        }

        public static MoonPhase GetMoonPhase(int currentDay, int totalDays)
        {
            int totalMoonPhases = Enum.GetValues(typeof(MoonPhase)).Length;

            double moonPhaseFraction = 100.0 / (double)totalMoonPhases;
            double currentDaysFraction = ((double)currentDay / (double)totalDays) * 100.0;

            int moonPhaseIndex = Convert.ToInt32((currentDaysFraction / moonPhaseFraction) - 1.0);

            if (moonPhaseIndex < 0)
            {
                moonPhaseIndex = 0;
            }
            else if (moonPhaseIndex > totalMoonPhases - 1)
            {
                moonPhaseIndex = totalMoonPhases - 1;
            }

            return (MoonPhase)moonPhaseIndex;
        }

        public static string GetMoonPhaseName(int index)
        {
            int currentDay = 0, totalDays = 0;

            if (Data.MoonsArray.Count != 0)
            {
                if (index < Data.MoonsArray.Count && index >= 0)
                {
                    MoonPropsObject mpo = Data.MoonsArray[index];

                    currentDay = mpo.CurrentDay;
                    totalDays = mpo.TotalDays;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

            string moonPhase = GetMoonPhase(currentDay, totalDays).ToString();

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < moonPhase.Length; i++)
            {
                char c = moonPhase[i];

                if (Char.IsUpper(c))
                {
                    if (i == 0)
                    {
                        sb.Append(Char.ToLower(c));
                    }
                    else
                    {
                        sb.Append(String.Format(" {0}", Char.ToLower(c)));
                    }
                }
                else
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        public static string GetMoonName(int index)
        {
            if (Data.MoonsArray.Count != 0)
            {
                if (index < Data.MoonsArray.Count && index >= 0)
                {
                    MoonPropsObject mpo = Data.MoonsArray[index];

                    return mpo.Name;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region Update Methods

        public static void IncrementMoonPhaseDay(int value, int day)
        {
            if (Data.MoonsArray.Count != 0)
            {
                lock (Data.MoonsArray)
                {
                    for (int i = 0; i < Data.MoonsArray.Count; i++)
                    {
                        MoonPropsObject mpo = Data.MoonsArray[i];

                        int currentDay = mpo.CurrentDay;
                        int totalDays = mpo.TotalDays;
                        int lastUpdateDay = mpo.LastUpdateDay;

                        if (day != lastUpdateDay)
                        {
                            currentDay += value;

                            if (currentDay > totalDays)
                            {
                                int amountOver = (int)((double)currentDay / (double)totalDays);
                                int leftOver = amountOver * totalDays;

                                currentDay -= leftOver;
                            }

                            mpo.LastUpdateDay = day;

                            Data.MoonsArray[i] = mpo;
                        }
                    }
                }
            }
        }

        #endregion
    }
}