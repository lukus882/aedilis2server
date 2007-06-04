/*
Notify AutoRestart V1.3
*/
using System;
using System.IO;
using System.Diagnostics;
using Server;
using Server.Commands;

namespace Server.Misc
{
	public class AutoRestart : Timer
	{		
		//Automatic Restart Settings
		public static bool Enabled = true; //is AutoRestarting enabled?
		private static TimeSpan RestartDay = TimeSpan.FromDays( 1 ); //How many days to remain up before AutoRestart (10/10/2004 + 1 = 10/11/2004) Does NOT affect [restart command
		private static TimeSpan RestartHour = TimeSpan.FromHours( 9 ); //What hour of the day to auto restart (12am + 2 = 2am) Does Not affect [restart command
		private static TimeSpan RestartMinute = TimeSpan.FromMinutes( 50 ); //What minute of the day to auto restart (2am + 30 = 2:30am) Does NOT affect [restart command
		
		//Global Settings Applies to both Automatic Retarts and [restart command		
		private static TimeSpan m_Delay = TimeSpan.FromMinutes( 10.0 ); // delay of when server will restart after command is issued or RestartTime reaches 0
		private static TimeSpan m_WarningDelay = TimeSpan.FromSeconds( 120 ); // how often to repeat restarting warning

		//Applies only to [restart Command
		private static bool ShowAdmin = false; //Announce to the players who used restart?
		
		//Do not edit below
		private static bool m_Restarting;
		private static DateTime m_RestartTime;
		public static bool Restarting{ get{ return m_Restarting; } } 
		private static int count;

		public static void Initialize()
		{
			CommandSystem.Register( "Restart", AccessLevel.GameMaster, new CommandEventHandler( Restart_OnCommand ) );
			CommandSystem.Register( "AutoRestart", AccessLevel.Administrator, new CommandEventHandler( AutoRestart_OnCommand ) );
			CommandSystem.Register( "NextRestart", AccessLevel.Player, new CommandEventHandler( NextRestart_OnCommand ) );
			new AutoRestart().Start();
		}

		[ Usage( "Restart <minutes>" ),
		Description( "Restarts the server with an optional delay in minutes before actual restart" ) ]
		public static void Restart_OnCommand( CommandEventArgs e )
		{
			if ( m_Restarting )
			{	
				if ( e.Mobile != null )
					e.Mobile.SendMessage( "The server is already restarting." );
					
				return;
			}
			else
			{
				if ( e.Mobile != null )
					e.Mobile.SendMessage( "You have initiated server restart." );
				
				int minutes = (int)m_Delay.TotalMinutes;
				try 
				{ 
					minutes = int.Parse( e.Arguments[ 0 ] ); 					
					m_Delay = TimeSpan.FromMinutes( minutes );
				}
				catch {}
				
				if ( e.Mobile != null && ShowAdmin )
					World.Broadcast( 0x22, true, "A restart has been issued by {0}.", e.Mobile.Name );
				
				if ( e.Mobile != null )
					Console.WriteLine( "Server Restart command issued by {0}", e.Mobile.Name );
				
				count = 0; //set how many times we warned the players to none
				Enabled = true; //if auto restarts disabled bypass safeguard
				m_RestartTime = DateTime.Now; //restart now
			}
		}
		
		//New in Version 1.2		
		public static bool AutRestartEnabled
		{
			get{ return Enabled; }
			set{ Enabled = value; }
		}
		
		[ Usage( "AutoRestart <true | false>" ),
		Description( "Enables or disables current automatic shard restart status." ) ]
		public static void AutoRestart_OnCommand( CommandEventArgs e )
		{
			if ( e.Length == 1 )
			{
				Enabled = e.GetBoolean( 0 );
				if ( Enabled )
				{
					m_RestartTime = DateTime.Now.Date;
					if ( m_RestartTime < DateTime.Now )
						m_RestartTime += RestartDay + RestartHour + RestartMinute;
				}
					
				e.Mobile.SendMessage( "Auto restarting has been {0}.", Enabled ? "enabled" : "disabled" );
			}
			else
			{
				e.Mobile.SendMessage( "Format: AutoRestart <true | false>" );
				e.Mobile.SendMessage( "Auto restarting is currently {0}.", Enabled ? "enabled" : "disabled" );
				return;
			}
		}
		
		//New to version 1.1
		[ Usage( " NextRestart" ),
		Description( "Displays the date and time of the next auto restart" ) ]
		public static void NextRestart_OnCommand( CommandEventArgs e )
		{
			if ( m_Restarting )
			{
				e.Mobile.SendMessage( "The server is already restarting." );
				return;
			}
			else if ( Enabled )
			{
				e.Mobile.SendMessage( "The next automatic restart will be\n{0}.", m_RestartTime );
				return;
			}
			else
			{
				e.Mobile.SendMessage( "Auto restarting is disabled.\nUse AutoRestart true to enable" );
				return;
			}
		}

		public AutoRestart() : base( TimeSpan.FromSeconds( 1.0 ), TimeSpan.FromSeconds( 1.0 ) )
		{
			Priority = TimerPriority.OneSecond;
			
			m_RestartTime = DateTime.Now.Date;

			if ( m_RestartTime < DateTime.Now )
				m_RestartTime += RestartDay + RestartHour + RestartMinute;
		}

		private void Warning_Callback()
		{
			World.Broadcast( 0x22, true, "The server is restarting and will be back online shortly." );
		}

		private void Restart_Callback()
		{	
			Warning_Callback();
			World.Save();
			Process.Start( Core.ExePath );
			Core.Process.Kill();
		}
		
		private void MultiWarning_Callback()
		{
			int s = (int)m_Delay.TotalSeconds - count;
			int m = s / 60;
			s %= 60;
			if ( m > 0 && s > 0 )
				World.Broadcast( 0x22, true, "The server will restart in {0} minute{1} and {2} second{3}.", m, m != 1 ? "s" : "", s, s != 1 ? "s" : "" );
			else if ( m > 0 )
				World.Broadcast( 0x22, true, "The server will restart in {0} minute{1}.", m, m != 1 ? "s" : "" );
			else if ( s > 0 )
				World.Broadcast( 0x22, true, "The server will restart in {0} second{1}.", s, s != 1 ? "s" : "" );
				
			count += (int)m_WarningDelay.TotalSeconds;
		}

		protected override void OnTick()
		{
			if ( m_Restarting || !Enabled )
				return;

			if ( DateTime.Now < m_RestartTime )
				return;

			m_Restarting = true;
			
			if ( m_Delay == TimeSpan.Zero )
			{
				Restart_Callback();
			}
			else
			{
				if ( m_WarningDelay > TimeSpan.Zero )
					Timer.DelayCall( TimeSpan.Zero, m_WarningDelay, new TimerCallback( MultiWarning_Callback ) );
					
				Timer.DelayCall( m_Delay, new TimerCallback( Restart_Callback ) );
			}
		}
	}
}