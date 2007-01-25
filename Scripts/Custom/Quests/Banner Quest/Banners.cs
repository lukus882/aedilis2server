/*
 * Created by SharpDevelop.
 * User: Sharon
 * Date: 5/13/2006
 * Time: 4:29 PM
 * 
 * 
 */
using System;

namespace Server.Items
{
	[Flipable(0x15C0,  0x15C1)]
	public class Banner1 : Item, IDyable
	{
		[Constructable]
		public Banner1() : base(0x15C0)
		{
			Weight = 4.0;			
		}

		public Banner1(Serial serial) : base(serial)
		{
		}
		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
			{
				return false;
			}

			Hue = sender.DyedHue;

			return true;
		}
		
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	[Flipable(0x15C2,  0x15C3)]
	public class Banner2 : Item, IDyable
	{
		[Constructable]
		public Banner2() : base(0x15C2)
		{
			Weight = 4.0;			
		}

		public Banner2(Serial serial) : base(serial)
		{
		}
		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
			{
				return false;
			}

			Hue = sender.DyedHue;

			return true;
		}
		
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	[Flipable(0x15D4,  0x15D5)]
	public class Banner3 : Item, IDyable
	{
		[Constructable]
		public Banner3() : base(0x15D4)
		{
			Weight = 4.0;			
		}

		public Banner3(Serial serial) : base(serial)
		{
		}
		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
			{
				return false;
			}

			Hue = sender.DyedHue;

			return true;
		}
		
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	[Flipable(0x15E8,  0x15E9)]
	public class Banner4 : Item, IDyable
	{
		[Constructable]
		public Banner4() : base(0x15E8)
		{
			Weight = 4.0;			
		}

		public Banner4(Serial serial) : base(serial)
		{
		}
		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
			{
				return false;
			}

			Hue = sender.DyedHue;

			return true;
		}
		
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}
