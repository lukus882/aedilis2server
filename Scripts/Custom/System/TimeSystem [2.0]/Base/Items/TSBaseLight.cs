using System;
using Server;

namespace Server.Items
{
    public abstract class TSBaseLight : BaseLight
    {
        private bool m_UseRandomLightOutage = true;
        private bool m_UseAutoLighting = true;

        [CommandProperty(AccessLevel.GameMaster)]
        public bool UseRandomLightOutage
        {
            get
            {
                return m_UseRandomLightOutage;
            }
            set
            {
                m_UseRandomLightOutage = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool UseAutoLighting
        {
            get
            {
                return m_UseAutoLighting;
            }
            set
            {
                m_UseAutoLighting = value;

                if (!value)
                {
                    m_UseRandomLightOutage = value;
                }
            }
        }

        [Constructable]
        public TSBaseLight(int itemID)
            : base(itemID)
        {
        }

        public TSBaseLight(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
            writer.Write(m_UseRandomLightOutage);
            writer.Write(m_UseAutoLighting);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    {
                        m_UseRandomLightOutage = reader.ReadBool();
                        m_UseAutoLighting = reader.ReadBool();

                        break;
                    }
            }
        }
    }
}
