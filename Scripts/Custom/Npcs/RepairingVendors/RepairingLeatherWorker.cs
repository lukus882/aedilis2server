//Full credit goes to LIACs as this is her script
//Revised version by killsom3thing
using System; 
using System.Collections; 
using Server;
using Server.Targeting;
using Server.Items;
using Server.Network;

namespace Server.Mobiles 
{
    public class RepairingLeatherWorker : BaseVendor
    {
        private ArrayList m_SBInfos = new ArrayList();
        protected override ArrayList SBInfos { get { return m_SBInfos; } }

        [Constructable]
        public RepairingLeatherWorker()
            : base("the leather worker")
        {
        }
        public override void InitSBInfo()
        {
            m_SBInfos.Add(new SBLeatherArmor());
            m_SBInfos.Add(new SBStuddedArmor());
            m_SBInfos.Add(new SBLeatherWorker());
        }
        public RepairingLeatherWorker(Serial serial)
            : base(serial)
        {
        }

        #region repair by LIACS
        public override void OnSpeech(SpeechEventArgs e)
        {
            if ((e.Speech.ToLower() == "repair"))
            {
                BeginRepair(e.Mobile);
            }

            else
            {
                base.OnSpeech(e);
            }

        }
        public void BeginRepair(Mobile from)
        {
            if (Deleted || !from.CheckAlive())
                return;

            SayTo(from, "What do you want me to repair?");

            from.Target = new RepairTarget(this);

        }

        private class RepairTarget : Target
        {
            private RepairingLeatherWorker m_LeatherWorker;

            public RepairTarget(RepairingLeatherWorker leatherworker)
                : base(12, false, TargetFlags.None)
            {
                m_LeatherWorker = leatherworker;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {

                if (targeted is BaseArmor)
                {
                    BaseArmor ba = targeted as BaseArmor;
                    Container pack = from.Backpack;
                    int toConsume = 0;
                    toConsume = (ba.MaxHitPoints - ba.HitPoints) * 20; //Adjuct price here by changing 3 to whatever you want.

                    if ((toConsume == 0) && (ba.Resource == CraftResource.RegularLeather || ba.Resource == CraftResource.SpinedLeather || ba.Resource == CraftResource.HornedLeather || ba.Resource == CraftResource.BarbedLeather))
                    {
                        m_LeatherWorker.SayTo(from, "That armor is not damaged.");
                    }
                    else if (ba.Resource == CraftResource.Iron || ba.Resource == CraftResource.DullCopper || ba.Resource == CraftResource.ShadowIron || ba.Resource == CraftResource.Copper || ba.Resource == CraftResource.Bronze || ba.Resource == CraftResource.Gold || ba.Resource == CraftResource.Agapite || ba.Resource == CraftResource.Verite || ba.Resource == CraftResource.Valorite)
                    {
                        m_LeatherWorker.SayTo(from, "I cannot repair that.");
                    }
                    else if ((ba.HitPoints < ba.MaxHitPoints) && (pack.ConsumeTotal(typeof(Gold), toConsume) && (ba.Resource == CraftResource.RegularLeather || ba.Resource == CraftResource.SpinedLeather || ba.Resource == CraftResource.HornedLeather || ba.Resource == CraftResource.BarbedLeather)))
                    {
                        ba.HitPoints = ba.MaxHitPoints;
                        m_LeatherWorker.SayTo(from, "Here is your armor.");
                        from.SendMessage(String.Format("You pay {0}gp.", toConsume));
                    }
                    else
                    {
                        m_LeatherWorker.SayTo(from, "It will cost you {0}gp to have your armor repaired.", toConsume);
                        from.SendMessage("You do not have enough gold.");
                    }
                }
            }
        }


        #endregion


            public override void Serialize(GenericWriter writer)
            {
                base.Serialize(writer);

                writer.Write((int)0); // version 
            }

            public override void Deserialize(GenericReader reader)
            {
                base.Deserialize(reader);

                int version = reader.ReadInt();
            }
        }
} 
