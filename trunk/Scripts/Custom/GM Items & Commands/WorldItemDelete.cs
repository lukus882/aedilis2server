//Original script name deleter, authored by Sparkin
//Modified by Ashlar, beloved of Morrigan.

using System;
using Server.Gumps;
using Server.Network;
using Server.Targeting;
using System.Collections;

namespace Server.Items
{
    public class WorldItemDelete : Item
    {
        private int toDelete;
        [CommandProperty( AccessLevel.Owner )]
        public int ToDelete { get { return toDelete; } set { toDelete = value; InvalidateProperties(); } }

        private int targetsCount;
        public int TargetsCount { get { return targetsCount; } set { targetsCount = value; InvalidateProperties(); } }

        [Constructable]
        public WorldItemDelete() : base( 0x22C5 ) //old id is 0x2259
        {
            Movable = true;
            Light = LightType.Circle300;
            Weight = 0;
            Name = "World Item Deletion";
            LootType = LootType.Blessed;

            toDelete = 0;
            targetsCount = 0;
        }

        public override void OnDoubleClick( Mobile from )
        {
            if ( from.AccessLevel == AccessLevel.Owner )
                from.SendGump( new WorldItemDeleteGump( this ) );
            else
                this.Delete();

        }
        public WorldItemDelete( Serial serial ) : base( serial )
        {
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );
            writer.WriteEncodedInt( ( int )0 ); // version

            writer.Write( ( int )toDelete );
            writer.Write( ( int )targetsCount );
        }
        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );
            int version = reader.ReadEncodedInt();

            toDelete = reader.ReadInt();
            targetsCount = reader.ReadInt();
        }

        public class WorldItemDeleteGump : Gump
        {
            public static int NegProtection( string numberstring )
            {
                int ns = Utility.ToInt32( numberstring );

                if ( ns < 0 )
                    return 0;
                else
                    return ns;
            }
            public void TargCount( WorldItemDelete wid )
            {
                ArrayList targets = new ArrayList();
                foreach ( Item it in World.Items.Values )
                    if ( !( it.ItemID < WID.ToDelete ) && !( it.ItemID > WID.ToDelete ) )
                        targets.Add( it );

                WID.TargetsCount = targets.Count;
            }
            WorldItemDelete WID;

            public WorldItemDeleteGump( WorldItemDelete wid ) : base( 0, 0 )
            {
                WID = wid;

                TargCount( WID );

                this.Closable = true;
                this.Disposable = true;
                this.Dragable = true;
                this.Resizable = false;

                this.AddPage( 0 );

                this.AddBackground( 88, 54, 382, 195, 2620 );
                this.AddLabel( 95, 60, 1149, @"World Item Delete:" );

                this.AddLabel( 95, 85, 1149, @"About to delete: " );
                this.AddLabel( 110, 105, 1149, @"" + WID.targetsCount );
                this.AddLabel( 95, 125, 1149, @"Items of ItemID:" );
                this.AddLabel( 210, 125, 1149, @"" + WID.ToDelete );

                this.AddButton( 95, 152, 1210, 1209, 2, GumpButtonType.Reply, 0 );//ChangeItemID
                this.AddLabel( 115, 148, 1149, @"Change ItemID to:" );
                this.AddTextEntry( 231, 148, 87, 20, 1149, 3, @"" + WID.ToDelete );

                this.AddLabel( 95, 175, 1149, @"Hit okay to delete, " );
                this.AddLabel( 95, 195, 1149, @"Cancel to get outa here!" );

                this.AddLabel( 290, 60, 1149, @"" + WID.ToDelete );
                this.AddLabel( 292, 78, 1149, @"Looks like:" );
                this.AddItem( 361, 162, +WID.ToDelete );

                this.AddButton( 314, 225, 1210, 1209, 4, GumpButtonType.Reply, 0 );
                this.AddLabel( 330, 221, 1149, @"Get ItemID By Target" );

                this.AddButton( 95, 220, 2073, 2072, 0, GumpButtonType.Reply, 0 );
                this.AddButton( 225, 220, 2076, 2075, 1, GumpButtonType.Reply, 0 );

            }

            public override void OnResponse( NetState state, RelayInfo info )
            {
                Mobile from = state.Mobile;

                TextRelay entry3 = info.GetTextEntry( 3 );
                int toDelete = NegProtection( entry3 == null ? "" : entry3.Text.Trim() );

                if ( info.ButtonID == 1 )
                {
                    ArrayList targets = new ArrayList();
                    foreach ( Item it in World.Items.Values )
                        if ( !( it.ItemID < WID.ToDelete ) && !( it.ItemID > WID.ToDelete ) )
                            targets.Add( it );

                    for ( int i = 0; i < targets.Count; ++i )
                    {
                        Item item = ( Item )targets[ i ];
                        item.Delete();
                    }
                }
                else if ( info.ButtonID == 2 )
                {
                    WID.ToDelete = toDelete;
                    from.SendGump( new WorldItemDeleteGump( WID ) );
                }
                else if ( info.ButtonID == 4 )
                {
                    from.Target = new AddItemIDByTarget( WID );
                }
            }
        }

        public class AddItemIDByTarget : Target
        {
            private WorldItemDelete WID;

            public AddItemIDByTarget( WorldItemDelete wid ) : base( 4, false, TargetFlags.None )
            {
                WID = wid;
            }

            protected override void OnTarget( Mobile from, object targeted )
            {
                if ( targeted is Item )
                {
                    Item it = ( Item )targeted;
                    WID.ToDelete = it.ItemID;
                }
                else
                    from.SendMessage( "That does not register as an Item." );

                from.CloseGump( typeof( WorldItemDeleteGump ) );
                from.SendGump( new WorldItemDeleteGump( WID ) );
            }
        }
    }
}
