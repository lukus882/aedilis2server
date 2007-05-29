using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.SkillHandlers; //tracking arrow
using Server.Engines.PartySystem;
using System.Collections;
using System.Collections.Generic;
using Server.Gumps;

namespace Server.Gumps
{
	public class PartyRadarGump : Gump
	{
		public PartyRadarGump( Mobile from, Mobile pm ) : base( 5, 5 )
		{
			int gumpX = 0; // Default X value
			int gumpY = 0; // Default Y value

			int minY = 20; // Minimum Radar Y value - needs to be verified
			int maxY = 250; //Maximum Radar Y value - needs to be verified

			int minX = 20; // Minimum Radar X value - needs to be verified
			int maxX = 250; // Maximum Radar X value - needs to be verified

			//Base Gump Objects
			AddPage( 0 );
			AddImage( 0, 0, 5011 );
			AddImage( 134, 134, 1209 );

			//Positioning of Icon gumps on radar
			Party p = Engines.PartySystem.Party.Get( from );

			foreach ( PartyMemberInfo mi in p.Members )
			{
				PlayerMobile pl = mi.Mobile as PlayerMobile;

				if ( pl != from )
				{
					gumpX = 0;
					gumpY = 0;

					Point3D myLoc = new Point3D( from.X, from.Y, from.Z);
					Point3D theirLoc = new Point3D( pl.X, pl.Y, pl.Z);

					double distanceX = Math.Sqrt(Math.Pow(theirLoc.X - myLoc.X, 2) ); // calculate distance from player to party member on X axis
					double distanceY = Math.Sqrt(Math.Pow(theirLoc.Y - myLoc.Y, 2) ); // calculate distance from player to party member on Y axis

					if ( pl.X < from.X )
					{
						gumpX = (134 - (Convert.ToInt32(distanceY))); // converts (center of gump - distance between players to integer = X Axis =
					}
					else
					{
						gumpX = (134 + Convert.ToInt32(distanceY)); // converts (center of gump - distance between players to integer = X Axis =
					}

					if (pl.Y < from.Y )
					{
						gumpY = (134 - (Convert.ToInt32(distanceX))); // converts (center of gump - distance between players to integer = Y Axis =
					}
					else
					{
						gumpY = (134 + Convert.ToInt32(distanceX)); // converts (center of gump - distance between players to integer = Y Axis =
					}

					if ( pl == p.Leader)
					{
						if ( gumpX < minX )
						{
							gumpX = minX;
						}
						if ( gumpX > maxX )
						{
							gumpX = maxX;
						}
						if ( gumpY < minY )
						{
							gumpY = minY;
						}
						if ( gumpY > maxY )
						{
							gumpY = maxY;
						}

						AddImage( gumpX, gumpY, 2361, 0x489 ); // Add a blue 'dot' for party leader
						AddLabel( (gumpX - 12), (gumpY -17), 0x489, pl.Name ); // Add party leader's name above dot
					}
					else
					{
						if ( gumpX < minX )
						{
							gumpX = minX;
						}
						if ( gumpX > maxX )
						{
							gumpX = maxX;
						}
						if ( gumpY < minY )
						{
							gumpY = minY;
						}
						if ( gumpY > maxY )
						{
							gumpY = maxY;
						}

						AddImage( gumpX, gumpY, 2361, 0x559 ); // Add a green 'dot' for party member
						AddLabel( (gumpX - 12), (gumpY - 17), 0x559, pl.Name ); // Add party member's name above dot
					}

					if ( pm.InRange ( from, 30 ) ) // display indication arrow until player is within 30 tiles
					{
						if ( from.QuestArrow != null ) // stop arrow tracking for members within range
						{
							from.QuestArrow.Stop();
						}
					}
					else
					{
						if ( pm.InRange( from, 200 ) && pm.Map == from.Map)
						{
							from.QuestArrow = new TrackArrow( from, pm, 200 ); // uses arrow from Tracking to indicate direction of member outside of radar
						}
						else if ( from.QuestArrow != null ) // stop arrow tracking if too far out of range or different map
						{
							from.QuestArrow.Stop();
						}
					}
				}
			}

			GumpTimer gumpTimer = new GumpTimer( from, pm );
			gumpTimer.Start();
		}
	}

	public class GumpTimer : Timer
	{
		private Mobile m_From, m_Target;
		private int m_LastX, m_LastY;

		public GumpTimer( Mobile from, Mobile member ) : base( TimeSpan.FromSeconds( 0.5 ))
		{
			m_From = from;
			m_Target = member;
		}

		protected override void OnTick()
		{
			if ( m_From.NetState == null || m_From.Deleted || m_Target.Deleted || m_From.Map != m_Target.Map )
			{
				Stop();
				return;
			}

			Party p = Engines.PartySystem.Party.Get( m_From );
			if ( p != null )
			{
				//Refresh Gump
				m_From.CloseGump( typeof (PartyRadarGump));
                                 
			         NetState ns = m_From.NetState;
			        if (ns != null)
				{
    
                        List<Gump> gumps = new List<Gump>( ns.Gumps );
                        
			for ( int i = 0; i < gumps.Count; ++i )
			{
                                if (gumps[i].GetType() == typeof(PartyRadarGump) )
				{
                                          ns.RemoveGump(i);

				}
			}

				}
				if (! m_From.HasGump( typeof( PartyRadarGump)))
				{
					PartyRadarGump pr = new PartyRadarGump( m_From, m_Target );
					m_From.SendGump( pr ); // Custom Radar Gump
				}

				// can switch pages instead of resending/recycling?
			}
			else
			{
				m_From.CloseGump( typeof (PartyRadarGump));
				Stop();
				return;
			}
		}
	}
}