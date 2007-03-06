using System;
using Server;

namespace Server.TimeSystem
{
    public class MobileObject
    {
        #region Private Variables

        private Mobile m_Mobile;

        private int m_LightLevel;

        private Season m_Season;

        private bool m_IsDarkestHour;
        private bool m_IsNightSightOn;
        private int m_OldLightLevel;

        #endregion

        #region Public Variables

        public Mobile Mobile { get { return m_Mobile; } set { m_Mobile = value; } }

        public int LightLevel { get { return m_LightLevel; } set { m_LightLevel = value; } }

        public Season Season { get { return m_Season; } set { m_Season = value; } }

        public bool IsDarkestHour { get { return m_IsDarkestHour; } set { m_IsDarkestHour = value; } }
        public bool IsNightSightOn { get { return m_IsNightSightOn; } set { m_IsNightSightOn = value; } }
        public int OldLightLevel { get { return m_OldLightLevel; } set { m_OldLightLevel = value; } }

        #endregion
    }
}
