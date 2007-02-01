using System;
using System.Collections;
using Server;
using Server.Network;

namespace Server.Items
{

	public class WinecrafterSugar : Item, ICommodity
	{
		string ICommodity.Description
		{
			get
			{
				return String.Format( Amount == 1 ? "{0} Sugar" : "{0} Sugar", Amount );
			}
		}

		[Constructable]
		public WinecrafterSugar() : this(1)
                {
		}

		[Constructable]
        public WinecrafterSugar(int amount) : base(0xF8F)
		{
			this.Stackable = true;
			this.Hue = 1150;
			this.Name = "a jar of sugar";
			this.ItemID = 4102;
			this.Amount = amount;
			this.Weight = 1;
		}

		public WinecrafterSugar(Serial serial) : base(serial)
		{
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