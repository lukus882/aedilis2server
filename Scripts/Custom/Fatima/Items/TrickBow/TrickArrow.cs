using System;
using Server;
using Server.Items;

namespace Fatima.Items
{
	public abstract class TrickArrow : Item, ICommodity
	{
		private static string ArrowName{ get{ return "Trick"; } }

		string ICommodity.Description
		{
			get
			{
				return String.Format( Amount == 1 ? "{0} {1} arrow" : "{0} {1} arrows", Amount, ArrowName.ToLower() );
			}
		}

		public TrickArrow() : this( 1 )
		{
		}

		public TrickArrow( int amount ) : base( 0xF3F )
		{
			Stackable = true;
			Weight = 0.1;
			Amount = amount;
		}

		public override void OnDoubleClick( Mobile from )
		{
			Item item = null;

			item = from.FindItemOnLayer( Layer.TwoHanded );
			if (item != null && item is ITrickBow)
			{
				((ITrickBow)item).ArmDifferentAmmo( this, from );
				return;
			}

			item = from.FindItemOnLayer( Layer.OneHanded );
			if (item != null && item is ITrickBow)
			{
				((ITrickBow)item).ArmDifferentAmmo( this, from );
				return;
			}
		}

		/* THESE PROPERTIES SHOULD EXIST IN CHILD ARROW CLASSES!!
			public static void OnArrowFired( ITrickBow bow, Mobile attacker, Mobile defender )
			{
			}
	
			public static void OnArrowHit( ITrickBow bow, Mobile attacker, Mobile defender )
			{
			}
	
			public static ArrowReq CanUse( Mobile user )
			{
				return ArrowReq.Usable;
			}
		*/

		public TrickArrow( Serial serial ) : base( serial )
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