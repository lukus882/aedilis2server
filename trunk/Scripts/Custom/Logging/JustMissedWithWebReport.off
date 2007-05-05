using System;
using System.Text;
using System.Collections;

using Server;
using Server.Gumps;
using Server.Mobiles;

using Server.Commands;

//28MAR2007 Adding Web Based List *** START ***
using System.IO;
using Server.Network;
//28MAR2007 Adding Web Based List *** END   ***

namespace Server.Custom.Misc
{
    public class JustMissed
    {
        public JustMissed() { }

        private static bool m_Enabled = true;
        private static Hashtable m_LogoutTable;

        [CommandProperty(AccessLevel.GameMaster)]
        public static bool Enabled { get { return m_Enabled; } set { m_Enabled = value; } }

        public static JustMissed Instance { get { return new JustMissed(); } }

        public static void Initialize()
        {
            EventSink.Login += new LoginEventHandler(EventSink_Login);
            EventSink.Logout += new LogoutEventHandler(EventSink_Logout);
            CommandSystem.Register("JMSettings", AccessLevel.GameMaster, new CommandEventHandler(JMSettings_OnCommand));

            m_LogoutTable = new Hashtable();
        }

        private static void JMSettings_OnCommand(CommandEventArgs e)
        {
            Mobile m = e.Mobile;

            if (m == null)
                return;

            m.SendGump(new PropertiesGump(m, Instance));
        }

        //28MAR2007 Adding Web Based List *** START ***
        private static string Encode(string input)
        {
            StringBuilder sb = new StringBuilder(input);

            sb.Replace("&", "&amp;");
            sb.Replace("<", "&lt;");
            sb.Replace(">", "&gt;");
            sb.Replace("\"", "&quot;");
            sb.Replace("'", "&apos;");

            return sb.ToString();
        }
        //28MAR2007 Adding Web Based List *** END   ***

        private static void EventSink_Logout(LogoutEventArgs e)
        {
            if (!Enabled)
                return;

            Mobile m = e.Mobile;

            if (m == null)
                return;

            if (m_LogoutTable.ContainsKey(m.Serial))
                m_LogoutTable.Remove(m.Serial);

            m_LogoutTable.Add(m.Serial, new JMLogTimer(m));

            //28MAR2007 Adding Web Based List *** START ***

            /*******************************************************/
            /*            path to where your website is stored     */
            string path = "C:/Aedilis 2.0/web";
            /*******************************************************/

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);

                using (StreamWriter op = new StreamWriter(path + "/youjustmissed.html"))
                {
                    op.WriteLine("<html>");
                    op.WriteLine("   <head>");
                    op.WriteLine("      <title>You Just Missed - updated by Ravon of An Nox</title>");
                    op.WriteLine("   </head>");
                    op.WriteLine("   <body bgcolor=\"black\">");
                    op.WriteLine("      <a href='http://annox.no-ip.com'><img src='http://bp2.blogger.com/_CGKuPVAFxb0/Rgp566xjEaI/AAAAAAAAAFo/Dv4bJaNU_xs/s200/Cemetery.jpg' alt='http://annox.no-ip.com' /></a>");
                    op.WriteLine("      <h1><font color='red'>You Just Missded:</h1>");
                    op.WriteLine("      <font color='yellow'>{0}<font color='white'> just left {1}", Encode(m.Name), m.Map);
                    op.WriteLine("      near the corodinates of {0}, {1}, {2} ", m.X, m.Y, m.Z);
                    op.WriteLine("      on {0}.<br>", DateTime.Now);
                    //op.WriteLine("      <table width=\"100%\">");
                    //op.WriteLine("      </table>");
                    //op.WriteLine("   </body>");
                    op.WriteLine("</html>");
                }
            }
            else
            {
                using (StreamWriter op = File.AppendText(path + "/youjustmissed.html"))
                {
                    op.WriteLine("<html>");
                    op.WriteLine("   <body bgcolor=\"black\">");
                    op.WriteLine("      <font color='yellow'>{0}<font color='white'> just left {1}", Encode(m.Name), m.Map);
                    op.WriteLine("      near the corodinates of {0}, {1}, {2} ", m.X, m.Y, m.Z);
                    op.WriteLine("      on {0}.<br>", DateTime.Now);
                    op.WriteLine("   </body>");
                    op.WriteLine("</html>");
                }
            }
            //28MAR2007 Adding Web Based List *** END   ***
        }

        private static void EventSink_Login(LoginEventArgs e)
        {
            if (!Enabled)
                return;

            Mobile m = e.Mobile;

            if (m == null)
                return;

            if (m_LogoutTable.ContainsKey(m.Serial))
                m_LogoutTable.Remove(m.Serial);

            foreach (JMLogTimer t in m_LogoutTable.Values)
            {
                if (t.Mobile == null)
                    continue;

                int minutes = t.Ticks / 60;
                m.SendMessage("You just missed {0}, they logged out {1} minute{2} ago.",
                    t.Mobile.Name,
                    (minutes > 0) ? minutes.ToString() : "less than 1",
                    (minutes > 0) ? "s" : "");
            }
        }

        public static void RemoveTimer(JMLogTimer timer)
        {
            if (m_LogoutTable.ContainsKey(timer.Mobile.Serial))
                m_LogoutTable.Remove(timer.Mobile.Serial);
        }
    }

    public class JMLogTimer : Timer
    {
        private Mobile m_LoggedMobile;
        private int m_Ticks;

        public Mobile Mobile { get { return m_LoggedMobile; } }
        public int Ticks { get { return m_Ticks; } }

        public JMLogTimer(Mobile m)
            : base(TimeSpan.FromSeconds(0.0), TimeSpan.FromSeconds(1.0))
        {
            m_LoggedMobile = m;
            Start();
        }

        protected override void OnTick()
        {
            if (m_Ticks > 3600)
            {
                JustMissed.RemoveTimer(this);
                Stop();
            }

            m_Ticks++;
        }
    }
}
