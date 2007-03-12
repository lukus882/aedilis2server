using System;
using Server;
using Server.Commands;

namespace Server.TimeSystem
{
    public class Syntax
    {
        #region Constant Variables

        private static readonly string SyntaxPrefix = String.Format("Usage: {0}{1}", CommandSystem.Prefix, Commands.TSCommand);

        #endregion

        #region Get Information

        public static string GetSyntax(bool incorrect, Command command)
        {
            string syntax = String.Empty;

            string commandString = command.ToString().ToUpper();

            switch (command)
            {
                case Command.Set: { syntax = "<variable> <value+>"; break; }
                case Command.Get: { syntax = "<variable>"; break; }
                case Command.Append: { syntax = "<variable> <value+>"; break; }
                case Command.RepopLightsList: { break; }
                case Command.Stop: { break; }
                case Command.Start: { break; }
                case Command.Restart: { break; }
                case Command.Load: { break; }
                case Command.Save: { break; }
                case Command.SetTime: { syntax = "<hh:mm>"; break; }
                case Command.Query: { break; }
                case Command.Version: { break; }
                case Command.ConvertLampPosts: { syntax = String.Format("\n{0} {1} ALL\n{0} {1} AREA", SyntaxPrefix, commandString); break; }
                case Command.AddMonth: { syntax = "<name> <days>"; break; }
                case Command.InsertMonth: { syntax = "<number> <name> <days>"; break; }
                case Command.SetMonth: { syntax = String.Format("<number> <name> <days>\n{0} {1} DEFAULTS", SyntaxPrefix, commandString); break; }
                case Command.GetMonth: { syntax = "<number>"; break; }
                case Command.RemoveMonth: { syntax = "<number|name>"; break; }
                case Command.ClearMonths: { break; }
                case Command.SetMonthProps: { syntax = "<number|name> <days>"; break; }
                case Command.AddMoon: { syntax = "<name> <total phase days> <current phase day*>"; break; }
                case Command.InsertMoon: { syntax = "<number> <name> <total phase days> <current phase day*>"; break; }
                case Command.SetMoon: { syntax = String.Format("<number> <name> <total phase days> <current phase day*>\n{0} {1} DEFAULTS", SyntaxPrefix, commandString); break; }
                case Command.GetMoon: { syntax = "<number>"; break; }
                case Command.RemoveMoon: { syntax = "<number|name>"; break; }
                case Command.ClearMoons: { break; }
                case Command.SetMoonProps: { syntax = "<number|name> <current phase day> <total phase days*>"; break; }
                case Command.SetFacetAdjust: { syntax = String.Format("<number|name> <value>\n{0} {1} DEFAULTS", SyntaxPrefix, commandString); break; }
                case Command.GetFacetAdjust: { syntax = "<number>"; break; }
                case Command.AddEmo: { break; }
                case Command.SetEmo: { syntax = "<EMO number> <EMO type> <value one> <value two*>"; break; }
                case Command.GetEmo: { syntax = String.Format("<EMO number*>\n{0} {1} TOTAL", SyntaxPrefix, commandString); break; }
                case Command.RemoveEmo: { syntax = "<EMO number>"; break; }
                case Command.ToggleEmo: { syntax = "<EMO number>"; break; }
                case Command.AddEemo: { break; }
                case Command.SetEemo: { syntax = "<EEMO number> <EEMO type> <value one> <value two*>"; break; }
                case Command.GetEemo: { syntax = String.Format("<EEMO number*>\n{0} {1} TOTAL", SyntaxPrefix, commandString); break; }
                case Command.RemoveEemo: { syntax = "<EEMO number>"; break; }
                case Command.ToggleEemo: { syntax = "<EEMO number>"; break; }
            }

            return String.Format("{0}{1} {2} {3}", incorrect ? "The syntax is incorrect!\n" : String.Empty, SyntaxPrefix, commandString, syntax);
        }

        #endregion
    }
}