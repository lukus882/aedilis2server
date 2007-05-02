using System;
using Server;
using Server.Mobiles;
using System.Collections;
using Server.Misc;
using Server.Gumps;
using Server.Targeting;
using Server.Commands;

namespace Fatima.CharacterFlags
{
	public class FlagsView
	{
		private class FlagsViewTarget : Target
		{
			public string Color( string text, int color )
			{
				return String.Format( "<BASEFONT COLOR=#{0:X6}>{1}</BASEFONT>", color, text );
			}
	
			public string Center( string text )
			{
				return String.Format( "<CENTER>{0}</CENTER>", text );
			}
	
			public FlagsViewTarget() : base( 10, false, TargetFlags.None )
			{
			}
	
			protected override void OnTarget( Mobile from, object targeted)
			{
				if (targeted != null && targeted is PlayerMobile)
				{
					PlayerMobile m = targeted as PlayerMobile;
					from.SendGump(new ViewCharacterFlagsGump(m));
				}
			}
		}

		public static void Initialize()
		{
			CommandSystem.Register( "Flags", AccessLevel.Player, new CommandEventHandler( Flags_OnCommand ) );
			CommandSystem.Register( "ListFlags", AccessLevel.GameMaster, new CommandEventHandler( ListFlags_OnCommand ) );
		}

		public static void Flags_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;

			from.SendGump(new ViewCharacterFlagsGump((PlayerMobile)from)); //Start at Page 0 - Front page.
		}

		public static void ListFlags_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;

			from.Target = new FlagsViewTarget(); //Start at Page 0 - Front page.
		}
	}
}