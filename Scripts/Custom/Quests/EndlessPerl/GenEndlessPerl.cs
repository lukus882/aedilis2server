using System;
using System.Collections;
using System.Collections.Generic;
using Server.Commands;
using Server.Commands.Generic;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Engines.Quests;
using Server.Engines.Quests.EndlessPerlQuest;

namespace Server
{
	public class GenerateEPQ
	{
		public GenerateEPQ()
		{
		}

		public static void Initialize()
		{
			CommandSystem.Register( "GenEPQ", AccessLevel.Administrator, new CommandEventHandler( GenerateEPQ_OnCommand ) );
		}

		[Usage( "GenEPQ" )]
		[Description( "Generates Endless Perl Quest" )]
		public static void GenerateEPQ_OnCommand( CommandEventArgs e )
		{
			e.Mobile.SendMessage( "Please hold while the quest is being generated." );

			GenMobiles.CreateMobiles();
			GenSpawners.CreateSpawners();

			e.Mobile.SendMessage( "Endless Perl Quest Generated." );
		}

		public class GenMobiles
		{
			
			public GenMobiles()
			{
			}

			private static Queue m_Queue = new Queue();

			public static bool FindMobile( Map map, Point3D p )
			{
				IPooledEnumerable eable = map.GetMobilesInRange( p, 0 );

				foreach ( Mobile mob in eable )
				{
					if ( mob is BaseQuester )
					{
						int delta = mob.Z - p.Z;

						if ( delta >= -12 && delta <= 12 )
							m_Queue.Enqueue( mob );
					}
				}

				eable.Free();

				while ( m_Queue.Count > 0 )
					((Mobile)m_Queue.Dequeue()).Delete();

				return false;
			}

			public static void CreateMobiles( Point3D pointLocation, Map mapLocation )
			{
				Point3D norivar = new Point3D( 1466, 1562, 30 );

				if ( !FindMobile( mapLocation, pointLocation ) )
				{
					Norivar nr = new Norivar();

					if ( pointLocation ==  norivar )
					{
						nr.Direction = Direction.East;
						nr.Location = pointLocation;
						nr.Map = mapLocation;
						World.AddMobile( nr );
					}
				}
			}

			public static void CreateMobiles( int xLoc, int yLoc, int zLoc, Map map )
			{
				CreateMobiles( new Point3D( xLoc, yLoc, zLoc ), map);
			}

			public static void CreateMobilesFacet( Map map )
			{
				CreateMobiles( 1466, 1562, 30, map );
			}

			public static void CreateMobiles()
			{
				CreateMobilesFacet( Map.Felucca );
			}
		}

		public class GenSpawners
		{

			//configuration
			private const bool TotalRespawn = true;//Should we spawn them up right away?
			private static TimeSpan MinTime = TimeSpan.FromMinutes( 3 );//min spawn time
			private static TimeSpan MaxTime = TimeSpan.FromMinutes( 5 );//max spawn time

			public GenSpawners()
			{
			}

			private static Queue m_ToDelete = new Queue();

			public static void ClearSpawners( int x, int y, int z, Map map )
			{
				IPooledEnumerable eable = map.GetItemsInRange( new Point3D( x, y, z ), 0 );

				foreach ( Item item in eable )
				{
					if ( item is Spawner && item.Z == z )
						m_ToDelete.Enqueue( item );
				}

				eable.Free();

				while ( m_ToDelete.Count > 0 )
					((Item)m_ToDelete.Dequeue()).Delete();
			}

			public static void CreateSpawners()
			{
				//locations
				PlaceSpawns( 366, 1481, 2, "DartmoorPony" );
			}

			public static void PlaceSpawns( int x, int y, int z, string types )
			{

				switch ( types )
				{
					case "DartmoorPony":
						MakeSpawner( "DartmoorPony", x, y, z, Map.Felucca, true );
						MinTime = TimeSpan.FromMinutes( 3 );
						MaxTime = TimeSpan.FromMinutes( 5 );
						break;
					default:
						break;
				}
			}

			private static void MakeSpawner( string types, int x, int y, int z, Map map, bool start )
			{
				ClearSpawners( x, y, z, map );

				Spawner sp = new Spawner( types );

				sp.Count = 1;

				sp.Running = true;
				sp.HomeRange = 15;
				sp.MinDelay = MinTime;
				sp.MaxDelay = MaxTime;

				sp.MoveToWorld( new Point3D( x, y, z ), map );

			}
		}
	}
}
