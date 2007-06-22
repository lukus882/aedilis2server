using System;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using System.Collections;

namespace Server.Gumps
{
	public class MysteryStoneGump : Gump
	{
		public MysteryStoneGump()
			: base( 0, 0 )
		{
			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(1);
			this.AddImage(179, 107, 101);
			this.AddImage(265, 229, 23);
			this.AddButton(251, 254, 5, 248, 0, GumpButtonType.Page, 2);
			this.AddImage(265, 282, 24);

			this.AddPage(2);
			this.AddImage(179, 107, 101);
			this.AddImage(265, 229, 23);
			this.AddImage(253, 250, 1417);
			this.AddButton(280, 277, 11320, 11320, 1, GumpButtonType.Reply, 0);

		}
		
		public override void OnResponse( NetState state, RelayInfo info ) 
      		{ 
			Mobile from = state.Mobile; 

			if ( from == null )
				return;

        		if ( info.ButtonID == 1 )
         		{
				Item[] GlassShards = from.Backpack.FindItemsByType( typeof( GlassShards ) );
				if ( from.Backpack.ConsumeTotal( typeof( GlassShards ), 1 ) )
				{
					from.SendMessage( "You've been moved to a secret location!" );
					from.MoveToWorld( new Point3D(1321, 534, -2), Map.Trammel);
					Effects.PlaySound( from.Location, from.Map, 0x5C9 );
					from.FixedEffect( 0x376A, 10, 16 );
				}
				else
				{
					from.SendMessage( "You don't have what is required to form this!" );
				}

			}
		}
	}
}