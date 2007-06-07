//Dreamerwolf (dreamerwolf@hotmail.com) 2003/10/22 2st Edition
//Login, Logout, Fastwalk, and Chat logs function
using System;
using System.IO;
using Server;
using Server.Network;
using System.Collections;

namespace Server.Misc
{
	public class LogRecorder
	{
		public static void Initialize()
		{
			//Login & Logout
			EventSink.Login += new LoginEventHandler( EventSink_Login );
			EventSink.Logout += new LogoutEventHandler( EventSink_Logout );
		}
		private static void EventSink_Login( LoginEventArgs args )
		{
			Stream fileStream = null;
			StreamWriter writeAdapter = null;
			Mobile m = args.Mobile;
			try
			{
				if ( !Directory.Exists( "logins" ) ) Directory.CreateDirectory( "logins" );
				fileStream = File.Open("logins/"+args.Mobile.Name+".log", FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
				writeAdapter = new StreamWriter(fileStream);
				writeAdapter.WriteLine(args.Mobile.Name + " " + DateTime.Now + "  Login" );
				writeAdapter.Close();
			}
			catch
			{
				Console.WriteLine( "Record Error......{0} Login",args.Mobile.Name );
				return;
			}
		}
		private static void EventSink_Logout( LogoutEventArgs args )
		{
			Stream fileStream = null;
			StreamWriter writeAdapter = null;
			Mobile m = args.Mobile;
			try
			{
				if ( !Directory.Exists( "logins" ) ) Directory.CreateDirectory( "logins" );
				fileStream = File.Open("logins/"+args.Mobile.Name+".log", FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
				writeAdapter = new StreamWriter(fileStream);
				writeAdapter.WriteLine(args.Mobile.Name + " " + DateTime.Now + "  Logout" );
				writeAdapter.Close();
			}
			catch
			{
				Console.WriteLine( "Record Error......{0} Logout",args.Mobile.Name );
				return;
			}
		} 
	}
}