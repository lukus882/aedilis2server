using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	public class SturdyBakersBoard : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefCooking.CraftSystem; } }

		[Constructable]
		public SturdyBakersBoard() : base( 0x14EA )
		{
			Name = "Sturdy Baker's Board";
			Weight = 4.0;
			UsesRemaining = 150;
			Hue = 0x3DA;
		}

		[Constructable]
		public SturdyBakersBoard( int uses ) : base( uses, 0x14EA )
		{

		}

		public SturdyBakersBoard( Serial serial ) : base( serial )
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

	public class SturdyCooksCauldron : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefCooking.CraftSystem; } }

		[Constructable]
		public SturdyCooksCauldron() : base( 0x9ED )
		{
			Name = "Cook's Cauldron";
			Weight = 4.0;
			UsesRemaining = 150;
			Hue = 0x3DA;
		}

		[Constructable]
		public SturdyCooksCauldron( int uses ) : base( uses, 0x9ED )
		{

		}

		public SturdyCooksCauldron( Serial serial ) : base( serial )
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

	public class SturdyFryingPan : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefCooking.CraftSystem; } }

		[Constructable]
		public SturdyFryingPan() : base( 0x9E2 )
		{
			Name = "Sturdy Frying Pan";
			Weight = 3.0;
			UsesRemaining = 150;
			Hue = 0x3DA;
		}

		[Constructable]
		public SturdyFryingPan( int uses ) : base( uses, 0x9E2 )
		{

		}

		public SturdyFryingPan( Serial serial ) : base( serial )
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