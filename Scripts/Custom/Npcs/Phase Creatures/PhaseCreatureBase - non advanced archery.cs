using System;
using Server.Mobiles;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles
{
	public class PhaseCreatureBase : BaseCreature
	{
		public PhaseCreatureBase() : base( AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Hue = 2997;
		}

		public PhaseCreatureBase(AIType aiType) : base( aiType, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Hue = 2997;
		}

		public PhaseCreatureBase(AIType aiType, FightMode mode) : base( aiType, mode, 10, 1, 0.2, 0.4 )
		{
			Hue = 2997;
		}

		public PhaseCreatureBase(AIType aiType, FightMode mode, int rper, int rfight, double aspeed, double pspeed) : base( aiType, mode, rper, rfight, aspeed, pspeed )
		{
			Hue = 2997;
		}

		public override void AlterMeleeDamageFrom( Mobile from, ref int damage )
		{
			if ( from != null && from != this )
			{
				if (from is PlayerMobile)
				{
					PlayerMobile p_PlayerMobile = from as PlayerMobile;
					Item weapon1 = p_PlayerMobile.FindItemOnLayer( Layer.OneHanded );
					Item weapon2 = p_PlayerMobile.FindItemOnLayer( Layer.TwoHanded );

					if (weapon1 != null)
					{
						if (weapon1 is BaseRanged )
						{
							damage = 0; // Immune to range attacks
							from.SendMessage("Range Attacks seem to pass right through him");
						}
						else
						{
							if (Utility.RandomBool())
							{
								damage = 0;
								from.SendMessage("Your weapon seamed to pass right through them");
							}
						}
					}

					else if (weapon2 != null)
					{
						if (weapon2 is BaseRanged )
						{
							damage = 0; // Immune to range attacks
							from.SendMessage("Range Attacks seem to pass right through him");
						}
						else
						{
							if (Utility.RandomBool())
							{
								damage = 0;
								from.SendMessage("Your weapon seamed to pass right through them");
							}
						}
					}
					else
					{
						if (Utility.RandomBool())
						{
							damage = 0;
							from.SendMessage("Your weapon seamed to pass right through them");
						}
					}
				}
				else
				{
					if (Utility.RandomBool())
					{
						damage = 0;
					}
				}
			}
		}

		public override void AlterSpellDamageFrom( Mobile from, ref int damage ) // was public virtual void
		{
			if ( from != null && from != this )
			{
				if (from is PlayerMobile)
				{
					if (Utility.RandomBool())
					{
						damage = 0;
						from.SendMessage("It seamed to have phased out when your spell hit.");
					}
				}
				else
				{
					if (Utility.RandomBool())
					{
						damage = 0;
					}
				}
			}
		}

	/*	public virtual void AlterDamageScalarFrom( Mobile from, ref double scalar )
		{
			if ( from != null && from != this )
			{
				if (from is PlayerMobile)
				{
					if (Utility.RandomBool())
					{
						scalar = 0.001;
						from.SendMessage("It seamed to have phased out when your spell hit.");
					}
				}
				else
				{
					if (Utility.RandomBool())
					{
						scalar = 0.001;
					}
				}
			}
		}*/

		public PhaseCreatureBase(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
}