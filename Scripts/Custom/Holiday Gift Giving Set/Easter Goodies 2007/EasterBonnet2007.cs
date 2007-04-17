using System;

namespace Server.Items
{
	public class EasterBonnet2007 : BaseHat
	{
		public override int PhysicalResistance{ get{ return 0; } }
		public override int FireResistance{ get{ return 5; } }
		public override int ColdResistance{ get{ return 9; } }
		public override int PoisonResistance{ get{ return 5; } }
		public override int EnergyResistance{ get{ return 5; } }

		[Constructable]
		public EasterBonnet2007() : this( 0 )
		{
		}

		[Constructable]
		public EasterBonnet2007( int hue ) : base( 0x1714, hue )
		{
			Weight = 1.0;
            Name = "Easter Bonnet 2007";
            Hue = Utility.RandomList( 0xFF, 0x1F, 0xBAD, 0xAFE, 0x4DE, 0xAF5 );
			LootType = LootType.Blessed;
		}

		public EasterBonnet2007( Serial serial ) : base( serial )
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