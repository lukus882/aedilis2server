using System;
using System.Collections;

using Server;
using Server.Items;

namespace Arya.Chess
{
	public class King : BaseChessPiece
	{
		public override int Power
		{
			get
			{
				return 100; // Useless
			}
		}

		public King( Chessboard board, ChessColor color, Point2D position ) : base( board, color, position )
		{
		}

		public override void InitializePiece()
		{
			m_Piece = new ChessMobile( this );
			m_Piece.Name = string.Format( "King [{0}]", m_Color.ToString() );

			m_Piece.Female = false;
			m_Piece.BodyValue = 0x190;

			m_Piece.Hue = Utility.RandomSkinHue();
			m_Piece.AddItem( new ShortHair( Utility.RandomHairHue() ) );

			Item item = null;

			item = new Boots( Hue );
			m_Piece.AddItem( item );

			item = new LongPants( Hue );
			m_Piece.AddItem( item );

			item = new FancyShirt( Hue );
			m_Piece.AddItem( item );

			item = new Doublet( SecondaryHue );
			m_Piece.AddItem( item );

			item = new Cloak( Hue );
			m_Piece.AddItem( item );

			item = new Scepter();
			item.Hue = SecondaryHue;
			m_Piece.AddItem( item );
		}

		public override bool CanMoveTo(Point2D newLocation, ref string err)
		{
			if ( ! base.CanMoveTo (newLocation, ref err) )
				return false;

			// Verify if this is a castle
			BaseChessPiece rook = m_Chessboard[ newLocation ];

			if ( rook is Rook && rook.Color == m_Color )
			{
				// Trying to castle
				return m_Chessboard.AllowCastle( this, rook, ref err );
			}

			int dx = newLocation.X - m_Position.X;
			int dy = newLocation.Y - m_Position.Y;

			if ( Math.Abs( dx ) > 1 || Math.Abs( dy ) > 1 )
			{
				err = "The can king can move only 1 tile at a time";
				return false; // King can move only 1 tile away from its position
			}

			// Verify target piece
			BaseChessPiece piece = m_Chessboard[ newLocation ];

			if ( piece == null || piece.Color != m_Color )
			{
				return true;
			}
			else
			{
				err = "You can't capture pieces of your same color";
				return false;
			}
		}

		public override ArrayList GetMoves(bool capture)
		{
			ArrayList moves = new ArrayList();

			for ( int dx = -1; dx <= 1; dx++ )
			{
				for ( int dy = -1; dy <= 1; dy++ )
				{
					if ( dx == 0 && dy == 0 )
						continue; // Can't move to same spot

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

		public override bool IsCastle(Point2D loc)
		{
			Rook rook = m_Chessboard[ loc ] as Rook;

			string err = null;

			return rook != null && rook.Color == m_Color && m_Chessboard.AllowCastle( this, rook, ref err );
		}

		public void EndCastle( Point2D location )
		{
			m_HasMoved = true;

			m_Move = new Move( this, location );

			Point2D worldLocation = m_Chessboard.BoardToWorld( location );

			m_Piece.GoTo( worldLocation );
		}
	}
}