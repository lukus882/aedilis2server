using System;
using Server.Network;
using Server.Gumps;
using Server.Multis;
using System.Collections.Generic;
using Server.ContextMenus; 


namespace Server.Items
{
   public class July4th2007GiftDeed : Item
   {
      [Constructable]
      public July4th2007GiftDeed() : base( 0x14F0 )
      {
         Movable = true;
         Hue = Utility.RandomDyedHue();
         Name = "a 4th of july 2007 gift deed";
      }

      public override void OnDoubleClick( Mobile from )
      {
      from.CloseGump( typeof( July4th2007GiftGump ) );
      from.SendGump( new July4th2007GiftGump(from) );
      this.Delete();
      }

      public July4th2007GiftDeed( Serial serial ) : base( serial )
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
