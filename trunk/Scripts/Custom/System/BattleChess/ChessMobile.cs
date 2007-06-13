using System;

using Server;
using Server.Items;
using Server.Mobiles;

namespace Arya.Chess
{
	/// <summary>
	/// The basic mobile that will be used as the actual chess piece
	/// </summary>
	public class ChessMobile : BaseCreature
	{
		/// <summary>
		/// The chess piece that owns this NPC
		/// </summary>
		private BaseChessPiece m_Piece;
		/// <summary>
		/// Specifies the location of the next position of this piece
		/// </summary>
		private Point3D m_NextMove = Point3D.Zero;

		public ChessMobile( BaseChessPiece piece ) : base( AIType.AI_Use_Default, FightMode.None, 1, 1, 1.0, 1.0 )
		{
			InitStats( 25, 100, 100 );
			m_Piece = piece;

			Blessed = true;
			Paralyzed = true;
			Direction = m_Piece.Facing;
		}

		#region Serialization

		public ChessMobile( Serial serial ) : base( serial )
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize( writer );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			Delete();
		}

		#endregion

		#region Movement on the chessboard

		/// <summary>
		/// Places the piece on the board for the first time
		/// </summary>
		/// <param name="location">The location where the piece should be placed</param>
		/// <param name="map">The map where the game takes place</param>
		public void Place( Point3D location, Map map )
		{
			MoveToWorld( location, map );
			FixedParticles( 0x373A, 1, 15, 5012, Hue, 2, EffectLayer.Waist );
		}

		/// <summary>
		/// Moves the NPC to the specified location
		/// </summary>
		/// <param name="to">The location the NPC should move to</param>
		public void GoTo( Point2D to )
		{
			AI = AIType.AI_Melee;

			m_NextMove = new Point3D( to, Z );

			WayPoint wp = new WayPoint();
			wp.MoveToWorld( m_NextMove, Map );
			CurrentWayPoint = wp;

			Paralyzed = false;
		}

		protected override void OnLocationChange(Point3D oldLocation)
		{
			if ( m_NextMove == Point3D.Zero || m_NextMove != Location )
				return;

			// The NPC is at the waypoint
			AI = AIType.AI_Use_Default;

			CurrentWayPoint.Delete();
			CurrentWayPoint = null;
			Paralyzed = true;

			m_NextMove = Point3D.Zero;

			Direction = m_Piece.Facing;

			m_Piece.OnMoveOver();
		}

		#endregion

		public override bool HandlesOnSpeech(Mobile from)
		{
			return false;
		}

		public override void OnDelete()
		{
			if ( m_Piece != null )
				m_Piece.OnPieceDeleted();

			base.OnDelete ();
		}

		public override bool OnMoveOver(Mobile m)
		{
			return true;
		}
	}
}