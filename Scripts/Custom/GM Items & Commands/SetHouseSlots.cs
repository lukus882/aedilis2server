using System;
using Server;
using Server.Mobiles; 
using Server.Gumps; 
using Server.Targeting;
using Server.Commands;
using Server.Commands.Generic;
using Server.Items;
using Server.Accounting;

namespace Server.Scripts.Commands
{
	public class SetHouseSlots
	{
		public static void Initialize()
		{
			CommandSystem.Register( "SetHouseSlots", AccessLevel.Administrator, new CommandEventHandler( SetHouseSlots_OnCommand ) );
		}

		[Usage( "SetHouseSlots [amount]" )]
		[Description( "Sets the targeted players house slots." )]
		private static void SetHouseSlots_OnCommand( CommandEventArgs e )
		{
			if ( e.Mobile is PlayerMobile )
			{
				if ( e.Length > 0 )
				{
                  			try 
                  			{ 
                     				int amount = e.GetInt32( 0 );
						e.Mobile.Target = new HouseSlotTarget( amount );
					}
                 	 		catch 
                 			{ 
						e.Mobile.SendMessage( "You must enter a number amount." ); 
                  			} 
				}
			}
			else
			{
				e.Mobile.SendMessage( "This command only works on players." );
			}
		}

		public class HouseSlotTarget : Target
		{
			private int m_Amount;

			public HouseSlotTarget( int amount ) : base( -1, true, TargetFlags.None )
			{
				m_Amount = amount;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is PlayerMobile )
				{
					Mobile m = (Mobile)o;
					Account acct = m.Account as Account;

					if ( m_Amount < 1 )
					{
						from.SendMessage( "This value is to low, Please select a higher value." );
						return;
					}

					acct.SetTag( "maxHouses", m_Amount.ToString() );
					m.SendMessage( "Your maxium Housing slots has changed to {0}.", m_Amount );
					from.SendMessage( "House slots set to {0}.", m_Amount );
				}	
			}
		}
	}
}