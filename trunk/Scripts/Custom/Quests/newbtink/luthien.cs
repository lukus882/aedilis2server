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
    [CorpseName("Corpse of a tinker")]
    public class Luthien : Mobile
    {

        public virtual bool IsInvulnerable { get { return true; } }
       
        [Constructable]
        public Luthien()
        {
            Title = ", Renowed Tinker";
            Name = "Luthien";
            Body = 400;
            Hue = 33770;
            VirtualArmor = 50;
            Blessed = true;

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

       
        public Luthien(Serial serial): base(serial)
        {
        }

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);
            list.Add(new LuthienEntry(from, this));
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

        public class LuthienEntry : ContextMenuEntry
        {
            private Mobile m_Mobile;
            private Mobile m_Giver;

            public LuthienEntry(Mobile from, Mobile giver): base(6146, 3)
            {
                m_Mobile = from;
                m_Giver = giver;
            }
			
			
			
		    public override void OnClick()
            {
                
                PlayerMobile mobile = (PlayerMobile)m_Mobile;
				Account acct=(Account)mobile.Account;
				bool SmithItemsReceived = Convert.ToBoolean( acct.GetTag("TinkItemsReceived") );
            
				{

					if ( SmithItemsReceived ) //account tag check
					
					{
					mobile.SendMessage("You have already done this quest.");
					
					}
					
					else
					
					{
					
					Item tm = mobile.Backpack.FindItemByType(typeof(NewbTinkMarker1));
					if (tm != null)
					{
					    Item si = mobile.Backpack.FindItemByType(typeof(KeyRing));
						Item sd = mobile.Backpack.FindItemByType(typeof (SkinningKnife));
						Item sk = mobile.Backpack.FindItemByType(typeof(Goblet));
						
                        if ( si == null || si.Amount < 1 || sd== null || sd.Amount < 1 || sk == null || sk.Amount < 1) 
						{
						mobile.SendMessage("You need more items");
						}	
						
						else
                        {
                        
                            mobile.SendGump(new NewbTinkQuestGump2(mobile));
                            mobile.Backpack.ConsumeTotal( typeof( KeyRing ), 1 );
                            mobile.Backpack.ConsumeTotal( typeof( SkinningKnife ), 1 );
                            mobile.Backpack.ConsumeTotal( typeof( Goblet ), 1 );
							tm.Delete();
	         			 	acct.SetTag( "TinkItemsReceived", "true" );
							
							
							switch ( Utility.Random( 4 ) )
							{
								
								case  0: mobile.AddToBackpack( new TinkerTools (500) );; break;
								case  1: mobile.AddToBackpack( new StarSapphire (10) );; break;
								case  2: mobile.AddToBackpack( new IronIngot (100) );; break;
								case  3: mobile.AddToBackpack (new Board (100));; break;
								
							
						
							}
                        }
                    }
					
										else
                            {
                                mobile.SendGump(new NewbTinkQuestGump(mobile));
							
                            }

					
					}
					
                    
                }
				
				
				
            }

       
    }
}
}
