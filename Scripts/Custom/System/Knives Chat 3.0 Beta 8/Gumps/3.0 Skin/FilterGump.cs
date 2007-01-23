using System;
using Server;

namespace Knives.Chat3
{
    public class FilterGump : GumpPlus
    {
        public FilterGump(Mobile m)
            : base(m, 100, 100)
        {
            NewGump();
        }

        protected override void BuildGump()
        {
            int width = 300;
            int y = 10;

            AddHtml(0, y, width, "<CENTER>" + General.Local(214));
            AddImage(width / 2 - 100, y + 2, 0x39);
            AddImage(width / 2 + 70, y + 2, 0x3B);

            AddHtml(0, y += 25, width, "<CENTER>" + General.Local(142));
            AddButton(width / 2 - 120, y, Data.FilterSpeech ? 0x2343 : 0x2342, "Filter Speech", new GumpCallback(FilterSpeech));
            AddButton(width / 2 + 100, y, Data.FilterSpeech ? 0x2343 : 0x2342, "Filter Speech", new GumpCallback(FilterSpeech));
            AddHtml(0, y += 20, width, "<CENTER>" + General.Local(143));
            AddButton(width / 2 - 120, y, Data.FilterMsg ? 0x2343 : 0x2342, "Filter Messages", new GumpCallback(FilterMsg));
            AddButton(width / 2 + 100, y, Data.FilterMsg ? 0x2343 : 0x2342, "Filter Messages", new GumpCallback(FilterMsg));

            AddHtml(0, y += 25, width, "<CENTER>" + General.Local(154) + ": " + General.Local(155 + (int)Data.FilterPenalty));
            AddButton(width / 2 - 80, y + 4, 0x2716, "Filter Penalty", new GumpCallback(FilterPenalty));
            AddButton(width / 2 + 70, y + 4, 0x2716, "Filter Penalty", new GumpCallback(FilterPenalty));

            if (Data.FilterPenalty == Chat3.FilterPenalty.Ban)
            {
                AddHtml(0, y += 25, width / 2 - 10, "<DIV ALIGN=RIGHT>" + General.Local(147));
                AddImageTiled(width / 2 + 15, y, 30, 21, 0xBBA);
                AddTextField(width / 2 + 15, y, 30, 21, 0x480, 0, "" + Data.FilterBanLength);
                AddHtml(width / 2 + 45, y, 100, "m");
                AddButton(width / 2 - 5, y + 4, 0x2716, "Submit", new GumpCallback(Submit));
            }

            if (Data.FilterPenalty != Chat3.FilterPenalty.None)
            {
                AddHtml(0, y += 25, width / 2 - 10, "<DIV ALIGN=RIGHT>" + General.Local(254));
                AddImageTiled(width / 2 + 15, y, 30, 21, 0xBBA);
                AddTextField(width / 2 + 15, y, 30, 21, 0x480, 2, "" + Data.FilterWarnings);
                AddButton(width / 2 - 5, y + 4, 0x2716, "Submit", new GumpCallback(Submit));
            }

            AddHtml(0, y += 25, width/2-10, "<DIV ALIGN=RIGHT>" + General.Local(148));
            AddImageTiled(width/2+15, y, 70, 21, 0xBBA);
            AddTextField(width/2+15, y, 70, 21, 0x480, 1, "");
            AddButton(width/2-5, y + 4, 0x2716, "Add/Remove Filter", new GumpCallback(AddFilter));

            string txt = General.Local(151) + " ";

            foreach (string filter in Data.Filters)
                txt += filter + " ";

            AddHtml(20, y += 25, width-40, 60, txt, false, false);

            AddBackgroundZero(0, 0, width, y+80, Data.GetData(Owner).DefaultBack);
        }

        private void FilterSpeech()
        {
            Data.FilterSpeech = !Data.FilterSpeech;

            NewGump();
        }

        private void FilterMsg()
        {
            Data.FilterMsg = !Data.FilterMsg;

            NewGump();
        }

        private void Submit()
        {
            Data.FilterBanLength = Utility.ToInt32(GetTextField(0));
            Data.FilterWarnings = Utility.ToInt32(GetTextField(2));

            NewGump();
        }

        private void AddFilter()
        {
            if (GetTextField(1).Trim() == "")
            {
                NewGump();
                return;
            }

            if (Data.Filters.Contains(GetTextField(1).ToLower()))
            {
                Data.Filters.Remove(GetTextField(1).ToLower());
                Owner.SendMessage(Data.GetData(Owner).SystemC, General.Local(149) + " " + GetTextField(1).ToLower());
            }
            else
            {
                Data.Filters.Add(GetTextField(1).ToLower());
                Owner.SendMessage(Data.GetData(Owner).SystemC, General.Local(150) + " " + GetTextField(1).ToLower());
            }

            NewGump();
        }

        private void FilterPenalty()
        {
            new FilterPenaltyGump(Owner, this);
        }


        private class FilterPenaltyGump : GumpPlus
        {
            private GumpPlus c_Gump;

            public FilterPenaltyGump(Mobile m, GumpPlus g)
                : base(m, 100, 100)
            {
                c_Gump = g;

                NewGump();
            }

            protected override void BuildGump()
            {
                int y = 10;

                AddHtml(0, y, 150, "<CENTER>" + General.Local(154));

                y += 5;

                for (int i = 0; i < 3; ++i)
                {
                    AddHtml(60, y += 20, 90, General.Local(155 + i));
                    AddButton(40, y + 4, 0x2716, "Select", new GumpStateCallback(Select), i);
                }

                AddBackgroundZero(0, 0, 150, y + 40, Data.GetData(Owner).DefaultBack);
            }

            protected override void OnClose()
            {
                c_Gump.NewGump();
            }

            private void Select(object o)
            {
                if (!(o is int))
                    return;

                Data.FilterPenalty = (FilterPenalty)(int)o;

                c_Gump.NewGump();
            }
        }
    }
}