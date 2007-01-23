using System;
using Server;
using Server.Targeting;
using Server.Mobiles;
using Server.Commands;

namespace Server.Scripts.Commands
{
	public class TameIt
	{
		public static void Initialize()
		{
			Register();
		}

		public static void Register()
		{
			CommandSystem.Register( "TameIt", AccessLevel.GameMaster, new CommandEventHandler( TameIt_OnCommand ) );
		}

		[Usage( "TameIt" )]
		[Description( "Instantly tames any tamable creature" )]
		private static void TameIt_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			
			from.SendMessage("Select the creature to tame");
			from.Target = new TameItTarget( from );
		}
		
		private class TameItTarget : Target
		{
			public TameItTarget( Mobile from ) : base( -1, true, TargetFlags.None )
			{
			}
			
			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
				{
					BaseCreature targ = (BaseCreature)o;
					
					if ( !targ.Tamable )
					{
						from.SendMessage("This will only work on tamable creatures.");
						return;
					}
					else
					{
						if ( !targ.Controlled )
						{
							targ.Controlled = true;
							targ.ControlMaster = from;
						}
						else
						{
							from.SendMessage("That is already tamed.");
						}
					}
				}
			}
		}
	}
}
