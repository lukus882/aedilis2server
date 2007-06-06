using System;
using Server;
using Server.Gumps;

namespace Server.Gumps
{
    public class QuestTemplate : Gump
    {
        int Image1 = Utility.RandomMinMax(30501, 30510);
        int Image2 = Utility.RandomMinMax(30501, 30510);

        public QuestTemplate(string QuestName, string QuestDescription)
            : base(400, 10)
        {
            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;
            this.AddPage(0);
            this.AddBackground(18, 12, 407, 422, 9300);
            this.AddLabel(116, 34, 0, @"" + QuestName);
            this.AddHtml(39, 68, 242, 318, @"<p>" + QuestDescription + "</p>", (bool)true, (bool)true);
            this.AddImage(296, 31, Image1);//30501 to 30510
            this.AddImage(296, 237, Image2);//30501 to 30510
            this.AddButton(133, 396, 247, 248, (int)Buttons.Button1, GumpButtonType.Reply, 0);

        }

        public enum Buttons
        {
            Button1,
        }

    }
}