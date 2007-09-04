// 15AUG2007 written by RavonTUS
//
//   /\\           |\\  ||
//  /__\\  |\\ ||  | \\ ||  /\\  \ //
// /    \\ | \\||  |  \\||  \//  / \\ 
// Play at An Nox, the cure for the UO addiction
// http://annox.no-ip.com  // RavonTUS@Yahoo.com

//use [add popup
//use [set name "Your PopUp information goes here."

using System;
using Server.Items;
using Server.Gumps;
using Server.Accounting;
using Server.Mobiles;

namespace Server.Items
{
    public class PopUp3 : Item
    {
        [Constructable]
        public PopUp3()
            : base(0x1BC3)
        {
            Movable = false;
            Visible = false;

            Name = "PopUp3 - use [set name 'Your PopUp3 information goes here.'";
        }

        public override bool HandlesOnMovement { get { return true; } }

        public override void OnMovement(Mobile from, Point3D oldLocation)
        {
            if ( from is PlayerMobile && from.InRange(this, 10))
            {
                if (!from.HasGump(typeof(PopUp3Gump)))
                    from.SendGump(new PopUp3Gump(Name));
            }
            if ( from is PlayerMobile && !from.InRange(this, 10) )
            {
                if (from.HasGump(typeof(PopUp3Gump)))
                    from.CloseGump(typeof(PopUp3Gump));
            }
        }

        //If you want to be able to double click on the 'PopUp3' then add the following lines.
        //public override void OnDoubleClick(Mobile from)
        //{
        //    if (!from.HasGump(typeof(PopUp3Gump)))
        //        from.SendGump(new PopUp3Gump(Name));
        //}

        public PopUp3(Serial serial)
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

//The Gump
namespace Server.Gumps
{
    public class PopUp3Gump : Gump
    {
        public PopUp3Gump(string Name)
            : base(0, 0)
        {
            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;
            this.AddPage(0);
            this.AddImage(89, 112, 9460);
            this.AddHtml(125, 149, 177, 100, @"<center>" + Name + "</center>", (bool)false, (bool)false);

            //for smaller sign replace the above two lines with these two lines.
            //this.AddImage(163, 134, 100);
            //this.AddHtml(191, 160, 87, 49, @"<center>" + Name + "</center>", (bool)false, (bool)false);
        }
    }
}


