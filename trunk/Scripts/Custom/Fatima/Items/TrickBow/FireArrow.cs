using System;
using Server;
using Server.Items;

namespace Fatima.Items
{
	public class FireArrow : TrickArrow, ICommodity
	{
		public static string ArrowName{ get{ return "Fire"; } }

		string ICommodity.Description
		{
			get
			{
				return String.Format( Amount == 1 ? "{0} {1} arrow" : "{0} {1} arrows", Amount, ArrowName.ToLower() );
			}
		}

		[Constructable]
		public FireArrow() : this( 1 )
		{
		}

		[Constructable]
		public FireArrow( int amount ) : base( amount )
		{
			Name = "Fire Arrow";
			Stackable = true;
			Weight = 0.1;
			Amount = amount;


			Hue = 0x489; //Fire
		}

		public static void OnArrowFired( ITrickBow bow, Mobile attacker, Mobile defender )
		{
			//attacker.MovingParticles( defender, 0x36E4, 5, 0, false, true, 3006, 4006, 0 );
			attacker.PlaySound( 0x1E5 );
			Effects.SendMovingEffect( attacker, defender, 0x36D4, 6, 0, false,false, 0, 0 ); //2nd to last 0 => COLOR (flame by default)
		}

		public static void OnArrowHit( ITrickBow bow, Mobile attacker, Mobile defender )
		{
			//+5 damage, 100% fire.
			AOS.Damage( defender, attacker, 5, 0, 100, 0, 0, 0 );
		}

		public static ArrowReq CanUse( Mobile user )
		{
			return ArrowReq.Usable;
		}

		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties( list );

			list.Add( 1060847, "{0}\t{1}", "Usable by", "all" ); // ~1_val~ ~2_val~

			list.Add( 1060658, "{0}\t{1}", "Bonus Fire Damage", "+5" ); // ~1_val~: ~2_val~
			//list.Add( 1060659, "{0}\t{1}", "", 100 ); // ~1_val~: ~2_val~
		}

		public FireArrow( Serial serial ) : base( serial )
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