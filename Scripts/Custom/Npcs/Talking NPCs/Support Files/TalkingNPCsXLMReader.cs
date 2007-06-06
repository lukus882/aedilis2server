// 19APR2006 Convertion to RunUO by RavonTUS 
//make sure to save Story.xml in your \data folder.

using System;
using System.IO;
using System.Xml;
using System.Collections;
using Server;

namespace Server
{
    public class TalkingNPCsXMLReader
    {
        private string m_Type;
        private string[] m_List;

        public string Type { get { return m_Type; } }
        public string[] List { get { return m_List; } }

        public TalkingNPCsXMLReader(string type, XmlElement xml)
        {
            m_Type = type;
            m_List = xml.InnerText.Split(',');
        }

        public string GetRandomName()
        {
            if (m_List.Length > 0)
                return m_List[Utility.Random(m_List.Length)].Trim();

            return "";
        }

        public static TalkingNPCsXMLReader GetTalkingNPCsXMLReader(string type)
        {
            return (TalkingNPCsXMLReader)m_Table[type];
        }

        public static string RandomName(string type)
        {
            TalkingNPCsXMLReader list = GetTalkingNPCsXMLReader(type);

            if (list != null)
                return list.GetRandomName();

            return "";
        }

        private static Hashtable m_Table;

        static TalkingNPCsXMLReader()
        {
            // + Custom/An Nox Custom/TalkingTownFolks/TalkingNPCsXMLReader.cs:
            //    CS0618: Line 53: 'System.Collections.CaseInsensitiveHashCodeProvider' is obsolete: 'Please use StringComparer instead.'
            //    CS0618: Line 53: 'System.Collections.Hashtable.Hashtable(System.Collections.IHashCodeProvider, System.Collections.IComparer)' is obsolete: 'Please use Hashtable(IEqualityComparer) instead.'

            m_Table = new Hashtable(CaseInsensitiveHashCodeProvider.Default, CaseInsensitiveComparer.Default);

            //may need to change the path to make it work correctly.
            //string filePath = Path.Combine(Core.BaseDirectory, "Data/names.xml");
            string filePath = Path.Combine(Core.BaseDirectory, "Data/TalkingNPCs.xml");

            if (!File.Exists(filePath))
                return;

            try
            {
                Load(filePath);
            }
            catch (Exception e)
            {
                Console.WriteLine("Warning: Exception caught loading name lists:");
                Console.WriteLine(e);
            }
        }

        private static void Load(string filePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlElement root = doc["names"];

            foreach (XmlElement element in root.GetElementsByTagName("namelist"))
            {
                string type = element.GetAttribute("type");

                if (type == null || type == String.Empty)
                    continue;

                try
                {
                    TalkingNPCsXMLReader list = new TalkingNPCsXMLReader(type, element);

                    m_Table[type] = list;
                }
                catch
                {
                }
            }
        }
    }
}