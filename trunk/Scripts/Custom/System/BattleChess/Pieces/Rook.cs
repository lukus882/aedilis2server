using System;
using System.Collections;

using Server;
using Server.Items;

namespace Arya.Chess
{
	public class Rook : BaseChessPiece
	{
		private bool m_Castle;

		public static int GetGumpID( ChessColor color )
		{
			return color == ChessColor.Black ? 2340 : 2333;
		}

		public override int Power
		{
			get
			{
				return 5;
			}
		}


		public Rook( Chessboard board, ChessColor color, Point2D position ) : base( board, color, position )
		{
		}

		public override void InitializePiece()
		{
			m_Piece = new ChessMobile( this );
			m_Piece.Name	= string.Format( "Rook [{0}]", m_Color.ToString() );

			m_Piece.Female = false;
			m_Piece.BodyValue = 14;

			m_Piece.Hue = Hue;
		}

		public override bool CanMoveTo(Point2D newLocation, ref string err)
		{
			if ( ! base.CanMoveTo (newLocation, ref err) )
				return false;

			// Verify if this is a castle
			BaseChessPiece king = m_Chessboard[ newLocation ];

			if ( king is King && king.Color == m_Color )
			{
				// Trying to castle
				return m_Chessboard.AllowCastle( king, this, ref err );
			}

			int dx = newLocation.X - m_Position.X;
			int dy = newLocation.Y - m_Position.Y;

			// Rooks can only move in one direction
			if ( dx != 0 && dy != 0 )
			{
				err = "Rooks can only move on straight lines";
				return false;
			}

			if ( dx != 0 )
			{
				// Moving on the X axis
				int direction = dx > 0 ? 1 : -1;

				if ( Math.Abs( dx ) > 1 )
				{
					// Verify that the cells in between are empty
					for ( int i = 1; i < Math.Abs( dx ); i++ ) // Start 1 tile after the rook, and stop one tile before destination
					{
						int offset = direction * i;

						if ( m_Chessboard[ m_Position.X + offset, m_Position.Y ] != null )
						{
							err = "Rooks can't move over pieces";
							return false; // There's a piece on the 
						}
					}
				}

				// Verify if there's a piece to each at the end
				BaseChessPiece piece = m_Chessboard[ newLocation ];

				if ( piece == null || piece.Color != m_Color )
					return true;
				else
				{
					err = "You can't capture pieces of your same color";
					return false;
				}
			}
			else
			{
				// Moving on the Y axis
				int direction = dy > 0 ? 1 : -1;

				if ( Math.Abs( dy ) > 1 )
				{
					// Verify that the cells in between are empty
					for ( int i = 1; i < Math.Abs( dy ); i++ )
					{
						int offset = direction * i;

						if ( m_Chessboard[ m_Position.X, m_Position.Y + offset ] != null )
						{
							err = "The rook can't move over other pieces";
							return false; // Piece on the way
						}
					}
				}

				// Verify for piece at end
				BaseChessPiece piece = m_Chessboard[ newLocation ];

				if ( piece == null || piece.Color != m_Color )
					return true;
				else
				{
					err = "You can't capture pieces of your same color";
					return false;
				}
			}
		}

		public override ArrayList GetMoves(bool capture)
		{
			ArrayList moves = new ArrayList();

			int[] xDirection = new int[] { -1, 1, 0, 0 };
			int[] yDirection = new int[] { 0, 0, 1, -1 };

			for ( int i = 0; i < 4; i++ )
			{
				int xDir = xDirection[ i ];
				int yDir = yDirection[ i ];

				int offset = 1;

				while ( true )
				{
					Point2D p = new Point2D( m_Position.X + offset * xDir, m_Position.Y + offset * yDir );

					if ( ! m_Chessboard.IsValid( p ) )
						break;

					BaseChessPiece piece = m_Chessboard[ p ];

					if ( piece == null )
					{
						moves.Add( p );
						offset++;
						continue;
					}

					if ( capture && piece.Color != m_Color )
					{
						moves.Add( p );
						break;
					}

					break;
				}
			}

			return moves;
		}

		public override bool IsCastle(Point2D loc)
		{
			King king = m_Chessboard[ loc ] as King;

			string err = null;

			return king != null && king.Color == m_Color && m_Chessboard.AllowCastle( king, this, ref err );
		}

		public void Castle()
		{
			m_Castle = true;

			int dx = 0;

			if ( m_Position.X == 0 )
				dx = 3;
			else if ( m_Position.X == 7 )
				dx = -2;

			Move move = new Move( this, new Point2D( m_Position.X + dx, m_Position.Y ) );

			MoveTo( move );
		}

		public override void OnMoveOver()
		{
			if ( ! m_Castle )
				base.OnMoveOver ();
			else
			{
				m_Castle = false;

				m_Chessboard.ApplyMove( m_Move );

				King king = m_Chessboard.GetKing( m_Color ) as King;
				
				int dx = 0;

				if ( m_Position.X == 3 )
					dx = -2;
				else
					dx = 2;

				king.EndCastle( new Point2D( king.Position.X + dx, king.Position.Y ) );
			}
		}
	}
}