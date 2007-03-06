using System;
using Server;

namespace Server.TimeSystem
{
    public class EffectsObject
    {
        #region Private Variables

        private EffectsMapObject m_EffectsMap = null;
        private EffectsExclusionMapObject m_EffectsExclusionMap = null;

        private Season m_LateralSeason = Season.None;
        private Season m_Season = Season.None;

        #endregion

        #region Public Variables

        public EffectsMapObject EffectsMap { get { return m_EffectsMap; } set { m_EffectsMap = value; } }
        public EffectsExclusionMapObject EffectsExclusionMap { get { return m_EffectsExclusionMap; } set { m_EffectsExclusionMap = value; } }

        public Season LateralSeason { get { return m_LateralSeason; } set { m_LateralSeason = value; } }
        public Season Season { get { return m_Season; } set { m_Season = value; } }

        #endregion
    }
}