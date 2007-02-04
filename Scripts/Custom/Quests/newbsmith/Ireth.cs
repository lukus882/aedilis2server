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
    [CorpseName("Corpse of a smith")]
    public class Ireth : Mobile
    {

        public virtual bool IsInvulnerable { get { return true; } }
       
        [Constructable]
        public Ireth()
        {
            Title = ", Renowed Blacksmith";
            Name = "Ireth";
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

       
        public Ireth(Serial serial): base(serial)
        {
        }

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);
            list.Add(new IrethEntry(from, this));
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

        public class IrethEntry : ContextMenuEntry
        {
            private Mobile m_Mobile;
            private Mobile m_Giver;

            public IrethEntry(Mobile from, Mobile giver): base(6146, 3)
            {
                m_Mobile = from;
                m_Giver = giver;
            }
			
			
			
		    public override void OnClick()
            {
                
                		PlayerMobile mobile = (PlayerMobile)m_Mobile;
				Account acct=(Account)mobile.Account;
				bool SmithItemsReceived = Convert.ToBoolean( acct.GetTag("SmithItemsReceived") );
            
				{

					if ( SmithItemsReceived ) //account tag check
					
					{
					mobile.SendMessage("You have already done this quest.");
					
					}
					
					else
					
					{
					
					Item tm = mobile.Backpack.FindItemByType(typeof(NewbSmithMarker1));
					if (tm != null)
					{
					    Item si = mobile.Backpack.FindItemByType(typeof(IronIngot));
						Item sd = mobile.Backpack.FindItemByType(typeof (RepairDeed));
						Item sk = mobile.Backpack.FindItemByType(typeof(Katana));
						
                        if ( si == null || si.Amount < 10 || sd== null || sd.Amount < 1 || sk == null || sk.Amount < 1) 
						{
						mobile.SendMessage("You need more items");
						}	
						
						else
                        {
                        
                            mobile.SendGump(new NewbSmithQuestGump2(mobile));
                            mobile.Backpack.ConsumeTotal( typeof( RepairDeed ), 1 );
                            mobile.Backpack.ConsumeTotal( typeof( Katana ), 1 );
                            mobile.Backpack.ConsumeTotal( typeof( IronIngot ), 10 );
							tm.Delete();
	         			 	acct.SetTag( "SmithItemsReceived", "true" );
							
							
							switch ( Utility.Random( 13 ) )
							{
								
								
								case  0: mobile.AddToBackpack (new ValoriteIngot (500));; break;
								case  1: mobile.AddToBackpack (new IronIngot (1000));; break;
								case  2: mobile.AddToBackpack (new VeriteIngot (500));; break;
								case  3: mobile.AddToBackpack (new IronIngot (1000));; break;
								case  4: mobile.AddToBackpack (new CopperIngot (500));; break;
								case  5: mobile.AddToBackpack (new ShadowIronIngot (500));; break;
								case  6: mobile.AddToBackpack (new DullCopperIngot (500));; break;
								case  7: mobile.AddToBackpack (new AgapiteIngot (500));; break;
								case  8: mobile.AddToBackpack (new GoldIngot (500));; break;
								case  9: mobile.AddToBackpack (new BronzeIngot (500));; break;
								case  10: mobile.AddToBackpack( new RunicHammer( CraftResource.DullCopper, Core.AOS ? 20 : 20 ) );; break;
								case  11: mobile.AddToBackpack( new RunicHammer( CraftResource.ShadowIron, Core.AOS ? 20 : 20 ) );; break;
								case  12: mobile.AddToBackpack( new RunicHammer( CraftResource.Copper, Core.AOS ? 20 : 20 ) );; break;

								
								
							
						
							}
                        }
                    }
					
										else
                            {
                                mobile.SendGump(new NewbSmithQuestGump(mobile));
							
                            }

					
					}
					
                    
                }
				
				
				
            }

       
    }
}
}
