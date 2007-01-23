using System;
using System.Collections;
using Server;

namespace Knives.Chat3
{
    public class MessageGump : GumpPlus
    {

        #region Class Definitions

        private Message c_Message;

        public Data GetData { get { return Data.GetData(Owner); } }

        #endregion

        #region Constructors

        public MessageGump(Mobile m, Message msg) : base(m, 200, 400)
        {
            Override = true;

            c_Message = msg;

            if (GetData.Messages.Contains(msg))
            {
                if (!msg.Read && Data.GetData(msg.From).ReadReceipt && msg.From.AccessLevel >= m.AccessLevel)
                    msg.From.SendMessage(Data.GetData(msg.From).SystemC, m.RawName + " " + General.Local(197) + " " + msg.Subject);

                msg.Read = true;
            }

            NewGump();
        }

        #endregion

        #region Methods

        protected override void BuildGump()
        {
            int width = 300;
            int y = 10;

            AddImage(10, y, 0x589);
            Avatar av = Avatar.GetAvatar(c_Message.From);
            AddImage(10 + av.X, y + av.Y, av.Id);
            AddButton(20, 20, 0x2716, "Profile", new GumpCallback(Profile));

            AddHtml(95, y += 15, width-105, General.Local(60) + " " + c_Message.From.RawName);
            AddHtml(95, y += 20, width-105, 42, c_Message.Subject, false, false);

            AddHtml(20, y += 50, width-40, 80, c_Message.Msg, true, true);
            y += 90;

            if (GetData.Messages.Contains(c_Message))
            {
                if (c_Message.Type == MsgType.Normal)
                {
                    if (Message.CanMessage(Owner, c_Message.From) && !Message.StaffTimeout(c_Message))
                    {
                        AddHtml(width - 85, y, 50, General.Local(248));
                        AddButton(width - 100, y+3, 0x2716, "Reply", new GumpCallback(Reply));
                    }

                    AddHtml(width - 145, y, 50, General.Local(249));
                    AddButton(width-160, y+3, 0x2716, "Delete", new GumpCallback(Delete));

                    if (c_Message.From.AccessLevel == AccessLevel.Player)
                    {
                        AddHtml(95, y, 50, General.Local(2));
                        AddButton(80, y+3, 0x2716, "Ignore", new GumpCallback(Ignore));
                    }
                }
                else if (c_Message.Type == MsgType.Invite)
                {
                    AddHtml(width - 85, y, 50, General.Local(250));
                    AddButton(width - 100, y+3, 0x2716, "Accept", new GumpCallback(Accept));

                    AddHtml(width - 145, y, 50, General.Local(251));
                    AddButton(width - 160, y+3, 0x2716, "Deny", new GumpCallback(Deny));

                    if (c_Message.From.AccessLevel == AccessLevel.Player)
                    {
                        AddHtml(95, y, 50, General.Local(2));
                        AddButton(80, y+3, 0x2716, "Ignore", new GumpCallback(Ignore));
                    }
                }
            }

            AddBackgroundZero(0, 0, width, y + 30, Data.GetData(Owner).DefaultBack);
        }

        #endregion

        #region Responses

        private void Profile()
        {
            NewGump();

            new ProfileGump(Owner, c_Message.From);
        }

        private void Reply()
        {
            if (Message.CanMessage(Owner, c_Message.From))
                new SendMessageGump(Owner, c_Message.From, "", c_Message, MsgType.Normal);
        }

        private void Delete()
        {
            GetData.DeleteMessage(c_Message);
        }

        private void Ignore()
        {
            GetData.Ignores.Add(c_Message.From);

            Owner.SendMessage(GetData.SystemC, General.Local(68) + " " + c_Message.From.RawName);
            if (c_Message.Type == MsgType.Invite)
                Deny();
        }

        private void Accept()
        {
            c_Message.From.SendMessage(Data.GetData(c_Message.From).SystemC, Owner.RawName + " " + General.Local(87));

            GetData.AddFriend(c_Message.From);
            Data.GetData(c_Message.From).AddFriend(Owner);

            GetData.Messages.Remove(c_Message);
        }

        private void Deny()
        {
            c_Message.From.SendMessage(Data.GetData(c_Message.From).SystemC, Owner.RawName + " " + General.Local(88));
            Owner.SendMessage(GetData.SystemC, General.Local(89) + " " + c_Message.From.RawName);

            GetData.Messages.Remove(c_Message);
        }

        #endregion

    }
}