using System;
using System.Collections;
using Server.Items;
using Server.Multis;
using Server.Mobiles;
using Server.Network;

namespace Server.Custom
{
    public class LinkedBook : BaseBook
    {
        private LinkedBook mate;
        [CommandProperty(AccessLevel.GameMaster)]
        public LinkedBook Mate { get { return mate; } set { mate = value; } }

        private bool Toggle;

        [Constructable]
        public LinkedBook()
            : base(0xFBD)
        {
            Name = "Linked Book";
            Weight = 1.0;
        }

        public LinkedBook(Serial serial)
            : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            BaseBook book;
            if (Toggle || mate == null) book = this;
            else book = mate;
            if (book.Title == null && book.Author == null && book.Writable == true)
            {
                book.Title = "a book";
                book.Author = from.Name;
            }

            from.Send(new BookHeader(from, book));
            from.Send(new BookPageDetails(book));
            Toggle = !Toggle;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version

            writer.Write(mate);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            mate = reader.ReadItem() as LinkedBook;
        }
    }

    public class LinkedBookBag : Bag
    {
        public override string DefaultName
        {
            get { return "a Bag of Linked Books"; }
        }

        [Constructable]
        public LinkedBookBag()
            : base()
        {
            Movable = true;
            Hue = Utility.RandomBlueHue();
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (Items.Count == 0)
            {
                LinkedBook bookA = new LinkedBook();
                LinkedBook bookB = new LinkedBook();
                bookA.Mate = bookB;
                bookB.Mate = bookA;
                bookA.Author = from.Name;
                bookB.Author = from.Name;
                bookA.Title = string.Format("{0}'s Master Book", from.Name);
                bookB.Title = string.Format("{0}'s Copy Book", from.Name);
                DropItem(bookA);
                DropItem(bookB);
            }
            base.OnDoubleClick(from);
        }

        public LinkedBookBag(Serial serial)
            : base(serial)
        {
        }

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