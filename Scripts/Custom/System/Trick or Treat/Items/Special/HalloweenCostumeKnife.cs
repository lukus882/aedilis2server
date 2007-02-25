using System;
using Server.Network;
using Server.Items;
using System.Collections;

namespace Server.Items
{
	[FlipableAttribute( 0x13F6, 0x13F7 )]
	public class HalloweenCostumeKnife : BaseKnife
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.InfectiousStrike; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.Disarm; } }

		public override int AosStrengthReq{ get{ return 5; } }
		public override int AosMinDamage{ get{ return 9; } }
		public override int AosMaxDamage{ get{ return 11; } }
		public override int AosSpeed{ get{ return 49; } }

		public override int OldStrengthReq{ get{ return 5; } }
		public override int OldMinDamage{ get{ return 2; } }
		public override int OldMaxDamage{ get{ return 14; } }
		public override int OldSpeed{ get{ return 40; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 40; } }

		[Constructable]
		public HalloweenCostumeKnife() : base( 0x13F6 )
		{
			Weight = 1.0;
			Name = "a Halloween Bleeding Costume Knife";
			Hue = 138;
		}

		public HalloweenCostumeKnife( Serial serial ) : base( serial )
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
		
		public override bool OnEquip( Mobile from )
		{

			from.SendMessage( "You equip your hallowenn costume knife." );
				
			BeginBleed( from );
	
			return base.OnEquip( from );
		}

		public override void OnRemoved( object parent )
		{
			if ( parent is Mobile )
			{
				Mobile from = ( Mobile ) parent;
				
				EndBleed ( from );
				
				from.SendMessage( "You unequip your halloween costume knife." );
			}

			base.OnRemoved( parent );
		}

		private static Hashtable m_Table = new Hashtable();

		public static bool IsBleeding( Mobile m )
		{
			return m_Table.Contains( m );
		}
		
		public static void BeginBleed( Mobile m  )
		{
			Timer t = (Timer)m_Table[m];

			if ( t != null )
				t.Stop();

			t = new InternalTimer( m );
			m_Table[m] = t;

			t.Start();
		}

		public static void DoBleed( Mobile m )
		{
			Blood blood = new Blood();
			blood.ItemID = Utility.Random( 0x122A, 5 );
			blood.MoveToWorld( m.Location, m.Map );
		}

		public static void EndBleed( Mobile m )
		{
			Timer t = (Timer)m_Table[m];

			if ( t == null )
				return;

			t.Stop();
			m_Table.Remove( m );
		}

		private class InternalTimer : Timer
		{
			private Mobile m_From;
			private int m_Count;

			public InternalTimer( Mobile from ) : base( TimeSpan.FromSeconds( 2.0 ), TimeSpan.FromSeconds( 2.0 ) )
			{
				m_From = from;
				Priority = TimerPriority.TwoFiftyMS;
			}

			protected override void OnTick()
			{
				DoBleed( m_From );
			}
		}
	}
}