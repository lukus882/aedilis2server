using System; 
using Server; 
using Server.Mobiles; 
using Server.Regions;
using Server.Prompts;

namespace Server.Commands
{
	public class RedSafeRes
	{
		public static void Initialize()
		{
			CommandSystem.Register( "RedSafeRes", AccessLevel.Player, new CommandEventHandler( RedSafeRes_OnCommand ) );
		}

		private static void RedSafeRes_OnCommand( CommandEventArgs e )
		{
			Mobile m = e.Mobile;
			if( m.Player && m is PlayerMobile )
			{
                                if (m.Alive)
                                {
                                	m.SendMessage("You are not dead");
                                }
                                else
                                {
					if (m.Region is Jail)
					{
						m.SendMessage("You cannot seem to escape the forces of Jail... you.. CRIMINAL!");
					}
					else
					{
						m.SendMessage("You have selected safe res");
		                                m.Map = Map.Felucca;
		                                m.Location = new Point3D(2707,2146,0);
					}
                                }
			}
		}

		
	}
}