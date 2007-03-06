using System;
using System.Text;
using Server;

namespace Server.TimeSystem
{
    public class Formatting
    {
        #region Constant Variables

        public const char CodeChar = '$';
        public const char ValueChar = '-';

        private const string m_MinuteCode = "mn";
        private const string m_HourCode = "hr";
        private const string m_DayCode = "da";
        private const string m_MonthCode = "mo";
        private const string m_YearCode = "yr";
        private const string m_SeasonCode = "sn";
        private const string m_MoonNameCode = "cmn";
        private const string m_MoonPhaseCode = "mp";
        private const string m_AmPmCode = "ap";
        private const string m_NthCode = "nth";
        private const string m_SpaceCode = "";

        #endregion

        #region Get Methods

        public static string GetTimeFormat(object o, Format format)
        {
            return GetTimeFormat(o, format, true);
        }

        public static string GetTimeFormat(object o, Format format, bool useTimeZoneScaling)
        {
            int minute = Data.Minute + TimeEngine.GetAdjustments(o, useTimeZoneScaling);
            int hour = Data.Hour;
            int day = Data.Day;
            int month = Data.Month;
            int year = Data.Year;

            TimeEngine.CheckTime(ref minute, ref hour, ref day, ref month, ref year, false);

            string timeFormat = null;

            switch (format)
            {
                case Format.Time: { timeFormat = Data.TimeFormat; break; }
                case Format.Clock: { timeFormat = Data.ClockTimeFormat; break; }
                case Format.Spyglass: { timeFormat = Data.SpyglassFormat; break; }
            }

            bool formatCode = false;

            StringBuilder sb = new StringBuilder();
            StringBuilder formatter = new StringBuilder();

            for (int i = 0; i < timeFormat.Length; i++)
            {
                char c = timeFormat[i];

                if (formatCode && c == CodeChar)
                {
                    formatCode = false;

                    string formattedValue = String.Format("{0}{1}{2}", CodeChar, formatter.ToString(), CodeChar);

                    string[] formatterSplit = formatter.ToString().Split(ValueChar);

                    string code = formatterSplit[0];
                    string codeValue = null;

                    if (formatterSplit.Length == 2)
                    {
                        codeValue = formatterSplit[1];
                    }

                    switch (code)
                    {
                        case m_MinuteCode:
                            {
                                string theMinute = minute.ToString();

                                object fo = codeValue;

                                fo = Support.GetValue(codeValue);

                                if (codeValue != null)
                                {
                                    if (fo is int)
                                    {
                                        int value = (int)fo;

                                        if (value >= 2)
                                        {
                                            StringBuilder minuteSB = new StringBuilder();

                                            for (int j = theMinute.Length; j < value; j++)
                                            {
                                                minuteSB.Append("0");
                                            }

                                            minuteSB.Append(theMinute);

                                            formattedValue = minuteSB.ToString();
                                        }
                                        else
                                        {
                                            formattedValue = theMinute;
                                        }
                                    }
                                }
                                else
                                {
                                    formattedValue = theMinute;
                                }

                                break;
                            }
                        case m_HourCode:
                            {
                                string theHour = hour.ToString();

                                object fo = codeValue;

                                fo = Support.GetValue(codeValue);

                                if (codeValue != null)
                                {
                                    if (fo is int)
                                    {
                                        int value = (int)fo;

                                        if (value >= 2)
                                        {
                                            StringBuilder hourSB = new StringBuilder();

                                            for (int j = theHour.Length; j < value; j++)
                                            {
                                                hourSB.Append("0");
                                            }

                                            hourSB.Append(theHour);

                                            formattedValue = hourSB.ToString();
                                        }
                                        else
                                        {
                                            formattedValue = theHour;
                                        }
                                    }
                                    else if (fo is string)
                                    {
                                        string value = (string)fo;

                                        if (value == m_AmPmCode)
                                        {
                                            int totalHours = Data.HoursPerDay;

                                            int changePoint = Convert.ToInt32((double)totalHours / 2.0);

                                            if (hour <= changePoint)
                                            {
                                                if (hour == 0)
                                                {
                                                    formattedValue = changePoint.ToString();
                                                }
                                                else
                                                {
                                                    formattedValue = hour.ToString();
                                                }
                                            }
                                            else
                                            {
                                                formattedValue = Convert.ToString(hour - changePoint);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    formattedValue = theHour;
                                }

                                break;
                            }
                        case m_DayCode:
                            {
                                string theDay = day.ToString();

                                object fo = codeValue;

                                fo = Support.GetValue(codeValue);

                                if (codeValue != null)
                                {
                                    if (fo is int)
                                    {
                                        int value = (int)fo;

                                        if (value >= 2)
                                        {
                                            StringBuilder daySB = new StringBuilder();

                                            for (int j = theDay.Length; j < value; j++)
                                            {
                                                daySB.Append("0");
                                            }

                                            daySB.Append(theDay);

                                            formattedValue = daySB.ToString();
                                        }
                                        else
                                        {
                                            formattedValue = theDay;
                                        }
                                    }
                                }
                                else
                                {
                                    formattedValue = theDay;
                                }

                                break;
                            }
                        case m_MonthCode:
                            {
                                string theMonth = month.ToString();

                                object fo = codeValue;

                                fo = Support.GetValue(codeValue);

                                if (codeValue != null)
                                {
                                    if (Data.MonthsArray.Count == 0)
                                    {
                                        formattedValue = theMonth;
                                    }
                                    else
                                    {
                                        if (fo is int)
                                        {
                                            int value = (int)fo;

                                            int totalMonths = Data.MonthsArray.Count;

                                            if (Data.UseRealTime)
                                            {
                                                switch (value)
                                                {
                                                    case 0:
                                                        {
                                                            formattedValue = DateTime.Now.ToString("MMMM");

                                                            break;
                                                        }
                                                    case 3:
                                                        {
                                                            formattedValue = DateTime.Now.ToString("MMM");

                                                            break;
                                                        }
                                                }
                                            }
                                            else
                                            {
                                                if (month > 0 && month <= totalMonths)
                                                {
                                                    switch (value)
                                                    {
                                                        case 0:
                                                            {
                                                                if (totalMonths > month - 1)
                                                                {
                                                                    MonthPropsObject mpo = Data.MonthsArray[month - 1];

                                                                    formattedValue = mpo.Name;
                                                                }

                                                                break;
                                                            }
                                                        case 3:
                                                            {
                                                                if (totalMonths > month - 1)
                                                                {
                                                                    MonthPropsObject mpo = Data.MonthsArray[month - 1];

                                                                    formattedValue = mpo.Name.Remove(3, mpo.Name.Length - 3);
                                                                }

                                                                break;
                                                            }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    formattedValue = theMonth;
                                }

                                break;
                            }
                        case m_YearCode:
                            {
                                string theYear = year.ToString();

                                object fo = codeValue;

                                fo = Support.GetValue(codeValue);

                                if (codeValue != null)
                                {
                                    if (fo is int)
                                    {
                                        int value = (int)fo;

                                        if (value <= theYear.Length)
                                        {
                                            int difference = theYear.Length - value;

                                            formattedValue = theYear.Remove(0, difference);
                                        }
                                        else
                                        {
                                            formattedValue = theYear;
                                        }
                                    }
                                }
                                else
                                {
                                    formattedValue = theYear;
                                }

                                break;
                            }
                        case m_SeasonCode:
                            {
                                if (codeValue == null)
                                {
                                    EffectsObject eo = EffectsEngine.GetEffects(o, true);

                                    formattedValue = eo.LateralSeason.ToString();
                                }

                                break;
                            }
                        case m_MoonNameCode:
                            {
                                if (codeValue != null)
                                {
                                    object fo = codeValue;

                                    fo = Support.GetValue(codeValue);

                                    if (fo is int)
                                    {
                                        int value = (int)fo;

                                        string moonName = MoonEngine.GetMoonName(value - 1);

                                        if (moonName != null)
                                        {
                                            formattedValue = moonName;
                                        }
                                    }
                                }

                                break;
                            }
                        case m_MoonPhaseCode:
                            {
                                if (codeValue != null)
                                {
                                    object fo = codeValue;

                                    fo = Support.GetValue(codeValue);

                                    if (fo is int)
                                    {
                                        int value = (int)fo;

                                        string moonPhaseName = MoonEngine.GetMoonPhaseName(value - 1);

                                        if (moonPhaseName != null)
                                        {
                                            formattedValue = moonPhaseName;
                                        }
                                    }
                                }

                                break;
                            }
                        case m_AmPmCode:
                            {
                                if (codeValue == null)
                                {
                                    int totalHours = Data.HoursPerDay;

                                    int changePoint = Convert.ToInt32((double)totalHours / 2.0);

                                    if (hour < changePoint)
                                    {
                                        formattedValue = "AM";
                                    }
                                    else
                                    {
                                        formattedValue = "PM";
                                    }
                                }

                                break;
                            }
                        case m_NthCode:
                            {
                                if (codeValue != null)
                                {
                                    int value = 0;

                                    switch (codeValue)
                                    {
                                        case "d":
                                            {
                                                value = day;

                                                break;
                                            }
                                        case "m":
                                            {
                                                value = month;

                                                break;
                                            }
                                    }

                                    switch (value)
                                    {
                                        case 11:
                                            {
                                                formattedValue = "th";

                                                break;
                                            }
                                        case 12:
                                            {
                                                formattedValue = "th";

                                                break;
                                            }
                                        case 13:
                                            {
                                                formattedValue = "th";

                                                break;
                                            }
                                        default:
                                            {
                                                string nth = value.ToString();

                                                if (nth.EndsWith("1"))
                                                {
                                                    formattedValue = "st";
                                                }
                                                else if (nth.EndsWith("2"))
                                                {
                                                    formattedValue = "nd";
                                                }
                                                else if (nth.EndsWith("3"))
                                                {
                                                    formattedValue = "rd";
                                                }
                                                else if (value != 0)
                                                {
                                                    formattedValue = "th";
                                                }

                                                break;
                                            }
                                    }
                                }

                                break;
                            }
                        case m_SpaceCode:
                            {
                                formattedValue = " ";

                                break;
                            }
                    }

                    sb.Append(formattedValue);

                    formatter = new StringBuilder();
                }
                else if (!formatCode && c == CodeChar)
                {
                    formatCode = true;
                }

                if (!formatCode)
                {
                    if (c != CodeChar)
                    {
                        sb.Append(c);
                    }
                }
                else
                {
                    if (c != CodeChar)
                    {
                        formatter.Append(c);
                    }
                }
            }

            return sb.ToString();
        }

        #endregion

        #region Message Formatting

        public static string ErrorMessageFormatter(string prop, object o, Type typeExpected)
        {
            string typeName = "null";

            if (o != null)
            {
                typeName = o.GetType().Name;
            }

            return String.Format("[{0}] cannot have a value of {1} type!  Please specify a value of {2} type.", prop, typeName, typeExpected.Name);
        }

        public static string ErrorMessageFormatter(string prop, object o, string minValue, string maxValue)
        {
            string value = null;

            if (o is int)
            {
                value = Convert.ToString((int)o);
            }
            else if (o is double)
            {
                value = Convert.ToString((double)o);
            }

            return String.Format("The specified value '{0}' for [{1}] must be within range from '{2}' to '{3}'!", value, prop, minValue, maxValue);
        }

        public static string ErrorMessageFormatter(string prop, object o, string minValue, string maxValue, Type typeExpected)
        {
            string typeName = "null";

            if (o != null)
            {
                typeName = o.GetType().Name;
            }

            return String.Format("[{0}] cannot have a value of {1} type!  Please specify a value of {2} type between '{3}' and '{4}'.", prop, typeName, typeExpected.Name, minValue, maxValue);
        }

        public static string ErrorMessageFormatter(string prop, object o, string minValueLowRange, string maxValueLowRange, string minValueHighRange, string maxValueHighRange)
        {
            string value = null;

            if (o is int)
            {
                value = Convert.ToString((int)o);
            }
            else if (o is double)
            {
                value = Convert.ToString((double)o);
            }

            return String.Format("The specified value '{0}' for [{1}] must be within range from '{2}' to '{3}' or '{4}' to '{5}'!", value, prop, minValueLowRange, maxValueLowRange, minValueHighRange, maxValueHighRange);
        }

        public static string ErrorMessageFormatter(string prop, object o, string[] enumList)
        {
            string value = "null";

            if (o is int)
            {
                value = Convert.ToString((int)o);
            }
            else if (o is double)
            {
                value = Convert.ToString((double)o);
            }
            else if (o is string)
            {
                value = Convert.ToString(o);
            }

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < enumList.Length; i++)
            {
                string enumItem = enumList[i];

                sb.Append(enumItem);

                if (i + 1 < enumList.Length)
                {
                    sb.Append(", ");
                }
            }

            return String.Format("The specified value '{0}' for [{1}] must be one of these values: '{2}'!", value, prop, sb.ToString());
        }

        public static string ErrorMessageFormatter(string prop, object o, string syntax)
        {
            string value = "null";

            if (o is int)
            {
                value = Convert.ToString((int)o);
            }
            else if (o is double)
            {
                value = Convert.ToString((double)o);
            }

            return String.Format("The specified value '{0}' for [{1}] syntax must be: {2}!", value, prop, syntax);
        }

        public static string VariableMessageFormatter(string prefix, string variableName, string value)
        {
            return VariableMessageFormatter(String.Format("{0}] [{1}", prefix, variableName), value, false);
        }

        public static string VariableMessageFormatter(string variableName, string value)
        {
            return VariableMessageFormatter(variableName, value, false);
        }

        public static string VariableMessageFormatter(string variableName, string value, bool append)
        {
            if (append)
            {
                return String.Format("[{0}] += '{1}'", variableName, value);
            }
            else
            {
                return String.Format("[{0}] = '{1}'", variableName, value);
            }
        }

        #endregion
    }
}