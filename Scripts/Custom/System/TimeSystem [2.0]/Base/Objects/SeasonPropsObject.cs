using System;
using Server;

namespace Server.TimeSystem
{
    public class SeasonPropsObject
    {
        #region Constructor

        public SeasonPropsObject()
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

        private Season m_StaticSeason;

        private DatePropsObject m_SpringDate;
        private DatePropsObject m_SummerDate;
        private DatePropsObject m_FallDate;
        private DatePropsObject m_WinterDate;

        #endregion

        #region Public Variables

        public Season StaticSeason { get { return m_StaticSeason; } set { m_StaticSeason = value; } }

        public DatePropsObject SpringDate { get { return m_SpringDate; } set { m_SpringDate = value; } }
        public DatePropsObject SummerDate { get { return m_SummerDate; } set { m_SummerDate = value; } }
        public DatePropsObject FallDate { get { return m_FallDate; } set { m_FallDate = value; } }
        public DatePropsObject WinterDate { get { return m_WinterDate; } set { m_WinterDate = value; } }

        #endregion
    }
}