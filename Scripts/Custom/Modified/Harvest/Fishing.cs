using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Engines.Quests;
using Server.Engines.Quests.Collector;
using System.Collections.Generic;

namespace Server.Engines.Harvest
{
	public class Fishing : HarvestSystem
	{
		private static Fishing m_System;

		public static Fishing System
		{
			get
			{
				if ( m_System == null )
					m_System = new Fishing();

				return m_System;
			}
		}

		private HarvestDefinition m_Definition;

		public HarvestDefinition Definition
		{
			get{ return m_Definition; }
		}

		private Fishing()
		{
			HarvestResource[] res;
			HarvestVein[] veins;

			#region Fishing
			HarvestDefinition fish = new HarvestDefinition();

			// Resource banks are every 8x8 tiles
			fish.BankWidth = 8;
			fish.BankHeight = 8;

			// Every bank holds from 5 to 15 fish
			fish.MinTotal = 5;
			fish.MaxTotal = 15;

			// A resource bank will respawn its content every 10 to 20 minutes
			fish.MinRespawn = TimeSpan.FromMinutes( 10.0 );
			fish.MaxRespawn = TimeSpan.FromMinutes( 20.0 );

			// Skill checking is done on the Fishing skill
			fish.Skill = SkillName.Fishing;

			// Set the list of harvestable tiles
			fish.Tiles = m_WaterTiles;
			fish.RangedTiles = true;

			// Players must be within 4 tiles to harvest
			fish.MaxRange = 4;

			// One fish per harvest action
			fish.ConsumedPerHarvest = 1;
			fish.ConsumedPerFeluccaHarvest = 1;

			// The fishing
			fish.EffectActions = new int[]{ 12 };
			fish.EffectSounds = new int[0];
			fish.EffectCounts = new int[]{ 1 };
			fish.EffectDelay = TimeSpan.Zero;
			fish.EffectSoundDelay = TimeSpan.FromSeconds( 8.0 );

			fish.NoResourcesMessage = 503172; // The fish don't seem to be biting here.
			fish.FailMessage = 503171; // You fish a while, but fail to catch anything.
			fish.TimedOutOfRangeMessage = 500976; // You need to be closer to the water to fish!
			fish.OutOfRangeMessage = 500976; // You need to be closer to the water to fish!
			fish.PackFullMessage = 503176; // You do not have room in your backpack for a fish.
			fish.ToolBrokeMessage = 503174; // You broke your fishing pole.

			res = new HarvestResource[]
				{
					new HarvestResource( 00.0, 00.0, 100.0, 1043297, typeof( LittleFish ) ),
					new HarvestResource( 10.0, -15.0, 100.0, "Bluegill", typeof( Bluegill ) ),
					new HarvestResource( 10.0, -15.0, 100.0, "Perch", typeof( Perch ) ),
					new HarvestResource( 20.0, -5.0, 100.0, "Carp", typeof( Carp ) ),
					new HarvestResource( 20.0, -5.0, 100.0, "Catfish", typeof( Catfish ) )
				};

			veins = new HarvestVein[]
				{
					new HarvestVein( 20.0, 0.0, res[0], null ),	// little fish
					new HarvestVein( 20.0, 0.5, res[1], res[0] ),	// bluegill
					new HarvestVein( 20.0, 0.5, res[2], res[0] ),	// perch
					new HarvestVein( 20.0, 0.5, res[3], res[0] ),	// carp
					new HarvestVein( 20.0, 0.5, res[4], res[0] )	// catfish
				};

			fish.Resources = res;
			fish.Veins = veins;

			m_Definition = fish;
			Definitions.Add( fish );
			#endregion
		}

		public override void OnConcurrentHarvest( Mobile from, Item tool, HarvestDefinition def, object toHarvest )
		{
			from.SendLocalizedMessage( 500972 ); // You are already fishing.
		}

		private class MutateEntry
		{
			public double m_ReqSkill, m_MinSkill, m_MaxSkill;
			public bool m_DeepWater;
			public Type[] m_Types;

			public MutateEntry( double reqSkill, double minSkill, double maxSkill, bool deepWater, params Type[] types )
			{
				m_ReqSkill = reqSkill;
				m_MinSkill = minSkill;
				m_MaxSkill = maxSkill;
				m_DeepWater = deepWater;
				m_Types = types;
			}
		}

		private static MutateEntry[] m_MutateTable = new MutateEntry[]
			{
				new MutateEntry(  40.0, 125.0, -420.0,   true, typeof( Cod ), typeof( Tuna ), typeof( Swordfish ), typeof( Sturgeon ) ),
				new MutateEntry(  40.0, 125.0, -420.0,   true, typeof( Seaweed ), typeof( FishBones ), typeof( NetPiece ), typeof( Kelp ) ),
				new MutateEntry(  80.0,  80.0,  4080.0,  true, typeof( SpecialFishingNet ) ),
				new MutateEntry(  80.0,  80.0,  4080.0,  true, typeof( BigFish ) ),
				new MutateEntry(  90.0,  80.0,  4080.0,  true, typeof( TreasureMap ) ),
				new MutateEntry( 100.0,  80.0,  4080.0,  true, typeof( MessageInABottle ) ),
				// woodcrafting driftwood
				new MutateEntry(   0.0, 120.0,  -120.0, false, typeof( DriftWood ) ),
                                new MutateEntry(   0.0, 105.0,  -420.0, false, typeof( Boots ), typeof( ThighBoots ) ),
				new MutateEntry(   0.0, 200.0,  -200.0, false, new Type[1]{ null } )
			};

		public override bool SpecialHarvest( Mobile from, Item tool, HarvestDefinition def, Map map, Point3D loc )
		{
			PlayerMobile player = from as PlayerMobile;

			if ( player != null )
			{
				QuestSystem qs = player.Quest;

				if ( qs is CollectorQuest )
				{
					QuestObjective obj = qs.FindObjective( typeof( FishPearlsObjective ) );

					if ( obj != null && !obj.Completed )
					{
						if ( Utility.RandomDouble() < 0.5 )
						{
							player.SendLocalizedMessage( 1055086, "", 0x59 ); // You pull a shellfish out of the water, and find a rainbow pearl inside of it.

							obj.CurProgress++;
						}
						else
						{
							player.SendLocalizedMessage( 1055087, "", 0x2C ); // You pull a shellfish out of the water, but it doesn't have a rainbow pearl.
						}

						return true;
					}
				}
			}

			return false;
		}

		public override Type MutateType( Type type, Mobile from, Item tool, HarvestDefinition def, Map map, Point3D loc, HarvestResource resource )
		{
			bool deepWater = SpecialFishingNet.FullValidation( map, loc.X, loc.Y );

			double skillBase = from.Skills[SkillName.Fishing].Base;
			double skillValue = from.Skills[SkillName.Fishing].Value;

			for ( int i = 0; i < m_MutateTable.Length; ++i )
			{
				MutateEntry entry = m_MutateTable[i];

				if ( !deepWater && entry.m_DeepWater )
					continue;

				if ( skillBase >= entry.m_ReqSkill )
				{
					double chance = (skillValue - entry.m_MinSkill) / (entry.m_MaxSkill - entry.m_MinSkill);

					if ( chance > Utility.RandomDouble() )
						return entry.m_Types[Utility.Random( entry.m_Types.Length )];
				}
			}

			return type;
		}

		private static Map SafeMap( Map map )
		{
			if ( map == null || map == Map.Internal )
				return Map.Trammel;

			return map;
		}

		public override bool CheckResources( Mobile from, Item tool, HarvestDefinition def, Map map, Point3D loc, bool timed )
		{
			Container pack = from.Backpack;

			if ( pack != null )
			{
				List<SOS> messages = pack.FindItemsByType<SOS>();

				for ( int i = 0; i < messages.Count; ++i )
				{
					SOS sos = messages[i];

					if ( from.Map == sos.TargetMap && from.InRange( sos.TargetLocation, 60 ) )
						return true;
				}
			}

			return base.CheckResources( from, tool, def, map, loc, timed );
		}

		public override Item Construct( Type type, Mobile from )
		{
			if ( type == typeof( TreasureMap ) )
			{
				int level;
				if ( from is PlayerMobile && ((PlayerMobile)from).Young && from.Map == Map.Felucca && TreasureMap.IsInHavenIsland( from ) )
					level = 0;
				else
					level = 1;

				return new TreasureMap( level, from.Map == Map.Felucca ? Map.Felucca : Map.Felucca );
			}
			else if ( type == typeof( MessageInABottle ) )
			{
				return new MessageInABottle( from.Map == Map.Felucca ? Map.Felucca : Map.Felucca );
			}

			Container pack = from.Backpack;

			if ( pack != null )
			{
				List<SOS> messages = pack.FindItemsByType<SOS>();

				for ( int i = 0; i < messages.Count; ++i )
				{
					SOS sos = messages[i];

					if ( from.Map == sos.TargetMap && from.InRange( sos.TargetLocation, 60 ) )
					{
						Item preLoot = null;

						switch ( Utility.Random( 7 ) )
						{
							case 0: // Bone parts
							{
								int[] list = new int[]
									{
										0x1AE0, 0x1AE1, 0x1AE2, 0x1AE3, 0x1AE4, // skulls
										0x1B09, 0x1B0A, 0x1B0B, 0x1B0C, 0x1B0D, 0x1B0E, 0x1B0F, 0x1B10, // bone piles
										0x1B15, 0x1B16, // pelvis bones
										0xECA, 0xECB, 0xECC, 0xECD, 0xECE, 0xECF, 0xED0, 0xED1, 0xED2 // skeletons
									};

								preLoot = new ShipwreckedItem( Utility.RandomList( list ) );
								break;
							}
							case 1: // Shells
							{
								preLoot = new ShipwreckedItem( Utility.Random( 0xFC4, 9 ) );
								break;
							}
							case 2: // Misc
							{
								int[] list = new int[]
									{
										0xE9F, 0xEA0, 0xEA1, 0xEA2, 0xEA3, 0xEA4, 0xEA5, 0xEA6, 0xEA7, 0xEA8, // paintings
										0x13A4, 0x13A5, 0x13A6, 0x13A7, 0x13A8, 0x13A9, 0x13AA, 0x13AB, 0x13AC, 0x13AD, 0x13AE, // pillows
										0x1EB5, // unfinished barrel
										0xA2A, // stool
										0xC1F, // broken clock
										0x1047, 0x1048, // globe
										0x1EB1, 0x1EB2, 0x1EB3, 0x1EB4, // barrel staves
										0xE25, 0xE26, 0xE27, 0xE28, 0xE29, 0xE2A, 0xE2B, 0xE2C, // bottles
										0xC19, 0xC1A, 0xC1B, 0xC1C, 0xC1D, 0xC1E, // broken chairs
										0xC2C, 0xC2D, 0xC2E, 0xC2F, 0xC30, // ruined painting, debris
										0x13E5, 0x13E6, 0x13E7, 0x13E8, 0x13E9, 0x13EA // hanging armor
									};

								preLoot = new ShipwreckedItem( Utility.RandomList( list ) );
								break;
							}
							case 3: // Ship Items
							{
								int[] list = new int[]
									{
										0x14F8, 0x14FA, // ropes
										0x14F7, 0x14F9, // anchors
										0x14EB, 0x14EC, // maps
										0x1057, 0x1058, // sextants
										0x171A // feathered hat
									};

								preLoot = new ShipwreckedItem( Utility.RandomList( list ) );
								break;
							}
							case 4: // Shells
							{
								preLoot = new ShipwreckedItem( Utility.Random( 0xFC4, 9 ) );
								break;
							}
							case 5:	//Hats
							{
								if ( Utility.RandomBool() )
									preLoot = new SkullCap();
								else
									preLoot = new TricorneHat();

								break;
							}
							case 6: // Misc
							{
								int[] list = new int[]
									{
										0x1EB5, // unfinished barrel
										0xA2A, // stool
										0xC1F, // broken clock
										0x1047, 0x1048, // globe
										0x1EB1, 0x1EB2, 0x1EB3, 0x1EB4 // barrel staves
									};

								if ( Utility.Random( list.Length + 1 ) == 0 )
									preLoot = new Candelabra();
								else
									preLoot = new ShipwreckedItem( Utility.RandomList( list ) );

								break;
							}
						}

						if ( preLoot != null )
						{
							if ( preLoot is IShipwreckedItem )
								( (IShipwreckedItem)preLoot ).IsShipwreckedItem = true;

							return preLoot;
						}

						LockableContainer chest;
						
						if ( Utility.RandomBool() )
							chest = new MetalGoldenChest();
						else
							chest = new WoodenChest();

						if ( sos.IsAncient )
							chest.Hue = 0x481;

						TreasureMapChest.Fill( chest, Math.Max( 1, Math.Max( 4, sos.Level ) ) );

						if ( sos.IsAncient )
							chest.DropItem( new FabledFishingNet() );
						else
							chest.DropItem( new SpecialFishingNet() );

						chest.Movable = true;
						chest.Locked = false;
						chest.TrapType = TrapType.None;
						chest.TrapPower = 0;
						chest.TrapLevel = 0;
// label chest with fisherman's name
						chest.Name = "Treasure chest fished up by " + from.Name;

						sos.Delete();

						return chest;
					}
				}
			}

			return base.Construct( type, from );
		}

		public override bool Give( Mobile m, Item item, bool placeAtFeet )
		{
			if ( item is TreasureMap || item is MessageInABottle || item is SpecialFishingNet )
			{
				BaseCreature serp;

				if ( 0.25 > Utility.RandomDouble() )
					serp = new DeepSeaSerpent();
				else
					serp = new SeaSerpent();

				int x = m.X, y = m.Y;

				Map map = m.Map;

				for ( int i = 0; map != null && i < 20; ++i )
				{
					int tx = m.X - 10 + Utility.Random( 21 );
					int ty = m.Y - 10 + Utility.Random( 21 );

					Tile t = map.Tiles.GetLandTile( tx, ty );

					if ( t.Z == -5 && ( (t.ID >= 0xA8 && t.ID <= 0xAB) || (t.ID >= 0x136 && t.ID <= 0x137) ) && !Spells.SpellHelper.CheckMulti( new Point3D( tx, ty, -5 ), map ) )
					{
						x = tx;
						y = ty;
						break;
					}
				}

				serp.MoveToWorld( new Point3D( x, y, -5 ), map );

				serp.Home = serp.Location;
				serp.RangeHome = 10;

				serp.PackItem( item );

				m.SendLocalizedMessage( 503170 ); // Uh oh! That doesn't look like a fish!

				return true; // we don't want to give the item to the player, it's on the serpent
			}

			if ( item is BigFish || item is WoodenChest || item is MetalGoldenChest )
				placeAtFeet = true;

			return base.Give( m, item, placeAtFeet );
		}

		public override void SendSuccessTo( Mobile from, Item item, HarvestResource resource )
		{
			if ( item is BigFish )
			{
				from.SendLocalizedMessage( 1042635 ); // Your fishing pole bends as you pull a big fish from the depths!

				((BigFish)item).Fisher = from;
			}
			else if ( item is WoodenChest || item is MetalGoldenChest )
			{
				from.SendLocalizedMessage( 503175 ); // You pull up a heavy chest from the depths of the ocean!
			}
			else
			{
				int number;
				string name;

				if ( item is LittleFish )
				{
					number = 1008124;
					name = "a little fish";
				}
				else if ( item is Cod )
				{
					number = 1008124;
					name = "a cod";
				}
				else if ( item is Sturgeon )
				{
					number = 1008124;
					name = "a sturgeon";
				}
				else if ( item is Swordfish )
				{
					number = 1008124;
					name = "a swordfish";
				}
				else if ( item is Tuna )
				{
					number = 1008124;
					name = "a tuna";
				}
				else if ( item is Bluegill )
				{
					number = 1008124;
					name = "a bluegill";
				}
				else if ( item is Perch )
				{
					number = 1008124;
					name = "a perch";
				}
				else if ( item is Catfish )
				{
					number = 1008124;
					name = "a catfish";
				}
				else if ( item is Carp )
				{
					number = 1008124;
					name = "a carp";
				}
				else if ( item is Seaweed )
				{
					number = 1008124;
					name = "some seaweed";
				}
				else if ( item is Kelp )
				{
					number = 1008124;
					name = "some kelp";
				}
				else if ( item is FishBones )
				{
					number = 1008124;
					name = "some fish bones";
				}
				else if ( item is NetPiece )
				{
					number = 1008124;
					name = "a piece of a fishing net";
				}
				else if ( item is BaseShoes )
				{
					number = 1008124;
					name = item.ItemData.Name;
				}
				else if ( item is TreasureMap )
				{
					number = 1008125;
					name = "a sodden piece of parchment";
				}
				else if ( item is MessageInABottle )
				{
					number = 1008125;
					name = "a bottle, with a message in it";
				}
				else if ( item is SpecialFishingNet )
				{
					number = 1008125;
					name = "a special fishing net"; // TODO: this is just a guess--what should it really be named?
				}
				else
				{
					number = 1043297;

					if ( (item.ItemData.Flags & TileFlag.ArticleA) != 0 )
						name = "a " + item.ItemData.Name;
					else if ( (item.ItemData.Flags & TileFlag.ArticleAn) != 0 )
						name = "an " + item.ItemData.Name;
					else
						name = item.ItemData.Name;
				}

				if ( number == 1043297 )
					from.SendLocalizedMessage( number, name );
// driftwood message
				else if (number == 9999999)
					from.SendMessage("You fish up a piece of driftwood!");
				else
					from.SendLocalizedMessage( number, true, name );
			}
		}

		public override void OnHarvestStarted( Mobile from, Item tool, HarvestDefinition def, object toHarvest )
		{
			base.OnHarvestStarted( from, tool, def, toHarvest );

			int tileID;
			Map map;
			Point3D loc;

			if ( GetHarvestDetails( from, tool, toHarvest, out tileID, out map, out loc ) )
				Timer.DelayCall( TimeSpan.FromSeconds( 1.5 ), 
					delegate
		{
						if( Core.ML )
							from.RevealingAction();

			Effects.SendLocationEffect( loc, map, 0x352D, 16, 4 );
			Effects.PlaySound( loc, map, 0x364 );
					} );
		}

		public override void OnHarvestFinished( Mobile from, Item tool, HarvestDefinition def, HarvestVein vein, HarvestBank bank, HarvestResource resource, object harvested )
		{
			base.OnHarvestFinished( from, tool, def, vein, bank, resource, harvested );

			if ( Core.ML )
				from.RevealingAction();
		}

		public override object GetLock( Mobile from, Item tool, HarvestDefinition def, object toHarvest )
		{
			return this;
		}

		public override bool BeginHarvesting( Mobile from, Item tool )
		{
			if ( !base.BeginHarvesting( from, tool ) )
				return false;

			from.SendLocalizedMessage( 500974 ); // What water do you want to fish in?
			return true;
		}

		public override bool CheckHarvest( Mobile from, Item tool )
		{
			if ( !base.CheckHarvest( from, tool ) )
				return false;

			if ( from.Mounted )
			{
				//from.SendLocalizedMessage( 500971 ); // You can't fish while riding!
				return true; // was false
			}

			return true;
		}

		public override bool CheckHarvest( Mobile from, Item tool, HarvestDefinition def, object toHarvest )
		{
			if ( !base.CheckHarvest( from, tool, def, toHarvest ) )
				return false;

			if ( from.Mounted )
			{
				//from.SendLocalizedMessage( 500971 ); // You can't fish while riding!
				return true; // was false
			}

			return true;
		}

		private static int[] m_WaterTiles = new int[]
			{
				0x00A8, 0x00AB,
				0x0136, 0x0137,
				0x5797, 0x579C,
				0x746E, 0x7485,
				0x7490, 0x74AB,
				0x74B5, 0x75D5
			};
	}
}