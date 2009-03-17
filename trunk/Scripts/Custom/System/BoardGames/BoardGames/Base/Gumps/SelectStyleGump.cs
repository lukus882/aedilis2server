using System;
using Server;
using Server.Items;
using Server.Network;
using Solaris.BoardGames;

namespace Server.Gumps
{
	public class SelectStyleGump : Gump
	{
		public virtual int Height{ get{ return 150; } }
		public virtual int Width{ get{ return 200; } }
		
		protected int _Y = 30;
		protected int _X = 20;
		
		protected BoardGameControlItem _ControlItem;
		
		public SelectStyleGump( Mobile owner, BoardGameControlItem controlitem ) : base( 450, 80 )
		{
			Closable = false;
			
			owner.CloseGump( typeof( SelectStyleGump ) );
			
			_ControlItem = controlitem;
			
			if( _ControlItem.Players.IndexOf( owner ) != 0 )
			{
				return;
			}
			
			AddPage( 0 );
			AddBackground( 0, 0, Width, Height, 0x1400 );

			AddButton( Width - 15, 0, 3, 4, 0, GumpButtonType.Reply, 0 );	
		}
		
		public void AddTextField( int x, int y, int width, int height, int index, string text )
		{
			AddImageTiled( x - 2, y - 2, width + 4, height + 4, 0xA2C );
			AddAlphaRegion( x -2, y - 2, width + 4, height + 4 );
			AddTextEntry( x + 2, y + 2, width - 4, height - 4, 1153, index, text );
		}
		
		public string GetTextField( RelayInfo info, int index )
		{
			TextRelay relay = info.GetTextEntry( index );
			return ( relay == null ? null : relay.Text.Trim() );
		}
		
		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;
			if( _ControlItem.Players.IndexOf( from ) != 0 )
			{
				return;
			}
			
			if( info.ButtonID == 1000 )
			{
				_ControlItem.SettingsReady = true;
				return;
			}
		}
	}
}
