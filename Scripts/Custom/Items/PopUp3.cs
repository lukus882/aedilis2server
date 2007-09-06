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
	    	if ( from is PlayerMobile)
		{
			PlayerMobile pm = (PlayerMobile)from;

            		if ( pm.PopUpToggle && pm.InRange(this, 10))
            			{
	
           	   		  		if (!pm.HasGump(typeof(PopUp3Gump)))
				 	 	{
            	   		 	 	pm.SendGump(new PopUp3Gump(Name));
						}
           			 }
           		 if (!pm.InRange(this, 10))
           			 {
            	 		   		if (pm.HasGump(typeof(PopUp3Gump)))
				 	 	{
            	 		  		 pm.CloseGump(typeof(PopUp3Gump));
						}
           			 }
		}
        }


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


