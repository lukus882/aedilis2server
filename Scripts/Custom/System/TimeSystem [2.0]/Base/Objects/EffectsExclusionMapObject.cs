using System;
using System.Collections;
using Server;

namespace Server.TimeSystem
{
    public class EffectsExclusionMapObject : MapObject
    {
        #region Constructor

        public EffectsExclusionMapObject(Map map, int x1, int y1, int x2, int y2)
            : base(map, x1, y1, x2, y2)
        {
        }

        #endregion

        #region Private Variables

        private int m_Index;

        #endregion

        #region Public Variables

        public int Index { get { return m_Index; } set { m_Index = value; } }

        #endregion
    }
}