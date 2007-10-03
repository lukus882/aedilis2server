using System;
using System.Collections;
using Server.Regions;
using Server.Mobiles;

namespace Server.Items
{
	public class SmellyPumpkinBombS : Item
	{
		[Constructable]
		public SmellyPumpkinBombS() : base( 0xC6C )
		{
		      	Weight = 1.0; Name = "Smelly Pumpkin";
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) ) from.SendLocalizedMessage( 1042001 );
			else if ( from.Region is TownRegion ) { from.SendMessage( "You are not allowed to do that in town" ); }
			else if ( from.Region.Name == "Tele Center Tram" || from.Region.Name == "Tele Center Fel" ) { from.SendMessage( "You are not allowed to do that in the Tele Center" ); }
			else
			{
				from.SendMessage("You throw the pumpkin at your feet lett off a clound of smoke through out the area");
				foreach ( Mobile mobile in from.GetMobilesInRange( 12 ) )
				{
					if ( mobile != null && mobile.AccessLevel < AccessLevel.GameMaster && from.CanBeHarmful(mobile) )
					{
						mobile.Say("*cough cough*");
						mobile.Poison = Poison.Regular;
					}
				}
				this.Delete();
			}
		}

		public SmellyPumpkinBombS( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class SmellyPumpkinBombM : Item
	{
		[Constructable]
		public SmellyPumpkinBombM() : base( 0xC6A )
		{
		      	Weight = 1.0; Name = "Smelly Pumpkin";
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) ) from.SendLocalizedMessage( 1042001 );
			else if ( from.Region is TownRegion ) { from.SendMessage( "You are not allowed to do that in town" ); }
			else if ( from.Region.Name == "Tele Center Tram" || from.Region.Name == "Tele Center Fel" ) { from.SendMessage( "You are not allowed to do that in the Tele Center" ); }
			else
			{
				from.SendMessage("You throw the pumpkin at your feet lett off a clound of smoke through out the area");
				foreach ( Mobile mobile in from.GetMobilesInRange( 12 ) )
				{
					if ( mobile != null && mobile.AccessLevel < AccessLevel.GameMaster && from.CanBeHarmful(mobile) )
					{
						mobile.Say("*cough cough*");
						mobile.Poison = Poison.Greater;
					}
				}
				this.Delete();
			}
		}

		public SmellyPumpkinBombM( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class SmellyPumpkinBombL : Item
	{
		[Constructable]
		public SmellyPumpkinBombL() : base( 0x3C10 )
		{
		      	Weight = 1.0; Name = "Smelly Pumpkin";
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) ) from.SendLocalizedMessage( 1042001 );
			else if ( from.Region is TownRegion ) { from.SendMessage( "You are not allowed to do that in town" ); }
			else if ( from.Region.Name == "Tele Center Tram" || from.Region.Name == "Tele Center Fel" ) { from.SendMessage( "You are not allowed to do that in the Tele Center" ); }
			else
			{
				from.SendMessage("You throw the pumpkin at your feet lett off a clound of smoke through out the area");
				foreach ( Mobile mobile in from.GetMobilesInRange( 12 ) )
				{
					if ( mobile != null && mobile.AccessLevel < AccessLevel.GameMaster && from.CanBeHarmful(mobile) )
					{
						mobile.Say("*cough cough*");
						mobile.Poison = Poison.Deadly;
					}
				}
				this.Delete();
			}
		}

		public SmellyPumpkinBombL( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
}