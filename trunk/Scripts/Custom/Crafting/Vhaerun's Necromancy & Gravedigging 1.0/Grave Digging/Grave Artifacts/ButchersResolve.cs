using System;
using Server;

namespace Server.Items
{
	public class ButchersResolve : Cleaver
	{
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public ButchersResolve()
		{
			Name = "the butchers resolve";
			Hue = 1157;
			SkillBonuses.SetValues( 0, SkillName.Cooking, 20.0 );
			PoisonCharges = 100;
			Poison = Poison.Lethal;
			Attributes.WeaponSpeed = 35;
			Attributes.WeaponDamage = 75;
			Attributes.SpellChanneling = 1;

			Identified = true;
		}

		public ButchersResolve( Serial serial ) : base( serial )
		{
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