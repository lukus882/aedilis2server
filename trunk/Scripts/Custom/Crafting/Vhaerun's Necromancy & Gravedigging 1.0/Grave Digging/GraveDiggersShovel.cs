using System;
using Server;
using Server.Gumps; 
using Server.Network; 
using Server.Misc; 
using Server.Mobiles;
using Server.Targeting;
using Server.Items;

namespace Server.Items
{
	public class GraveDiggersShovel : Item
	{
		private int m_Uses;
		private bool m_IsDigging;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Uses
		{
			get{ return m_Uses; }
			set{ m_Uses = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool IsDigging
		{
			get{ return m_IsDigging; }
			set{ m_IsDigging = value; }
		}

		[Constructable]
		public GraveDiggersShovel() : base( 0xF39 )
		{
			Weight = 0.0;
			Hue = 1109;
			Name = "a grave diggers shovel";
			m_Uses = Utility.RandomMinMax( 15, 40 );
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1060662, "Uses\t{0}", m_Uses );
		}

		public GraveDiggersShovel( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf (from.Backpack))
			{
				from.SendMessage( "You must have this in your backpack in order to dig." );
			}	
			if ( IsDigging != true )
			{
				from.Target = new GraveTarget( this, from );
				from.SendMessage( "What grave would you like it dig in?" );
			}
			else
			{
				from.SendMessage( "You must wait to use this item again." );
			}
		}

		private static void GetRandomAOSStats( out int attributeCount, out int min, out int max )
		{
			int rnd = Utility.Random( 15 );

			if ( rnd < 2 )
			{
				attributeCount = Utility.RandomMinMax( 5, 9 );
				min = 50; max = 100;
			}
			else if ( rnd < 4 )
			{
				attributeCount = Utility.RandomMinMax( 4, 8 );
				min = 40; max = 80;
			}
			else if ( rnd < 6 )
			{
				attributeCount = Utility.RandomMinMax( 3, 6 );
				min = 30; max = 60;
			}
			else if ( rnd < 8 )
			{
				attributeCount = Utility.RandomMinMax( 2, 5 );
				min = 20; max = 50;
			}
			else if ( rnd < 10 )
			{
				attributeCount = Utility.RandomMinMax( 1, 4 );
				min = 10; max = 40;
			}
			else
			{
				attributeCount = 1;
				min = 10; max = 30;
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version 

			writer.Write( m_Uses );
			writer.Write( m_IsDigging );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			m_Uses = reader.ReadInt();
			m_IsDigging = reader.ReadBool();
		}

		private class DigTimer : Timer
		{ 
			private Mobile m_From;
			private GraveDiggersShovel m_Item;

			public DigTimer( Mobile from, GraveDiggersShovel shovel, TimeSpan duration ) : base( duration ) 
			{ 
				Priority = TimerPriority.OneSecond;
				m_From = from;
				m_Item = shovel;
			}

			protected override void OnTick() 
			{
				Item gem = Loot.RandomGem();
				Item reg = Loot.RandomPossibleReagent();

				Item equip;
				equip = Loot.RandomArmorOrShieldOrWeaponOrJewelry();

				if ( m_Item != null )
					m_Item.IsDigging = false;

				if ( equip is BaseWeapon )
				{
					BaseWeapon weapon = (BaseWeapon)equip;

					int attributeCount;
					int min, max;

					GetRandomAOSStats( out attributeCount, out min, out max );

					BaseRunicTool.ApplyAttributesTo( weapon, attributeCount, min, max );
				}
				else if ( equip is BaseArmor )
				{
					BaseArmor armor = (BaseArmor)equip;

					int attributeCount;
					int min, max;

					GetRandomAOSStats( out attributeCount, out min, out max );

					BaseRunicTool.ApplyAttributesTo( armor, attributeCount, min, max );
				}
				else if ( equip is BaseJewel )
				{
					int attributeCount;
					int min, max;

					GetRandomAOSStats( out attributeCount, out min, out max );

					BaseRunicTool.ApplyAttributesTo( (BaseJewel)equip, attributeCount, min, max );
				}

				if ( m_From.Skills[SkillName.Mining].Base < 15.0 )
				{
					if ( Utility.Random( 100 ) < 65 )
					{
						m_From.SendMessage( "You fail to dig anything up." );
					}
					else
					{
						switch ( Utility.Random ( 3 ) )
						{
							case 0:
							m_From.AddToBackpack( gem );
							m_From.SendMessage( "You dig up a gem." );
							break;

							case 1:
							m_From.AddToBackpack( reg );
							m_From.SendMessage( "You dig up a reagent." );
							break;

							case 2:
							m_From.AddToBackpack( new Bone( Utility.RandomMinMax( 2, 10 ) ) );
							m_From.SendMessage( "You dig up some bones." );
							break;
						}
					}
				}
				else if ( m_From.Skills[SkillName.Mining].Base < 35.0 )
				{
					if ( Utility.Random( 100 ) < 55 )
					{
						m_From.SendMessage( "You fail to dig anything up." );
					}
					else
					{
						gem.Amount = Utility.RandomMinMax( 2, 4 );
						reg.Amount = Utility.RandomMinMax( 2, 4 );

						switch ( Utility.Random ( 5 ) )
						{
							case 0:
							m_From.AddToBackpack( gem );
							m_From.SendMessage( "You dig up some gems." );
							break;

							case 1:
							m_From.AddToBackpack( reg );
							m_From.SendMessage( "You dig up some reagents." );
							break;

							case 2:
							m_From.AddToBackpack( new Bone( Utility.RandomMinMax( 2, 10 ) ) );
							m_From.SendMessage( "You dig up some bones." );
							break;

							case 3:
							m_From.AddToBackpack( Loot.RandomScroll( 0, 20, SpellbookType.Regular ) );
							m_From.SendMessage( "You dig up a scroll." );
							break;

							case 4:
							m_From.AddToBackpack( Loot.RandomScroll( 0, 5, SpellbookType.Necromancer ) );
							m_From.SendMessage( "You dig up a scroll." );
							break;
						}
					}
				}
				else if ( m_From.Skills[SkillName.Mining].Base < 50.0 )
				{
					if ( Utility.Random( 100 ) < 45 )
					{
						m_From.SendMessage( "You fail to dig anything up." );
					}
					else
					{
						gem.Amount = Utility.RandomMinMax( 2, 10 );
						reg.Amount = Utility.RandomMinMax( 2, 10 );

						switch ( Utility.Random ( 6 ) )
						{
							case 0:
							m_From.AddToBackpack( gem );
							m_From.SendMessage( "You dig up some gems." );
							break;

							case 1:
							m_From.AddToBackpack( reg );
							m_From.SendMessage( "You dig up some reagents." );
							break;

							case 2:
							m_From.AddToBackpack( equip );
							m_From.SendMessage( "You dig up some equipment." );
							break;

							case 3:
							m_From.AddToBackpack( Loot.RandomScroll( 0, 40, SpellbookType.Regular ) );
							m_From.SendMessage( "You dig up a scroll." );
							break;

							case 4:
							m_From.AddToBackpack( Loot.RandomScroll( 0, 10, SpellbookType.Necromancer ) );
							m_From.SendMessage( "You dig up a scroll." );
							break;

							case 5:
							m_From.AddToBackpack( new Bone( Utility.RandomMinMax( 2, 10 ) ) );
							m_From.SendMessage( "You dig up some bones." );
							break;
						}
					}
				}
				else if ( m_From.Skills[SkillName.Mining].Base < 75.0 )
				{
					if ( Utility.Random( 100 ) < 35 )
					{
						m_From.SendMessage( "You fail to dig anything up." );
					}
					else
					{
						gem.Amount = Utility.RandomMinMax( 2, 20 );
						reg.Amount = Utility.RandomMinMax( 2, 20 );

						switch ( Utility.Random ( 7 ) )
						{
							case 0:
							m_From.AddToBackpack( gem );
							m_From.SendMessage( "You dig up some gems." );
							break;

							case 1:
							m_From.AddToBackpack( reg );
							m_From.SendMessage( "You dig up some reagents." );
							break;

							case 2:
							m_From.AddToBackpack( equip );
							m_From.SendMessage( "You dig up some equipment." );
							break;

							case 3:
							m_From.AddToBackpack( Loot.RandomScroll( 0, 50, SpellbookType.Regular ) );
							m_From.SendMessage( "You dig up a scroll." );
							break;

							case 4:
							m_From.AddToBackpack( Loot.RandomScroll( 0, 13, SpellbookType.Necromancer ) );
							m_From.SendMessage( "You dig up a scroll." );
							break;
	
							case 5:
							m_From.AddToBackpack( new Bone( Utility.RandomMinMax( 2, 10 ) ) );
							m_From.SendMessage( "You dig up some bones." );
							break;

							case 6:
							m_From.AddToBackpack( new Gold( 50, 150 ) );
							m_From.SendMessage( "You dig up some gold." );
							break;
						}
					}
				}
				else if ( m_From.Skills[SkillName.Mining].Base < 90.0 )
				{
					if ( Utility.Random( 100 ) < 25 )
					{
						m_From.SendMessage( "You fail to dig anything up." );
					}
					else
					{
						gem.Amount = Utility.RandomMinMax( 2, 30 );
						reg.Amount = Utility.RandomMinMax( 2, 30 );

						switch ( Utility.Random ( 9 ) )
						{
							case 0:
							m_From.AddToBackpack( gem );
							m_From.SendMessage( "You dig up some gems." );
							break;

							case 1:
							m_From.AddToBackpack( reg );
							m_From.SendMessage( "You dig up some reagents." );
							break;

							case 2:
							m_From.AddToBackpack( equip );
							m_From.SendMessage( "You dig up some equipment." );
							break;

							case 3:
							m_From.AddToBackpack( Loot.RandomScroll( 0, 63, SpellbookType.Regular ) );
							m_From.SendMessage( "You dig up a scroll." );
							break;

							case 4:
							m_From.AddToBackpack( Loot.RandomScroll( 0, 15, SpellbookType.Necromancer ) );
							m_From.SendMessage( "You dig up a scroll." );
							break;

							case 5:
							m_From.AddToBackpack( new Bone( Utility.RandomMinMax( 2, 10 ) ) );
							m_From.SendMessage( "You dig up some bones." );
							break;

							case 6:
							m_From.AddToBackpack( new Gold( 50, 150 ) );
							m_From.SendMessage( "You dig up some gold." );
							break;

							case 7:
							m_From.AddToBackpack( new GraveItem() );
							m_From.SendMessage( "You dig up an ancient artifact." );
							break;

							case 8:
							m_From.AddToBackpack( new SoulGem() );
							m_From.SendMessage( "You dig up a strange, glowing stone." );
							break;
						}
					}
				}
				else if ( m_From.Skills[SkillName.Mining].Base < 150.0)
				{
					if ( Utility.Random( 500 ) < 5 )
					{
						m_From.SendMessage( "You dig up and item of great value." );
						switch ( Utility.Random ( 5 ) )
						{
							case 0:
							m_From.AddToBackpack( new ArmoredRobe() );
							break;

							case 1:
							m_From.AddToBackpack( new ButchersResolve() );
							break;

							case 2:
							m_From.AddToBackpack( new FollowerOfTheOldLord() );
							break;

							case 3:
							m_From.AddToBackpack( new SkirtOfTheAmazon() );
							break;

							case 4:
							m_From.AddToBackpack( new HolyHammerOfExorcism() );
							break;
						}
					}
					else
					{
						gem.Amount = Utility.RandomMinMax( 2, 40 );
						reg.Amount = Utility.RandomMinMax( 2, 40 );

						switch ( Utility.Random ( 9 ) )
						{
							case 0:
							m_From.AddToBackpack( gem );
							m_From.SendMessage( "You dig up some gems." );
							break;

							case 1:
							m_From.AddToBackpack( reg );
							m_From.SendMessage( "You dig up some reagents." );
							break;

							case 2:
							m_From.AddToBackpack( equip );
							m_From.SendMessage( "You dig up some equipment." );
							break;

							case 3:
							m_From.AddToBackpack( Loot.RandomScroll( 0, 63, SpellbookType.Regular ) );
							m_From.SendMessage( "You dig up a scroll." );
							break;

							case 4:
							m_From.AddToBackpack( Loot.RandomScroll( 0, 15, SpellbookType.Necromancer ) );
							m_From.SendMessage( "You dig up a scroll." );
							break;

							case 5:
							m_From.AddToBackpack( new Bone( Utility.RandomMinMax( 2, 10 ) ) );
							m_From.SendMessage( "You dig up some bones." );
							break;

							case 6:
							m_From.AddToBackpack( new Gold( 50, 150 ) );
							m_From.SendMessage( "You dig up some gold." );
							break;

							case 7:
							m_From.AddToBackpack( new GraveItem() );
							m_From.SendMessage( "You dig up an ancient artifact." );
							break;

							case 8:
							m_From.AddToBackpack( new SoulGem() );
							m_From.SendMessage( "You dig up a strange, glowing stone." );
							break;
						}
					}
				}
				else
				{
					m_From.SendMessage( "You fail to dig anything up." );
				}

				Stop();
			}
		}

		private class GraveTarget : Target
		{

			//Grave ItemIDs
			public static int[] m_Grave = new int[]
			{
				3795,
				3807,
				3808,
				3809,
				3810,
				3816
			
			};

			private GraveDiggersShovel m_Item;
			private Mobile m_From;

			public GraveTarget( GraveDiggersShovel item, Mobile from ) : base( 12, false, TargetFlags.None )
			{
				m_Item = item;
				m_From = from;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is Item )
				{
					Item i = (Item)targeted;

					bool isGrave = false;

					foreach ( int check in m_Grave )
					{
  						if ( check == i.ItemID )
    							isGrave = true;
					}
				
					if ( isGrave == true )
					{
						m_Item.Uses -= 1;
						if ( m_Item.Uses == 0 )
						{
							m_Item.Delete();
							if ( m_From != null )
								m_From.SendMessage( "Your shovel has broken." );
						}

						if ( m_From != null )
							m_From.SendMessage( "You start to dig." );

						DigTimer dt = new DigTimer( m_From, m_Item, TimeSpan.FromSeconds( 10.0 ) );
						dt.Start();
						m_From.PlaySound( Utility.RandomList( 0x125, 0x126 ) );
						m_From.Animate( 11, 1, 1, true, false, 0 );
						m_Item.IsDigging = true;
					}
					else
					{
						if ( m_From != null )
							m_From.SendMessage( "That is not a grave." );
					}

				}
				else if ( targeted is StaticTarget )
				{
					StaticTarget i = (StaticTarget)targeted;

					bool isGrave = false;

					foreach ( int check in m_Grave )
					{
  						if ( check == i.ItemID )
    							isGrave = true;
					}

					if ( isGrave == true )
					{
						m_Item.Uses -= 1;
						if ( m_Item.Uses <= 0 )
						{
							m_Item.Delete();
							if ( m_From != null )
								m_From.SendMessage( "Your shovel has broken." );
						}

						if ( m_From != null )
							m_From.SendMessage( "You start to dig." );

						DigTimer dt = new DigTimer( m_From, m_Item, TimeSpan.FromSeconds( 10.0 ) );
						dt.Start();
						m_From.PlaySound( Utility.RandomList( 0x125, 0x126 ) );
						m_From.Animate( 11, 1, 1, true, false, 0 );
						m_Item.IsDigging = true;
					}
				}
				else
				{
					m_From.SendMessage( "That is not a grave." );
				}
			}
		}
	}
}
