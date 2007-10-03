// Original Author Unknown
// Updated to be halloween 2007 by GreyWolf

using System;
using Server;
using Server.Items;

namespace Server.Items
{  
	public class HalloweenDoublet2007 : Doublet
	{
           	[Constructable]
           	public HalloweenDoublet2007()
           	{
           		Name = "a spectral doublet";
			Hue = 0x4001;
			LootType = LootType.Blessed;
           	}

           	[Constructable]
           	public HalloweenDoublet2007(int amount)
           	{
           	}

           	public HalloweenDoublet2007(Serial serial) : base( serial )
           	{
           	}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1060662, "Halloween\t2007" );
		}

          	public override void Serialize(GenericWriter writer)
          	{
           		base.Serialize(writer);

           		writer.Write((int)0); // version 
     		}

           	public override void Deserialize(GenericReader reader)
      	{
           		base.Deserialize(reader);

          		int version = reader.ReadInt();
           	}
	}
}
