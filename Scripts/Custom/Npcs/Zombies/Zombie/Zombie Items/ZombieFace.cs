using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class ZombieFace : TribalMask
    {
        [Constructable]
        public ZombieFace()
        {
            
	    Name = "Zombie Face";            

	    this.Hue = 1425;
            this.Attributes.BonusDex = 10;
            this.Attributes.BonusHits = 10;
            this.Attributes.BonusInt = 10;
            this.Attributes.BonusMana = 10;
            this.Attributes.BonusStam = 10;
            this.Attributes.NightSight = 1;
            this.Attributes.ReflectPhysical = 10;
            this.Attributes.RegenHits = 2;
            this.Attributes.RegenMana = 2;
            this.Attributes.RegenStam = 2;

            this.LootType = LootType.Regular;
        }

        public ZombieFace( Serial serial ) : base( serial )
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