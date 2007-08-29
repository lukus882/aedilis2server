/*
***********************************
*************Created By************
***********************************
**************Broadside ***********
***********************************
****************AKA****************
***********************************
**************Bad Karma************
***********************************
 */
using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles
{
        [CorpseName("an Dragon Warrior corpse")]
        public class DragonWarrior : BaseCreature
        {[Constructable]
            public DragonWarrior()
                : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
            {
                Name = "Dragon Warrior";
                Body = 792;
                BaseSoundID = 0x45A;
		Hue = 302;

                SetStr(400, 500);
                SetDex(91, 115);
                SetInt(300, 320);

                SetHits(3000, 4000);

                SetDamage(10, 15);

                SetDamageType(ResistanceType.Physical, 100);

                SetResistance(ResistanceType.Cold, 30, 40);
                SetResistance(ResistanceType.Fire, 30, 40);
                SetResistance(ResistanceType.Poison, 30, 40);
                SetResistance(ResistanceType.Energy, 30, 40);

                SetSkill(SkillName.EvalInt, 85.0, 100.0);
                SetSkill(SkillName.Tactics, 75.1, 100.0);
                SetSkill(SkillName.MagicResist, 75.0, 97.5);
                SetSkill(SkillName.Wrestling, 100.2, 105.0);
                SetSkill(SkillName.Meditation, 120.0);
                SetSkill(SkillName.Focus, 120.0);
                SetSkill(SkillName.Fencing, 100.0, 105);

                Fame = 2500;
                Karma = -2500;

                VirtualArmor = 50;

            }

            public override void GenerateLoot()
            {
                PackGold(500, 1000);
                AddLoot(LootPack.Rich);
                AddLoot(LootPack.MedScrolls, 2);
                AddLoot(LootPack.Gems, 5);
            }

            public override bool AlwaysMurderer { get { return true; } }
            public override bool BardImmune { get { return true; } }

            public DragonWarrior(Serial serial)
                : base(serial)
            {
            }

            public override void Serialize(GenericWriter writer)
            {
                base.Serialize(writer);
                writer.Write((int)0);
            }

            public override void Deserialize(GenericReader reader)
            {
                base.Deserialize(reader);
                int version = reader.ReadInt();
            }
        }
    }
