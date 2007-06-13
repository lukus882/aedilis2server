using System;
using System.Collections;

using Server;
using Server.Items;

namespace Arya.Chess
{
	public class Knight : BaseChessPiece
	{
		public static int GetGumpID( ChessColor color )
		{
			return color == ChessColor.Black ? 2342 : 2335;
		}

		public override int Power
		{
			get
			{
				return 3;
			}
		}


		public Knight( Chessboard board, ChessColor color, Point2D position ) : base( board, color, position )
		{
		}

		public override void InitializePiece()
		{
			m_Piece = new ChessMobile( this );
			m_Piece.Name = string.Format( "Knight [{0}]", m_Color.ToString() );

			m_Piece.Female = false;
			m_Piece.BodyValue = 0x190;

			m_Piece.Hue = Utility.RandomSkinHue();
			m_Piece.AddItem( new PonyTail( Utility.RandomHairHue() ) );

			Item item = null;

			item = new PlateLegs();
			item.Hue = Hue;
			m_Piece.AddItem( item );

			item = new PlateChest();
			item.Hue = Hue;
			m_Piece.AddItem( item );

			item = new PlateArms();
			item.Hue = Hue;
			m_Piece.AddItem( item );

			item = new PlateGorget();
			item.Hue = Hue;
			m_Piece.AddItem( item );

			item = new PlateGloves();
			item.Hue = Hue;
			m_Piece.AddItem( item );

			item = new Doublet( SecondaryHue );
			m_Piece.AddItem( item );

			item = new Lance();
			item.Hue = Hue;
			m_Piece.AddItem( item );

			Server.Mobiles.Horse horse = new Server.Mobiles.Horse();
			horse.BodyValue = 200;
			horse.Hue = Hue;

			horse.Rider = m_Piece;

			m_Piece.Direction = Facing;
		}

		public override bool CanMoveTo(Point2D newLocation, ref string err)
		{
			if ( ! base.CanMoveTo (newLocation, ref err) )
				return false;

			// Care only about absolutes for knights
			int dx = Math.Abs( newLocation.X - m_Position.X );
			int dy = Math.Abs( newLocation.Y - m_Position.Y );

			if ( ! ( ( dx == 1 && dy == 2 ) || ( dx == 2 && dy == 1 ) ) )
			{
				err = "Knights can only make L shaped moves (2-3 tiles length)";
				return false; // Wrong move
			}
            
			// Verify target piece
			BaseChessPiece piece = m_Chessboard[ newLocation ];

			if ( piece == null || piece.Color != m_Color )
				return true;
			else
			{
				err = "You can't capture pieces of your same color";
				return false;
			}
		}

		public override ArrayList GetMoves(bool capture)
		{
			ArrayList moves = new ArrayList();

			for ( int dx = -2; dx <= 2; dx++ )
			{
				for ( int dy = -2; dy <= 2; dy++ )
				{
					if ( ! ( ( Math.Abs( dx ) == 1 && Math.Abs( dy ) == 2 ) || ( Math.Abs( dx ) == 2 && Math.Abs( dy ) == 1 ) ) )
						continue;

					Point2D p = new Point2D( m_Position.X + dx, m_Position.Y + dy );

					if ( ! m_Chessboard.IsValid( p ) )
						continue;

					BaseChessPiece piece = m_Chessboard[ p ];

					if ( piece == null )
						moves.Add( p );
					else if ( capture && piece.Color != m_Color )
						moves.Add( p );
				}
			}

			return moves;
		}

	}
}