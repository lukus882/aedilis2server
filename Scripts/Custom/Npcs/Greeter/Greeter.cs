using System;
using System.Collections;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;
using Server;
using Server.Items;
using Server.Gumps;

namespace Server.Mobiles
{
     	public class Greeter : BaseGuildmaster
	    {
		public override NpcGuild NpcGuild{ get{ return NpcGuild.TailorsGuild; } }

		public override bool ClickTitle{ get{ return false; } }
		
		private static bool m_Talked; // flag to prevent spam 
		
		      string[] kfcsay = new string[] // things to say while greating 
		      { 
		         "Welcome To The World of Aedilis. If this is your first visit please take some time to read through our website. You can get there by clicking on the welcome book in your pack.",   
      };

		[Constructable]
		public Greeter() : base( "Greeter" )
		{
			Name = "Greeter";
			Female = false;

			AddItem( new Server.Items.FancyShirt() );
			AddItem( new Server.Items.LongPants() );
			AddItem( new Server.Items.BodySash() );
			}
			public override void OnMovement( Mobile m, Point3D oldLocation ) 
			               {                                                    
			         if( m_Talked == false ) 
			         { 
			            if ( m.InRange( this, 4 ) ) 
			            {                
			               m_Talked = true; 
			               SayRandom( kfcsay, this ); 
			               this.Move( GetDirectionTo( m.Location ) ); 
			                  // Start timer to prevent spam 
			               SpamTimer t = new SpamTimer(); 
			               t.Start(); 
			            } 
			         } 
			      } 
			
			      private class SpamTimer : Timer 
			      { 
			         public SpamTimer() : base( TimeSpan.FromSeconds( 8 ) ) 
			         { 
			            Priority = TimerPriority.OneSecond; 
			         } 
			
			         protected override void OnTick() 
			         { 
			            m_Talked = false; 
			         } 
			      } 
			
			      private static void SayRandom( string[] say, Mobile m ) 
			      { 
			         m.Say( say[Utility.Random( say.Length )] ); 
			      } 
					public Greeter( Serial serial ) : base( serial )
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
		
		public class GreeterEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public GreeterEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
			{
				m_Mobile = from;
				m_Giver = giver;
			}

			public override void OnClick()
			{
				

                          if( !( m_Mobile is PlayerMobile ) )
					return;
				
				PlayerMobile mobile = (PlayerMobile) m_Mobile;

				{
					if ( ! mobile.HasGump( typeof( GreeterGump ) ) )
					{
						mobile.SendGump( new GreeterGump( mobile ));
						
				         	
				         }
				         
				  }       
				     
                        }
           }
   }   
}	
					 				       
					 				         