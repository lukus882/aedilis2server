using System;
using System.Collections.Generic;
using Server;
using Server.Targeting;
using Server.Mobiles;
using Fatima.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x13B2, 0x13B1 )]
	public class Bow : BaseRanged, ITrickBow
	{
		public override int EffectID{ get{ return 0xF42; } }

		private Type m_TrickAmmoType = typeof(Arrow);

		private bool UsingNormalArrows
		{ 
			get
			{
				return m_TrickAmmoType == typeof(Arrow);
			}
		}

		public void ClearTrickAmmo(){ m_TrickAmmoType = typeof(Arrow); }

		public Type TrickAmmoType
		{
			get{ return m_TrickAmmoType; }
			set
			{
				if (value != null)
					m_TrickAmmoType = value;
				else
					m_TrickAmmoType = typeof(Arrow);

				InvalidateProperties();
			}
		}

		public Item TrickArrow
		{
			get
			{
				try
				{
					if (UsingNormalArrows)
						return new Arrow();

					object arrow = Activator.CreateInstance(m_TrickAmmoType);
					if (arrow != null && arrow is Item )
						return (Item)arrow;
				}
				catch{ return new Arrow(); }

				return new Arrow();
			}
		}

		public override Type AmmoType{ get{ return TrickAmmoType; } }
		public override Item Ammo{ get{ return TrickArrow; } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ParalyzingBlow; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.MortalStrike; } }

		public override int AosStrengthReq{ get{ return 30; } }
		public override int AosMinDamage{ get{ return 16; } }
		public override int AosMaxDamage{ get{ return 18; } }
		public override int AosSpeed{ get{ return 25; } }

		public override int OldStrengthReq{ get{ return 20; } }
		public override int OldMinDamage{ get{ return 9; } }
		public override int OldMaxDamage{ get{ return 41; } }
		public override int OldSpeed{ get{ return 20; } }

		public override int DefMaxRange{ get{ return 10; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 60; } }

		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.ShootBow; } }

		[Constructable]
		public Bow() : base( 0x13B2 )
		{
			Layer = Layer.TwoHanded;
			Weight = 6.0;
		}

		public bool ArmDifferentAmmo( TrickArrow arrow, Mobile from )
		{
			if (arrow == null || from == null)
				return false;

			Type t = arrow.GetType();
			bool usable = Bow.ArrowUsable(t, from);

			if (usable)
			{
				this.TrickAmmoType = t;
				from.SendMessage("You have changed to a new type of ammunition.");
			}
			else
				from.SendMessage( "You do not meet the requirements to use this ammunition." );

			return usable;
		}

		public static bool ArrowUsable( Type arrowType, Mobile user )
		{
			if (arrowType == null)
				return false;

			ArrowReq usable = ArrowReq.NotUsable;
			try
			{
				usable = (ArrowReq)arrowType.GetMethod( "CanUse" ).Invoke( null, new object[]{ user } );
			}
			catch 
			{
				//we failed to find the CanUse method. Therefore, we can assume
				//that there exists no requirements, so.. let them use it.

				usable = ArrowReq.NoReq;
			}
			if ( usable == ArrowReq.Usable || usable == ArrowReq.NoReq )
				return true;
			
			return false;
		}

		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties( list );

			if (UsingNormalArrows)
				list.Add( 1060847, "{0}\t{1}", "Can use Special", "Arrows" ); // ~1_val~ ~2_val~
			else
			{
				//attempt to get the name of the arrow we're firing..
				string arrowName = String.Empty;
				try 
				{
					arrowName = m_TrickAmmoType.GetProperty( "ArrowName" ).GetValue(null, null).ToString();
				} catch{ arrowName = "Trick"; }

				list.Add( 1060847, "{0}\t{1}", "Firing: " + arrowName, "Arrows" ); // ~1_val~ ~2_val~
			}
		}

		public override void GetContextMenuEntries( Mobile from, List<Server.ContextMenus.ContextMenuEntry> list )
		{
			base.GetContextMenuEntries(from,list);

			list.Add( new SelectTrickArrowTypeContextMenu( this ) );

			if (!UsingNormalArrows)
				list.Add( new ClearTrickArrowTypeContextMenu( this ) );
		}

		public override bool OnFired( Mobile attacker, Mobile defender )
		{
			Container pack = attacker.Backpack;

			if (pack == null)
				return false;

			if ( !UsingNormalArrows )
			{
				//check if we can fire it or not. They might have lost the skill or whatever to do it.
				bool usable = ArrowUsable( m_TrickAmmoType, attacker );
				if (usable)
				{
					//scan for the amount of arrows they have left. If == 0, then lets switch the arrow type back to basic arrows.
					int arrowsLeft = pack.GetAmount( m_TrickAmmoType, true );
	
					if (arrowsLeft <= 0)
					{
						TrickAmmoType = typeof(Arrow);
						attacker.SendMessage("Your have run out of your special ammunition. Now, you will fire with regular arrows.");
					}
				}
				else
				{
					attacker.SendMessage("You do not meet the requirements to use this ammunition. Oh well. Regular arrows will now be used instead.");
					ClearTrickAmmo(); //reset to Arrow
				}
			}

			if (base.OnFired(attacker,defender))
			{
				if ( m_TrickAmmoType.IsSubclassOf(typeof(TrickArrow)) )
				{
					//lets try to see if we have a method in the arrow code so that we can
					//activate any special effects, the arrow might have.
					try
					{
						m_TrickAmmoType.GetMethod( "OnArrowFired" ).Invoke( null, new object[]{ this, attacker, defender } );
					} catch{}
	
					//Effects.SendMovingEffect( attacker, defender, 0x36D4,6, 0, false,false, 67, 0 ); //Poison Effect!
				}

				return true;
			}
			return false;
		}

		public override void OnHit( Mobile attacker, Mobile defender, double dmgbonus )
		{
			if ( m_TrickAmmoType.IsSubclassOf(typeof(TrickArrow)) )
			{
				//lets try to see if we have a method in the arrow code so that we can
				//activate any special abilities, the arrow might have.
				try
				{
					m_TrickAmmoType.GetMethod( "OnArrowHit" ).Invoke( null, new object[]{ this, attacker, defender } );
				}
				catch// (Exception e)
				{
					//Console.WriteLine( String.Format("CAUGHT Exception at OnArrowHit [arrow type= {0}] invoke...Message: {1}\nStack Trace:\n\n{2}", m_TrickAmmoType.FullName, e.Message, e.StackTrace) );
				}
			}

			base.OnHit(attacker,defender, dmgbonus);
		}

		public Bow( Serial serial ) : base( serial )
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