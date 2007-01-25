using System; 
using Server; 
using System.Collections;
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.ContextMenus;

namespace Server.Mobiles
{
	
	public class Ralph : BaseVendor
	{
        private ArrayList m_SBInfos = new ArrayList();
		protected override ArrayList SBInfos{ get { return m_SBInfos; } }
		public override bool IsActiveVendor{ get{ return false; } }
		public override bool IsInvulnerable{ get{ return true; } }
		public override bool DisallowAllMoves{ get{ return false; } }
		public override bool ClickTitle{ get { return false; } }
		public override bool CanTeach{ get{ return false; } }
		public override void InitSBInfo()
		{
		}

		[Constructable]
		public Ralph(): base( "the Banner Crafter" )
		{
		}
		
		public override void InitBody()
		{
			InitStats( 100, 100, 100 );
			Name = "Ralph";           
			Body = 0x190;
			Hue = 0x83F8;
		}
		public override void InitOutfit()
		{
			
			AddItem( new LongPants(32)); 
			AddItem( new Boots(32) );
			AddItem( new Shirt());
			AddItem( new Bonnet(32));
                      
			//Utility.AssignRandomHair( this );
			HairItemID = 0x203B;  
            HairHue = 1175;
			
					
			}
	
			public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			
			if ( m.Alive && m is PlayerMobile )
			{
				PlayerMobile pm = (PlayerMobile)m;

				
				if ( InRange( pm, 2 ) && !InRange( oldLocation, 2 ) )
				{
					
					WormSilk ws = pm.Backpack.FindItemByType( typeof ( WormSilk ) ) as WormSilk;
		        

					if ( ws == null )
					{
						if ( ! pm.HasGump( typeof( RalphGump ) ) )
					{
					        pm.SendGump( new RalphGump( pm ) );
						
						return;
					}
					}
					else if ( ws != null )
					{
						Say( "I see you have found some worm silk! If you have 10, may I have them please?");
						
						return;
					}
				}
			}
		}
		public Ralph( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		} 

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{          		
         	Mobile m = from;
			PlayerMobile mobile = m as PlayerMobile;
           
			if ( mobile != null)
			{
				if( dropped is WormSilk )
            
         		{
         			if(dropped.Amount!=10)
         			{
					int amount = dropped.Amount;
				    this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "I have no need for that.", mobile.NetState );
				    this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "I need 10 Worm Silk not " +dropped.Amount+".", mobile.NetState );
				
         				return false;
         			}
         			
         			dropped.Delete(); 
         			if( Utility.Random( 100 ) < 100 ) 
         			switch ( Utility.Random( 4 ) )
			{ 
				case 0: mobile.AddToBackpack( new Banner1() ); break; 
				case 1: mobile.AddToBackpack( new Banner2() ); break; 
				case 2: mobile.AddToBackpack( new Banner3() ); break; 
				case 3: mobile.AddToBackpack( new Banner4() ); break; 				
			}
		            mobile.SendGump( new RalphFinishGump( mobile ) );
         			return true;
         		}
         		else if( dropped is WormSilk )
         		{
					this.PrivateOverheadMessage( MessageType.Regular, 1153, 1054071, mobile.NetState );
         			return false;
				}
         		else
         		{
					this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "I have no need for that!", mobile.NetState );
     			}
			}
			return false;
		}
	}
}
