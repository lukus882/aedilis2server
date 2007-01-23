using System;
using Server.Items; 
using Server.Gumps;
using Server.Network;

namespace Server.Items
{
	[FlipableAttribute( 0xFEF, 0xFF0, 0xFF1, 0xFF2, 0xFF3, 0xFF4, 0xFBD, 0xFBE )]
	public class NetBook : Item
	{
		// private string i_url = "www.aedilis.us"; // set default url here or 
		private string i_url; // use this instead for default of null.

		[CommandProperty( AccessLevel.GameMaster )]
		public string URL
		{
			get { return i_url; }
			set { i_url = value; }
		}

		[Constructable]
		public NetBook() : base( 0xFEF + Utility.Random( 4 ) )
		{
			Name = "a worn book";
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( i_url != null ) 
			{                
				if ( IsChildOf( from.Backpack ) || from.InRange( this, 1 )) 
					from.SendGump( new NetBookGump( from, i_url ) );
			}
		}

		public override void OnSingleClick( Mobile from ) 
		{ 
			this.LabelTo( from, this.Name ); 
		}
 
		public NetBook( Serial serial ) : base( serial )
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize( writer );
			writer.Write( (int) 1 );
			writer.Write( i_url );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			switch ( version )
			{
				case 1:
				{
					i_url = reader.ReadString();
					break;
				}
			}
		}
	}

	public class NetBookGump : Gump
	{
		private string m_URL;

		public NetBookGump( Mobile owner, string URL ) : base( 25, 25 )
		{
			owner.CloseGump( typeof( NetBookGump ) );

			m_URL = URL;

			Closable = true;
			Disposable = true;
			Dragable = true;
			Resizable = false;

			AddPage( 0 );

			AddImage( 0, 0, 0x898 );
			AddButton( 64, 160, 0xF7, 0xF8, 1, GumpButtonType.Reply, 0 ); // OK
			AddButton( 228, 160, 0xF2, 0xF1, 2, GumpButtonType.Reply, 0 ); // Cancel

			string msg = "This book is presented in a Web format. Do you wish to view it now?";
			AddHtml( 36, 20, 112, 112, msg, false, false );
			AddHtml( 200, 20, 112, 112, m_URL, false, false );
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			if ( info.ButtonID == 1 )
				state.Mobile.LaunchBrowser( m_URL );
		}
	}
}