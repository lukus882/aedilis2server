using System;
using System.Diagnostics;
using System.IO;

using Server;
using Server.Accounting;
using Server.Commands;
using Server.Mobiles;
using Server.Network;

namespace Khazman.Logging {
	public class SpeechLogging {
		private const bool Enabled = true;
		private static bool ConsoleEnabled = false;

		private static StreamWriter writer;
		private static string LogPath = Path.Combine( "Logs\\Speech", String.Format( "{0}.log", DateTime.Now.ToLongDateString() ) );

		public static void Initialize() {
			if( Enabled ) {
				Console.Write( "SpeechLogging: Initializing..." );
				Stopwatch sw = new Stopwatch();
				sw.Start();

				EventSink.Speech += new SpeechEventHandler( EventSink_Speech );

				if( !Directory.Exists( "Logs" ) )
					Directory.CreateDirectory( "Logs" );

				string directory = "Logs\\Speech";

				if( !Directory.Exists( directory ) )
					Directory.CreateDirectory( directory );

				try {
					writer = new StreamWriter( LogPath, true );
					writer.AutoFlush = true;

					writer.WriteLine( "####################################" );
					writer.WriteLine( "Logging started on {0}", DateTime.Now );
					writer.WriteLine();

					writer.Close();

					Console.WriteLine( "done ({0:F2} seconds)", (sw.ElapsedMilliseconds / 1000) );
				}
				catch( Exception e ) {
					Console.WriteLine( "failed:\n{0}", e );
				}
				finally {
					sw.Stop();
				}

				CommandSystem.Register( "ConsoleListen", AccessLevel.Administrator, new CommandEventHandler( ConsoleListen_OnCommand ) );
			} else
				Console.WriteLine( "SpeechLogging: disabled." );
		}

		public static object Format( object o ) {
			if( o is Mobile ) {
				Mobile m = (Mobile)o;

				if( m.Account == null )
					return String.Format( "{0} (no account)", m );
				else
					return String.Format( "{0} ('{1}')", m, ((Account)m.Account).Username );
			} else if( o is Item ) {
				Item item = (Item)o;

				return String.Format( "0x{0:X} ({1})", item.Serial.Value, item.GetType().Name );
			}

			return o;
		}

		public static void WriteDroppedLog( Mobile from, string format, params object[] args ) {
			WriteDroppedLog( from, String.Format( format, args ) );
		}

		public static void WriteDroppedLog( Mobile from, string text ) {
			try {
				using( StreamWriter w = new StreamWriter( @"Logs\DroppedItems.log", true ) ) {
					w.WriteLine( "{0}: {1}: {2}", DateTime.Now, from.NetState, text );
					w.Close();
				}
			}
			catch { }
		}

		public static void WriteLine( Mobile from, string format, params object[] args ) {
			WriteLine( from, String.Format( format, args ) );
		}

		public static void WriteLine( Mobile from, string text ) {
			if( !Enabled )
				return;

			try {
				writer = new StreamWriter( LogPath, true );
				writer.AutoFlush = true;

				writer.WriteLine( "{0}: {1}: {2}", DateTime.Now, from.NetState, text );

				writer.Close();

				string path = Core.BaseDirectory;
				string name = (((Account)from.Account) == null ? from.RawName : ((Account)from.Account).Username);

				AppendPath( ref path, "Logs" );
				AppendPath( ref path, "Speech" );
				AppendPath( ref path, from.AccessLevel.ToString() );
				path = Path.Combine( path, String.Format( "{0}.log", name ) );

				using( StreamWriter sw = new StreamWriter( path, true ) ) {
					sw.WriteLine( "{0}: {1}: {2}", DateTime.Now, from.NetState, text );
					sw.Close();
				}
			}
			catch { }
		}

		private static char[] m_NotSafe = new char[] { '\\', '/', ':', '<', '>', '|', '{', '}' };

		public static void AppendPath( ref string path, string toAppend ) {
			path = Path.Combine( path, toAppend );

			if( !Directory.Exists( path ) )
				Directory.CreateDirectory( path );
		}

		public static string Safe( string ip ) {
			if( ip == null )
				return "null";

			ip = ip.Trim();

			if( ip.Length == 0 )
				return "empty";

			bool isSafe = true;

			for( int i = 0; isSafe && i < m_NotSafe.Length; ++i )
				isSafe = (ip.IndexOf( m_NotSafe[i] ) == -1);

			if( isSafe )
				return ip;

			System.Text.StringBuilder sb = new System.Text.StringBuilder( ip );

			for( int i = 0; i < m_NotSafe.Length; ++i )
				sb.Replace( m_NotSafe[i], '_' );

			return sb.ToString();
		}

		public static void EventSink_Speech( SpeechEventArgs e ) {
			WriteLine( e.Mobile, "{0}: {1}", Format( e.Mobile ), e.Speech );

			if( ConsoleEnabled )
				Console.WriteLine( e.Mobile.Name + String.Format( " ({0}): ", ((Account)e.Mobile.Account).Username ) + e.Speech );
		}

		[Usage( "ConsoleListen <true | false>" )]
		[Description( "Enables or disables outputting speech to the console." )]
		public static void ConsoleListen_OnCommand( CommandEventArgs e ) {
			if( e.Length == 1 ) {
				ConsoleEnabled = e.GetBoolean( 0 );
				e.Mobile.SendMessage( "Console speech output has been {0}.", ConsoleEnabled ? "enabled" : "disabled" );
				Console.WriteLine( "World listen has been {0}.", ConsoleEnabled ? "enabled" : "disabled" );
			} else {
				e.Mobile.SendMessage( "Format: ConsoleListen <true | false >" );
			}
		}
	}
}
