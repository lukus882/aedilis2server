using System;
using System.Collections.Generic;
using System.Text;
using Server;

namespace Server.TimeSystem
{
    public class Custom
    {
        #region Custom Months

        public static VariableObject AddMonth(string setMonthName, string setMonthDays)
        {
            return SetMonth(CommandType.Add, "-1", setMonthName, setMonthDays);
        }

        public static VariableObject InsertMonth(string index, string setMonthName, string setMonthDays)
        {
            return SetMonth(CommandType.Insert, index, setMonthName, setMonthDays);
        }

        public static VariableObject SetMonth(string index, string setMonthName, string setMonthDays)
        {
            return SetMonth(CommandType.Set, index, setMonthName, setMonthDays);
        }

        private static VariableObject SetMonth(CommandType ct, string index, string setMonthName, string setMonthDays)
        {
            VariableObject vo = new VariableObject();

            if (Support.CheckForceScriptSettings(ref vo, "Custom Months"))
            {
                return vo;
            }

            if (Data.DataFileInUse)
            {
                vo.Success = false;
                vo.Message = Data.DataFileInUseMessage;

                return vo;
            }

            if (Data.MonthsArray.Count == 0)
            {
                if (ct != CommandType.Add)
                {
                    vo.Success = false;
                    vo.Message = "There are no custom months!";

                    return vo;
                }
            }

            if (Support.CheckAlreadyExistsInArray(ref vo, Data.MonthsArray, setMonthName))
            {
                return vo;
            }

            lock (Data.MonthsArray)
            {
                int monthIndex = 0;

                int newMonthDays = 0;

                bool success = false;
                string message = null;

                if (ct != CommandType.Add) // If not adding, check index number.
                {
                    object o = Support.GetValue(index);

                    if (Config.CheckVariable(o, "Number", 1, Data.MonthsArray.Count, typeof(int), ref success, ref message))
                    {
                        int value = (int)o;

                        monthIndex = value - 1;
                    }
                }
                else // Adding, so increment index by 1.
                {
                    success = true;

                    index = (Data.MonthsArray.Count + 1).ToString();
                }

                if (success) // Check month days.
                {
                    object o = Support.GetValue(setMonthDays);

                    if (Config.CheckVariable(o, "Days", 1, int.MaxValue, typeof(int), ref success, ref message))
                    {
                        int value = (int)o;

                        newMonthDays = value;
                    }
                }

                if (success) // If successful then finalize.
                {
                    MonthPropsObject mpo = new MonthPropsObject(setMonthName, newMonthDays);

                    switch (ct)
                    {
                        case CommandType.Add:
                            {
                                Data.MonthsArray.Add(mpo);

                                message = String.Format("Month #{0} '{1}' has been added with '{2}' day{3}.", index, setMonthName, newMonthDays, newMonthDays == 1 ? "" : "s");

                                break;
                            }
                        case CommandType.Insert:
                            {
                                Data.MonthsArray.Insert(monthIndex, mpo);

                                message = String.Format("Month #{0} '{1}' has been inserted with '{2}' day{3}.  All succeeding months indexes have moved down.", index, setMonthName, newMonthDays, newMonthDays == 1 ? "" : "s");

                                break;
                            }
                        case CommandType.Set:
                            {
                                Data.MonthsArray[monthIndex] = mpo;

                                message = String.Format("Month #{0} '{1}' has been set with '{2}' day{3}.", index, setMonthName, newMonthDays, newMonthDays == 1 ? "" : "s");

                                break;
                            }
                    }

                    Engine.Restart();
                }

                vo.Success = success;
                vo.Message = message;
            }

            return vo;
        }

        public static VariableObject GetMonth(string index)
        {
            VariableObject vo = new VariableObject();

            if (Data.MonthsArray.Count == 0)
            {
                vo.Success = false;
                vo.Message = "There are no custom months!";

                return vo;
            }

            object o = Support.GetValue(index);

            lock (Data.MonthsArray)
            {
                bool success = false;
                string message = null;

                if (Config.CheckVariable(o, "Number", 1, Data.MonthsArray.Count, typeof(int), ref success, ref message))
                {
                    int value = (int)o;

                    MonthPropsObject mpo = Data.MonthsArray[value - 1];

                    message = String.Format("Month #{0} '{1}' has '{2}' day{3}.", value, mpo.Name, mpo.TotalDays, mpo.TotalDays == 1 ? "" : "s");
                }

                vo.Success = success;
                vo.Message = message;
            }

            return vo;
        }

        public static VariableObject RemoveMonth(string index)
        {
            VariableObject vo = new VariableObject();

            if (Support.CheckForceScriptSettings(ref vo, "Custom Months"))
            {
                return vo;
            }

            if (Data.DataFileInUse)
            {
                vo.Success = false;
                vo.Message = Data.DataFileInUseMessage;

                return vo;
            }

            if (Data.MonthsArray.Count == 0)
            {
                vo.Success = false;
                vo.Message = "There are no custom months!";

                return vo;
            }

            object o = Support.GetValue(index);

            lock (Data.MonthsArray)
            {
                bool success = false;
                string message = null;

                if (o is string)
                {
                    for (int i = 0; i < Data.MonthsArray.Count; i++)
                    {
                        MonthPropsObject mpo = Data.MonthsArray[i];

                        if (mpo.Name.ToLower() == index.ToLower())
                        {
                            o = Support.GetValue((i + 1).ToString());

                            break;
                        }
                    }
                }

                if (Config.CheckVariable(o, "Number", 1, Data.MonthsArray.Count, typeof(int), ref success, ref message))
                {
                    int value = (int)o;

                    MonthPropsObject mpo = Data.MonthsArray[value - 1];

                    Data.MonthsArray.RemoveAt(value - 1);
                    Data.MonthsArray.TrimExcess();

                    message = String.Format("Month #{0} '{1}' has been removed.  All succeeding months indexes have moved up.", value, mpo.Name);

                    Engine.Restart();
                }
                else
                {
                    StringBuilder sb = new StringBuilder();

                    sb.Append(message);
                    sb.Append("\r\nYou may also specify the month name instead of the number.");

                    message = sb.ToString();
                }

                vo.Success = success;
                vo.Message = message;
            }

            return vo;
        }

        public static VariableObject SetMonthProps(string index, string setMonthDays)
        {
            VariableObject vo = new VariableObject();

            if (Support.CheckForceScriptSettings(ref vo, "Custom Months"))
            {
                return vo;
            }

            if (Data.MonthsArray.Count == 0)
            {
                vo.Success = false;
                vo.Message = "There are no custom months!";

                return vo;
            }

            lock (Data.MonthsArray)
            {
                int monthIndex = 0;

                string monthName = null;
                int newMonthDays = 0;

                bool success = false;
                string message = null;

                {
                    object o = Support.GetValue(index);

                    if (o is string)
                    {
                        for (int i = 0; i < Data.MonthsArray.Count; i++)
                        {
                            MonthPropsObject mpo = Data.MonthsArray[i];

                            if (mpo.Name.ToLower() == index.ToLower())
                            {
                                o = Support.GetValue((i + 1).ToString());

                                break;
                            }
                        }
                    }

                    if (Config.CheckVariable(o, "Number", 1, Data.MonthsArray.Count, typeof(int), ref success, ref message))
                    {
                        int value = (int)o;

                        monthIndex = value - 1;

                        index = value.ToString();

                        MonthPropsObject mpo = Data.MonthsArray[monthIndex];

                        monthName = mpo.Name;
                    }
                    else
                    {
                        StringBuilder sb = new StringBuilder();

                        sb.Append(message);
                        sb.Append("\r\nYou may also specify the month name instead of the number.");

                        message = sb.ToString();
                    }
                }

                if (success)
                {
                    object o = Support.GetValue(setMonthDays);

                    if (Config.CheckVariable(o, "Days", 1, int.MaxValue, typeof(int), ref success, ref message))
                    {
                        int value = (int)o;

                        newMonthDays = value;
                    }
                }

                if (success)
                {
                    MonthPropsObject mpo = new MonthPropsObject(monthName, newMonthDays);

                    Data.MonthsArray[monthIndex] = mpo;

                    message = String.Format("Month #{0} '{1}' has been set with '{2}' day{3}.", index, monthName, newMonthDays, newMonthDays == 1 ? "" : "s");

                    Engine.Restart();
                }

                vo.Success = success;
                vo.Message = message;
            }

            return vo;
        }

        public static void ClearMonths()
        {
            lock (Data.MonthsArray)
            {
                for (int i = 1; i < Data.MonthsArray.Count; i++)
                {
                    Data.MonthsArray.RemoveAt(i);

                    i--;
                }

                if (Data.Day > 30)
                {
                    Data.Day = 30;
                }

                Data.MonthsArray[0].Name = "NewMonth";
                Data.MonthsArray[0].TotalDays = 30;
            }
        }

        #endregion

        #region Custom Moons

        public static VariableObject AddMoon(string setMoonName, string setMoonTotalDays, string setMoonCurrentDay)
        {
            return SetMoon(CommandType.Add, "-1", setMoonName, setMoonTotalDays, setMoonCurrentDay);
        }

        public static VariableObject InsertMoon(string index, string setMoonName, string setMoonTotalDays, string setMoonCurrentDay)
        {
            return SetMoon(CommandType.Insert, index, setMoonName, setMoonTotalDays, setMoonCurrentDay);
        }

        public static VariableObject SetMoon(string index, string setMoonName, string setMoonTotalDays, string setMoonCurrentDay)
        {
            return SetMoon(CommandType.Set, index, setMoonName, setMoonTotalDays, setMoonCurrentDay);
        }

        private static VariableObject SetMoon(CommandType ct, string index, string setMoonName, string setMoonTotalDays, string setMoonCurrentDay)
        {
            VariableObject vo = new VariableObject();

            if (Support.CheckForceScriptSettings(ref vo, "Custom Moons"))
            {
                return vo;
            }

            if (Data.DataFileInUse)
            {
                vo.Success = false;
                vo.Message = Data.DataFileInUseMessage;

                return vo;
            }

            if (Data.MoonsArray.Count == 0)
            {
                if (ct != CommandType.Add)
                {
                    vo.Success = false;
                    vo.Message = "There are no custom moons!";

                    return vo;
                }
            }

            if (Support.CheckAlreadyExistsInArray(ref vo, Data.MoonsArray, setMoonName))
            {
                return vo;
            }

            lock (Data.MoonsArray)
            {
                int moonIndex = 0;

                int newMoonTotalDays = 0;
                int newMoonCurrentDay = 1;

                bool success = false;
                string message = null;

                if (ct != CommandType.Add) // If not adding, check index number.
                {
                    object o = Support.GetValue(index);

                    if (Config.CheckVariable(o, "Number", 1, Data.MoonsArray.Count, typeof(int), ref success, ref message))
                    {
                        int value = (int)o;

                        moonIndex = value - 1;
                    }
                }
                else // Adding, so increment index by 1.
                {
                    success = true;

                    index = (Data.MoonsArray.Count + 1).ToString();
                }

                if (success) // Check total phase days.
                {
                    object o = Support.GetValue(setMoonTotalDays);

                    if (Config.CheckVariable(o, "Total Phase Days", Enum.GetNames(typeof(MoonPhase)).Length, int.MaxValue, typeof(int), ref success, ref message))
                    {
                        int value = (int)o;

                        newMoonTotalDays = value;
                    }
                }

                if (success) // Check current phase day.
                {
                    if (setMoonCurrentDay != null)
                    {
                        object o = Support.GetValue(setMoonCurrentDay);

                        if (Config.CheckVariable(o, "Current Phase Day", 1, newMoonTotalDays, typeof(int), ref success, ref message))
                        {
                            int value = (int)o;

                            newMoonCurrentDay = value;
                        }
                    }
                }

                if (success) // If successful then finalize.
                {
                    MoonPropsObject mpo = new MoonPropsObject(setMoonName, newMoonTotalDays, newMoonCurrentDay);

                    switch (ct)
                    {
                        case CommandType.Add:
                            {
                                Data.MoonsArray.Add(mpo);

                                message = String.Format("Moon #{0} '{1}' has been added with '{2}' total phase day{3} and current phase day is '{4}'.", index, setMoonName, newMoonTotalDays, newMoonTotalDays == 1 ? "" : "s", newMoonCurrentDay);

                                break;
                            }
                        case CommandType.Insert:
                            {
                                Data.MoonsArray.Insert(moonIndex, mpo);

                                message = String.Format("Moon #{0} '{1}' has been inserted with '{2}' total phase day{3} and current phase day is '{4}'.  All succeeding moons indexes have moved down.", index, setMoonName, newMoonTotalDays, newMoonTotalDays == 1 ? "" : "s", newMoonCurrentDay);

                                break;
                            }
                        case CommandType.Set:
                            {
                                Data.MoonsArray[moonIndex] = mpo;

                                message = String.Format("Moon #{0} '{1}' has been set with '{2}' total phase day{3} and current phase day is '{4}'.", index, setMoonName, newMoonTotalDays, newMoonTotalDays == 1 ? "" : "s", newMoonCurrentDay);

                                break;
                            }
                    }

                    Engine.Restart();
                }

                vo.Success = success;
                vo.Message = message;
            }

            return vo;
        }

        public static VariableObject GetMoon(string index)
        {
            VariableObject vo = new VariableObject();

            if (Data.MoonsArray.Count == 0)
            {
                vo.Success = false;
                vo.Message = "There are no custom moons!";

                return vo;
            }

            object o = Support.GetValue(index);

            lock (Data.MoonsArray)
            {
                bool success = false;
                string message = null;

                if (Config.CheckVariable(o, "Number", 1, Data.MoonsArray.Count, typeof(int), ref success, ref message))
                {
                    int value = (int)o;

                    MoonPropsObject mpo = Data.MoonsArray[value - 1];

                    message = String.Format("Moon #{0} '{1}' has '{2}' total phase day{3} and current phase day is '{4}'.", value, mpo.Name, mpo.TotalDays, mpo.TotalDays == 1 ? "" : "s", mpo.CurrentDay);
                }

                vo.Success = success;
                vo.Message = message;
            }

            return vo;
        }

        public static VariableObject RemoveMoon(string index)
        {
            VariableObject vo = new VariableObject();

            if (Support.CheckForceScriptSettings(ref vo, "Custom Moons"))
            {
                return vo;
            }

            if (Data.DataFileInUse)
            {
                vo.Success = false;
                vo.Message = Data.DataFileInUseMessage;

                return vo;
            }

            if (Data.MoonsArray.Count == 0)
            {
                vo.Success = false;
                vo.Message = "There are no custom moons!";

                return vo;
            }

            object o = Support.GetValue(index);

            lock (Data.MoonsArray)
            {
                bool success = false;
                string message = null;

                if (o is string)
                {
                    for (int i = 0; i < Data.MoonsArray.Count; i++)
                    {
                        MoonPropsObject mpo = Data.MoonsArray[i];

                        if (mpo.Name.ToLower() == index.ToLower())
                        {
                            o = Support.GetValue((i + 1).ToString());

                            break;
                        }
                    }
                }

                if (Config.CheckVariable(o, "Number", 1, Data.MoonsArray.Count, typeof(int), ref success, ref message))
                {
                    int value = (int)o;

                    MoonPropsObject mpo = Data.MoonsArray[value - 1];

                    Data.MoonsArray.RemoveAt(value - 1);
                    Data.MoonsArray.TrimExcess();

                    message = String.Format("Moon #{0} '{1}' has been removed.  All succeeding moons indexes have moved up.", value, mpo.Name);

                    Engine.Restart();
                }
                else
                {
                    StringBuilder sb = new StringBuilder();

                    sb.Append(message);
                    sb.Append("\r\nYou may also specify the moon name instead of the number.");

                    message = sb.ToString();
                }

                vo.Success = success;
                vo.Message = message;
            }

            return vo;
        }

        public static VariableObject SetMoonProps(string index, string setMoonCurrentDay, string setMoonTotalDays)
        {
            VariableObject vo = new VariableObject();

            if (Support.CheckForceScriptSettings(ref vo, "Custom Moons") && setMoonTotalDays != null)
            {
                return vo;
            }

            if (Data.MoonsArray.Count == 0)
            {
                vo.Success = false;
                vo.Message = "There are no custom moons!";

                return vo;
            }

            lock (Data.MoonsArray)
            {
                int moonIndex = 0;

                string moonName = null;
                int oldMoonTotalDays = 0;
                int newMoonTotalDays = 0;
                int newMoonCurrentDay = 0;

                bool success = false;
                string message = null;

                {
                    object o = Support.GetValue(index);

                    if (o is string)
                    {
                        for (int i = 0; i < Data.MoonsArray.Count; i++)
                        {
                            MoonPropsObject mpo = Data.MoonsArray[i];

                            if (mpo.Name.ToLower() == index.ToLower())
                            {
                                o = Support.GetValue((i + 1).ToString());

                                break;
                            }
                        }
                    }

                    if (Config.CheckVariable(o, "Number", 1, Data.MoonsArray.Count, typeof(int), ref success, ref message))
                    {
                        int value = (int)o;

                        moonIndex = value - 1;

                        index = value.ToString();

                        MoonPropsObject mpo = Data.MoonsArray[moonIndex];

                        moonName = mpo.Name;
                        oldMoonTotalDays = mpo.TotalDays;
                    }
                    else
                    {
                        StringBuilder sb = new StringBuilder();

                        sb.Append(message);
                        sb.Append("\r\nYou may also specify the moon name instead of the number.");

                        message = sb.ToString();
                    }
                }

                if (success)
                {
                    if (setMoonTotalDays != null)
                    {
                        object o = Support.GetValue(setMoonTotalDays);

                        if (Config.CheckVariable(o, "Total Phase Days", Enum.GetNames(typeof(MoonPhase)).Length, int.MaxValue, typeof(int), ref success, ref message))
                        {
                            int value = (int)o;

                            newMoonTotalDays = value;
                        }
                    }
                    else
                    {
                        newMoonTotalDays = oldMoonTotalDays;
                    }
                }

                if (success)
                {
                    object o = Support.GetValue(setMoonCurrentDay);

                    if (Config.CheckVariable(o, "Current Phase Day", 1, newMoonTotalDays, typeof(int), ref success, ref message))
                    {
                        int value = (int)o;

                        newMoonCurrentDay = value;
                    }
                }

                if (success)
                {
                    MoonPropsObject mpo = new MoonPropsObject(moonName, newMoonTotalDays, newMoonCurrentDay);

                    Data.MoonsArray[moonIndex] = mpo;

                    message = String.Format("Moon #{0} '{1}' has been set to {2} total phase day{3} and current phase day is '{4}'.", index, moonName, newMoonTotalDays, newMoonTotalDays == 1 ? "" : "s", newMoonCurrentDay);

                    Engine.Restart();
                }

                vo.Success = success;
                vo.Message = message;
            }

            return vo;
        }

        #endregion

        #region Facet Adjustments

        public static VariableObject SetFacetAdjust(string index, string setAdjustment)
        {
            VariableObject vo = new VariableObject();

            if (Support.CheckForceScriptSettings(ref vo, "Facet Adjustment"))
            {
                return vo;
            }

            if (Data.DataFileInUse)
            {
                vo.Success = false;
                vo.Message = Data.DataFileInUseMessage;

                return vo;
            }

            lock (Data.FacetArray)
            {
                int mapIndex = 0;

                bool success = false;
                string message = null;

                {
                    object o = Support.GetValue(index);

                    if (o is string)
                    {
                        Map map = Support.GetMapFromName(index, false);

                        if (map != null)
                        {
                            o = Support.GetValue(map.MapIndex.ToString());
                        }
                    }

                    if (Config.CheckVariable(o, "Number", 0, Data.NumberOfFacets - 1, typeof(int), ref success, ref message))
                    {
                        int value = (int)o;

                        mapIndex = value;
                    }
                    else
                    {
                        StringBuilder sb = new StringBuilder();

                        sb.Append(message);
                        sb.Append("\r\nYou may also specify the facet name instead of the number.");

                        message = sb.ToString();
                    }
                }

                if (success)
                {
                    object o = Support.GetValue(setAdjustment);

                    if (Config.CheckVariable(o, "Adjustment", 0, int.MaxValue - 1440, typeof(int), ref success, ref message))
                    {
                        int value = (int)o;

                        Data.FacetArray[mapIndex] = new FacetPropsObject(Map.Maps[mapIndex], value);

                        string name = Map.Maps[mapIndex].Name;

                        message = String.Format("Facet #{0} '{1}' has been adjusted to be '{2}' minute{3} ahead of base time.", mapIndex, name, value, value == 1 ? "" : "s");

                        Engine.Restart();

                    }
                }

                vo.Success = success;
                vo.Message = message;
            }

            return vo;
        }

        public static VariableObject GetFacetAdjust(string index)
        {
            VariableObject vo = new VariableObject();

            object o = Support.GetValue(index);

            lock (Data.FacetArray)
            {
                bool success = false;
                string message = null;

                if (Config.CheckVariable(o, "Number", 0, Data.NumberOfFacets - 1, typeof(int), ref success, ref message))
                {
                    int value = (int)o;

                    string name = Map.Maps[value].Name;

                    FacetPropsObject fpo = Data.FacetArray[value];

                    int adjustment = fpo.Adjustment;

                    message = String.Format("Facet #{0} '{1}' has an adjustment of '{2}' minute{3} ahead of base time.", value, name, adjustment, adjustment == 1 ? "" : "s");
                }

                vo.Success = success;
                vo.Message = message;
            }

            return vo;
        }

        #endregion

        #region Effects Maps

        public static VariableObject AddEmo(Mobile mobile)
        {
            VariableObject vo = new VariableObject();

            if (Support.CheckForceScriptSettings(ref vo, "Add EffectsMapObject"))
            {
                return vo;
            }

            if (Data.DataFileInUse)
            {
                vo.Success = false;
                vo.Message = Data.DataFileInUseMessage;

                return vo;
            }

            bool success = false;
            string message = null;

            EffectsMapObject emo = null;

            int index = -1;

            lock (Data.EffectsMapArray)
            {
                emo = Config.SetDefaultEffectsValues(new EffectsMapObject(mobile.Map, -1, -1, -1, -1));

                index = Data.EffectsMapArray.Count;

                emo.Index = index;

                Data.EffectsMapArray.Add(emo);

                success = true;
            }

            int height = emo.Y2 - emo.Y1;

            int outerLatitudeHeight = (int)(height * emo.OuterLatitudePercent);
            int innerLatitudeHeight = (int)(height * emo.InnerLatitudePercent);
            int middleLatitude = emo.Y1 + (int)(height / 2);

            int upperOuterLowRange = emo.Y1;
            int upperOuterHighRange = emo.Y1 + outerLatitudeHeight;

            int lowerOuterLowRange = emo.Y1 + (height - outerLatitudeHeight);
            int lowerOuterHighRange = emo.Y1 + height;

            int innerLowRange = middleLatitude - innerLatitudeHeight;
            int innerHighRange = middleLatitude + innerLatitudeHeight;

            StringBuilder sb = new StringBuilder();

            sb.Append(String.Format("EMO #{0} has been added!\n", index));

            message = sb.ToString();

            vo.Success = success;
            vo.Message = message;

            return vo;
        }

        public static VariableObject SetEmo(string index, string type, string valueOne, string valueTwo)
        {
            VariableObject vo = new VariableObject();

            if (Support.CheckForceScriptSettings(ref vo, "Set EffectsMapObject"))
            {
                return vo;
            }

            if (Data.DataFileInUse)
            {
                vo.Success = false;
                vo.Message = Data.DataFileInUseMessage;

                return vo;
            }

            lock (Data.EffectsMapArray)
            {
                bool success = false;
                string message = null;

                EffectsMapObject emo = null;

                {
                    object o = Support.GetValue(index);

                    if (Config.CheckVariable(o, "Number", 0, Data.EffectsMapArray.Count - 1, typeof(int), ref success, ref message))
                    {
                        int value = (int)o;

                        emo = Data.EffectsMapArray[value];
                    }
                }

                if (emo == null)
                {
                    vo.Success = false;
                    vo.Message = message;

                    return vo;
                }

                EffectsMapType emoType = Support.GetEmoTypeFromName(type);

                switch (emoType)
                {
                    default:
                        {
                            success = false;
                            message = "That EMO type does not exist!";

                            break;
                        }
                    case EffectsMapType.Priority:
                        {
                            if (valueTwo != null)
                            {
                                success = false;
                                message = Syntax.GetSyntax(true, Command.SetEmo);

                                break;
                            }

                            object o = Support.GetValue(valueOne);

                            if (Config.CheckVariable(o, emoType.ToString(), 0, int.MaxValue, typeof(int), ref success, ref message))
                            {
                                int value = (int)o;

                                emo.Priority = value;

                                message = Formatting.VariableMessageFormatter(String.Format("EMO #{0}", emo.Index), emoType.ToString(), value.ToString());
                            }

                            break;
                        }
                    case EffectsMapType.Map:
                        {
                            if (valueTwo != null)
                            {
                                success = false;
                                message = Syntax.GetSyntax(true, Command.SetEmo);

                                break;
                            }

                            Map map = Support.GetMapFromName(valueOne, false);

                            if (map == null)
                            {
                                message = Formatting.ErrorMessageFormatter(emoType.ToString(), valueOne, Support.GetMapList(false));

                                break;
                            }

                            success = true;

                            emo.Map = map;

                            message = Formatting.VariableMessageFormatter(String.Format("EMO #{0}", emo.Index), emoType.ToString(), map.Name);

                            break;
                        }
                    case EffectsMapType.X1Y1:
                        {
                            bool overallSuccess = true;

                            if (valueTwo == null)
                            {
                                success = false;
                                message = Syntax.GetSyntax(true, Command.SetEmo);

                                break;
                            }

                            int x1 = -1, y1 = -1;

                            {
                                object o = Support.GetValue(valueOne);

                                if (Config.CheckVariable(o, "X1", 0, emo.X2 - 1, typeof(int), ref success, ref message))
                                {
                                    int value = (int)o;

                                    x1 = value;
                                }
                                else
                                {
                                    overallSuccess = false;
                                }
                            }

                            {
                                object o = Support.GetValue(valueTwo);

                                StringBuilder sb = new StringBuilder();

                                if (message != null)
                                {
                                    sb.Append(message);
                                    sb.Append("\n");
                                }

                                if (Config.CheckVariable(o, "Y1", 0, emo.Y2 - 1, typeof(int), ref success, ref message))
                                {
                                    int value = (int)o;

                                    y1 = value;
                                }
                                else
                                {
                                    overallSuccess = false;

                                    sb.Append(message);
                                }

                                message = sb.ToString();
                            }

                            if (overallSuccess)
                            {
                                emo.X1 = x1;
                                emo.Y1 = y1;

                                string value = String.Format("{0}, {1}", x1, y1);

                                message = Formatting.VariableMessageFormatter(String.Format("EMO #{0}", emo.Index), emoType.ToString(), value);
                            }

                            break;
                        }
                    case EffectsMapType.X2Y2:
                        {
                            bool overallSuccess = true;

                            if (valueTwo == null)
                            {
                                success = false;
                                message = Syntax.GetSyntax(true, Command.SetEmo);

                                break;
                            }

                            int x2 = -1, y2 = -1;

                            {
                                object o = Support.GetValue(valueOne);

                                if (Config.CheckVariable(o, "X2", emo.X1 + 1, emo.Map.Width, typeof(int), ref success, ref message))
                                {
                                    int value = (int)o;

                                    x2 = value;
                                }
                                else
                                {
                                    overallSuccess = false;
                                }
                            }

                            {
                                object o = Support.GetValue(valueTwo);

                                StringBuilder sb = new StringBuilder();

                                if (message != null)
                                {
                                    sb.Append(message);
                                    sb.Append("\n");
                                }

                                if (Config.CheckVariable(o, "Y2", emo.Y1 + 1, emo.Map.Height, typeof(int), ref success, ref message))
                                {
                                    int value = (int)o;

                                    y2 = value;
                                }
                                else
                                {
                                    overallSuccess = false;

                                    sb.Append(message);
                                }

                                message = sb.ToString();
                            }

                            if (overallSuccess)
                            {
                                emo.X2 = x2;
                                emo.Y2 = y2;

                                string value = String.Format("{0}, {1}", x2, y2);

                                message = Formatting.VariableMessageFormatter(String.Format("EMO #{0}", emo.Index), emoType.ToString(), value);
                            }

                            break;
                        }
                    case EffectsMapType.UseLatitude:
                        {
                            if (valueTwo != null)
                            {
                                success = false;
                                message = Syntax.GetSyntax(true, Command.SetEmo);

                                break;
                            }

                            object o = Support.GetValue(valueOne);

                            if (Config.CheckVariable(o, emoType.ToString(), false, true, typeof(bool), ref success, ref message))
                            {
                                bool value = (bool)o;

                                emo.UseLatitude = value;

                                message = Formatting.VariableMessageFormatter(String.Format("EMO #{0}", emo.Index), emoType.ToString(), value.ToString());
                            }

                            break;
                        }
                    case EffectsMapType.OuterInnerLatitude:
                        {
                            if (valueTwo == null)
                            {
                                success = false;
                                message = Syntax.GetSyntax(true, Command.SetEmo);

                                break;
                            }

                            {
                                object o = Support.GetValue(valueOne);

                                if (Config.CheckVariable(o, "OuterLatitudePercent", 0, 100, typeof(int), ref success, ref message))
                                {
                                    int value = (int)o;

                                    emo.OuterLatitudePercent = value / 100;

                                    message = Formatting.VariableMessageFormatter(String.Format("EMO #{0}", emo.Index), "OuterLatitudePercent", String.Format("{0}%", value));
                                }
                            }

                            {
                                object o = Support.GetValue(valueTwo);

                                StringBuilder sb = new StringBuilder();

                                if (message != null)
                                {
                                    sb.Append(message);
                                    sb.Append("\n");
                                }

                                if (Config.CheckVariable(o, "InnerLatitudePercent", 0, 100, typeof(int), ref success, ref message))
                                {
                                    int value = (int)o;

                                    emo.InnerLatitudePercent = value / 100;

                                    message = Formatting.VariableMessageFormatter(String.Format("EMO #{0}", emo.Index), "InnerLatitudePercent", String.Format("{0}%", value));
                                }

                                sb.Append(message);

                                message = sb.ToString();
                            }

                            break;
                        }
                    case EffectsMapType.UseSeasons:
                        {
                            if (valueTwo != null)
                            {
                                success = false;
                                message = Syntax.GetSyntax(true, Command.SetEmo);

                                break;
                            }

                            object o = Support.GetValue(valueOne);

                            if (Config.CheckVariable(o, emoType.ToString(), false, true, typeof(bool), ref success, ref message))
                            {
                                bool value = (bool)o;

                                emo.UseSeasons = value;

                                message = Formatting.VariableMessageFormatter(String.Format("EMO #{0}", emo.Index), emoType.ToString(), value.ToString());
                            }

                            break;
                        }
                    case EffectsMapType.StaticSeason:
                        {
                            if (valueTwo != null)
                            {
                                success = false;
                                message = Syntax.GetSyntax(true, Command.SetEmo);

                                break;
                            }

                            Season season = Support.GetSeasonFromName(valueOne);

                            if (season == Season.None && valueOne.ToUpper() != season.ToString().ToUpper())
                            {
                                message = Formatting.ErrorMessageFormatter(emoType.ToString(), valueOne, Support.GetSeasonList());

                                break;
                            }

                            success = true;

                            emo.StaticSeason = season;

                            message = Formatting.VariableMessageFormatter(String.Format("EMO #{0}", emo.Index), emoType.ToString(), season.ToString());

                            break;
                        }
                    case EffectsMapType.SpringDate:
                        {
                            if (valueTwo == null)
                            {
                                success = false;
                                message = Syntax.GetSyntax(true, Command.SetEmo);

                                break;
                            }

                            {
                                object o = Support.GetValue(valueOne);

                                if (Config.CheckVariable(o, "Spring Date Month", 1, Data.MonthsArray.Count, typeof(int), ref success, ref message))
                                {
                                    int value = (int)o;

                                    emo.SpringDate.Month = value;

                                    message = Formatting.VariableMessageFormatter(String.Format("EMO #{0}", emo.Index), "Spring Date Month", value.ToString());
                                }
                            }

                            if (success)
                            {
                                object o = Support.GetValue(valueTwo);

                                StringBuilder sb = new StringBuilder();

                                if (message != null)
                                {
                                    sb.Append(message);
                                    sb.Append("\n");
                                }

                                if (Config.CheckVariable(o, "Spring Date Day", 1, Data.MonthsArray[emo.SpringDate.Month - 1].TotalDays, typeof(int), ref success, ref message))
                                {
                                    int value = (int)o;

                                    emo.SpringDate.Day = value;

                                    message = Formatting.VariableMessageFormatter(String.Format("EMO #{0}", emo.Index), "Spring Date Day", value.ToString());
                                }

                                sb.Append(message);

                                message = sb.ToString();
                            }

                            break;
                        }
                    case EffectsMapType.SummerDate:
                        {
                            if (valueTwo == null)
                            {
                                success = false;
                                message = Syntax.GetSyntax(true, Command.SetEmo);

                                break;
                            }

                            {
                                object o = Support.GetValue(valueOne);

                                if (Config.CheckVariable(o, "Summer Date Month", 1, Data.MonthsArray.Count, typeof(int), ref success, ref message))
                                {
                                    int value = (int)o;

                                    emo.SummerDate.Month = value;

                                    message = Formatting.VariableMessageFormatter(String.Format("EMO #{0}", emo.Index), "Summer Date Month", value.ToString());
                                }
                            }

                            if (success)
                            {
                                object o = Support.GetValue(valueTwo);

                                StringBuilder sb = new StringBuilder();

                                if (message != null)
                                {
                                    sb.Append(message);
                                    sb.Append("\n");
                                }

                                if (Config.CheckVariable(o, "Summer Date Day", 1, Data.MonthsArray[emo.SpringDate.Month - 1].TotalDays, typeof(int), ref success, ref message))
                                {
                                    int value = (int)o;

                                    emo.SummerDate.Day = value;

                                    message = Formatting.VariableMessageFormatter(String.Format("EMO #{0}", emo.Index), "Summer Date Day", value.ToString());
                                }

                                sb.Append(message);

                                message = sb.ToString();
                            }

                            break;
                        }
                    case EffectsMapType.FallDate:
                        {
                            if (valueTwo == null)
                            {
                                success = false;
                                message = Syntax.GetSyntax(true, Command.SetEmo);

                                break;
                            }

                            {
                                object o = Support.GetValue(valueOne);

                                if (Config.CheckVariable(o, "Fall Date Month", 1, Data.MonthsArray.Count, typeof(int), ref success, ref message))
                                {
                                    int value = (int)o;

                                    emo.FallDate.Month = value;

                                    message = Formatting.VariableMessageFormatter(String.Format("EMO #{0}", emo.Index), "Fall Date Month", value.ToString());
                                }
                            }

                            if (success)
                            {
                                object o = Support.GetValue(valueTwo);

                                StringBuilder sb = new StringBuilder();

                                if (message != null)
                                {
                                    sb.Append(message);
                                    sb.Append("\n");
                                }

                                if (Config.CheckVariable(o, "Fall Date Day", 1, Data.MonthsArray[emo.SpringDate.Month - 1].TotalDays, typeof(int), ref success, ref message))
                                {
                                    int value = (int)o;

                                    emo.FallDate.Day = value;

                                    message = Formatting.VariableMessageFormatter(String.Format("EMO #{0}", emo.Index), "Fall Date Day", value.ToString());
                                }

                                sb.Append(message);

                                message = sb.ToString();
                            }

                            break;
                        }
                    case EffectsMapType.WinterDate:
                        {
                            if (valueTwo == null)
                            {
                                success = false;
                                message = Syntax.GetSyntax(true, Command.SetEmo);

                                break;
                            }

                            {
                                object o = Support.GetValue(valueOne);

                                if (Config.CheckVariable(o, "Winter Date Month", 1, Data.MonthsArray.Count, typeof(int), ref success, ref message))
                                {
                                    int value = (int)o;

                                    emo.WinterDate.Month = value;

                                    message = Formatting.VariableMessageFormatter(String.Format("EMO #{0}", emo.Index), "Winter Date Month", value.ToString());
                                }
                            }

                            if (success)
                            {
                                object o = Support.GetValue(valueTwo);

                                StringBuilder sb = new StringBuilder();

                                if (message != null)
                                {
                                    sb.Append(message);
                                    sb.Append("\n");
                                }

                                if (Config.CheckVariable(o, "Winter Date Day", 1, Data.MonthsArray[emo.SpringDate.Month - 1].TotalDays, typeof(int), ref success, ref message))
                                {
                                    int value = (int)o;

                                    emo.WinterDate.Day = value;

                                    message = Formatting.VariableMessageFormatter(String.Format("EMO #{0}", emo.Index), "Winter Date Day", value.ToString());
                                }

                                sb.Append(message);

                                message = sb.ToString();
                            }

                            break;
                        }
                    case EffectsMapType.UseDarkestHour:
                        {
                            if (valueTwo != null)
                            {
                                success = false;
                                message = Syntax.GetSyntax(true, Command.SetEmo);

                                break;
                            }

                            object o = Support.GetValue(valueOne);

                            if (Config.CheckVariable(o, emoType.ToString(), false, true, typeof(bool), ref success, ref message))
                            {
                                bool value = (bool)o;

                                emo.UseDarkestHour = value;

                                message = Formatting.VariableMessageFormatter(String.Format("EMO #{0}", emo.Index), emoType.ToString(), value.ToString());
                            }

                            break;
                        }
                    case EffectsMapType.UseAutoLighting:
                        {
                            if (valueTwo != null)
                            {
                                success = false;
                                message = Syntax.GetSyntax(true, Command.SetEmo);

                                break;
                            }

                            object o = Support.GetValue(valueOne);

                            if (Config.CheckVariable(o, emoType.ToString(), false, true, typeof(bool), ref success, ref message))
                            {
                                bool value = (bool)o;

                                emo.UseAutoLighting = value;

                                message = Formatting.VariableMessageFormatter(String.Format("EMO #{0}", emo.Index), emoType.ToString(), value.ToString());
                            }

                            break;
                        }
                    case EffectsMapType.UseRandomLightOutage:
                        {
                            if (valueTwo != null)
                            {
                                success = false;
                                message = Syntax.GetSyntax(true, Command.SetEmo);

                                break;
                            }

                            object o = Support.GetValue(valueOne);

                            if (Config.CheckVariable(o, emoType.ToString(), false, true, typeof(bool), ref success, ref message))
                            {
                                bool value = (bool)o;

                                emo.UseRandomLightOutage = value;

                                message = Formatting.VariableMessageFormatter(String.Format("EMO #{0}", emo.Index), emoType.ToString(), value.ToString());
                            }

                            break;
                        }
                    case EffectsMapType.LightOutageChancePerTick:
                        {
                            if (valueTwo != null)
                            {
                                success = false;
                                message = Syntax.GetSyntax(true, Command.SetEmo);

                                break;
                            }

                            object o = Support.GetValue(valueOne);

                            if (Config.CheckVariable(o, emoType.ToString(), 0, 100, typeof(int), ref success, ref message))
                            {
                                int value = (int)o;

                                emo.LightOutageChancePerTick = value;

                                message = Formatting.VariableMessageFormatter(String.Format("EMO #{0}", emo.Index), emoType.ToString(), String.Format("{0}%", value));
                            }

                            break;
                        }
                    case EffectsMapType.UseNightSightDarkestHourOverride:
                        {
                            if (valueTwo != null)
                            {
                                success = false;
                                message = Syntax.GetSyntax(true, Command.SetEmo);

                                break;
                            }

                            object o = Support.GetValue(valueOne);

                            if (Config.CheckVariable(o, emoType.ToString(), false, true, typeof(bool), ref success, ref message))
                            {
                                bool value = (bool)o;

                                emo.UseNightSightDarkestHourOverride = value;

                                message = Formatting.VariableMessageFormatter(String.Format("EMO #{0}", emo.Index), emoType.ToString(), value.ToString());
                            }

                            break;
                        }
                    case EffectsMapType.NightSightDarkestHourReduction:
                        {
                            if (valueTwo != null)
                            {
                                success = false;
                                message = Syntax.GetSyntax(true, Command.SetEmo);

                                break;
                            }

                            object o = Support.GetValue(valueOne);

                            if (Config.CheckVariable(o, emoType.ToString(), 0, 100, typeof(int), ref success, ref message))
                            {
                                int value = (int)o;

                                emo.NightSightDarkestHourReduction = value;

                                message = Formatting.VariableMessageFormatter(String.Format("EMO #{0}", emo.Index), emoType.ToString(), String.Format("{0}%", value));
                            }

                            break;
                        }
                    case EffectsMapType.UseNightSightOverride:
                        {
                            if (valueTwo != null)
                            {
                                success = false;
                                message = Syntax.GetSyntax(true, Command.SetEmo);

                                break;
                            }

                            object o = Support.GetValue(valueOne);

                            if (Config.CheckVariable(o, emoType.ToString(), false, true, typeof(bool), ref success, ref message))
                            {
                                bool value = (bool)o;

                                emo.UseNightSightOverride = value;

                                message = Formatting.VariableMessageFormatter(String.Format("EMO #{0}", emo.Index), emoType.ToString(), value.ToString());
                            }

                            break;
                        }
                    case EffectsMapType.NightSightLevelReduction:
                        {
                            if (valueTwo != null)
                            {
                                success = false;
                                message = Syntax.GetSyntax(true, Command.SetEmo);

                                break;
                            }

                            object o = Support.GetValue(valueOne);

                            if (Config.CheckVariable(o, emoType.ToString(), 0, 100, typeof(int), ref success, ref message))
                            {
                                int value = (int)o;

                                emo.NightSightLevelReduction = value;

                                message = Formatting.VariableMessageFormatter(String.Format("EMO #{0}", emo.Index), emoType.ToString(), String.Format("{0}%", value));
                            }

                            break;
                        }
                    case EffectsMapType.UseLightLevelOverride:
                        {
                            if (valueTwo != null)
                            {
                                success = false;
                                message = Syntax.GetSyntax(true, Command.SetEmo);

                                break;
                            }

                            object o = Support.GetValue(valueOne);

                            if (Config.CheckVariable(o, emoType.ToString(), false, true, typeof(bool), ref success, ref message))
                            {
                                bool value = (bool)o;

                                emo.UseLightLevelOverride = value;

                                message = Formatting.VariableMessageFormatter(String.Format("EMO #{0}", emo.Index), emoType.ToString(), value.ToString());
                            }

                            break;
                        }
                    case EffectsMapType.LightLevelOverrideAdjust:
                        {
                            if (valueTwo != null)
                            {
                                success = false;
                                message = Syntax.GetSyntax(true, Command.SetEmo);

                                break;
                            }

                            object o = Support.GetValue(valueOne);

                            if (Config.CheckVariable(o, emoType.ToString(), Data.MinLightLevel, Data.MaxLightLevel, typeof(int), ref success, ref message))
                            {
                                int value = (int)o;

                                emo.LightLevelOverrideAdjust = value;

                                message = Formatting.VariableMessageFormatter(String.Format("EMO #{0}", emo.Index), emoType.ToString(), value.ToString());
                            }

                            break;
                        }
                    case EffectsMapType.UseMurdererDarkestHourBonus:
                        {
                            if (valueTwo != null)
                            {
                                success = false;
                                message = Syntax.GetSyntax(true, Command.SetEmo);

                                break;
                            }

                            object o = Support.GetValue(valueOne);

                            if (Config.CheckVariable(o, emoType.ToString(), false, true, typeof(bool), ref success, ref message))
                            {
                                bool value = (bool)o;

                                emo.UseMurdererDarkestHourBonus = value;

                                message = Formatting.VariableMessageFormatter(String.Format("EMO #{0}", emo.Index), emoType.ToString(), value.ToString());
                            }

                            break;
                        }
                    case EffectsMapType.MurdererDarkestHourLevelBonus:
                        {
                            if (valueTwo != null)
                            {
                                success = false;
                                message = Syntax.GetSyntax(true, Command.SetEmo);

                                break;
                            }

                            object o = Support.GetValue(valueOne);

                            if (Config.CheckVariable(o, emoType.ToString(), Data.MinLightLevel, Data.MaxLightLevel, typeof(int), ref success, ref message))
                            {
                                int value = (int)o;

                                emo.MurdererDarkestHourLevelBonus = value;

                                message = Formatting.VariableMessageFormatter(String.Format("EMO #{0}", emo.Index), emoType.ToString(), value.ToString());
                            }

                            break;
                        }
                    case EffectsMapType.UseEvilSpawners:
                        {
                            if (valueTwo != null)
                            {
                                success = false;
                                message = Syntax.GetSyntax(true, Command.SetEmo);

                                break;
                            }

                            object o = Support.GetValue(valueOne);

                            if (Config.CheckVariable(o, emoType.ToString(), false, true, typeof(bool), ref success, ref message))
                            {
                                bool value = (bool)o;

                                emo.UseEvilSpawners = value;

                                message = Formatting.VariableMessageFormatter(String.Format("EMO #{0}", emo.Index), emoType.ToString(), value.ToString());
                            }

                            break;
                        }
                }

                if (success)
                {
                    Data.EffectsMapArray[emo.Index] = emo;
                }

                vo.Success = success;
                vo.Message = message;
            }

            return vo;
        }

        public static VariableObject GetEmo(Mobile mobile, string index)
        {
            VariableObject vo = new VariableObject();

            if (Data.EffectsMapArray.Count == 0)
            {
                vo.Success = false;
                vo.Message = "There are no EffectsMapObjects!";

                return vo;
            }

            lock (Data.EffectsMapArray)
            {
                bool success = false;
                string message = null;

                EffectsMapObject emo = null;

                if (index == null)
                {
                    success = true;

                    List<EffectsMapObject> emoArray = EffectsEngine.GetEffectsMapArray(mobile.Map, mobile.X, mobile.Y, true);
                    List<EffectsExclusionMapObject> eemoArray = EffectsEngine.GetEffectsExclusionMapArray(mobile.Map, mobile.X, mobile.Y, true);

                    if (emoArray.Count > 0)
                    {
                        EffectsMapObject emoParentSeason = null;

                        for (int i = 0; i < emoArray.Count; i++)
                        {
                            EffectsMapObject effectsMap = emoArray[i];

                            if (effectsMap.Enabled && effectsMap.UseSeasons)
                            {
                                emoParentSeason = effectsMap;

                                break;
                            }
                        }

                        StringBuilder sb = new StringBuilder();

                        sb.Append("Legend:\n* - EMO with highest priority\n@ - EMO with season priority\n! - Disabled EMO\n\n");
                        sb.Append("You are in the following EMO bounds in priortized order:\n");

                        if (emoArray.Count > 1)
                        {
                            for (int i = 0; i < emoArray.Count; i++)
                            {
                                if (i + 1 < emoArray.Count)
                                {
                                    sb.Append(String.Format("{0}{1}{2}{3}, ", emoArray[i].Index, i == 0 ? "*" : "", emoArray[i] == emoParentSeason ? "@" : "", !emoArray[i].Enabled ? "!" : ""));
                                }
                                else
                                {
                                    sb.Append(String.Format("{0}{1}{2}{3}", emoArray[i].Index, i == 0 ? "*" : "", emoArray[i] == emoParentSeason ? "@" : "", !emoArray[i].Enabled ? "!" : ""));
                                }
                            }
                        }
                        else
                        {
                            sb.Append(String.Format("{0}*{1}{2}", emoArray[0].Index, emoArray[0].UseSeasons ? "@" : "", !emoArray[0].Enabled ? "!" : ""));
                        }

                        if (eemoArray.Count > 0 && emoArray.Count > 0 && eemoArray[0].Priority >= emoArray[0].Priority)
                        {
                            sb.Append(String.Format("\n\nEEMO #{0} has priority over EMO #{1}.", eemoArray[0].Index, emoArray[0].Index));
                        }

                        message = sb.ToString();
                    }
                    else
                    {
                        message = "You are not in an area that is under any effects!";
                    }
                }
                else
                {
                    object o = Support.GetValue(index);

                    Type typeExpected = typeof(int);

                    int lowValue = 0;
                    int highValue = Data.EffectsMapArray.Count - 1;

                    string minValue = Convert.ToString(lowValue);
                    string maxValue = Convert.ToString(highValue);

                    if (o is int)
                    {
                        int value = (int)o;

                        if (value >= lowValue && value <= highValue)
                        {
                            success = true;

                            emo = Data.EffectsMapArray[value];

                            int height = emo.Y2 - emo.Y1;

                            int outerLatitudeHeight = (int)(height * emo.OuterLatitudePercent);
                            int innerLatitudeHeight = (int)(height * emo.InnerLatitudePercent);
                            int middleLatitude = emo.Y1 + (int)(height / 2);

                            int upperOuterLowRange = emo.Y1;
                            int upperOuterHighRange = emo.Y1 + outerLatitudeHeight;

                            int lowerOuterLowRange = emo.Y1 + (height - outerLatitudeHeight);
                            int lowerOuterHighRange = emo.Y1 + height;

                            int innerLowRange = middleLatitude - innerLatitudeHeight;
                            int innerHighRange = middleLatitude + innerLatitudeHeight;

                            StringBuilder sb = new StringBuilder();

                            sb.Append(String.Format("EMO #{0} [Priority: {1}]: Bounds: ({2}, {3}) to ({4}, {5}) on map '{6}'.\n", emo.Index, emo.Priority, emo.X1, emo.Y1, emo.X2, emo.Y2, emo.Map));
                            sb.Append(String.Format("Enabled: {0}\n", emo.Enabled));
                            sb.Append(String.Format("UseLatitude: {0}\nOuter Percent: {1}%\nInner Percent: {2}%\n", emo.UseLatitude, emo.OuterLatitudePercent * 100, emo.InnerLatitudePercent * 100));
                            sb.Append(String.Format("Middle Latitude: {0}\n", middleLatitude));
                            sb.Append(String.Format("Upper Outer Range: {0} - {1}\n", upperOuterLowRange, upperOuterHighRange));
                            sb.Append(String.Format("Lower Outer Range: {0} - {1}\n", lowerOuterLowRange, lowerOuterHighRange));
                            sb.Append(String.Format("Inner Range: {0} - {1}\n", innerLowRange, innerHighRange));
                            sb.Append(String.Format("UseSeasons: {0}\nStatic Season: {1}\n", emo.UseSeasons, emo.StaticSeason));
                            sb.Append(String.Format("Spring Date: {0}/{1}\n", emo.SpringDate.Month, emo.SpringDate.Day));
                            sb.Append(String.Format("Summer Date: {0}/{1}\n", emo.SummerDate.Month, emo.SummerDate.Day));
                            sb.Append(String.Format("Fall Date: {0}/{1}\n", emo.FallDate.Month, emo.FallDate.Day));
                            sb.Append(String.Format("Winter Date: {0}/{1}\n", emo.WinterDate.Month, emo.WinterDate.Day));
                            sb.Append(String.Format("UseDarkestHour: {0}\n", emo.UseDarkestHour));
                            sb.Append(String.Format("UseAutoLighting: {0}\n", emo.UseAutoLighting));
                            sb.Append(String.Format("UseRandomLightOutage: {0}\n", emo.UseRandomLightOutage));
                            sb.Append(String.Format("LightOutageChancePerTick: {0}%\n", emo.LightOutageChancePerTick));
                            sb.Append(String.Format("UseNightSightDarkestHourOverride: {0}\n", emo.UseNightSightDarkestHourOverride));
                            sb.Append(String.Format("NightSightDarkestHourReduction: {0}%\n", emo.NightSightDarkestHourReduction));
                            sb.Append(String.Format("UseNightSightOverride: {0}\n", emo.UseNightSightOverride));
                            sb.Append(String.Format("NightSightLevelReduction: {0}%\n", emo.NightSightLevelReduction));
                            sb.Append(String.Format("UseLightLevelOverride: {0}\n", emo.UseLightLevelOverride));
                            sb.Append(String.Format("LightLevelOverrideAdjust: {0}%\n", emo.LightLevelOverrideAdjust));
                            sb.Append(String.Format("UseMurdererDarkestHourBonus: {0}\n", emo.UseMurdererDarkestHourBonus));
                            sb.Append(String.Format("MurdererDarkestHourLevelBonus: {0}%\n", emo.MurdererDarkestHourLevelBonus));
                            sb.Append(String.Format("UseEvilSpawners: {0}\n", emo.UseEvilSpawners));

                            message = sb.ToString();
                        }
                        else
                        {
                            StringBuilder sb = new StringBuilder();

                            sb.Append(Formatting.ErrorMessageFormatter("EMO Number", o, minValue, maxValue));

                            message = sb.ToString();
                        }
                    }
                    else
                    {
                        StringBuilder sb = new StringBuilder();

                        sb.Append(Formatting.ErrorMessageFormatter("EMO Number", o, minValue, maxValue, typeExpected));

                        message = sb.ToString();
                    }
                }

                vo.Success = success;
                vo.Message = message;
            }

            return vo;
        }

        public static VariableObject RemoveEmo(string index)
        {
            VariableObject vo = new VariableObject();

            if (Support.CheckForceScriptSettings(ref vo, "Remove EffectsMapObject"))
            {
                return vo;
            }

            if (Data.DataFileInUse)
            {
                vo.Success = false;
                vo.Message = Data.DataFileInUseMessage;

                return vo;
            }

            if (Data.EffectsMapArray.Count == 0)
            {
                vo.Success = false;
                vo.Message = "There are no EffectsMapObjects!";

                return vo;
            }

            lock (Data.EffectsMapArray)
            {
                bool success = false;
                string message = null;

                object o = Support.GetValue(index);

                Type typeExpected = typeof(int);

                int lowValue = 0;
                int highValue = Data.EffectsMapArray.Count - 1;

                string minValue = Convert.ToString(lowValue);
                string maxValue = Convert.ToString(highValue);

                if (o is int)
                {
                    int value = (int)o;

                    if (value >= lowValue && value <= highValue)
                    {
                        Data.EffectsMapArray.RemoveAt(value);
                        Data.EffectsMapArray.TrimExcess();

                        Support.ReIndexArray(Data.EffectsMapArray);

                        message = String.Format("EMO #{0} has been removed.  All succeeding EMO indexes have moved up.", value);
                    }
                    else
                    {
                        StringBuilder sb = new StringBuilder();

                        sb.Append(Formatting.ErrorMessageFormatter("EMO Number", o, minValue, maxValue));

                        message = sb.ToString();
                    }
                }
                else
                {
                    StringBuilder sb = new StringBuilder();

                    sb.Append(Formatting.ErrorMessageFormatter("EMO Number", o, minValue, maxValue, typeExpected));

                    message = sb.ToString();
                }

                vo.Success = success;
                vo.Message = message;
            }

            return vo;
        }

        public static VariableObject ToggleEmo(string index)
        {
            VariableObject vo = new VariableObject();

            if (Support.CheckForceScriptSettings(ref vo, "Toggle EffectsMapObject"))
            {
                return vo;
            }

            if (Data.DataFileInUse)
            {
                vo.Success = false;
                vo.Message = Data.DataFileInUseMessage;

                return vo;
            }

            if (Data.EffectsMapArray.Count == 0)
            {
                vo.Success = false;
                vo.Message = "There are no EffectsMapObjects!";

                return vo;
            }

            lock (Data.EffectsMapArray)
            {
                bool success = false;
                string message = null;

                object o = Support.GetValue(index);

                Type typeExpected = typeof(int);

                int lowValue = 0;
                int highValue = Data.EffectsMapArray.Count - 1;

                string minValue = Convert.ToString(lowValue);
                string maxValue = Convert.ToString(highValue);

                if (o is int)
                {
                    int value = (int)o;

                    if (value >= lowValue && value <= highValue)
                    {
                        bool enabled = Data.EffectsMapArray[value].Enabled;

                        Data.EffectsMapArray[value].Enabled = !enabled;

                        message = String.Format("EMO #{0} has been {1}.", value, !enabled ? "enabled" : "disabled");
                    }
                    else
                    {
                        StringBuilder sb = new StringBuilder();

                        sb.Append(Formatting.ErrorMessageFormatter("EMO Number", o, minValue, maxValue));

                        message = sb.ToString();
                    }
                }
                else
                {
                    StringBuilder sb = new StringBuilder();

                    sb.Append(Formatting.ErrorMessageFormatter("EMO Number", o, minValue, maxValue, typeExpected));

                    message = sb.ToString();
                }

                vo.Success = success;
                vo.Message = message;
            }

            return vo;
        }

        #endregion

        #region Effects Exclusion Maps

        public static VariableObject AddEemo(Mobile mobile)
        {
            VariableObject vo = new VariableObject();

            if (Support.CheckForceScriptSettings(ref vo, "Add EffectsExclusionMapObject"))
            {
                return vo;
            }

            if (Data.DataFileInUse)
            {
                vo.Success = false;
                vo.Message = Data.DataFileInUseMessage;

                return vo;
            }

            bool success = false;
            string message = null;

            EffectsExclusionMapObject eemo = null;

            int index = -1;

            lock (Data.EffectsExclusionMapArray)
            {
                eemo = Config.SetDefaultEffectsExclusionValues(new EffectsExclusionMapObject(mobile.Map, -1, -1, -1, -1));

                index = Data.EffectsExclusionMapArray.Count;

                eemo.Index = index;

                Data.EffectsExclusionMapArray.Add(eemo);

                success = true;
            }

            StringBuilder sb = new StringBuilder();

            sb.Append(String.Format("EEMO #{0} has been added!\n", index));

            message = sb.ToString();

            vo.Success = success;
            vo.Message = message;

            return vo;
        }

        public static VariableObject SetEemo(string index, string type, string valueOne, string valueTwo)
        {
            VariableObject vo = new VariableObject();

            if (Support.CheckForceScriptSettings(ref vo, "Set EffectsExclusionMapObject"))
            {
                return vo;
            }

            if (Data.DataFileInUse)
            {
                vo.Success = false;
                vo.Message = Data.DataFileInUseMessage;

                return vo;
            }

            lock (Data.EffectsExclusionMapArray)
            {
                bool success = false;
                string message = null;

                EffectsExclusionMapObject eemo = null;

                {
                    object o = Support.GetValue(index);

                    if (Config.CheckVariable(o, "Number", 0, Data.EffectsExclusionMapArray.Count - 1, typeof(int), ref success, ref message))
                    {
                        int value = (int)o;

                        eemo = Data.EffectsExclusionMapArray[value];
                    }
                }

                if (eemo == null)
                {
                    vo.Success = false;
                    vo.Message = message;

                    return vo;
                }

                EffectsExclusionMapType eemoType = Support.GetEemoTypeFromName(type);

                switch (eemoType)
                {
                    default:
                        {
                            success = false;
                            message = "That EEMO type does not exist!";

                            break;
                        }
                    case EffectsExclusionMapType.Priority:
                        {
                            if (valueTwo != null)
                            {
                                success = false;
                                message = Syntax.GetSyntax(true, Command.SetEemo);

                                break;
                            }

                            object o = Support.GetValue(valueOne);

                            if (Config.CheckVariable(o, eemoType.ToString(), 0, int.MaxValue, typeof(int), ref success, ref message))
                            {
                                int value = (int)o;

                                eemo.Priority = value;

                                message = Formatting.VariableMessageFormatter(String.Format("EEMO #{0}", eemo.Index), eemoType.ToString(), value.ToString());
                            }

                            break;
                        }
                    case EffectsExclusionMapType.Map:
                        {
                            if (valueTwo != null)
                            {
                                success = false;
                                message = Syntax.GetSyntax(true, Command.SetEemo);

                                break;
                            }

                            Map map = Support.GetMapFromName(valueOne, false);

                            if (map == null)
                            {
                                message = Formatting.ErrorMessageFormatter(eemoType.ToString(), valueOne, Support.GetMapList(false));

                                break;
                            }

                            success = true;

                            eemo.Map = map;

                            message = Formatting.VariableMessageFormatter(String.Format("EEMO #{0}", eemo.Index), eemoType.ToString(), map.Name);

                            break;
                        }
                    case EffectsExclusionMapType.X1Y1:
                        {
                            bool overallSuccess = true;

                            if (valueTwo == null)
                            {
                                success = false;
                                message = Syntax.GetSyntax(true, Command.SetEemo);

                                break;
                            }

                            int x1 = -1, y1 = -1;

                            {
                                object o = Support.GetValue(valueOne);

                                if (Config.CheckVariable(o, "X1", 0, eemo.X2 - 1, typeof(int), ref success, ref message))
                                {
                                    int value = (int)o;

                                    x1 = value;
                                }
                                else
                                {
                                    overallSuccess = false;
                                }
                            }

                            {
                                object o = Support.GetValue(valueTwo);

                                StringBuilder sb = new StringBuilder();

                                if (message != null)
                                {
                                    sb.Append(message);
                                    sb.Append("\n");
                                }

                                if (Config.CheckVariable(o, "Y1", 0, eemo.Y2 - 1, typeof(int), ref success, ref message))
                                {
                                    int value = (int)o;

                                    y1 = value;
                                }
                                else
                                {
                                    overallSuccess = false;

                                    sb.Append(message);
                                }

                                message = sb.ToString();
                            }

                            if (overallSuccess)
                            {
                                eemo.X1 = x1;
                                eemo.Y1 = y1;

                                string value = String.Format("{0}, {1}", x1, y1);

                                message = Formatting.VariableMessageFormatter(String.Format("EEMO #{0}", eemo.Index), eemoType.ToString(), value);
                            }

                            break;
                        }
                    case EffectsExclusionMapType.X2Y2:
                        {
                            bool overallSuccess = true;

                            if (valueTwo == null)
                            {
                                success = false;
                                message = Syntax.GetSyntax(true, Command.SetEemo);

                                break;
                            }

                            int x2 = -1, y2 = -1;

                            {
                                object o = Support.GetValue(valueOne);

                                if (Config.CheckVariable(o, "X2", eemo.X1 + 1, eemo.Map.Width, typeof(int), ref success, ref message))
                                {
                                    int value = (int)o;

                                    x2 = value;
                                }
                                else
                                {
                                    overallSuccess = false;
                                }
                            }

                            {
                                object o = Support.GetValue(valueTwo);

                                StringBuilder sb = new StringBuilder();

                                if (message != null)
                                {
                                    sb.Append(message);
                                    sb.Append("\n");
                                }

                                if (Config.CheckVariable(o, "Y2", eemo.Y1 + 1, eemo.Map.Height, typeof(int), ref success, ref message))
                                {
                                    int value = (int)o;

                                    y2 = value;
                                }
                                else
                                {
                                    overallSuccess = false;

                                    sb.Append(message);
                                }

                                message = sb.ToString();
                            }

                            if (overallSuccess)
                            {
                                eemo.X2 = x2;
                                eemo.Y2 = y2;

                                string value = String.Format("{0}, {1}", x2, y2);

                                message = Formatting.VariableMessageFormatter(String.Format("EEMO #{0}", eemo.Index), eemoType.ToString(), value);
                            }

                            break;
                        }
                }

                if (success)
                {
                    Data.EffectsExclusionMapArray[eemo.Index] = eemo;
                }

                vo.Success = success;
                vo.Message = message;
            }

            return vo;
        }

        public static VariableObject GetEemo(Mobile mobile, string index)
        {
            VariableObject vo = new VariableObject();

            if (Data.EffectsExclusionMapArray.Count == 0)
            {
                vo.Success = false;
                vo.Message = "There are no EffectsExclusionMapObjects!";

                return vo;
            }

            lock (Data.EffectsExclusionMapArray)
            {
                bool success = false;
                string message = null;

                EffectsExclusionMapObject eemo = null;

                if (index == null)
                {
                    success = true;

                    List<EffectsExclusionMapObject> eemoArray = EffectsEngine.GetEffectsExclusionMapArray(mobile.Map, mobile.X, mobile.Y, true);
                    List<EffectsMapObject> emoArray = EffectsEngine.GetEffectsMapArray(mobile.Map, mobile.X, mobile.Y, true);

                    if (eemoArray.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder();

                        sb.Append("Legend:\n* - EEMO with highest priority\n! - Disabled EEMO\n\n");
                        sb.Append("You are in the following EEMO bounds in priortized order:\n");

                        if (eemoArray.Count > 1)
                        {
                            for (int i = 0; i < eemoArray.Count; i++)
                            {
                                if (i + 1 < eemoArray.Count)
                                {
                                    sb.Append(String.Format("{0}{1}{2}, ", eemoArray[i].Index, i == 0 ? "*" : "", !eemoArray[i].Enabled ? "!" : ""));
                                }
                                else
                                {
                                    sb.Append(String.Format("{0}{1}{2}", eemoArray[i].Index, i == 0 ? "*" : "", !eemoArray[i].Enabled ? "!" : ""));
                                }
                            }
                        }
                        else
                        {
                            sb.Append(String.Format("{0}*{1}", eemoArray[0].Index, !eemoArray[0].Enabled ? "!" : ""));
                        }

                        if (emoArray.Count > 0 && eemoArray.Count > 0 && emoArray[0].Priority > eemoArray[0].Priority)
                        {
                            sb.Append(String.Format("\n\nEMO #{0} has priority over EEMO #{1}.", emoArray[0].Index, eemoArray[0].Index));
                        }

                        message = sb.ToString();
                    }
                    else
                    {
                        message = "You are not in an area that is under any effect exclusions!";
                    }
                }
                else
                {
                    object o = Support.GetValue(index);

                    Type typeExpected = typeof(int);

                    int lowValue = 0;
                    int highValue = Data.EffectsExclusionMapArray.Count - 1;

                    string minValue = Convert.ToString(lowValue);
                    string maxValue = Convert.ToString(highValue);

                    if (o is int)
                    {
                        int value = (int)o;

                        if (value >= lowValue && value <= highValue)
                        {
                            success = true;

                            eemo = Data.EffectsExclusionMapArray[value];

                            StringBuilder sb = new StringBuilder();

                            sb.Append(String.Format("EEMO #{0} [Priority: {1}]: Bounds: ({2}, {3}) to ({4}, {5}) on map '{6}'.\n", eemo.Index, eemo.Priority, eemo.X1, eemo.Y1, eemo.X2, eemo.Y2, eemo.Map));
                            sb.Append(String.Format("Enabled: {0}\n", eemo.Enabled));

                            message = sb.ToString();

                        }
                        else
                        {
                            StringBuilder sb = new StringBuilder();

                            sb.Append(Formatting.ErrorMessageFormatter("EEMO Number", o, minValue, maxValue));

                            message = sb.ToString();
                        }
                    }
                    else
                    {
                        StringBuilder sb = new StringBuilder();

                        sb.Append(Formatting.ErrorMessageFormatter("EEMO Number", o, minValue, maxValue, typeExpected));

                        message = sb.ToString();
                    }
                }

                vo.Success = success;
                vo.Message = message;
            }

            return vo;
        }

        public static VariableObject RemoveEemo(string index)
        {
            VariableObject vo = new VariableObject();

            if (Support.CheckForceScriptSettings(ref vo, "Remove EffectsExclusionMapObject"))
            {
                return vo;
            }

            if (Data.DataFileInUse)
            {
                vo.Success = false;
                vo.Message = Data.DataFileInUseMessage;

                return vo;
            }

            if (Data.EffectsExclusionMapArray.Count == 0)
            {
                vo.Success = false;
                vo.Message = "There are no EffectsExclusionMapObjects!";

                return vo;
            }

            lock (Data.EffectsExclusionMapArray)
            {
                bool success = false;
                string message = null;

                object o = Support.GetValue(index);

                Type typeExpected = typeof(int);

                int lowValue = 0;
                int highValue = Data.EffectsExclusionMapArray.Count - 1;

                string minValue = Convert.ToString(lowValue);
                string maxValue = Convert.ToString(highValue);

                if (o is int)
                {
                    int value = (int)o;

                    if (value >= lowValue && value <= highValue)
                    {
                        Data.EffectsExclusionMapArray.RemoveAt(value);
                        Data.EffectsExclusionMapArray.TrimExcess();

                        Support.ReIndexArray(Data.EffectsExclusionMapArray);

                        message = String.Format("EEMO #{0} has been removed.  All succeeding EEMO indexes have moved up.", value);
                    }
                    else
                    {
                        StringBuilder sb = new StringBuilder();

                        sb.Append(Formatting.ErrorMessageFormatter("EEMO Number", o, minValue, maxValue));

                        message = sb.ToString();
                    }
                }
                else
                {
                    StringBuilder sb = new StringBuilder();

                    sb.Append(Formatting.ErrorMessageFormatter("EEMO Number", o, minValue, maxValue, typeExpected));

                    message = sb.ToString();
                }

                vo.Success = success;
                vo.Message = message;
            }

            return vo;
        }

        public static VariableObject ToggleEemo(string index)
        {
            VariableObject vo = new VariableObject();

            if (Support.CheckForceScriptSettings(ref vo, "Toggle EffectsExclusionMapObject"))
            {
                return vo;
            }

            if (Data.DataFileInUse)
            {
                vo.Success = false;
                vo.Message = Data.DataFileInUseMessage;

                return vo;
            }

            if (Data.EffectsExclusionMapArray.Count == 0)
            {
                vo.Success = false;
                vo.Message = "There are no EffectsExclusionMapObjects!";

                return vo;
            }

            lock (Data.EffectsExclusionMapArray)
            {
                bool success = false;
                string message = null;

                object o = Support.GetValue(index);

                Type typeExpected = typeof(int);

                int lowValue = 0;
                int highValue = Data.EffectsExclusionMapArray.Count - 1;

                string minValue = Convert.ToString(lowValue);
                string maxValue = Convert.ToString(highValue);

                if (o is int)
                {
                    int value = (int)o;

                    if (value >= lowValue && value <= highValue)
                    {
                        bool enabled = Data.EffectsExclusionMapArray[value].Enabled;

                        Data.EffectsExclusionMapArray[value].Enabled = !enabled;

                        message = String.Format("EEMO #{0} has been {1}.", value, !enabled ? "enabled" : "disabled");
                    }
                    else
                    {
                        StringBuilder sb = new StringBuilder();

                        sb.Append(Formatting.ErrorMessageFormatter("EEMO Number", o, minValue, maxValue));

                        message = sb.ToString();
                    }
                }
                else
                {
                    StringBuilder sb = new StringBuilder();

                    sb.Append(Formatting.ErrorMessageFormatter("EEMO Number", o, minValue, maxValue, typeExpected));

                    message = sb.ToString();
                }

                vo.Success = success;
                vo.Message = message;
            }

            return vo;
        }

        #endregion
    }
}