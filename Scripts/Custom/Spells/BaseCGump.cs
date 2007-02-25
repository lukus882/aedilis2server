using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Spells;
using Server.Network;

namespace Server.Gumps
{
	public abstract class BaseCGump : Gump
	{
		public Spellbook m_Book;
		public string[]  m_Defs;

		public abstract int TextHue{ get; }
		public abstract int BGImage{ get; }
		public abstract int SpellBtn{ get; }
		public abstract int SpellBtnP{ get; }
		public abstract string MLab{ get; }

		public BaseCGump( Mobile from, Spellbook book ) : base( 150, 200 )
		{
			m_Book = book;

			AddBackground();
			AddPage(1);

			AddLabel( 150, 17, TextHue, MLab );
			AddSpells();
		}

		private void AddBackground()
		{
			AddPage(0);
			AddImage( 100, 10, BGImage, 0 );
		}

		public virtual void AddSpells()
		{
			int NextPage = 2;

			for( int i = 0; i < m_Book.BookCount; i++ )
			{
				if( HasSpell( m_Book.BookOffset + i ) )
				{
					string[] Def = ParseDefFor( m_Book.BookOffset + i );
					if( Def[0] != null )
					{
						if( i > 0 && i % 16 == 0 )
						{
							AddButton( 396, 15, 2206, 2206, 0, GumpButtonType.Page, NextPage );
							AddPage(NextPage);
							AddButton( 123, 15, 2205, 2205, 0, GumpButtonType.Page, NextPage-1 );
							NextPage++;
						}

						AddLabel( (i>7?315:145), 40+(i>7?(i-8)*20:i*20), TextHue, Def[0] );
						AddButton( (i>7?295:125), 43+(i>7?(i-8)*20:i*20), SpellBtn, SpellBtnP, m_Book.BookOffset+i, GumpButtonType.Reply, 0 );
					}
				}
			}

			for( int i = 0; i < m_Book.BookCount; i++ )
			{
				if( HasSpell( m_Book.BookOffset + i ) )
				{
					string[] Def = ParseDefFor( m_Book.BookOffset + i );
					int Y = 35;

					AddButton( 396, 14, 2206, 2206, 0, GumpButtonType.Page, NextPage );
					AddPage(NextPage);
					AddButton( 123, 14, 2205, 2205, 0, GumpButtonType.Page, NextPage-1 );
					NextPage++;

					if( Def[0] != null )
						AddLabel( 150, 37, TextHue, Def[0] );
					if( Def[1] != null )
						AddHtml( 130, 60, 130, 145, "<basefont color=black>" + Def[1] + "</font>", false, (Def[1].Length > 130) );
					if( Def[2] != null )
					{
						string[] Regs = Def[2].Split(';');
						AddLabel( 295, Y, TextHue, "Reagents :" );
						Y += 20;
						foreach( string r in Regs )
						{
							AddLabel( 300, Y, TextHue, r.TrimStart() );
							Y += 20;
							if( Y > 185 )
								break;
						}
						Y += 10;
					}
					if( Def[3] != null && Y <= 185 )
					{
						string[] Info = Def[3].Split(';');
						foreach( string s in Info )
						{
							AddLabel( 295, Y, TextHue, s.TrimStart() );
							Y += 20;
							if( Y > 185 )
								break;
						}
					}
				}
			}

			AddButton( 396, 14, 2206, 2206, 0, GumpButtonType.Page, NextPage );
			AddPage(NextPage);
			AddButton( 123, 14, 2205, 2205, 0, GumpButtonType.Page, NextPage-1 );

		}

		private string[] ParseDefFor( int spellID )
		{
			string[] def = SpellDefRegistry.GetDefFor( spellID );
			return def;
		}

		public bool HasSpell( int spellID )
		{
			return( m_Book != null && m_Book.HasSpell( spellID ) );
		}
	}
}