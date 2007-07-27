using System;
using Server;

namespace Server.Items
{
	public class StoneFirePit : Item
	{
		[Constructable]
		public StoneFirePit() : base( 0x29FD )
		{
		      	Weight = 5.0;
			Movable = true;
            	}

		public StoneFirePit( Serial serial ) : base( serial )
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

	public class PegBoardSouth : Item
	{
		[Constructable]
		public PegBoardSouth() : base( 0xC39 )
		{
		      	Weight = 2.0;
			Movable = true;
            	}

		public PegBoardSouth( Serial serial ) : base( serial )
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

	public class PegBoardEast : Item
	{
		[Constructable]
		public PegBoardEast() : base( 0xC3A )
		{
		      	Weight = 2.0;
			Movable = true;
            	}

		public PegBoardEast( Serial serial ) : base( serial )
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

	public class DecoCampfire : BaseLight
	{
		public override int LitItemID{ get { return 0xFAC; } }
		public override int UnlitItemID{ get { return 0xFAC; } }

		[Constructable]
		public DecoCampfire() : base( 0xFAC )
		{
			Name = "Campfire";
			Burning = false;
			Light = LightType.Circle300;
		      	Weight = 5.0;
			Duration = TimeSpan.Zero;
			Movable = true;
            	}

		public DecoCampfire( Serial serial ) : base( serial )
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

	public class CookingCauldron : Item
	{
		[Constructable]
		public CookingCauldron() : base( 0x3DAF )
		{
			Name = "Cooking Cauldron";
		      	Weight = 5.0;
			Movable = true;
            	}

		public CookingCauldron( Serial serial ) : base( serial )
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