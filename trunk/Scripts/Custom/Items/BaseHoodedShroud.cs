using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
   [FlipableAttribute( 0x2683, 0x2684 )]
   public class BaseHoodedShroud : BaseOuterTorso
   {
      [Constructable]
      public BaseHoodedShroud() : base( 0x1F03 )
      {
         Weight = 5.0;
         Name = "A Hooded Robe";
         Hue = 0;
         Layer = Layer.OuterTorso;
         LootType=LootType.Blessed;
      }

      public override void OnDoubleClick( Mobile m )
      {
         if( Parent != m )
         {
            m.SendMessage( "You can not use this item from your pack!" );
         }
         else
         {
            if ( ItemID == 0x2683 || ItemID == 0x2684 )
            {
               m.SendMessage( "You lower the hood." );
               m.PlaySound( 0x57 );
               ItemID = 0x1F03;
               m.NameMod = null;
               LootType=LootType.Blessed;
               m.RemoveItem(this);
               m.EquipItem(this);
            }
            else if ( ItemID == 0x1F03 || ItemID == 0x1F04 )
            {
               m.SendMessage( "You pull the hood over your head." );
               m.PlaySound( 0x57 );
               ItemID = 0x2683;
               LootType=LootType.Blessed;
               m.RemoveItem(this);
               m.EquipItem(this);
            }
         }
      }

       public override bool OnEquip( Mobile from )
      {
         return base.OnEquip(from);
      }

      public override void OnRemoved( Object o )
      {
      }

      public override bool Dye ( Mobile from, DyeTub sender )
      {
      return false;
      }

      public BaseHoodedShroud( Serial serial ) : base( serial )
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

