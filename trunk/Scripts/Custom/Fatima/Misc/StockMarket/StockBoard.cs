using System;
using Server;
using Server.Mobiles;
using Server.Items;

namespace Fatima.Items
{
        [FlipableAttribute( 0x1E5E, 0x1E5F )]
        public class StockBoard : Item
        {
                [Constructable]
                public StockBoard() : base( 0x1E5F )
                {
			Name = "Commodity Board";
                        Weight = 2500.0;
                }

		public override void OnDoubleClick( Mobile from )
		{
			from.SendGump( new Fatima.Gumps.StockViewGump( from ) );
		}

                public StockBoard( Serial serial ) : base( serial )
                {
		}

                public override void Serialize( GenericWriter writer )
                {
                        base.Serialize( writer );

                        writer.Write( (int) 0 );
                }

                public override void Deserialize(GenericReader reader)
                {
                        base.Deserialize( reader );

                        int version = reader.ReadInt();
                }
        }
}
