using System;
using Server;
using Server.Mobiles;
using Server.Spells;

namespace Server.Items
{
	public class NecroGreaterSkeletonEssence : Item
	{
		[Constructable]
		public NecroGreaterSkeletonEssence() : base( 0x186F )
		{
			Weight = 1.0;
			Name = "Greater Skeleton Essence";
			Hue = 0x2EF;
		}

		public NecroGreaterSkeletonEssence( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			double minSkill = 90.0;

			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
				return;
			}

			double necromancySkill = from.Skills[SkillName.Necromancy].Value;

			if ( necromancySkill < 90.0 )
			{
				from.SendMessage( "You do not have the knowledge required to attempt this." );
				return;
			}
			else if ( (from.Followers + 1) > from.FollowersMax )
			{
				from.SendLocalizedMessage( 1049607 ); // You have too many followers to control that creature.
				return;
			}

			double scalar;

			if ( necromancySkill >= 100.0 )
				scalar = 1.0;
			else if ( necromancySkill >= 90.0 )
				scalar = 0.8;
			else if ( necromancySkill >= 80.0 )
				scalar = 0.6;
			else
				scalar = 0.5;

			double maxSkill = minSkill + 60.0;

			if ( from.Mounted )
			{
				from.SendMessage( "You cannot summon while mounted." );
				return;
			}

				from.Animate( 17, 7, 1, true, false, 0 );

				if ( !from.CheckSkill( SkillName.Necromancy, minSkill, maxSkill ) )
				{
					from.PlaySound( 0x241A );
					from.SendMessage( "Your summoning has failed." );
					Delete();
					return;
				}
				else
				{
					NecroGreaterSkeleton m = new NecroGreaterSkeleton( true, scalar );

					if ( m.SetControlMaster( from ) )
					{
						Delete();

						m.MoveToWorld( from.Location, from.Map );
						from.PlaySound( 0x24A );
						from.SendMessage( "You have summoned a greater skeleton." );
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