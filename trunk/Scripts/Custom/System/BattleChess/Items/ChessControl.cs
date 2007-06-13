using System;

using Server;

namespace Arya.Chess
{
	/// <summary>
	/// This is the control stone that allows player to play chess
	/// </summary>
	public class ChessControl : Item
	{
		private ChessGame m_Game;
		private Rectangle2D m_Bounds;
		private int m_SquareWidth = 2;
		private int m_BoardHeight = 0;

		[ CommandProperty( AccessLevel.GameMaster ) ]
		public Rectangle2D Bounds
		{
			get { return m_Bounds; }
		}

		[ CommandProperty( AccessLevel.GameMaster ) ]
		public Point2D BoardNorthWestCorner
		{
			get { return m_Bounds.Start; }
			set
			{
				m_Bounds.Start = value;
				m_Bounds.Width = 8 * m_SquareWidth;
				m_Bounds.Height = 8 * m_SquareWidth;

				InvalidateProperties();
			}
		}

		[ CommandProperty( AccessLevel.GameMaster ) ]
		public int SquareWidth
		{
			get { return m_SquareWidth; }
			set
			{
				if ( value < 1 )
					return;

				m_SquareWidth = value;

				if ( m_Bounds.Start != Point2D.Zero )
				{
					m_Bounds.Width = 8 * m_SquareWidth;
					m_Bounds.Height = 8 * m_SquareWidth;

					InvalidateProperties();
				}
			}
		}

		[ CommandProperty( AccessLevel.GameMaster ) ]
		public int BoardHeight
		{
			get { return m_BoardHeight; }
			set { m_BoardHeight = value; }
		}

		[ Constructable ]
		public ChessControl() : base( 3796 )
		{
			Movable = false;
			Hue = 1285;
			Name = "Battle Chess";
			m_Bounds = new Rectangle2D( 0, 0, 0, 0 );
		}

		public ChessControl( Serial serial ) : base( serial )
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize (writer);

			writer.Write( 0 ); // Version;

			writer.Write( m_Bounds );
			writer.Write( m_SquareWidth );
			writer.Write( m_BoardHeight );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize (reader);

			int version = reader.ReadInt();

			m_Bounds = reader.ReadRect2D();
			m_SquareWidth = reader.ReadInt();
			m_BoardHeight = reader.ReadInt();
		}

		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties (list);

			string status = "Not Configured";

			if ( m_Bounds.Width > 0 )
			{
				if ( m_Game == null )
					status = "Ready";
				else
					status = "Game in progress";
			}

			list.Add( 1060658, "Game Status\t{0}", status );
		}

		public override void OnDoubleClick(Mobile from)
		{
			if ( m_Bounds.Width == 0 )
			{
				// Not configured yet
				if ( from.AccessLevel >= AccessLevel.GameMaster )
				{
					from.Target = new ChessTarget( from, "Target the north west corner of the chessboard you wish to create",
						new ChessTargetCallback( OnBoardTarget ) );
				}
				else
				{
					from.SendMessage( 0x40, "This chess board isn't ready for a game yet. Please contact a game master for assistance with its configuration." );
				}
			}
			else if ( m_Game != null )
				from.SendMessage( 0x40, "A chess game is currently in progress. Please try again later." );
			else
			{
				m_Game = new ChessGame( this, from, m_Bounds, m_BoardHeight );
				InvalidateProperties();
			}
		}

		public void OnGameOver()
		{
			m_Game = null;
			InvalidateProperties();
		}

		private void OnBoardTarget( Mobile from, object targeted )
		{
			IPoint3D p = targeted as IPoint3D;

			if ( p == null )
			{
				from.SendMessage( 0x40, "Invalid location" );
				return;
			}

			BuildBoard( this, new Point3D( p ), Map );
			InvalidateProperties();
		}

		public override void OnDelete()
		{
			if ( m_Game != null )
				m_Game.Cleanup();

			base.OnDelete();
		}


		private static void BuildBoard( ChessControl chess, Point3D p, Map map )
		{
			chess.m_BoardHeight = p.Z + 5; // Placing stairs on the specified point
			chess.BoardNorthWestCorner = new Point2D( p.X, p.Y );

			#region Board Tiles

			int stairNW = 1909;
			int stairSE = 1910;
			int stairSW = 1911;
			int stairNE = 1912;
			int stairS = 1901;
			int stairE = 1902;
			int stairN = 1903;
			int stairW = 1904;
			int black = 1295;
			int white = 1298;

			#endregion

			for ( int x = 0; x < 8; x++ )
			{
				for ( int y = 0; y < 8; y++ )
				{
					int tile = 0;

					if ( x % 2 == 0 )
					{
						if ( y % 2 == 0 )
							tile = black;
						else
							tile = white;
					}
					else
					{
						if ( y % 2 == 0 )
							tile = white;
						else
							tile = black;
					}

					for ( int kx = 0; kx < chess.m_SquareWidth; kx++ )
					{
						for ( int ky = 0; ky < chess.m_SquareWidth; ky++ )
						{
							Server.Items.Static s = new Server.Items.Static( tile );
							Point3D target = new Point3D( p.X + x * chess.m_SquareWidth + kx, p.Y + y * chess.m_SquareWidth + ky, chess.m_BoardHeight );
							s.MoveToWorld( target, map );
						}
					}
				}
			}

			Point3D nw = new Point3D( p.X - 1, p.Y - 1, p.Z );
			Point3D ne = new Point3D( p.X + 8 * chess.m_SquareWidth, p.Y - 1, p.Z );
			Point3D se = new Point3D( p.X + 8 * chess.m_SquareWidth, p.Y + 8 * chess.m_SquareWidth, p.Z );
			Point3D sw = new Point3D( p.X - 1, p.Y + 8 * chess.m_SquareWidth, p.Z );

			new Server.Items.Static( stairNW ).MoveToWorld( nw, map );
			new Server.Items.Static( stairNE ).MoveToWorld( ne, map );
			new Server.Items.Static( stairSE ).MoveToWorld( se, map );
			new Server.Items.Static( stairSW ).MoveToWorld( sw, map );

			for ( int x = 0; x < 8 * chess.m_SquareWidth; x++ )
			{
				Point3D top = new Point3D( p.X + x, p.Y - 1, p.Z );
				Point3D bottom = new Point3D( p.X + x, p.Y + 8 * chess.m_SquareWidth, p.Z );
				Point3D left = new Point3D( p.X - 1, p.Y + x, p.Z );
				Point3D right = new Point3D( p.X + chess.m_SquareWidth * 8, p.Y + x, p.Z );

				new Server.Items.Static( stairN ).MoveToWorld( top, map );
				new Server.Items.Static( stairS ).MoveToWorld( bottom, map );
				new Server.Items.Static( stairW ).MoveToWorld( left, map );
				new Server.Items.Static( stairE ).MoveToWorld( right, map );
			}
		}
	}
}
