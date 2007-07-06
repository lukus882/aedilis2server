using System; 
using Server.Network; 
using Server.Items; 
using Server.Accounting;
using Server.Misc;

namespace Server.Items 
{ 
[Flipable( 0x1541, 0x1542 )]
	public class PictureSash : BaseMiddleTorso
	{
		[Constructable]
		public PictureSash() : this( 0 )
		{
		}

		[Constructable]
		public PictureSash( int hue ) : base( 0x1541, hue )
		{
			Weight = 1.0;
                        this.Name = "Picture Taker's Sash";
                        this.Attributes.Luck = 20;
                        this.Attributes.NightSight = 1;
                        LootType=LootType.Blessed;
		}

		public PictureSash( Serial serial ) : base( serial )
		{
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
    
}
