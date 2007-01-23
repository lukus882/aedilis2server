using System;

using Server;
using Server.Targeting;

namespace Arya.Auction
{
	/// <summary>
	/// General purpose target used by the auction system
	/// </summary>
	public class AuctionTarget : Target
	{
		private AuctionTargetCallback m_Callback;

		public AuctionTarget( AuctionTargetCallback callback, int range, bool allowground ) : base( range, allowground, TargetFlags.None )
		{
			m_Callback = callback;
		}

		protected override void OnTarget(Mobile from, object targeted)
		{
			try
			{
				m_Callback.DynamicInvoke( new object[] { from, targeted } );
			}
			catch
			{
				Console.WriteLine( "The auction system cannot access the cliloc.enu file. Please review the system instructions for proer installation" );
			}
		}

		protected override void OnTargetCancel(Mobile from, TargetCancelType cancelType)
		{
			if ( AuctionSystem.Running )
			{
				from.SendGump( new AuctionGump( from ) );
			}
		}
	}
}
