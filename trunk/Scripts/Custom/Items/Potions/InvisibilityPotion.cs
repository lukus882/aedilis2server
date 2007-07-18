using System;
using Server;
using System.Collections;

namespace Server.Items
{
	public class InvisibilityPotion : BasePotion
	{
		public override int LabelNumber{ get{ return 1072941; } } // Potion of Invisibility
		
		
		[Constructable]
		public InvisibilityPotion() : base( 0xF0E, PotionEffect.Invisibility )
		{
			Hue = 0x25A;
		}

		public InvisibilityPotion( Serial serial ) : base( serial )
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

		public override void Drink( Mobile m )
		{
			TimeSpan duration = TimeSpan.FromMinutes( 1 );
			
			if (m.Hidden == false)
			{
				m.FixedParticles( 0x376A, 9, 32, 5007, EffectLayer.Waist );
				m.PlaySound( 0x3C4 );
				
				BuffInfo.RemoveBuff( m, BuffIcon.HidingAndOrStealth );
				BuffInfo.AddBuff( m, new BuffInfo( BuffIcon.Invisibility, 1075825 ) );	//Invisibility/Invisible

				BasePotion.PlayDrinkEffect( m );

				this.Consume();
				
				m.Hidden = true;
				
				RemoveTimer( m );
				
				Timer t = new InternalTimer( m, duration );

				m_Table[m] = t;

				t.Start();
				
			}
			else
			{
				m.SendMessage( "An invisibility potion is already taking effect on your person." ); //An invisibility potion is already taking effect on your person.
			}
		}
		
		private static Hashtable m_Table = new Hashtable();
		
		public static bool HasTimer( Mobile m )
		{
			return m_Table[m] != null;
		}

		public static void RemoveTimer( Mobile m )
		{
			Timer t = (Timer)m_Table[m];

			if ( t != null )
			{
				t.Stop();
				m_Table.Remove( m );
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_Mobile;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( duration )
			{
				Priority = TimerPriority.OneSecond;
				m_Mobile = m;
			}

			protected override void OnTick()
			{
				m_Mobile.RevealingAction();
				RemoveTimer( m_Mobile );
				m_Mobile.SendMessage( "The potion loses its effect, and you are revealed." );
				m_Mobile.Hidden = false;
			}
		}
	}
}
