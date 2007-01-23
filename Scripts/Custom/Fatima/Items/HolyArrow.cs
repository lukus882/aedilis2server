using System;
using Server;
using Server.Items;

namespace Fatima.Items
{
	public class HolyArrow : TrickArrow, ICommodity
	{
		public static string ArrowName{ get{ return "Holy"; } }

		string ICommodity.Description
		{
			get
			{
				return String.Format( Amount == 1 ? "{0} {1} arrow" : "{0} {1} arrows", Amount, ArrowName.ToLower() );
			}
		}

		[Constructable]
		public HolyArrow() : this( 1 )
		{
		}

		[Constructable]
		public HolyArrow( int amount ) : base( amount )
		{
			Name = "Holy Arrow";
			Stackable = true;
			Weight = 0.1;
			Amount = amount;


			Hue = 0x481; //Fire
		}

		private static bool IsUndead( Mobile npc )
		{
			return SlayerGroup.GetEntryByName( SlayerName.Silver ).Slays( npc ); 
		}

		public static void OnArrowFired( TrickBow bow, Mobile attacker, Mobile defender )
		{
			if (IsUndead(defender))
			{
				attacker.PlaySound( 0x1E5 );
				Effects.SendMovingEffect( attacker, defender, 0x36D4, 6, 0, false,false, 0x481, 0 ); //2nd to last 0 => COLOR (flame by default)
			}
		}

		public static void OnArrowHit( TrickBow bow, Mobile attacker, Mobile defender )
		{
			if (IsUndead(defender))
			{
				//+5 damage, 100% fire.
				//AOS.Damage( defender, attacker, 5, 0, 100, 0, 0, 0 );
				defender.Damage( 15, attacker ); //raw damage.
			}
		}

		public static ArrowReq CanUse( Mobile user )
		{
			return (user.Skills[SkillName.Healing].Value >= 65 && user.Skills[SkillName.Archery].Value >= 100) ? ArrowReq.Usable : ArrowReq.NotUsable;
		}

		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties( list );

			list.Add( 1060658, "{0}\t{1}", "Bonus Holy Damage", "+15" ); // ~1_val~: ~2_val~
			//list.Add( 1060659, "{0}\t{1}", "", 100 ); // ~1_val~: ~2_val~

			list.Add( 1060660, "{0}\t{1}", "Archery Required ", 100 ); // ~1_val~: ~2_val~
			list.Add( 1060661, "{0}\t{1}", "Healing Required ", 65 ); // ~1_val~: ~2_val~
		}

		public HolyArrow( Serial serial ) : base( serial )
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