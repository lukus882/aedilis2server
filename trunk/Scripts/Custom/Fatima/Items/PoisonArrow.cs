using System;
using Server;
using Server.Items;

namespace Fatima.Items
{
	public class PoisonArrow : TrickArrow, ICommodity
	{
		public static string ArrowName{ get{ return "Poison"; } }

		string ICommodity.Description
		{
			get
			{
				return String.Format( Amount == 1 ? "{0} {1} arrow" : "{0} {1} arrows", Amount, ArrowName.ToLower() );
			}
		}

		[Constructable]
		public PoisonArrow() : this( 1 )
		{
		}

		[Constructable]
		public PoisonArrow( int amount ) : base( amount )
		{
			Name = "Poison Arrow";
			Stackable = true;
			Weight = 0.1;
			Amount = amount;

			Hue = 67; //Green
		}

		public static void OnArrowFired( TrickBow bow, Mobile attacker, Mobile defender )
		{
			//attacker.MovingParticles( defender, 0x36E4, 5, 0, false, true, 3006, 4006, 0 );
			attacker.PlaySound( 0x1E5 );
			Effects.SendMovingEffect( attacker, defender, 0x36D4, 6, 0, false,false, 67, 0 ); //2nd to last 0 => COLOR (flame by default)
		}

		public static void OnArrowHit( TrickBow bow, Mobile attacker, Mobile defender )
		{
			//+8 damage, 100% poison.
			//( Mobile m, Mobile from, int damage, int phys, int fire, int cold, int pois, int nrgy )
			AOS.Damage( defender, attacker, 8, 0, 0, 0, 100, 0 );

			if (10 >= Utility.RandomMinMax(1,100))
			{ //Poison them! Muhaha..
				defender.FixedParticles( 0x374A, 10, 15, 5021, EffectLayer.Waist );
				defender.PlaySound( 0x474 );
				defender.ApplyPoison( attacker, Poison.Regular );
			}
		}

		public static ArrowReq CanUse( Mobile user )
		{
			return (user.Skills[SkillName.Poisoning].Value >= 65 && user.Skills[SkillName.Archery].Value >= 100) ? ArrowReq.Usable : ArrowReq.NotUsable;
		}

		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties( list );

			list.Add( 1060658, "{0}\t{1}", "Regular Poison Chance", "10%" ); // ~1_val~: ~2_val~
			list.Add( 1060659, "{0}\t{1}", "Bonus Poison Damage", "+8" ); // ~1_val~: ~2_val~
			//list.Add( 1060659, "{0}\t{1}", "", 100 ); // ~1_val~: ~2_val~

			list.Add( 1060660, "{0}\t{1}", "Archery Required ", 100 ); // ~1_val~: ~2_val~
			list.Add( 1060661, "{0}\t{1}", "Poisoning Required ", 65 ); // ~1_val~: ~2_val~
		}

		public PoisonArrow( Serial serial ) : base( serial )
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