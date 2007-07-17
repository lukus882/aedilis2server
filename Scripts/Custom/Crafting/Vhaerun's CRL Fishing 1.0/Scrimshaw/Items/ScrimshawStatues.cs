using System;
using Server.Network;

namespace Server.Items
{
	public class ScrimGoatStatue : Item
	{
		[Constructable]
		public ScrimGoatStatue() : base( 0x2580 )
		{
			Name = "Goat Statue of Scrimshaw";
		      	Weight = 4.0;
			Hue = 0x47E;

            	}

		public ScrimGoatStatue( Serial serial ) : base( serial )
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

	public class ScrimCougarStatue : Item
	{
		[Constructable]
		public ScrimCougarStatue() : base( 0x2583 )
		{
			Name = "Cougar Statue of Scrimshaw";
		      	Weight = 4.0;
			Hue = 0x47E;

           	}

		public ScrimCougarStatue( Serial serial ) : base( serial )
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

	public class ScrimDogStatue : Item
	{
		[Constructable]
		public ScrimDogStatue() : base( 0x2588 )
		{
			Name = "Dog Statue of Scrimshaw";
		      	Weight = 4.0;
			Hue = 0x47E;

           	}

		public ScrimDogStatue( Serial serial ) : base( serial )
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

	public class ScrimWolfStatue : Item
	{
		[Constructable]
		public ScrimWolfStatue() : base( 0x25D0 )
		{
			Name = "Wolf Statue of Scrimshaw";
		      	Weight = 4.0;
			Hue = 0x47E;

           	}

		public ScrimWolfStatue( Serial serial ) : base( serial )
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

	public class ScrimCraneStatue : Item
	{
		[Constructable]
		public ScrimCraneStatue() : base( 0x2764 )
		{
			Name = "Crane Statue of Scrimshaw";
		      	Weight = 4.0;
			Hue = 0x47E;

           	}

		public ScrimCraneStatue( Serial serial ) : base( serial )
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

	public class ScrimCatStatue : Item
	{
		[Constructable]
		public ScrimCatStatue() : base( 0x211B )
		{
			Name = "Cat Statue of Scrimshaw";
		      	Weight = 4.0;
			Hue = 0x47E;

           	}

		public ScrimCatStatue( Serial serial ) : base( serial )
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

	public class ScrimBearStatue : Item
	{
		[Constructable]
		public ScrimBearStatue() : base( 0x2118 )
		{
			Name = "Bear Statue of Scrimshaw";
		      	Weight = 4.0;
			Hue = 0x47E;

           	}

		public ScrimBearStatue( Serial serial ) : base( serial )
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

	public class ScrimRabbitStatue : Item
	{
		[Constructable]
		public ScrimRabbitStatue() : base( 0x2125 )
		{
			Name = "Rabbit Statue of Scrimshaw";
		      	Weight = 4.0;
			Hue = 0x47E;

           	}

		public ScrimRabbitStatue( Serial serial ) : base( serial )
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

	public class ScrimBirdStatue : Item
	{
		[Constructable]
		public ScrimBirdStatue() : base( 0x20EE )
		{
			Name = "Bird Statue of Scrimshaw";
		      	Weight = 4.0;
			Hue = 0x47E;

           	}

		public ScrimBirdStatue( Serial serial ) : base( serial )
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

	public class ScrimLizardStatue : Item
	{
		[Constructable]
		public ScrimLizardStatue() : base( 0x2131 )
		{
			Name = "Lizard Statue of Scrimshaw";
		      	Weight = 4.0;
			Hue = 0x47E;

           	}

		public ScrimLizardStatue( Serial serial ) : base( serial )
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

	public class ScrimFaerieStatue : Item
	{
		[Constructable]
		public ScrimFaerieStatue() : base( 0x25B6 )
		{
			Name = "Faerie Statue of Scrimshaw";
		      	Weight = 4.0;
			Hue = 0x47E;

           	}

		public ScrimFaerieStatue( Serial serial ) : base( serial )
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

	public class ScrimMageStatue : Item
	{
		[Constructable]
		public ScrimMageStatue() : base( 0x25F8 )
		{
			Name = "Mage Statue of Scrimshaw";
		      	Weight = 4.0;
			Hue = 0x47E;

           	}

		public ScrimMageStatue( Serial serial ) : base( serial )
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

	public class ScrimPhoenixStatue : Item
	{
		[Constructable]
		public ScrimPhoenixStatue() : base( 0x276A )
		{
			Name = "Phoenix Statue of Scrimshaw";
		      	Weight = 4.0;
			Hue = 0x47E;

           	}

		public ScrimPhoenixStatue( Serial serial ) : base( serial )
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

	public class ScrimFerretStatue : Item
	{
		[Constructable]
		public ScrimFerretStatue() : base( 0x2D98 )
		{
			Name = "Ferret Statue of Scrimshaw";
		      	Weight = 4.0;
			Hue = 0x47E;

           	}

		public ScrimFerretStatue( Serial serial ) : base( serial )
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

	public class ScrimFishStatue : Item
	{
		[Constructable]
		public ScrimFishStatue() : base( 0x3AFE )
		{
			Name = "Fish Statue of Scrimshaw";
		      	Weight = 4.0;
			Hue = 0x47E;

           	}

		public ScrimFishStatue( Serial serial ) : base( serial )
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

	public class ScrimTreeStatue : Item
	{
		[Constructable]
		public ScrimTreeStatue() : base( 0x20FA )
		{
			Name = "Tree Statue of Scrimshaw";
		      	Weight = 4.0;
			Hue = 0x47E;

           	}

		public ScrimTreeStatue( Serial serial ) : base( serial )
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