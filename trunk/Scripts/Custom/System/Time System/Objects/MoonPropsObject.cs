using System;
using Server;

namespace Server.TimeSystem
{
    public class MoonPropsObject
    {
        #region Constructor

        public MoonPropsObject()
        {
        }

        public MoonPropsObject(string name, int totalDays)
        {
            m_Name = name;

            m_TotalDays = totalDays;
            m_CurrentDay = 0;

            m_LastUpdateDay = 0;
        }

        public MoonPropsObject(string name, int totalDays, int currentDay)
        {
            m_Name = name;

            m_TotalDays = totalDays;
            m_CurrentDay = currentDay;

            m_LastUpdateDay = 0;
        }

        #endregion

        #region Private Variables

        private string m_Name;

        private int m_TotalDays;
        private int m_CurrentDay;

        private int m_LastUpdateDay;

        #endregion

        #region Public Variables

        public string Name { get { return m_Name; } set { m_Name = value; } }

        public int TotalDays { get { return m_TotalDays; } set { m_TotalDays = value; } }
        public int CurrentDay { get { return m_CurrentDay; } set { m_CurrentDay = value; } }

        public int LastUpdateDay { get { return m_LastUpdateDay; } set { m_LastUpdateDay = value; } }

        #endregion
    }
}