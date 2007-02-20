using System;
using System.IO;
using System.Text;
using System.Collections;

using Server;
using Server.Network;
using Server.Guilds;

namespace Server.Misc
{
	public class SmallStatusPage : Timer
	{
		public static void Initialize()
		{
			new SmallStatusPage().Start();
		}

		public SmallStatusPage() : base( TimeSpan.FromSeconds( 5.0 ), TimeSpan.FromSeconds( 60.0 ) )
		{
			Priority = TimerPriority.FiveSeconds;
		}

		private static string Encode( string input )
		{
			StringBuilder sb = new StringBuilder( input );

			sb.Replace( "&", "&amp;" );
			sb.Replace( "<", "&lt;" );
			sb.Replace( ">", "&gt;" );
			sb.Replace( "\"", "&quot;" );
			sb.Replace( "'", "&apos;" );

			return sb.ToString();
		}

		protected override void OnTick()
		{
			if ( !Directory.Exists( "web" ) )
				Directory.CreateDirectory( "web" );

			using ( StreamWriter op = new StreamWriter( "web/status2.html" ) )
			{
				op.WriteLine( "<html>" );
				op.WriteLine( "   <head>" );
				op.WriteLine( "      <title>Server Status</title>");
				op.WriteLine( "   </head>" );
				op.WriteLine( "   <body>" );
				op.WriteLine( "      Online clients:<br><br>" );
	


				foreach ( NetState state in NetState.Instances )
				{
					Mobile m = state.Mobile;

					if ( m != null )
					{
						
						op.Write( Encode( m.Name ) );
						

						op.WriteLine( "<br>" );

					}
				}


				op.WriteLine( "   </body>" );
				op.WriteLine( "</html>" );
			}
		}
	}
}