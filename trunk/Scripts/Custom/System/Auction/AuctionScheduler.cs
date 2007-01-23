using System;
using System.Collections;
using System.Timers;

namespace Arya.Auction
{
	/// <summary>
	/// Summary description for AuctionScheduler.
	/// </summary>
	public class AuctionScheduler
	{
		/// <summary>
		/// Occurs when a deadline is reached by the auction scheduler
		/// </summary>
		public static event EventHandler DeadlineReached;

		private static Timer m_Timer;
		private static DateTime m_Deadline = DateTime.MaxValue;

		/// <summary>
		/// Gets the next deadline
		/// </summary>
		public static DateTime Deadline
		{
			get
			{
				return m_Deadline;
			}
		}

		public static void Initialize()
		{
			m_Timer = new Timer( 5000 );
			m_Timer.Elapsed += new ElapsedEventHandler( OnTimer );
			ResetTimer();
		}

		public static void Stop()
		{
			if ( m_Timer != null && m_Timer.Enabled )
			{
				m_Timer.Stop();
				m_Timer = null;
			}
		}

		private static void ResetTimer()
		{
			if ( AuctionSystem.Running )
			{
				CalculateDeadline();
			}
			m_Timer.Start();
		}

		/// <summary>
		/// Calculates the next deadline for the scheduler
		/// </summary>
		private static void CalculateDeadline()
		{
			ArrayList list = new ArrayList( AuctionSystem.Auctions );
			list.AddRange( AuctionSystem.Pending );

			m_Deadline = DateTime.MaxValue;

			foreach( AuctionItem auction in list )
			{
				if ( auction.Deadline < m_Deadline )
				{
					m_Deadline = auction.Deadline;
				}
			}
		}

		/// <summary>
		/// This method accepts a new deadline being added to the system
		/// </summary>
		/// <param name="deadline">The new deadline</param>
		public static void UpdateDeadline( DateTime deadline )
		{
			if ( deadline < m_Deadline )
			{
				m_Deadline = deadline;
			}
		}

		/// <summary>
		/// Fires the DeadlineReached event
		/// </summary>
		private static void OnDeadlineReached()
		{
			if ( DeadlineReached != null )
			{
				DeadlineReached( null, EventArgs.Empty );
			}
		}

		private static void OnTimer(object sender, ElapsedEventArgs e)
		{
			if ( m_Deadline < DateTime.Now )
			{
				m_Timer.Stop();

				OnDeadlineReached();

				ResetTimer();
			}
		}
	}
}