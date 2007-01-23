using System; 
using Server; 
using Server.Mobiles; 
using Server.Regions;
using Server.Prompts;

namespace Server.Commands
{
	public class BlueSafeRes
	{
		public static void Initialize()
		{
			CommandSystem.Register( "BlueSafeRes", AccessLevel.Player, new CommandEventHandler( BlueSafeRes_OnCommand ) );
		}

		private static void BlueSafeRes_OnCommand( CommandEventArgs e )
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
		                                m.Location = new Point3D(1483,1610,20);
					}
                                }
			}
		}

		
	}
}