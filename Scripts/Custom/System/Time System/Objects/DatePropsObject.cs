using System;
using Server;

namespace Server.TimeSystem
{
    public class DatePropsObject
    {
        #region Private Variables

        private Season m_Season;

        private int m_Month;
        private int m_Day;

        #endregion

        #region Public Variables

        public Season Season { get { return m_Season; } set { m_Season = value; } }

        public int Month { get { return m_Month; } set { m_Month = value; } }
        public int Day { get { return m_Day; } set { m_Day = value; } }

        #endregion

        #region Get Methods

        public Season OppositeSeason()
        {
            switch (m_Season)
            {
                case Season.Spring: { return Season.Fall; }
                case Season.Summer: { return Season.Winter; }
                case Season.Fall: { return Season.Spring; }
                case Season.Winter: { return Season.Summer; }
            }

            return Season.None;
        }

        #endregion
    }
}