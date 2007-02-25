using System;
using Server;
using Server.Network;

namespace Server.Items
{
	[FlipableAttribute( 0x1451, 0x1456 )]
	public class HalloweenSkeletonMask : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 3; } }
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 4; } }
		public override int BasePoisonResistance{ get{ return 2; } }
		public override int BaseEnergyResistance{ get{ return 4; } }

		public override int InitMinHits{ get{ return 25; } }
		public override int InitMaxHits{ get{ return 30; } }

		public override int AosStrReq{ get{ return 20; } }
		public override int OldStrReq{ get{ return 40; } }

		public override int ArmorBase{ get{ return 30; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

		[Constructable]
		public HalloweenSkeletonMask() : base( 0x1451 )
		{
			Weight = 3.0;
			Name = "a Screaming Skeleton Mask";
		}

		public HalloweenSkeletonMask( Serial serial ) : base( serial )
		{
		}
		
		public override bool OnEquip( Mobile from )
		{
			from.SendMessage( "You put on your scary screaming skeleton mask." );

			return base.OnEquip( from );
		}

		public override void OnRemoved( object parent )
		{
			if ( parent is Mobile )
			{
				Mobile from = ( Mobile ) parent;
				
				from.SendMessage( "You remove the skeleton mask." );
			}

			base.OnRemoved( parent );
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( Parent != from )
			{
				from.SendMessage( "This must be worn for you to use it." );
			}
			else
			{
				if (from.Female)
				{
					Effects.PlaySound( from.Location, from.Map, 0x32E );
				}
				else
				{
					Effects.PlaySound( from.Location, from.Map, 0x440 );
				}
			}
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}