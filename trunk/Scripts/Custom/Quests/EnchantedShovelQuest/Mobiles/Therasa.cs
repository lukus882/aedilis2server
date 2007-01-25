using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.ContextMenus;
using Server.Gumps;
using Server.Misc;
using Server.Network;
using Server.Spells;
using Server.Accounting;
using System.Collections.Generic;

namespace Server.Mobiles
{
	[CorpseName( "Therasa corpse" )]
	public class Therasa : Mobile
	{
                public virtual bool IsInvulnerable{ get{ return true; } }
		[Constructable]
		public Therasa()
		{
			Name = "Therasa";
                        Title = "the Miners Wife";
			Body = 0x191;
			CantWalk = true;
			Hue = Utility.RandomSkinHue();

			FancyDress fd = new FancyDress();
                        fd.Hue = 1172;
                        AddItem( fd );

                        Sandals s = new Sandals();
                        s.Hue = 1172;
                        AddItem( s );
                 
                        AddItem( new LongHair(2213));

		}

		public Therasa( Serial serial ) : base( serial )
		{
		}

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list) 
	        { 
	                base.GetContextMenuEntries( from, list ); 
        	        list.Add( new TherasaEntry( from, this ) ); 
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

		public class TherasaEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public TherasaEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
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
					if ( ! mobile.HasGump( typeof( TherasaGump ) ) )
					{
						mobile.SendGump( new TherasaGump( mobile ));
						mobile.AddToBackpack( new MagicConnectionBox() );
					} 
				}
			}
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{          		
         	        Mobile m = from;
			PlayerMobile mobile = m as PlayerMobile;
                        Account acct=(Account)from.Account;
			bool UnchargedEnchantedShovelRecieved = Convert.ToBoolean( acct.GetTag("UnchargedEnchantedShovelRecieved") );

			if ( mobile != null)
			{
				if( dropped is UnchargedEnchantedShovel )
            
         		{
         			if(dropped.Amount!=1)
         			{
					this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "Restore the power of the Shovel!", mobile.NetState );
         				return false;
         			}
                                if ( !UnchargedEnchantedShovelRecieved ) //added account tag check
		                {
					dropped.Delete(); 
					mobile.AddToBackpack( new EnchantedShovel() );
					mobile.SendMessage( "Thank you for your help!" );
                                        acct.SetTag( "UnchargedEnchantedShovelRecieved", "true" );

				
         		        }
				else //what to do if account has already been tagged
         			{
         				mobile.SendMessage("You already did this for me... oh well, suppose I should give you some gold anyway!");
         				mobile.AddToBackpack( new Gold( 2000 ) );
         				dropped.Delete();
         			}
         		}
         		else
         		{
					this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "Why on earth would I want to have that?", mobile.NetState );
     			}
			}
			return false;
		}
	}
}
