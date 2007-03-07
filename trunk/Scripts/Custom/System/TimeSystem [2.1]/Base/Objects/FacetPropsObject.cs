using System;
using Server;

namespace Server.TimeSystem
{
    public class FacetPropsObject
    {
        #region Constructor

        public FacetPropsObject()
        {
        }

        public FacetPropsObject(Map map, int adjustment)
        {
            m_Map = map;

            m_Adjustment = adjustment;
        }

        #endregion

        #region Private Variables

        private Map m_Map;

        private int m_Adjustment;

        #endregion

        #region Public Variables

        public Map Map { get { return m_Map; } set { m_Map = value; } }

        public int Adjustment { get { return m_Adjustment; } set { m_Adjustment = value; } }

        #endregion
    }
}