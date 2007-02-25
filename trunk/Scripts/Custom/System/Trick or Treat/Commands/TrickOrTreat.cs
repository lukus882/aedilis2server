using System;
using Server;
using Server.Items;
using Server.Guilds;
using Server.Mobiles;
using Server.Gumps;
using Server.Network;

namespace Server.Misc
{
	public class TrickOrTreat
	{
		public static void Initialize()
		{
			// Register our speech handler
			EventSink.Speech += new SpeechEventHandler( EventSink_Speech );
		}

		public static void EventSink_Speech( SpeechEventArgs args )
		{
			Mobile from = args.Mobile;
			int[] keywords = args.Keywords;


			if ( from is PlayerMobile )
			{
				PlayerMobile player = from as PlayerMobile;
				
				if ( args.Speech.ToLower().Equals("trick or treat"))
				{
					Item[] items = from.Backpack.FindItemsByType( typeof( TrickOrTreatBag ) );

					if ( items.Length == 0 )
					{
						from.SendMessage ("You need a goodie bag to go trick or treating!");
					}
					else
					{
						bool foundbag = false;
						
						foreach( TrickOrTreatBag tb in items )
						{
							if ( tb.Uses > 0 )
							{ 
								foreach ( Mobile m in from.GetMobilesInRange( 3 ) ) // TODO: Validate range
								{
									if ( !m.Player && m.Body.IsHuman && ( m is BaseVendor ) )
									{
										if (m is BaseCreature && (((BaseCreature)m).IsHumanInTown() ) )
										{
											from.Direction = from.GetDirectionTo( m );
											m.Direction = m.GetDirectionTo( from );
											
											TrickOrTreat.GiveTreat( from, m, tb );
											tb.ConsumeUse( from );
											
											return;
										}
									}
								}

								foundbag = true;

								break;
							}
						}
						
						
						if ( !foundbag )
						{
							from.SendMessage("You don't have any uses left on your goodie bags");
						}
					}
				}
			}
		}

		private static void PlaceItemIn( Container parent, int x, int y, Item item )
		{
			parent.AddItem( item );
			item.Location = new Point3D( x, y, 0 );
		}
		
		public static void GiveTreat ( Mobile from, Mobile vendor, Container gb)
		{			
			if ( Utility.Random( 100 ) < 5 )
			{
				//Give special Items.
				vendor.Say ("Well, I am out of goodies, but let me give you something special");
			
				switch ( Utility.Random ( 3) )
				{
					case 0:
						//Give mask
						PlaceItemIn( gb, 93, 96, new HalloweenSkeletonMask() );
						
						break;
					case 1:
						//Give knife
						PlaceItemIn( gb, 93, 75, new HalloweenCostumeKnife() );
						
						break;
					case 2:
						//Give costume
						PlaceItemIn( gb, 93, 34, new GhostCostume() );
						
						break;
				}
				
			}
			else
			{
				switch ( Utility.Random ( 9 ) )
				{
					case 0:
						//Give apple
						vendor.Say ("Here's an apple for you.  Happy Halloween");
						PlaceItemIn( gb, 44, 34, new Apple() );
						
						break;
					case 1:
						//Give pear
						vendor.Say ("Here's a pear for you.  Happy Halloween");
						PlaceItemIn( gb, 70, 34, new Pear() );
						
						break;
					case 2:
						//Give cake
						vendor.Say ("Here's a cake for you.  Happy Halloween");
						PlaceItemIn( gb, 29, 92, new Cake() );
						
						break;
					case 3:
						//Give cookies
						vendor.Say ("Here's some cookies for you.  Happy Halloween");
						PlaceItemIn( gb, 29, 53, new Cookies() );
						
						break;
					case 4:
						//Give pumpkin pie
						vendor.Say ("Here's a nice pumpkin pie for you.  Happy Halloween");
						PlaceItemIn( gb, 29, 34, new PumpkinPie() );
						
						break;
					case 5:
						//Give Candy Heart
						vendor.Say ("Here's some scary candy for you.  Happy Halloween");
						PlaceItemIn( gb, 55, 56, new CandyHeart() );
						
						break;
					case 6:
						//Give Gummy Worm
						vendor.Say ("Here's a some candy.  Happy Halloween");
						PlaceItemIn( gb, 59, 84, new GummyWorm() );
						
						break;
					case 7:
						//Give Chocolate Skeleton
						vendor.Say ("Here's some scary candy.  Happy Halloween");
						PlaceItemIn( gb, 77, 56, new ChocolateSkeleton() );
						
						break;
					case 8:
						//Give Pumpkin Candycorn
						vendor.Say ("Here's some candy for you.  Happy Halloween");
						PlaceItemIn( gb, 73, 96, new PumpkinCandyCorn() );
						
						break;
				}
			}
		}
	}
}