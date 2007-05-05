/**************************************
*Script Name: Generic Help Menu       *
*Author: Joeku                        *
*For use with RunUO 2.0               *
*Client Tested with: 5.0.7.1          *
*Version: 1.3                         *
*Initial Release: 10/05/06            *
*Revision Date: 01/12/07              *
**************************************/

using System;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using Server;
using Server.Commands;
using Server.Commands.Generic;
using Server.Engines.Help;
using Server.Gumps;
using Server.Menus;
using Server.Menus.Questions;
using Server.Network;
using Server.Misc;

namespace Server.Help
{
	public enum Subject
	{
		General = 0,
		Commands = 1,
		CommandsWithDescriptions = 2,
	}

	public class Variables
	{
		public static readonly string Website = "http://www.aedilis.us/";
		public static readonly string Email = "support@uo.com";	//Ex. "support@uo.com"
		public static readonly string Generic = String.Format( "If you wish to obtain more information, you can visit the website below");

		public static readonly int ErrorHue = 32;
		public static readonly int SuccessHue = 62;

		public static SubjectInfo[] Subjects = new SubjectInfo[]
		{
			new SubjectInfo( Subject.General,					"" ),
			new SubjectInfo( Subject.Commands,					"COMMAND DISPLAY" ),
			new SubjectInfo( Subject.CommandsWithDescriptions,	"COMMAND DISPLAY DESC" ),
			};

		public static string SubjectTitle( Subject subject )
		{
			string str = subject.ToString();

			int index = 1;
			int i = FindIndex(str, index);
			while( i >= 0 )
			{
				str = str.Insert(i, " ");
				index = i + 2;
				i = FindIndex(str, index);
			}

			return str;
		}

		public static int FindIndex(string s, int i)
		{
			char[] uppercase = new char[] {'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','W','X','Y','Z'};
			return( s.IndexOfAny( uppercase, i ) );
		}

		public static string CommandDisplay( Mobile m, bool des )
		{
			List<CommandEntry> list = new List<CommandEntry>();

			foreach ( CommandEntry entry in CommandSystem.Entries.Values )
				if ( m.AccessLevel >= entry.AccessLevel )
					list.Add( entry );

			list.Sort();

			StringBuilder sb = new StringBuilder();

			MethodInfo mi;
			string descString;
			object[] attrs;
			DescriptionAttribute desc;

			for ( int i = 0; i < list.Count; ++i )
			{
				string v = list[i].Command;
				if( des )
				{
					mi = list[i].Handler.Method;

					attrs = mi.GetCustomAttributes( typeof( DescriptionAttribute ), false );
					descString = " ...";
					if( attrs.Length > 0 )
					{
						desc = attrs[0] as DescriptionAttribute;
						if( desc.Description.Length > 0 )
							descString += " " + desc.Description.Replace( "<", "(" ).Replace( ">", ")" );
					}
					
					sb.Append( String.Format("  {0}) {1}{2}<br>", i+1, v, descString) );
				}
				else
				{
					sb.Append( String.Format("{0}, ", v) );
				}
			}

			string str = sb.ToString();
			if( str.EndsWith( "<br>" ) )
				str = str.Substring( 0, str.Length-4 );
			else
				str = str.Substring( 0, str.Length-2 );
			return str;
		}

		[Usage( "Help" )]
		[Description( "Brings up an extensive Help Menu; created by Joeku." )]
		public static void Help_OnCommand( CommandEventArgs e )
		{ e.Mobile.SendGump( new HelpMenu() ); }
	}

	public class SubjectInfo
	{
		private Subject p_Subject;
		private string p_Content, p_Title;
		
		public Subject Subject{ get{ return p_Subject; } set{ p_Subject = value; }}
		public string Content{ get{ return p_Content; } set{ p_Content = value; }}
		public string Title{ get{ return p_Title; } set{ p_Title = value; }}

		public SubjectInfo( Subject subject, string content ) : this( subject, null, content ){}

		public SubjectInfo( Subject subject, string title, string content )
		{
			p_Subject = subject;
			p_Title = title;
			p_Content = content;
		}
	}

	public class HelpMenu : Gump
	{
		private SubjectInfo p_Subject;

		public HelpMenu() : base( 250, 100 )
		{
			AddJunkOne();

			AddHtml(75, 100, 300, 70, String.Format("<center><u>Main Help Menu</u></center><br>You may use this to view any topics we feel have a degree of importance that need clarification. If you wish to obtain more information, you can visit the website below", Variables.Email), true, true); 
			AddBackground(75, 180, 300, 155, 9200); 

			AddJunkTwo();

			int pages = (int)Math.Ceiling((double)Variables.Subjects.Length/6);
			int startIndex, indexCount;

			for( int i = 1; i <= pages; i++ )
			{
				this.AddPage(i);
				startIndex = (i-1)*6;
				indexCount = Variables.Subjects.Length-startIndex;
				if( indexCount > 6 )
				{
					indexCount = 6;
					if( pages > i )
						AddButton(349, 309, 5602, 5606, 0, GumpButtonType.Page, i+1);
				}
				if(i > 1)
						AddButton(349, 210, 5600, 5604, 0, GumpButtonType.Page, i-1); 

				AddHtml(80, 187, 290, 30, String.Format("<center>Page {0} of {1}, subjects {2}-{3} of {4}</u></center>", i, pages, startIndex+1, startIndex+indexCount, Variables.Subjects.Length ), false, false); 

				string sub = "";
				for(int j = startIndex; j < startIndex+indexCount; j++)
				{
					sub = ( Variables.Subjects[j].Title != null ? Variables.Subjects[j].Title : Variables.SubjectTitle(Variables.Subjects[j].Subject));
					AddButton(85, 211+((j%6)*20), 10710, 10711, j+10, GumpButtonType.Reply, 0); 
					AddLabelCropped(115, 207+((j%6)*20), 225, 20, 0, sub); 
				}
			}
		}

		public HelpMenu( Subject subject, Mobile m ) : base( 250, 100 )
		{
			m.CloseGump( typeof( HelpMenu ) );
			p_Subject = Variables.Subjects[(int)subject];

			AddJunkOne();

			string st = p_Subject.Content;
			if( st == "COMMAND DISPLAY" )
				st = Variables.CommandDisplay( m, false );
			else if( st == "COMMAND DISPLAY DESC" )
				st = Variables.CommandDisplay( m, true );

			AddHtml(75, 100, 300, 220, String.Format("<center><u>{0}</u></center><br>{1}<br><p>{2}", ( p_Subject.Title != null ? p_Subject.Title : Variables.SubjectTitle(p_Subject.Subject)), st, Variables.Generic ), true, true); 

			AddImageTiled( 75, 310, 300, 2, 10000 );

			AddButton(100, 325, 10710, 10711, 1, GumpButtonType.Reply, 0); 
			AddLabel(135, 321, 0, @"View the Main Help Menu..."); 

			AddJunkTwo();
		}

		public void AddJunkOne()
		{
			AddPage(0);
			AddBackground(50, 60, 350, 375, 2600); 
			AddImage(0, 0, 10400);		//Dragon Head
			AddImage(0, 174, 10401);	//Dragon Body
			AddImage(0, 339, 10402);	//Dragon Tail

			AddImage(108, 38, 1419); 
			AddImage(184, 21, 1417); 
			AddImage(195, 30, 5608); 
			AddLabel(212, 41, 2101, @"Help"); 
			AddLabel(210, 54, 2101, @"Menu"); 
		}

		public void AddJunkTwo()
		{
			//AddButton(100, 345, 10710, 10711, 2, GumpButtonType.Reply, 0); 
			//AddLabel(135, 341, 0, @"Page staff for further assistance..."); 

			AddButton(100, 365, 10710, 10711, 3, GumpButtonType.Reply, 0); 
			AddLabel(135, 361, 0, String.Format("Visit the {0} website...", ServerList.ServerName )); 

			AddButton(194, 387, 247, 248, 0, GumpButtonType.Reply, 0); //Close
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;
			int button = info.ButtonID;

			switch( button )
			{
				default:
					Subject s = (Subject)(button-10);
					from.SendGump( new HelpMenu( s, from ) );
					break;
				case 0:
					break;
				case 1:
					from.SendGump( new HelpMenu()); break;
				case 2:
					from.SendGump( this );
					if( !PageQueue.CheckAllowedToPage( from ))
						break;
					if( PageQueue.Contains( from ))
						from.SendMenu( new ContainedMenu( from ));
					else
					{
						from.SendLocalizedMessage( 501234, "", 0x35 ); //Next available counselor will respond...
						if( p_Subject != null )
							PageQueue.Enqueue( new PageEntry( from, String.Format( "This is an automated message: {0} needs assistance with Help Menu subject \"{1}\".", from.Name, Variables.SubjectTitle(p_Subject.Subject) ), PageType.Other ) );
						else
							PageQueue.Enqueue( new PageEntry( from, String.Format( "This is an automated message: {0} needs assistance.", from.Name ), PageType.Other ) );
					}
					break;
				case 3:
					from.SendGump( this );
					from.SendMessage( "Launching website, please wait..." );
					from.LaunchBrowser( Variables.Website );
					break;
			}
		}
	}
}