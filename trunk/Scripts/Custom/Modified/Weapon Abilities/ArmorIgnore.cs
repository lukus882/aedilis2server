// $Id: //depot/c%23/RunUO Core Scripts/RunUO Core Scripts/Items/Weapons/Abilities/ArmorIgnore.cs#3 $

using System;

namespace Server.Items
{
	/// <summary>
	/// This special move allows the skilled warrior to bypass his target's physical resistance, for one shot only.
	/// The Armor Ignore shot does slightly less damage than normal.
	/// Against a heavily armored opponent, this ability is a big win, but when used against a very lightly armored foe, it might be better to use a standard strike!
	/// </summary>
	public class ArmorIgnore : WeaponAbility
	{
		public ArmorIgnore()
		{
		}

		public override int BaseMana { get { return 30; } }
		public override double DamageScalar { get { return 0.9; } }

		public override bool OnBeforeSwing(Mobile attacker, Mobile defender, bool validate)
		{
			if (validate && (!Validate(attacker) || !CheckMana(attacker, true)))
				return false;
			else
				return true;
		}

		public override void OnHit(Mobile attacker, Mobile defender, int damage)
		{
			ClearCurrentAbility(attacker);

			attacker.SendLocalizedMessage(1060076); // Your attack penetrates their armor!
			defender.SendLocalizedMessage(1060077); // The blow penetrated your armor!

			defender.PlaySound(0x56);
			defender.FixedParticles(0x3728, 200, 25, 9942, EffectLayer.Waist);
		}
	}
}
