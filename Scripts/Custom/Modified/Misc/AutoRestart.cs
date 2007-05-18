using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Server;
using Server.Commands;

namespace Server.Misc
{
    public class AutoRestart : Timer
    {
        //† Autorestart config †//
        public static bool Enabled { get { return true; } }  // Is autorestart enabled?
        private static TimeSpan AutoRestartWarning = TimeSpan.FromSeconds(60.0); // Warning before autorestart
        private static TimeSpan AutoRestartInterval = TimeSpan.Parse("00:00:00"); // Iteration every 24 hours.
        private static bool UseAtTime = true; // Switching to false will count AutoRestartInterval as In.
        //‡ Autorestart config ‡//

        //† Restart config †//
        //private static TimeSpan WarningDelay = TimeSpan.Zero; // Disabled Warning Message
        private static TimeSpan WarningDelay = TimeSpan.FromSeconds(10.0); // Warning message interval
        //‡ Restart config ‡//

        private static bool m_ServerWars;
        private static bool m_Restarting;
        private static DateTime m_RestartTime;

        public static bool Restarting { get { return m_Restarting; } }
        public static bool ServerWars { get { return m_ServerWars; } }

        public static void Initialize()
        {
            CommandSystem.Register("Restart", AccessLevel.GameMaster, new CommandEventHandler(Restart_OnCommand));
            new AutoRestart();
        }

        [Usage("restart [delaytime]")]
        [Description("Allows you to restart server with specified delay.  Disable countdown with parameter 0.")]
        public static void Restart_OnCommand(CommandEventArgs e)
        {
            if (!m_Restarting && e.ArgString != "")
            {
                switch (e.ArgString.ToLower())
                {
                    case "0":
                        e.Mobile.SendMessage("No restart in progress.");
                        break;
                    case "sw":
                        e.Mobile.SendMessage("No restart in progress, you are unable to run Server Wars.");
                        break;
                    case "nosave":
                        Process.Start(Core.ExePath, Core.Arguments);
                        Core.Process.Kill();
                        break;
                    case "info":
                        e.Mobile.SendMessage("AutoRestart is {0}.", Enabled ? "enabled" : "disabled");
                        if (Enabled)
                        {
                            e.Mobile.SendMessage("Current Time is {0}.", DateTime.Now);
                            e.Mobile.SendMessage("Auto Restart at {0}.", m_RestartTime);
                        }
                        break;
                    default:
                        if (TimeSpan.FromSeconds(e.GetDouble(0)) <= TimeSpan.FromSeconds(86399))
                        {
                            Timer Warning;
                            m_RestartTime = DateTime.Now + TimeSpan.FromSeconds(e.GetDouble(0));
                            World.Broadcast(0x22, true, "{0} has initiated server shutdown in {1}.", e.Mobile.Name, TimeSpan.FromSeconds(e.GetDouble(0)));
                            m_Restarting = true;
                            if (WarningDelay > TimeSpan.Zero)
                                Warning = new WarningTimer(WarningDelay);
                        }
                        else
                            e.Mobile.SendMessage("Value is too high. You can set up to 86399 = 23:59:59");
                        break;
                }
            }
            else if (m_Restarting && e.ArgString != "")
            {
                switch (e.ArgString.ToLower())
                {
                    case "0":
                        if (!m_ServerWars)
                        {
                            m_Restarting = false;
                            if (UseAtTime)
                                m_RestartTime = DateTime.Now.Date + AutoRestartInterval;
                            else
                                m_RestartTime = DateTime.Now + AutoRestartInterval;
                            World.Broadcast(0x22, true, "Server restart terminated by {0}!", e.Mobile.Name);
                        }
                        else
                        {
                            e.Mobile.SendMessage("Server Wars running, unable to terminate.");
                        }
                        break;
                    case "sw":
                        if (!m_ServerWars)
                        {
                            m_ServerWars = true;
                            World.Broadcast(0x22, true, "Server Wars time! If you are not sure what is it read webpage for more informations!", e.Mobile.Name);
                            break;
                        }
                        goto default;

                    default:
                        {
                            e.Mobile.SendMessage(m_ServerWars ? "The Server wars already running." : "The server already restarting.");
                        }
                        break;
                }
            }
            else
            {
                if (!m_Restarting)
                {
                    World.Broadcast(0x22, true, "Server restarting. Will be back up in few minutes.");
                    AutoSave.Save();
                    Process.Start(Core.ExePath, Core.Arguments);
                    Core.Process.Kill();
                }
                else
                    e.Mobile.SendMessage(m_ServerWars ? "The Server wars already running." : "The server already restarting.");
            }
        }

        public AutoRestart()
            : base(TimeSpan.Zero, TimeSpan.FromSeconds(1.0))
        {
            Priority = TimerPriority.OneSecond;
            if (UseAtTime)
            {
                m_RestartTime = DateTime.Now.Date + AutoRestartInterval;
                if (m_RestartTime < DateTime.Now)
                    m_RestartTime += TimeSpan.FromDays(1.0);
            }
            else
                m_RestartTime = DateTime.Now + AutoRestartInterval;

            Start();
        }

        private void Restart()
        {
            Process.Start(Core.ExePath, Core.Arguments);
            Core.Process.Kill();
        }

        private void Restart_Callback()
        {
            if (!m_ServerWars)
            {
                World.Broadcast(0x22, true, "Server restarting. Will be back up in few minutes.");
                AutoSave.Save();
                Timer.DelayCall(TimeSpan.FromSeconds(5.0), new TimerCallback(Restart));
            }
            else
            {
                World.Broadcast(0x22, true, "Server Wars ended and server restarting. Will be up in few seconds.");
                Timer.DelayCall(TimeSpan.FromSeconds(5.0), new TimerCallback(Restart));
            }
        }

        protected override void OnTick()
        {
            if (Enabled && DateTime.Now >= m_RestartTime.Subtract(AutoRestartWarning) && DateTime.Now <= m_RestartTime.Subtract(AutoRestartWarning + TimeSpan.FromSeconds(-2.0)) && !m_Restarting)
            {
                if (AutoRestartWarning > TimeSpan.Zero)
                {
                    if (UseAtTime)
                    {
                        DateTime result = DateTime.Now.Date.AddDays(1.0) + AutoRestartInterval;
                        World.Broadcast(0x22, true, "Server automatic restart initiated. Expect next at {0}!", result);
                    }
                    else
                    {
                        DateTime result = DateTime.Now + AutoRestartInterval;
                        World.Broadcast(0x22, true, "Server automatic restart initiated. Expect next in around {0}!", result);
                    }

                }
                Timer.DelayCall(AutoRestartWarning, new TimerCallback(Restart_Callback));
                Stop();
                return;
            }

            if (DateTime.Now < m_RestartTime || !m_Restarting)
                return;

            Stop();
            Restart_Callback();
        }

        protected class WarningTimer : Timer
        {
            private DateTime result = m_RestartTime.Subtract(TimeSpan.Parse(DateTime.Now.ToLongTimeString()));

            public WarningTimer(TimeSpan delay)
                : base(delay, delay)
            {
                Start();
                Priority = TimerPriority.TenMS;
            }

            protected override void OnTick()
            {
                if (WarningDelay < m_RestartTime.Subtract(DateTime.Now) && m_Restarting)
                {
                    result -= WarningDelay;
                    World.Broadcast(0x22, true, m_ServerWars ? "ServerWars are going to end in {0}!" : "Server is going to restart in {0}!", result.ToString("HH:MM:ss"));
                }
                else
                    Stop();
            }
        }
    }
}