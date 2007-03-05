using System;
using Server;
using Server.Items;


namespace Server.Items
{
	public class EventBag : Bag
	{
		[Constructable]
		public EventBag () : this( 1 )
		{
		}
		
		[Constructable]
		public EventBag ( int amount )
		{
			Name = "Bag Of Belongings";
			Hue = 1153;
		}
		
		public EventBag ( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( ( int ) 0 ); // version
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}

