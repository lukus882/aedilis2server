using System;
using System.IO;
using System.Collections;
using Server.Items;
using Server.Gumps;

namespace Server.Mobiles
{
	public class BaseGuide : BaseCreature
	{
		public static string path = "Data/guide.txt";

		private int m_Count;
		private Timer m_Timer;
		private bool m_NoWalk;
		private bool m_Active;
		private bool m_Random;
		private TimeSpan m_Delay;

		[CommandProperty( AccessLevel.GameMaster )]
		public TimeSpan Delay{ get{ return m_Delay; } set{ m_Delay = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public bool NoWalk
		{
			get{ return m_NoWalk; }
			set
			{
				m_NoWalk = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Active
		{
			get{return m_Active;}
			set
			{
				m_Active = value;
				this.OnActivate();
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Random
		{
			get
			{
					return m_Random;
			}
			set { m_Random = value; }
		}

		public int Count{ get{ return m_Count; } set{ m_Count = value; } }


		[Constructable]
		public BaseGuide() : base( AIType.AI_Thief, FightMode.None, 10, 1, 0.8, 1.6 )
		{
			InitStats( 100, 100, 25 );

			Female = false;
			Body = 0x190;
			Hue = Utility.RandomSkinHue();
			NameHue = 0x35;
			Frozen = true;
			m_Active = false;
			m_NoWalk = true;
			m_Random = false;
			m_Delay = new TimeSpan( 0, 0, 10 );
			this.Blessed = true;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.AccessLevel >= AccessLevel.Seer )
				from.SendGump( new GuideGump( this, path ) );
			else
				base.OnDoubleClick( from );
		}
      
		public override void OnDelete()
		{
			BaseGuide m_Crier = this as BaseGuide;
			base.OnDelete();
		}

		public override void OnSpeech( SpeechEventArgs args )
		{
			if( args.Mobile.InRange( this, 4 ))
			{
				if ( args.Speech.ToLower().IndexOf( "help" ) >= 0 )
				{
					string message = SpamMessage( this );
					this.Say( message );
				}
			}
		}

		public virtual void OnActivate()
		{
			TimeSpan delay;

			if ( m_Timer != null )
			{
				m_Timer.Stop();
				m_Timer = null;
			}

			if ( m_Timer == null )
			{
					delay = Delay;
					m_Timer = new SpamTimer( this, delay );
					m_Timer.Start();
			}
			
			if ( !m_Active)
			{
				m_Timer.Stop();
				m_Timer = null;
			}
		}

		public class SpamTimer : Timer
		{
			private BaseGuide m_Crier;

			public SpamTimer( BaseGuide crier, TimeSpan m_Delay ) : base( TimeSpan.Zero, m_Delay )
			{
				m_Crier = crier as BaseGuide;
			}

			protected override void OnTick()
			{
				string message = SpamMessage( m_Crier );
				m_Crier.Say( message );
			}
		}

		public static string SpamMessage( BaseGuide crier )
		{
			ArrayList m_Lines = new ArrayList();

			if ( File.Exists( path ) )
			{
				using ( StreamReader ip = new StreamReader( path ) )
				{
					string line;

					while ( (line = ip.ReadLine()) != null )
					{
						if ( line.Length > 0 )
							m_Lines.Add( line );
					}
				}
			}

			string message;

			if ( m_Lines.Count == 0 )
			{
				message = "";
			}
			else if ( (crier.Active && crier.Random) )
			{
				int i = Utility.Random( m_Lines.Count );
				message = ""+ m_Lines[i];
			}
			else
			{
				try
				{
					message = ""+ m_Lines[crier.Count++];
					if ( crier.Count == m_Lines.Count )
					crier.Count = 0;
				}
				catch
				{
					crier.Count = 0;
					message = ""+ m_Lines[crier.Count++];
				}
			}
			return message;
		}

		public BaseGuide( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( m_NoWalk );
			writer.Write( m_Random );
			writer.Write( m_Delay );
			writer.Write( this.NoWalk );
			writer.Write( m_Active );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			switch ( version )
			{
				case 0:
				{
					m_NoWalk = reader.ReadBool();
					m_Random = reader.ReadBool();
					m_Delay = reader.ReadTimeSpan();
					this.NoWalk = reader.ReadBool();
					m_Active = reader.ReadBool();

					break;
				}
			}
		}
	}
}
