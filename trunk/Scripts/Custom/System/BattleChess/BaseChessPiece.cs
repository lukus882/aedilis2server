using System;
using System.Collections;

using Server;

namespace Arya.Chess
{
	/// <summary>
	/// Defines the two colors used in the chess game
	/// </summary>
	public enum ChessColor
	{
		Black,
		White
	}

	/// <summary>
	/// This abstract class defines the basic features of a chess piece
	/// </summary>
	public abstract class BaseChessPiece
	{
		#region Variables

		/// <summary>
		/// This represents the NPC that corresponds to this chess piece
		/// </summary>
		protected ChessMobile m_Piece;
		/// <summary>
		/// The Chessboard object parent of this chess piece
		/// </summary>
		protected Chessboard m_Chessboard;
		/// <summary>
		/// The color of this piece
		/// </summary>
		protected ChessColor m_Color;
		/// <summary>
		/// The position of the chess piece on the board
		/// </summary>
		protected Point2D m_Position;
		/// <summary>
		/// Specifies if this piece has been killed
		/// </summary>
		protected bool m_Dead = false;
		/// <summary>
		/// Specifies if the piece has already been moved or not
		/// </summary>
		protected bool m_HasMoved = false;
		/// <summary>
		/// The move this piece is performing
		/// </summary>
		protected Move m_Move;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the NPC corresponding to this chess piece
		/// </summary>
		public ChessMobile Piece
		{
			get { return m_Piece; }
		}

		/// <summary>
		/// Gets the color of this piece
		/// </summary>
		public ChessColor Color
		{
			get { return m_Color; }
		}

		/// <summary>
		/// Gets the color of the enemy
		/// </summary>
		public ChessColor EnemyColor
		{
			get
			{
				if ( m_Color == ChessColor.Black )
					return ChessColor.White;
				else
					return ChessColor.Black;
			}
		}

		/// <summary>
		/// Gets or sets the position of this chess piece.
		/// This does NOT move the chess piece on the chess board
		/// </summary>
		public Point2D Position
		{
			get { return m_Position; }
			set { m_Position = value; }
		}

		/// <summary>
		/// Gets the facing for the NPC when it's standing
		/// </summary>
		public virtual Direction Facing
		{
			get
			{
				if ( m_Color == ChessColor.Black )
					return Direction.South;
				else
					return Direction.North;
			}
		}

		/// <summary>
		/// Gets the hue used to color items for this piece
		/// </summary>
		public virtual int Hue
		{
			get
			{
				if ( m_Color == ChessColor.Black )
					return ChessConfig.BlackHue;
				else
					return ChessConfig.WhiteHue;
			}
		}

		/// <summary>
		/// Gets the opposite hue for this piece
		/// </summary>
		public virtual int SecondaryHue
		{
			get
			{
				if ( m_Color == ChessColor.Black )
					return ChessConfig.WhiteHue;
				else
					return ChessConfig.BlackHue;
			}
		}

		/// <summary>
		/// Gets the power value of this piece
		/// </summary>
		public abstract int Power
		{
			get;
		}

		/// <summary>
		/// States whether this piece has already moved
		/// </summary>
		public virtual bool HasMoved
		{
			get { return m_HasMoved; }
		}

		/// <summary>
		/// Specifies if this piece can be captured by a pawn en passant
		/// </summary>
		public virtual bool AllowEnPassantCapture
		{
			get { return false; }
			set {}
		}

		#endregion

		/// <summary>
		/// Creates a new chess piece object
		/// </summary>
		/// <param name="board">The Chessboard object hosting this piece</param>
		/// <param name="color">The color of this piece</param>
		/// <param name="position">The initial position on the board</param>
		public BaseChessPiece( Chessboard board, ChessColor color, Point2D position )
		{
			m_Chessboard = board;
			m_Color = color;
			m_Position = position;

			CreatePiece();
		}

		#region NPC Creation

		/// <summary>
		/// Creates the NPC that will represent this piece and places it in the correct world location
		/// </summary>
		protected virtual void CreatePiece()
		{
			InitializePiece();
			Point3D loc = new Point3D( m_Chessboard.BoardToWorld( m_Position ), m_Chessboard.Z );

			m_Piece.MoveToWorld( loc, m_Chessboard.Map );
			m_Piece.FixedParticles( 14089, 1, 15, 5012, Hue, 2, EffectLayer.Waist );
		}

		/// <summary>
		/// Creates and initializes the chess piece NPC
		/// </summary>
		/// <returns></returns>
		public abstract void InitializePiece();

		#endregion

		#region Piece Movement

		/// <summary>
		/// Verifies if this piece can move to a specified location.
		/// </summary>
		/// <param name="newLocation">The new location</param>
		/// <param name="err">Will hold the eventual error message</param>
		/// <returns>True if the move is allowed, false otherwise.</returns>
		public virtual bool CanMoveTo( Point2D newLocation, ref string err )
		{
			if ( newLocation == m_Position )
			{
				err = "Can't move to the same spot";
				return false; // Same spot isn't a valid move
			}

			// Base version, check only for out of bounds
			if ( newLocation.X >= 0 && newLocation.Y >= 0 && newLocation.X < 8 && newLocation.Y < 8 )
			{
				return true;
			}
			else
			{
				err = "Can't move out of chessboard";
				return false;
			}
		}

		/// <summary>
		/// Moves the chess piece to the specified position. This function assumes that a previous call
		/// to CanMoveTo() has been made and the move has been authorized.
		/// </summary>
		/// <param name="move">The move performed</param>
		public virtual void MoveTo( Move move )
		{
			m_HasMoved = true;

			m_Move = move;

			Point2D worldLocation = m_Chessboard.BoardToWorld( move.To );

			m_Piece.GoTo( worldLocation );
		}

		/// <summary>
		/// This function is called by the NPC when its move is over
		/// </summary>
		public virtual void OnMoveOver()
		{
			m_Chessboard.OnMoveOver( m_Move );

			if ( m_Move.Capture )
			{
				m_Piece.FixedParticles( 14089, 1, 15, 5012, Hue, 2, EffectLayer.Waist );
				m_Move.CapturedPiece.Die();
			}

			m_Move = null;
		}

		/// <summary>
		/// Gets the list of possible moves this piece can perform
		/// </summary>
		/// <param name="capture">Specifies whether the moves should include squares where a piece would be captured</param>
		/// <returns>An ArrayList objects of Point2D values</returns>
		public abstract ArrayList GetMoves( bool capture );

		/// <summary>
		/// Gets the piece that this piece would capture when moving to a specific location.
		/// This function assumes that the square can be reached.
		/// </summary>
		/// <param name="at">The target location with the potential capture</param>
		/// <param name="enpassant">Will hold a value stating whether this move is made en passant</param>
		/// <returns>A BaseChessPiece if a capture is possible, null otherwise</returns>
		public virtual BaseChessPiece GetCaptured( Point2D at, ref bool enpassant )
		{
			enpassant = false;

			BaseChessPiece piece = m_Chessboard[ at ];

			if ( piece != null && piece.Color != m_Color )
				return piece;
			else
				return null;
		}

		/// <summary>
		/// Verifies if a given move would be a castle
		/// </summary>
		/// <param name="loc">The target location</param>
		/// <returns>True if the move is a castle</returns>
		public virtual bool IsCastle( Point2D loc )
		{
			return false;
		}

		#endregion

		#region Deletion and killing

		/// <summary>
		/// This function is invoked whenever a piece NPC is deleted
		/// </summary>
		public virtual void OnPieceDeleted()
		{
			if ( ! m_Dead )
			{
				m_Chessboard.OnStaffDelete();
			}
		}

		/// <summary>
		/// This function is invoked when the piece is captured and the NPC should be removed from the board
		/// </summary>
		public virtual void Die()
		{
			m_Dead = true;
			m_Piece.Delete();
		}

		/// <summary>
		/// Forces the deletion of this piece
		/// </summary>
		public virtual void ForceDelete()
		{
			if ( m_Piece != null && ! m_Piece.Deleted )
			{
				m_Dead = true;
				m_Piece.Delete();
			}
		}

		#endregion
	}
}