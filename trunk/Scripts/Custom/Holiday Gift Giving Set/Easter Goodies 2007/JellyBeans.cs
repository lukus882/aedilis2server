using System;
using Server.Network;

namespace Server.Items
{
	public class JellyBean1 : Food
	{
		[Constructable]
		public JellyBean1() : this( 1 )
		{
		}

		[Constructable]
		public JellyBean1( int amount ) : base( 0x171f )
		{
            Stackable = true;
			Weight = 1.0;
			FillFactor = 1;
            Amount = amount;
            ItemID = 3859;
		}

        public override void AddNameProperty(ObjectPropertyList list)
        {
            if (Amount > 1)
                list.Add(String.Format("{0} Jelly Beans", Amount));
            else
                list.Add("A Jelly Bean");
        }		
		
		public JellyBean1( Serial serial ) : base( serial )
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

    public class JellyBean2 : Food
	{
		[Constructable]
		public JellyBean2() : this( 1 )
		{
		}

		[Constructable]
		public JellyBean2( int amount ) : base( 0x171f )
		{
            Stackable = true;
			Weight = 1.0;
			FillFactor = 1;
            Amount = amount;
            ItemID = 3877;
		}

        public override void AddNameProperty(ObjectPropertyList list)
        {
            if (Amount > 1)
                list.Add(String.Format("{0} Jelly Beans", Amount));
            else
                list.Add("A Jelly Bean");
        }		
		
		public JellyBean2( Serial serial ) : base( serial )
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

	public class JellyBean3 : Food
	{
		[Constructable]
		public JellyBean3() : this( 1 )
		{
		}

		[Constructable]
		public JellyBean3( int amount ) : base( 0x171f )
		{
            Stackable = true;
			Weight = 1.0;
			FillFactor = 1;
            Amount = amount;
            ItemID = 3861;
		}

        public override void AddNameProperty(ObjectPropertyList list)
        {
            if (Amount > 1)
                list.Add(String.Format("{0} Jelly Beans", Amount));
            else
                list.Add("A Jelly Bean");
        }		
		
		public JellyBean3( Serial serial ) : base( serial )
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
 
	public class JellyBean4 : Food
	{
		[Constructable]
		public JellyBean4() : this( 1 )
		{
		}

		[Constructable]
		public JellyBean4( int amount ) : base( 0x171f )
		{
            Stackable = true;
			Weight = 1.0;
			FillFactor = 1;
            Amount = amount;
            ItemID = 3862;
		}

        public override void AddNameProperty(ObjectPropertyList list)
        {
            if (Amount > 1)
                list.Add(String.Format("{0} Jelly Beans", Amount));
            else
                list.Add("A Jelly Bean");
        }		
		
		public JellyBean4( Serial serial ) : base( serial )
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

      public class JellyBean5 : Food
	{
		[Constructable]
		public JellyBean5() : this( 1 )
		{
		}

		[Constructable]
		public JellyBean5( int amount ) : base( 0x171f )
		{
            Stackable = true;
			Weight = 1.0;
			FillFactor = 1;
            Amount = amount;
            ItemID = 3856;
		}

        public override void AddNameProperty(ObjectPropertyList list)
        {
            if (Amount > 1)
                list.Add(String.Format("{0} Jelly Beans", Amount));
            else
                list.Add("A Jelly Bean");
        }		
		
		public JellyBean5( Serial serial ) : base( serial )
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

 	public class JellyBean6 : Food
	{
		[Constructable]
		public JellyBean6() : this( 1 )
		{
		}

		[Constructable]
		public JellyBean6( int amount ) : base( 0x171f )
		{
            Stackable = true;
			Weight = 1.0;
			FillFactor = 1;
            Amount = amount;
            ItemID = 3865;
		}

        public override void AddNameProperty(ObjectPropertyList list)
        {
            if (Amount > 1)
                list.Add(String.Format("{0} Jelly Beans", Amount));
            else
                list.Add("A Jelly Bean");
        }		
		
		public JellyBean6( Serial serial ) : base( serial )
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
 
 	public class JellyBean7 : Food
	{
		[Constructable]
		public JellyBean7() : this( 1 )
		{
		}

		[Constructable]
		public JellyBean7( int amount ) : base( 0x171f )
		{
            Stackable = true;
			Weight = 1.0;
			FillFactor = 1;
            Amount = amount;
            ItemID = 3873;
		}

        public override void AddNameProperty(ObjectPropertyList list)
        {
            if (Amount > 1)
                list.Add(String.Format("{0} Jelly Beans", Amount));
            else
                list.Add("A Jelly Bean");
        }		
		
		public JellyBean7( Serial serial ) : base( serial )
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

    public class JellyBean8 : Food
	{
		[Constructable]
		public JellyBean8() : this( 1 )
		{
		}

		[Constructable]
		public JellyBean8( int amount ) : base( 0x171f )
		{
            Stackable = true;
			Weight = 1.0;
			FillFactor = 1;
            Amount = amount;
            ItemID = 3885;
		}

        public override void AddNameProperty(ObjectPropertyList list)
        {
            if (Amount > 1)
                list.Add(String.Format("{0} Jelly Beans", Amount));
            else
                list.Add("A Jelly Bean");
        }		
		
		public JellyBean8( Serial serial ) : base( serial )
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