// Original Author Unknown
// Updated to be halloween 2007 by GreyWolf

using System;
using Server;
using Server.Items;

namespace Server.Items
{  
	public class HalloweenBag2007 : Bag
	{
           	[Constructable]
           	public HalloweenBag2007()
           	{
           		Name = "Have A Spooky Halloween 2007";
			Hue = 1258;
           	}

           	[Constructable]
           	public HalloweenBag2007(int amount)
           	{
           	}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1060662, "Halloween\t2007" );
		}

           	public HalloweenBag2007(Serial serial) : base( serial )
           	{
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
