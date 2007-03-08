using System;
using System.Collections;
using Server;

namespace Server.TimeSystem
{
    public class EffectsMapObject : MapObject
    {
        #region Constructor

        public EffectsMapObject(Map map, int x1, int y1, int x2, int y2)
            : base(map, x1, y1, x2, y2)
        {
            m_SpringDate = new DatePropsObject();
            m_SummerDate = new DatePropsObject();
            m_FallDate = new DatePropsObject();
            m_WinterDate = new DatePropsObject();

            m_SpringDate.Season = Season.Spring;
            m_SummerDate.Season = Season.Summer;
            m_FallDate.Season = Season.Fall;
            m_WinterDate.Season = Season.Winter;
        }

        #endregion

        #region Private Variables

        private int m_Index;

        private bool m_UseLatitude;

        private double m_OuterLatitudePercent;
        private double m_InnerLatitudePercent;

        private bool m_UseSeasons;

        private Season m_StaticSeason;

        private DatePropsObject m_SpringDate;
        private DatePropsObject m_SummerDate;
        private DatePropsObject m_FallDate;
        private DatePropsObject m_WinterDate;

        private bool m_UseNightSightDarkestHourOverride;
        private int m_NightSightDarkestHourReduction;

        private bool m_UseNightSightOverride;
        private int m_NightSightLevelReduction;

        private bool m_UseLightLevelOverride;
        private int m_LightLevelOverrideAdjust;

        private bool m_UseMurdererDarkestHourBonus;
        private int m_MurdererDarkestHourLevelBonus;

        #endregion

        #region Public Variables

        public int Index { get { return m_Index; } set { m_Index = value; } }

        public bool UseLatitude { get { return m_UseLatitude; } set { m_UseLatitude = value; } }

        public double OuterLatitudePercent { get { return m_OuterLatitudePercent; } set { m_OuterLatitudePercent = value; } }
        public double InnerLatitudePercent { get { return m_InnerLatitudePercent; } set { m_InnerLatitudePercent = value; } }

        public bool UseSeasons { get { return m_UseSeasons; } set { m_UseSeasons = value; } }

        public Season StaticSeason { get { return m_StaticSeason; } set { m_StaticSeason = value; } }

        public DatePropsObject SpringDate { get { return m_SpringDate; } set { m_SpringDate = value; } }
        public DatePropsObject SummerDate { get { return m_SummerDate; } set { m_SummerDate = value; } }
        public DatePropsObject FallDate { get { return m_FallDate; } set { m_FallDate = value; } }
        public DatePropsObject WinterDate { get { return m_WinterDate; } set { m_WinterDate = value; } }

        public bool UseNightSightDarkestHourOverride { get { return m_UseNightSightDarkestHourOverride; } set { m_UseNightSightDarkestHourOverride = value; } }
        public int NightSightDarkestHourReduction { get { return m_NightSightDarkestHourReduction; } set { m_NightSightDarkestHourReduction = value; } }

        public bool UseNightSightOverride { get { return m_UseNightSightOverride; } set { m_UseNightSightOverride = value; } }
        public int NightSightLevelReduction { get { return m_NightSightLevelReduction; } set { m_NightSightLevelReduction = value; } }

        public bool UseLightLevelOverride { get { return m_UseLightLevelOverride; } set { m_UseLightLevelOverride = value; } }
        public int LightLevelOverrideAdjust { get { return m_LightLevelOverrideAdjust; } set { m_LightLevelOverrideAdjust = value; } }

        public bool UseMurdererDarkestHourBonus { get { return m_UseMurdererDarkestHourBonus; } set { m_UseMurdererDarkestHourBonus = value; } }
        public int MurdererDarkestHourLevelBonus { get { return m_MurdererDarkestHourLevelBonus; } set { m_MurdererDarkestHourLevelBonus = value; } }

        #endregion

        #region Set Methods

        public void SetSpringDate(int month, int day)
        {
            if (IsValidDate(month, day))
            {
                m_SpringDate.Month = month;
                m_SpringDate.Day = day;
            }
        }

        public void SetSummerDate(int month, int day)
        {
            if (IsValidDate(month, day))
            {
                m_SummerDate.Month = month;
                m_SummerDate.Day = day;
            }
        }

        public void SetFallDate(int month, int day)
        {
            if (IsValidDate(month, day))
            {
                m_FallDate.Month = month;
                m_FallDate.Day = day;
            }
        }

        public void SetWinterDate(int month, int day)
        {
            if (IsValidDate(month, day))
            {
                m_WinterDate.Month = month;
                m_WinterDate.Day = day;
            }
        }

        #endregion

        #region Get Methods

        public void GetUpperOuterLatitudeRange(Map map, ref int y1, ref int y2)
        {
            if (!m_UseLatitude || !IsValid() || map != Map)
            {
                y1 = -1;
                y2 = -1;

                return;
            }

            int height = Y2 - Y1;

            int outerLatitudeHeight = (int)(height * m_OuterLatitudePercent);

            y1 = Y1;
            y2 = Y1 + outerLatitudeHeight;
        }

        public void GetLowerOuterLatitudeRange(Map map, ref int y1, ref int y2)
        {
            if (!m_UseLatitude || !IsValid() || map != Map)
            {
                y1 = -1;
                y2 = -1;

                return;
            }

            int height = Y2 - Y1;

            int outerLatitudeHeight = (int)(height * m_OuterLatitudePercent);

            y1 = Y2 - outerLatitudeHeight;
            y2 = Y2;
        }

        public void GetInnerLatitudeRange(Map map, ref int y1, ref int y2)
        {
            if (!m_UseLatitude || !IsValid() || map != Map)
            {
                y1 = -1;
                y2 = -1;

                return;
            }

            int height = Y2 - Y1;

            int innerLatitudeHeight = (int)(height * (m_InnerLatitudePercent / 2));

            int middleLatitude = Y1 + (int)(height / 2);

            y1 = middleLatitude - innerLatitudeHeight;
            y2 = middleLatitude + innerLatitudeHeight;
        }

        public Season GetSeason(Map map, int x, int y)
        {
            if (!Data.UseSeasons || !m_UseSeasons || !IsIn(map, x, y))
            {
                return Season.None;
            }

            if (m_StaticSeason != Season.None)
            {
                return m_StaticSeason;
            }

            if (ContainsInvalidDate())
            {
                return Season.None;
            }

            if (UseLatitude)
            {
                int height = Y2 - Y1;

                int outerLatitudeHeight = (int)(height * OuterLatitudePercent);
                int innerLatitudeHeight = (int)(height * InnerLatitudePercent);
                int middleLatitude = Y1 + (int)(height / 2);

                int upperOuterLowRange = Y1;
                int upperOuterHighRange = Y1 + outerLatitudeHeight;

                int lowerOuterLowRange = Y1 + (height - outerLatitudeHeight);
                int lowerOuterHighRange = Y1 + height;

                int innerLowRange = middleLatitude - innerLatitudeHeight;
                int innerHighRange = middleLatitude + innerLatitudeHeight;

                if ((y >= upperOuterLowRange && y <= upperOuterHighRange) || (y >= lowerOuterLowRange && y <= lowerOuterHighRange))
                {
                    return Season.Winter;
                }
                else if (y >= innerLowRange && y <= innerHighRange)
                {
                    return Season.Summer;
                }
            }

            return GetLateralSeason(map, x, y);
        }

        public Season GetLateralSeason(Map map, int x, int y)
        {
            if (!Data.UseSeasons || !m_UseSeasons || !IsIn(map, x, y) || ContainsInvalidDate())
            {
                return Season.None;
            }

            DatePropsObject[] dpos = new DatePropsObject[]
            {
                m_SpringDate,
                m_SummerDate,
                m_FallDate,
                m_WinterDate
            };

            Season season = Season.None;

            DatePropsObject dateProps = new DatePropsObject();

            for (int i = 0; i < dpos.Length; i++)
            {
                DatePropsObject dpo = dpos[i];

                if ((dpo.Month == dateProps.Month && dpo.Day > dateProps.Day) || dpo.Month > dateProps.Month)
                {
                    dateProps.Month = dpo.Month;
                    dateProps.Day = dpo.Day;

                    season = dpo.Season;

                    if (UseLatitude)
                    {
                        int height = Y2 - Y1;

                        int middleLatitude = Y1 + (int)(height / 2);

                        if (y > middleLatitude)
                        {
                            season = dpo.OppositeSeason();
                        }
                    }
                }
            }

            int month = 0, day = 0;

            TimeEngine.GetTimeMonthDay(map, x, out month, out day);

            int setMonth = 0;

            for (int i = 0; i < dpos.Length; i++)
            {
                DatePropsObject dpo = dpos[i];

                int seasonMonth = dpo.Month;
                int seasonDay = dpo.Day;
                
                if ((month == seasonMonth && day >= seasonDay) || month > seasonMonth)
                {
                    if (setMonth <= seasonMonth)
                    {
                        setMonth = seasonMonth;

                        season = dpo.Season;

                        if (UseLatitude)
                        {
                            int height = Y2 - Y1;

                            int middleLatitude = Y1 + (int)(height / 2);

                            if (y > middleLatitude)
                            {
                                season = dpo.OppositeSeason();
                            }
                        }
                    }
                }
            }

            return season;
        }

        #endregion

        #region Check Methods

        public bool IsInOuterLatitude(Map map, int y)
        {
            if (!m_UseLatitude || !IsValid() || map != Map)
            {
                return false;
            }

            int y1 = 0, y2 = 0;

            GetUpperOuterLatitudeRange(map, ref y1, ref y2);

            if (y >= y1 && y <= y2)
            {
                return true;
            }
            else
            {
                GetLowerOuterLatitudeRange(map, ref y1, ref y2);

                if (y >= y1 && y <= y2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool IsInInnerLatitude(Map map, int y)
        {
            if (!m_UseLatitude || !IsValid() || map != Map)
            {
                return false;
            }

            int y1 = 0, y2 = 0;

            GetInnerLatitudeRange(map, ref y1, ref y2);

            if (y >= y1 && y <= y2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ContainsInvalidDate()
        {
            DatePropsObject[] dpos = new DatePropsObject[]
            {
                m_SpringDate,
                m_SummerDate,
                m_FallDate,
                m_WinterDate
            };

            for (int i = 0; i < dpos.Length; i++)
            {
                DatePropsObject dpo = dpos[i];

                if (dpo != null)
                {
                    if (!IsValidDate(dpo.Month, dpo.Day))
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsValidDate(int month, int day)
        {
            if (Data.MonthsArray.Count > 0 && month > 0 && day > 0 && month <= Data.MonthsArray.Count)
            {
                MonthPropsObject mpo = Data.MonthsArray[month - 1];

                if (mpo.TotalDays > 0 && day <= mpo.TotalDays)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}