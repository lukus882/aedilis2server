using System; 
using Server; 
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{ 
	public class BagOfJellyBeans : Bag 
	{ 
		[Constructable] 
		public BagOfJellyBeans() : this( 1 ) 
		{ 
			Movable = true; 
            Hue = Utility.RandomList( 0x36, 0x17, 0x120, 0xD2, 0xC1, 0x2C );
			Name = "A Bag Of Jelly Beans";
		} 
		
		[Constructable]
		public BagOfJellyBeans( int amount )
		{
			DropItem( new JellyBean1( 5 ) );
			DropItem( new JellyBean2( 5 ) );		   
			DropItem( new JellyBean3( 5 ) );		   
			DropItem( new JellyBean4( 5 ) );	
			DropItem( new JellyBean5( 5 ) );
			DropItem( new JellyBean6( 5 ) );
			DropItem( new JellyBean7( 5 ) );	
			DropItem( new JellyBean8( 5 ) );		   
		}

		public BagOfJellyBeans( Serial serial ) : base( serial ) 
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