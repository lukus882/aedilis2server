using System;
using Server.Items;

namespace Server.Items
{

	public class ItemRemovalTimer : Timer 
	{ 
		private Item i_item; 
		public ItemRemovalTimer( Item item ) : base( TimeSpan.FromDays( 7.0 ) ) 
		{ 
			Priority = TimerPriority.OneSecond; 
			i_item = item; 
		} 

		protected override void OnTick() 
		{ 
		        if (( i_item != null ) && ( !i_item.Deleted )) 
			        i_item.Delete(); 
		} 
	} 

	public class CorpseGrave : Item
	{
		public CorpseGrave() : base( 0x3679 )
		{
			Movable = false;
			Weight = 10.0;
			ItemRemovalTimer thisTimer = new ItemRemovalTimer( this ); 
			thisTimer.Start(); 
		}

		public CorpseGrave( Serial serial ) : base( serial )
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