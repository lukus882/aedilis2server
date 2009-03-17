using System;
using Server;
using Server.Items;
using Server.Network;
using Solaris.BoardGames;

namespace Server.Gumps
{
	public class SelectBombermanStyleGump : SelectStyleGump
	{
		public override int Height{ get{ return 150 + Enum.GetNames( typeof( BombermanStyle ) ).Length * 20; } }
		
		public SelectBombermanStyleGump( Mobile owner, BoardGameControlItem controlitem ) : base( owner, controlitem )
		{
			AddLabel( 20, 14, 1152, "Bomberman Board Style" );

			AddButton( Width - 15, 0, 3, 4, 0, GumpButtonType.Reply, 0 );	
			
			string[] stylenames = Enum.GetNames( typeof( BombermanStyle ) );
			
			foreach( string stylename in stylenames )
			{
				int index = (int)Enum.Parse( typeof( BombermanStyle ), stylename );
				int buttonid = ( (int)((BombermanControlItem)_ControlItem).Style == index ? 0x2C92 : 0x2C88 );
				
				AddButton( _X, _Y += 20, buttonid, buttonid, index + 1, GumpButtonType.Reply, 0 );
				AddLabel( _X + 20, _Y - 2, 1152, stylename );
			}
			
			AddLabel( _X, _Y += 30, 1152, "Width:" );
			AddTextField( _X + 50, _Y, 30, 20, 0, _ControlItem.BoardWidth.ToString() );
			AddLabel( _X + 90, _Y, 1152, "Height:" );
			AddTextField( _X + 140, _Y, 30, 20, 1, _ControlItem.BoardHeight.ToString() );
			
			AddButton( _X + 20, _Y += 40, 0xEE, 0xEF, 100, GumpButtonType.Reply, 0 );
			AddButton( _X + 100, _Y, 0xF7, 0xF8, 1000, GumpButtonType.Reply, 0 );
		}
		
		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;
			if( _ControlItem.Players.IndexOf( from ) != 0 )
			{
				return;
			}
			
			try
			{
				_ControlItem.BoardWidth = Int32.Parse( GetTextField( info, 0 ) );
				_ControlItem.BoardHeight = Int32.Parse( GetTextField( info, 1 ) );
				
			}
			catch
			{
			}
			
			int selection = info.ButtonID - 1;
			if( selection >= 0 )
			{
				try
				{
					if( info.ButtonID < 100 )
					{
						((BombermanControlItem)_ControlItem).Style = (BombermanStyle)selection;
					}
				}
				catch
				{
					from.SendMessage( "Invalid value" );
				}
				if( info.ButtonID != 1000 )
				{
					from.SendGump( new SelectBombermanStyleGump( from, _ControlItem ) );
				}
			}
			
			base.OnResponse( state, info );
		}
	}
}
