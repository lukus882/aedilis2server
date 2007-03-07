using System;
using System.IO;
using Server;

namespace Server.TimeSystem
{
    public class LegacySupport
    {
        #region Loading

        public static bool Load(BinaryReader reader, int version)
        {
            try
            {
                int useless = 0;

                Config.SetDefaults(true);

                if (version >= 1)
                {
                    Data.DayLevel = reader.ReadInt32();
                    Data.NightLevel = reader.ReadInt32();
                    Data.DarkestHourLevel = reader.ReadInt32();
                    Data.LightsOnLevel = reader.ReadInt32();

                    Data.UseRealTime = reader.ReadBoolean();

                    Data.UseAutoLighting = reader.ReadBoolean();
                    Data.UseRandomLightOutage = reader.ReadBoolean();
                    Data.LightOutageChancePerTick = reader.ReadInt32();
                    useless = reader.ReadInt32();
                    useless = reader.ReadInt32();

                    Data.TimerSpeed = reader.ReadDouble();
                    Data.MinutesPerTick = reader.ReadInt32();

                    Data.MinutesPerHour = reader.ReadInt32();
                    Data.HoursPerDay = reader.ReadInt32();
                    useless = reader.ReadInt32();
                    useless = reader.ReadInt32();

                    Data.NightStartHour = reader.ReadInt32();
                    Data.DayStartHour = reader.ReadInt32();
                    Data.ScaleTimeMinutes = reader.ReadInt32();

                    Data.UseDarkestHour = reader.ReadBoolean();
                    Data.DarkestHourMinutesAfterNight = reader.ReadInt32();
                    Data.DarkestHourScaleTimeMinutes = reader.ReadInt32();
                    Data.DarkestHourLength = reader.ReadInt32();

                    useless = reader.ReadInt32();
                    useless = reader.ReadInt32();
                    Data.MoonLevelAdjust = reader.ReadInt32();

                    Data.Year = reader.ReadInt32();
                    Data.Month = reader.ReadInt32();
                    Data.Day = reader.ReadInt32();
                    Data.Hour = reader.ReadInt32();
                    Data.Minute = reader.ReadInt32();
                }

                if (version >= 2)
                {
                    Data.Enabled = reader.ReadBoolean();

                    Data.UseTimeZones = reader.ReadBoolean();
                    Data.TimeZoneXDivisor = reader.ReadInt32();

                    Data.TimeFormat = reader.ReadString();
                    Data.ClockTimeFormat = reader.ReadString();
                }

                reader.Close();

                Data.DataFileInUse = false;

                Console.WriteLine("Time System: Loading complete.");

                return true;
            }
            catch (EndOfStreamException e)
            {
                reader.Close();

                Data.DataFileInUse = false;

                Support.ConsoleWriteLine(String.Format("Time System: \"{0}\" is corrupt.  Creating a new file using the current settings.", Data.DataFileName));

                Config.SetDefaults(true);

                Engine.Restart();

                Data.Save();

                string goAwayWarning = e.ToString(); // Stop compile warning.

                return false;
            }
            catch (Exception e)
            {
                reader.Close();

                Data.DataFileInUse = false;

                Support.ConsoleWriteLine(String.Format("Time System: Unable to load data from file \"{0}\"!  Creating a new file using the current settings.\r\n\r\nException:\r\n\r\n{1}", Data.DataFileName, e.ToString()));

                Config.SetDefaults(true);

                Engine.Restart();

                Data.Save();

                return false;
            }

        }

        #endregion
    }
}