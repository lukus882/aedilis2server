using System;
using Server.Items;
using Server.Gumps;
using Server.Accounting;
using Server.Mobiles;

namespace Server.Items
{
    public class PopUp2 : Item
    {
        [Constructable]
        public PopUp2()
            : base(0x1BC3)
        {
            Movable = false;
            Visible = false;

            Name = "PopUp2 - use [set name 'Your PopUp2 information goes here.'";
        }

        public override bool HandlesOnMovement { get { return true; } }

        public override void OnMovement(Mobile from, Point3D oldLocation)
        {
	   	if ( from is PlayerMobile)
		{
			PlayerMobile pm = (PlayerMobile)from;

            		if ( pm.PopUpToggle && pm.InRange(this, 5))
            			{
	
           	   		  		if (!pm.HasGump(typeof(PopUp2Gump)))
				 	 	{
            	   		 	 	pm.SendGump(new PopUp2Gump(Name));
						}
           			 }
           		 if (!pm.InRange(this, 5))
           			 {
            	 		   		if (pm.HasGump(typeof(PopUp2Gump)))
				 	 	{
            	 		  		 pm.CloseGump(typeof(PopUp2Gump));
						}
           			 }
		}
        }

        public PopUp2(Serial serial)
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
    public class PopUp2Gump : Gump
    {
        public PopUp2Gump(string Name)
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


