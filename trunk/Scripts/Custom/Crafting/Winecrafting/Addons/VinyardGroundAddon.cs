//================================================//
// Created by dracana				  //
// Desc: For players to place dirt tiles in their //
//       houses.  Especially useful for players   //
//       with non-custom housing.                 //
//================================================//
using System;
using System.Collections;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Multis;
using Server.Network;

namespace Server.Items
{
	public class VinyardGroundAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new VinyardGroundAddonDeed(); } }

		#region Constructors
		[Constructable]
		public VinyardGroundAddon( VinyardGroundType type, int width, int height ) : this( (int)type, width, height )
		{
		}

		public VinyardGroundAddon( int type, int width, int height )
		{
			VinyardGroundInfo info = VinyardGroundInfo.GetInfo( type );
			
			AddComponent( new AddonComponent( info.GetItemPart( VinyardGroundPosition.Top ).ItemID ), 0, 0, 0 );
			AddComponent( new AddonComponent( info.GetItemPart( VinyardGroundPosition.Right ).ItemID ), width, 0, 0 );
			AddComponent( new AddonComponent( info.GetItemPart( VinyardGroundPosition.Left ).ItemID ), 0, height, 0 );
			AddComponent( new AddonComponent( info.GetItemPart( VinyardGroundPosition.Bottom ).ItemID ), width, height, 0 );
			
			int w = width - 1;
			int h = height - 1;
			
			for ( int y = 1; y <= h; ++y )
				AddComponent( new AddonComponent( info.GetItemPart( VinyardGroundPosition.West ).ItemID ), 0, y, 0 );
			
			for ( int x = 1; x <= w; ++x )
				AddComponent( new AddonComponent( info.GetItemPart( VinyardGroundPosition.North ).ItemID ), x, 0, 0 );
			
			for ( int y = 1; y <= h; ++y )
				AddComponent( new AddonComponent( info.GetItemPart( VinyardGroundPosition.East ).ItemID ), width, y, 0 );
			
			for ( int x = 1; x <= w; ++x )
				AddComponent( new AddonComponent( info.GetItemPart( VinyardGroundPosition.South ).ItemID ), x, height, 0 );
			
			for ( int x = 1; x <= w; ++x )
				for ( int y = 1; y <= h; ++y )
					AddComponent( new AddonComponent( info.GetItemPart( VinyardGroundPosition.Center ).ItemID ), x, y, 0 );
		}

		public VinyardGroundAddon( Serial serial ) : base( serial )
		{
		}
		#endregion

		public override void OnDoubleClick( Mobile from )
		{
			BaseHouse house = BaseHouse.FindHouseAt( this );

			if ( house != null && house.IsCoOwner( from ) )
			{
				if ( from.InRange( GetWorldLocation(), 3 ) )
				{
                    from.SendGump(new ConfirmRemovalGump( this ));
				}
				else
				{
					from.SendLocalizedMessage( 500295 ); // You are too far away to do that.
				}
			}
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
	
	public enum VinyardGroundType
	{
		FurrowNoBorder,
		FurrowBorder,
		PlainNoBorder,
		PlainBorder
	}
	
	public enum VinyardGroundPosition
	{
		Top,
		Bottom,
		Left,
		Right,
		West,
		North,
		East,
		South,
		Center
	}
	
	public class VinyardGroundInfo
	{
		private VinyardGroundItemPart[] m_Entries;
		
		public VinyardGroundItemPart[] Entries{ get{ return m_Entries; } }
		
		public VinyardGroundInfo( VinyardGroundItemPart[] entries )
		{
			m_Entries = entries;
		}
		
		public VinyardGroundItemPart GetItemPart( VinyardGroundPosition pos )
		{
			int i = (int)pos;

			if ( i < 0 || i >= m_Entries.Length )
				i = 0;

			return m_Entries[i];
		}
		
		public static VinyardGroundInfo GetInfo( int type )
		{
			if ( type < 0 || type >= m_Infos.Length )
				type = 0;

			return m_Infos[type];
		}
		
		#region VinyardGroundInfo definitions
		private static VinyardGroundInfo[] m_Infos = new VinyardGroundInfo[] {
/* FurrowNoBorder */		new VinyardGroundInfo( new VinyardGroundItemPart[] { 
						new VinyardGroundItemPart( 0x32C9, VinyardGroundPosition.Top, -1, -1 ),
						new VinyardGroundItemPart( 0x32C9, VinyardGroundPosition.Bottom, -1, -1 ),
						new VinyardGroundItemPart( 0x32C9, VinyardGroundPosition.Left, -1, -1 ),
						new VinyardGroundItemPart( 0x32C9, VinyardGroundPosition.Right, -1, -1 ),
						new VinyardGroundItemPart( 0x32C9, VinyardGroundPosition.West, -1, -1 ),
						new VinyardGroundItemPart( 0x32C9, VinyardGroundPosition.North, -1, -1 ),
						new VinyardGroundItemPart( 0x32C9, VinyardGroundPosition.East, -1, -1 ),
						new VinyardGroundItemPart( 0x32C9, VinyardGroundPosition.South, -1, -1 ),
						new VinyardGroundItemPart( 0x32C9, VinyardGroundPosition.Center, 44, 24 )
					}),
/* FurrowBorder */		new VinyardGroundInfo( new VinyardGroundItemPart[] { 
						new VinyardGroundItemPart( 0x1B30, VinyardGroundPosition.Top, 44, 7 ),
						new VinyardGroundItemPart( 0x1B2F, VinyardGroundPosition.Bottom, 44, 68 ),
						new VinyardGroundItemPart( 0x1B31, VinyardGroundPosition.Left, 0, 35 ),
						new VinyardGroundItemPart( 0x1B32, VinyardGroundPosition.Right, 88, 32 ),
						new VinyardGroundItemPart( 0x1B29, VinyardGroundPosition.West, 22, 12 ),
						new VinyardGroundItemPart( 0x1B2A, VinyardGroundPosition.North, 66, 12 ),
						new VinyardGroundItemPart( 0x1B28, VinyardGroundPosition.East, 66, 46 ),
						new VinyardGroundItemPart( 0x1B27, VinyardGroundPosition.South, 22, 46 ),
						new VinyardGroundItemPart( 0x32C9, VinyardGroundPosition.Center, 44, 24 )
					}),
/* PlainNoBorder */		new VinyardGroundInfo( new VinyardGroundItemPart[] { 
						new VinyardGroundItemPart( 0x31F4, VinyardGroundPosition.Top, -1, -1 ),
						new VinyardGroundItemPart( 0x31F4, VinyardGroundPosition.Bottom, -1, -1 ),
						new VinyardGroundItemPart( 0x31F4, VinyardGroundPosition.Left, -1, -1 ),
						new VinyardGroundItemPart( 0x31F4, VinyardGroundPosition.Right, -1, -1 ),
						new VinyardGroundItemPart( 0x31F4, VinyardGroundPosition.West, -1, -1 ),
						new VinyardGroundItemPart( 0x31F4, VinyardGroundPosition.North, -1, -1 ),
						new VinyardGroundItemPart( 0x31F4, VinyardGroundPosition.East, -1, -1 ),
						new VinyardGroundItemPart( 0x31F4, VinyardGroundPosition.South, -1, -1 ),
						new VinyardGroundItemPart( 0x31F4, VinyardGroundPosition.Center, 44, 24 )
					}),
/* PlainBorder */		new VinyardGroundInfo( new VinyardGroundItemPart[] { 
						new VinyardGroundItemPart( 0x1B30, VinyardGroundPosition.Top, 44, 7 ),
						new VinyardGroundItemPart( 0x1B2F, VinyardGroundPosition.Bottom, 44, 68 ),
						new VinyardGroundItemPart( 0x1B31, VinyardGroundPosition.Left, 0, 35 ),
						new VinyardGroundItemPart( 0x1B32, VinyardGroundPosition.Right, 88, 32 ),
						new VinyardGroundItemPart( 0x1B29, VinyardGroundPosition.West, 22, 11 ),
						new VinyardGroundItemPart( 0x1B2A, VinyardGroundPosition.North, 66, 12 ),
						new VinyardGroundItemPart( 0x1B28, VinyardGroundPosition.East, 66, 46 ),
						new VinyardGroundItemPart( 0x1B27, VinyardGroundPosition.South, 22, 46 ),
						new VinyardGroundItemPart( 0x31F4, VinyardGroundPosition.Center, 44, 24 )
					})
			};
			#endregion
			
		public static VinyardGroundInfo[] Infos{ get{ return m_Infos; } }
	}
	
	public class VinyardGroundItemPart
	{
		private int m_ItemID;
		private  VinyardGroundPosition m_Info;
		private int m_OffsetX;
		private int m_OffsetY;
		
		public int ItemID
		{
			get{ return m_ItemID; }
		}
		
		public  VinyardGroundPosition VinyardGroundPosition
		{
			get{ return m_Info; }
		}
		
		// For Gump Rendering
		public int OffsetX
		{
			get{ return m_OffsetX; }
		}
		
		// For Gump Rendering
		public int OffsetY
		{
			get{ return m_OffsetY; }
		}
		
		public VinyardGroundItemPart( int itemID,  VinyardGroundPosition info, int offsetX, int offsetY )
		{
			m_ItemID = itemID;
			m_Info = info;
			m_OffsetX = offsetX;
			m_OffsetY = offsetY;
		}
	}

    public class ConfirmRemovalGump : Gump
    {
        private VinyardGroundAddon m_VGAddon;

        public ConfirmRemovalGump(VinyardGroundAddon vgaddon)
            : base(50, 50)
        {
            m_VGAddon = vgaddon;

            AddBackground(0, 0, 450, 260, 9270);

            AddAlphaRegion(12, 12, 426, 22);
            AddTextEntry(13, 13, 379, 20, 32, 0, @"Warning!");

            AddAlphaRegion(12, 39, 426, 209);

            AddHtml(15, 50, 420, 185, "<BODY>" +
"<BASEFONT COLOR=YELLOW>You are about to remove your vinyard ground addon!<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Before removing, be sure to use your grapevine placement tool "+
"<BASEFONT COLOR=YELLOW>to delete any grapevines that you have placed.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Upon removal of this addon, a replacement vinyard ground addon deed " +
"<BASEFONT COLOR=YELLOW>will be placed in your backpack.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Are you sure you want to remove this addon?<BR><BR>" +
                             "</BODY>", false, false);

            AddButton(13, 220, 0xFA5, 0xFA6, 1, GumpButtonType.Reply, 0);
            AddHtmlLocalized(47, 222, 150, 20, 1052072, 0x7FFF, false, false); // Continue

            //AddButton(200, 245, 0xFB1, 0xFB2, 0, GumpButtonType.Reply, 0);
            //AddHtmlLocalized(47, 247, 450, 20, 1060051, 0x7FFF, false, false); // CANCEL
            AddButton(350, 220, 0xFB1, 0xFB2, 0, GumpButtonType.Reply, 0);
            AddHtmlLocalized(385, 222, 100, 20, 1060051, 0x7FFF, false, false); // CANCEL
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            if (info.ButtonID == 0 )
                return;

            Mobile from = sender.Mobile;

            from.AddToBackpack(new VinyardGroundAddonDeed());
            m_VGAddon.Delete();

            from.SendMessage( "Vinyard ground addon deleted" );
        }
    }
}
