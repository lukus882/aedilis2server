////////////////////////////////////
// Bonsai Mountain
//
// by Rincewind - admin@plccontractor.com
//                www.discworld.plccontractor.com
//				  msm - UK_Sparky
//
//You are welcome to do what ever you want with this code ;)
////////////////////////////////////



using System;
using System.Collections;
using Server;
using Server.Misc;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;


namespace Server.Items
{
	public class BonsaiMountain : Item
	{
					
		public static int age;
		public static int antispam;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Bonsai_Age
		{
			get { return age; }
			set { age = value; InvalidateProperties(); }
		}

							
		[Constructable]
		public BonsaiMountain() : base(0x1777)
		{
			Name = "A baby bonsai mountain";
			Weight = 10;
		}

		public BonsaiMountain(Serial serial) : base(serial)
		{
		}

		public override bool HandlesOnMovement { get { return true; } }

		public override void OnMovement(Mobile m, Point3D oldLocation)
		{
			if (TimeSystem.System.Month == 1 && antispam == 0)
			{
				antispam = 1;
				age++;
				switch(age)
				{
					case 0: this.Name = "A baby bonsai mountain";break;
					case 1: this.Name = ("A baby bonsai mountain that is " + age + "yr old");break;
					case 2: this.Name = ("A baby bonsai mountain that is " + age + "yrs old");break;
					case 3: this.Name = ("A baby bonsai mountain that is " + age + "yrs old");break;
					case 4: this.Name = ("A young bonsai mountain that is " + age + "yrs old");this.Weight = 50.0;this.ItemID = 0x1775;break;
					case 5: this.Name = ("A young bonsai mountain that is " + age + "yrs old");break;
					case 6: this.Name = ("A young bonsai mountain that is " + age + "yrs old");break;
					case 7: this.Name = ("A young bonsai mountain that is " + age + "yrs old");break;
					case 8: this.Name = ("A bonsai mountain that is " + age + "yrs old");this.Weight = 100.0;break;
					case 9: this.Name = ("A bonsai mountain that is " + age + "yrs old");this.Weight = 150.0;break;
					case 10: this.Name = ("A bonsai mountain that is " + age + "yrs old");this.Weight = 200.0;break;
					case 11: this.Name = ("A bonsai mountain that is " + age + "yrs old");this.Weight = 250.0;break;
					case 12: this.Name = ("An old bonsai mountain that is " + age + "yrs old");this.Weight = 300.0;this.ItemID = 0x1776;break;
					case 13: this.Name = ("An old bonsai mountain that is " + age + "yrs old");this.Weight = 350.0;break;
					default: this.Name = ("An old bonsai mountain that is " + age + "yrs old");this.Weight = 450.0;break;
				}
			}
			else if (TimeSystem.System.Month == 2 && antispam == 1)
			{
				antispam = 0;
			}
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
