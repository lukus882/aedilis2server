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
        }

        #endregion

        #region Private Variables

        private int m_Index;

        private bool m_UseSeasons;

        private SeasonPropsObject m_SeasonProps = new SeasonPropsObject();
        private NightSightPropsObject m_NightSightProps = new NightSightPropsObject();

        #endregion

        #region Public Variables

        public int Index { get { return m_Index; } set { m_Index = value; } }

        public bool UseSeasons { get { return m_UseSeasons; } set { m_UseSeasons = value; } }

        public SeasonPropsObject SeasonProps { get { return m_SeasonProps; } set { m_SeasonProps = value; } }
        public NightSightPropsObject NightSightProps { get { return m_NightSightProps; } set { m_NightSightProps = value; } }

        #endregion

        #region Set Methods

        public void SetSpringDate(int month, int day)
        {
            if (IsValidDate(month, day))
            {
                m_SeasonProps.SpringDate.Month = month;
                m_SeasonProps.SpringDate.Day = day;
            }
        }

        public void SetSummerDate(int month, int day)
        {
            if (IsValidDate(month, day))
            {
                m_SeasonProps.SummerDate.Month = month;
                m_SeasonProps.SummerDate.Day = day;
            }
        }

        public void SetFallDate(int month, int day)
        {
            if (IsValidDate(month, day))
            {
                m_SeasonProps.FallDate.Month = month;
                m_SeasonProps.FallDate.Day = day;
            }
        }

        public void SetWinterDate(int month, int day)
        {
            if (IsValidDate(month, day))
            {
                m_SeasonProps.WinterDate.Month = month;
                m_SeasonProps.WinterDate.Day = day;
            }
        }

        #endregion

        #region Get Methods

        public Season GetSeason(Map map, int x, int y)
        {
            if (!Data.UseSeasons || !m_UseSeasons || !IsIn(map, x, y))
            {
                return Season.None;
            }

            if (m_SeasonProps.StaticSeason != Season.None)
            {
                return m_SeasonProps.StaticSeason;
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
                m_SeasonProps.SpringDate,
                m_SeasonProps.SummerDate,
                m_SeasonProps.FallDate,
                m_SeasonProps.WinterDate
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

        public bool ContainsInvalidDate()
        {
            DatePropsObject[] dpos = new DatePropsObject[]
            {
                m_SeasonProps.SpringDate,
                m_SeasonProps.SummerDate,
                m_SeasonProps.FallDate,
                m_SeasonProps.WinterDate
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