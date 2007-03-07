/*******************************************
 *               Time System
 * 
 * Version        : 2.0.0
 * By             : Morxeton
 * 
 * Date Created   : February 8, 2006
 * Date Modified  : March 5, 2007
 * 
 *******************************************/

using System;
using Server;
using Server.Mobiles;
using Server.Network;

namespace Server.TimeSystem
{
    public class Engine
    {
        #region Initialize

        public static void Initialize()
        {
            EventSink.Login += new LoginEventHandler(OnLogin);
            EventSink.Disconnected += new DisconnectedEventHandler(OnDisconnected);
            EventSink.WorldSave += new WorldSaveEventHandler(OnWorldSave);

            new DelayedInitializationTimer().Start();
        }

        private static void InitializeSystem()
        {
            Data.Loading = true;

            Console.WriteLine(String.Format("Time System: Version {0} loading...", Data.Version));

            Support.OpenLogFile();

            SelfInstaller.Install();

            LightsEngine.PopulateLightsList();

            Data.Load();

            Support.CheckForceScriptSettings();

            Support.UpdateCodeChars();

            Start();

            Support.ConsoleWriteLine(String.Format("Time System: {0}", Formatting.GetTimeFormat(null, Format.Time)));

            Data.Loading = false;
        }

        #endregion

        #region Private Variables

        private static TimeSystemTimer m_TimeSystemTimer;

        #endregion

        #region Event Sinks

        public static void OnLogin(LoginEventArgs args)
        {
            Mobile mobile = args.Mobile;

            MobileObject mo = new MobileObject();

            mo.Mobile = mobile;

            mo.IsNightSightOn = !mobile.CanBeginAction(typeof(LightCycle));

            Data.MobilesTable.Add(mobile, mo);
        }

        public static void OnDisconnected(DisconnectedEventArgs args)
        {
            Mobile mobile = args.Mobile;

            Data.MobilesTable.Remove(mobile);
        }

        public static void OnWorldSave(WorldSaveEventArgs e)
        {
            new DelayedSaveTimer().Start();
        }

        #endregion

        #region System Control

        public static void Stop()
        {
            Data.Enabled = false;

            if (m_TimeSystemTimer != null)
            {
                m_TimeSystemTimer.Stop();
            }
        }

        public static void Start()
        {
            Data.Enabled = true;

            m_TimeSystemTimer = new TimeSystemTimer();
            m_TimeSystemTimer.Start();
        }

        public static void Restart()
        {
            if (Data.Enabled)
            {
                Stop();
                Start();
            }
        }

        #endregion

        #region Calculation Methods

        private static void OnSystemTick()
        {
            TimeEngine.CalculateBaseTime();
            LightsEngine.CheckLights();

            foreach (MobileObject mo in Data.MobilesTable.Values)
            {
                if (mo != null)
                {
                    mo.Mobile.CheckLightLevels(false);
                }
            }
        }

        #endregion

        #region Delayed Initialization Timer

        private class DelayedInitializationTimer : Timer
        {
            public DelayedInitializationTimer()
                : base(TimeSpan.FromSeconds(0.1))
            {
            }

            protected override void OnTick()
            {
                InitializeSystem();
            }
        }

        #endregion

        #region Time System Timer

        private class TimeSystemTimer : Timer
        {
            public TimeSystemTimer()
                : base(TimeSpan.FromSeconds(1.0), TimeSpan.FromSeconds(Data.TimerSpeed))
            {
            }

            protected override void OnTick()
            {
                OnSystemTick();
            }
        }

        #endregion

        #region Delayed Save Timer

        private class DelayedSaveTimer : Timer
        {
            public DelayedSaveTimer()
                : base(TimeSpan.FromSeconds(0.5))
            {
            }

            protected override void OnTick()
            {
                Data.Save();
            }
        }

        #endregion
    }
}
