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
using Server;
using Server.Items; 

namespace Server.Items
{
    public class BluePaste : Item
    {
		[Constructable]
		public BluePaste() : this( 1 )
		{
		}

        [Constructable]
        public BluePaste( int amount ) : base(0x9EC)
        {
	Name = "Blue Paste";
	Stackable = true;       
            Hue = 5;
            Weight = 1.0;
	Amount = amount;
        }

        public BluePaste(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

    }
   
        public class RedPaste : Item
        {

		[Constructable]
		public RedPaste() : this( 1 )
		{
		}

            [Constructable]
            public RedPaste( int amount )
                : base(0x9EC)
            {
	Name = "Red Paste";
	Stackable = true;       
                Hue = 33;
                Weight = 1.0;
	Amount = amount;
            }

            public RedPaste(Serial serial)
                : base(serial)
            {
            }

            public override void Serialize(GenericWriter writer)
            {
                base.Serialize(writer);

                writer.Write((int)0); // version
            }

            public override void Deserialize(GenericReader reader)
            {
                base.Deserialize(reader);

                int version = reader.ReadInt();
            }

        }

            public class YellowPaste : Item
            {

		[Constructable]
		public YellowPaste() : this( 1 )
		{
		}
                [Constructable]
                public YellowPaste( int amount )
                    : base(0x9EC)
                {
	Name = "Yellow Paste";
	Stackable = true;       
                    Hue = 56;
                    Weight = 1.0;
	Amount = amount;
                }

                public YellowPaste(Serial serial)
                    : base(serial)
                {
                }

                public override void Serialize(GenericWriter writer)
                {
                    base.Serialize(writer);

                    writer.Write((int)0); // version
                }

                public override void Deserialize(GenericReader reader)
                {
                    base.Deserialize(reader);

                    int version = reader.ReadInt();
                }

            }

              public class WhitePaste : Item
                {

		[Constructable]
		public WhitePaste() : this( 1 )
		{
		}
                    [Constructable]
                    public WhitePaste( int amount )
                        : base(0x9EC)
                    {
	Name = "White Paste";
	Stackable = true;       
                        Hue = 1153;
                        Weight = 1.0;
	Amount = amount;
                    }

                    public WhitePaste(Serial serial)
                        : base(serial)
                    {
                    }

                    public override void Serialize(GenericWriter writer)
                    {
                        base.Serialize(writer);

                        writer.Write((int)0); // version
                    }

                    public override void Deserialize(GenericReader reader)
                    {
                        base.Deserialize(reader);

                        int version = reader.ReadInt();
                    }

                }

                
                    public class BlackPaste : Item
                    {

		[Constructable]
		public BlackPaste() : this( 1 )
		{
		}

          
                        [Constructable]
                        public BlackPaste( int amount )
                            : base(0x9EC)
                        {
	Name = "Black Paste";
	Stackable = true;       
                            Hue = 1109;
                            Weight = 1.0;
	Amount = amount;
                        }

                        public BlackPaste(Serial serial)
                            : base(serial)
                        {
                        }

                        public override void Serialize(GenericWriter writer)
                        {
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
