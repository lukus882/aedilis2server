using System;
using System.Collections;

using Server;
using Server.Items;

namespace Arya.Chess
{
	public class Bishop : BaseChessPiece
	{
		public static int GetGumpID( ChessColor color )
		{
			return color == ChessColor.Black ? 2339 : 2332;
		}

		public override int Power
		{
			get
			{
				return 3;
			}
		}


		public Bishop( Chessboard board, ChessColor color, Point2D position ) : base( board, color, position )
		{
		}

		public override void InitializePiece()
		{
			m_Piece = new ChessMobile( this );
			m_Piece.Name = string.Format( "Bishop [{0}]", m_Color.ToString() );

			m_Piece.Female = false;
			m_Piece.BodyValue = 0x190;

			m_Piece.Hue = Utility.RandomSkinHue();

			Item item = null;
			
			item = new HoodedShroudOfShadows( Hue );
			item.Name = "Bishop's Robe";
			m_Piece.AddItem( item );

			item = new Boots( Hue );
			m_Piece.AddItem( item );

			item = new QuarterStaff();
			item.Hue = SecondaryHue;
			m_Piece.AddItem( item );
		}

		public override bool CanMoveTo(Point2D newLocation, ref string err)
		{
			if ( ! base.CanMoveTo (newLocation, ref err) )
				return false;

			int dx = newLocation.X - m_Position.X;
			int dy = newLocation.Y - m_Position.Y;

			if ( Math.Abs( dx ) != Math.Abs( dy ) )
			{
				err = "Bishops can move only on diagonals";
				return false; // Not a diagonal movement
			}

			int xDirection = dx > 0 ? 1 : -1;
			int yDirection = dy > 0 ? 1 : -1;

			if ( Math.Abs( dx ) > 1 )
			{
				// Verify that the path to target is empty
				for ( int i = 1; i < Math.Abs( dx ); i++ ) // Skip the bishop square and stop before target
				{
					int xOffset = xDirection * i;
					int yOffset = yDirection * i;

					if ( m_Chessboard[ m_Position.X + xOffset, m_Position.Y + yOffset ] != null )
					{
						err = "Bishops can't move over other pieces";
						return false;
					}
				}
			}

			// Verify target piece
			BaseChessPiece piece = m_Chessboard[ newLocation ];

			if ( piece == null || piece.Color != m_Color )
			{
				return true;
			}
			else
			{
				err = "You can't capture pieces of your own color";
				return false;
			}
		}

		public override ArrayList GetMoves(bool capture)
		{
			ArrayList moves = new ArrayList();

			int[] xDirection = new int[] { -1, 1, -1, 1 };
			int[] yDirection = new int[] { -1, 1, 1, -1 };

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
	}
}
