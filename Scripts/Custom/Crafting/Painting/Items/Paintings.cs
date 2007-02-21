/*
 created by:Bad Karma aka Broadside

  			        BBBBB        A      DDDDD  
  			       BB  BB      AAA     DDDDDD
  			      BB  BB     AA AA    DD   DD
 			     BBBBBB	   AA   AA   DD   DD
  		    	BBBBB     AAAAAAA   DD   DD
   		       BB  BB    AAAAAAA   DD   DD
   		      BB  BB    AA   AA   DDDDDD
		     BBBBBB    AA   AA   DDDDD

			        KK   KK       A      RRRRRR    MM   MM      A
			       KK  KK       AAA     RR   RR   MMM MMM     AAA
			      KK KK       AA AA    RR   RR   MMMMMMM    AA AA
			     KKKK       AA   AA   RRRRRR    MM M MM   AA   AA
			    KKKK       AAAAAAA   RRRR      MM   MM   AAAAAAA
			   KK KK      AAAAAAA   RR RR     MM   MM   AAAAAAA
			  KK   KK    AA   AA   RR  RR    MM   MM   AA   AA
			 KK     KK  AA   AA   RR   RR   MM   MM   AA   AA
 */
using System;
using Server.Network;

namespace Server.Items
{
    #region //South Portraits
    public class ManPortrait1South : Item
    {
        [Constructable]
        public ManPortrait1South()
            : base(0xEA3)
        {
            Weight = 10;
            Name = "Portrait";
        }

        public ManPortrait1South(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class BlueWomanPortraitSouth : Item
    {
        [Constructable]
        public BlueWomanPortraitSouth()
            : base(0xEE7)
        {
            Weight = 10;
            Name = "Portrait";
        }

        public BlueWomanPortraitSouth(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class OrangeWomanPortraitSouth : Item
    {
        [Constructable]
        public OrangeWomanPortraitSouth()
            : base(0xE9F)
        {
            Weight = 10;
            Name = "Portrait";
        }

        public OrangeWomanPortraitSouth(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class RedWomanPortraitSouth : Item
    {
        [Constructable]
        public RedWomanPortraitSouth()
            : base(0xEA7)
        {
            Weight = 10;
            Name = "Portrait";
        }

        public RedWomanPortraitSouth(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class GreenWomanPortraitSouth : Item
    {
        [Constructable]
        public GreenWomanPortraitSouth()
            : base(0xEA6)
        {
            Weight = 10;
            Name = "Portrait";
        }

        public GreenWomanPortraitSouth(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
    
    public class CasualManPortraitSouth : Item
    {
        [Constructable]
        public CasualManPortraitSouth()
            : base(0x2A99)
        {
            Weight = 10;
            Name = "Portrait";
        }

        public CasualManPortraitSouth(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    #endregion

    #region //East Portraits
    public class ManPortrait1East : Item
    {
        [Constructable]
        public ManPortrait1East()
            : base(0xEA4)
        {
            Weight = 10;
            Name = "Portrait";
        }

        public ManPortrait1East(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class GreenWomanPortraitEast : Item
    {
        [Constructable]
        public GreenWomanPortraitEast()
            : base(0xEC8)
        {
            Weight = 10;
            Name = "Portrait";
        }

        public GreenWomanPortraitEast(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class BlueWomanPortraitEast : Item
    {
        [Constructable]
        public BlueWomanPortraitEast()
            : base(0xEC9)
        {
            Weight = 10;
            Name = "Portrait";
        }

        public BlueWomanPortraitEast(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class PurpleWomanPortraitEast : Item
    {
        [Constructable]
        public PurpleWomanPortraitEast()
            : base(0xEA5)
        {
            Weight = 10;
            Name = "Portrait";
        }

        public PurpleWomanPortraitEast(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
    #endregion

#region //Large South Paintings
    public class LargeWomanPortrait : Item
    {
        [Constructable]
        public LargeWomanPortrait()
            : base(0xEA0)
        {
            Weight = 10;
            Name = "Lady's Portrait";
        }

        public LargeWomanPortrait(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class LargeManPortraitSouth : Item
    {
        [Constructable]
        public LargeManPortraitSouth()
            : base(0x2A5D)
        {
            Weight = 10;
            Name = "Portrait";
        }

        public LargeManPortraitSouth(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class FancyLadyPortraitSouth1 : Item
    {
        [Constructable]
        public FancyLadyPortraitSouth1()
            : base(0x2A66)
        {
            Weight = 10;
            Name = "Painting";
        }

        public FancyLadyPortraitSouth1(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class YoungManPortraitSouth : Item
    {
        [Constructable]
        public YoungManPortraitSouth()
            : base(0x2A69)
        {
            Weight = 10;
            Name = "Portrait";
        }

        public YoungManPortraitSouth(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

#endregion

    #region //Large East Portraits

    public class LargeManPortraitEast : Item
    {
        [Constructable]
        public LargeManPortraitEast()
            : base(0x2A61)
        {
            Weight = 10;
            Name = "Portrait";
        }

        public LargeManPortraitEast(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class FancyLadyPortraitEast1 : Item
    {
        [Constructable]
        public FancyLadyPortraitEast1()
            : base(0x2A67)
        {
            Weight = 10;
            Name = "Portrait";
        }

        public FancyLadyPortraitEast1(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class YoungManPortraitEast : Item
    {
        [Constructable]
        public YoungManPortraitEast()
            : base(0x2A6D)
        {
            Weight = 10;
            Name = "Portrait";
        }

        public YoungManPortraitEast(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    #endregion

    #region   //Eastern Paintings
    public class RedPaintingSouth : Item
    {
        [Constructable]
        public RedPaintingSouth()
            : base(0x240D)
        {
            Weight = 10;
            Name = "Painting";
        }

        public RedPaintingSouth(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class RedPaintingEast : Item
    {
        [Constructable]
        public RedPaintingEast()
            : base(0x240E)
        {
            Weight = 10;
            Name = "Painting";
        }

        public RedPaintingEast(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class TanPaintingSouth : Item
    {
        [Constructable]
        public TanPaintingSouth()
            : base(0x240F)
        {
            Weight = 10;
            Name = "Painting";
        }

        public TanPaintingSouth(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class TanPaintingEast : Item
    {
        [Constructable]
        public TanPaintingEast()
            : base(0x2410)
        {
            Weight = 10;
            Name = "Painting";
        }

        public TanPaintingEast(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class TallPaintingSouth : Item
    {
        [Constructable]
        public TallPaintingSouth()
            : base(0x2411)
        {
            Weight = 10;
            Name = "Painting";
        }

        public TallPaintingSouth(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class TallPaintingEast : Item
    {
        [Constructable]
        public TallPaintingEast()
            : base(0x2412)
        {
            Weight = 10;
            Name = "Painting";
        }

        public TallPaintingEast(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class WarriorPaintingSouth : Item
    {
        [Constructable]
        public WarriorPaintingSouth()
            : base(0x2413)
        {
            Weight = 10;
            Name = "Painting";
        }

        public WarriorPaintingSouth(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class WarriorPaintingEast : Item
    {
        [Constructable]
        public WarriorPaintingEast()
            : base(0x2414)
        {
            Weight = 10;
            Name = "Painting";
        }

        public WarriorPaintingEast(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class EasternPaintingSouth1 : Item
    {
        [Constructable]
        public EasternPaintingSouth1()
            : base(0x2415)
        {
            Weight = 10;
            Name = "Painting";
        }

        public EasternPaintingSouth1(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class EasternPaintingEast1 : Item
    {
        [Constructable]
        public EasternPaintingEast1()
            : base(0x2416)
        {
            Weight = 10;
            Name = "Painting";
        }

        public EasternPaintingEast1(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class EasternPaintingSouth2 : Item
    {
        [Constructable]
        public EasternPaintingSouth2()
            : base(0x2417)
        {
            Weight = 10;
            Name = "Painting";
        }

        public EasternPaintingSouth2(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class EasternPaintingEast2 : Item
    {
        [Constructable]
        public EasternPaintingEast2()
            : base(0x2418)
        {
            Weight = 10;
            Name = "Painting";
        }

        public EasternPaintingEast2(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
    #endregion 

    #region //Murals
    public class MuralSouth : Item
    {
        [Constructable]
        public MuralSouth()
            : base(0x2887)
        {
            Weight = 10;
            Name = "Mural";
        }

        public MuralSouth(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

        public class MuralEast : Item
        {
            [Constructable]
            public MuralEast()
                : base(0x2886)
            {
                Weight = 10;
                Name = "Mural";
            }

            public MuralEast(Serial serial)
                : base(serial)
            {
            }

            public override void Serialize(GenericWriter writer)
            {
                base.Serialize(writer);

                writer.Write((int)0);
            }

            public override void Deserialize(GenericReader reader)
            {
                base.Deserialize(reader);

                int version = reader.ReadInt();
            }
   
        }
   #endregion
    }
