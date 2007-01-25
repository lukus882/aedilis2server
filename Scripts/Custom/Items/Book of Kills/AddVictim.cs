//Author plus
using System;
using Server;
using Server.Items;
using System.Collections;

namespace Server.Mobiles
{ 
	public class KillBookSystem
	{		
		public static void Initialize()
		{ 
			EventSink.PlayerDeath += new PlayerDeathEventHandler(EventSink_PlayerDeath); 	
		}		

		public static void EventSink_PlayerDeath(PlayerDeathEventArgs e)
		{   
			Killed(e.Mobile);
		}
           	
		public static void Killed( Mobile m ) //done
		{
		
			PlayerMobile owner = m as PlayerMobile; 
			
			Mobile m_Killer = (Mobile)m.LastKiller;
			

			if ( m_Killer != null && m_Killer.Player && owner != null && owner.Player )
			{ 					
				KillBook book = m_Killer.Backpack.FindItemByType( typeof( KillBook ), true ) as KillBook;
				
				if( book != null )
					{
						if( ( owner != book.BookOwner ) && ( m_Killer == book.BookOwner ) )
						{					
							book.AddEntry(owner.Name, 1);
							book.TotKills++;
						}
					}
			}
			
			if( owner != null && owner.Player && m_Killer != null && m_Killer.Player )
			{
				KillBook deathbook = owner.Backpack.FindItemByType( typeof( KillBook ), true) as KillBook;
				
				if ( deathbook != null )
				{
					if ( owner == deathbook.BookOwner )
					{
						if( deathbook.TotDeaths >= 0 )
							deathbook.TotDeaths++;
					}
				}
			}
		}
	} 
} 
