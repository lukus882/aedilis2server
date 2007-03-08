using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Server;
using Server.Accounting;
using Server.Commands;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.TimeSystem
{
    public class Support
    {
        #region Private Variables

        private static int m_LastRandomSeed = DateTime.Now.Millisecond;

        #endregion

        #region Calculated Variables

        private static int RandomSeed
        {
            get
            {
                int randomSeed = new Random(m_LastRandomSeed).Next();
                m_LastRandomSeed = new Random(randomSeed).Next();

                return randomSeed;
            }
        }

        #endregion

        #region Logging

        public static void ConsoleWrite(string text)
        {
            Console.Write(text);

            WriteToLogFile(text, false);
        }

        public static void ConsoleWriteLine(string text)
        {
            Console.WriteLine(text);

            WriteToLogFile(text, true);
        }

        public static void WriteToLogFile(string text, bool writeLine)
        {
            if (Data.Logging)
            {
                try
                {
                    if (writeLine)
                    {
                        text = String.Format("{0}\r\n", text);
                    }

                    lock (Data.LogWriter)
                    {
                        Data.LogWriter.Write(String.Format("{0}$ {1}", DateTime.Now, text));
                        Data.LogWriter.Flush();
                    }
                }
                catch (Exception e)
                {
                    Data.Logging = false;
                    Data.LogWriter = null;

                    Console.WriteLine(String.Format("Time System: Unable to write to log file \"{0}\"!  Logging has been disabled.\r\n{1}", Data.LogFileName, e.ToString()));
                }
            }
        }

        public static void WriteToLogFile(CommandEventArgs e)
        {
            Mobile mobile = e.Mobile;

            string mobileName = mobile.Name;
            string accountName = ((Account)mobile.Account).Username;

            WriteToLogFile(String.Format("Account [{0}] - Mobile [{1}]: {2}{3} {4}", accountName, mobileName, CommandSystem.Prefix, e.Command, e.ArgString), true);
        }

        public static void OpenLogFile()
        {
            if (!Data.Logging)
            {
                ConsoleWriteLine("Time System: Logging is disabled.");

                return;
            }

            CheckLogPath();

            try
            {
                if (File.Exists(Data.LogFile))
                {
                    File.SetAttributes(Data.LogFile, FileAttributes.Normal);
                }

                Data.LogWriter = new StreamWriter(Data.LogFile, true);

                if (Data.Loading)
                {
                    WriteToLogFile(String.Format("Time System: Version {0} loading...", Data.Version), true);
                }

                ConsoleWriteLine("Time System: Logging is enabled.");
            }
            catch (Exception e)
            {
                Data.Logging = false;
                Data.LogWriter = null;

                ConsoleWriteLine(String.Format("Time System: Unable to open log file!  Logging has been disabled.\r\n{0}", e.ToString()));
            }
        }

        #endregion

        #region Send Methods

        public static void SendLightLevelUpdate(MobileObject mo)
        {
            Mobile mobile = mo.Mobile;

            mobile.NetState.Send(GlobalLightLevel.Instantiate(mo.LightLevel));
            mobile.NetState.Send(new PersonalLightLevel(mobile, mobile.LightLevel));
        }

        public static void SendClockData(Item item, Mobile mobile)
        {
            string text = Formatting.GetTimeFormat(mobile, Format.Clock);

            mobile.NetState.Send(new UnicodeMessage(item.Serial, item.ItemID, MessageType.Regular, 0x3B2, 3, "ENU", item.Name, text));
        }

        public static void SendSpyglassData(Mobile mobile)
        {
            mobile.LocalOverheadMessage(MessageType.Regular, 0x3B2, false, TimeSystem.Formatting.GetTimeFormat(mobile, Format.Spyglass));
        }

        public static void SendStream(Mobile mobile, string stream)
        {
            string[] streamSplit = stream.Split('\n');

            for (int i = 0; i < streamSplit.Length; i++)
            {
                mobile.SendMessage(streamSplit[i]);
            }
        }

        public static void SendCommandNames(Mobile mobile)
        {
            string[] commandNames = Enum.GetNames(typeof(Command));

            int count = 1;

            StringBuilder sb = new StringBuilder();

            sb.Append("List of commands:\n");

            for (int i = 1; i < commandNames.Length; i++)
            {
                string commandName = commandNames[i];

                if (i + 1 < commandNames.Length)
                {
                    sb.Append(String.Format("{0}, ", commandName));
                }
                else
                {
                    sb.Append(commandName);
                }

                if (count == Data.StreamSendLimit)
                {
                    count = 1;

                    mobile.SendMessage(sb.ToString());

                    sb = new StringBuilder();
                }
                else
                {
                    count++;
                }
            }

            if (sb.Length > 0)
            {
                mobile.SendMessage(sb.ToString());
            }
        }

        public static void SendVariableNames(Mobile mobile)
        {
            string[] variableNames = Enum.GetNames(typeof(Variable));

            int count = 1;

            StringBuilder sb = new StringBuilder();

            sb.Append("List of variables:\r\n");

            for (int i = 2; i < variableNames.Length; i++)
            {
                string name = variableNames[i];

                if (i + 1 < variableNames.Length)
                {
                    sb.Append(String.Format("{0}, ", name));
                }
                else
                {
                    sb.Append(name);
                }

                if (count == Data.StreamSendLimit)
                {
                    count = 1;

                    mobile.SendMessage(sb.ToString());

                    sb = new StringBuilder();
                }
                else
                {
                    count++;
                }
            }

            if (sb.Length > 0)
            {
                mobile.SendMessage(sb.ToString());
            }
        }

        #endregion

        #region Set Methods

        public static void WipeAllArrays()
        {
            Data.MonthsArray = new List<MonthPropsObject>();
            Data.MoonsArray = new List<MoonPropsObject>();
            Data.FacetArray = new List<FacetPropsObject>();
            Data.EffectsMapArray = new List<EffectsMapObject>();
            Data.EffectsExclusionMapArray = new List<EffectsExclusionMapObject>();
        }

        public static void ReIndexArray(object o)
        {
            if(o is List<EffectsMapObject>)
            {
                for (int i = 0; i < Data.EffectsMapArray.Count; i++)
                {
                    Data.EffectsMapArray[i].Index = i;
                }
            }
            else if (o is List<EffectsExclusionMapObject>)
            {
                for (int i = 0; i < Data.EffectsExclusionMapArray.Count; i++)
                {
                    Data.EffectsExclusionMapArray[i].Index = i;
                }
            }
        }

        #endregion

        #region Get Methods

        public static object GetValue(string value)
        {
            object o = null;

            if (value != null)
            {
                bool success = false;

                if (!success && value.IndexOf('.') > -1)
                {
                    try
                    {
                        o = double.Parse(value);

                        success = true;
                    }
                    catch { }
                }

                if (!success && (value.ToLower() == "true" || value.ToLower() == "false"))
                {
                    try
                    {
                        o = bool.Parse(value);

                        success = true;
                    }
                    catch { }
                }

                if (!success)
                {
                    try
                    {
                        o = int.Parse(value);

                        return o;
                    }
                    catch { }

                    o = value;
                }
            }

            return o;
        }

        public static object GetValue(string value, Type typeExpected)
        {
            object o = GetValue(value);

            if (typeExpected != null)
            {
                if (typeExpected == typeof(double))
                {
                    if (!(o is double))
                    {
                        return (double)0.0;
                    }
                }
                else if (typeExpected == typeof(bool))
                {
                    if (!(o is bool))
                    {
                        return false;
                    }
                }
                else if (typeExpected == typeof(int))
                {
                    if (!(o is int))
                    {
                        return (int)0;
                    }
                }
                else if (typeExpected == typeof(string))
                {
                    return o;
                }
            }

            return o;
        }

        public static Command GetCommandFromName(string commandName)
        {
            Command command = Command.None;

            commandName = commandName.ToUpper();

            int totalCommands = Enum.GetValues(typeof(Command)).Length;

            for (int i = 1; i < totalCommands; i++)
            {
                string name = ((Command)i).ToString().ToUpper();

                if (commandName == name)
                {
                    command = (Command)i;

                    break;
                }
            }

            return command;
        }

        public static Variable GetVariableFromName(string variableName)
        {
            Variable variable = Variable.None;

            variableName = variableName.ToUpper();

            int totalVariables = Enum.GetValues(typeof(Variable)).Length;

            for (int i = 1; i < totalVariables; i++)
            {
                string name = ((Variable)i).ToString().ToUpper();

                if (variableName == name)
                {
                    variable = (Variable)i;

                    break;
                }
            }

            return variable;
        }

        public static string GetEmoTypeNames()
        {
            string[] emoTypeNames = Enum.GetNames(typeof(EffectsMapType));

            int count = 1;

            StringBuilder sb = new StringBuilder();

            for (int i = 1; i < emoTypeNames.Length; i++)
            {
                string emoTypeName = emoTypeNames[i];

                if (i + 1 < emoTypeNames.Length)
                {
                    sb.Append(String.Format("{0}, ", emoTypeName));
                }
                else
                {
                    sb.Append(emoTypeName);
                }

                if (count == Data.StreamSendLimit)
                {
                    count = 1;

                    sb.Append("\n");
                }
                else
                {
                    count++;
                }
            }

            return sb.ToString();
        }

        public static string GetEemoTypeNames()
        {
            string[] eemoTypeNames = Enum.GetNames(typeof(EffectsExclusionMapType));

            int count = 1;

            StringBuilder sb = new StringBuilder();

            for (int i = 1; i < eemoTypeNames.Length; i++)
            {
                string eemoTypeName = eemoTypeNames[i];

                if (i + 1 < eemoTypeNames.Length)
                {
                    sb.Append(String.Format("{0}, ", eemoTypeName));
                }
                else
                {
                    sb.Append(eemoTypeName);
                }

                if (count == Data.StreamSendLimit)
                {
                    count = 1;

                    sb.Append("\n");
                }
                else
                {
                    count++;
                }
            }

            return sb.ToString();
        }

        public static int GetXAxis(object o)
        {
            int x = -1;

            if (o is Mobile)
            {
                x = ((Mobile)o).X;
            }
            else if (o is Item)
            {
                x = ((Item)o).X;
            }

            return x;
        }

        public static int GetYAxis(object o)
        {
            int y = -1;

            if (o is Mobile)
            {
                y = ((Mobile)o).Y;
            }
            else if (o is Item)
            {
                y = ((Item)o).Y;
            }

            return y;
        }

        public static Map GetMap(object o)
        {
            Map map = null;

            if (o is Mobile)
            {
                map = ((Mobile)o).Map;
            }
            else if (o is Item)
            {
                map = ((Item)o).Map;
            }

            return map;
        }

        public static int GetMapWidth(object o)
        {
            int width = -1;

            if (o is Mobile)
            {
                width = ((Mobile)o).Map.Width;
            }
            else if (o is Item)
            {
                width = ((Item)o).Map.Width;
            }
            else if (o is Map)
            {
                width = ((Map)o).Width;
            }

            return width;
        }

        public static int GetMapHeight(object o)
        {
            int height = -1;

            if (o is Mobile)
            {
                height = ((Mobile)o).Map.Height;
            }
            else if (o is Item)
            {
                height = ((Item)o).Map.Height;
            }
            else if (o is Map)
            {
                height = ((Map)o).Height;
            }

            return height;
        }

        public static int GetMapIndex(object o)
        {
            int index = -1;

            if (o is Mobile)
            {
                index = ((Mobile)o).Map.MapIndex;
            }
            else if (o is Item)
            {
                index = ((Item)o).Map.MapIndex;
            }
            else if (o is Map)
            {
                index = ((Map)o).MapIndex;
            }

            return index;
        }

        public static Map GetMapFromName(string mapName, bool includeInternal)
        {
            for (int i = 0; i < Map.Maps.Length; i++)
            {
                Map map = Map.Maps[i];

                string name = map.Name.ToLower();

                if (name == mapName.ToLower())
                {
                    if ((map != Map.Internal) || (map == Map.Internal && includeInternal))
                    {
                        return map;
                    }
                }
            }

            return null;
        }

        public static string[] GetMapList(bool includeInternal)
        {
            ArrayList list = new ArrayList();

            for (int i = 0; i < Map.Maps.Length; i++)
            {
                Map map = Map.Maps[i];

                string name = map.Name.ToLower();

                if ((map != Map.Internal) || (map == Map.Internal && includeInternal))
                {
                    list.Add(map.Name);
                }
            }

            return (string[])list.ToArray(typeof(string));
        }

        public static Season GetSeasonFromName(string seasonName)
        {
            for (int i = 0; i < Enum.GetNames(typeof(Season)).Length; i++)
            {
                string name = ((Season)i).ToString().ToLower();

                if (name == seasonName.ToLower())
                {
                    return (Season)i;
                }
            }

            return Season.None;
        }

        public static string[] GetSeasonList()
        {
            return Enum.GetNames(typeof(Season));
        }

        public static EffectsMapType GetEmoTypeFromName(string type)
        {
            for (int i = 0; i < Enum.GetNames(typeof(EffectsMapType)).Length; i++)
            {
                string name = ((EffectsMapType)i).ToString().ToLower();

                if (name == type.ToLower())
                {
                    return (EffectsMapType)i;
                }
            }

            return EffectsMapType.None;
        }

        public static EffectsExclusionMapType GetEemoTypeFromName(string type)
        {
            for (int i = 0; i < Enum.GetNames(typeof(EffectsExclusionMapType)).Length; i++)
            {
                string name = ((EffectsExclusionMapType)i).ToString().ToLower();

                if (name == type.ToLower())
                {
                    return (EffectsExclusionMapType)i;
                }
            }

            return EffectsExclusionMapType.None;
        }

        public static MobileObject GetMobileObject(Mobile mobile)
        {
            object mo = Data.MobilesTable[mobile];

            if (mo != null)
            {
                return (MobileObject)mo;
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region Check Methods

        public static bool CheckDataPath()
        {
            if (!Directory.Exists(Data.DataDirectory))
            {
                Directory.CreateDirectory(Data.DataDirectory);
                File.Create(Data.DataFile).Close();

                return false;
            }
            else if (!File.Exists(Data.DataFile))
            {
                File.Create(Data.DataFile).Close();

                return false;
            }

            return true;
        }

        public static bool CheckLogPath()
        {
            if (!Directory.Exists(Data.LogDirectory))
            {
                Directory.CreateDirectory(Data.LogDirectory);
                File.Create(Data.LogFile).Close();

                return false;
            }
            else if (!File.Exists(Data.LogFile))
            {
                File.Create(Data.LogFile).Close();

                return false;
            }

            return true;
        }

        public static void CheckForceScriptSettings()
        {
            bool forceScriptSettings = Config.ForceScriptSettings;

            if (forceScriptSettings)
            {
                Config.SetDefaults(false);

                ConsoleWriteLine("Time System: Force script settings is enabled.");
            }
        }

        public static bool CheckForceScriptSettings(ref VariableObject vo, string reference)
        {
            if (Config.ForceScriptSettings)
            {
                vo.Success = false;
                vo.Message = String.Format("ForceScriptSettings is set to true.  You are unable to make changes to [{0}] in-game.", reference);
            }

            return Config.ForceScriptSettings;
        }

        public static bool CheckAlreadyExistsInArray(ref VariableObject vo, object array, object o)
        {
            ArrayList objectList = new ArrayList();
            
            if (array is List<MonthPropsObject>)
            {
                List<MonthPropsObject> mpos = (List<MonthPropsObject>)array;

                for (int i = 0; i < mpos.Count; i++)
                {
                    objectList.Add(mpos[i]);
                }
            }

            if (array is List<MoonPropsObject>)
            {
                List<MoonPropsObject> mpos = (List<MoonPropsObject>)array;

                for (int i = 0; i < mpos.Count; i++)
                {
                    objectList.Add(mpos[i]);
                }
            }

            return CheckAlreadyExistsInList(ref vo, objectList, o, false);
        }

        public static bool CheckAlreadyExistsInList(ref VariableObject vo, ArrayList list, object o)
        {
            return CheckAlreadyExistsInList(ref vo, list, o, false);
        }

        public static bool CheckAlreadyExistsInList(ref VariableObject vo, ArrayList list, object o, bool caseSensitive)
        {
            string value = null;

            CheckAlreadyExistsInList(ref vo, list, o, caseSensitive, ref value);

            if (!vo.Success)
            {
                vo.Message = String.Format("[{0}] already exists!", value);
            }

            return !vo.Success;
        }

        private static bool CheckAlreadyExistsInList(ref VariableObject vo, ArrayList list, object o, bool caseSensitive, ref string value)
        {
            vo.Success = true;

            if (list == null)
            {
                return false;
            }

            for (int i = 0; i < list.Count; i++)
            {
                object lo = list[i];

                if (o == lo)
                {
                    vo.Success = false;

                    value = lo.ToString();

                    break;
                }

                if (lo is ArrayList)
                {
                    if (CheckAlreadyExistsInList(ref vo, (ArrayList)lo, o, caseSensitive, ref value))
                    {
                        vo.Success = false;
                    }
                }

                if (o is MonthPropsObject && lo is MonthPropsObject)
                {
                    MonthPropsObject oValue = (MonthPropsObject)o;
                    MonthPropsObject loValue = (MonthPropsObject)lo;

                    if (oValue.Name.ToLower() == loValue.Name.ToLower())
                    {
                        vo.Success = false;

                        value = loValue.Name;

                        break;
                    }
                }

                if (o is MoonPropsObject && lo is MoonPropsObject)
                {
                    MoonPropsObject oValue = (MoonPropsObject)o;
                    MoonPropsObject loValue = (MoonPropsObject)lo;

                    if (oValue.Name.ToLower() == loValue.Name.ToLower())
                    {
                        vo.Success = false;

                        value = loValue.Name;

                        break;
                    }
                }
            }

            return !vo.Success;
        }

        public static bool CheckEmoType(string type)
        {
            string[] emoTypeNames = Enum.GetNames(typeof(EffectsMapType));

            for (int i = 0; i < emoTypeNames.Length; i++)
            {
                string emoTypeName = emoTypeNames[i].ToUpper();

                if (type.ToUpper() == emoTypeName)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool CheckEemoType(string type)
        {
            string[] eemoTypeNames = Enum.GetNames(typeof(EffectsExclusionMapType));

            for (int i = 0; i < eemoTypeNames.Length; i++)
            {
                string eemoTypeName = eemoTypeNames[i].ToUpper();

                if (type.ToUpper() == eemoTypeName)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region Update Methods

        public static void UpdateCodeChars() // Incase CodeChar was changed from default '$' to something else.
        {
            Data.TimeFormat = Data.TimeFormat.Replace('$', Formatting.CodeChar);
            Data.ClockTimeFormat = Data.ClockTimeFormat.Replace('$', Formatting.CodeChar);
            Data.SpyglassFormat = Data.SpyglassFormat.Replace('$', Formatting.CodeChar);
        }

        #endregion

        #region Mathematic Methods

        public static int GetRandom(int lowNumber, int highNumber)
        {
            if (lowNumber > highNumber)
            {
                int swap = highNumber;
                highNumber = lowNumber;
                lowNumber = swap;
            }

            return new Random(RandomSeed).Next(lowNumber, highNumber + 1);
        }

        public static int GetRandom(int highNumber)
        {
            return new Random(RandomSeed).Next(highNumber + 1);
        }

        public static int GetRandom()
        {
            return new Random(RandomSeed).Next();
        }

        public static int GetWholeNumber(int number, int divisor, bool getFloor)
        {
            if (getFloor)
            {
                return (int)Math.Floor((double)number / (double)divisor);
            }
            else
            {
                return (int)Math.Ceiling((double)number / (double)divisor);
            }
        }

        #endregion

        #region LampPost Conversion

        public static void ConvertLampPosts(Mobile mobile)
        {
            if (Data.LightsList == null)
            {
                mobile.SendMessage("The LightsList is null!  Conversion process halted!");

                return;
            }

            mobile.SendMessage("Converting all LampPosts in the LightsList to TSBaseLights...");

            DateTime startTime = DateTime.Now;

            int count = 0;

            for (int i = 0; i < Data.LightsList.Count; i++)
            {
                BaseLight baseLight = (BaseLight)Data.LightsList[i];

                if (LightsEngine.DefragLightsList(baseLight))
                {
                    i--;
                }
                else
                {
                    ConvertLampPost(baseLight, ref count);
                }
            }

            TimeSpan deltaTime = DateTime.Now - startTime;

            double seconds = deltaTime.Milliseconds / 100.0;

            mobile.SendMessage(String.Format("{0} LampPost{1} ha{2} been converted to TSBaseLight.  The process took {3} seconds.", count, count == 1 ? "" : "s", count == 1 ? "s" : "ve", seconds));
        }

        public static void ConvertLampPosts_Callback(Mobile mobile, Map map, Point3D start, Point3D end, object state)
        {
            IPooledEnumerable eable = map.GetItemsInBounds(new Rectangle2D(start.X, start.Y, end.X - start.X + 1, end.Y - start.Y + 1));

            mobile.SendMessage("Converting all LampPosts in the bounding box to TSBaseLights...");

            DateTime startTime = DateTime.Now;

            int count = 0;

            foreach (Item item in eable)
            {
                if (item is BaseLight)
                {
                    BaseLight baseLight = (BaseLight)item;

                    ConvertLampPost(baseLight, ref count);
                }
            }

            eable.Free();

            TimeSpan deltaTime = DateTime.Now - startTime;

            double seconds = deltaTime.Milliseconds / 100.0;

            if (count > 0)
            {
                mobile.SendMessage(String.Format("{0} LampPost{1} ha{2} been converted to TSBaseLight.  The process took {3} seconds.", count, count == 1 ? "" : "s", count == 1 ? "s" : "ve", seconds));
            }
            else
            {
                mobile.SendMessage("No LampPosts found in the bounding box to convert!");
            }
        }

        public static void ConvertLampPost(Mobile mobile)
        {
            mobile.SendMessage("Target a lamp post to convert.");

            mobile.Target = new ConvertLampPostTarget();
        }

        private class ConvertLampPostTarget : Target
        {
            public ConvertLampPostTarget()
                : base(15, false, TargetFlags.None)
            {
            }

            protected override void OnTarget(Mobile mobile, object o)
            {
                if (o is BaseLight)
                {
                    if (ConvertLampPost((BaseLight)o))
                    {
                        mobile.SendMessage("The targetted lamp post has been converted to a TSBaseLight.");
                    }
                    else
                    {
                        mobile.SendMessage("The targetted lamp post is already a TSBaseLight!");
                    }
                }
                else
                {
                    mobile.SendMessage("The targetted object cannot be converted!");
                }
            }
        }

        private static void ConvertLampPost(BaseLight baseLight, ref int count)
        {
            if (ConvertLampPost(baseLight))
            {
                count++;
            }
        }

        private static bool ConvertLampPost(BaseLight baseLight)
        {
            if (!(baseLight is TSBaseLight))
            {
                TSBaseLight tsBaseLight = null;

                if (baseLight is LampPost1)
                {
                    tsBaseLight = new TSLampPost1();
                }
                else if (baseLight is LampPost2)
                {
                    tsBaseLight = new TSLampPost2();
                }
                else if (baseLight is LampPost3)
                {
                    tsBaseLight = new TSLampPost3();
                }

                if (tsBaseLight != null)
                {
                    CopyProperties(baseLight, tsBaseLight);

                    tsBaseLight.MoveToWorld(baseLight.Location);

                    baseLight.Delete();

                    return true;
                }
            }

            return false;
        }

        private static void CopyProperties(BaseLight baseLight, TSBaseLight tsBaseLight)
        {
            Type type = typeof(BaseLight);

            PropertyInfo[] props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            for (int i = 0; i < props.Length; i++)
            {
                PropertyInfo prop = props[i];

                if (prop.CanRead && prop.CanWrite)
                {
                    try
                    {
                        prop.SetValue(tsBaseLight, prop.GetValue(baseLight, null), null);
                    }
                    catch
                    {
                    }
                }
            }
        }

        #endregion
    }
}