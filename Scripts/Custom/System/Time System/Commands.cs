using System;
using System.Collections.Generic;
using System.Text;
using Server;
using Server.Commands;
using Server.Mobiles;

namespace Server.TimeSystem
{
    public class Commands
    {
        #region Initialize

        public static void Initialize()
        {
            CommandSystem.Register(TSCommand, AccessLevel.GameMaster, new CommandEventHandler(TimeSystem_OnCommand));
            CommandSystem.Register(BaseTimeCommand, AccessLevel.GameMaster, new CommandEventHandler(BaseTime_OnCommand));
            CommandSystem.Register(TimeCommand, AccessLevel.Player, new CommandEventHandler(Time_OnCommand));
            CommandSystem.Register(CompareTimeCommand, AccessLevel.GameMaster, new CommandEventHandler(CompareTime_OnCommand));
            CommandSystem.Register(XTimeCommand, AccessLevel.GameMaster, new CommandEventHandler(XTime_OnCommand));
            CommandSystem.Register(SeasonCommand, AccessLevel.GameMaster, new CommandEventHandler(Season_OnCommand));
        }

        #endregion

        #region Constant Variables

        public const string TSCommand = "TS";
        public const string BaseTimeCommand = "BaseTime";
        public const string TimeCommand = "Time";
        public const string CompareTimeCommand = "CompareTime";
        public const string XTimeCommand = "XTime";
        public const string SeasonCommand = "Season";

        #endregion

        #region Game Commands

        [Usage("TS <command> <parameters>")]
        [Description("Issues commands into the Time System.")]
        private static void TimeSystem_OnCommand(CommandEventArgs e)
        {
            Mobile mobile = e.Mobile;

            VariableObject vo = null;

            Support.WriteToLogFile(e);

            if (e.Length >= 1)
            {
                Command command = Support.GetCommandFromName(e.GetString(0));

                if (e.Length == 2 && e.GetString(1) == "?")
                {
                    mobile.SendMessage(Syntax.GetSyntax(false, command));

                    return;
                }

                switch (command)
                {
                    case Command.Set:
                        {
                            Config.SetVariable(mobile, e, false);

                            break;
                        }
                    case Command.Get:
                        {
                            if (e.Length == 1)
                            {
                                Support.SendVariableNames(mobile);
                            }
                            else if (e.Length == 2)
                            {
                                string variableName = e.GetString(1).ToUpper();

                                vo = Config.GetVariable(variableName);

                                mobile.SendMessage(vo.Message);
                            }
                            else
                            {
                                mobile.SendMessage(Syntax.GetSyntax(true, command));
                            }

                            break;
                        }
                    case Command.Append:
                        {
                            Config.SetVariable(mobile, e, true);

                            break;
                        }
                    case Command.RepopLightsList:
                        {
                            if (e.Length == 1)
                            {
                                LightsEngine.PopulateLightsList();

                                mobile.SendMessage("The managed lights list has been repopulated.");
                            }
                            else
                            {
                                mobile.SendMessage(Syntax.GetSyntax(true, command));
                            }

                            break;
                        }
                    case Command.Stop:
                        {
                            if (e.Length == 1)
                            {
                                Engine.Stop();

                                mobile.SendMessage("The time system has been stopped.");
                            }
                            else
                            {
                                mobile.SendMessage(Syntax.GetSyntax(true, command));
                            }

                            break;
                        }
                    case Command.Start:
                        {
                            if (e.Length == 1)
                            {
                                Engine.Start();

                                mobile.SendMessage("The time system has been started.");
                            }
                            else
                            {
                                mobile.SendMessage(Syntax.GetSyntax(true, command));
                            }

                            break;
                        }
                    case Command.Restart:
                        {
                            if (e.Length == 1)
                            {
                                if (Data.Enabled)
                                {
                                    Engine.Restart();

                                    mobile.SendMessage("The time system has been restarted.");
                                }
                                else
                                {
                                    mobile.SendMessage(String.Format("The time system has been stopped.  To start it again, please type {0}{1} {2}", CommandSystem.Prefix, TSCommand, Command.Start.ToString().ToUpper()));
                                }
                            }
                            else
                            {
                                mobile.SendMessage(Syntax.GetSyntax(true, command));
                            }

                            break;
                        }
                    case Command.Load:
                        {
                            if (e.Length == 1)
                            {
                                if (Data.Load())
                                {
                                    mobile.SendMessage("The time system has been successfully loaded from file.");

                                    Engine.Restart();
                                }
                                else
                                {
                                    mobile.SendMessage("The time system has failed to load from file!");
                                }
                            }
                            else
                            {
                                mobile.SendMessage(Syntax.GetSyntax(true, command));
                            }

                            break;
                        }
                    case Command.Save:
                        {
                            if (e.Length == 1)
                            {
                                if (Data.Save())
                                {
                                    mobile.SendMessage("The time system has been successfully saved to file.");
                                }
                                else
                                {
                                    mobile.SendMessage("The time system has failed to save to file!");
                                }
                            }
                            else
                            {
                                mobile.SendMessage(Syntax.GetSyntax(true, command));
                            }

                            break;
                        }
                    case Command.SetTime:
                        {
                            bool success = false;

                            if (e.Length == 2)
                            {
                                string value = e.GetString(1);

                                string[] timeSplit = value.Split(':');

                                if (timeSplit.Length == 2)
                                {
                                    VariableObject hourObject = new VariableObject();
                                    VariableObject minuteObject = new VariableObject();

                                    string hour = timeSplit[0];
                                    string minute = timeSplit[1];

                                    hourObject = Config.SetVariable("hour", hour, false, false);
                                    minuteObject = Config.SetVariable("minute", minute, false, false);

                                    mobile.SendMessage(hourObject.Message);
                                    mobile.SendMessage(minuteObject.Message);

                                    if (hourObject.Success || minuteObject.Success)
                                    {
                                        success = true;

                                        Engine.Restart();
                                    }
                                }
                            }

                            if (!success)
                            {
                                mobile.SendMessage(Syntax.GetSyntax(true, command));
                            }

                            break;
                        }
                    case Command.Query:
                        {
                            if (e.Length == 1)
                            {
                                if (Data.Enabled)
                                {
                                    int gameMinutes = Data.MinutesPerTick;
                                    double perSeconds = Data.TimerSpeed;

                                    mobile.SendMessage(String.Format("The time system is running at {0} game minute{1} every {2} real second{3}.", gameMinutes, gameMinutes == 1 ? "" : "s", perSeconds, perSeconds == 1.0 ? "" : "s"));
                                }
                                else
                                {
                                    mobile.SendMessage("The time system is not running.");
                                }
                            }
                            else
                            {
                                mobile.SendMessage(Syntax.GetSyntax(true, command));
                            }

                            break;
                        }
                    case Command.Version:
                        {
                            if (e.Length == 1)
                            {
                                mobile.SendMessage(String.Format("The time system version is {0}.", Data.Version));
                            }
                            else
                            {
                                mobile.SendMessage(Syntax.GetSyntax(true, command));
                            }

                            break;
                        }
                    case Command.ConvertLampPosts:
                        {
                            if (e.Length == 2)
                            {
                                string cmd = e.GetString(1).ToLower();

                                switch (cmd)
                                {
                                    case "all": { Support.ConvertLampPosts(mobile); break; }
                                    case "area": { BoundingBoxPicker.Begin(mobile, new BoundingBoxCallback(Support.ConvertLampPosts_Callback), null); break; }
                                    default: { mobile.SendMessage(Syntax.GetSyntax(true, command)); break; }
                                }
                            }
                            else if (e.Length == 1)
                            {
                                Support.ConvertLampPost(mobile);
                            }
                            else
                            {
                                mobile.SendMessage(Syntax.GetSyntax(true, command));
                            }

                            break;
                        }
                    case Command.AddMonth:
                        {
                            if (e.Length == 3)
                            {
                                string monthName = e.GetString(1);
                                string monthDays = e.GetString(2);

                                vo = Custom.AddMonth(monthName, monthDays);

                                mobile.SendMessage(vo.Message);
                            }
                            else
                            {
                                mobile.SendMessage(Syntax.GetSyntax(true, command));
                            }

                            break;
                        }
                    case Command.InsertMonth:
                        {
                            if (e.Length == 4)
                            {
                                string index = e.GetString(1);
                                string monthName = e.GetString(2);
                                string monthDays = e.GetString(3);

                                vo = Custom.InsertMonth(index, monthName, monthDays);

                                mobile.SendMessage(vo.Message);
                            }
                            else
                            {
                                mobile.SendMessage(Syntax.GetSyntax(true, command));
                            }

                            break;
                        }
                    case Command.SetMonth:
                        {
                            if (e.Length == 4)
                            {
                                string index = e.GetString(1);
                                string monthName = e.GetString(2);
                                string monthDays = e.GetString(3);

                                vo = Custom.SetMonth(index, monthName, monthDays);

                                mobile.SendMessage(vo.Message);
                            }
                            else if (e.Length == 2)
                            {
                                string arg = e.GetString(1).ToLower();

                                if (arg == "defaults")
                                {
                                    Config.SetDefaultMonths();

                                    mobile.SendMessage("All custom month's have been set to defaults.");
                                }
                                else
                                {
                                    mobile.SendMessage(Syntax.GetSyntax(true, command));
                                }
                            }
                            else
                            {
                                mobile.SendMessage(Syntax.GetSyntax(true, command));
                            }

                            break;
                        }
                    case Command.GetMonth:
                        {
                            if (e.Length == 2)
                            {
                                string index = e.GetString(1);

                                vo = Custom.GetMonth(index);

                                mobile.SendMessage(vo.Message);
                            }
                            else
                            {
                                mobile.SendMessage(Syntax.GetSyntax(true, command));
                            }

                            break;
                        }
                    case Command.RemoveMonth:
                        {
                            if (e.Length == 2)
                            {
                                string index = e.GetString(1);

                                vo = Custom.RemoveMonth(index);

                                mobile.SendMessage(vo.Message);
                            }
                            else
                            {
                                mobile.SendMessage(Syntax.GetSyntax(true, command));
                            }

                            break;
                        }
                    case Command.SetMonthProps:
                        {
                            if (e.Length == 3)
                            {
                                string index = e.GetString(1);
                                string monthDays = e.GetString(2);

                                vo = Custom.SetMonthProps(index, monthDays);

                                mobile.SendMessage(vo.Message);
                            }
                            else
                            {
                                mobile.SendMessage(Syntax.GetSyntax(true, command));
                            }

                            break;
                        }
                    case Command.ClearMonths:
                        {
                            if (e.Length == 1)
                            {
                                Data.MonthsArray = new List<MonthPropsObject>();

                                mobile.SendMessage("All custom months have been cleared.");
                            }
                            else
                            {
                                mobile.SendMessage(Syntax.GetSyntax(true, command));
                            }

                            break;
                        }
                    case Command.AddMoon:
                        {
                            if (e.Length == 3 || e.Length == 4)
                            {
                                string moonName = e.GetString(1);
                                string moonTotalDays = e.GetString(2);
                                string moonCurrentDay = null;

                                if (e.Length == 4)
                                {
                                    moonCurrentDay = e.GetString(3);
                                }

                                vo = Custom.AddMoon(moonName, moonTotalDays, moonCurrentDay);

                                mobile.SendMessage(vo.Message);
                            }
                            else
                            {
                                mobile.SendMessage(Syntax.GetSyntax(true, command));
                            }

                            break;
                        }
                    case Command.InsertMoon:
                        {
                            if (e.Length == 4 || e.Length == 5)
                            {
                                string index = e.GetString(1);
                                string moonName = e.GetString(2);
                                string moonTotalDays = e.GetString(3);
                                string moonCurrentDay = null;

                                if (e.Length == 5)
                                {
                                    moonCurrentDay = e.GetString(4);
                                }

                                vo = Custom.InsertMoon(index, moonName, moonTotalDays, moonCurrentDay);

                                mobile.SendMessage(vo.Message);
                            }
                            else
                            {
                                mobile.SendMessage(Syntax.GetSyntax(true, command));
                            }

                            break;
                        }
                    case Command.SetMoon:
                        {
                            if (e.Length == 4 || e.Length == 5)
                            {
                                string index = e.GetString(1);
                                string moonName = e.GetString(2);
                                string moonTotalDays = e.GetString(3);
                                string moonCurrentDay = null;

                                if (e.Length == 5)
                                {
                                    moonCurrentDay = e.GetString(4);
                                }

                                vo = Custom.SetMoon(index, moonName, moonTotalDays, moonCurrentDay);

                                mobile.SendMessage(vo.Message);
                            }
                            else if (e.Length == 2)
                            {
                                string arg = e.GetString(1).ToLower();

                                if (arg == "defaults")
                                {
                                    Config.SetDefaultMoons();

                                    mobile.SendMessage("All custom moon's have been set to defaults.");
                                }
                                else
                                {
                                    mobile.SendMessage(Syntax.GetSyntax(true, command));
                                }
                            }
                            else
                            {
                                mobile.SendMessage(Syntax.GetSyntax(true, command));
                            }

                            break;
                        }
                    case Command.GetMoon:
                        {
                            if (e.Length == 2)
                            {
                                string index = e.GetString(1);

                                vo = Custom.GetMoon(index);

                                mobile.SendMessage(vo.Message);
                            }
                            else
                            {
                                mobile.SendMessage(Syntax.GetSyntax(true, command));
                            }

                            break;
                        }
                    case Command.RemoveMoon:
                        {
                            if (e.Length == 2)
                            {
                                string index = e.GetString(1);

                                vo = Custom.RemoveMoon(index);

                                mobile.SendMessage(vo.Message);
                            }
                            else
                            {
                                mobile.SendMessage(Syntax.GetSyntax(true, command));
                            }

                            break;
                        }
                    case Command.SetMoonProps:
                        {
                            if (e.Length == 3 || e.Length == 4)
                            {
                                string index = e.GetString(1);
                                string moonCurrentDay = e.GetString(2);
                                string moonTotalDays = null;

                                if (e.Length == 4)
                                {
                                    moonTotalDays = e.GetString(3);
                                }

                                vo = Custom.SetMoonProps(index, moonCurrentDay, moonTotalDays);

                                mobile.SendMessage(vo.Message);
                            }
                            else
                            {
                                mobile.SendMessage(Syntax.GetSyntax(true, command));
                            }

                            break;
                        }
                    case Command.ClearMoons:
                        {
                            if (e.Length == 1)
                            {
                                Data.MoonsArray = new List<MoonPropsObject>();

                                mobile.SendMessage("All custom moons have been cleared.");
                            }
                            else
                            {
                                mobile.SendMessage(Syntax.GetSyntax(true, command));
                            }

                            break;
                        }
                    case Command.SetFacetAdjust:
                        {
                            if (e.Length == 2)
                            {
                                string arg = e.GetString(1).ToLower();

                                if (arg == "defaults")
                                {
                                    Config.SetDefaultFacetAdjustments();

                                    mobile.SendMessage("All facet adjustments have been set to defaults.");
                                }
                                else
                                {
                                    mobile.SendMessage(Syntax.GetSyntax(true, command));
                                }
                            }
                            else if (e.Length == 3)
                            {
                                string index = e.GetString(1);
                                string adjustment = e.GetString(2);

                                vo = Custom.SetFacetAdjust(index, adjustment);

                                mobile.SendMessage(vo.Message);
                            }
                            else
                            {
                                mobile.SendMessage(Syntax.GetSyntax(true, command));
                            }

                            break;
                        }
                    case Command.GetFacetAdjust:
                        {
                            if (e.Length == 2)
                            {
                                string index = e.GetString(1);

                                vo = Custom.GetFacetAdjust(index);

                                mobile.SendMessage(vo.Message);
                            }
                            else
                            {
                                mobile.SendMessage(Syntax.GetSyntax(true, command));
                            }

                            break;
                        }
                    case Command.AddEmo:
                        {
                            if (e.Length == 1)
                            {
                                vo = Custom.AddEmo(mobile);

                                Support.SendStream(mobile, vo.Message);
                            }
                            else
                            {
                                mobile.SendMessage(Syntax.GetSyntax(true, command));
                            }

                            break;
                        }
                    case Command.SetEmo:
                        {
                            if (e.Length == 2 && e.GetString(1) == "??")
                            {
                                StringBuilder sb = new StringBuilder();

                                sb.Append("List of EMO types:\n");

                                sb.Append(Support.GetEmoTypeNames());

                                Support.SendStream(mobile, sb.ToString());
                            }
                            else if (e.Length == 4 || e.Length == 5)
                            {
                                string index = e.GetString(1);
                                string type = e.GetString(2);
                                string valueOne = e.GetString(3);
                                string valueTwo = e.Length == 5 ? e.GetString(4) : null;

                                if (!Support.CheckEmoType(type))
                                {
                                    mobile.SendMessage("That EMO type does not exist!");
                                }
                                else
                                {
                                    vo = Custom.SetEmo(index, type, valueOne, valueTwo);

                                    mobile.SendMessage(vo.Message);
                                }
                            }
                            else
                            {
                                mobile.SendMessage(Syntax.GetSyntax(true, command));
                            }

                            break;
                        }
                    case Command.GetEmo:
                        {
                            string index = null;
                            
                            if (e.Length == 1 || e.Length == 2)
                            {
                                if (e.Length == 2)
                                {
                                    index = e.GetString(1);

                                    if (index.ToLower() == "total")
                                    {
                                        mobile.SendMessage("Total EMOs: {0}", Data.EffectsMapArray.Count);

                                        break;
                                    }
                                }

                                vo = Custom.GetEmo(mobile, index);

                                Support.SendStream(mobile, vo.Message);
                            }
                            else
                            {
                                mobile.SendMessage(Syntax.GetSyntax(true, command));
                            }

                            break;
                        }
                    case Command.RemoveEmo:
                        {
                            if (e.Length == 2)
                            {
                                string index = e.GetString(1);

                                vo = Custom.RemoveEmo(index);

                                mobile.SendMessage(vo.Message);
                            }
                            else
                            {
                                mobile.SendMessage(Syntax.GetSyntax(true, command));
                            }

                            break;
                        }
                    case Command.AddEemo:
                        {
                            if (e.Length == 1)
                            {
                                vo = Custom.AddEemo(mobile);

                                Support.SendStream(mobile, vo.Message);
                            }
                            else
                            {
                                mobile.SendMessage(Syntax.GetSyntax(true, command));
                            }

                            break;
                        }
                    case Command.SetEemo:
                        {
                            if (e.Length == 2 && e.GetString(1) == "??")
                            {
                                StringBuilder sb = new StringBuilder();

                                sb.Append("List of EEMO types:\n");

                                sb.Append(Support.GetEemoTypeNames());

                                Support.SendStream(mobile, sb.ToString());
                            }
                            else if (e.Length == 4 || e.Length == 5)
                            {
                                string index = e.GetString(1);
                                string type = e.GetString(2);
                                string valueOne = e.GetString(3);
                                string valueTwo = e.Length == 5 ? e.GetString(4) : null;

                                if (!Support.CheckEemoType(type))
                                {
                                    mobile.SendMessage("That EEMO type does not exist!");
                                }
                                else
                                {
                                    vo = Custom.SetEemo(index, type, valueOne, valueTwo);

                                    mobile.SendMessage(vo.Message);
                                }
                            }
                            else
                            {
                                mobile.SendMessage(Syntax.GetSyntax(true, command));
                            }

                            break;
                        }
                    case Command.GetEemo:
                        {
                            string index = null;

                            if (e.Length == 1 || e.Length == 2)
                            {
                                if (e.Length == 2)
                                {
                                    index = e.GetString(1);

                                    if (index.ToLower() == "total")
                                    {
                                        mobile.SendMessage("Total EEMOs: {0}", Data.EffectsExclusionMapArray.Count);

                                        break;
                                    }
                                }

                                vo = Custom.GetEemo(mobile, index);

                                Support.SendStream(mobile, vo.Message);
                            }
                            else
                            {
                                mobile.SendMessage(Syntax.GetSyntax(true, command));
                            }

                            break;
                        }
                    case Command.RemoveEemo:
                        {
                            if (e.Length == 2)
                            {
                                string index = e.GetString(1);

                                vo = Custom.RemoveEemo(index);

                                mobile.SendMessage(vo.Message);
                            }
                            else
                            {
                                mobile.SendMessage(Syntax.GetSyntax(true, command));
                            }

                            break;
                        }
                    default:
                        {
                            mobile.SendMessage("That command does not exist!");

                            break;
                        }
                }
            }
            else
            {
                Support.SendCommandNames(mobile);
            }
        }

        [Usage("BaseTime")]
        [Description("Gets the current base date and time.")]
        private static void BaseTime_OnCommand(CommandEventArgs e)
        {
            e.Mobile.SendMessage(Formatting.GetTimeFormat(null, Format.Time));
        }

        [Usage("Time")]
        [Description("Gets the current date and time.")]
        private static void Time_OnCommand(CommandEventArgs e)
        {
            e.Mobile.SendMessage(Formatting.GetTimeFormat(e.Mobile, Format.Time));
        }

        [Usage("CompareTime")]
        [Description("Compares time vs base time.")]
        private static void CompareTime_OnCommand(CommandEventArgs e)
        {
            string baseTime = String.Format("BaseTime: {0}", Formatting.GetTimeFormat(null, Format.Time));
            string time = String.Format("Time: {0}", Formatting.GetTimeFormat(e.Mobile, Format.Time));

            e.Mobile.SendMessage(baseTime);
            e.Mobile.SendMessage(time);
        }

        [Usage("XTime")]
        [Description("Gets the current date and time based off of TimeZoneXDivisor only and not TimeZoneScaleMinutes.")]
        private static void XTime_OnCommand(CommandEventArgs e)
        {
            e.Mobile.SendMessage(Formatting.GetTimeFormat(e.Mobile, Format.Time, false));
        }

        [Usage("Season")]
        [Description("Gets the current lateral season and season.")]
        private static void Season_OnCommand(CommandEventArgs e)
        {
            Mobile mobile = e.Mobile;

            EffectsObject eo = EffectsEngine.GetEffects(mobile, true);

            string lateralSeason = eo.LateralSeason.ToString();
            string season = eo.Season.ToString();

            if (eo.EffectsMap != null)
            {
                mobile.SendMessage(String.Format("The lateral season is {0} and season at your location is {1}.", lateralSeason, season));
            }
            else
            {
                mobile.SendMessage("You are not in an area that is under any effects!");
            }
        }

        #endregion
    }
}