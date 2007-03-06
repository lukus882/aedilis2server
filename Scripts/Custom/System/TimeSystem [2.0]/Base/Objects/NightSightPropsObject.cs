using System;
using Server;

namespace Server.TimeSystem
{
    public class NightSightPropsObject
    {
        #region Private Variables

        private bool m_UseNightSightDarkestHourOverride;
        private bool m_UseNightSightOverride;
        private int m_NightSightLevelReduction;

        #endregion

        #region Public Variables

        public bool UseNightSightDarkestHourOverride { get { return m_UseNightSightDarkestHourOverride; } set { m_UseNightSightDarkestHourOverride = value; } }
        public bool UseNightSightOverride { get { return m_UseNightSightOverride; } set { m_UseNightSightOverride = value; } }
        public int NightSightLevelReduction { get { return m_NightSightLevelReduction; } set { m_NightSightLevelReduction = value; } }

        #endregion
    }
}