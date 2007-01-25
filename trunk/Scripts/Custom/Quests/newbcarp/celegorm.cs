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
    [CorpseName("Corpse of a carpenter")]
    public class Celegorm : Mobile
    {

        public virtual bool IsInvulnerable { get { return true; } }
       
        [Constructable]
        public Celegorm()
        {
            Title = ", Renowed Carpenter";
            Name = "Celegorm";
            Body = 400;
            Hue = 33770;
            VirtualArmor = 50;

            AddItem( new Server.Items.LongPants( Utility.RandomNeutralHue()) );
			AddItem( new Server.Items.FancyShirt( Utility.RandomNeutralHue()) );;
            AddItem( new Server.Items.HalfApron( Utility.RandomNeutralHue()) );
            AddItem(new Server.Items.Shoes(Utility.RandomNeutralHue()));
			
			Item hair = new Item( Utility.RandomList( 0x203B, 0x2049, 0x2048, 0x204A ) );
			
			    hair.Hue = Utility.RandomHairHue();
				hair.Layer = Layer.Hair;
				hair.Movable = false;
				AddItem( hair );

        
        }

       
        public Celegorm(Serial serial): base(serial)
        {
        }

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);
            list.Add(new CelegormEntry(from, this));
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

        public class CelegormEntry : ContextMenuEntry
        {
            private Mobile m_Mobile;
            private Mobile m_Giver;

            public CelegormEntry(Mobile from, Mobile giver): base(6146, 3)
            {
                m_Mobile = from;
                m_Giver = giver;
            }
			
			
			
		    public override void OnClick()
            {
                
                PlayerMobile mobile = (PlayerMobile)m_Mobile;
				Account acct=(Account)mobile.Account;
				bool CarpItemsReceived = Convert.ToBoolean( acct.GetTag("CarpItemsReceived") );
            
				{

					if ( CarpItemsReceived ) //account tag check
					
					{
					mobile.SendMessage("You have already done this quest.");
					
					}
					
					else
					
					{
					
					Item tm = mobile.Backpack.FindItemByType(typeof(NewbCarpMarker1));
					if (tm != null)
					{
					    Item si = mobile.Backpack.FindItemByType(typeof(WoodenBox));
						Item sd = mobile.Backpack.FindItemByType(typeof (Board));
						Item sk = mobile.Backpack.FindItemByType(typeof(WoodenChair));
						
                        if ( si == null || si.Amount < 1 || sd== null || sd.Amount < 10 || sk == null || sk.Amount < 1) 
						{
						mobile.SendMessage("You need more items");
						}	
						
						else
                        {
                        
                            mobile.SendGump(new NewbCarpQuestGump2(mobile));
                            mobile.Backpack.ConsumeTotal( typeof( WoodenBox ), 1 );
                            mobile.Backpack.ConsumeTotal( typeof( WoodenChair ), 1 );
                            mobile.Backpack.ConsumeTotal( typeof( Board ), 10 );
							tm.Delete();
	         			 	acct.SetTag( "CarpItemsReceived", "true" );
							
							
							switch ( Utility.Random( 1 ) )
							{
								
								case  0: mobile.AddToBackpack( new Nails( 500 ) );; break;
								
							
						
							}
                        }
                    }
					
										else
                            {
                                mobile.SendGump(new NewbCarpQuestGump(mobile));
							
                            }

					
					}
					
                    
                }
				
				
				
            }

       
    }
}
}
