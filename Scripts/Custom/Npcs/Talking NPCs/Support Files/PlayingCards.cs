using System;

namespace Server.Items
{
    [Furniture]
    [Flipable(0xE15, 0XE16, 0XE18, 0XE19)]
    public class PlayingCards : Item
    {
        [Constructable]
        public PlayingCards()
            : base(0xE15)
        {
            Weight = 20.0;
        }

        public PlayingCards(Serial serial)
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

    [Furniture]
    public class PlayingCardsSingle : Item
    {
        [Constructable]
        public PlayingCardsSingle()
            : base(0xE17)
        {
            Weight = 20.0;
        }

        public PlayingCardsSingle(Serial serial)
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

    [Furniture]
    public class PlayingCardsTwoHandDealt : Item
    {
        [Constructable]
        public PlayingCardsTwoHandDealt()
            : base(0xFA2)
        {
            Weight = 20.0;
        }

        public PlayingCardsTwoHandDealt(Serial serial)
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

    [Furniture]
    public class PlayingCardsTwoHandUndealt : Item
    {
        [Constructable]
        public PlayingCardsTwoHandUndealt()
            : base(0xFA3)
        {
            Weight = 20.0;
        }

        public PlayingCardsTwoHandUndealt(Serial serial)
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
