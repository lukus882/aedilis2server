using System;
using Server;
using Server.Mobiles;
using Server.Spells;

namespace Server.Items
{
    public class AdvancedRobotInstructions : Item
	{
		public override string DefaultName
		{
			get { return "Advanced Robot Building Instructions"; }
		}

		[Constructable]
		public AdvancedRobotInstructions() : base( 8792 )
		{
			Weight = 5.0;
			Hue = 73;
		}

		public AdvancedRobotInstructions( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
				return;
			}

			double tinkerSkill = from.Skills[SkillName.Tinkering].Value;

			if ( tinkerSkill < 90.0 )
			{
				from.SendMessage( "You lack the tinkering to make sence of anything on here." );
				return;
			}
			else if ( (from.Followers + 5) > from.FollowersMax )
			{
				from.SendLocalizedMessage( 1049607 ); // You have too many followers to control that creature.
				return;
			}

			double scalar;

			if ( tinkerSkill >= 100.0 )
				scalar = 1.0;
			else if ( tinkerSkill >= 90.0 )
				scalar = 0.9;
			else if ( tinkerSkill >= 80.0 )
				scalar = 0.8;
			else if ( tinkerSkill >= 70.0 )
				scalar = 0.7;
			else
				scalar = 0.6;

			Container pack = from.Backpack;

			if ( pack == null )
				return;

			int res = pack.ConsumeTotal(
				new Type[]
				{
					typeof( PowerCrystal ),
					typeof( IronIngot ),
					typeof( BronzeIngot ),
					typeof( Gears )
				},
				new int[]
				{
					5,
					200,
					300,
					15
				} );

			switch ( res )
			{
				case 0:
				{
					from.SendMessage( "You must have 5 power crystals to construct the golem." );
					break;
				}
				case 1:
				{
					from.SendMessage( "You must have 200 iron ingots to construct the golem." );
					break;
				}
				case 2:
				{
					from.SendMessage( "You must have 300 bronze ingots to construct the golem." );
					break;
				}
				case 3:
				{
					from.SendMessage( "You must have 15 gears to construct the golem." );
					break;
				}
				default:
				{
					Robot g = new Robot( true, scalar );

					if ( g.SetControlMaster( from ) )
					{
						Delete();

						g.MoveToWorld( from.Location, from.Map );
						from.PlaySound( 0x241 );
					}

					break;
				}
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}