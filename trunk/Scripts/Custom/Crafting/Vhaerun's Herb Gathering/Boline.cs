//	Vhaerun's Herb Gathering 1.0	//

using System;
using Server.Network;
using Server.Targeting;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0xEC5, 0xEC4 )]
	public class Boline : BaseBoline
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.InfectiousStrike; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.ShadowStrike; } }

		public override int AosStrengthReq{ get{ return 10; } }
		public override int AosMinDamage{ get{ return 6; } }
		public override int AosMaxDamage{ get{ return 8; } }
		public override int AosSpeed{ get{ return 56; } }

		public override int OldStrengthReq{ get{ return 1; } }
		public override int OldMinDamage{ get{ return 3; } }
		public override int OldMaxDamage{ get{ return 15; } }
		public override int OldSpeed{ get{ return 55; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 40; } }

		public override SkillName DefSkill{ get{ return SkillName.Fencing; } }
		public override WeaponType DefType{ get{ return WeaponType.Piercing; } }
		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Pierce1H; } }

		[Constructable]
		public Boline() : base( 0xEC5 )
		{
			Name = "boline";
			Hue = 0x36C;
			Weight = 1.0;
		}

		public Boline( Serial serial ) : base( serial )
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