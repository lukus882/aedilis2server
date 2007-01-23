using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Prompts;
using Server.Targeting;
using Server.Engines.XmlSpawner2;


namespace Server.Gumps
{

	public class XmlQuestStatusGump : Gump
	{
		public static string Color( string text, string color )
		{
			return String.Format( "<BASEFONT COLOR=#{0}>{1}</BASEFONT>", color, text );
		}


		public void DisplayQuestStatus(int x, int y, string objectivestr, string statestr, bool status, string descriptionstr)
		{
			if(objectivestr != null && objectivestr.Length > 0)
			{
				// look for special keywords
				string [] arglist = BaseXmlSpawner.ParseString(objectivestr,5, ",");
				int targetcount=1;
				bool foundkill = false;
				bool foundcollect = false;
				bool foundgive = false;
				bool foundescort = false;
				string name=null;
				string mobname=null;
				string type = null;

				string status_str;
				string text = null;
				string typestr;
				bool checkprop;

				if(arglist.Length > 0)
					switch(arglist[0])
					{
						case "GIVE":
							// format for the objective string will be GIVE,mobname,itemtype[,count][,proptest]
							if(arglist.Length > 2)
							{
								mobname = arglist[1];
								//name = arglist[2];
								type = arglist[2];
							}

							XmlQuest.CheckArgList(arglist,3,null,out typestr,out targetcount,out checkprop,out status_str);

							foundgive = true;
							break;
						case "GIVENAMED":
							// format for the objective string will be GIVENAMED,mobname,itemname[,type][,count][,proptest]
							if(arglist.Length > 2)
							{
								mobname = arglist[1];
								name = arglist[2];
							}

							XmlQuest.CheckArgList(arglist,3,null,out typestr,out targetcount,out checkprop,out status_str);

							if(typestr != null) type = typestr;

							foundgive = true;
							break;
						case "KILL":
							// format for the objective string will be KILL,mobtype[,count][,proptest]

							if(arglist.Length > 1)
							{
								//name = arglist[1];
								type = arglist[1];
							}

							XmlQuest.CheckArgList(arglist,2,null,out typestr,out targetcount,out checkprop,out status_str);

							foundkill = true;
							break;

						case "KILLNAMED":
							// format for the objective string KILLNAMED,mobname[,type][,count][,proptest]
							if(arglist.Length > 1)
							{
								name = arglist[1];
							}

							XmlQuest.CheckArgList(arglist,2,null,out typestr,out targetcount,out checkprop,out status_str);
                        
							if(typestr != null) type = typestr;

							foundkill = true;
							break;
						case "COLLECT":
							// format for the objective string will be COLLECT,itemtype[,count][,proptest]
							if(arglist.Length > 1)
							{
								//name = arglist[1];
								type = arglist[1];
							}

							XmlQuest.CheckArgList(arglist,2,null,out typestr,out targetcount,out checkprop,out status_str);



							foundcollect = true;
							break;
						case "COLLECTNAMED":
							// format for the objective string will be COLLECTNAMED,itemname[,itemtype][,count][,proptest]
							if(arglist.Length > 1)
							{
								name = arglist[1];
							}

							XmlQuest.CheckArgList(arglist,2,null,out typestr,out targetcount,out checkprop,out status_str);
                        
							if(typestr != null) type = typestr;

							foundcollect = true;
							break;
						case "ESCORT":
							// format for the objective string will be ESCORT,mobname[,proptest]
							if(arglist.Length > 1)
							{
								name = arglist[1];
							}
							foundescort = true;
							break;
					}

				if(foundkill)
				{
					// get the current kill status
					int killed = 0;
					try
					{
						killed = int.Parse(statestr);
					} 
					catch{}

					int remaining = targetcount-killed;

					if(remaining < 0) remaining = 0;

					// report the kill task objective status
					if(descriptionstr != null)
						text = String.Format("{0} ({1} left)",descriptionstr,remaining);
					else
					{
						if(name != null)
						{
							if(type == null) type = "mob";

							text = String.Format("Kill {0} {1}(s) named {2} ({3} left)",targetcount, type, name, remaining);
						}
						else
							text = String.Format("Kill {0} {1}(s) ({2} left)",targetcount, type, remaining);
					}
				} 
				else
					if(foundescort)
				{
					// get the current escort status
					int escorted = 0;
					try
					{
						escorted = int.Parse(statestr);
					} 
					catch{}

					int remaining = targetcount-escorted;

					if(remaining < 0) remaining = 0;

					// report the escort task objective status
					if(descriptionstr != null)
						text = descriptionstr;
					else
						text = String.Format("Escort {0}",name);
				} 
				else
					if(foundcollect)
				{
					// get the current collection status
					int collected = 0;
					try
					{
						collected = int.Parse(statestr);
					} 
					catch{}

					int remaining = targetcount-collected;

					if(remaining < 0) remaining = 0;

					// report the collect task objective status
					if(descriptionstr != null)
						text = String.Format("{0} ({1} left)",descriptionstr,remaining);
					else
					{
						if(name != null)
						{
							if(type == null) type = "mob";

							text = String.Format("Collect {0} {1}(s) named {2} ({3} left)", targetcount, type, name, remaining);
						}
						else
							text = String.Format("Collect {0} {1}(s) ({2} left)", targetcount, type, remaining);
					}
				} 
				else
					if(foundgive)
				{
					// get the current give status
					int collected = 0;

					try
					{
						collected = int.Parse(statestr);
					} 
					catch{}

					int remaining = targetcount-collected;

					if(remaining < 0) remaining = 0;

					// report the collect task objective status
					if(descriptionstr != null)
						text = String.Format("{0} ({1} left)",descriptionstr,remaining);
					else
					{
						if(name != null)
						{
							if(type == null) type = "item";

							text = String.Format("Give {0} {1}(s) named {2} to {3} ({4} left)",targetcount, type, name, mobname, remaining);
						}
						else
							text = String.Format("Give {0} {1}(s) to {2} ({3} left)",targetcount, type, mobname, remaining);
					}
				} 
				else
				{
					// just report the objectivestring
					text = objectivestr;
				}
                
				AddHtml( x, y, 223, 35, XmlSimpleGump.Color( text, "EFEF5A" ), false , false );

				if(status)
				{
					AddImage( x-20, y+3, 0x939 ); // bullet
					AddHtmlLocalized(x+222, y, 225, 37, 1046033, 0xff42 , false , false ); // Complete
				} 
				else 
				{
					AddImage( x-20, y+3, 0x938 ); // bullet
					AddHtmlLocalized(x+222, y, 225, 37, 1046034, 0x7fff , false , false ); // Incomplete
				}
			}
		}

		private IXmlQuest m_questitem;
		private string m_gumptitle;
		private int m_X;
		private int m_Y;
		private bool m_solid;

		public XmlQuestStatusGump( IXmlQuest questitem , string gumptitle) : this( questitem, gumptitle, 0, 0 , false)
		{
		}

		public XmlQuestStatusGump( IXmlQuest questitem , string gumptitle, int X, int Y, bool solid) : base( X, Y )
		{
			Closable = true;
			Dragable = true;
			m_X = X;
			m_Y = Y;
			m_solid = solid;
			m_questitem = questitem;
			m_gumptitle = gumptitle;
			AddPage( 0 );

			if(!solid)
			{
				AddImageTiled(  54, 33, 369, 400, 2624 );
				AddAlphaRegion( 54, 33, 369, 400 );
			} 
			else
			{
				AddBackground(  54, 33, 369, 400, 5054 );
			}

			AddImageTiled( 416, 39, 44, 389, 203 );

			//			AddButton( 338, 392, 2130, 2129, 3, GumpButtonType.Reply, 0 ); // Okay button

			AddHtmlLocalized( 139, 59, 200, 30, 1046026, 0x7fff, false , false ); // Quest Log
			AddImage( 97, 49, 9005 ); // quest ribbon

			AddImageTiled( 58, 39, 29, 390, 10460 ); // left hand border
			AddImageTiled( 412, 37, 31, 389, 10460 ); // right hand border
			AddImage( 430, 9, 10441);
			AddImageTiled( 40, 38, 17, 391, 9263 );
			AddImage( 6, 25, 10421 );
			AddImage( 34, 12, 10420 );
			AddImageTiled( 94, 25, 342, 15, 10304 ); // top border
			AddImageTiled( 40, 414, 415, 16, 10304 ); // bottom border
			AddImage( -10, 314, 10402 );
			AddImage( 56, 150, 10411 );

			AddImage( 136, 84, 96 );
			AddImage( 372, 57, 1417);
			AddImage( 381, 66, 5576);

			if(gumptitle != null && gumptitle.Length > 0)
			{ // display the title if it is there
				AddImage( 146, 91, 2103 ); // bullet
				AddHtml( 164, 86, 200, 30, XmlSimpleGump.Color( gumptitle, "00FF42" ), false, false );
			}

			if(questitem.NoteString != null && questitem.NoteString.Length > 0)
			{ // display the note string if it is there
				AddHtml( 100, 106, 270, 80, questitem.NoteString, true, true );
			}

			DisplayQuestStatus(130, 192, questitem.Objective1, questitem.State1, questitem.Completed1, questitem.Description1);
			DisplayQuestStatus(130, 224, questitem.Objective2, questitem.State2, questitem.Completed2, questitem.Description2);
			DisplayQuestStatus(130, 256, questitem.Objective3, questitem.State3, questitem.Completed3, questitem.Description3);
			DisplayQuestStatus(130, 288, questitem.Objective4, questitem.State4, questitem.Completed4, questitem.Description4);
			DisplayQuestStatus(130, 320, questitem.Objective5, questitem.State5, questitem.Completed5, questitem.Description5);

			//if(questitem.HasCollect){
			AddButton( 100, 350, 0x2A4E, 0x2A3A, 700, GumpButtonType.Reply, 0 );
			AddLabel( 135, 356, 0x384, "Collect" );
			//}

			if((questitem.RewardItem != null && !questitem.RewardItem.Deleted))
			{
				m_questitem.CheckRewardItem();
                
				if(questitem.RewardItem.Amount > 1)
				{
					AddLabel( 230, 356, 55, String.Format("Reward: {0} ({1})", questitem.RewardItem.GetType().Name,
						questitem.RewardItem.Amount) );
					AddLabel( 230, 373, 55, String.Format("Weight: {0}", questitem.RewardItem.Weight*questitem.RewardItem.Amount));
				} 
				else
					if(questitem.RewardItem is Container)
				{
					AddLabel( 230, 356, 55, String.Format("Reward: {0} ({1} items)", questitem.RewardItem.GetType().Name,
						questitem.RewardItem.TotalItems));
					AddLabel( 230, 373, 55, String.Format("Weight: {0}", questitem.RewardItem.TotalWeight + questitem.RewardItem.Weight));
				} 
				else
				{
					AddLabel( 230, 356, 55, String.Format("Reward: {0}", questitem.RewardItem.GetType().Name));
					AddLabel( 230, 373, 55, String.Format("Weight: {0}", questitem.RewardItem.Weight));
				}
				AddImageTiled( 330, 373, 81, 40, 200 );
				AddItem(340,376, questitem.RewardItem.ItemID);

			}
			if(questitem.RewardAttachment != null && !questitem.RewardAttachment.Deleted)
			{
				AddLabel( 230, 339, 55, String.Format("Bonus: {0}",questitem.RewardAttachment.GetType().Name));
			}
            
			if((questitem.RewardItem != null && !questitem.RewardItem.Deleted) || (questitem.RewardAttachment != null && !questitem.RewardAttachment.Deleted))
			{
				if(questitem.CanSeeReward)
				{
					AddButton( 400, 380, 2103, 2103, 800, GumpButtonType.Reply, 0 );
				}
			}

			// indicate any status info
			XmlQuest.VerifyObjectives(questitem);
			if(questitem.Status != null)
			{
				AddLabel( 100, 392, 33, questitem.Status);
			} 
			else
				// indicate the expiration time
				if(questitem.IsValid)
			{
				//AddHtmlLocalized(150, 400, 50, 37, 1046033, 0xf0000 , false , false ); // Expires
				AddHtml( 130, 392, 200, 37, XmlSimpleGump.Color( questitem.ExpirationString, "00FF42" ), false, false );
			} 
			else
				if(questitem.AlreadyDone)
			{
				if(!questitem.Repeatable)
				{
					AddLabel( 100, 392, 33, "Already done - cannot be repeated" );
				} 
				else
				{
					ArrayList a = XmlAttach.FindAttachments(questitem.Owner,typeof(XmlQuestAttachment), questitem.Name);
					if(a != null && a.Count > 0)
					{
						AddLabel( 100, 392, 33, String.Format("Repeatable in {0}" ,((XmlQuestAttachment)a[0]).Expiration));
					} 
					else 
					{
						AddLabel( 150, 392, 33, "Already done - ???" );
					}
				}
			} 
			else
			{
				//AddHtml( 150, 384, 200, 37, XmlSimpleGump.Color( "No longer valid", "00FF42" ), false, false );
				AddLabel( 150, 392, 33, "No longer valid" );
			}
			if(XmlQuest.QuestPointsEnabled)
			{
				AddHtml( 250, 40, 200, 30, XmlSimpleGump.Color( String.Format("Difficulty Level {0}",questitem.Difficulty), "00FF42" ), false, false );
			}
			if(questitem.PartyEnabled)
			{
				AddHtml( 250, 55, 200, 30, XmlSimpleGump.Color( "Party Quest", "00FF42" ), false, false );
				if(questitem.PartyRange >= 0)
				{
					AddHtml( 250, 70, 200, 30, XmlSimpleGump.Color( String.Format("Party Range {0}",questitem.PartyRange), "00FF42" ), false, false );
				} 
				else 
				{
					AddHtml( 250, 70, 200, 30, XmlSimpleGump.Color( "No Range Limit", "00FF42" ), false, false );
				}
			} 
			else 
			{
				AddHtml( 250, 55, 200, 30, XmlSimpleGump.Color( "Solo Quest", "00FF42" ), false, false );
			}
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			if(info == null || state == null || state.Mobile == null || state.Mobile.Deleted)
				return;
			switch ( info.ButtonID )
			{
				case 700:
					state.Mobile.Target = new XmlQuest.GetCollectTarget(m_questitem);
					state.Mobile.SendGump( new XmlQuestStatusGump( m_questitem, m_gumptitle, m_X, m_Y, m_solid) );
					break;
				case 800:
					if(m_questitem.RewardItem != null || m_questitem.RewardAttachment != null)
					{
						// open a new status gump
						state.Mobile.SendGump( new XmlQuestStatusGump( m_questitem, m_gumptitle, m_X, m_Y, m_solid) );
					}
					// display the reward item
					if(m_questitem.RewardItem != null)
					{
						// show the contents of the xmlquest pack
						if(m_questitem.Pack != null)
							m_questitem.Pack.DisplayTo(state.Mobile);
					}
					// identify the reward attachment
					if(m_questitem.RewardAttachment != null)
					{
						//state.Mobile.SendMessage("{0}",m_questitem.RewardAttachment.OnIdentify(state.Mobile));
						state.Mobile.CloseGump(typeof(DisplayAttachmentGump));
						state.Mobile.SendGump(new DisplayAttachmentGump(state.Mobile, m_questitem.RewardAttachment.OnIdentify(state.Mobile)));
					}
					break;
			}
		}
        
		private class DisplayAttachmentGump : Gump
		{
			public DisplayAttachmentGump( Mobile from, string text) : base( 0,0)
			{
				// prepare the page
				AddPage( 0 );
    
				AddBackground( 0, 0, 400, 150, 5054 );
				AddAlphaRegion( 0, 0, 400, 150 );
				AddLabel( 20, 2, 55, "Quest Attachment Description" );

				AddHtml( 20,20, 360, 110, text, true , true );
			}
		}
	}
}
