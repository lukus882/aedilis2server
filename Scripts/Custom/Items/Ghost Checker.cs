//; Script Name: GhostChecker Teleporter
//; Author(s): Tecmo (GhostChecker)

using System;
using Server;
using Server.Network;

namespace Server.Items
{		
	#region Tecmo Ghost Rejector
//;===========START OF SCRIPT============
//; Script Name: GhostRejector
//; Author(s): Tecmo
//; Version: 1.01
//; Revision Date: 30Apr2006 (1.01)
//; Public Release: 03Nov2006
//; Copyright (GhostRejector) © 2006 Exiled Sosaria 
//; Purpose: Prevent Ghosts from continuing beyond the teleporter
	public class GhostRejector : Item
	{					
		private bool m_Active;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public bool Active
		{
			get { return m_Active; }
			set { m_Active = value; InvalidateProperties(); }
		}

		public override int LabelNumber{ get{ return 1026095; } } // teleporter

		
		[Constructable]
		public GhostRejector() : base( 0x1BC3 )
		{
			Movable = false;
			Visible = false;
			Name = "Ghost Rejector";
			m_Active = true;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( m_Active )
				list.Add( 1060742 ); // active
			else
				list.Add( 1060743 ); // inactive
		}

		public override bool OnMoveOver( Mobile m )
		{
			if ( m_Active )
			{
				if ( !m.Alive && m.Map != null && m.Map.CanFit( m.Location, 16, false, false ))
				{
					//m.SendMessage( "Ghost are not allowed to continue beyond this point" );
					return false;
				}
			}
			return true;
		}		
		
		public GhostRejector( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
			writer.Write( m_Active );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_Active = reader.ReadBool();
		}
	}
	#endregion				

	#region Tecmo GhostOnlyPermitter
//;===========START OF SCRIPT============
//; Script Name: GhostOnlyPermitter
//; Author(s): Tecmo (GhostOnlyPermitter)
//; Version: 1.01
//; Revision Date: 06May06 (1.01)
//; Public Release: 03Nov06
//; Copyright (GhostOnlyPermitter) © 2006 Exiled Sosaria 
//; Purpose: Prevents all mobiles except ghosts from continuing beyond the teleporter
	public class GhostOnlyPermitter : Item
	{					
		private bool m_Active;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public bool Active
		{
			get { return m_Active; }
			set { m_Active = value; InvalidateProperties(); }
		}

		public override int LabelNumber{ get{ return 1026095; } } // teleporter

		
		[Constructable]
		public GhostOnlyPermitter() : base( 0x1BC3 )
		{
			Movable = false;
			Visible = false;
			Name = "Ghost only Permitter";
			m_Active = true;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( m_Active )
				list.Add( 1060742 ); // active
			else
				list.Add( 1060743 ); // inactive
		}

		public override bool OnMoveOver( Mobile m )
		{
			if ( m_Active )
			{
				if ( !m.Alive && m.Map != null && m.Map.CanFit( m.Location, 16, false, false ))
				{
					//m.SendMessage( "Only Ghosts are allowed to continue beyond this point" );
					return true;
				}
			}
			return false;
		}		
		
		public GhostOnlyPermitter( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
			writer.Write( m_Active );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_Active = reader.ReadBool();
		}
	}
	#endregion					
	
}
