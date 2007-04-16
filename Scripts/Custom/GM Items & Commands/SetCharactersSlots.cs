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
	public class SetCharacterSlots
	{
		public static void Initialize()
		{
			CommandSystem.Register( "SetCharSlots", AccessLevel.Administrator, new CommandEventHandler( SetCharSlots_OnCommand ) );
		}

		[Usage( "SetCharSlots [amount]" )]
		[Description( "Sets the targeted players character slots, NOTE: Value can only be 1, 5, or 6." )]
		private static void SetCharSlots_OnCommand( CommandEventArgs e )
		{
			if ( e.Mobile is PlayerMobile )
			{
				if ( e.Length > 0 )
				{
                  			try 
                  			{ 
                     				int amount = e.GetInt32( 0 );
						e.Mobile.Target = new CharSlotTarget( amount );
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

		public class CharSlotTarget : Target
		{
			private int m_Amount;

			public CharSlotTarget( int amount ) : base( -1, true, TargetFlags.None )
			{
				m_Amount = amount;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is PlayerMobile )
				{
					Mobile m = (Mobile)o;
					Account acct = m.Account as Account;

					if ( m_Amount > 1 && m_Amount < 5 || m_Amount > 6 || m_Amount < 1 )
					{
						from.SendMessage( "Bad Format. Value must by 1, 5, or 6." );
						return;
					}

					acct.SetTag( "maxChars", m_Amount.ToString() );
					m.SendMessage( "Your maxium character slots has changed to {0}.", m_Amount );
					from.SendMessage( "Character slots set to {0}.", m_Amount );
				}	
			}
		}
	}
}