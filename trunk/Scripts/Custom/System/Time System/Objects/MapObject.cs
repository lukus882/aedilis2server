using System;
using Server;

namespace Server.TimeSystem
{
    public class MapObject
    {
        #region Constructor

        public MapObject(Map map, int x1, int y1, int x2, int y2)
        {
            m_Priority = 0;

            m_Map = map;

            m_X1 = x1;
            m_Y1 = y1;
            m_X2 = x2;
            m_Y2 = y2;
        }

        #endregion

        #region Private Variables

        private int m_Priority;

        private Map m_Map;

        private int m_X1;
        private int m_Y1;
        private int m_X2;
        private int m_Y2;

        #endregion

        #region Public Variables

        public int Priority { get { return m_Priority; } set { m_Priority = value; } }

        public Map Map { get { return m_Map; } set { m_Map = value; } }

        public int X1 { get { return m_X1; } set { m_X1 = value; } }
        public int Y1 { get { return m_Y1; } set { m_Y1 = value; } }
        public int X2 { get { return m_X2; } set { m_X2 = value; } }
        public int Y2 { get { return m_Y2; } set { m_Y2 = value; } }

        #endregion

        #region Check Methods

        public virtual bool IsValid()
        {
            if (m_Map != null && m_Map != Map.Internal && m_X1 >= 0 && m_X1 < m_X2 && m_X2 <= m_Map.Width && m_Y1 >= 0 && m_Y1 < m_Y2 && m_Y2 <= m_Map.Height)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual bool IsIn(Map map, int x, int y)
        {
            if (!IsValid() || map != m_Map)
            {
                return false;
            }

            if ((x >= m_X1 && x <= m_X2) && (y >= m_Y1 && y <= m_Y2))
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}