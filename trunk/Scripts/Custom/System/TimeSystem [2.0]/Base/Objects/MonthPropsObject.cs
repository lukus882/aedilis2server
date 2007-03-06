using System;
using Server;

namespace Server.TimeSystem
{
    public class MonthPropsObject
    {
        #region Constructor

        public MonthPropsObject()
        {
        }

        public MonthPropsObject(string name, int totalDays)
        {
            m_Name = name;

            m_TotalDays = totalDays;
        }

        #endregion

        #region Private Variables

        private string m_Name;

        private int m_TotalDays;

        #endregion

        #region Public Variables

        public string Name { get { return m_Name; } set { m_Name = value; } }

        public int TotalDays { get { return m_TotalDays; } set { m_TotalDays = value; } }

        #endregion
    }
}