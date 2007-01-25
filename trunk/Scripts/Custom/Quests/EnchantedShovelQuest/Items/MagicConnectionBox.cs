using System;
using Server;
using Server.Gumps;
using Server.Network;
using System.Collections;
using Server.Multis;
using Server.Mobiles;


namespace Server.Items
{

	public class MagicConnectionBox : Item
	{
		[Constructable]
		public MagicConnectionBox() : this( null )
		{
		}

		[Constructable]
		public MagicConnectionBox ( string name ) : base ( 0x9A8 )
		{
			Name = "A Magical Connection Box";
			LootType = LootType.Blessed;
			Hue = 1581;
		}

		public MagicConnectionBox ( Serial serial ) : base ( serial )
		{
		}

      		
		public override void OnDoubleClick( Mobile m )

		{
			Item a = m.Backpack.FindItemByType( typeof(EnchantedShovelHead) );
			if ( a != null )
			{	
			Item b = m.Backpack.FindItemByType( typeof(EnchantedShovelArm) );
			if ( b != null )
			{
			Item c = m.Backpack.FindItemByType( typeof(EnchantedShovelHandle) );
			if ( c != null )
			{
			
				m.AddToBackpack( new UnchargedEnchantedShovel() );
				a.Delete();
				b.Delete();
				c.Delete();
				m.SendMessage( "The Box Glows and Spits Out A Shovel" );
				this.Delete();
			}
			}
				else
			{
				m.SendMessage( "Are You Not Forgetting Something?" );
		}
		}
		}
		
		

		public override void Serialize ( GenericWriter writer)
		{
			base.Serialize ( writer );

			writer.Write ( (int) 0);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize ( reader );

			int version = reader.ReadInt();
		}
	}
}