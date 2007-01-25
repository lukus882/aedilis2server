/*

Scripted by Rosey1


*/

using System;
using Server;
using Server.ContextMenus;
using Server.Accounting;
using Server.Gumps;
using Server.Items;
using Server.Menus;
using Server.Menus.Questions;
using Server.Mobiles;
using Server.Network;
using Server.Prompts;
using Server.Regions;
using Server.Misc;
using System.Collections;
using System.Collections.Generic;

namespace Server.Mobiles
{
    [CorpseName("Corpse of a tailor")]
    public class Tari : Mobile
    {

        public virtual bool IsInvulnerable { get { return true; } }
       
        [Constructable]
        public Tari()
		{
        
            Title = ", Renowed Tailor";
            Name = "Tari";
            Body = 401;
            Hue = 33770;
			CantWalk=true;

            VirtualArmor = 50;

            Item hair = new Item( Utility.RandomList( 0x203B, 0x203C, 0x203D, 0x2044, 0x2045, 0x2047, 0x2049, 0x204A ) );
            hair.Hue = Utility.RandomHairHue();
		    hair.Layer = Layer.Hair;
            hair.Movable = false;
            AddItem( hair );

            AddItem(new Server.Items.Skirt( Utility.RandomNeutralHue() ));
			AddItem(new Server.Items.Doublet( Utility.RandomNeutralHue() ));
            AddItem(new Server.Items.Sandals(0));

            GoldNecklace goldnecklace = new GoldNecklace();
            goldnecklace.Hue = 0;
            goldnecklace.Movable = false;
            AddItem(goldnecklace);

            GoldRing goldring = new GoldRing();
            goldring.Hue = 0;
            goldring.Movable = false;
            AddItem(goldring);
			
			Blessed = true;

        
        }

       
        public Tari(Serial serial): base(serial)
        {
        }

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);
            list.Add(new TariEntry(from, this));
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }

        public class TariEntry : ContextMenuEntry
        {
            private Mobile m_Mobile;
            private Mobile m_Giver;

            public TariEntry(Mobile from, Mobile giver): base(6146, 3)
            {
                m_Mobile = from;
                m_Giver = giver;
            }
			
			
			
		    public override void OnClick()
            {
                
                PlayerMobile mobile = (PlayerMobile)m_Mobile;
				Account acct=(Account)mobile.Account;
				bool TailorItemsReceived = Convert.ToBoolean( acct.GetTag("TailorItemsReceived") );
            
				{

					if ( TailorItemsReceived ) //account tag check
					
					{
					mobile.SendMessage("You have already done this quest.");
					
					}
					
					else
					
					{
					
					Item tm = mobile.Backpack.FindItemByType(typeof(NewbTailorMarker1));
					if (tm != null)
					{
					    Item tw = mobile.Backpack.FindItemByType(typeof(Wool));
						Item tb = mobile.Backpack.FindItemByType(typeof(BoltOfCloth));
						Item tt = mobile.Backpack.FindItemByType(typeof(SpoolOfThread));
						
                        if ( tw == null || tw.Amount < 10 || tb== null || tb.Amount < 10 || tt == null || tt.Amount < 10) 
						{
						mobile.SendMessage("You need more items or make sure that the only items in your pack are the ones I asked for.");
						}	
						
						else
                        {
                        
                            mobile.SendGump(new NewbTailorQuestGump2(mobile));
                            mobile.Backpack.ConsumeTotal( typeof( Wool ), 10 );
                            mobile.Backpack.ConsumeTotal( typeof( BoltOfCloth ), 10 );
                            mobile.Backpack.ConsumeTotal( typeof( SpoolOfThread ), 10 );
							tm.Delete();
	         			 	acct.SetTag( "TailorItemsReceived", "true" );
							
							
							switch ( Utility.Random( 4 ) )
							{
								
								case  0: mobile.AddToBackpack( new RunicSewingKit( CraftResource.SpinedLeather, Core.AOS ? 20 : 20 ) );; break;
								case  1: mobile.AddToBackpack( new RunicSewingKit( CraftResource.HornedLeather, Core.AOS ? 20 : 20 ) );; break;
								case  2: mobile.AddToBackpack( new RunicSewingKit( CraftResource.BarbedLeather, Core.AOS ? 20 : 20 ) );; break;
								case  3: {BoltOfCloth boc = new BoltOfCloth (); 
boc.Hue = Utility.RandomList( 0x481, 0x489, 0x48E, 0x494, 0x484, 0x48D, 0x497 ); 
mobile.AddToBackpack( boc ); 
};; break;
								
							
						
							}
                        }
                    }
					
										else
                            {
                                mobile.SendGump(new NewbTailorQuestGump(mobile));
							
                            }

					
					}
					
                    
                }
				
				
				
            }

       
    }
}
}
