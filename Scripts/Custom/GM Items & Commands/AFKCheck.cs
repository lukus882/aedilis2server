// If running RUNUO 2.0 keep the below in, otherwise comment it out!
#define RUNUO20
/*
Package Name: AFKChecker
Author: CEO
Version: 1.1
Public Release: 04/21/06
Purpose:  Performs an AFK check on targeted player.
 * 
 * This file is a self-contained package with all the necessary modules to allow
 * your staff to perform a consistent policy based AFK check for shards that do not
 * allow Unattended Macroing for resources or other UM/AFK actions. It is difficult to
 * OCR (I won't say impossible), by existing methods. Check it out and you'll see what
 * I mean. I take no responsiblity in the number of pissed off users this utility may
 * cause on your shard. :) A Staff member must initiate the gump, but it's setup such
 * that it would be very easy to hook it into some kind of automated function.
 * 
 * Before using on your shard modify the options (see below) to match your policies. Be sure to
 * adjust these two things though!
 * 
 *          //set to a website of your TOS/UM policy or leave null if not used.
 *          private string WebSite = "http://www.easyuo.com/forum/viewtopic.php?t=15991";
 * 
 *          // Set these map points to a location to whisk the AFKer to. Set all
 *          // points to 0 and MoveToMap to null if not using. Note: jail set to true
 *          // will override this location.
 *          private Point3D MoveToLoc = new Point3D(5259, 4005, 41);  // Delucia Pen near bank.
 *          private Map MoveToMap = Map.Felucca;
 * 
 * 
 *          [Usage("afki or afkcheck : {target player}")]
 * 
 *	Brings up information regarding certain skills, time online, and results of previous
 *  afk checks. Recommended you do this first before sending an afkcheck to the user.
 * 
 *         [Usage("afk or afkcheck : [JAIL/NOJAIL] [KICK/NOKICK] [NOWEB] [FORCE]{target player}")]
 * 
 * Jail = true/false ; see below. Jail player if they don't respond to check.
 * Kick = true/false ; see below. Kick player if they don't respond to check.
 * WebSite ; see below. Set to a website of your TOS/UM policy or leave null if not used.
 * FORCE = Force an AFK check to a player even if the last check was under 4 hours.
 * 
 * Command line options will override your default values you set below.
*/
using System;
using System.Text;
using Server;
using Server.Accounting;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Network;
#if RUNUO20
using Server.Commands;
#else
using Server.Scripts.Commands;
#endif
using Server.Targeting;

#if RUNUO20
namespace Server.Commands
#else
namespace Server.Scripts.Commands
#endif
{
    public class AFKCheckCom
    {
        public static void Initialize()
        {
#if RUNUO20
            CommandSystem.Register("AFK", AccessLevel.Counselor, new CommandEventHandler(AFKCheck_OnCommand));
            CommandSystem.Register("AFKCheck", AccessLevel.Counselor, new CommandEventHandler(AFKCheck_OnCommand));
#else
			Server.Commands.Register("AFK", AccessLevel.Counselor, new CommandEventHandler(AFKCheck_OnCommand));
            Server.Commands.Register("AFKCheck", AccessLevel.Counselor, new CommandEventHandler(AFKCheck_OnCommand));
#endif
        }

        [Usage("afk or afkcheck : [JAIL/NOJAIL] [KICK/NOKICK] [NOWEB] {target player}")]
        [Description("Performs an AFK check on targeted player.")]
        public static void AFKCheck_OnCommand(CommandEventArgs e)
        {

            bool Jail = false;          // Set to true if you want to jail the AFKer.
            bool Kick = false;          // set to true if you want to disconnect/kick the AFKer.
            bool LaunchBrowser = true;  // Used to override website value below (only if false).
            bool Force = false;         // Leave false, can override with the FORCE option.

            Mobile m = e.Mobile;
            if (e.Length > 0)
            {
                for (int i = 0; i < e.Length; ++i)
                {
                    switch (e.Arguments[i].ToUpper())
                    {
                        case "FORCE":
                            Force = true;
                            break;

                        case "JAIL":
                            Jail = true;
                            break;

                        case "NOJAIL":
                            Jail = false;
                            break;

                        case "KICK":
                            Kick = true;
                            break;

                        case "NOKICK":
                            Kick = false;
                            break;

                        case "NOWEB":
                            LaunchBrowser = false;
                            break;

                        default:
                            break;
                    }
                }
            }
            m.SendMessage("Target player now.");
            m.Target = new InternalTarget(Force, Jail, Kick, LaunchBrowser);
        }

        private class InternalTarget : Target
        {

            //set to a website of your TOS/UM policy or leave null if not used.
            private string WebSite = "http://www.easyuo.com/forum/viewtopic.php?t=15991";

            // Set these map points to a location to whisk the AFKer to. Set all
            // points to 0 and MoveToMap to null if not using. Note: jail set to true
            // will override this location.
            private Point3D MoveToLoc = new Point3D(5259, 4005, 41);  // Delucia Pen near bank.
            private Map MoveToMap = Map.Felucca;

            private const byte MaxRetries = 5;
            private const int MaxSeconds = 90;
            private bool tJail;
            private bool tKick;
            private bool tForce;

            public InternalTarget(bool Force, bool Jail, bool Kick, bool LaunchBrowser)
                : base(12, false, TargetFlags.None)
            {
                this.CheckLOS = true;
                this.AllowGround = false;
                this.Range = 15;
                tJail = Jail;
                tKick = Kick;
                tForce = Force;
                if (!LaunchBrowser)
                    WebSite = null;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (!(o is PlayerMobile) || o == null)
                {
                    from.SendMessage("Invalid target, must be a player.");
                }
                else
                {
                    Mobile t = (Mobile)o;
					NetState ns = t.NetState;
                    if (t.Deleted || t.Map == Map.Internal)
                        from.SendMessage("Invalid target, must be an online player.");
                    else if (t.AccessLevel != AccessLevel.Player)
                        from.SendMessage("Invalid target, not AccessLevel.Player.");
					else if (ns == null)
						from.SendMessage("Invalid target, this player is either disconnected or logging out.");
                    else
                    {
                        Account acct = t.Account as Account;
                        if (acct != null)
                        {
                            string s_LastAFKCheck = acct.GetTag("LastAFKCheck");
                            DateTime LastAFKCheck = DateTime.Now.Subtract(TimeSpan.FromHours(5));
                            if (s_LastAFKCheck != null)
                            {
                                try
                                {
                                    LastAFKCheck = DateTime.Parse(s_LastAFKCheck);
                                }
                                catch { };
                            }
                            else
                                LastAFKCheck = DateTime.Now.Subtract(TimeSpan.FromHours(5));
                            if (DateTime.Now - LastAFKCheck > TimeSpan.FromHours(4) || tForce)
                            {
                                t.CloseGump(typeof(AFKCheckGump));
                                t.SendGump(new AFKCheckGump(from, t, 0, MaxRetries, 0, MaxSeconds, null, tKick, tJail, MoveToLoc, MoveToMap, WebSite, null));
                                from.SendMessage(1150,"AFK check gump sent to {0}.", t.Name);
                                CommandLogging.WriteLine(from, "{0} {1} Issuing AFK Check to: {2} ", from.AccessLevel, CommandLogging.Format(from), CommandLogging.Format(t));
                                acct.SetTag("LastAFKCheck", DateTime.Now.ToString());
                                acct.SetTag("LastAFKCheckBy", from.Name);
                            }
                            else
                            {
                                from.SendMessage(36, "Last AFKCheck under 4 hours.");
                                from.SendMessage(36, "Use FORCE option.");
                                from.SendGump(new AFKInfoGump(t));
                            }
                        }
                        else
                            from.SendMessage("Invalid target, Account property = null!");
                    }
                }
            }
        }
    }
}

namespace Server.Gumps
{
    public class AFKCPassPhraseGump : Gump
    {
        private const int MaxDistractors = 4;

        public AFKCPassPhraseGump(string passphrase)
            : base(275, 200)
        {
            Closable = false;
            Disposable = true;
            Dragable = false;
            Resizable = false;
            AddBackground(0, 0, 250, 90, Utility.RandomList(5100, 5120, 9200, 9250, 9260, 9270, 9300, 9350, 9400, 9450, 9500, 9550));
            AddAlphaRegion(0, 0, 250, 90);
            for (int i = 0; i < 5; i++)
            {
                AddLabel(25 + (i * 40) + Utility.Random(40), 40 + (Utility.Random(2) == 0 ? Utility.Random(25) : -Utility.Random(25)), Utility.Random(1200), getchar(passphrase.Substring(i, 1)));
                for (int h = 0; h < Utility.Random(MaxDistractors); h++)
                {
                    AddLabel(20 + (i * 41) + Utility.Random(41), 15 + Utility.Random(50), Utility.Random(1200), ".");
                }
            }
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;
            if (from == null)
                return;
            from.SendMessage("Invalid Response. Stop your Razor macro during AFK check!");
        }

        private string getchar(string passchar)
        {
            switch (passchar)
            {                   
                case "a":
                    {
                        switch (Utility.Random(4))
                        {
                            case 0:
                                return "\u0101";
                            case 1:
                                return "\u0103";
                            case 2:
                                return "\u0105";
                        }
                        break;
                    }

                case "A":
                    {
                        switch (Utility.Random(6))
                        {
                            case 0:
                                return "\u0100";
                            case 1:
                                return "\u0102";
                            case 2:
                                return "\u0104";
                            case 3:
                                return "\u00C4";
                            case 4:
                                return "\u00C5";
                        }
                        break;
                    }

                case "b":
                    {
                        switch (Utility.Random(2))
                        {
                            case 0:
                                return "\u00FE";
                        }
                        break;
                    }

                case "B":
                    {
                        switch (Utility.Random(3))
                        {
                            case 0:
                                return "\u00DF";
                            case 1:
                                return "\u03B2";
                        }
                        break;
                    }

                case "c":
                    {
                        switch (Utility.Random(5))
                        {
                            case 0:
                                return "\u0107";
                            case 1:
                                return "\u0109";
                            case 2:
                                return "\u010B";
                            case 3:
                                return "\u010D";
                        }
                        break;
                    }

                case "C":
                    {
                        switch (Utility.Random(5))
                        {
                            case 0:
                                return "\u0106";
                            case 1:
                                return "\u0108";
                            case 2:
                                return "\u010A";
                            case 3:
                                return "\u010C";
                        }
                        break;
                    }

                case "d":
                    {
                        switch (Utility.Random(3))
                        {
                            case 0:
                                return "\u010F";
                            case 1:
                                return "\u0111";
                        }
                        break;
                    }

                case "D":
                    {
                        switch (Utility.Random(3))
                        {
                            case 0:
                                return "\u010E";
                            case 1:
                                return "\u0110";
                        }
                        break;
                    }

                case "e":
                    {
                        switch (Utility.Random(6))
                        {
                            case 0:
                                return "\u0113";
                            case 1:
                                return "\u0115";
                            case 2:
                                return "\u0117";
                            case 3:
                                return "\u0119";
                            case 4:
                                return "\u011B";
                        }
                        break;
                    }

                case "E":
                    {
                        switch (Utility.Random(8))
                        {
                            case 0:
                                return "\u0112";
                            case 1:
                                return "\u0114";
                            case 2:
                                return "\u0116";
                            case 3:
                                return "\u0118";
                            case 4:
                                return "\u011A";
                            case 5:
                                return "\u00CB";
                            case 6:
                                return "\u0404";
                        }
                        break;
                    }

                case "g":
                    {
                        switch (Utility.Random(5))
                        {
                            case 0:
                                return "\u011D";
                            case 1:
                                return "\u011F";
                            case 2:
                                return "\u0121";
                            case 3:
                                return "\u0123";
                        }
                        break;
                    }

                case "G":
                    {
                        switch (Utility.Random(5))
                        {
                            case 0:
                                return "\u011C";
                            case 1:
                                return "\u011E";
                            case 2:
                                return "\u0120";
                            case 3:
                                return "\u0122";
                        }
                        break;
                    }

                case "h":
                    {
                        switch (Utility.Random(3))
                        {
                            case 0:
                                return "\u0125";
                            case 1:
                                return "\u0127";
                        }
                        break;
                    }

                case "H":
                    {
                        switch (Utility.Random(3))
                        {
                            case 0:
                                return "\u0124";
                            case 1:
                                return "\u0126";
                        }
                        break;
                    }

                case "j":
                    {
                        switch (Utility.Random(2))
                        {
                            case 0:
                                return "\u0135";
                        }
                        break;
                    }

                case "J":
                    {
                        switch (Utility.Random(2))
                        {
                            case 0:
                                return "\u0134";
                        }
                        break;
                    }

                case "k":
                    goto case "K";

                case "K":
                    {
                        switch (Utility.Random(4))
                        {
                            case 0:
                                return "\u0136";
                            case 1:
                                return "\u0137";
                            case 2:
                                return "\u0138";
                        }
                        break;
                    }

                case "n":
                    {
                        switch (Utility.Random(6))
                        {
                            case 0:
                                return "\u0144";
                            case 1:
                                return "\u0146";
                            case 2:
                                return "\u0148";
                            case 3:
                                return "\u0149";
                            case 4:
                                return "\u014B";
                        }
                        break;
                    }

                case "N":
                    {
                        switch (Utility.Random(5))
                        {
                            case 0:
                                return "\u0143";
                            case 1:
                                return "\u0145";
                            case 2:
                                return "\u0147";
                            case 3:
                                return "\u014A";
                        }
                        break;
                    }

                case "r":
                    {
                        switch (Utility.Random(5))
                        {
                            case 0:
                                return "\u0155";
                            case 1:
                                return "\u0157";
                            case 2:
                                return "\u0159";
                            case 3:
                                return "\u044F";
                        }
                        break;
                    }

                case "R":
                    {
                        switch (Utility.Random(5))
                        {
                            case 0:
                                return "\u0154";
                            case 1:
                                return "\u0156";
                            case 2:
                                return "\u0158";
                            case 3:
                                return "\u042F";
                        }
                        break;
                    }

                case "s":
                    {
                        switch (Utility.Random(5))
                        {
                            case 0:
                                return "\u015B";
                            case 1:
                                return "\u015D";
                            case 2:
                                return "\u015F";
                            case 3:
                                return "\u0161";
                        }
                        break;
                    }

                case "S":
                    {
                        switch (Utility.Random(5))
                        {
                            case 0:
                                return "\u015A";
                            case 1:
                                return "\u015C";
                            case 2:
                                return "\u015E";
                            case 3:
                                return "\u0160";
                        }
                        break;
                    }


                case "t":
                    {
                        switch (Utility.Random(5))
                        {
                            case 0:
                                return "\u0163";
                            case 1:
                                return "\u0165";
                            case 2:
                                return "\u0167";
                            case 3:
                                return "\u03C4";
                        }
                        break;
                    }
                case "T":
                    {
                        switch (Utility.Random(4))
                        {
                            case 0:
                                return "\u0162";
                            case 1:
                                return "\u0164";
                            case 2:
                                return "\u0166";
                        }
                        break;
                    }

                case "u":
                    {
                        switch (Utility.Random(7))
                        {
                            case 0:
                                return "\u0169";
                            case 1:
                                return "\u016B";
                            case 2:
                                return "\u016D";
                            case 3:
                                return "\u016F";
                            case 4:
                                return "\u0171";
                            case 5:
                                return "\u0173";
                        }
                        break;
                    }

                case "U":
                    {
                        switch (Utility.Random(8))
                        {
                            case 0:
                                return "\u0168";
                            case 1:
                                return "\u016A";
                            case 2:
                                return "\u016C";
                            case 3:
                                return "\u016E";
                            case 4:
                                return "\u0170";
                            case 5:
                                return "\u0172";
                            case 6:
                                return "\u00DC";
                        }
                        break;
                    }

                case "V":
                    {
                        switch (Utility.Random(2))
                        {
                            case 0:
                                return "\u0474";
                        }
                        break;
                    }

                case "w":
                    {
                        switch (Utility.Random(3))
                        {
                            case 0:
                                return "\u0175";
                            case 1:
                                return "\u03C9";
                        }
                        break;
                    }

                case "W":
                    {
                        switch (Utility.Random(2))
                        {
                            case 0:
                                return "\u0174";
                        }
                        break;
                    }

                case "X":
                    {
                        switch (Utility.Random(2))
                        {
                            case 0:
                                return "\u03C7";
                        }
                        break;
                    }

                case "y":
                    goto case "Y";

                case "Y":

                    {
                        switch (Utility.Random(4))
                        {
                            case 0:
                                return "\u0176";
                            case 1:
                                return "\u0177";
                            case 2:
                                return "\u0178";
                        }
                        break;
                    }

                case "z":
                    {
                        switch (Utility.Random(4))
                        {
                            case 0:
                                return "\u017A";
                            case 1:
                                return "\u017C";
                            case 2:
                                return "\u017E";
                        }
                        break;
                    }

                case "Z":
                    {
                        switch (Utility.Random(4))
                        {
                            case 0:
                                return "\u0179";
                            case 1:
                                return "\u017B";
                            case 2:
                                return "\u017D";
                        }
                        break;
                    }

                default:
                    return passchar;
            }
            return passchar;
        }
    }

    public class AFKCheckGump : Gump
    {
        private Mobile m_Staff;
        private Mobile m_From;
        private string m_userphrase;
        private byte m_RetryCount;
        private byte m_MaxRetries;
        private int m_TotalSeconds;
        private int m_MaxSeconds;
        private bool m_Jail;
        private bool m_Kick;
        private Point3D m_MoveTo;
        private Map m_MoveToMap;
        private string m_Website;
        private string m_passphrase;
        private int m_textID;
        private byte m_seconds;
        private GumpTimer m_GumpTimer;
        private KickTimer m_KickTimer;

        public AFKCheckGump(Mobile Staff, Mobile From, byte RetryCount, byte MaxRetries, int TotalSeconds, int MaxSeconds, string userphrase, bool Kick, bool Jail, Point3D MoveTo, Map MoveToMap, string Website, string passphrase)
            : base((RetryCount >= MaxRetries || TotalSeconds > MaxSeconds) ? 300 : 290, (RetryCount >= MaxRetries || TotalSeconds > MaxSeconds) ? 140 : 280)
        {
            m_Staff = Staff;
            m_From = From;
            m_RetryCount = RetryCount;
            m_MaxRetries = MaxRetries;
            m_TotalSeconds = TotalSeconds;
            m_MaxSeconds = MaxSeconds;
            m_Kick = Kick;
            m_Jail = Jail;
            m_MoveTo = MoveTo;
            m_MoveToMap = MoveToMap;
            m_Website = Website;
            m_userphrase = userphrase;
            m_passphrase = passphrase;
            Closable = RetryCount >= MaxRetries || TotalSeconds > MaxSeconds ? true : false;
            Disposable = true;
            Dragable = false;
            Resizable = false;

            if (m_RetryCount >= m_MaxRetries || m_TotalSeconds > m_MaxSeconds)
            {
                AddBackground(0, 0, 200, 60, 9200);
                AddLabel(45, 20, 1150, "Where were you?");
                UserAFK(m_From);
            }
            else
            {
                string keytext = "23456789abcdefghjkmnopqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ";
                string[] keychar = new string[5];
                m_textID = 100 + Utility.Random(1000);
                AddBackground(0, 0, 220, 57, 9260);
                for (int i = 0; i < 5; i++)
                {
                    keychar[i] = keytext.Substring(Utility.Random(keytext.Length), 1);
                    m_passphrase = m_passphrase + keychar[i];
                }
                AddImageTiled(17, 16, 190, 25, 2624);
                AddLabel(20, 18, 1150, "AFK Check:");
                AddImageTiled(92, 19, 60, 19, 0xBBC);
                AddTextEntry(92, 18, 60, 19, 1150, m_textID, userphrase);
                AddButton(157, 18, 2450, 2451, m_textID, GumpButtonType.Reply, 0);
                Effects.PlaySound(new Point3D(m_From.X, m_From.Y, m_From.Z), m_From.Map, 0x307);
                m_From.SendMessage(36, "***** AFK Check! *****");
                m_seconds = 0;
                ActivatePassphraseTimer(m_From, m_passphrase, false);
                m_From.CloseGump(typeof(AFKCPassPhraseGump));
                m_From.SendGump(new AFKCPassPhraseGump(m_passphrase));
            }
        }

        public void ActivatePassphraseTimer(Mobile m_From, string m_passphrase, bool incseconds)
        {
            if (incseconds)
            {
                m_TotalSeconds++;
                m_seconds++;
                if ((m_MaxSeconds - m_TotalSeconds) < 10 && m_seconds > 10 && m_From != null)
                    m_From.PlaySound(976);
            }
            m_GumpTimer = new GumpTimer(m_Staff, m_From, m_RetryCount, m_MaxRetries, m_TotalSeconds, m_MaxSeconds, m_userphrase, m_Kick, m_Jail, m_MoveTo, m_MoveToMap, m_Website, m_passphrase, this, m_seconds, TimeSpan.FromSeconds(1));
            m_GumpTimer.Start();
        }

        private void UserThere(Mobile m)
        {
            if (m == null)
                return;
            m.Say("I'm Here!");
            m.SendMessage(71, "Thank you for being here!");
            Account mAccount = m.Account as Account;
            if (mAccount != null)
            {
                CommandLogging.WriteLine(m, "{0} {1} {2} ", m.AccessLevel, CommandLogging.Format(m), "Passed AFK Check.");
                string text = String.Format("{0} AFK Check-{1}: Passed! Retries={2}/{3} Seconds={4}/{5}", DateTime.Now, m.Name,m_RetryCount, m_MaxRetries, m_TotalSeconds, m_MaxSeconds);
                mAccount.Comments.Add(new AccountComment("AFK Checker", text));
                if (m_Staff != null && !m_Staff.Deleted && m_Staff.Map != Map.Internal)
                    m_Staff.SendMessage(66, "*{0} Passed AFK Check*", m.Name);
            }
        }

        private void UserAFK(Mobile m)
        {
            if (m == null)
                return;
            m.CloseGump(typeof(AFKCPassPhraseGump));
            m.Say("I'm AFK!");
            Account mAccount = m.Account as Account;
            if (mAccount != null)
            {
                CommandLogging.WriteLine(m, "{0} {1} {2} ", m.AccessLevel, CommandLogging.Format(m), "Failed AFK Check.");
                string text = String.Format("{0} AFK Check-{1}: **Failed** Retries={2}/{3} Seconds={4}/{5}", DateTime.Now, m.Name, m_RetryCount, m_MaxRetries, m_TotalSeconds, m_MaxSeconds);
                mAccount.Comments.Add(new AccountComment("AFK Checker", text));
                if (m_Jail)
                    MoveToJail(m);
                else if (m_MoveToMap != null && m_MoveTo != Point3D.Zero)
                {
                    m.MoveToWorld(m_MoveTo, m_MoveToMap);
                    Effects.SendBoltEffect(m, true, 0);
                    Effects.PlaySound(new Point3D(m.X, m.Y, m.Z), m.Map, 0x307);
                }
                if (m_Website != null)
                {
                    try { m.LaunchBrowser(m_Website); }
                    catch { }
                }
                if (m_Kick)
                {
                    m_KickTimer = new KickTimer(m, TimeSpan.FromSeconds(5));
                    m_KickTimer.Start();
                }
                if (m_Staff != null && !m_Staff.Deleted && m_Staff.Map != Map.Internal)
                    m_Staff.SendMessage(37, "*{0} FAILED AFK Check*", m.Name);
            }
        }

        public void MoveToJail(Mobile m)
        {
            m.MoveToWorld(new Point3D(5301, 1184, 0), Map.Felucca);
            Effects.SendBoltEffect(m, true, 0);
            Effects.PlaySound(new Point3D(m.X, m.Y, m.Z), m.Map, 0x307);
            CommandLogging.WriteLine(m, "{0} {1} {2} ", m.AccessLevel, CommandLogging.Format(m), "Jailed.");
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;
            if (m_GumpTimer != null)
                m_GumpTimer.Stop();
            if (from == null)
                return;
            from.CloseGump(typeof(AFKCPassPhraseGump));
            if (Closable && info.ButtonID == 0)
            {
                from.CloseGump(typeof(AFKCheckGump));
                return;
            }
            else if (info.ButtonID == m_textID)
            {
                if (m_RetryCount < m_MaxRetries)
                {
                    TextRelay tr = info.GetTextEntry(m_textID);
                    if (tr != null && tr.Text.ToUpper() == m_passphrase.ToUpper())
                    {
                        UserThere(from);
                        from.CloseGump(typeof(AFKCPassPhraseGump));
                        return;
                    }
                    else
                    {
                        m_passphrase = null;
                        from.SendMessage("Incorrect, try again.");
                    }
                    m_RetryCount++;
                    from.SendGump(new AFKCheckGump(m_Staff, m_From, m_RetryCount, m_MaxRetries, m_TotalSeconds, m_MaxSeconds, m_userphrase, m_Kick, m_Jail, m_MoveTo, m_MoveToMap, m_Website, m_passphrase));
                }
            }
            else
            {
                if (m_RetryCount < m_MaxRetries)
                {
                    if (Utility.RandomDouble() < .035) // Give a random retry count to prevent an endless passphrase loop
                    {
                        m_RetryCount++;
                        if (m_Staff != null && !m_Staff.Deleted && m_Staff.Map != Map.Internal)
                            m_Staff.SendMessage(1151, "AFKCheckGump: {0} using Razor/Macro gump packets, retries={1}.", from.Name, m_RetryCount);
                    }
                    from.SendMessage("Invalid Button. Stop your Razor macro during AFK check!");
                    from.SendGump(new AFKCheckGump(m_Staff, m_From, m_RetryCount, m_MaxRetries, m_TotalSeconds, m_MaxSeconds, m_userphrase, m_Kick, m_Jail, m_MoveTo, m_MoveToMap, m_Website, m_passphrase));
                }
            }
        }

        private class KickTimer : Timer
        {
            private Mobile m;
           
            public KickTimer(Mobile mobile, TimeSpan delay)
                : base(delay)
            {
                Priority = TimerPriority.OneSecond;
                m = mobile;
            }

            protected override void OnTick()
            {
                if (m.Map != Map.Internal)
                {
                    NetState ns = m.NetState;
                    if (ns != null)
                    {
                        Account mAccount = ns.Account as Account;
                        if (mAccount != null)
                        {
                            CommandLogging.WriteLine(m, "{0} {1} {2} ", m.AccessLevel, CommandLogging.Format(m), "Kicked.");
                            m.Say("I've been kicked for unattended macroing!");
                            ns.Dispose();
                        }
                    }
                }
            }
        }

        private class GumpTimer : Timer
        {
            private Mobile m_Staff;
            private Mobile m_From;
            private Gump m_afkgump;
            private string m_userphrase;
            private byte m_RetryCount;
            private byte m_MaxRetries;
            private int m_TotalSeconds;
            private int m_MaxSeconds;
            private bool m_Jail;
            private bool m_Kick;
            private Point3D m_MoveTo;
            private Map m_MoveToMap;
            private string m_Website;
            private string m_passphrase;
            private byte m_seconds;

            public GumpTimer(Mobile Staff, Mobile From, byte RetryCount, byte MaxRetries, int TotalSeconds, int MaxSeconds, string userphrase, bool Kick, bool Jail, Point3D MoveTo, Map MoveToMap, string Website, string passphrase, Gump afkgump, byte seconds, TimeSpan delay)
                : base(delay)
            {
                Priority = TimerPriority.OneSecond;
                m_Staff = Staff;
                m_From = From;
                m_afkgump = afkgump;
                m_RetryCount = RetryCount;
                m_MaxRetries = MaxRetries;
                m_TotalSeconds = TotalSeconds;
                m_MaxSeconds = MaxSeconds;
                m_Kick = Kick;
                m_Jail = Jail;
                m_MoveTo = MoveTo;
                m_MoveToMap = MoveToMap;
                m_Website = Website;
                m_passphrase = passphrase;
                m_seconds = seconds;
            }

            protected override void OnTick()
            {
                if (m_seconds < 20)
                {
                    m_From.CloseGump(typeof(AFKCPassPhraseGump));
                    m_From.SendGump(new AFKCPassPhraseGump(m_passphrase));
                    ((AFKCheckGump)m_afkgump).ActivatePassphraseTimer(m_From, m_passphrase, true);
                }
                else
                {
                    m_From.CloseGump(typeof(AFKCPassPhraseGump));
                    m_From.CloseGump(typeof(AFKCheckGump));
                    m_From.SendGump(new AFKCheckGump(m_Staff, m_From, m_RetryCount, m_MaxRetries, m_TotalSeconds, m_MaxSeconds, m_userphrase, m_Kick, m_Jail, m_MoveTo, m_MoveToMap, m_Website, null));
                }
            }
        }
    }

    public class AFKInfoGump : Gump
    {
        private const int MaxDistractors = 5;

        public AFKInfoGump(Mobile m)
            : base(275, 200)
        {
            Closable = true;
            Disposable = true;
            Dragable = true;
            Resizable = false;
            AddBackground(0, 0, 600, 270, 9200);
            string text = String.Format("Character: {0}-({1})", m.Name, m.Account);
            AddLabel(20, 5, 1150, @text);
            AddLabel(250, 5, 1150, "Online:");
			NetState ns = m.NetState;
			TimeSpan TimeOnline = TimeSpan.Zero;
			if (ns != null)
			{
				TimeOnline = DateTime.Now - ((PlayerMobile)m).SessionStart;
				text = String.Format("{0:g} - ({1:##:##:##.##})", ((PlayerMobile)m).SessionStart, TimeOnline);
			}
			else
			{
				text = String.Format("This player is currently offline.");
			}
            AddLabel(290, 5, TimeOnline > TimeSpan.FromHours(2) ? (TimeOnline > TimeSpan.FromHours(4) ? 37 : 51) : 71, @text);
            bool capped = m.SkillsTotal >= m.SkillsCap;
            text = String.Format("Skill Total/Cap: {0}/{1}", m.SkillsTotal, m.SkillsCap);
            AddLabel(20, 20, capped ? 37 : 71, @text);
            text = String.Format("Mining: {0}-{1}", m.Skills[SkillName.Mining].Base, m.Skills[SkillName.Mining].Lock);
            AddLabel(20, 35, SkillCheck(m, capped, SkillName.Mining), @text);
            text = String.Format("Lumberjacking: {0}-{1}",m.Skills[SkillName.Lumberjacking].Base, m.Skills[SkillName.Lumberjacking].Lock);
            AddLabel(150, 35, SkillCheck(m, capped, SkillName.Lumberjacking), @text);
            text = String.Format("Lockpicking: {0}-{1}", m.Skills[SkillName.Lockpicking].Base, m.Skills[SkillName.Lockpicking].Lock);
            AddLabel(320, 35, SkillCheck(m, capped, SkillName.Lockpicking), @text);
            text = String.Format("Fishing: {0}-{1}", m.Skills[SkillName.Fishing].Base, m.Skills[SkillName.Fishing].Lock);
            AddLabel(470, 35, SkillCheck(m, capped, SkillName.Fishing), @text);
 
            StringBuilder sb = new StringBuilder();
            Account a = m.Account as Account;
            if (a != null)
            {
                int Failed = 0;
                int Passed = 0;
                for (int i = 0; i < a.Comments.Count; ++i)
                {
                    AccountComment c = (AccountComment)a.Comments[i];
                    if (Insensitive.Contains(c.Content, "AFK Check"))
                    {
                        sb.AppendFormat("{0}<BR>", c.Content);
                        if (Insensitive.Contains(c.Content, "Failed"))
                            Failed++;
                        if (Insensitive.Contains(c.Content, "Passed"))
                            Passed++;
                    }
                }

                AddHtml(10, 55, 585, 170, sb.ToString(), true, true);
                text = String.Format("Total AFK Checks: {0}",Failed + Passed);
                AddLabel(20, 230, 1150, text );
                text = String.Format("Failed={0}", Failed);
                AddLabel(170, 230, (Failed > 0) ? 37 : 1150, @text);
                text = String.Format("Passed={0}", Passed);
                AddLabel(240, 230, (Passed > 0) ? 71 : 1150, @text);
                string LastAFKCheck = a.GetTag("LastAFKCheck");
                string LastAFKCheckBy = a.GetTag("LastAFKCheckBy");
                if (LastAFKCheck != null && LastAFKCheckBy != null)
                {
                    DateTime d_LastAFKCheck = DateTime.Now;
                    try
                    {
                        d_LastAFKCheck = DateTime.Parse(LastAFKCheck);
                    }
                    catch { };
                    if (DateTime.Now - d_LastAFKCheck < TimeSpan.FromHours(4))
                        AddLabel(445, 245, 3, "** Under 4 Hours **");
                    text = String.Format("Last Check: {0} by {1}.", LastAFKCheck, LastAFKCheckBy);
                }
                else
                    text = String.Format("Last Check: Never");
                AddLabel(20, 245, 1150, @text);
            }
        }

        private int SkillCheck(Mobile m, bool capped, SkillName skill)
        {
            if (m.Skills[skill].Lock == SkillLock.Locked)
                return 37;
            if (m.Skills[skill].Base >= m.Skills[skill].Cap)
                return 37;
            if (capped && m.Skills[skill].Lock == SkillLock.Up)
                return 51;
            return 71;
        }
    }
}

#if RUNUO20
namespace Server.Commands
#else
namespace Server.Scripts.Commands
#endif
{
	public class AFKInfoCom
	{
		public static void Initialize()
		{
#if RUNUO20
			CommandSystem.Register("AFKInfo", AccessLevel.Counselor, new CommandEventHandler(AFKInfo_OnCommand));
			CommandSystem.Register("AFKI", AccessLevel.Counselor, new CommandEventHandler(AFKInfo_OnCommand));
#else
			Server.Commands.Register("AFKInfo", AccessLevel.Counselor, new CommandEventHandler(AFKInfo_OnCommand));
			Server.Commands.Register("AFKI", AccessLevel.Counselor, new CommandEventHandler(AFKInfo_OnCommand));
#endif
		}

		[Usage("afki or afkinfo {target player}")]
		[Description("Gets player information and history of past AFK checks.")]
		public static void AFKInfo_OnCommand(CommandEventArgs e)
		{
			Mobile m = e.Mobile;
			m.SendMessage("Target player now.");
			m.Target = new InternalTarget();
		}

		private class InternalTarget : Target
		{
			public InternalTarget()
				: base(12, false, TargetFlags.None)
			{
				this.CheckLOS = true;
				this.AllowGround = false;
				this.Range = 15;
			}

			protected override void OnTarget(Mobile from, object o)
			{
				if (!(o is PlayerMobile) || o == null)
				{
					from.SendMessage("Invalid target, must be a player.");
				}
				else
				{
					Mobile t = (Mobile)o;
					Account acct = t.Account as Account;
					if (t.Deleted || t.Map == Map.Internal)
						from.SendMessage("Invalid target, must be an online player.");
					else if (acct == null)
						from.SendMessage("Invalid target, Account property = null!");
					else
						from.SendGump(new AFKInfoGump(t));
				}
			}
		}
	}
}