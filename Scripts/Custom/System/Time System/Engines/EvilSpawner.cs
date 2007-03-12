using System;
using System.Collections.Generic;
using System.IO;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	public class EvilSpawner : Item
    {
        #region Constructor

        [Constructable]
        public EvilSpawner() : base( 0x1f13 )
		{
            Name = "an Evil Spawner";
            Visible = false;
            Movable = false;

            m_Enabled = false;

            m_Spawns = new List<BaseCreature>();

            TimeSystem.Data.EvilSpawnersList.Add(this);
        }

        public EvilSpawner(Serial serial)
            : base(serial)
		{
		}

        #endregion

        #region Private Variables

        private int m_BinaryVersion = 1;

        private bool m_Enabled;
        private bool m_Active;

        private int m_MaxSpawns;
        private double m_MaxSpawnPercentage;

        private int m_MinDelay;
        private int m_MaxDelay;

        private int m_SpawnRange;

        private List<BaseCreature> m_Spawns;

        private SpawnerTimer m_Timer;

        #endregion

        #region Public Variables

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Enabled
        {
            get { return m_Enabled; }
            set
            {
                if (m_Active && !value)
                {
                    m_Active = false;

                    Stop();
                }

                m_Enabled = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool IsActive
        {
            get { return m_Active; }
        }

        public bool Active
        {
            get { return m_Active; }
            set
            {
                if (!m_Active && value)
                {
                    Start();
                }
                else if (m_Active && !value)
                {
                    Stop();
                }

                m_Active = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int MaxSpawns { get { return m_MaxSpawns; } set { m_MaxSpawns = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public double MaxSpawnPercentage
        {
            get { return m_MaxSpawnPercentage; }
            set
            {
                if (value > 1.0)
                {
                    value = 1.0;
                }
                else if (value < 0)
                {
                    value = 0;
                }

                m_MaxSpawnPercentage = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int MinDelay { get { return m_MinDelay; } set { m_MinDelay = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int MaxDelay { get { return m_MaxDelay; } set { m_MaxDelay = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int SpawnRange { get { return m_SpawnRange; } set { m_SpawnRange = value; } }

        public List<BaseCreature> Spawns { get { return m_Spawns; } set { m_Spawns = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int TotalSpawns { get { return m_Spawns.Count; } }

        #endregion

        #region Spawning Control

        public void Start()
        {
            m_Timer = new SpawnerTimer(this, TimeSystem.Support.GetRandom(m_MinDelay, m_MaxDelay));
            m_Timer.Start();
        }

        public void Stop()
        {
            if (m_Timer != null)
            {
                m_Timer.Stop();
            }

            m_Timer = null;

            Despawn();
        }

        public void Spawn()
        {
            try
            {
                object o = Activator.CreateInstance(typeof(EvilSpirit));

                BaseCreature bc = (BaseCreature)o;

                m_Spawns.Add(bc);

                Point3D loc = (GetSpawnPosition());

                bc.OnBeforeSpawn(loc, Map);

                InvalidateProperties();

                bc.MoveToWorld(loc, Map);

                bc.RangeHome = m_SpawnRange;
                bc.Home = Location;

                bc.OnAfterSpawn();
            }
            catch
            {
            }
        }

        public Point3D GetSpawnPosition()
        {
            if (Map == null)
            {
                return Location;
            }

            for (int i = 0; i < 10; i++)
            {
                int x = Location.X + (TimeSystem.Support.GetRandom(-m_SpawnRange, m_SpawnRange));
                int y = Location.Y + (TimeSystem.Support.GetRandom(-m_SpawnRange, m_SpawnRange));
                int z = Map.GetAverageZ(x, y);

                if (Map.CanSpawnMobile(new Point2D(x, y), Location.Z))
                {
                    return new Point3D(x, y, Location.Z);
                }
                else if (Map.CanSpawnMobile(new Point2D(x, y), z))
                {
                    return new Point3D(x, y, z);
                }
            }

            return Location;
        }

        public void Defrag()
        {
            for (int i = 0; i < m_Spawns.Count; i++)
            {
                BaseCreature bc = m_Spawns[i];

                bool defrag = false;

                if (bc != null && bc.Deleted)
                {
                    defrag = true;
                }
                else if (bc == null)
                {
                    defrag = true;
                }

                if (defrag)
                {
                    m_Spawns.RemoveAt(i);

                    i--;
                }
            }
        }

        public void Despawn()
        {
            for (int i = 0; i < m_Spawns.Count; i++)
            {
                BaseCreature bc = m_Spawns[i];

                if (bc != null && !bc.Deleted)
                {
                    bc.Delete();
                }
            }

            m_Spawns.Clear();
            m_Spawns.TrimExcess();
        }

        public void OnTick()
        {
            Defrag();

            int totalSpawns = TotalSpawns;

            if (totalSpawns < m_MaxSpawns)
            {
                int spawnCount = (int)(m_MaxSpawns * m_MaxSpawnPercentage);

                if ((totalSpawns + spawnCount) > m_MaxSpawns)
                {
                    spawnCount = m_MaxSpawns - totalSpawns;
                }

                for (int i = 0; i < spawnCount; i++)
                {
                    Spawn();
                }
            }
        }

        #endregion

        #region Overrides

        public override void OnDelete()
        {
            m_Active = false;

            Stop();

            TimeSystem.Data.EvilSpawnersList.Remove(this);

            base.OnDelete();
        }

        #endregion

        #region Serialization

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(m_BinaryVersion);

            writer.Write(m_Enabled);
            writer.Write(m_Active);

            writer.Write(m_MaxSpawns);
            writer.Write(m_MaxSpawnPercentage);

            writer.Write(m_MinDelay);
            writer.Write(m_MaxDelay);

            writer.Write(m_SpawnRange);

            writer.Write(m_Spawns.Count);

            for (int i = 0; i < m_Spawns.Count; ++i)
            {
                BaseCreature bc = m_Spawns[i];

                writer.Write((Mobile)bc);
            }
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 1:
                    {
                        m_Enabled = reader.ReadBool();
                        m_Active = reader.ReadBool();

                        m_MaxSpawns = reader.ReadInt();
                        m_MaxSpawnPercentage = reader.ReadDouble();

                        m_MinDelay = reader.ReadInt();
                        m_MaxDelay = reader.ReadInt();

                        m_SpawnRange = reader.ReadInt();

                        int count = reader.ReadInt();

                        m_Spawns = new List<BaseCreature>(count);

                        for (int i = 0; i < count; ++i)
                        {
                            IEntity entity = World.FindEntity(reader.ReadInt());

                            if (entity != null)
                            {
                                m_Spawns.Add((BaseCreature)entity);
                            }
                        }

                        break;
                    }
            }

            if (m_Enabled && m_Active)
            {
                Start();
            }

            TimeSystem.Data.EvilSpawnersList.Add(this);
        }

        #endregion

        #region Timer

        private class SpawnerTimer : Timer
        {
            private EvilSpawner m_EvilSpawner;

            public SpawnerTimer(EvilSpawner evilSpawner, int delay)
                : base(TimeSpan.Zero, TimeSpan.FromSeconds(delay))
            {
                m_EvilSpawner = evilSpawner;
            }

            protected override void OnTick()
            {
                if (m_EvilSpawner != null && !m_EvilSpawner.Deleted && m_EvilSpawner.Enabled)
                {
                    m_EvilSpawner.OnTick();
                }
                else
                {
                    Stop();
                }
            }
        }

        #endregion
    }
}