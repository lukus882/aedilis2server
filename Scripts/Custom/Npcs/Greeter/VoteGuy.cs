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
     	public class VoteGuy : BaseGuildmaster
	    {
		public override NpcGuild NpcGuild{ get{ return NpcGuild.TailorsGuild; } }

		public override bool ClickTitle{ get{ return false; } }
		
		private static bool m_Talked; // flag to prevent spam 
		
		      string[] kfcsay = new string[] // things to say while greating 
		      { 
		         "Hey, I hear there is this dimensional voting thing. Have you voted for Aedilis yet? You can vote once per week on our primary stone and once a day on the secondary stone. Click on the voting stone to help other visitors find out about our world.",   
      };

		[Constructable]
		public VoteGuy() : base( "VoteGuy" )
		{
			Name = "VoteGuy";
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
					public VoteGuy( Serial serial ) : base( serial )
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
					 				       
					 				         