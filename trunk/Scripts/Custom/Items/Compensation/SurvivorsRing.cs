using System;
using System.Collections;
using Server;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Gumps;
using Server.Misc;
using Server.Regions;
using Server.Spells;

namespace Server.Items
{
	public class SurvivorsRing : BaseRing
	{

		public override int ArtifactRarity{ get{ return 1337; } }

		[Constructable]
		public SurvivorsRing() : base( 0x108a )
		{
			Weight = 2;
			Name = "An Old World Survivor's Ring";
			Hue = 2;
		}

		public SurvivorsRing( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{  
			

			if ( Parent != from ) 
			{ 
			from.SendMessage( 22, "You must equip this item to use it." ); 
			} 
			else if (!from.Alive)  
			{ 
			from.SendMessage("You are dead try using the safe res command" ); 
			} 
			else if (from.Region is Jail)  
			{ 
			from.SendMessage("You cannot seem to escape the forces of Jail... you.. CRIMINAL!"); 
			} 
			else if (from.Region is DungeonRegion)  
			{ 
			from.SendMessage("Your ring does not work while you are in a dungeon"); 
			} 
			else if ( Server.Misc.WeightOverloading.IsOverloaded( from ) )
			{
				from.SendLocalizedMessage( 502359, "", 0x22 ); // Thou art too encumbered to move.
			}
			else if ( SpellHelper.CheckCombat( from ) )
			{
				from.SendLocalizedMessage( 1005564, "", 0x22 ); // Wouldst thou flee during the heat of battle??
			}
			else
			{
				switch ( Utility.Random( 31 ))
				{ 
				case 0:
				from.Location = ( new Point3D( 1456, 854, 0 ));
				from.Map = Map.Trammel;
				break; 

				case 1: 
				from.Location = ( new Point3D( 1856, 872, -1)); 
				from.Map = Map.Trammel;
				break; 

				case 2: 
				from.Location = ( new Point3D( 4217, 564, 36));
				from.Map = Map.Trammel;
				break; 

				case 3: 
				from.Location = ( new Point3D( 1730, 3528, 3)); 
				from.Map = Map.Trammel;
				break; 

				case 4: 
				from.Location = ( new Point3D( 4276, 3699, 0)); 
				from.Map = Map.Trammel;
				break; 

				case 5: 
				from.Location = ( new Point3D( 1301, 639, 16));  
				from.Map = Map.Trammel;
				break; 

				case 6: 
				from.Location = ( new Point3D( 3355, 299, 9)); 
				from.Map = Map.Trammel;
				break; 

				case 7: 
				from.Location = ( new Point3D( 1589, 2485, 5)); 
				from.Map = Map.Trammel;
				break; 

				case 8: 
				from.Location = ( new Point3D( 2496, 3932, 0)); 
				from.Map = Map.Trammel;
				break; 

				case 9: 
				from.Location = ( new Point3D( 2043, 238, 10)); 
				from.Map = Map.Trammel;
				break; 

				case 10: 
				from.Location = ( new Point3D( 514, 1561, 0)); 
				from.Map = Map.Trammel;
				break; 

				case 11: 
				from.Location = ( new Point3D( 4721, 3822, 0)); 
				from.Map = Map.Trammel;
				break; 

				case 12: 
				from.Location = ( new Point3D( 1176, 2637, 0)); 
				from.Map = Map.Trammel;
				break; 

				case 13: 
				from.Location = ( new Point3D( 1298, 1080, 0)); 
				from.Map = Map.Trammel;
				break; 

				case 14: 
				from.Location = ( new Point3D( 4111, 432, 5)); 
				from.Map = Map.Trammel;
				break; 

				case 15: 
				from.Location = ( new Point3D( 2499, 919, 0)); 
				from.Map = Map.Trammel;
				break; 

				case 16: 
				from.Location = ( new Point3D( 1323, 1624, 55)); 
				from.Map = Map.Trammel;
				break; 

				case 17: 
				from.Location = ( new Point3D( 2285, 1209, 0)); 
				from.Map = Map.Trammel;
				break; 

				case 18: 
				from.Location = ( new Point3D( 1398, 3742, -21)); 
				from.Map = Map.Trammel;
				break; 

				case 19: 
				from.Location = ( new Point3D( 3792, 2248, 20)); 
				from.Map = Map.Trammel;
				break; 

				case 20: 
				from.Location = ( new Point3D( 2539, 501, 30)); 
				from.Map = Map.Trammel;
				break; 

				case 21: 
				from.Location = ( new Point3D( 4442, 1122, 5)); 
				from.Map = Map.Trammel;
				break; 

				case 22: 
				from.Location = ( new Point3D( 3728, 1360, 5)); 
				from.Map = Map.Trammel;
				break; 

				case 23: 
				from.Location = ( new Point3D( 535, 992, 0)); 
				from.Map = Map.Trammel;
				break; 

				case 24: 
				from.Location = ( new Point3D( 1362, 896, 0)); 
				from.Map = Map.Trammel;
				break; 

				case 25: 
				from.Location = ( new Point3D( 2882, 788, 0)); 
				from.Map = Map.Trammel;
				break; 

				case 26: 
				from.Location = ( new Point3D( 1927, 2779, 0)); 
				from.Map = Map.Trammel;
				break; 

				case 27: 
				from.Location = ( new Point3D( 639, 2236, -3)); 
				from.Map = Map.Trammel;
				break; 

				case 28: 
				from.Location = ( new Point3D( 3011, 3526, 15)); 
				from.Map = Map.Trammel;
				break; 

				case 29: 
				from.Location = ( new Point3D( 3650, 2653, 0)); 
				from.Map = Map.Trammel;
				break; 

				case 30: 
				from.Location = ( new Point3D( 5769, 3176, 0)); 
				from.Map = Map.Trammel;
				break; 
				}

			}
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