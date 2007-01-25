using System;

namespace Server.Items
{
	public class SkullOfDeath : BaseTrap
	{
		[Constructable]
		public SkullOfDeath() : base( 0x1854 )
		{
		}

		public override bool PassivelyTriggered{ get{ return true; } }
		public override TimeSpan PassiveTriggerDelay{ get{ return TimeSpan.FromSeconds( 2.0 ); } }
		public override int PassiveTriggerRange{ get{ return 0; } }
		public override TimeSpan ResetDelay{ get{ return TimeSpan.FromSeconds( 0.5 ); } }

		public override void OnTrigger( Mobile from )
		{
			Visible = false;
			if ( from.AccessLevel > AccessLevel.Player )
				return;


			if ( from.Alive && CheckRange( from.Location, 0 ) )
				Spells.SpellHelper.Damage( TimeSpan.FromSeconds( 0.5 ), from, from, Utility.RandomMinMax( 500, 500 ), 500, 500, 500, 500, 500 );
		}

		public SkullOfDeath( Serial serial ) : base( serial )
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