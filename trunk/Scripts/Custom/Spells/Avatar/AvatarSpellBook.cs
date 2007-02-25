using System;
using Server.Network;
using Server.Gumps;
using Server.Spells;
using Server.Spells.Avatar;

namespace Server.Items
{
	public class AvatarSpellbook : Spellbook
	{
		public override SpellbookType SpellbookType{ get{ return SpellbookType.Avatar; } }
		public override int BookOffset{ get{ return 210; } }
		public override int BookCount{ get{ return 3; } }

		[Constructable]
		public AvatarSpellbook() : this( (ulong)0 )
		{
		}

		[Constructable]
		public AvatarSpellbook( ulong content ) : base( content, 0xEFA )
		{
			Hue = 1150;
			Name = "Avatar Spell Book";
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.AccessLevel == AccessLevel.Player )
			{
				Container pack = from.Backpack;
				if( !(Parent == from || (pack != null && Parent == pack)) )
				{
					from.SendMessage( "The spellbook must be in your backpack [and not in a container within] to open." );
					return;
				}
			}

			from.CloseGump( typeof( AvatarSpellbookGump ) );
			from.SendGump( new AvatarSpellbookGump( from, this ) );
		}

		public AvatarSpellbook( Serial serial ) : base( serial )
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
