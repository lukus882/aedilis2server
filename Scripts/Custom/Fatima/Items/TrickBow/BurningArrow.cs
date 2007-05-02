using System;
using Server;
using Server.Items;
using Fatima.Misc;

namespace Fatima.Items
{
	public class BurningArrow : TrickArrow, ICommodity
	{
		private static DotTickEventHandler m_DotEvent;
		public static string ArrowName{ get{ return "Burning"; } }

		string ICommodity.Description
		{
			get
			{
				return String.Format( Amount == 1 ? "{0} {1} arrow" : "{0} {1} arrows", Amount, ArrowName.ToLower() );
			}
		}

		static BurningArrow()
		{
			m_DotEvent += new DotTickEventHandler( OnDotTick );
		}

		public static void OnDotTick( DotTickEventArgs args )
		{
			if (!args.End)
			{
				args.Defender.FixedParticles( 0x19AB, 10, 30, 5052, EffectLayer.Waist );
				args.Defender.FixedParticles( 0x19AB, 10, 30, 5052, EffectLayer.Head );
				args.Defender.FixedParticles( 0x19AB, 10, 30, 5052, EffectLayer.LeftFoot );
				args.Defender.SendMessage("You are burning!!");
			}
			else
				args.Defender.SendMessage("The burning flames die out.");
		}

		[Constructable]
		public BurningArrow() : this( 1 )
		{
		}

		[Constructable]
		public BurningArrow( int amount ) : base( amount )
		{
			Name = "Burning Arrow";
			Stackable = true;
			Weight = 0.1;
			Amount = amount;

			Hue = 0x489; //Glowy Yellowish
		}

		public static void OnArrowFired( ITrickBow bow, Mobile attacker, Mobile defender )
		{
			//attacker.MovingParticles( defender, 0x36E4, 5, 0, false, true, 3006, 4006, 0 );
			attacker.PlaySound( 0x1E5 );
			Effects.SendMovingEffect( attacker, defender, 0x36D4, 6, 0, false,false, 0, 0 ); //2nd to last 0 => COLOR (flame by default)
		}

		public static void OnArrowHit( ITrickBow bow, Mobile attacker, Mobile defender )
		{
			if (15 >= Utility.RandomMinMax(1,100))
			{ //Make them Burn! Muhaha..
				attacker.SendMessage("Your burning arrow has set your enemy on fire!");
				defender.SendMessage( String.Format("{0}'s burning arrow has set you on fire!", attacker.Name) );

				AOSDamageEntry dmgEntry = new AOSDamageEntry( defender, attacker, 10, 0, 100, 0, 0, 0 );
				DoTDamageEntry dotDMG = new DoTDamageEntry( m_DotEvent, true, 5, TimeSpan.FromSeconds( 25 ), dmgEntry );

				DoTHandler.NewDoTDamage( typeof( BurningArrow ), dotDMG );
			}
			else
			{
				AOS.Damage( defender, attacker, 5, 0, 100, 0, 0, 0 );
			}
		}

		public static ArrowReq CanUse( Mobile user )
		{
			return (user.Skills[SkillName.Magery].Value >= 65 && user.Skills[SkillName.Archery].Value >= 100) ? ArrowReq.Usable : ArrowReq.NotUsable;
		}

		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties( list );

			list.Add( 1060658, "{0}\t{1}", "Bonus Fire Damage", "+5" ); // ~1_val~: ~2_val~
			list.Add( 1060659, "{0}\t{1}", "15% Chance to Burn", "50 damage over 25 seconds" ); // ~1_val~: ~2_val~

			list.Add( 1060660, "{0}\t{1}", "Archery Required ", 100 ); // ~1_val~: ~2_val~
			list.Add( 1060661, "{0}\t{1}", "Magery Required ", 65 ); // ~1_val~: ~2_val~
		}

		public BurningArrow( Serial serial ) : base( serial )
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