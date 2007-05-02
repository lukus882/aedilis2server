using System;
using Server;
using System.IO;

namespace Fatima.Misc
{
	public class LogWriter
	{
		public static void Initialize()
		{
			if ( !Directory.Exists( "Logs" ) )
				Directory.CreateDirectory( "Logs" );

			string directory = "Logs/Special";

			if ( !Directory.Exists( directory ) )
				Directory.CreateDirectory( directory );
		}

		private static StreamWriter m_Output;
		private static bool m_Enabled = true;

		public static bool Enabled{ get{ return m_Enabled; } set{ m_Enabled = value; } }

		public static StreamWriter Output{ get{ return m_Output; } }

		public static object Format( object o )
		{
			return o;
		}

		public static void WriteLine( string name, string item )
		{
			WriteLine(name,"Special",item);
		}

		public static void WriteLine( string name, string folder, string item )
		{
			WriteLine(name,folder,".log",item);
		}

		public static void WriteLine( string name, string folder, string extension, string item )
		{
			try
			{
				string path = Core.BaseDirectory;

				AppendPath( ref path, "Logs" );
				AppendPath( ref path, folder );

				if ( !Directory.Exists( path ) )
					Directory.CreateDirectory( path );

				path = Path.Combine( path, name + extension );

				using ( StreamWriter sw = new StreamWriter( path, true ) )
					sw.WriteLine( "{0} : {1}", DateTime.Now, item );
			}
			catch
			{
			}
		}

		private static char[] m_NotSafe = new char[]{ '\\', '/', ':', '*', '?', '"', '<', '>', '|' };

		public static void AppendPath( ref string path, string toAppend )
		{
			path = Path.Combine( path, toAppend );

			if ( !Directory.Exists( path ) )
				Directory.CreateDirectory( path );
		}

		public static string Safe( string ip )
		{
			if ( ip == null )
				return "null";

			ip = ip.Trim();

			if ( ip.Length == 0 )
				return "empty";

			bool isSafe = true;

			for ( int i = 0; isSafe && i < m_NotSafe.Length; ++i )
				isSafe = ( ip.IndexOf( m_NotSafe[i] ) == -1 );

			if ( isSafe )
				return ip;

			System.Text.StringBuilder sb = new System.Text.StringBuilder( ip );

			for ( int i = 0; i < m_NotSafe.Length; ++i )
				sb.Replace( m_NotSafe[i], '_' );

			return sb.ToString();
		}
	}
}