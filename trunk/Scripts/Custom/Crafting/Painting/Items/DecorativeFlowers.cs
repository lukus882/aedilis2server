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
using Server.Items;
using Server.Network;

namespace Server.Items
{
    #region //Campions
    public class BrightBlueCampionFlowers : Item
	{
		[Constructable]
        public BrightBlueCampionFlowers() : base(0xC83)
		{
            Name = "Decorative Bright Blue Campion Flowers";
            Hue = 0x5;
			Weight = 4.0;
		}

        public BrightBlueCampionFlowers(Serial serial)
            : base(serial)
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
    
    public class BlackCampionFlowers : Item
    {
        [Constructable]
        public BlackCampionFlowers()
            : base(0xC83)
        {
            Name = "Decorative Black Campion Flowers";
            Hue = 0x455;
            Weight = 4.0;
        }

        public BlackCampionFlowers(Serial serial)
            : base(serial)
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

    public class WhiteCampionFlowers : Item
    {
        [Constructable]
        public WhiteCampionFlowers()
            : base(0xC83)
        {
            Name = "Decorative White Campion Flowers";
            Hue = 0x481;
            Weight = 4.0;
        }

        public WhiteCampionFlowers(Serial serial)
            : base(serial)
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

    public class PinkCampionFlowers : Item
    {
        [Constructable]
        public PinkCampionFlowers()
            : base(0xC83)
        {
            Name = "Decorative Pink Campion Flowers";
            Hue = 0x483;
            Weight = 4.0;
        }

        public PinkCampionFlowers(Serial serial)
            : base(serial)
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

    public class MagentaCampionFlowers : Item
    {
        [Constructable]
        public MagentaCampionFlowers()
            : base(0xC83)
        {
            Name = "Decorative Magenta Campion Flowers";
            Hue = 0x486;
            Weight = 4.0;
        }

        public MagentaCampionFlowers(Serial serial)
            : base(serial)
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

    public class AquaCampionFlowers : Item
    {
        [Constructable]
        public AquaCampionFlowers()
            : base(0xC83)
        {
            Name = "Decorative Aqua Campion Flowers";
            Hue = 0x495;
            Weight = 4.0;
        }

        public AquaCampionFlowers(Serial serial)
            : base(serial)
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

    public class FireRedCampionFlowers : Item
    {
        [Constructable]
        public FireRedCampionFlowers()
            : base(0xC83)
        {
            Name = "Fire Red Campion Flowers";
            Hue = 0x489;
            Weight = 4.0;
        }

        public FireRedCampionFlowers(Serial serial)
            : base(serial)
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

    public class CampionFlowers : Item
    {
        [Constructable]
        public CampionFlowers()
            : base(0xC83)
        {
            Name = "Decorative Campion Flowers";
            Hue = 0x0;
            Weight = 4.0;
        }

        public CampionFlowers(Serial serial)
            : base(serial)
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

    public class RedCampionFlowers : Item
    {
        [Constructable]
        public RedCampionFlowers()
            : base(0xC83)
        {
            Name = "Decorative Red Campion Flowers";
            Hue = 0x66D;
            Weight = 4.0;
        }

        public RedCampionFlowers(Serial serial)
            : base(serial)
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

    public class BlueCampionFlowers : Item
    {
        [Constructable]
        public BlueCampionFlowers()
            : base(0xC83)
        {
            Name = "Decorative Blue Campion Flowers";
            Hue = 0x53D;
            Weight = 4.0;
        }

        public BlueCampionFlowers(Serial serial)
            : base(serial)
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

    public class PurpleCampionFlowers : Item
    {
        [Constructable]
        public PurpleCampionFlowers()
            : base(0xC83)
        {
            Name = "Decorative Purple Campion Flowers";
            Hue = 0xD;
            Weight = 4.0;
        }

        public PurpleCampionFlowers(Serial serial)
            : base(serial)
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

    public class YellowCampionFlowers : Item
    {
        [Constructable]
        public YellowCampionFlowers()
            : base(0xC83)
        {
            Name = "Decorative Yellow Campion Flowers";
            Hue = 0x8A5;
            Weight = 4.0;
        }

        public YellowCampionFlowers(Serial serial)
            : base(serial)
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

    public class OrangeCampionFlowers : Item
    {
        [Constructable]
        public OrangeCampionFlowers()
            : base(0xC83)
        {
            Name = "Decorative Orange Campion Flowers";
            Hue = 0x46F;
            Weight = 4.0;
        }

        public OrangeCampionFlowers(Serial serial)
            : base(serial)
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

    public class GreenCampionFlowers : Item
    {
        [Constructable]
        public GreenCampionFlowers()
            : base(0xC83)
        {
            Name = "Decorative Green Campion Flowers";
            Hue = 0x59B;
            Weight = 4.0;
        }

        public GreenCampionFlowers(Serial serial)
            : base(serial)
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

    public class BrightRedCampionFlowers : Item
    {
        [Constructable]
        public BrightRedCampionFlowers()
            : base(0xC83)
        {
            Name = "Decorative Bright Red Campion Flowers";
            Hue = 0x21;
            Weight = 4.0;
        }

        public BrightRedCampionFlowers(Serial serial)
            : base(serial)
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

    public class BrightPurpleCampionFlowers : Item
    {
        [Constructable]
        public BrightPurpleCampionFlowers()
            : base(0xC83)
        {
            Name = "Decorative Bright Purple Campion Flowers";
            Hue = 0x10;
            Weight = 4.0;
        }

        public BrightPurpleCampionFlowers(Serial serial)
            : base(serial)
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

    public class BrightYellowCampionFlowers : Item
    {
        [Constructable]
        public BrightYellowCampionFlowers()
            : base(0xC83)
        {
            Name = "Decorative Bright Yellow Campion Flowers";
            Hue = 0x38;
            Weight = 4.0;
        }

        public BrightYellowCampionFlowers(Serial serial)
            : base(serial)
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

    public class BrightOrangeCampionFlowers : Item
    {
        [Constructable]
        public BrightOrangeCampionFlowers()
            : base(0xC83)
        {
            Name = "Decorative Bright Orange Campion Flowers";
            Hue = 0x2B;
            Weight = 4.0;
        }

        public BrightOrangeCampionFlowers(Serial serial)
            : base(serial)
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

    public class BrightGreenCampionFlowers : Item
    {
        [Constructable]
        public BrightGreenCampionFlowers()
            : base(0xC83)
        {
            Name = "Decorative Bright Green Campion Flowers";
            Hue = 0x42;
            Weight = 4.0;
        }

        public BrightGreenCampionFlowers(Serial serial)
            : base(serial)
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
    #endregion
    #region //Poppies
    public class BrightBluePoppies : Item
    {
        [Constructable]
        public BrightBluePoppies()
            : base(0xC86)
        {
            Name = "Decorative Bright Blue Poppies";
            Hue = 0x5;
            Weight = 4.0;
        }

        public BrightBluePoppies(Serial serial)
            : base(serial)
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

    public class BlackPoppies : Item
    {
        [Constructable]
        public BlackPoppies()
            : base(0xC86)
        {
            Name = "Decorative Black Poppies";
            Hue = 0x455;
            Weight = 4.0;
        }

        public BlackPoppies(Serial serial)
            : base(serial)
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

    public class WhitePoppies : Item
    {
        [Constructable]
        public WhitePoppies()
            : base(0xC86)
        {
            Name = "Decorative White Poppies";
            Hue = 0x481;
            Weight = 4.0;
        }

        public WhitePoppies(Serial serial)
            : base(serial)
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

    public class PinkPoppies : Item
    {
        [Constructable]
        public PinkPoppies()
            : base(0xC86)
        {
            Name = "Decorative Pink Poppies";
            Hue = 0x483;
            Weight = 4.0;
        }

        public PinkPoppies(Serial serial)
            : base(serial)
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

    public class MagentaPoppies : Item
    {
        [Constructable]
        public MagentaPoppies()
            : base(0xC86)
        {
            Name = "Decorative Magenta Poppies";
            Hue = 0x486;
            Weight = 4.0;
        }

        public MagentaPoppies(Serial serial)
            : base(serial)
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

    public class AquaPoppies : Item
    {
        [Constructable]
        public AquaPoppies()
            : base(0xC86)
        {
            Name = "Decorative Aqua Poppies";
            Hue = 0x495;
            Weight = 4.0;
        }

        public AquaPoppies(Serial serial)
            : base(serial)
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

    public class FireRedPoppies : Item
    {
        [Constructable]
        public FireRedPoppies()
            : base(0xC86)
        {
            Name = "Fire Red Poppies";
            Hue = 0x489;
            Weight = 4.0;
        }

        public FireRedPoppies(Serial serial)
            : base(serial)
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

    public class Poppies : Item
    {
        [Constructable]
        public Poppies()
            : base(0xC86)
        {
            Name = "Decorative Poppies";
            Hue = 0x0;
            Weight = 4.0;
        }

        public Poppies(Serial serial)
            : base(serial)
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

    public class RedPoppies : Item
    {
        [Constructable]
        public RedPoppies()
            : base(0xC86)
        {
            Name = "Decorative Red Poppies";
            Hue = 0x66D;
            Weight = 4.0;
        }

        public RedPoppies(Serial serial)
            : base(serial)
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

    public class BluePoppies : Item
    {
        [Constructable]
        public BluePoppies()
            : base(0xC86)
        {
            Name = "Decorative Blue Poppies";
            Hue = 0x53D;
            Weight = 4.0;
        }

        public BluePoppies(Serial serial)
            : base(serial)
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

    public class PurplePoppies : Item
    {
        [Constructable]
        public PurplePoppies()
            : base(0xC86)
        {
            Name = "Decorative Purple Poppies";
            Hue = 0xD;
            Weight = 4.0;
        }

        public PurplePoppies(Serial serial)
            : base(serial)
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

    public class YellowPoppies : Item
    {
        [Constructable]
        public YellowPoppies()
            : base(0xC86)
        {
            Name = "Decorative Yellow Poppies";
            Hue = 0x8A5;
            Weight = 4.0;
        }

        public YellowPoppies(Serial serial)
            : base(serial)
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

    public class OrangePoppies : Item
    {
        [Constructable]
        public OrangePoppies()
            : base(0xC86)
        {
            Name = "Decorative Orange Poppies";
            Hue = 0x46F;
            Weight = 4.0;
        }

        public OrangePoppies(Serial serial)
            : base(serial)
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

    public class GreenPoppies : Item
    {
        [Constructable]
        public GreenPoppies()
            : base(0xC86)
        {
            Name = "Decorative Green Poppies";
            Hue = 0x59B;
            Weight = 4.0;
        }

        public GreenPoppies(Serial serial)
            : base(serial)
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

    public class BrightRedPoppies : Item
    {
        [Constructable]
        public BrightRedPoppies()
            : base(0xC86)
        {
            Name = "Decorative Bright Red Poppies";
            Hue = 0x21;
            Weight = 4.0;
        }

        public BrightRedPoppies(Serial serial)
            : base(serial)
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

    public class BrightPurplePoppies : Item
    {
        [Constructable]
        public BrightPurplePoppies()
            : base(0xC86)
        {
            Name = "Decorative Bright Purple Poppies";
            Hue = 0x10;
            Weight = 4.0;
        }

        public BrightPurplePoppies(Serial serial)
            : base(serial)
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

    public class BrightYellowPoppies : Item
    {
        [Constructable]
        public BrightYellowPoppies()
            : base(0xC86)
        {
            Name = "Decorative Bright Yellow Poppies";
            Hue = 0x38;
            Weight = 4.0;
        }

        public BrightYellowPoppies(Serial serial)
            : base(serial)
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

    public class BrightOrangePoppies : Item
    {
        [Constructable]
        public BrightOrangePoppies()
            : base(0xC86)
        {
            Name = "Decorative Bright Orange Poppies";
            Hue = 0x2B;
            Weight = 4.0;
        }

        public BrightOrangePoppies(Serial serial)
            : base(serial)
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

    public class BrightGreenPoppies : Item
    {
        [Constructable]
        public BrightGreenPoppies()
            : base(0xC86)
        {
            Name = "Decorative Bright Green Poppies";
            Hue = 0x42;
            Weight = 4.0;
        }

        public BrightGreenPoppies(Serial serial)
            : base(serial)
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
    #endregion
    #region //Snowdrops
    public class BrightBlueSnowdrops : Item
    {
        [Constructable]
        public BrightBlueSnowdrops()
            : base(0xC88)
        {
            Name = "Decorative Bright Blue Snowdrops";
            Hue = 0x5;
            Weight = 4.0;
        }

        public BrightBlueSnowdrops(Serial serial)
            : base(serial)
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

    public class BlackSnowdrops : Item
    {
        [Constructable]
        public BlackSnowdrops()
            : base(0xC88)
        {
            Name = "Decorative Black Snowdrops";
            Hue = 0x455;
            Weight = 4.0;
        }

        public BlackSnowdrops(Serial serial)
            : base(serial)
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

    public class WhiteSnowdrops : Item
    {
        [Constructable]
        public WhiteSnowdrops()
            : base(0xC88)
        {
            Name = "Decorative White Snowdrops";
            Hue = 0x481;
            Weight = 4.0;
        }

        public WhiteSnowdrops(Serial serial)
            : base(serial)
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

    public class PinkSnowdrops : Item
    {
        [Constructable]
        public PinkSnowdrops()
            : base(0xC88)
        {
            Name = "Decorative Pink Snowdrops";
            Hue = 0x483;
            Weight = 4.0;
        }

        public PinkSnowdrops(Serial serial)
            : base(serial)
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

    public class MagentaSnowdrops : Item
    {
        [Constructable]
        public MagentaSnowdrops()
            : base(0xC88)
        {
            Name = "Decorative Magenta Snowdrops";
            Hue = 0x486;
            Weight = 4.0;
        }

        public MagentaSnowdrops(Serial serial)
            : base(serial)
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

    public class AquaSnowdrops : Item
    {
        [Constructable]
        public AquaSnowdrops()
            : base(0xC88)
        {
            Name = "Decorative Aqua Snowdrops";
            Hue = 0x495;
            Weight = 4.0;
        }

        public AquaSnowdrops(Serial serial)
            : base(serial)
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

    public class FireRedSnowdrops : Item
    {
        [Constructable]
        public FireRedSnowdrops()
            : base(0xC88)
        {
            Name = "Fire Red Snowdrops";
            Hue = 0x489;
            Weight = 4.0;
        }

        public FireRedSnowdrops(Serial serial)
            : base(serial)
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

    public class Snowdrops : Item
    {
        [Constructable]
        public Snowdrops()
            : base(0xC88)
        {
            Name = "Decorative Snowdrops";
            Hue = 0x0;
            Weight = 4.0;
        }

        public Snowdrops(Serial serial)
            : base(serial)
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

    public class RedSnowdrops : Item
    {
        [Constructable]
        public RedSnowdrops()
            : base(0xC88)
        {
            Name = "Decorative Red Snowdrops";
            Hue = 0x66D;
            Weight = 4.0;
        }

        public RedSnowdrops(Serial serial)
            : base(serial)
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

    public class BlueSnowdrops : Item
    {
        [Constructable]
        public BlueSnowdrops()
            : base(0xC88)
        {
            Name = "Decorative Blue Snowdrops";
            Hue = 0x53D;
            Weight = 4.0;
        }

        public BlueSnowdrops(Serial serial)
            : base(serial)
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

    public class PurpleSnowdrops : Item
    {
        [Constructable]
        public PurpleSnowdrops()
            : base(0xC88)
        {
            Name = "Decorative Purple Snowdrops";
            Hue = 0xD;
            Weight = 4.0;
        }

        public PurpleSnowdrops(Serial serial)
            : base(serial)
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

    public class YellowSnowdrops : Item
    {
        [Constructable]
        public YellowSnowdrops()
            : base(0xC88)
        {
            Name = "Decorative Yellow Snowdrops";
            Hue = 0x8A5;
            Weight = 4.0;
        }

        public YellowSnowdrops(Serial serial)
            : base(serial)
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

    public class OrangeSnowdrops : Item
    {
        [Constructable]
        public OrangeSnowdrops()
            : base(0xC88)
        {
            Name = "Decorative Orange Snowdrops";
            Hue = 0x46F;
            Weight = 4.0;
        }

        public OrangeSnowdrops(Serial serial)
            : base(serial)
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

    public class GreenSnowdrops : Item
    {
        [Constructable]
        public GreenSnowdrops()
            : base(0xC88)
        {
            Name = "Decorative Green Snowdrops";
            Hue = 0x59B;
            Weight = 4.0;
        }

        public GreenSnowdrops(Serial serial)
            : base(serial)
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

    public class BrightRedSnowdrops : Item
    {
        [Constructable]
        public BrightRedSnowdrops()
            : base(0xC88)
        {
            Name = "Decorative Bright Red Snowdrops";
            Hue = 0x21;
            Weight = 4.0;
        }

        public BrightRedSnowdrops(Serial serial)
            : base(serial)
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

    public class BrightPurpleSnowdrops : Item
    {
        [Constructable]
        public BrightPurpleSnowdrops()
            : base(0xC88)
        {
            Name = "Decorative Bright Purple Snowdrops";
            Hue = 0x10;
            Weight = 4.0;
        }

        public BrightPurpleSnowdrops(Serial serial)
            : base(serial)
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

    public class BrightYellowSnowdrops : Item
    {
        [Constructable]
        public BrightYellowSnowdrops()
            : base(0xC88)
        {
            Name = "Decorative Bright Yellow Snowdrops";
            Hue = 0x38;
            Weight = 4.0;
        }

        public BrightYellowSnowdrops(Serial serial)
            : base(serial)
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

    public class BrightOrangeSnowdrops : Item
    {
        [Constructable]
        public BrightOrangeSnowdrops()
            : base(0xC88)
        {
            Name = "Decorative Bright Orange Snowdrops";
            Hue = 0x2B;
            Weight = 4.0;
        }

        public BrightOrangeSnowdrops(Serial serial)
            : base(serial)
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

    public class BrightGreenSnowdrops : Item
    {
        [Constructable]
        public BrightGreenSnowdrops()
            : base(0xC88)
        {
            Name = "Decorative Bright Green Snowdrops";
            Hue = 0x42;
            Weight = 4.0;
        }

        public BrightGreenSnowdrops(Serial serial)
            : base(serial)
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
    #endregion
    #region //Bulrushes
    public class BrightBlueBulrushes : Item
    {
        [Constructable]
        public BrightBlueBulrushes()
            : base(0xC94)
        {
            Name = "Decorative Bright Blue Bulrushes";
            Hue = 0x5;
            Weight = 4.0;
        }

        public BrightBlueBulrushes(Serial serial)
            : base(serial)
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

    public class BlackBulrushes : Item
    {
        [Constructable]
        public BlackBulrushes()
            : base(0xC94)
        {
            Name = "Decorative Black Bulrushes";
            Hue = 0x455;
            Weight = 4.0;
        }

        public BlackBulrushes(Serial serial)
            : base(serial)
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

    public class WhiteBulrushes : Item
    {
        [Constructable]
        public WhiteBulrushes()
            : base(0xC94)
        {
            Name = "Decorative White Bulrushes";
            Hue = 0x481;
            Weight = 4.0;
        }

        public WhiteBulrushes(Serial serial)
            : base(serial)
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

    public class PinkBulrushes : Item
    {
        [Constructable]
        public PinkBulrushes()
            : base(0xC94)
        {
            Name = "Decorative Pink Bulrushes";
            Hue = 0x483;
            Weight = 4.0;
        }

        public PinkBulrushes(Serial serial)
            : base(serial)
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

    public class MagentaBulrushes : Item
    {
        [Constructable]
        public MagentaBulrushes()
            : base(0xC94)
        {
            Name = "Decorative Magenta Bulrushes";
            Hue = 0x486;
            Weight = 4.0;
        }

        public MagentaBulrushes(Serial serial)
            : base(serial)
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

    public class AquaBulrushes : Item
    {
        [Constructable]
        public AquaBulrushes()
            : base(0xC94)
        {
            Name = "Decorative Aqua Bulrushes";
            Hue = 0x495;
            Weight = 4.0;
        }

        public AquaBulrushes(Serial serial)
            : base(serial)
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

    public class FireRedBulrushes : Item
    {
        [Constructable]
        public FireRedBulrushes()
            : base(0xC94)
        {
            Name = "Fire Red Bulrushes";
            Hue = 0x489;
            Weight = 4.0;
        }

        public FireRedBulrushes(Serial serial)
            : base(serial)
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

    public class Bulrushes : Item
    {
        [Constructable]
        public Bulrushes()
            : base(0xC94)
        {
            Name = "Decorative Bulrushes";
            Hue = 0x0;
            Weight = 4.0;
        }

        public Bulrushes(Serial serial)
            : base(serial)
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

    public class RedBulrushes : Item
    {
        [Constructable]
        public RedBulrushes()
            : base(0xC94)
        {
            Name = "Decorative Red Bulrushes";
            Hue = 0x66D;
            Weight = 4.0;
        }

        public RedBulrushes(Serial serial)
            : base(serial)
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

    public class BlueBulrushes : Item
    {
        [Constructable]
        public BlueBulrushes()
            : base(0xC94)
        {
            Name = "Decorative Blue Bulrushes";
            Hue = 0x53D;
            Weight = 4.0;
        }

        public BlueBulrushes(Serial serial)
            : base(serial)
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

    public class PurpleBulrushes : Item
    {
        [Constructable]
        public PurpleBulrushes()
            : base(0xC94)
        {
            Name = "Decorative Purple Bulrushes";
            Hue = 0xD;
            Weight = 4.0;
        }

        public PurpleBulrushes(Serial serial)
            : base(serial)
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

    public class YellowBulrushes : Item
    {
        [Constructable]
        public YellowBulrushes()
            : base(0xC94)
        {
            Name = "Decorative Yellow Bulrushes";
            Hue = 0x8A5;
            Weight = 4.0;
        }

        public YellowBulrushes(Serial serial)
            : base(serial)
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

    public class OrangeBulrushes : Item
    {
        [Constructable]
        public OrangeBulrushes()
            : base(0xC94)
        {
            Name = "Decorative Orange Bulrushes";
            Hue = 0x46F;
            Weight = 4.0;
        }

        public OrangeBulrushes(Serial serial)
            : base(serial)
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

    public class GreenBulrushes : Item
    {
        [Constructable]
        public GreenBulrushes()
            : base(0xC94)
        {
            Name = "Decorative Green Bulrushes";
            Hue = 0x59B;
            Weight = 4.0;
        }

        public GreenBulrushes(Serial serial)
            : base(serial)
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

    public class BrightRedBulrushes : Item
    {
        [Constructable]
        public BrightRedBulrushes()
            : base(0xC94)
        {
            Name = "Decorative Bright Red Bulrushes";
            Hue = 0x21;
            Weight = 4.0;
        }

        public BrightRedBulrushes(Serial serial)
            : base(serial)
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

    public class BrightPurpleBulrushes : Item
    {
        [Constructable]
        public BrightPurpleBulrushes()
            : base(0xC94)
        {
            Name = "Decorative Bright Purple Bulrushes";
            Hue = 0x10;
            Weight = 4.0;
        }

        public BrightPurpleBulrushes(Serial serial)
            : base(serial)
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

    public class BrightYellowBulrushes : Item
    {
        [Constructable]
        public BrightYellowBulrushes()
            : base(0xC94)
        {
            Name = "Decorative Bright Yellow Bulrushes";
            Hue = 0x38;
            Weight = 4.0;
        }

        public BrightYellowBulrushes(Serial serial)
            : base(serial)
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

    public class BrightOrangeBulrushes : Item
    {
        [Constructable]
        public BrightOrangeBulrushes()
            : base(0xC94)
        {
            Name = "Decorative Bright Orange Bulrushes";
            Hue = 0x2B;
            Weight = 4.0;
        }

        public BrightOrangeBulrushes(Serial serial)
            : base(serial)
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

    public class BrightGreenBulrushes : Item
    {
        [Constructable]
        public BrightGreenBulrushes()
            : base(0xC94)
        {
            Name = "Decorative Bright Green Bulrushes";
            Hue = 0x42;
            Weight = 4.0;
        }

        public BrightGreenBulrushes(Serial serial)
            : base(serial)
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
    #endregion
    #region //Lilies
    public class BrightBlueLilies : Item
    {
        [Constructable]
        public BrightBlueLilies()
            : base(0xC8B)
        {
            Name = "Decorative Bright Blue Lilies";
            Hue = 0x5;
            Weight = 4.0;
        }

        public BrightBlueLilies(Serial serial)
            : base(serial)
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

    public class BlackLilies : Item
    {
        [Constructable]
        public BlackLilies()
            : base(0xC8B)
        {
            Name = "Decorative Black Lilies";
            Hue = 0x455;
            Weight = 4.0;
        }

        public BlackLilies(Serial serial)
            : base(serial)
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

    public class WhiteLilies : Item
    {
        [Constructable]
        public WhiteLilies()
            : base(0xC8B)
        {
            Name = "Decorative White Lilies";
            Hue = 0x481;
            Weight = 4.0;
        }

        public WhiteLilies(Serial serial)
            : base(serial)
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

    public class PinkLilies : Item
    {
        [Constructable]
        public PinkLilies()
            : base(0xC8B)
        {
            Name = "Decorative Pink Lilies";
            Hue = 0x483;
            Weight = 4.0;
        }

        public PinkLilies(Serial serial)
            : base(serial)
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

    public class MagentaLilies : Item
    {
        [Constructable]
        public MagentaLilies()
            : base(0xC8B)
        {
            Name = "Decorative Magenta Lilies";
            Hue = 0x486;
            Weight = 4.0;
        }

        public MagentaLilies(Serial serial)
            : base(serial)
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

    public class AquaLilies : Item
    {
        [Constructable]
        public AquaLilies()
            : base(0xC8B)
        {
            Name = "Decorative Aqua Lilies";
            Hue = 0x495;
            Weight = 4.0;
        }

        public AquaLilies(Serial serial)
            : base(serial)
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

    public class FireRedLilies : Item
    {
        [Constructable]
        public FireRedLilies()
            : base(0xC8B)
        {
            Name = "Fire Red Lilies";
            Hue = 0x489;
            Weight = 4.0;
        }

        public FireRedLilies(Serial serial)
            : base(serial)
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

    public class Lilies : Item
    {
        [Constructable]
        public Lilies()
            : base(0xC8B)
        {
            Name = "Decorative Lilies";
            Hue = 0x0;
            Weight = 4.0;
        }

        public Lilies(Serial serial)
            : base(serial)
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

    public class RedLilies : Item
    {
        [Constructable]
        public RedLilies()
            : base(0xC8B)
        {
            Name = "Decorative Red Lilies";
            Hue = 0x66D;
            Weight = 4.0;
        }

        public RedLilies(Serial serial)
            : base(serial)
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

    public class BlueLilies : Item
    {
        [Constructable]
        public BlueLilies()
            : base(0xC8B)
        {
            Name = "Decorative Blue Lilies";
            Hue = 0x53D;
            Weight = 4.0;
        }

        public BlueLilies(Serial serial)
            : base(serial)
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

    public class PurpleLilies : Item
    {
        [Constructable]
        public PurpleLilies()
            : base(0xC8B)
        {
            Name = "Decorative Purple Lilies";
            Hue = 0xD;
            Weight = 4.0;
        }

        public PurpleLilies(Serial serial)
            : base(serial)
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

    public class YellowLilies : Item
    {
        [Constructable]
        public YellowLilies()
            : base(0xC8B)
        {
            Name = "Decorative Yellow Lilies";
            Hue = 0x8A5;
            Weight = 4.0;
        }

        public YellowLilies(Serial serial)
            : base(serial)
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

    public class OrangeLilies : Item
    {
        [Constructable]
        public OrangeLilies()
            : base(0xC8B)
        {
            Name = "Decorative Orange Lilies";
            Hue = 0x46F;
            Weight = 4.0;
        }

        public OrangeLilies(Serial serial)
            : base(serial)
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

    public class GreenLilies : Item
    {
        [Constructable]
        public GreenLilies()
            : base(0xC8B)
        {
            Name = "Decorative Green Lilies";
            Hue = 0x59B;
            Weight = 4.0;
        }

        public GreenLilies(Serial serial)
            : base(serial)
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

    public class BrightRedLilies : Item
    {
        [Constructable]
        public BrightRedLilies()
            : base(0xC8B)
        {
            Name = "Decorative Bright Red Lilies";
            Hue = 0x21;
            Weight = 4.0;
        }

        public BrightRedLilies(Serial serial)
            : base(serial)
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

    public class BrightPurpleLilies : Item
    {
        [Constructable]
        public BrightPurpleLilies()
            : base(0xC8B)
        {
            Name = "Decorative Bright Purple Lilies";
            Hue = 0x10;
            Weight = 4.0;
        }

        public BrightPurpleLilies(Serial serial)
            : base(serial)
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

    public class BrightYellowLilies : Item
    {
        [Constructable]
        public BrightYellowLilies()
            : base(0xC8B)
        {
            Name = "Decorative Bright Yellow Lilies";
            Hue = 0x38;
            Weight = 4.0;
        }

        public BrightYellowLilies(Serial serial)
            : base(serial)
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

    public class BrightOrangeLilies : Item
    {
        [Constructable]
        public BrightOrangeLilies()
            : base(0xC8B)
        {
            Name = "Decorative Bright Orange Lilies";
            Hue = 0x2B;
            Weight = 4.0;
        }

        public BrightOrangeLilies(Serial serial)
            : base(serial)
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

    public class BrightGreenLilies : Item
    {
        [Constructable]
        public BrightGreenLilies()
            : base(0xC8B)
        {
            Name = "Decorative Bright Green Lilies";
            Hue = 0x42;
            Weight = 4.0;
        }

        public BrightGreenLilies(Serial serial)
            : base(serial)
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
    #endregion
    #region //PampasGrass
    public class BrightBluePampasGrass : Item
    {
        [Constructable]
        public BrightBluePampasGrass()
            : base(0xCA5)
        {
            Name = "Decorative Bright Blue Pampas Grass";
            Hue = 0x5;
            Weight = 4.0;
        }

        public BrightBluePampasGrass(Serial serial)
            : base(serial)
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

    public class BlackPampasGrass : Item
    {
        [Constructable]
        public BlackPampasGrass()
            : base(0xCA5)
        {
            Name = "Decorative Black Pampas Grass";
            Hue = 0x455;
            Weight = 4.0;
        }

        public BlackPampasGrass(Serial serial)
            : base(serial)
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

    public class WhitePampasGrass : Item
    {
        [Constructable]
        public WhitePampasGrass()
            : base(0xCA5)
        {
            Name = "Decorative White Pampas Grass";
            Hue = 0x481;
            Weight = 4.0;
        }

        public WhitePampasGrass(Serial serial)
            : base(serial)
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

    public class PinkPampasGrass : Item
    {
        [Constructable]
        public PinkPampasGrass()
            : base(0xCA5)
        {
            Name = "Decorative Pink Pampas Grass";
            Hue = 0x483;
            Weight = 4.0;
        }

        public PinkPampasGrass(Serial serial)
            : base(serial)
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

    public class MagentaPampasGrass : Item
    {
        [Constructable]
        public MagentaPampasGrass()
            : base(0xCA5)
        {
            Name = "Decorative Magenta Pampas Grass";
            Hue = 0x486;
            Weight = 4.0;
        }

        public MagentaPampasGrass(Serial serial)
            : base(serial)
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

    public class AquaPampasGrass : Item
    {
        [Constructable]
        public AquaPampasGrass()
            : base(0xCA5)
        {
            Name = "Decorative Aqua Pampas Grass";
            Hue = 0x495;
            Weight = 4.0;
        }

        public AquaPampasGrass(Serial serial)
            : base(serial)
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

    public class FireRedPampasGrass : Item
    {
        [Constructable]
        public FireRedPampasGrass()
            : base(0xCA5)
        {
            Name = "Fire Red Pampas Grass";
            Hue = 0x489;
            Weight = 4.0;
        }

        public FireRedPampasGrass(Serial serial)
            : base(serial)
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

    public class PampasGrass : Item
    {
        [Constructable]
        public PampasGrass()
            : base(0xCA5)
        {
            Name = "Decorative Pampas Grass";
            Hue = 0x0;
            Weight = 4.0;
        }

        public PampasGrass(Serial serial)
            : base(serial)
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

    public class RedPampasGrass : Item
    {
        [Constructable]
        public RedPampasGrass()
            : base(0xCA5)
        {
            Name = "Decorative Red Pampas Grass";
            Hue = 0x66D;
            Weight = 4.0;
        }

        public RedPampasGrass(Serial serial)
            : base(serial)
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

    public class BluePampasGrass : Item
    {
        [Constructable]
        public BluePampasGrass()
            : base(0xCA5)
        {
            Name = "Decorative Blue Pampas Grass";
            Hue = 0x53D;
            Weight = 4.0;
        }

        public BluePampasGrass(Serial serial)
            : base(serial)
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

    public class PurplePampasGrass : Item
    {
        [Constructable]
        public PurplePampasGrass()
            : base(0xCA5)
        {
            Name = "Decorative Purple Pampas Grass";
            Hue = 0xD;
            Weight = 4.0;
        }

        public PurplePampasGrass(Serial serial)
            : base(serial)
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

    public class YellowPampasGrass : Item
    {
        [Constructable]
        public YellowPampasGrass()
            : base(0xCA5)
        {
            Name = "Decorative Yellow Pampas Grass";
            Hue = 0x8A5;
            Weight = 4.0;
        }

        public YellowPampasGrass(Serial serial)
            : base(serial)
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

    public class OrangePampasGrass : Item
    {
        [Constructable]
        public OrangePampasGrass()
            : base(0xCA5)
        {
            Name = "Decorative Orange Pampas Grass";
            Hue = 0x46F;
            Weight = 4.0;
        }

        public OrangePampasGrass(Serial serial)
            : base(serial)
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

    public class GreenPampasGrass : Item
    {
        [Constructable]
        public GreenPampasGrass()
            : base(0xCA5)
        {
            Name = "Decorative Green Pampas Grass";
            Hue = 0x59B;
            Weight = 4.0;
        }

        public GreenPampasGrass(Serial serial)
            : base(serial)
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

    public class BrightRedPampasGrass : Item
    {
        [Constructable]
        public BrightRedPampasGrass()
            : base(0xCA5)
        {
            Name = "Decorative Bright Red Pampas Grass";
            Hue = 0x21;
            Weight = 4.0;
        }

        public BrightRedPampasGrass(Serial serial)
            : base(serial)
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

    public class BrightPurplePampasGrass : Item
    {
        [Constructable]
        public BrightPurplePampasGrass()
            : base(0xCA5)
        {
            Name = "Decorative Bright Purple Pampas Grass";
            Hue = 0x10;
            Weight = 4.0;
        }

        public BrightPurplePampasGrass(Serial serial)
            : base(serial)
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

    public class BrightYellowPampasGrass : Item
    {
        [Constructable]
        public BrightYellowPampasGrass()
            : base(0xCA5)
        {
            Name = "Decorative Bright Yellow Pampas Grass";
            Hue = 0x38;
            Weight = 4.0;
        }

        public BrightYellowPampasGrass(Serial serial)
            : base(serial)
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

    public class BrightOrangePampasGrass : Item
    {
        [Constructable]
        public BrightOrangePampasGrass()
            : base(0xCA5)
        {
            Name = "Decorative Bright Orange Pampas Grass";
            Hue = 0x2B;
            Weight = 4.0;
        }

        public BrightOrangePampasGrass(Serial serial)
            : base(serial)
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

    public class BrightGreenPampasGrass : Item
    {
        [Constructable]
        public BrightGreenPampasGrass()
            : base(0xCA5)
        {
            Name = "Decorative Bright Green Pampas Grass";
            Hue = 0x42;
            Weight = 4.0;
        }

        public BrightGreenPampasGrass(Serial serial)
            : base(serial)
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
    #endregion
    #region //Rushes
    public class BrightBlueRushes : Item
    {
        [Constructable]
        public BrightBlueRushes()
            : base(0xCA7)
        {
            Name = "Decorative Bright Blue Rushes";
            Hue = 0x5;
            Weight = 4.0;
        }

        public BrightBlueRushes(Serial serial)
            : base(serial)
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

    public class BlackRushes : Item
    {
        [Constructable]
        public BlackRushes()
            : base(0xCA7)
        {
            Name = "Decorative Black Rushes";
            Hue = 0x455;
            Weight = 4.0;
        }

        public BlackRushes(Serial serial)
            : base(serial)
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

    public class WhiteRushes : Item
    {
        [Constructable]
        public WhiteRushes()
            : base(0xCA7)
        {
            Name = "Decorative White Rushes";
            Hue = 0x481;
            Weight = 4.0;
        }

        public WhiteRushes(Serial serial)
            : base(serial)
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

    public class PinkRushes : Item
    {
        [Constructable]
        public PinkRushes()
            : base(0xCA7)
        {
            Name = "Decorative Pink Rushes";
            Hue = 0x483;
            Weight = 4.0;
        }

        public PinkRushes(Serial serial)
            : base(serial)
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

    public class MagentaRushes : Item
    {
        [Constructable]
        public MagentaRushes()
            : base(0xCA7)
        {
            Name = "Decorative Magenta Rushes";
            Hue = 0x486;
            Weight = 4.0;
        }

        public MagentaRushes(Serial serial)
            : base(serial)
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

    public class AquaRushes : Item
    {
        [Constructable]
        public AquaRushes()
            : base(0xCA7)
        {
            Name = "Decorative Aqua Rushes";
            Hue = 0x495;
            Weight = 4.0;
        }

        public AquaRushes(Serial serial)
            : base(serial)
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

    public class FireRedRushes : Item
    {
        [Constructable]
        public FireRedRushes()
            : base(0xCA7)
        {
            Name = "Fire Red Rushes";
            Hue = 0x489;
            Weight = 4.0;
        }

        public FireRedRushes(Serial serial)
            : base(serial)
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

    public class Rushes : Item
    {
        [Constructable]
        public Rushes()
            : base(0xCA7)
        {
            Name = "Decorative Rushes";
            Hue = 0x0;
            Weight = 4.0;
        }

        public Rushes(Serial serial)
            : base(serial)
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

    public class RedRushes : Item
    {
        [Constructable]
        public RedRushes()
            : base(0xCA7)
        {
            Name = "Decorative Red Rushes";
            Hue = 0x66D;
            Weight = 4.0;
        }

        public RedRushes(Serial serial)
            : base(serial)
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

    public class BlueRushes : Item
    {
        [Constructable]
        public BlueRushes()
            : base(0xCA7)
        {
            Name = "Decorative Blue Rushes";
            Hue = 0x53D;
            Weight = 4.0;
        }

        public BlueRushes(Serial serial)
            : base(serial)
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

    public class PurpleRushes : Item
    {
        [Constructable]
        public PurpleRushes()
            : base(0xCA7)
        {
            Name = "Decorative Purple Rushes";
            Hue = 0xD;
            Weight = 4.0;
        }

        public PurpleRushes(Serial serial)
            : base(serial)
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

    public class YellowRushes : Item
    {
        [Constructable]
        public YellowRushes()
            : base(0xCA7)
        {
            Name = "Decorative Yellow Rushes";
            Hue = 0x8A5;
            Weight = 4.0;
        }

        public YellowRushes(Serial serial)
            : base(serial)
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

    public class OrangeRushes : Item
    {
        [Constructable]
        public OrangeRushes()
            : base(0xCA7)
        {
            Name = "Decorative Orange Rushes";
            Hue = 0x46F;
            Weight = 4.0;
        }

        public OrangeRushes(Serial serial)
            : base(serial)
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

    public class GreenRushes : Item
    {
        [Constructable]
        public GreenRushes()
            : base(0xCA7)
        {
            Name = "Decorative Green Rushes";
            Hue = 0x59B;
            Weight = 4.0;
        }

        public GreenRushes(Serial serial)
            : base(serial)
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

    public class BrightRedRushes : Item
    {
        [Constructable]
        public BrightRedRushes()
            : base(0xCA7)
        {
            Name = "Decorative Bright Red Rushes";
            Hue = 0x21;
            Weight = 4.0;
        }

        public BrightRedRushes(Serial serial)
            : base(serial)
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

    public class BrightPurpleRushes : Item
    {
        [Constructable]
        public BrightPurpleRushes()
            : base(0xCA7)
        {
            Name = "Decorative Bright Purple Rushes";
            Hue = 0x10;
            Weight = 4.0;
        }

        public BrightPurpleRushes(Serial serial)
            : base(serial)
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

    public class BrightYellowRushes : Item
    {
        [Constructable]
        public BrightYellowRushes()
            : base(0xCA7)
        {
            Name = "Decorative Bright Yellow Rushes";
            Hue = 0x38;
            Weight = 4.0;
        }

        public BrightYellowRushes(Serial serial)
            : base(serial)
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

    public class BrightOrangeRushes : Item
    {
        [Constructable]
        public BrightOrangeRushes()
            : base(0xCA7)
        {
            Name = "Decorative Bright Orange Rushes";
            Hue = 0x2B;
            Weight = 4.0;
        }

        public BrightOrangeRushes(Serial serial)
            : base(serial)
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

    public class BrightGreenRushes : Item
    {
        [Constructable]
        public BrightGreenRushes()
            : base(0xCA7)
        {
            Name = "Decorative Bright Green Rushes";
            Hue = 0x42;
            Weight = 4.0;
        }

        public BrightGreenRushes(Serial serial)
            : base(serial)
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
    #endregion
    #region //ElephantEarPlant
    public class BrightBlueElephantEarPlant : Item
    {
        [Constructable]
        public BrightBlueElephantEarPlant()
            : base(0xC97)
        {
            Name = "Decorative Bright Blue Elephant Ear Plant";
            Hue = 0x5;
            Weight = 4.0;
        }

        public BrightBlueElephantEarPlant(Serial serial)
            : base(serial)
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

    public class BlackElephantEarPlant : Item
    {
        [Constructable]
        public BlackElephantEarPlant()
            : base(0xC97)
        {
            Name = "Decorative Black Elephant Ear Plant";
            Hue = 0x455;
            Weight = 4.0;
        }

        public BlackElephantEarPlant(Serial serial)
            : base(serial)
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

    public class WhiteElephantEarPlant : Item
    {
        [Constructable]
        public WhiteElephantEarPlant()
            : base(0xC97)
        {
            Name = "Decorative White Elephant Ear Plant";
            Hue = 0x481;
            Weight = 4.0;
        }

        public WhiteElephantEarPlant(Serial serial)
            : base(serial)
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

    public class PinkElephantEarPlant : Item
    {
        [Constructable]
        public PinkElephantEarPlant()
            : base(0xC97)
        {
            Name = "Decorative Pink Elephant Ear Plant";
            Hue = 0x483;
            Weight = 4.0;
        }

        public PinkElephantEarPlant(Serial serial)
            : base(serial)
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

    public class MagentaElephantEarPlant : Item
    {
        [Constructable]
        public MagentaElephantEarPlant()
            : base(0xC97)
        {
            Name = "Decorative Magenta Elephant Ear Plant";
            Hue = 0x486;
            Weight = 4.0;
        }

        public MagentaElephantEarPlant(Serial serial)
            : base(serial)
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

    public class AquaElephantEarPlant : Item
    {
        [Constructable]
        public AquaElephantEarPlant()
            : base(0xC97)
        {
            Name = "Decorative Aqua Elephant Ear Plant";
            Hue = 0x495;
            Weight = 4.0;
        }

        public AquaElephantEarPlant(Serial serial)
            : base(serial)
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

    public class FireRedElephantEarPlant : Item
    {
        [Constructable]
        public FireRedElephantEarPlant()
            : base(0xC97)
        {
            Name = "Fire Red Elephant Ear Plant";
            Hue = 0x489;
            Weight = 4.0;
        }

        public FireRedElephantEarPlant(Serial serial)
            : base(serial)
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

    public class ElephantEarPlant : Item
    {
        [Constructable]
        public ElephantEarPlant()
            : base(0xC97)
        {
            Name = "Decorative Elephant Ear Plant";
            Hue = 0x0;
            Weight = 4.0;
        }

        public ElephantEarPlant(Serial serial)
            : base(serial)
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

    public class RedElephantEarPlant : Item
    {
        [Constructable]
        public RedElephantEarPlant()
            : base(0xC97)
        {
            Name = "Decorative Red Elephant Ear Plant";
            Hue = 0x66D;
            Weight = 4.0;
        }

        public RedElephantEarPlant(Serial serial)
            : base(serial)
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

    public class BlueElephantEarPlant : Item
    {
        [Constructable]
        public BlueElephantEarPlant()
            : base(0xC97)
        {
            Name = "Decorative Blue Elephant Ear Plant";
            Hue = 0x53D;
            Weight = 4.0;
        }

        public BlueElephantEarPlant(Serial serial)
            : base(serial)
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

    public class PurpleElephantEarPlant : Item
    {
        [Constructable]
        public PurpleElephantEarPlant()
            : base(0xC97)
        {
            Name = "Decorative Purple Elephant Ear Plant";
            Hue = 0xD;
            Weight = 4.0;
        }

        public PurpleElephantEarPlant(Serial serial)
            : base(serial)
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

    public class YellowElephantEarPlant : Item
    {
        [Constructable]
        public YellowElephantEarPlant()
            : base(0xC97)
        {
            Name = "Decorative Yellow Elephant Ear Plant";
            Hue = 0x8A5;
            Weight = 4.0;
        }

        public YellowElephantEarPlant(Serial serial)
            : base(serial)
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

    public class OrangeElephantEarPlant : Item
    {
        [Constructable]
        public OrangeElephantEarPlant()
            : base(0xC97)
        {
            Name = "Decorative Orange Elephant Ear Plant";
            Hue = 0x46F;
            Weight = 4.0;
        }

        public OrangeElephantEarPlant(Serial serial)
            : base(serial)
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

    public class GreenElephantEarPlant : Item
    {
        [Constructable]
        public GreenElephantEarPlant()
            : base(0xC97)
        {
            Name = "Decorative Green Elephant Ear Plant";
            Hue = 0x59B;
            Weight = 4.0;
        }

        public GreenElephantEarPlant(Serial serial)
            : base(serial)
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

    public class BrightRedElephantEarPlant : Item
    {
        [Constructable]
        public BrightRedElephantEarPlant()
            : base(0xC97)
        {
            Name = "Decorative Bright Red Elephant Ear Plant";
            Hue = 0x21;
            Weight = 4.0;
        }

        public BrightRedElephantEarPlant(Serial serial)
            : base(serial)
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

    public class BrightPurpleElephantEarPlant : Item
    {
        [Constructable]
        public BrightPurpleElephantEarPlant()
            : base(0xC97)
        {
            Name = "Decorative Bright Purple Elephant Ear Plant";
            Hue = 0x10;
            Weight = 4.0;
        }

        public BrightPurpleElephantEarPlant(Serial serial)
            : base(serial)
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

    public class BrightYellowElephantEarPlant : Item
    {
        [Constructable]
        public BrightYellowElephantEarPlant()
            : base(0xC97)
        {
            Name = "Decorative Bright Yellow Elephant Ear Plant";
            Hue = 0x38;
            Weight = 4.0;
        }

        public BrightYellowElephantEarPlant(Serial serial)
            : base(serial)
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

    public class BrightOrangeElephantEarPlant : Item
    {
        [Constructable]
        public BrightOrangeElephantEarPlant()
            : base(0xC97)
        {
            Name = "Decorative Bright Orange Elephant Ear Plant";
            Hue = 0x2B;
            Weight = 4.0;
        }

        public BrightOrangeElephantEarPlant(Serial serial)
            : base(serial)
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

    public class BrightGreenElephantEarPlant : Item
    {
        [Constructable]
        public BrightGreenElephantEarPlant()
            : base(0xC97)
        {
            Name = "Decorative Bright Green Elephant Ear Plant";
            Hue = 0x42;
            Weight = 4.0;
        }

        public BrightGreenElephantEarPlant(Serial serial)
            : base(serial)
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
    #endregion
    #region //Fern
    public class BrightBlueFern : Item
    {
        [Constructable]
        public BrightBlueFern()
            : base(0xC9F)
        {
            Name = "Decorative Bright Blue Fern";
            Hue = 0x5;
            Weight = 4.0;
        }

        public BrightBlueFern(Serial serial)
            : base(serial)
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

    public class BlackFern : Item
    {
        [Constructable]
        public BlackFern()
            : base(0xC9F)
        {
            Name = "Decorative Black Fern";
            Hue = 0x455;
            Weight = 4.0;
        }

        public BlackFern(Serial serial)
            : base(serial)
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

    public class WhiteFern : Item
    {
        [Constructable]
        public WhiteFern()
            : base(0xC9F)
        {
            Name = "Decorative White Fern";
            Hue = 0x481;
            Weight = 4.0;
        }

        public WhiteFern(Serial serial)
            : base(serial)
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

    public class PinkFern : Item
    {
        [Constructable]
        public PinkFern()
            : base(0xC9F)
        {
            Name = "Decorative Pink Fern";
            Hue = 0x483;
            Weight = 4.0;
        }

        public PinkFern(Serial serial)
            : base(serial)
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

    public class MagentaFern : Item
    {
        [Constructable]
        public MagentaFern()
            : base(0xC9F)
        {
            Name = "Decorative Magenta Fern";
            Hue = 0x486;
            Weight = 4.0;
        }

        public MagentaFern(Serial serial)
            : base(serial)
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

    public class AquaFern : Item
    {
        [Constructable]
        public AquaFern()
            : base(0xC9F)
        {
            Name = "Decorative Aqua Fern";
            Hue = 0x495;
            Weight = 4.0;
        }

        public AquaFern(Serial serial)
            : base(serial)
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

    public class FireRedFern : Item
    {
        [Constructable]
        public FireRedFern()
            : base(0xC9F)
        {
            Name = "Fire Red Fern";
            Hue = 0x489;
            Weight = 4.0;
        }

        public FireRedFern(Serial serial)
            : base(serial)
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

    public class Fern : Item
    {
        [Constructable]
        public Fern()
            : base(0xC9F)
        {
            Name = "Decorative Fern";
            Hue = 0x0;
            Weight = 4.0;
        }

        public Fern(Serial serial)
            : base(serial)
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

    public class RedFern : Item
    {
        [Constructable]
        public RedFern()
            : base(0xC9F)
        {
            Name = "Decorative Red Fern";
            Hue = 0x66D;
            Weight = 4.0;
        }

        public RedFern(Serial serial)
            : base(serial)
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

    public class BlueFern : Item
    {
        [Constructable]
        public BlueFern()
            : base(0xC9F)
        {
            Name = "Decorative Blue Fern";
            Hue = 0x53D;
            Weight = 4.0;
        }

        public BlueFern(Serial serial)
            : base(serial)
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

    public class PurpleFern : Item
    {
        [Constructable]
        public PurpleFern()
            : base(0xC9F)
        {
            Name = "Decorative Purple Fern";
            Hue = 0xD;
            Weight = 4.0;
        }

        public PurpleFern(Serial serial)
            : base(serial)
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

    public class YellowFern : Item
    {
        [Constructable]
        public YellowFern()
            : base(0xC9F)
        {
            Name = "Decorative Yellow Fern";
            Hue = 0x8A5;
            Weight = 4.0;
        }

        public YellowFern(Serial serial)
            : base(serial)
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

    public class OrangeFern : Item
    {
        [Constructable]
        public OrangeFern()
            : base(0xC9F)
        {
            Name = "Decorative Orange Fern";
            Hue = 0x46F;
            Weight = 4.0;
        }

        public OrangeFern(Serial serial)
            : base(serial)
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

    public class GreenFern : Item
    {
        [Constructable]
        public GreenFern()
            : base(0xC9F)
        {
            Name = "Decorative Green Fern";
            Hue = 0x59B;
            Weight = 4.0;
        }

        public GreenFern(Serial serial)
            : base(serial)
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

    public class BrightRedFern : Item
    {
        [Constructable]
        public BrightRedFern()
            : base(0xC9F)
        {
            Name = "Decorative Bright Red Fern";
            Hue = 0x21;
            Weight = 4.0;
        }

        public BrightRedFern(Serial serial)
            : base(serial)
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

    public class BrightPurpleFern : Item
    {
        [Constructable]
        public BrightPurpleFern()
            : base(0xC9F)
        {
            Name = "Decorative Bright Purple Fern";
            Hue = 0x10;
            Weight = 4.0;
        }

        public BrightPurpleFern(Serial serial)
            : base(serial)
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

    public class BrightYellowFern : Item
    {
        [Constructable]
        public BrightYellowFern()
            : base(0xC9F)
        {
            Name = "Decorative Bright Yellow Fern";
            Hue = 0x38;
            Weight = 4.0;
        }

        public BrightYellowFern(Serial serial)
            : base(serial)
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

    public class BrightOrangeFern : Item
    {
        [Constructable]
        public BrightOrangeFern()
            : base(0xC9F)
        {
            Name = "Decorative Bright Orange Fern";
            Hue = 0x2B;
            Weight = 4.0;
        }

        public BrightOrangeFern(Serial serial)
            : base(serial)
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

    public class BrightGreenFern : Item
    {
        [Constructable]
        public BrightGreenFern()
            : base(0xC9F)
        {
            Name = "Decorative Bright Green Fern";
            Hue = 0x42;
            Weight = 4.0;
        }

        public BrightGreenFern(Serial serial)
            : base(serial)
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
    #endregion
    #region //PonytailPalm
    public class BrightBluePonytailPalm : Item
    {
        [Constructable]
        public BrightBluePonytailPalm()
            : base(0xCA6)
        {
            Name = "Decorative Bright Blue Ponytail Palm";
            Hue = 0x5;
            Weight = 4.0;
        }

        public BrightBluePonytailPalm(Serial serial)
            : base(serial)
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

    public class BlackPonytailPalm : Item
    {
        [Constructable]
        public BlackPonytailPalm()
            : base(0xCA6)
        {
            Name = "Decorative Black Ponytail Palm";
            Hue = 0x455;
            Weight = 4.0;
        }

        public BlackPonytailPalm(Serial serial)
            : base(serial)
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

    public class WhitePonytailPalm : Item
    {
        [Constructable]
        public WhitePonytailPalm()
            : base(0xCA6)
        {
            Name = "Decorative White Ponytail Palm";
            Hue = 0x481;
            Weight = 4.0;
        }

        public WhitePonytailPalm(Serial serial)
            : base(serial)
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

    public class PinkPonytailPalm : Item
    {
        [Constructable]
        public PinkPonytailPalm()
            : base(0xCA6)
        {
            Name = "Decorative Pink Ponytail Palm";
            Hue = 0x483;
            Weight = 4.0;
        }

        public PinkPonytailPalm(Serial serial)
            : base(serial)
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

    public class MagentaPonytailPalm : Item
    {
        [Constructable]
        public MagentaPonytailPalm()
            : base(0xCA6)
        {
            Name = "Decorative Magenta Ponytail Palm";
            Hue = 0x486;
            Weight = 4.0;
        }

        public MagentaPonytailPalm(Serial serial)
            : base(serial)
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

    public class AquaPonytailPalm : Item
    {
        [Constructable]
        public AquaPonytailPalm()
            : base(0xCA6)
        {
            Name = "Decorative Aqua Ponytail Palm";
            Hue = 0x495;
            Weight = 4.0;
        }

        public AquaPonytailPalm(Serial serial)
            : base(serial)
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

    public class FireRedPonytailPalm : Item
    {
        [Constructable]
        public FireRedPonytailPalm()
            : base(0xCA6)
        {
            Name = "Fire Red Ponytail Palm";
            Hue = 0x489;
            Weight = 4.0;
        }

        public FireRedPonytailPalm(Serial serial)
            : base(serial)
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

    public class PonytailPalm : Item
    {
        [Constructable]
        public PonytailPalm()
            : base(0xCA6)
        {
            Name = "Decorative Ponytail Palm";
            Hue = 0x0;
            Weight = 4.0;
        }

        public PonytailPalm(Serial serial)
            : base(serial)
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

    public class RedPonytailPalm : Item
    {
        [Constructable]
        public RedPonytailPalm()
            : base(0xCA6)
        {
            Name = "Decorative Red Ponytail Palm";
            Hue = 0x66D;
            Weight = 4.0;
        }

        public RedPonytailPalm(Serial serial)
            : base(serial)
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

    public class BluePonytailPalm : Item
    {
        [Constructable]
        public BluePonytailPalm()
            : base(0xCA6)
        {
            Name = "Decorative Blue Ponytail Palm";
            Hue = 0x53D;
            Weight = 4.0;
        }

        public BluePonytailPalm(Serial serial)
            : base(serial)
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

    public class PurplePonytailPalm : Item
    {
        [Constructable]
        public PurplePonytailPalm()
            : base(0xCA6)
        {
            Name = "Decorative Purple Ponytail Palm";
            Hue = 0xD;
            Weight = 4.0;
        }

        public PurplePonytailPalm(Serial serial)
            : base(serial)
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

    public class YellowPonytailPalm : Item
    {
        [Constructable]
        public YellowPonytailPalm()
            : base(0xCA6)
        {
            Name = "Decorative Yellow Ponytail Palm";
            Hue = 0x8A5;
            Weight = 4.0;
        }

        public YellowPonytailPalm(Serial serial)
            : base(serial)
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

    public class OrangePonytailPalm : Item
    {
        [Constructable]
        public OrangePonytailPalm()
            : base(0xCA6)
        {
            Name = "Decorative Orange Ponytail Palm";
            Hue = 0x46F;
            Weight = 4.0;
        }

        public OrangePonytailPalm(Serial serial)
            : base(serial)
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

    public class GreenPonytailPalm : Item
    {
        [Constructable]
        public GreenPonytailPalm()
            : base(0xCA6)
        {
            Name = "Decorative Green Ponytail Palm";
            Hue = 0x59B;
            Weight = 4.0;
        }

        public GreenPonytailPalm(Serial serial)
            : base(serial)
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

    public class BrightRedPonytailPalm : Item
    {
        [Constructable]
        public BrightRedPonytailPalm()
            : base(0xCA6)
        {
            Name = "Decorative Bright Red Ponytail Palm";
            Hue = 0x21;
            Weight = 4.0;
        }

        public BrightRedPonytailPalm(Serial serial)
            : base(serial)
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

    public class BrightPurplePonytailPalm : Item
    {
        [Constructable]
        public BrightPurplePonytailPalm()
            : base(0xCA6)
        {
            Name = "Decorative Bright Purple Ponytail Palm";
            Hue = 0x10;
            Weight = 4.0;
        }

        public BrightPurplePonytailPalm(Serial serial)
            : base(serial)
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

    public class BrightYellowPonytailPalm : Item
    {
        [Constructable]
        public BrightYellowPonytailPalm()
            : base(0xCA6)
        {
            Name = "Decorative Bright Yellow Ponytail Palm";
            Hue = 0x38;
            Weight = 4.0;
        }

        public BrightYellowPonytailPalm(Serial serial)
            : base(serial)
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

    public class BrightOrangePonytailPalm : Item
    {
        [Constructable]
        public BrightOrangePonytailPalm()
            : base(0xCA6)
        {
            Name = "Decorative Bright Orange Ponytail Palm";
            Hue = 0x2B;
            Weight = 4.0;
        }

        public BrightOrangePonytailPalm(Serial serial)
            : base(serial)
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

    public class BrightGreenPonytailPalm : Item
    {
        [Constructable]
        public BrightGreenPonytailPalm()
            : base(0xCA6)
        {
            Name = "Decorative Bright Green Ponytail Palm";
            Hue = 0x42;
            Weight = 4.0;
        }

        public BrightGreenPonytailPalm(Serial serial)
            : base(serial)
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
    #endregion
    #region //SmallPalm
    public class BrightBlueSmallPalm : Item
    {
        [Constructable]
        public BrightBlueSmallPalm()
            : base(0xC9C)
        {
            Name = "Decorative Bright Blue Small Palm";
            Hue = 0x5;
            Weight = 4.0;
        }

        public BrightBlueSmallPalm(Serial serial)
            : base(serial)
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

    public class BlackSmallPalm : Item
    {
        [Constructable]
        public BlackSmallPalm()
            : base(0xC9C)
        {
            Name = "Decorative Black Small Palm";
            Hue = 0x455;
            Weight = 4.0;
        }

        public BlackSmallPalm(Serial serial)
            : base(serial)
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

    public class WhiteSmallPalm : Item
    {
        [Constructable]
        public WhiteSmallPalm()
            : base(0xC9C)
        {
            Name = "Decorative White Small Palm";
            Hue = 0x481;
            Weight = 4.0;
        }

        public WhiteSmallPalm(Serial serial)
            : base(serial)
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

    public class PinkSmallPalm : Item
    {
        [Constructable]
        public PinkSmallPalm()
            : base(0xC9C)
        {
            Name = "Decorative Pink Small Palm";
            Hue = 0x483;
            Weight = 4.0;
        }

        public PinkSmallPalm(Serial serial)
            : base(serial)
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

    public class MagentaSmallPalm : Item
    {
        [Constructable]
        public MagentaSmallPalm()
            : base(0xC9C)
        {
            Name = "Decorative Magenta Small Palm";
            Hue = 0x486;
            Weight = 4.0;
        }

        public MagentaSmallPalm(Serial serial)
            : base(serial)
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

    public class AquaSmallPalm : Item
    {
        [Constructable]
        public AquaSmallPalm()
            : base(0xC9C)
        {
            Name = "Decorative Aqua Small Palm";
            Hue = 0x495;
            Weight = 4.0;
        }

        public AquaSmallPalm(Serial serial)
            : base(serial)
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

    public class FireRedSmallPalm : Item
    {
        [Constructable]
        public FireRedSmallPalm()
            : base(0xC9C)
        {
            Name = "Fire Red Small Palm";
            Hue = 0x489;
            Weight = 4.0;
        }

        public FireRedSmallPalm(Serial serial)
            : base(serial)
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

    public class SmallPalm : Item
    {
        [Constructable]
        public SmallPalm()
            : base(0xC9C)
        {
            Name = "Decorative Small Palm";
            Hue = 0x0;
            Weight = 4.0;
        }

        public SmallPalm(Serial serial)
            : base(serial)
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

    public class RedSmallPalm : Item
    {
        [Constructable]
        public RedSmallPalm()
            : base(0xC9C)
        {
            Name = "Decorative Red Small Palm";
            Hue = 0x66D;
            Weight = 4.0;
        }

        public RedSmallPalm(Serial serial)
            : base(serial)
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

    public class BlueSmallPalm : Item
    {
        [Constructable]
        public BlueSmallPalm()
            : base(0xC9C)
        {
            Name = "Decorative Blue Small Palm";
            Hue = 0x53D;
            Weight = 4.0;
        }

        public BlueSmallPalm(Serial serial)
            : base(serial)
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

    public class PurpleSmallPalm : Item
    {
        [Constructable]
        public PurpleSmallPalm()
            : base(0xC9C)
        {
            Name = "Decorative Purple Small Palm";
            Hue = 0xD;
            Weight = 4.0;
        }

        public PurpleSmallPalm(Serial serial)
            : base(serial)
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

    public class YellowSmallPalm : Item
    {
        [Constructable]
        public YellowSmallPalm()
            : base(0xC9C)
        {
            Name = "Decorative Yellow Small Palm";
            Hue = 0x8A5;
            Weight = 4.0;
        }

        public YellowSmallPalm(Serial serial)
            : base(serial)
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

    public class OrangeSmallPalm : Item
    {
        [Constructable]
        public OrangeSmallPalm()
            : base(0xC9C)
        {
            Name = "Decorative Orange Small Palm";
            Hue = 0x46F;
            Weight = 4.0;
        }

        public OrangeSmallPalm(Serial serial)
            : base(serial)
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

    public class GreenSmallPalm : Item
    {
        [Constructable]
        public GreenSmallPalm()
            : base(0xC9C)
        {
            Name = "Decorative Green Small Palm";
            Hue = 0x59B;
            Weight = 4.0;
        }

        public GreenSmallPalm(Serial serial)
            : base(serial)
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

    public class BrightRedSmallPalm : Item
    {
        [Constructable]
        public BrightRedSmallPalm()
            : base(0xC9C)
        {
            Name = "Decorative Bright Red Small Palm";
            Hue = 0x21;
            Weight = 4.0;
        }

        public BrightRedSmallPalm(Serial serial)
            : base(serial)
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

    public class BrightPurpleSmallPalm : Item
    {
        [Constructable]
        public BrightPurpleSmallPalm()
            : base(0xC9C)
        {
            Name = "Decorative Bright Purple Small Palm";
            Hue = 0x10;
            Weight = 4.0;
        }

        public BrightPurpleSmallPalm(Serial serial)
            : base(serial)
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

    public class BrightYellowSmallPalm : Item
    {
        [Constructable]
        public BrightYellowSmallPalm()
            : base(0xC9C)
        {
            Name = "Decorative Bright Yellow Small Palm";
            Hue = 0x38;
            Weight = 4.0;
        }

        public BrightYellowSmallPalm(Serial serial)
            : base(serial)
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

    public class BrightOrangeSmallPalm : Item
    {
        [Constructable]
        public BrightOrangeSmallPalm()
            : base(0xC9C)
        {
            Name = "Decorative Bright Orange Small Palm";
            Hue = 0x2B;
            Weight = 4.0;
        }

        public BrightOrangeSmallPalm(Serial serial)
            : base(serial)
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

    public class BrightGreenSmallPalm : Item
    {
        [Constructable]
        public BrightGreenSmallPalm()
            : base(0xC9C)
        {
            Name = "Decorative Bright Green Small Palm";
            Hue = 0x42;
            Weight = 4.0;
        }

        public BrightGreenSmallPalm(Serial serial)
            : base(serial)
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
    #endregion
    #region //CenturyPlant
    public class BrightBlueCenturyPlant : Item
    {
        [Constructable]
        public BrightBlueCenturyPlant()
            : base(0xD31)
        {
            Name = "Decorative Bright Blue Century Plant";
            Hue = 0x5;
            Weight = 4.0;
        }

        public BrightBlueCenturyPlant(Serial serial)
            : base(serial)
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

    public class BlackCenturyPlant : Item
    {
        [Constructable]
        public BlackCenturyPlant()
            : base(0xD31)
        {
            Name = "Decorative Black Century Plant";
            Hue = 0x455;
            Weight = 4.0;
        }

        public BlackCenturyPlant(Serial serial)
            : base(serial)
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

    public class WhiteCenturyPlant : Item
    {
        [Constructable]
        public WhiteCenturyPlant()
            : base(0xD31)
        {
            Name = "Decorative White Century Plant";
            Hue = 0x481;
            Weight = 4.0;
        }

        public WhiteCenturyPlant(Serial serial)
            : base(serial)
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

    public class PinkCenturyPlant : Item
    {
        [Constructable]
        public PinkCenturyPlant()
            : base(0xD31)
        {
            Name = "Decorative Pink Century Plant";
            Hue = 0x483;
            Weight = 4.0;
        }

        public PinkCenturyPlant(Serial serial)
            : base(serial)
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

    public class MagentaCenturyPlant : Item
    {
        [Constructable]
        public MagentaCenturyPlant()
            : base(0xD31)
        {
            Name = "Decorative Magenta Century Plant";
            Hue = 0x486;
            Weight = 4.0;
        }

        public MagentaCenturyPlant(Serial serial)
            : base(serial)
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

    public class AquaCenturyPlant : Item
    {
        [Constructable]
        public AquaCenturyPlant()
            : base(0xD31)
        {
            Name = "Decorative Aqua Century Plant";
            Hue = 0x495;
            Weight = 4.0;
        }

        public AquaCenturyPlant(Serial serial)
            : base(serial)
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

    public class FireRedCenturyPlant : Item
    {
        [Constructable]
        public FireRedCenturyPlant()
            : base(0xD31)
        {
            Name = "Fire Red Century Plant";
            Hue = 0x489;
            Weight = 4.0;
        }

        public FireRedCenturyPlant(Serial serial)
            : base(serial)
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

    public class CenturyPlant : Item
    {
        [Constructable]
        public CenturyPlant()
            : base(0xD31)
        {
            Name = "Decorative Century Plant";
            Hue = 0x0;
            Weight = 4.0;
        }

        public CenturyPlant(Serial serial)
            : base(serial)
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

    public class RedCenturyPlant : Item
    {
        [Constructable]
        public RedCenturyPlant()
            : base(0xD31)
        {
            Name = "Decorative Red Century Plant";
            Hue = 0x66D;
            Weight = 4.0;
        }

        public RedCenturyPlant(Serial serial)
            : base(serial)
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

    public class BlueCenturyPlant : Item
    {
        [Constructable]
        public BlueCenturyPlant()
            : base(0xD31)
        {
            Name = "Decorative Blue Century Plant";
            Hue = 0x53D;
            Weight = 4.0;
        }

        public BlueCenturyPlant(Serial serial)
            : base(serial)
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

    public class PurpleCenturyPlant : Item
    {
        [Constructable]
        public PurpleCenturyPlant()
            : base(0xD31)
        {
            Name = "Decorative Purple Century Plant";
            Hue = 0xD;
            Weight = 4.0;
        }

        public PurpleCenturyPlant(Serial serial)
            : base(serial)
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

    public class YellowCenturyPlant : Item
    {
        [Constructable]
        public YellowCenturyPlant()
            : base(0xD31)
        {
            Name = "Decorative Yellow Century Plant";
            Hue = 0x8A5;
            Weight = 4.0;
        }

        public YellowCenturyPlant(Serial serial)
            : base(serial)
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

    public class OrangeCenturyPlant : Item
    {
        [Constructable]
        public OrangeCenturyPlant()
            : base(0xD31)
        {
            Name = "Decorative Orange Century Plant";
            Hue = 0x46F;
            Weight = 4.0;
        }

        public OrangeCenturyPlant(Serial serial)
            : base(serial)
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

    public class GreenCenturyPlant : Item
    {
        [Constructable]
        public GreenCenturyPlant()
            : base(0xD31)
        {
            Name = "Decorative Green Century Plant";
            Hue = 0x59B;
            Weight = 4.0;
        }

        public GreenCenturyPlant(Serial serial)
            : base(serial)
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

    public class BrightRedCenturyPlant : Item
    {
        [Constructable]
        public BrightRedCenturyPlant()
            : base(0xD31)
        {
            Name = "Decorative Bright Red Century Plant";
            Hue = 0x21;
            Weight = 4.0;
        }

        public BrightRedCenturyPlant(Serial serial)
            : base(serial)
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

    public class BrightPurpleCenturyPlant : Item
    {
        [Constructable]
        public BrightPurpleCenturyPlant()
            : base(0xD31)
        {
            Name = "Decorative Bright Purple Century Plant";
            Hue = 0x10;
            Weight = 4.0;
        }

        public BrightPurpleCenturyPlant(Serial serial)
            : base(serial)
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

    public class BrightYellowCenturyPlant : Item
    {
        [Constructable]
        public BrightYellowCenturyPlant()
            : base(0xD31)
        {
            Name = "Decorative Bright Yellow Century Plant";
            Hue = 0x38;
            Weight = 4.0;
        }

        public BrightYellowCenturyPlant(Serial serial)
            : base(serial)
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

    public class BrightOrangeCenturyPlant : Item
    {
        [Constructable]
        public BrightOrangeCenturyPlant()
            : base(0xD31)
        {
            Name = "Decorative Bright Orange Century Plant";
            Hue = 0x2B;
            Weight = 4.0;
        }

        public BrightOrangeCenturyPlant(Serial serial)
            : base(serial)
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

    public class BrightGreenCenturyPlant : Item
    {
        [Constructable]
        public BrightGreenCenturyPlant()
            : base(0xD31)
        {
            Name = "Decorative Bright Green Century Plant";
            Hue = 0x42;
            Weight = 4.0;
        }

        public BrightGreenCenturyPlant(Serial serial)
            : base(serial)
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
    #endregion
    #region //WaterPlant
    public class BrightBlueWaterPlant : Item
    {
        [Constructable]
        public BrightBlueWaterPlant()
            : base(0xD04)
        {
            Name = "Decorative Bright Blue Water Plant";
            Hue = 0x5;
            Weight = 4.0;
        }

        public BrightBlueWaterPlant(Serial serial)
            : base(serial)
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

    public class BlackWaterPlant : Item
    {
        [Constructable]
        public BlackWaterPlant()
            : base(0xD04)
        {
            Name = "Decorative Black Water Plant";
            Hue = 0x455;
            Weight = 4.0;
        }

        public BlackWaterPlant(Serial serial)
            : base(serial)
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

    public class WhiteWaterPlant : Item
    {
        [Constructable]
        public WhiteWaterPlant()
            : base(0xD04)
        {
            Name = "Decorative White Water Plant";
            Hue = 0x481;
            Weight = 4.0;
        }

        public WhiteWaterPlant(Serial serial)
            : base(serial)
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

    public class PinkWaterPlant : Item
    {
        [Constructable]
        public PinkWaterPlant()
            : base(0xD04)
        {
            Name = "Decorative Pink Water Plant";
            Hue = 0x483;
            Weight = 4.0;
        }

        public PinkWaterPlant(Serial serial)
            : base(serial)
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

    public class MagentaWaterPlant : Item
    {
        [Constructable]
        public MagentaWaterPlant()
            : base(0xD04)
        {
            Name = "Decorative Magenta Water Plant";
            Hue = 0x486;
            Weight = 4.0;
        }

        public MagentaWaterPlant(Serial serial)
            : base(serial)
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

    public class AquaWaterPlant : Item
    {
        [Constructable]
        public AquaWaterPlant()
            : base(0xD04)
        {
            Name = "Decorative Aqua Water Plant";
            Hue = 0x495;
            Weight = 4.0;
        }

        public AquaWaterPlant(Serial serial)
            : base(serial)
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

    public class FireRedWaterPlant : Item
    {
        [Constructable]
        public FireRedWaterPlant()
            : base(0xD04)
        {
            Name = "Fire Red Water Plant";
            Hue = 0x489;
            Weight = 4.0;
        }

        public FireRedWaterPlant(Serial serial)
            : base(serial)
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

    public class WaterPlant : Item
    {
        [Constructable]
        public WaterPlant()
            : base(0xD04)
        {
            Name = "Decorative Water Plant";
            Hue = 0x0;
            Weight = 4.0;
        }

        public WaterPlant(Serial serial)
            : base(serial)
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

    public class RedWaterPlant : Item
    {
        [Constructable]
        public RedWaterPlant()
            : base(0xD04)
        {
            Name = "Decorative Red Water Plant";
            Hue = 0x66D;
            Weight = 4.0;
        }

        public RedWaterPlant(Serial serial)
            : base(serial)
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

    public class BlueWaterPlant : Item
    {
        [Constructable]
        public BlueWaterPlant()
            : base(0xD04)
        {
            Name = "Decorative Blue Water Plant";
            Hue = 0x53D;
            Weight = 4.0;
        }

        public BlueWaterPlant(Serial serial)
            : base(serial)
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

    public class PurpleWaterPlant : Item
    {
        [Constructable]
        public PurpleWaterPlant()
            : base(0xD04)
        {
            Name = "Decorative Purple Water Plant";
            Hue = 0xD;
            Weight = 4.0;
        }

        public PurpleWaterPlant(Serial serial)
            : base(serial)
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

    public class YellowWaterPlant : Item
    {
        [Constructable]
        public YellowWaterPlant()
            : base(0xD04)
        {
            Name = "Decorative Yellow Water Plant";
            Hue = 0x8A5;
            Weight = 4.0;
        }

        public YellowWaterPlant(Serial serial)
            : base(serial)
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

    public class OrangeWaterPlant : Item
    {
        [Constructable]
        public OrangeWaterPlant()
            : base(0xD04)
        {
            Name = "Decorative Orange Water Plant";
            Hue = 0x46F;
            Weight = 4.0;
        }

        public OrangeWaterPlant(Serial serial)
            : base(serial)
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

    public class GreenWaterPlant : Item
    {
        [Constructable]
        public GreenWaterPlant()
            : base(0xD04)
        {
            Name = "Decorative Green Water Plant";
            Hue = 0x59B;
            Weight = 4.0;
        }

        public GreenWaterPlant(Serial serial)
            : base(serial)
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

    public class BrightRedWaterPlant : Item
    {
        [Constructable]
        public BrightRedWaterPlant()
            : base(0xD04)
        {
            Name = "Decorative Bright Red Water Plant";
            Hue = 0x21;
            Weight = 4.0;
        }

        public BrightRedWaterPlant(Serial serial)
            : base(serial)
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

    public class BrightPurpleWaterPlant : Item
    {
        [Constructable]
        public BrightPurpleWaterPlant()
            : base(0xD04)
        {
            Name = "Decorative Bright Purple Water Plant";
            Hue = 0x10;
            Weight = 4.0;
        }

        public BrightPurpleWaterPlant(Serial serial)
            : base(serial)
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

    public class BrightYellowWaterPlant : Item
    {
        [Constructable]
        public BrightYellowWaterPlant()
            : base(0xD04)
        {
            Name = "Decorative Bright Yellow Water Plant";
            Hue = 0x38;
            Weight = 4.0;
        }

        public BrightYellowWaterPlant(Serial serial)
            : base(serial)
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

    public class BrightOrangeWaterPlant : Item
    {
        [Constructable]
        public BrightOrangeWaterPlant()
            : base(0xD04)
        {
            Name = "Decorative Bright Orange Water Plant";
            Hue = 0x2B;
            Weight = 4.0;
        }

        public BrightOrangeWaterPlant(Serial serial)
            : base(serial)
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

    public class BrightGreenWaterPlant : Item
    {
        [Constructable]
        public BrightGreenWaterPlant()
            : base(0xD04)
        {
            Name = "Decorative Bright Green Water Plant";
            Hue = 0x42;
            Weight = 4.0;
        }

        public BrightGreenWaterPlant(Serial serial)
            : base(serial)
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
    #endregion
    #region //SnakePlant
    public class BrightBlueSnakePlant : Item
    {
        [Constructable]
        public BrightBlueSnakePlant()
            : base(0xCA9)
        {
            Name = "Decorative Bright Blue Snake Plant";
            Hue = 0x5;
            Weight = 4.0;
        }

        public BrightBlueSnakePlant(Serial serial)
            : base(serial)
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

    public class BlackSnakePlant : Item
    {
        [Constructable]
        public BlackSnakePlant()
            : base(0xCA9)
        {
            Name = "Decorative Black Snake Plant";
            Hue = 0x455;
            Weight = 4.0;
        }

        public BlackSnakePlant(Serial serial)
            : base(serial)
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

    public class WhiteSnakePlant : Item
    {
        [Constructable]
        public WhiteSnakePlant()
            : base(0xCA9)
        {
            Name = "Decorative White Snake Plant";
            Hue = 0x481;
            Weight = 4.0;
        }

        public WhiteSnakePlant(Serial serial)
            : base(serial)
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

    public class PinkSnakePlant : Item
    {
        [Constructable]
        public PinkSnakePlant()
            : base(0xCA9)
        {
            Name = "Decorative Pink Snake Plant";
            Hue = 0x483;
            Weight = 4.0;
        }

        public PinkSnakePlant(Serial serial)
            : base(serial)
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

    public class MagentaSnakePlant : Item
    {
        [Constructable]
        public MagentaSnakePlant()
            : base(0xCA9)
        {
            Name = "Decorative Magenta Snake Plant";
            Hue = 0x486;
            Weight = 4.0;
        }

        public MagentaSnakePlant(Serial serial)
            : base(serial)
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

    public class AquaSnakePlant : Item
    {
        [Constructable]
        public AquaSnakePlant()
            : base(0xCA9)
        {
            Name = "Decorative Aqua Snake Plant";
            Hue = 0x495;
            Weight = 4.0;
        }

        public AquaSnakePlant(Serial serial)
            : base(serial)
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

    public class FireRedSnakePlant : Item
    {
        [Constructable]
        public FireRedSnakePlant()
            : base(0xCA9)
        {
            Name = "Fire Red Snake Plant";
            Hue = 0x489;
            Weight = 4.0;
        }

        public FireRedSnakePlant(Serial serial)
            : base(serial)
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

    public class SnakePlant : Item
    {
        [Constructable]
        public SnakePlant()
            : base(0xCA9)
        {
            Name = "Decorative Snake Plant";
            Hue = 0x0;
            Weight = 4.0;
        }

        public SnakePlant(Serial serial)
            : base(serial)
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

    public class RedSnakePlant : Item
    {
        [Constructable]
        public RedSnakePlant()
            : base(0xCA9)
        {
            Name = "Decorative Red Snake Plant";
            Hue = 0x66D;
            Weight = 4.0;
        }

        public RedSnakePlant(Serial serial)
            : base(serial)
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

    public class BlueSnakePlant : Item
    {
        [Constructable]
        public BlueSnakePlant()
            : base(0xCA9)
        {
            Name = "Decorative Blue Snake Plant";
            Hue = 0x53D;
            Weight = 4.0;
        }

        public BlueSnakePlant(Serial serial)
            : base(serial)
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

    public class PurpleSnakePlant : Item
    {
        [Constructable]
        public PurpleSnakePlant()
            : base(0xCA9)
        {
            Name = "Decorative Purple Snake Plant";
            Hue = 0xD;
            Weight = 4.0;
        }

        public PurpleSnakePlant(Serial serial)
            : base(serial)
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

    public class YellowSnakePlant : Item
    {
        [Constructable]
        public YellowSnakePlant()
            : base(0xCA9)
        {
            Name = "Decorative Yellow Snake Plant";
            Hue = 0x8A5;
            Weight = 4.0;
        }

        public YellowSnakePlant(Serial serial)
            : base(serial)
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

    public class OrangeSnakePlant : Item
    {
        [Constructable]
        public OrangeSnakePlant()
            : base(0xCA9)
        {
            Name = "Decorative Orange Snake Plant";
            Hue = 0x46F;
            Weight = 4.0;
        }

        public OrangeSnakePlant(Serial serial)
            : base(serial)
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

    public class GreenSnakePlant : Item
    {
        [Constructable]
        public GreenSnakePlant()
            : base(0xCA9)
        {
            Name = "Decorative Green Snake Plant";
            Hue = 0x59B;
            Weight = 4.0;
        }

        public GreenSnakePlant(Serial serial)
            : base(serial)
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

    public class BrightRedSnakePlant : Item
    {
        [Constructable]
        public BrightRedSnakePlant()
            : base(0xCA9)
        {
            Name = "Decorative Bright Red Snake Plant";
            Hue = 0x21;
            Weight = 4.0;
        }

        public BrightRedSnakePlant(Serial serial)
            : base(serial)
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

    public class BrightPurpleSnakePlant : Item
    {
        [Constructable]
        public BrightPurpleSnakePlant()
            : base(0xCA9)
        {
            Name = "Decorative Bright Purple Snake Plant";
            Hue = 0x10;
            Weight = 4.0;
        }

        public BrightPurpleSnakePlant(Serial serial)
            : base(serial)
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

    public class BrightYellowSnakePlant : Item
    {
        [Constructable]
        public BrightYellowSnakePlant()
            : base(0xCA9)
        {
            Name = "Decorative Bright Yellow Snake Plant";
            Hue = 0x38;
            Weight = 4.0;
        }

        public BrightYellowSnakePlant(Serial serial)
            : base(serial)
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

    public class BrightOrangeSnakePlant : Item
    {
        [Constructable]
        public BrightOrangeSnakePlant()
            : base(0xCA9)
        {
            Name = "Decorative Bright Orange Snake Plant";
            Hue = 0x2B;
            Weight = 4.0;
        }

        public BrightOrangeSnakePlant(Serial serial)
            : base(serial)
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

    public class BrightGreenSnakePlant : Item
    {
        [Constructable]
        public BrightGreenSnakePlant()
            : base(0xCA9)
        {
            Name = "Decorative Bright Green Snake Plant";
            Hue = 0x42;
            Weight = 4.0;
        }

        public BrightGreenSnakePlant(Serial serial)
            : base(serial)
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
    #endregion
    #region //PricklyPearCactus
    public class BrightBluePricklyPearCactus : Item
    {
        [Constructable]
        public BrightBluePricklyPearCactus()
            : base(0xD2C)
        {
            Name = "Decorative Bright Blue Prickly Pear Cactus";
            Hue = 0x5;
            Weight = 4.0;
        }

        public BrightBluePricklyPearCactus(Serial serial)
            : base(serial)
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

    public class BlackPricklyPearCactus : Item
    {
        [Constructable]
        public BlackPricklyPearCactus()
            : base(0xD2C)
        {
            Name = "Decorative Black Prickly Pear Cactus";
            Hue = 0x455;
            Weight = 4.0;
        }

        public BlackPricklyPearCactus(Serial serial)
            : base(serial)
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

    public class WhitePricklyPearCactus : Item
    {
        [Constructable]
        public WhitePricklyPearCactus()
            : base(0xD2C)
        {
            Name = "Decorative White Prickly Pear Cactus";
            Hue = 0x481;
            Weight = 4.0;
        }

        public WhitePricklyPearCactus(Serial serial)
            : base(serial)
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

    public class PinkPricklyPearCactus : Item
    {
        [Constructable]
        public PinkPricklyPearCactus()
            : base(0xD2C)
        {
            Name = "Decorative Pink Prickly Pear Cactus";
            Hue = 0x483;
            Weight = 4.0;
        }

        public PinkPricklyPearCactus(Serial serial)
            : base(serial)
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

    public class MagentaPricklyPearCactus : Item
    {
        [Constructable]
        public MagentaPricklyPearCactus()
            : base(0xD2C)
        {
            Name = "Decorative Magenta Prickly Pear Cactus";
            Hue = 0x486;
            Weight = 4.0;
        }

        public MagentaPricklyPearCactus(Serial serial)
            : base(serial)
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

    public class AquaPricklyPearCactus : Item
    {
        [Constructable]
        public AquaPricklyPearCactus()
            : base(0xD2C)
        {
            Name = "Decorative Aqua Prickly Pear Cactus";
            Hue = 0x495;
            Weight = 4.0;
        }

        public AquaPricklyPearCactus(Serial serial)
            : base(serial)
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

    public class FireRedPricklyPearCactus : Item
    {
        [Constructable]
        public FireRedPricklyPearCactus()
            : base(0xD2C)
        {
            Name = "Fire Red Prickly Pear Cactus";
            Hue = 0x489;
            Weight = 4.0;
        }

        public FireRedPricklyPearCactus(Serial serial)
            : base(serial)
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

    public class PricklyPearCactus : Item
    {
        [Constructable]
        public PricklyPearCactus()
            : base(0xD2C)
        {
            Name = "Decorative Prickly Pear Cactus";
            Hue = 0x0;
            Weight = 4.0;
        }

        public PricklyPearCactus(Serial serial)
            : base(serial)
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

    public class RedPricklyPearCactus : Item
    {
        [Constructable]
        public RedPricklyPearCactus()
            : base(0xD2C)
        {
            Name = "Decorative Red Prickly Pear Cactus";
            Hue = 0x66D;
            Weight = 4.0;
        }

        public RedPricklyPearCactus(Serial serial)
            : base(serial)
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

    public class BluePricklyPearCactus : Item
    {
        [Constructable]
        public BluePricklyPearCactus()
            : base(0xD2C)
        {
            Name = "Decorative Blue Prickly Pear Cactus";
            Hue = 0x53D;
            Weight = 4.0;
        }

        public BluePricklyPearCactus(Serial serial)
            : base(serial)
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

    public class PurplePricklyPearCactus : Item
    {
        [Constructable]
        public PurplePricklyPearCactus()
            : base(0xD2C)
        {
            Name = "Decorative Purple Prickly Pear Cactus";
            Hue = 0xD;
            Weight = 4.0;
        }

        public PurplePricklyPearCactus(Serial serial)
            : base(serial)
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

    public class YellowPricklyPearCactus : Item
    {
        [Constructable]
        public YellowPricklyPearCactus()
            : base(0xD2C)
        {
            Name = "Decorative Yellow Prickly Pear Cactus";
            Hue = 0x8A5;
            Weight = 4.0;
        }

        public YellowPricklyPearCactus(Serial serial)
            : base(serial)
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

    public class OrangePricklyPearCactus : Item
    {
        [Constructable]
        public OrangePricklyPearCactus()
            : base(0xD2C)
        {
            Name = "Decorative Orange Prickly Pear Cactus";
            Hue = 0x46F;
            Weight = 4.0;
        }

        public OrangePricklyPearCactus(Serial serial)
            : base(serial)
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

    public class GreenPricklyPearCactus : Item
    {
        [Constructable]
        public GreenPricklyPearCactus()
            : base(0xD2C)
        {
            Name = "Decorative Green Prickly Pear Cactus";
            Hue = 0x59B;
            Weight = 4.0;
        }

        public GreenPricklyPearCactus(Serial serial)
            : base(serial)
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

    public class BrightRedPricklyPearCactus : Item
    {
        [Constructable]
        public BrightRedPricklyPearCactus()
            : base(0xD2C)
        {
            Name = "Decorative Bright Red Prickly Pear Cactus";
            Hue = 0x21;
            Weight = 4.0;
        }

        public BrightRedPricklyPearCactus(Serial serial)
            : base(serial)
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

    public class BrightPurplePricklyPearCactus : Item
    {
        [Constructable]
        public BrightPurplePricklyPearCactus()
            : base(0xD2C)
        {
            Name = "Decorative Bright Purple Prickly Pear Cactus";
            Hue = 0x10;
            Weight = 4.0;
        }

        public BrightPurplePricklyPearCactus(Serial serial)
            : base(serial)
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

    public class BrightYellowPricklyPearCactus : Item
    {
        [Constructable]
        public BrightYellowPricklyPearCactus()
            : base(0xD2C)
        {
            Name = "Decorative Bright Yellow Prickly Pear Cactus";
            Hue = 0x38;
            Weight = 4.0;
        }

        public BrightYellowPricklyPearCactus(Serial serial)
            : base(serial)
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

    public class BrightOrangePricklyPearCactus : Item
    {
        [Constructable]
        public BrightOrangePricklyPearCactus()
            : base(0xD2C)
        {
            Name = "Decorative Bright Orange Prickly Pear Cactus";
            Hue = 0x2B;
            Weight = 4.0;
        }

        public BrightOrangePricklyPearCactus(Serial serial)
            : base(serial)
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

    public class BrightGreenPricklyPearCactus : Item
    {
        [Constructable]
        public BrightGreenPricklyPearCactus()
            : base(0xD2C)
        {
            Name = "Decorative Bright Green Prickly Pear Cactus";
            Hue = 0x42;
            Weight = 4.0;
        }

        public BrightGreenPricklyPearCactus(Serial serial)
            : base(serial)
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
    #endregion
    #region //BarrelCactus
    public class BrightBlueBarrelCactus : Item
    {
        [Constructable]
        public BrightBlueBarrelCactus()
            : base(0xD26)
        {
            Name = "Decorative Bright Blue Barrel Cactus";
            Hue = 0x5;
            Weight = 4.0;
        }

        public BrightBlueBarrelCactus(Serial serial)
            : base(serial)
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

    public class BlackBarrelCactus : Item
    {
        [Constructable]
        public BlackBarrelCactus()
            : base(0xD26)
        {
            Name = "Decorative Black Barrel Cactus";
            Hue = 0x455;
            Weight = 4.0;
        }

        public BlackBarrelCactus(Serial serial)
            : base(serial)
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

    public class WhiteBarrelCactus : Item
    {
        [Constructable]
        public WhiteBarrelCactus()
            : base(0xD26)
        {
            Name = "Decorative White Barrel Cactus";
            Hue = 0x481;
            Weight = 4.0;
        }

        public WhiteBarrelCactus(Serial serial)
            : base(serial)
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

    public class PinkBarrelCactus : Item
    {
        [Constructable]
        public PinkBarrelCactus()
            : base(0xD26)
        {
            Name = "Decorative Pink Barrel Cactus";
            Hue = 0x483;
            Weight = 4.0;
        }

        public PinkBarrelCactus(Serial serial)
            : base(serial)
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

    public class MagentaBarrelCactus : Item
    {
        [Constructable]
        public MagentaBarrelCactus()
            : base(0xD26)
        {
            Name = "Decorative Magenta Barrel Cactus";
            Hue = 0x486;
            Weight = 4.0;
        }

        public MagentaBarrelCactus(Serial serial)
            : base(serial)
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

    public class AquaBarrelCactus : Item
    {
        [Constructable]
        public AquaBarrelCactus()
            : base(0xD26)
        {
            Name = "Decorative Aqua Barrel Cactus";
            Hue = 0x495;
            Weight = 4.0;
        }

        public AquaBarrelCactus(Serial serial)
            : base(serial)
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

    public class FireRedBarrelCactus : Item
    {
        [Constructable]
        public FireRedBarrelCactus()
            : base(0xD26)
        {
            Name = "Fire Red Barrel Cactus";
            Hue = 0x489;
            Weight = 4.0;
        }

        public FireRedBarrelCactus(Serial serial)
            : base(serial)
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

    public class BarrelCactus : Item
    {
        [Constructable]
        public BarrelCactus()
            : base(0xD26)
        {
            Name = "Decorative Barrel Cactus";
            Hue = 0x0;
            Weight = 4.0;
        }

        public BarrelCactus(Serial serial)
            : base(serial)
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

    public class RedBarrelCactus : Item
    {
        [Constructable]
        public RedBarrelCactus()
            : base(0xD26)
        {
            Name = "Decorative Red Barrel Cactus";
            Hue = 0x66D;
            Weight = 4.0;
        }

        public RedBarrelCactus(Serial serial)
            : base(serial)
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

    public class BlueBarrelCactus : Item
    {
        [Constructable]
        public BlueBarrelCactus()
            : base(0xD26)
        {
            Name = "Decorative Blue Barrel Cactus";
            Hue = 0x53D;
            Weight = 4.0;
        }

        public BlueBarrelCactus(Serial serial)
            : base(serial)
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

    public class PurpleBarrelCactus : Item
    {
        [Constructable]
        public PurpleBarrelCactus()
            : base(0xD26)
        {
            Name = "Decorative Purple Barrel Cactus";
            Hue = 0xD;
            Weight = 4.0;
        }

        public PurpleBarrelCactus(Serial serial)
            : base(serial)
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

    public class YellowBarrelCactus : Item
    {
        [Constructable]
        public YellowBarrelCactus()
            : base(0xD26)
        {
            Name = "Decorative Yellow Barrel Cactus";
            Hue = 0x8A5;
            Weight = 4.0;
        }

        public YellowBarrelCactus(Serial serial)
            : base(serial)
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

    public class OrangeBarrelCactus : Item
    {
        [Constructable]
        public OrangeBarrelCactus()
            : base(0xD26)
        {
            Name = "Decorative Orange Barrel Cactus";
            Hue = 0x46F;
            Weight = 4.0;
        }

        public OrangeBarrelCactus(Serial serial)
            : base(serial)
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

    public class GreenBarrelCactus : Item
    {
        [Constructable]
        public GreenBarrelCactus()
            : base(0xD26)
        {
            Name = "Decorative Green Barrel Cactus";
            Hue = 0x59B;
            Weight = 4.0;
        }

        public GreenBarrelCactus(Serial serial)
            : base(serial)
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

    public class BrightRedBarrelCactus : Item
    {
        [Constructable]
        public BrightRedBarrelCactus()
            : base(0xD26)
        {
            Name = "Decorative Bright Red Barrel Cactus";
            Hue = 0x21;
            Weight = 4.0;
        }

        public BrightRedBarrelCactus(Serial serial)
            : base(serial)
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

    public class BrightPurpleBarrelCactus : Item
    {
        [Constructable]
        public BrightPurpleBarrelCactus()
            : base(0xD26)
        {
            Name = "Decorative Bright Purple Barrel Cactus";
            Hue = 0x10;
            Weight = 4.0;
        }

        public BrightPurpleBarrelCactus(Serial serial)
            : base(serial)
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

    public class BrightYellowBarrelCactus : Item
    {
        [Constructable]
        public BrightYellowBarrelCactus()
            : base(0xD26)
        {
            Name = "Decorative Bright Yellow Barrel Cactus";
            Hue = 0x38;
            Weight = 4.0;
        }

        public BrightYellowBarrelCactus(Serial serial)
            : base(serial)
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

    public class BrightOrangeBarrelCactus : Item
    {
        [Constructable]
        public BrightOrangeBarrelCactus()
            : base(0xD26)
        {
            Name = "Decorative Bright Orange Barrel Cactus";
            Hue = 0x2B;
            Weight = 4.0;
        }

        public BrightOrangeBarrelCactus(Serial serial)
            : base(serial)
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

    public class BrightGreenBarrelCactus : Item
    {
        [Constructable]
        public BrightGreenBarrelCactus()
            : base(0xD26)
        {
            Name = "Decorative Bright Green Barrel Cactus";
            Hue = 0x42;
            Weight = 4.0;
        }

        public BrightGreenBarrelCactus(Serial serial)
            : base(serial)
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
    #endregion
    #region //TribarrelCactus
    public class BrightBlueTribarrelCactus : Item
    {
        [Constructable]
        public BrightBlueTribarrelCactus()
            : base(0xD26)
        {
            Name = "Decorative Bright Blue Tribarrel Cactus";
            Hue = 0x5;
            Weight = 4.0;
        }

        public BrightBlueTribarrelCactus(Serial serial)
            : base(serial)
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

    public class BlackTribarrelCactus : Item
    {
        [Constructable]
        public BlackTribarrelCactus()
            : base(0xD26)
        {
            Name = "Decorative Black Tribarrel Cactus";
            Hue = 0x455;
            Weight = 4.0;
        }

        public BlackTribarrelCactus(Serial serial)
            : base(serial)
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

    public class WhiteTribarrelCactus : Item
    {
        [Constructable]
        public WhiteTribarrelCactus()
            : base(0xD26)
        {
            Name = "Decorative White Tribarrel Cactus";
            Hue = 0x481;
            Weight = 4.0;
        }

        public WhiteTribarrelCactus(Serial serial)
            : base(serial)
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

    public class PinkTribarrelCactus : Item
    {
        [Constructable]
        public PinkTribarrelCactus()
            : base(0xD26)
        {
            Name = "Decorative Pink Tribarrel Cactus";
            Hue = 0x483;
            Weight = 4.0;
        }

        public PinkTribarrelCactus(Serial serial)
            : base(serial)
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

    public class MagentaTribarrelCactus : Item
    {
        [Constructable]
        public MagentaTribarrelCactus()
            : base(0xD26)
        {
            Name = "Decorative Magenta Tribarrel Cactus";
            Hue = 0x486;
            Weight = 4.0;
        }

        public MagentaTribarrelCactus(Serial serial)
            : base(serial)
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

    public class AquaTribarrelCactus : Item
    {
        [Constructable]
        public AquaTribarrelCactus()
            : base(0xD26)
        {
            Name = "Decorative Aqua Tribarrel Cactus";
            Hue = 0x495;
            Weight = 4.0;
        }

        public AquaTribarrelCactus(Serial serial)
            : base(serial)
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

    public class FireRedTribarrelCactus : Item
    {
        [Constructable]
        public FireRedTribarrelCactus()
            : base(0xD26)
        {
            Name = "Fire Red Tribarrel Cactus";
            Hue = 0x489;
            Weight = 4.0;
        }

        public FireRedTribarrelCactus(Serial serial)
            : base(serial)
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

    public class TribarrelCactus : Item
    {
        [Constructable]
        public TribarrelCactus()
            : base(0xD26)
        {
            Name = "Decorative Tribarrel Cactus";
            Hue = 0x0;
            Weight = 4.0;
        }

        public TribarrelCactus(Serial serial)
            : base(serial)
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

    public class RedTribarrelCactus : Item
    {
        [Constructable]
        public RedTribarrelCactus()
            : base(0xD26)
        {
            Name = "Decorative Red Tribarrel Cactus";
            Hue = 0x66D;
            Weight = 4.0;
        }

        public RedTribarrelCactus(Serial serial)
            : base(serial)
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

    public class BlueTribarrelCactus : Item
    {
        [Constructable]
        public BlueTribarrelCactus()
            : base(0xD26)
        {
            Name = "Decorative Blue Tribarrel Cactus";
            Hue = 0x53D;
            Weight = 4.0;
        }

        public BlueTribarrelCactus(Serial serial)
            : base(serial)
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

    public class PurpleTribarrelCactus : Item
    {
        [Constructable]
        public PurpleTribarrelCactus()
            : base(0xD26)
        {
            Name = "Decorative Purple Tribarrel Cactus";
            Hue = 0xD;
            Weight = 4.0;
        }

        public PurpleTribarrelCactus(Serial serial)
            : base(serial)
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

    public class YellowTribarrelCactus : Item
    {
        [Constructable]
        public YellowTribarrelCactus()
            : base(0xD26)
        {
            Name = "Decorative Yellow Tribarrel Cactus";
            Hue = 0x8A5;
            Weight = 4.0;
        }

        public YellowTribarrelCactus(Serial serial)
            : base(serial)
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

    public class OrangeTribarrelCactus : Item
    {
        [Constructable]
        public OrangeTribarrelCactus()
            : base(0xD26)
        {
            Name = "Decorative Orange Tribarrel Cactus";
            Hue = 0x46F;
            Weight = 4.0;
        }

        public OrangeTribarrelCactus(Serial serial)
            : base(serial)
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

    public class GreenTribarrelCactus : Item
    {
        [Constructable]
        public GreenTribarrelCactus()
            : base(0xD26)
        {
            Name = "Decorative Green Tribarrel Cactus";
            Hue = 0x59B;
            Weight = 4.0;
        }

        public GreenTribarrelCactus(Serial serial)
            : base(serial)
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

    public class BrightRedTribarrelCactus : Item
    {
        [Constructable]
        public BrightRedTribarrelCactus()
            : base(0xD26)
        {
            Name = "Decorative Bright Red Tribarrel Cactus";
            Hue = 0x21;
            Weight = 4.0;
        }

        public BrightRedTribarrelCactus(Serial serial)
            : base(serial)
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

    public class BrightPurpleTribarrelCactus : Item
    {
        [Constructable]
        public BrightPurpleTribarrelCactus()
            : base(0xD26)
        {
            Name = "Decorative Bright Purple Tribarrel Cactus";
            Hue = 0x10;
            Weight = 4.0;
        }

        public BrightPurpleTribarrelCactus(Serial serial)
            : base(serial)
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

    public class BrightYellowTribarrelCactus : Item
    {
        [Constructable]
        public BrightYellowTribarrelCactus()
            : base(0xD26)
        {
            Name = "Decorative Bright Yellow Tribarrel Cactus";
            Hue = 0x38;
            Weight = 4.0;
        }

        public BrightYellowTribarrelCactus(Serial serial)
            : base(serial)
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

    public class BrightOrangeTribarrelCactus : Item
    {
        [Constructable]
        public BrightOrangeTribarrelCactus()
            : base(0xD26)
        {
            Name = "Decorative Bright Orange Tribarrel Cactus";
            Hue = 0x2B;
            Weight = 4.0;
        }

        public BrightOrangeTribarrelCactus(Serial serial)
            : base(serial)
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

    public class BrightGreenTribarrelCactus : Item
    {
        [Constructable]
        public BrightGreenTribarrelCactus()
            : base(0xD26)
        {
            Name = "Decorative Bright Green Tribarrel Cactus";
            Hue = 0x42;
            Weight = 4.0;
        }

        public BrightGreenTribarrelCactus(Serial serial)
            : base(serial)
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
    #endregion
    #region //Bonsai
    public class CommonGreenBonsai : Item
    {
        [Constructable]
        public CommonGreenBonsai()
            : base(0x28DC)
        {
            Name = "Decorative Common Bonsai";
            Hue = 0;
            Weight = 4.0;
        }

        public CommonGreenBonsai(Serial serial)
            : base(serial)
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

    public class CommonPinkBonsai : Item
    {
        [Constructable]
        public CommonPinkBonsai()
            : base(0x28DF)
        {
            Name = "Decorative Common Bonsai";
            Hue = 0;
            Weight = 4.0;
        }

        public CommonPinkBonsai(Serial serial)
            : base(serial)
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

    public class UncommonGreenBonsai : Item
    {
        [Constructable]
        public UncommonGreenBonsai()
            : base(0x28DD)
        {
            Name = "Decorative Uncommon Bonsai";
            Hue = 0;
            Weight = 4.0;
        }

        public UncommonGreenBonsai(Serial serial)
            : base(serial)
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

    public class UncommonPinkBonsai : Item
    {
        [Constructable]
        public UncommonPinkBonsai()
            : base(0x28E0)
        {
            Name = "Decorative Uncommon Bonsai";
            Hue = 0;
            Weight = 4.0;
        }

        public UncommonPinkBonsai(Serial serial)
            : base(serial)
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

    public class RareGreenBonsai : Item
    {
        [Constructable]
        public RareGreenBonsai()
            : base(0x28DE)
        {
            Name = "Decorative Rare Bonsai";
            Hue = 0;
            Weight = 4.0;
        }

        public RareGreenBonsai(Serial serial)
            : base(serial)
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

    public class RarePinkBonsai : Item
    {
        [Constructable]
        public RarePinkBonsai()
            : base(0x28E1)
        {
            Name = "Decorative Rare Bonsai";
            Hue = 0;
            Weight = 4.0;
        }

        public RarePinkBonsai(Serial serial)
            : base(serial)
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

    public class ExceptionalBonsai : Item
    {
        [Constructable]
        public ExceptionalBonsai()
            : base(0x28E2)
        {
            Name = "Decorative Exceptional Bonsai";
            Hue = 0;
            Weight = 4.0;
        }

        public ExceptionalBonsai(Serial serial)
            : base(serial)
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

    public class ExoticBonsai : Item
    {
        [Constructable]
        public ExoticBonsai()
            : base(0x28E3)
        {
            Name = "Decorative Exotic Bonsai";
            Hue = 0;
            Weight = 4.0;
        }

        public ExoticBonsai(Serial serial)
            : base(serial)
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
    #endregion
	}
