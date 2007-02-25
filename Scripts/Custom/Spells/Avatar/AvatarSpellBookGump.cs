using System;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Network;

namespace Server.Spells.Avatar
{
	public class AvatarSpellbookGump : BaseCGump
	{
		public override int TextHue{   get{ return 2307; } }
		public override int BGImage{   get{ return 2203; } }
		public override int SpellBtn{  get{ return 2362; } }
		public override int SpellBtnP{ get{ return 2361; } }
		public override string MLab{   get{ return "Avatar Spells"; } }

		public AvatarSpellbookGump( Mobile from, AvatarSpellbook book ) : base( from, book )
		{
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;
			int BID = info.ButtonID;

			if( BID >= 210 && BID <= 213 )
			{
				Spell spell = SpellRegistry.NewSpell( BID, from, null );
				if( spell != null )
					spell.Cast();
			}
		}
	}
}