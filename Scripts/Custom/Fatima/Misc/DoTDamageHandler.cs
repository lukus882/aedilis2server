using System;
using System.Collections;
using Server;

namespace Fatima.Misc
{
	public delegate void DotTickEventHandler( DotTickEventArgs e );

	public class AOSDamageEntry
	{
		private Mobile m_Attacker;
		private Mobile m_Defender;

		private int m_Damage;

		//Resists, all in percentages in integer form. 0-100% are expected parameters.
		private int m_PctPhys;
		private int m_PctFire;
		private int m_PctCold;
		private int m_PctPoison;
		private int m_PctEnergy;

		public Mobile Attacker{ get{ return m_Attacker; } }
		public Mobile Defender{ get{ return m_Defender; } }

		public int Damage{ get{ return m_Damage; } }

		public int PctPhys{ get{ return m_PctPhys; } }
		public int PctFire{ get{ return m_PctFire; } }
		public int PctCold{ get{ return m_PctCold; } }
		public int PctPoison{ get{ return m_PctPoison; } }
		public int PctEnergy{ get{ return m_PctEnergy; } }


		public AOSDamageEntry( Mobile defender, Mobile attacker, int damage, int phys, int fire, int cold, int pois, int nrgy )
		{
			m_Defender = defender;
			m_Attacker = attacker;
			m_Damage = damage;

			m_PctPhys = phys;
			m_PctFire = fire;
			m_PctCold = cold;
			m_PctPoison = pois;
			m_PctEnergy = nrgy;
		}
	}

	public class DotTickEventArgs : EventArgs
	{
		private Mobile m_Attacker;
		private Mobile m_Defender;
		private bool m_End;

		public Mobile Attacker{ get{ return m_Attacker; } }
		public Mobile Defender{ get{ return m_Defender; } }
		public bool End{ get{ return m_End; }set{ m_End = value; } }

		public DotTickEventArgs( Mobile attacker, Mobile defender, bool dotEnded )
		{
			m_Attacker = attacker;
			m_Defender = defender;
			m_End = dotEnded;
		}
	}

	public class DoTDamageEntry
	{
		private DotTickEventHandler m_TickHandle;
		private bool m_AOSDmg;

		private int m_TickLength;
		private TimeSpan m_Duration;
		private DateTime m_LastTick;
		private DateTime m_Start;
		private AOSDamageEntry m_DMG;

		public DotTickEventHandler TickHandle{ get{ return m_TickHandle; } }
		public bool AOSDmg{ get{ return m_AOSDmg; } }

		public int TickLength{ get{ return m_TickLength; } }
		public TimeSpan Duration{ get{ return m_Duration; } }
		public DateTime LastTick{ get{ return m_LastTick; } set{ m_LastTick = value; } }
		public DateTime Start{ get{ return m_Start; } }
		public AOSDamageEntry DMG{ get{ return m_DMG; } }

		public Mobile Attacker{ get{ return DMG.Attacker; } }
		public Mobile Defender{ get{ return DMG.Defender; } }

		//Time to do damage, yet?
		public bool IsDamageTime
		{
			get
			{
				return (DateTime.Now - m_LastTick) >= TimeSpan.FromSeconds( m_TickLength );
			}
		}

		//has the spell/effect run out?
		public bool HasExpired
		{
			get
			{
				return (DateTime.Now - m_Start) > m_Duration || !Defender.Alive;
			}
		}

		public void PerformDamage()
		{
			if (AOSDmg)
			{
				AOS.Damage( Defender, Attacker, DMG.Damage, DMG.PctPhys, DMG.PctFire, DMG.PctCold, DMG.PctPoison, DMG.PctEnergy );
			}
			else
			{
				Defender.Damage( DMG.Damage, Attacker );
			}
		}

		public void Slice( bool end )
		{
			if ( m_TickHandle != null )
				m_TickHandle( new DotTickEventArgs( Attacker, Defender, end ) );
		}

		public DoTDamageEntry( DotTickEventHandler tickHandle, bool aosDmg, int tickLength, TimeSpan duration, AOSDamageEntry dmg )
		{
			m_TickHandle = tickHandle;
			m_AOSDmg = aosDmg;
			m_TickLength = tickLength;

			m_Duration = duration;
			m_LastTick = DateTime.Now;
			m_Start = DateTime.Now;
			m_DMG = dmg;
		}
	}

	public class DoTHandler
	{
		private static InternalTimer m_Timer = new InternalTimer();
		private static Hashtable m_Mobiles = new Hashtable(); 
		//key = SERIAL (Defender), object => Hashtable [KEY: typeof( CALLING TYPE ), OBJECT: DoTDamageEntry]

		//public event FatimaGraphicFinishedEventHandler GraphicFinished;

		public static void NewDoTDamage( Type dotObject, DoTDamageEntry entry )
		{
			if (entry == null)
				return;

			if ( !m_Mobiles.ContainsKey( entry.Defender ) )
			{
				m_Mobiles.Add( entry.Defender, new Hashtable() ); 
			}

			Hashtable innerTable = (Hashtable)m_Mobiles[entry.Defender];

			if ( !innerTable.ContainsKey( dotObject ) )
			{
				innerTable.Add( dotObject, entry );
			}
			else
			{
				innerTable[dotObject] = entry; //update, essentially.
			}

			TriggerTimer();
		}

		public static void TriggerTimer()
		{
			if (m_Timer == null)
				m_Timer = new InternalTimer();

			m_Timer.Start();
		}

		private class InternalTimer : Timer
		{
			public InternalTimer() : base( TimeSpan.FromSeconds( 1 ) , TimeSpan.FromSeconds( 1 ) ) 
			{
			}

			protected override void OnTick()
			{
				if (m_Mobiles.Count <= 0)
					return;

				ArrayList keys = new ArrayList( m_Mobiles.Keys );
				foreach( object key in keys )
				{
					Hashtable innerList = (Hashtable)m_Mobiles[key];
					if (innerList != null && innerList.Count > 0)
					{
						ArrayList innerKeys = new ArrayList( innerList.Keys );
						foreach( Type callingType in innerKeys )
						{
							DoTDamageEntry dmgData = (DoTDamageEntry)innerList[callingType];

							if (dmgData != null && dmgData.Defender.Map != Map.Internal ) //no damage if they're in the internal map. However, the dot can expire...
							{
								if ( dmgData.HasExpired )
								{
									innerList.Remove( callingType ); //kill the dot damage.

									if (innerList.Count <= 0) //remove them, if they have no other dots on them.
										m_Mobiles.Remove( key );

									dmgData.Slice( true );
								}
								if ( dmgData.IsDamageTime )
								{
									dmgData.PerformDamage();
									dmgData.LastTick = DateTime.Now;
									dmgData.Slice( false );
								}
							}
						}
					}
				}
			}
		}
	}
}