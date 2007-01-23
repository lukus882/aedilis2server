using System;
using System.Collections;
using System.Collections.Generic;
using Server.Multis;

namespace Server.Items
{
	public class TrashPack : Container
	{

		public override int MaxWeight{ get{ return 0; } } // A value of 0 signals unlimited weight

		public override int DefaultGumpID{ get{ return 0x3C; } }
		public override int DefaultDropSound{ get{ return 0x42; } }

		public override Rectangle2D Bounds
		{
			get{ return new Rectangle2D( 44, 65, 142, 94 ); }
		}

	public override bool IsDecoContainer
		{
			get{ return false; }
		}

		[Constructable]
		public TrashPack() : base( 0x9B2 )
		{
            Name = "Trash Bag";
			Hue = 1166;
			Movable = true;
		}

		public TrashPack( Serial serial ) : base( serial )
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

			if ( Items.Count > 0 )
			{
				m_Timer = new EmptyTimer( this );
				m_Timer.Start();
			}
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if ( !base.OnDragDrop( from, dropped ) )
				return false;

			if ( TotalItems >= 10 )
			{
				Empty( 501478 ); // The trash is full!  Emptying!
			}
			else
			{
				from.SendMessage( "Items will delete in 30 seconds!" ); // The item will be deleted in three minutes

				if ( m_Timer != null )
					m_Timer.Stop();
				else
					m_Timer = new EmptyTimer( this );

				m_Timer.Start();
			}

			return true;
		}

		public override bool OnDragDropInto( Mobile from, Item item, Point3D p )
		{
			if ( !base.OnDragDropInto( from, item, p ) )
				return false;

			if ( TotalItems >= 10 )
			{
				Empty( 501478 ); // The trash is full!  Emptying!
			}
			else
			{
				from.SendMessage( "Items will delete in 30 sconds!" ); // The item will be deleted in three minutes

				if ( m_Timer != null )
					m_Timer.Stop();
				else
					m_Timer = new EmptyTimer( this );

				m_Timer.Start();
			}

			return true;
		}

	
		public void Empty( int message )
		{
			List<Item> items = this.Items;

			if ( items.Count > 0 )
			{
				PublicOverheadMessage( Network.MessageType.Regular, 0x3B2, message, "" );

				for ( int i = items.Count - 1; i >= 0; --i )
				{
					if ( i >= items.Count )
						continue;

					items[i].Delete();
				}
			}

			if ( m_Timer != null )
				m_Timer.Stop();

			m_Timer = null;
		}

		private Timer m_Timer;

		private class EmptyTimer : Timer
		{
			private TrashPack m_Pack;

			public EmptyTimer( TrashPack pack ) : base( TimeSpan.FromMinutes( .5 ) )
			{
				m_Pack = pack;
				Priority = TimerPriority.FiveSeconds;
			}

			protected override void OnTick()
			{
				m_Pack.Empty( 501479 ); // Emptying the trashcan!
			}
		}
	}
}
