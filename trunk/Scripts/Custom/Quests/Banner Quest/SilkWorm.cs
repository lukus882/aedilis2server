using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a silk worm corpse")]
    public class SilkWorm : BaseCreature
    {
        [Constructable]
        public SilkWorm()
            : base(AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            Name = "a silk worm";
            Body = 52;
            Hue = 1810;
            BaseSoundID = 0xDB;

            SetStr(122, 134);
            SetDex(16, 25);
            SetInt(6, 10);

            SetHits(215, 219);
            SetMana(0);

            SetDamage(11, 14);

            SetDamageType(ResistanceType.Physical, 50);
            SetDamageType(ResistanceType.Cold, 50);

            SetResistance(ResistanceType.Physical, 40, 50);
            SetResistance(ResistanceType.Cold, 40, 50);

            SetSkill(SkillName.Wrestling, 50.1, 70.0);
            SetSkill(SkillName.MagicResist, 15.1, 20.0);
            SetSkill(SkillName.Tactics, 44.3, 54.0);
            SetSkill(SkillName.Wrestling, 19.3, 34.0);

            Fame = 300;
            Karma = -300;

            VirtualArmor = 20;
        }
        public override void OnCarve(Mobile from, Corpse corpse)
        {
            if (corpse.Carved)
            {
                base.OnCarve(from, corpse);
            }
            else
            {
                if (Utility.Random(5) == 1)
                {
                    from.SendMessage("You dig through the worm goo and find some silk!");
                    corpse.DropItem(new WormSilk());
                }
                else
                {
                    from.SendMessage("You carve into the gooey worm corpse, but find nothing useful.");
                }

                corpse.Carved = true;
            }
        }

        public override Poison PoisonImmune { get { return Poison.Lesser; } }
        public override Poison HitPoison { get { return Poison.Lesser; } }


        public SilkWorm(Serial serial)
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