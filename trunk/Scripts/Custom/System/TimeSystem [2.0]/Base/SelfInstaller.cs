using System;
using System.Text;
using System.IO;
using Server;

namespace Server.TimeSystem
{
    public class SelfInstaller
    {
        #region Constant Variables

        private const bool m_Enabled = false;
        private const bool m_UpdateLightCycle = true;
        private const bool m_UpdateClocks = true;
        private const bool m_UpdateSpyglass = true;
        private const bool m_UpdateNightSightSpell = true;
        private const bool m_UpdateNightSightPotion = true;
        private const bool m_UpdatePlayerMobile = true;

        private const string m_ExpectedVersion = "2.0 2357.32527"; // Major.Minor Build.Revision

        private static readonly string m_ModdedPath = Path.Combine(Core.BaseDirectory, @"Scripts\~Modded");

        private static readonly string m_LightCycleFile = Path.Combine(Core.BaseDirectory, @"Scripts\Misc\LightCycle.cs");
        private static readonly string m_ClocksFile = Path.Combine(Core.BaseDirectory, @"Scripts\Items\Skill Items\Tinkering\Clocks.cs");
        private static readonly string m_SpyglassFile = Path.Combine(Core.BaseDirectory, @"Scripts\Items\Skill Items\Tinkering\Spyglass.cs");
        private static readonly string m_NightSightSpellFile = Path.Combine(Core.BaseDirectory, @"Scripts\Spells\First\NightSight.cs");
        private static readonly string m_NightSightPotionFile = Path.Combine(Core.BaseDirectory, @"Scripts\Items\Skill Items\Magical\Potions\NightSight.cs");
        private static readonly string m_PlayerMobileFile = Path.Combine(Core.BaseDirectory, @"Scripts\Mobiles\PlayerMobile.cs");

        private const string m_BeginEdit = "// ** EDIT ** Time System";
        private const string m_EndEdit = "// ** END *** Time System";

        #endregion

        #region Get Methods

        private static string GetAssemblyVersion()
        {
            Version version = Core.Assembly.GetName().Version;

            return String.Format("{0}.{1} {2}.{3}", version.Major, version.Minor, version.Build, version.Revision);
        }

        private static string GetDirectoryMinusBase(string directory)
        {
            return directory.Replace(String.Format("{0}\\Scripts\\", Core.BaseDirectory), String.Empty);
        }

        private static string GetDirectoryMinusBaseAndFileName(string fileName)
        {
            return fileName.Replace(String.Format("\\{0}",GetFileName(fileName)),String.Empty).Replace(String.Format("{0}\\Scripts\\", Core.BaseDirectory), String.Empty);
        }

        private static string GetFileName(string fileName)
        {
            string[] fileNameSplit = fileName.Split('\\');

            return fileNameSplit[fileNameSplit.Length - 1];
        }

        #endregion

        #region Check Methods

        private static bool IsExpectedVersion()
        {
            string version = GetAssemblyVersion();

            if (m_ExpectedVersion != version)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private static void CheckPaths()
        {
            string newLightCyclePath = Path.Combine(m_ModdedPath, GetDirectoryMinusBaseAndFileName(m_LightCycleFile));
            string newClocksPath = Path.Combine(m_ModdedPath, GetDirectoryMinusBaseAndFileName(m_ClocksFile));
            string newSpyglassPath = Path.Combine(m_ModdedPath, GetDirectoryMinusBaseAndFileName(m_SpyglassFile));
            string newNightSightSpellPath = Path.Combine(m_ModdedPath, GetDirectoryMinusBaseAndFileName(m_NightSightSpellFile));
            string newNightSightPotionPath = Path.Combine(m_ModdedPath, GetDirectoryMinusBaseAndFileName(m_NightSightPotionFile));
            string newPlayerMobilePath = Path.Combine(m_ModdedPath, GetDirectoryMinusBaseAndFileName(m_PlayerMobileFile));

            CreateDirectory(m_ModdedPath);
            CreateDirectory(newLightCyclePath);
            CreateDirectory(newClocksPath);
            CreateDirectory(newSpyglassPath);
            CreateDirectory(newNightSightSpellPath);
            CreateDirectory(newNightSightPotionPath);
            CreateDirectory(newPlayerMobilePath);
        }

        private static void CreateDirectory(string directory)
        {
            string directoryMinusBase = GetDirectoryMinusBase(directory);
            string baseDirectory = directory.Replace(directoryMinusBase, String.Empty);

            string[] directorySplit = directoryMinusBase.Split('\\');

            string combineDirectory = String.Empty;

            for (int i = 0; i < directorySplit.Length; i++)
            {
                if (i > 0)
                {
                    combineDirectory = String.Format("{0}\\{1}", combineDirectory, directorySplit[i]);
                }
                else
                {
                    combineDirectory = directorySplit[i];
                }

                string checkDirectory = Path.Combine(baseDirectory, combineDirectory);

                if (i > 0)
                {
                    if (!Directory.Exists(checkDirectory))
                    {
                        Directory.CreateDirectory(checkDirectory);
                    }
                }
            }
        }

        #endregion

        #region Installer

        public static void Install()
        {
            bool enabled = m_Enabled;

            if (enabled)
            {
                Support.ConsoleWriteLine("Time System: SelfInstaller: Executing...");

                if (!IsExpectedVersion())
                {
                    Support.ConsoleWriteLine("Time System: SelfInstaller: RunUO version is different than expected!  SelfInstaller cancelled!");
                }
                else
                {
                    CheckPaths();

                    bool lightCycleSuccess = false, clocksSuccess = false, spyglassSuccess = false, nightSightSpellSuccess = false, nightSightPotionSuccess = false, playerMobileSuccess = false;

                    Support.ConsoleWriteLine(String.Format("Time System: SelfInstaller: {0}", UpdateLightCycleFile(ref lightCycleSuccess)));
                    Support.ConsoleWriteLine(String.Format("Time System: SelfInstaller: {0}", UpdateClocksFile(ref clocksSuccess)));
                    Support.ConsoleWriteLine(String.Format("Time System: SelfInstaller: {0}", UpdateSpyglassFile(ref spyglassSuccess)));
                    Support.ConsoleWriteLine(String.Format("Time System: SelfInstaller: {0}", UpdateNightSightSpellFile(ref nightSightSpellSuccess)));
                    Support.ConsoleWriteLine(String.Format("Time System: SelfInstaller: {0}", UpdateNightSightPotionFile(ref nightSightPotionSuccess)));
                    Support.ConsoleWriteLine(String.Format("Time System: SelfInstaller: {0}", UpdatePlayerMobileFile(ref playerMobileSuccess)));

                    Support.ConsoleWriteLine("Time System: SelfInstaller: Execution has completed.");

                    if (lightCycleSuccess || clocksSuccess || spyglassSuccess || nightSightSpellSuccess || nightSightPotionSuccess || playerMobileSuccess)
                    {
                        Support.ConsoleWriteLine("Time System: SelfInstaller: Please restart your server for changes to take effect!");
                    }
                }
            }
            else
            {
                Support.ConsoleWriteLine("Time System: SelfInstaller is disabled.");
            }
        }

        private static void ReadWrite(StreamReader r, StreamWriter w)
        {
            if (r.Peek() > 0)
            {
                w.WriteLine(r.ReadLine());
            }
        }

        #endregion

        #region Update LightCycle.cs

        private static string UpdateLightCycleFile(ref bool success)
        {
            bool enabled = m_UpdateLightCycle;

            if (!enabled)
            {
                success = false;

                return "LightCycle.cs has been set to skip the update!";
            }

            string newLightCyclePath = Path.Combine(m_ModdedPath, GetDirectoryMinusBaseAndFileName(m_LightCycleFile));
            string newLightCycleFile = Path.Combine(newLightCyclePath, GetFileName(m_LightCycleFile));

            if (File.Exists(newLightCycleFile))
            {
                success = false;

                return "LightCycle.cs already updated!";
            }

            if (!File.Exists(m_LightCycleFile))
            {
                success = false;

                return "Original LightCycle.cs not found!";
            }

            StreamReader r = null;
            StreamWriter w = null;

            try
            {
                bool ignoreNextWrite = false;

                r = new StreamReader(m_LightCycleFile);
                w = new StreamWriter(newLightCycleFile);

                while (r.Peek() > 0)
                {
                    string read = r.ReadLine();

                    if (read.IndexOf("public static void Initialize()") > -1)
                    {
                        ignoreNextWrite = true;

                        w.WriteLine(read);

                        ReadWrite(r, w);

                        w.WriteLine();
                        w.WriteLine(m_BeginEdit);
                        w.WriteLine();

                        string line = r.ReadLine();

                        if (line.IndexOf("new LightCycleTimer().Start();") > -1)
                        {
                            w.WriteLine(line.Replace("new LightCycleTimer().Start();", "//new LightCycleTimer().Start();"));
                        }

                        line = r.ReadLine();

                        if (line.IndexOf("EventSink.Login += new LoginEventHandler( OnLogin );") > -1)
                        {
                            w.WriteLine(line.Replace("EventSink.Login += new LoginEventHandler( OnLogin );", "//EventSink.Login += new LoginEventHandler( OnLogin );"));
                        }

                        w.WriteLine();
                        w.WriteLine(m_EndEdit);
                    }

                    if (read.IndexOf("public static int ComputeLevelFor( Mobile from )") > -1)
                    {
                        ignoreNextWrite = true;

                        w.WriteLine(read);

                        ReadWrite(r, w);

                        string line = r.ReadLine();

                        while (line.IndexOf("return m_LevelOverride;") == -1)
                        {
                            w.WriteLine(line);

                            line = r.ReadLine();
                        }

                        w.WriteLine(line);

                        ReadWrite(r, w);

                        w.WriteLine(m_BeginEdit);
                        w.WriteLine();
                        w.WriteLine("\t\t\treturn TimeSystem.TimeEngine.CalculateLightLevel(from);");
                        w.WriteLine();
                        w.WriteLine("\t\t\t/*");

                        line = r.ReadLine();

                        while (line.IndexOf("return NightLevel; // should never be") == -1)
                        {
                            if (line.IndexOf("/*") > -1)
                            {
                                w.WriteLine(line.Replace("/*", "//"));
                            }
                            else if (line.IndexOf("*/") > -1)
                            {
                                w.WriteLine(line.Replace("*/", "//"));
                            }
                            else
                            {
                                w.WriteLine(line);
                            }

                            line = r.ReadLine();
                        }

                        w.WriteLine(line);
                        w.WriteLine("\t\t\t*/");
                        w.WriteLine();
                        w.WriteLine(m_EndEdit);
                        w.WriteLine();
                    }

                    if (read.IndexOf("public class NightSightTimer : Timer") > -1)
                    {
                        ignoreNextWrite = true;

                        w.WriteLine(read);

                        ReadWrite(r, w);

                        string line = r.ReadLine();

                        while (line.IndexOf("BuffInfo.RemoveBuff( m_Owner, BuffIcon.NightSight );") == -1)
                        {
                            w.WriteLine(line);

                            line = r.ReadLine();
                        }

                        w.WriteLine(line);

                        w.WriteLine();
                        w.WriteLine(m_BeginEdit);
                        w.WriteLine();
                        w.WriteLine("\t\t\t\tTimeSystem.EffectsEngine.SetNightSightOff(m_Owner);");
                        w.WriteLine();
                        w.WriteLine(m_EndEdit);
                        w.WriteLine();
                    }

                    if (!ignoreNextWrite)
                    {
                        w.WriteLine(read);
                    }
                    else
                    {
                        ignoreNextWrite = false;
                    }
                }

                r.Close();
                w.Close();

                File.Move(m_LightCycleFile, String.Format("{0}_old",m_LightCycleFile));
            }
            catch(Exception e)
            {
                if (r != null)
                {
                    r.Close();
                }

                if (w != null)
                {
                    w.Close();
                }

                success = false;

                return String.Format("LightCycle.cs update failed!\r\n{0}",e.ToString());
            }

            success = true;

            return "LightCycle.cs updated successfully.";
        }

        #endregion

        #region Update Clocks.cs

        private static string UpdateClocksFile(ref bool success)
        {
            bool enabled = m_UpdateClocks;

            if (!enabled)
            {
                success = false;

                return "Clocks.cs has been set to skip the update!";
            }

            string newClocksPath = Path.Combine(m_ModdedPath, GetDirectoryMinusBaseAndFileName(m_ClocksFile));
            string newClocksFile = Path.Combine(newClocksPath, GetFileName(m_ClocksFile));

            if (File.Exists(newClocksFile))
            {
                success = false;

                return "Clocks.cs already updated!";
            }

            if (!File.Exists(m_ClocksFile))
            {
                success = false;

                return "Original Clocks.cs not found!";
            }

            StreamReader r = null;
            StreamWriter w = null;

            try
            {
                bool ignoreNextWrite = false;

                r = new StreamReader(m_ClocksFile);
                w = new StreamWriter(newClocksFile);

                while (r.Peek() > 0)
                {
                    string read = r.ReadLine();

                    if (read.IndexOf("using Server;") > -1)
                    {
                        ignoreNextWrite = true;

                        w.WriteLine(read);
                        w.WriteLine(m_BeginEdit);
                        w.WriteLine("using Server.Mobiles;");
                        w.WriteLine("using Server.Network;");
                        w.WriteLine(m_EndEdit);
                    }

                    if (read.IndexOf("public static void GetTime( Map map, int x, int y, out int hours, out int minutes, out int totalMinutes )") > -1)
                    {
                        ignoreNextWrite = true;

                        w.WriteLine(read);

                        ReadWrite(r, w);

                        w.WriteLine();
                        w.WriteLine(m_BeginEdit);
                        w.WriteLine();
                        w.WriteLine("\t\t\ttotalMinutes = 0;");
                        w.WriteLine();
                        w.WriteLine("\t\t\tTimeSystem.TimeEngine.GetTimeMinHour(map, x, out minutes, out hours);");
                        w.WriteLine();
                        w.WriteLine("\t\t\t/*");

                        string line = r.ReadLine();

                        while (line.IndexOf("minutes = totalMinutes % 60;") == -1)
                        {
                            w.WriteLine(line);

                            line = r.ReadLine();
                        }

                        w.WriteLine(line);
                        w.WriteLine("\t\t\t*/");
                        w.WriteLine();
                        w.WriteLine(m_EndEdit);
                        w.WriteLine();
                    }

                    if (read.IndexOf("public override void OnDoubleClick( Mobile from )") > -1)
                    {
                        ignoreNextWrite = true;

                        w.WriteLine(read);

                        ReadWrite(r, w);

                        w.WriteLine();
                        w.WriteLine(m_BeginEdit);
                        w.WriteLine();
                        w.WriteLine("\t\t\tTimeSystem.Support.SendClockData(this, from);");
                        w.WriteLine();
                        w.WriteLine("\t\t\t/*");

                        string line = r.ReadLine();

                        while (line.IndexOf("SendLocalizedMessageTo( from, 1042958, exactTime ); // ~1_TIME~ to be exact") == -1)
                        {
                            w.WriteLine(line);

                            line = r.ReadLine();
                        }

                        w.WriteLine(line);
                        w.WriteLine("\t\t\t*/");
                        w.WriteLine();
                        w.WriteLine(m_EndEdit);
                        w.WriteLine();
                    }

                    if (!ignoreNextWrite)
                    {
                        w.WriteLine(read);
                    }
                    else
                    {
                        ignoreNextWrite = false;
                    }
                }

                r.Close();
                w.Close();

                File.Move(m_ClocksFile, String.Format("{0}_old", m_ClocksFile));
            }
            catch (Exception e)
            {
                if (r != null)
                {
                    r.Close();
                }

                if (w != null)
                {
                    w.Close();
                }

                success = false;

                return String.Format("Clocks.cs update failed!\r\n{0}", e.ToString());
            }

            success = true;

            return "Clocks.cs updated successfully.";
        }

        #endregion

        #region Update Spyglass.cs

        private static string UpdateSpyglassFile(ref bool success)
        {
            bool enabled = m_UpdateSpyglass;

            if (!enabled)
            {
                success = false;

                return "Spyglass.cs has been set to skip the update!";
            }

            string newSpyglassPath = Path.Combine(m_ModdedPath, GetDirectoryMinusBaseAndFileName(m_SpyglassFile));
            string newSpyglassFile = Path.Combine(newSpyglassPath, GetFileName(m_SpyglassFile));

            if (File.Exists(newSpyglassFile))
            {
                success = false;

                return "Spyglass.cs already updated!";
            }

            if (!File.Exists(m_SpyglassFile))
            {
                success = false;

                return "Original Spyglass.cs not found!";
            }

            StreamReader r = null;
            StreamWriter w = null;

            try
            {
                bool ignoreNextWrite = false;

                r = new StreamReader(m_SpyglassFile);
                w = new StreamWriter(newSpyglassFile);

                while (r.Peek() > 0)
                {
                    string read = r.ReadLine();

                    if (read.IndexOf("public override void OnDoubleClick( Mobile from )") > -1)
                    {
                        ignoreNextWrite = true;

                        w.WriteLine(read);

                        ReadWrite(r, w);

                        w.WriteLine();
                        w.WriteLine(m_BeginEdit);
                        w.WriteLine();
                        w.WriteLine("\t\t\tTimeSystem.Support.SendSpyglassData(from);");
                        w.WriteLine();
                        w.WriteLine("\t\t\t/*");

                        string line = r.ReadLine();

                        while (line.IndexOf("from.Send( new MessageLocalizedAffix( from.Serial, from.Body, MessageType.Regular, 0x3B2, 3, 1008146 + (int)Clock.GetMoonPhase( Map.Felucca, from.X, from.Y ), \"\", AffixType.Prepend, \"Felucca : \", \"\" ) );") == -1)
                        {
                            w.WriteLine(line);

                            line = r.ReadLine();
                        }

                        w.WriteLine(line);
                        w.WriteLine("\t\t\t*/");
                        w.WriteLine();
                        w.WriteLine(m_EndEdit);
                    }

                    if (!ignoreNextWrite)
                    {
                        w.WriteLine(read);
                    }
                    else
                    {
                        ignoreNextWrite = false;
                    }
                }

                r.Close();
                w.Close();

                File.Move(m_SpyglassFile, String.Format("{0}_old", m_SpyglassFile));
            }
            catch (Exception e)
            {
                if (r != null)
                {
                    r.Close();
                }

                if (w != null)
                {
                    w.Close();
                }

                success = false;

                return String.Format("Spyglass.cs update failed!\r\n{0}", e.ToString());
            }

            success = true;

            return "Spyglass.cs updated successfully.";
        }

        #endregion

        #region Update NightSight.cs (spell)

        private static string UpdateNightSightSpellFile(ref bool success)
        {
            bool enabled = m_UpdateNightSightSpell;

            if (!enabled)
            {
                success = false;

                return "NightSight.cs (spell) has been set to skip the update!";
            }

            string newNightSightSpellPath = Path.Combine(m_ModdedPath, GetDirectoryMinusBaseAndFileName(m_NightSightSpellFile));
            string newNightSightSpellFile = Path.Combine(newNightSightSpellPath, GetFileName(m_NightSightSpellFile));

            if (File.Exists(newNightSightSpellFile))
            {
                success = false;

                return "NightSight.cs (spell) already updated!";
            }

            if (!File.Exists(m_NightSightSpellFile))
            {
                success = false;

                return "Original NightSight.cs (spell) not found!";
            }

            StreamReader r = null;
            StreamWriter w = null;

            try
            {
                bool ignoreNextWrite = false;

                r = new StreamReader(m_NightSightSpellFile);
                w = new StreamWriter(newNightSightSpellFile);

                while (r.Peek() > 0)
                {
                    string read = r.ReadLine();

                    if (read.IndexOf("private class NightSightTarget : Target") > -1)
                    {
                        ignoreNextWrite = true;

                        w.WriteLine(read);

                        string line = r.ReadLine();

                        while (line.IndexOf("if ( targ.BeginAction( typeof( LightCycle ) ) )") == -1)
                        {
                            w.WriteLine(line);

                            line = r.ReadLine();
                        }

                        w.WriteLine(line);

                        ReadWrite(r, w);

                        w.WriteLine();
                        w.WriteLine(m_BeginEdit);
                        w.WriteLine();

                        line = r.ReadLine();

                        if (line.IndexOf("new LightCycle.NightSightTimer( targ ).Start();") > -1)
                        {
                            w.WriteLine(line.Replace("new LightCycle.NightSightTimer( targ ).Start();", "//new LightCycle.NightSightTimer( targ ).Start();"));
                        }

                        line = r.ReadLine();

                        while (line.IndexOf("targ.LightLevel = level;") == -1)
                        {
                            w.WriteLine(line);

                            line = r.ReadLine();
                        }

                        w.WriteLine("\t\t\t\t\t\tint oldLevel = level;");
                        w.WriteLine();
                        w.WriteLine("\t\t\t\t\t\tlevel = TimeSystem.EffectsEngine.GetNightSightLevel(targ, level);");
                        w.WriteLine();
                        w.WriteLine("\t\t\t\t\t\tif (level > -1)");
                        w.WriteLine("\t\t\t\t\t\t{");
                        w.WriteLine(String.Format("\t{0}", line));

                        line = r.ReadLine();

                        while (line.IndexOf("BuffInfo.AddBuff( targ, new BuffInfo( BuffIcon.NightSight, 1075643 ) );	//Night Sight/You ignore lighting effects") == -1)
                        {
                            w.WriteLine(String.Format("\t{0}", line));

                            line = r.ReadLine();
                        }

                        w.WriteLine(String.Format("\t{0}", line));
                        w.WriteLine();
                        w.WriteLine("\t\t\t\t\t\t\tTimeSystem.EffectsEngine.SetNightSightOn(targ, oldLevel);");
                        w.WriteLine("\t\t\t\t\t\t}");
                        w.WriteLine("\t\t\t\t\t\telse");
                        w.WriteLine("\t\t\t\t\t\t{");
                        w.WriteLine("\t\t\t\t\t\t\ttarg.EndAction(typeof(LightCycle));");
                        w.WriteLine();
                        w.WriteLine("\t\t\t\t\t\t\tfrom.SendMessage(\"Your spell seems to have no effect.\");");
                        w.WriteLine("\t\t\t\t\t\t}");
                        w.WriteLine();
                        w.WriteLine(m_EndEdit);
                        w.WriteLine();
                    }

                    if (!ignoreNextWrite)
                    {
                        w.WriteLine(read);
                    }
                    else
                    {
                        ignoreNextWrite = false;
                    }
                }

                r.Close();
                w.Close();

                File.Move(m_NightSightSpellFile, String.Format("{0}_old", m_NightSightSpellFile));
            }
            catch (Exception e)
            {
                if (r != null)
                {
                    r.Close();
                }

                if (w != null)
                {
                    w.Close();
                }

                success = false;

                return String.Format("NightSight.cs (spell) update failed!\r\n{0}", e.ToString());
            }

            success = true;

            return "NightSight.cs (spell) updated successfully.";
        }

        #endregion

        #region Update NightSight.cs (potion)

        private static string UpdateNightSightPotionFile(ref bool success)
        {
            bool enabled = m_UpdateNightSightPotion;

            if (!enabled)
            {
                success = false;

                return "NightSight.cs (potion) has been set to skip the update!";
            }

            string newNightSightPotionPath = Path.Combine(m_ModdedPath, GetDirectoryMinusBaseAndFileName(m_NightSightPotionFile));
            string newNightSightPotionFile = Path.Combine(newNightSightPotionPath, GetFileName(m_NightSightPotionFile));

            if (File.Exists(newNightSightPotionFile))
            {
                success = false;

                return "NightSight.cs (potion) already updated!";
            }

            if (!File.Exists(m_NightSightPotionFile))
            {
                success = false;

                return "Original NightSight.cs (potion) not found!";
            }

            StreamReader r = null;
            StreamWriter w = null;

            try
            {
                bool ignoreNextWrite = false;

                r = new StreamReader(m_NightSightPotionFile);
                w = new StreamWriter(newNightSightPotionFile);

                while (r.Peek() > 0)
                {
                    string read = r.ReadLine();

                    if (read.IndexOf("public override void Drink( Mobile from )") > -1)
                    {
                        ignoreNextWrite = true;

                        w.WriteLine(read);

                        ReadWrite(r, w);
                        ReadWrite(r, w);
                        ReadWrite(r, w);

                        w.WriteLine();
                        w.WriteLine(m_BeginEdit);
                        w.WriteLine();

                        string line = r.ReadLine();

                        if(line.IndexOf("new LightCycle.NightSightTimer( from ).Start();") > -1)
                        {
                            w.WriteLine(line.Replace("new LightCycle.NightSightTimer( from ).Start();", "//new LightCycle.NightSightTimer( from ).Start();"));
                        }

                        line = r.ReadLine();

                        if (line.IndexOf("from.LightLevel = LightCycle.DungeonLevel / 2;") > -1)
                        {
                            w.WriteLine(line.Replace("from.LightLevel = LightCycle.DungeonLevel / 2;", "//from.LightLevel = LightCycle.DungeonLevel / 2;"));
                        }

                        line = r.ReadLine();

                        while (line.IndexOf("this.Consume();") == -1)
                        {
                            w.WriteLine(line);

                            line = r.ReadLine();
                        }

                        w.WriteLine(line);

                        w.WriteLine();
                        w.WriteLine("\t\t\t\tint oldLevel = LightCycle.DungeonLevel / 2;");
                        w.WriteLine();
                        w.WriteLine("\t\t\t\tint level = TimeSystem.EffectsEngine.GetNightSightLevel(from, oldLevel);");
                        w.WriteLine();
                        w.WriteLine("\t\t\t\tif (level > -1)");
                        w.WriteLine("\t\t\t\t{");
                        w.WriteLine("\t\t\t\t\tfrom.LightLevel = level;");
                        w.WriteLine();
                        w.WriteLine("\t\t\t\t\tTimeSystem.EffectsEngine.SetNightSightOn(from, oldLevel);");
                        w.WriteLine("\t\t\t\t}");
                        w.WriteLine("\t\t\t\telse");
                        w.WriteLine("\t\t\t\t{");
                        w.WriteLine("\t\t\t\t\tfrom.EndAction(typeof(LightCycle));");
                        w.WriteLine();
                        w.WriteLine("\t\t\t\t\tfrom.SendMessage(\"The potion seems to have no effect.\");");
                        w.WriteLine("\t\t\t\t}");
                        w.WriteLine();
                        w.WriteLine(m_EndEdit);
                        w.WriteLine();
                    }

                    if (!ignoreNextWrite)
                    {
                        w.WriteLine(read);
                    }
                    else
                    {
                        ignoreNextWrite = false;
                    }
                }

                r.Close();
                w.Close();

                File.Move(m_NightSightPotionFile, String.Format("{0}_old", m_NightSightPotionFile));
            }
            catch (Exception e)
            {
                if (r != null)
                {
                    r.Close();
                }

                if (w != null)
                {
                    w.Close();
                }

                success = false;

                return String.Format("NightSight.cs (potion) update failed!\r\n{0}", e.ToString());
            }

            success = true;

            return "NightSight.cs (potion) updated successfully.";
        }

        #endregion

        #region Update PlayerMobile.cs

        private static string UpdatePlayerMobileFile(ref bool success)
        {
            bool enabled = m_UpdatePlayerMobile;

            if (!enabled)
            {
                success = false;

                return "PlayerMobile.cs has been set to skip the update!";
            }

            string newPlayerMobilePath = Path.Combine(m_ModdedPath, GetDirectoryMinusBaseAndFileName(m_PlayerMobileFile));
            string newPlayerMobileFile = Path.Combine(newPlayerMobilePath, GetFileName(m_PlayerMobileFile));

            if (File.Exists(newPlayerMobileFile))
            {
                success = false;

                return "PlayerMobile.cs already updated!";
            }

            if (!File.Exists(m_PlayerMobileFile))
            {
                success = false;

                return "Original PlayerMobile.cs not found!";
            }

            StreamReader r = null;
            StreamWriter w = null;

            try
            {
                bool ignoreNextWrite = false;

                r = new StreamReader(m_PlayerMobileFile);
                w = new StreamWriter(newPlayerMobileFile);

                while (r.Peek() > 0)
                {
                    string read = r.ReadLine();

                    if (read.IndexOf("public override void ComputeBaseLightLevels( out int global, out int personal )") > -1)
                    {
                        ignoreNextWrite = true;

                        w.WriteLine(read);

                        ReadWrite(r, w);
                        ReadWrite(r, w);
                        ReadWrite(r, w);

                        w.WriteLine(m_BeginEdit);
                        w.WriteLine();

                        string line = r.ReadLine();

                        if (line.IndexOf("if ( this.LightLevel < 21 && AosAttributes.GetValue( this, AosAttribute.NightSight ) > 0 )") > -1)
                        {
                            w.WriteLine(line.Replace("if ( this.LightLevel < 21 && AosAttributes.GetValue( this, AosAttribute.NightSight ) > 0 )", "/*if ( this.LightLevel < 21 && AosAttributes.GetValue( this, AosAttribute.NightSight ) > 0 )"));
                        }

                        line = r.ReadLine();

                        while (line.IndexOf("personal = this.LightLevel;") == -1)
                        {
                            w.WriteLine(line);

                            line = r.ReadLine();
                        }

                        if (line.IndexOf("personal = this.LightLevel;") > -1)
                        {
                            w.WriteLine(line.Replace("personal = this.LightLevel;", "personal = this.LightLevel;*/"));
                        }

                        w.WriteLine();
                        w.WriteLine("\t\t\tif (this.LightLevel < 21 && AosAttributes.GetValue(this, AosAttribute.NightSight) > 0)");
                        w.WriteLine("\t\t\t{");
                        w.WriteLine("\t\t\t\tint level = TimeSystem.EffectsEngine.GetNightSightLevel(this, 21);");
                        w.WriteLine();
                        w.WriteLine("\t\t\t\tif (level > -1)");
                        w.WriteLine("\t\t\t\t{");
                        w.WriteLine("\t\t\t\t\tpersonal = level;");
                        w.WriteLine("\t\t\t\t}");
                        w.WriteLine("\t\t\t\telse");
                        w.WriteLine("\t\t\t\t{");
                        w.WriteLine("\t\t\t\t\tpersonal = 0;");
                        w.WriteLine("\t\t\t\t}");
                        w.WriteLine("\t\t\t}");
                        w.WriteLine("\t\t\telse");
                        w.WriteLine("\t\t\t{");
                        w.WriteLine("\t\t\t\tpersonal = this.LightLevel;");
                        w.WriteLine("\t\t\t}");
                        w.WriteLine();
                        w.WriteLine(m_EndEdit);
                        w.WriteLine();
                    }

                    if (!ignoreNextWrite)
                    {
                        w.WriteLine(read);
                    }
                    else
                    {
                        ignoreNextWrite = false;
                    }
                }

                r.Close();
                w.Close();

                File.Move(m_PlayerMobileFile, String.Format("{0}_old", m_PlayerMobileFile));
            }
            catch (Exception e)
            {
                if (r != null)
                {
                    r.Close();
                }

                if (w != null)
                {
                    w.Close();
                }

                success = false;

                return String.Format("PlayerMobile.cs update failed!\r\n{0}", e.ToString());
            }

            success = true;

            return "PlayerMobile.cs updated successfully.";
        }

        #endregion
    }
}