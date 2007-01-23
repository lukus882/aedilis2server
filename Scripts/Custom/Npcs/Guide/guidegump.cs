using System;
using System.IO;
using System.Collections;
using System.Text;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Targeting;
using Server.Targets;

namespace Server.Gumps
{
	public class GuideGump : Gump
	{
		public static string path;
		public BaseGuide m_Crier;

		public GuideGump( BaseGuide crier, string incpath ) : base( 50, 50 )
		{
			m_Crier = crier;
			path = incpath;

			AddPage( 0 );
			AddImageTiled( 0, 0, 410, 144, 0x52 );
			AddAlphaRegion( 1, 1, 408, 142 );

			AddLabel( 160, 12, 2100, "Guide Controls" );

			string nowalk = Convert.ToString( m_Crier.NoWalk );
			AddButton( 10, 34, 4005, 4007, 1, GumpButtonType.Reply, 0 );
			AddLabel( 50, 34, 2100, "Frozen = "+ nowalk );

			string active = Convert.ToString( m_Crier.Active );
			AddButton( 10, 56, 4005, 4007, 2, GumpButtonType.Reply, 0 );
			AddLabel( 50, 56, 2100, "Active = "+ active );

			string random = Convert.ToString( m_Crier.Random );
			AddButton( 200, 34, 4005, 4007, 3, GumpButtonType.Reply, 0 );
			AddLabel( 240, 34, 2100, "Random = "+ random );

			string delay = Convert.ToString( m_Crier.Delay );
			AddButton( 200, 56, 4005, 4007, 4, GumpButtonType.Reply, 0 );
			AddImageTiled( 240, 56, 70, 20, 0xBBC );
			AddImageTiled( 241, 57, 68, 18, 0x2426 );
			AddTextEntry( 241, 57, 68, 18, 0x480, 0, delay );
			AddLabel( 315, 56, 2100, "Delay" );

			AddButton( 10, 78, 4005, 4007, 5, GumpButtonType.Reply, 0 );
			AddLabel( 50, 78, 2100, "Edit News" );

		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;
			switch( info.ButtonID )
			{
				case 1:
				{
					if ( m_Crier.NoWalk )
					{
						m_Crier.NoWalk = false;
						m_Crier.Frozen = false;
					}
					else
					{
						m_Crier.NoWalk = true;
						m_Crier.Frozen = true;
					}

					from.SendGump( new GuideGump( m_Crier, path ) );
					break;
				}
				case 2:
				{
					if ( m_Crier.Active )
						m_Crier.Active = false;
					else
						m_Crier.Active = true;

					from.SendGump( new GuideGump( m_Crier, path ) );
					break;
				}
				case 3:
				{
					if ( m_Crier.Random )
						m_Crier.Random = false;
					else
						m_Crier.Random = true;

					from.SendGump( new GuideGump( m_Crier, path ) );
					break;
				}
				case 4:
				{
					TextRelay text = info.GetTextEntry( 0 );
               
					if ( text != null )
					{
						try
						{
							string[] temp = text.Text.Split(':');
							TimeSpan time = new TimeSpan( Convert.ToInt32( temp[0] ), Convert.ToInt32( temp[1] ), Convert.ToInt32( temp[2] ) );
							m_Crier.Delay = time;

							if ( m_Crier.Active )
							{
								m_Crier.Active = false;
								m_Crier.Active = true;
							}
						}
						catch
						{
							from.SendMessage( 0x35, "Bad format. ##:##:## expected" );
						}
					}

					from.SendGump( new GuideGump( m_Crier, path ) );
					break;
				}
				case 5:
				{
					from.SendGump( new EditGuideGump( m_Crier, path ) );
					break;
				}
			}
		}
	}

	public class EditGuideGump : Gump
	{
		public static string path;
		public BaseGuide m_Crier;

		public EditGuideGump( BaseGuide crier, string incpath ) : base( 10, 10 )
		{
			m_Crier = crier;
			path = incpath;

			AddPage( 0 );
			AddImageTiled( 0, 0, 620, 460, 0x52 );
			AddAlphaRegion( 1, 1, 618, 458 );

			AddLabel( 250, 12, 2100, "Edit Guide News" );
			AddLabel( 10, 34, 2100, "News Entries" );

			ArrayList m_Lines = GetFile();

			int row = 1;
			for ( int i = 0; i < 15; i++ )
			{
				string line = m_Lines[i] as string;

				AddImageTiled( 10, 34 + (row * 22), 598, 20, 0xBBC );
				AddImageTiled( 11, 34 + (row * 22) + 1, 596, 18, 0x2426 );
				AddTextEntry( 11, 34 + (row++ * 22) + 1, 596, 18, 0x480, i, line );
			}

			AddLabel( 500,  408, 2100, "Update File" );
			AddButton( 576, 408, 4005, 4007, 1, GumpButtonType.Reply, 0 );
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			switch( info.ButtonID )
			{
				case 0:
				{
						from.SendGump( new GuideGump( m_Crier, path ) );
						break;
				}
				case 1:
				{
					using ( StreamWriter op = new StreamWriter( path ) )
					{
						for ( int i = 0; i < 15; i++ )
						{
							TextRelay relay = info.GetTextEntry( i );
							string text = Convert.ToString( relay.Text );
							if ( text != null )
							{
								if ( text.Length > 0 )
								op.WriteLine( text );
							}
						}
					}
					from.SendMessage( 0x35, "News file has been updated." );
					from.SendGump( new EditGuideGump( m_Crier, path ) );
					break;
				}
			}
		}

		public ArrayList GetFile()
		{
			ArrayList m_Lines = new ArrayList();

			if ( !File.Exists( path ) )
			{
				for ( int i = 0; i < 15; i++ )
				{
					m_Lines.Add( "" );
				}
				return m_Lines;
			}

			using ( StreamReader ip = new StreamReader( path ) )
			{
				string line;

				while ( (line = ip.ReadLine()) != null )
				{
					if ( line.Length > 0 )
						m_Lines.Add( line );
				}
			}

			if ( m_Lines.Count < 15 )
			{
				int m_Count = 15 - m_Lines.Count;

				for ( int i = 0; i < m_Count; i++ )
				{
					m_Lines.Add( "" );
				}
			}
			return m_Lines;
		}
	}
}
