using System;
using Server;
using Server.Gumps;
using Server.Network;
using System.Collections;
using Server.Mobiles;
using Server.Accounting;
using Server.Commands;
using Server.Commands.Generic;
namespace Server.Gumps
{
	public class Capfix
	{
		public static void Initialize()
		{
            	CommandSystem.Register("Capfix", AccessLevel.Owner, new CommandEventHandler(Capfix_OnCommand));
		}

		[Usage( "Capfix" )]
		[Description( "Lowers the cap and skill levels of everyone over 100 " )]
        	private static void Capfix_OnCommand(CommandEventArgs e)
		{

				ArrayList results = new ArrayList();
				string matchEntry = e.GetString( 0 );
				string search = matchEntry.ToLower();
				ArrayList mobiles = new ArrayList( World.Mobiles.Values );

				foreach( Mobile m in mobiles )
				{
					if (m != null && m is PlayerMobile)
					{

//caps
						if ( m.Skills[SkillName.Alchemy].Cap > 100 )
						{
							m.Skills.Alchemy.Cap = 100;
						}
		
						if ( m.Skills[SkillName.Anatomy].Cap > 100 )
						{
							m.Skills.Anatomy.Cap = 100;
						}
						if ( m.Skills[SkillName.AnimalLore].Cap > 100 )
						{
							m.Skills.AnimalLore.Cap = 100;
						}
						if ( m.Skills[SkillName.ItemID].Cap > 100 )
						{
							m.Skills.ItemID.Cap = 100;
						}
						if ( m.Skills[SkillName.ArmsLore].Cap > 100 )
						{
							m.Skills.ArmsLore.Cap = 100;
						}
						if ( m.Skills[SkillName.Parry].Cap > 100 )
						{
							m.Skills.Parry.Cap = 100;
						}
						if ( m.Skills[SkillName.Begging].Cap > 100 )
						{
							m.Skills.Begging.Cap = 100;					
						}
						if ( m.Skills[SkillName.Fletching].Cap > 100 )
						{
							m.Skills.Fletching.Cap = 100;
						}
						if ( m.Skills[SkillName.Peacemaking].Cap > 100 )
						{
							m.Skills.Peacemaking.Cap = 100;
						}
						if ( m.Skills[SkillName.Camping].Cap > 100 )
						{
							m.Skills.Camping.Cap = 100;
						}
						if ( m.Skills[SkillName.Carpentry].Cap > 100 )
						{
							m.Skills.Carpentry.Cap = 100;
						}
						if ( m.Skills[SkillName.Cartography].Cap > 100 )
						{
							m.Skills.Cartography.Cap = 100;
						}
						if ( m.Skills[SkillName.Cooking].Cap > 100 )
						{
							m.Skills.Cooking.Cap = 100;
						}
						if ( m.Skills[SkillName.DetectHidden].Cap > 100 )
						{
							m.Skills.DetectHidden.Cap = 100;
						}
						if ( m.Skills[SkillName.Discordance].Cap > 100 )
						{
							m.Skills.Discordance.Cap = 100;
						}
						if ( m.Skills[SkillName.EvalInt].Cap > 100 )
						{
							m.Skills.EvalInt.Cap = 100;
						}
						if ( m.Skills[SkillName.Healing].Cap > 100 )
						{
							m.Skills.Healing.Cap = 100;
						}
						if ( m.Skills[SkillName.Fishing].Cap > 100 )
						{
							m.Skills.Fishing.Cap = 100;
						}
						if ( m.Skills[SkillName.Forensics].Cap > 100 )
						{
							m.Skills.Forensics.Cap = 100;
						}
						if ( m.Skills[SkillName.Herding].Cap > 100 )
						{
							m.Skills.Herding.Cap = 100;
						}
						if ( m.Skills[SkillName.Hiding].Cap > 100 )
						{
							m.Skills.Hiding.Cap = 100;
						}
						if ( m.Skills[SkillName.Provocation].Cap > 100 )
						{
							m.Skills.Provocation.Cap = 100;
						}
						if ( m.Skills[SkillName.Inscribe].Cap > 100 )
						{
							m.Skills.Inscribe.Cap = 100;
						}
						if ( m.Skills[SkillName.Lockpicking].Cap > 100 )
						{
							m.Skills.Lockpicking.Cap = 100;
						}
						if ( m.Skills[SkillName.Magery].Cap > 100 )
						{
							m.Skills.Magery.Cap = 100;
						}
						if ( m.Skills[SkillName.MagicResist].Cap > 100 )
						{
							m.Skills.MagicResist.Cap = 100;
						}
						if ( m.Skills[SkillName.Tactics].Cap > 100 )
						{
							m.Skills.Tactics.Cap = 100;
						}
						if ( m.Skills[SkillName.Snooping].Cap > 100 )
						{
							m.Skills.Snooping.Cap = 100;
						}
						if ( m.Skills[SkillName.Musicianship].Cap > 100 )
						{
							m.Skills.Musicianship.Cap = 100;
						}
						if ( m.Skills[SkillName.Poisoning].Cap > 100 )
						{
							m.Skills.Poisoning.Cap = 100;
						}
						if ( m.Skills[SkillName.Archery].Cap > 100 )
						{
							m.Skills.Archery.Cap = 100;
						}
						if ( m.Skills[SkillName.SpiritSpeak].Cap > 100 )
						{
							m.Skills.SpiritSpeak.Cap = 100;
						}
						if ( m.Skills[SkillName.Stealing].Cap > 100 )
						{
							m.Skills.Stealing.Cap = 100;
						}
						if ( m.Skills[SkillName.AnimalTaming].Cap > 100 )
						{
							m.Skills.AnimalTaming.Cap = 100;
						}
						if ( m.Skills[SkillName.TasteID].Cap > 100 )
						{
							m.Skills.TasteID.Cap = 100;
						}
						if ( m.Skills[SkillName.Tinkering].Cap > 100 )
						{
							m.Skills.Tinkering.Cap = 100;
						}
						if ( m.Skills[SkillName.Tracking].Cap > 100 )
						{
							m.Skills.Tracking.Cap = 100;
						}
						if ( m.Skills[SkillName.Veterinary].Cap > 100 )
						{
							m.Skills.Veterinary.Cap = 100;
						}
						if ( m.Skills[SkillName.Swords].Cap > 100 )
						{
							m.Skills.Swords.Cap = 100;
						}
						if ( m.Skills[SkillName.Macing].Cap > 100 )
						{
							m.Skills.Macing.Cap = 100;
						}
						if ( m.Skills[SkillName.Fencing].Cap > 100 )
						{
							m.Skills.Fencing.Cap = 100;
						}
						if ( m.Skills[SkillName.Wrestling].Cap > 100 )
						{
							m.Skills.Wrestling.Cap = 100;
						}
						if ( m.Skills[SkillName.Lumberjacking].Cap > 100 )
						{
							m.Skills.Lumberjacking.Cap = 100;
						}
						if ( m.Skills[SkillName.Mining].Cap > 100 )
						{
							m.Skills.Mining.Cap = 100;
						}
						if ( m.Skills[SkillName.Meditation].Cap > 100 )
						{
							m.Skills.Meditation.Cap = 100;
						}
						if ( m.Skills[SkillName.Stealth].Cap > 100 )
						{
							m.Skills.Stealth.Cap = 100;
						}
						if ( m.Skills[SkillName.RemoveTrap].Cap > 100 )
						{
							m.Skills.RemoveTrap.Cap = 100;
						}
						if ( m.Skills[SkillName.Necromancy].Cap > 100 )
						{
							m.Skills.Necromancy.Cap = 100;
						}
						if ( m.Skills[SkillName.Focus].Cap > 100 )
						{
							m.Skills.Focus.Cap = 100;
						}
						if ( m.Skills[SkillName.Chivalry].Cap > 100 )
						{
							m.Skills.Chivalry.Cap = 100;
						}
						if ( m.Skills[SkillName.Bushido].Cap > 100 )
						{
							m.Skills.Bushido.Cap = 100;
						}
						if ( m.Skills[SkillName.Ninjitsu].Cap > 100 )
						{
							m.Skills.Ninjitsu.Cap = 100;
						}
						if ( m.Skills[SkillName.Spellweaving].Cap > 100 )
						{
							m.Skills.Spellweaving.Cap = 100;
						}

//end caps

// values
						if ( m.Skills[SkillName.Alchemy].Value > 100 )
						{
							m.Skills.Alchemy.Base = 100;
						}

						if ( m.Skills[SkillName.Anatomy].Value > 100 )
						{
							m.Skills.Anatomy.Base = 100;
						}
						if ( m.Skills[SkillName.AnimalLore].Value > 100 )
						{
							m.Skills.AnimalLore.Base = 100;
						}
						if ( m.Skills[SkillName.ItemID].Value > 100 )
						{
							m.Skills.ItemID.Base = 100;
						}
						if ( m.Skills[SkillName.ArmsLore].Value > 100 )
						{
							m.Skills.ArmsLore.Base = 100;
						}
						if ( m.Skills[SkillName.Parry].Value > 100 )
						{
							m.Skills.Parry.Base = 100;
						}
						if ( m.Skills[SkillName.Begging].Value > 100 )
						{
							m.Skills.Begging.Base = 100;					
						}
						if ( m.Skills[SkillName.Fletching].Value > 100 )
						{
							m.Skills.Fletching.Base = 100;
						}
						if ( m.Skills[SkillName.Peacemaking].Value > 100 )
						{
							m.Skills.Peacemaking.Base = 100;
						}
						if ( m.Skills[SkillName.Camping].Value > 100 )
						{
							m.Skills.Camping.Base = 100;
						}
						if ( m.Skills[SkillName.Carpentry].Value > 100 )
						{
							m.Skills.Carpentry.Base = 100;
						}
						if ( m.Skills[SkillName.Cartography].Value > 100 )
						{
							m.Skills.Cartography.Base = 100;
						}
						if ( m.Skills[SkillName.Cooking].Value > 100 )
						{
							m.Skills.Cooking.Base = 100;
						}
						if ( m.Skills[SkillName.DetectHidden].Value > 100 )
						{
							m.Skills.DetectHidden.Base = 100;
						}
						if ( m.Skills[SkillName.Discordance].Value > 100 )
						{
							m.Skills.Discordance.Base = 100;
						}
						if ( m.Skills[SkillName.EvalInt].Value > 100 )
						{
							m.Skills.EvalInt.Base = 100;
						}
						if ( m.Skills[SkillName.Healing].Value > 100 )
						{
							m.Skills.Healing.Base = 100;
						}
						if ( m.Skills[SkillName.Fishing].Value > 100 )
						{
							m.Skills.Fishing.Base = 100;
						}
						if ( m.Skills[SkillName.Forensics].Value > 100 )
						{
							m.Skills.Forensics.Base = 100;
						}
						if ( m.Skills[SkillName.Herding].Value > 100 )
						{
							m.Skills.Herding.Base = 100;
						}
						if ( m.Skills[SkillName.Hiding].Value > 100 )
						{
							m.Skills.Hiding.Base = 100;
						}
						if ( m.Skills[SkillName.Provocation].Value > 100 )
						{
						m.Skills.Provocation.Base = 100;
						}
						if ( m.Skills[SkillName.Inscribe].Value > 100 )
						{
							m.Skills.Inscribe.Base = 100;
						}
						if ( m.Skills[SkillName.Lockpicking].Value > 100 )
						{
							m.Skills.Lockpicking.Base = 100;
						}
						if ( m.Skills[SkillName.Magery].Value > 100 )
						{
							m.Skills.Magery.Base = 100;
						}
						if ( m.Skills[SkillName.MagicResist].Value > 100 )
						{
							m.Skills.MagicResist.Base = 100;
						}
						if ( m.Skills[SkillName.Tactics].Value > 100 )
						{
							m.Skills.Tactics.Base = 100;
						}
						if ( m.Skills[SkillName.Snooping].Value > 100 )
						{
							m.Skills.Snooping.Base = 100;
						}
						if ( m.Skills[SkillName.Musicianship].Value > 100 )
						{
							m.Skills.Musicianship.Base = 100;
						}
						if ( m.Skills[SkillName.Poisoning].Value > 100 )
						{
							m.Skills.Poisoning.Base = 100;
						}
						if ( m.Skills[SkillName.Archery].Value > 100 )
						{
							m.Skills.Archery.Base = 100;
						}
						if ( m.Skills[SkillName.SpiritSpeak].Value > 100 )
						{
							m.Skills.SpiritSpeak.Base = 100;
						}
						if ( m.Skills[SkillName.Stealing].Value > 100 )
						{
							m.Skills.Stealing.Base = 100;
						}
						if ( m.Skills[SkillName.AnimalTaming].Value > 100 )
						{
							m.Skills.AnimalTaming.Base = 100;
						}
						if ( m.Skills[SkillName.TasteID].Value > 100 )
						{
							m.Skills.TasteID.Base = 100;
						}
						if ( m.Skills[SkillName.Tinkering].Value > 100 )
						{
							m.Skills.Tinkering.Base = 100;
						}
						if ( m.Skills[SkillName.Tracking].Value > 100 )
						{
							m.Skills.Tracking.Base = 100;
						}
						if ( m.Skills[SkillName.Veterinary].Value > 100 )
						{
							m.Skills.Veterinary.Base = 100;
						}
						if ( m.Skills[SkillName.Swords].Value > 100 )
						{
							m.Skills.Swords.Base = 100;
						}
						if ( m.Skills[SkillName.Macing].Value > 100 )
						{
							m.Skills.Macing.Base = 100;
						}
						if ( m.Skills[SkillName.Fencing].Value > 100 )
						{
							m.Skills.Fencing.Base = 100;
						}
						if ( m.Skills[SkillName.Wrestling].Value > 100 )
						{
							m.Skills.Wrestling.Base = 100;
						}
						if ( m.Skills[SkillName.Lumberjacking].Value > 100 )
						{
							m.Skills.Lumberjacking.Base = 100;
						}
						if ( m.Skills[SkillName.Mining].Value > 100 )
						{
							m.Skills.Mining.Base = 100;
						}
						if ( m.Skills[SkillName.Meditation].Value > 100 )
						{
							m.Skills.Meditation.Base = 100;
						}
						if ( m.Skills[SkillName.Stealth].Value > 100 )
						{
							m.Skills.Stealth.Base = 100;
						}
						if ( m.Skills[SkillName.RemoveTrap].Value > 100 )
						{
							m.Skills.RemoveTrap.Base = 100;
						}
						if ( m.Skills[SkillName.Necromancy].Value > 100 )
						{
							m.Skills.Necromancy.Base = 100;
						}
						if ( m.Skills[SkillName.Focus].Value > 100 )
						{
							m.Skills.Focus.Base = 100;
						}
						if ( m.Skills[SkillName.Chivalry].Value > 100 )
						{
							m.Skills.Chivalry.Base = 100;
						}
						if ( m.Skills[SkillName.Bushido].Value > 100 )
						{
							m.Skills.Bushido.Base = 100;
						}
						if ( m.Skills[SkillName.Ninjitsu].Value > 100 )
						{
							m.Skills.Ninjitsu.Base = 100;
						}
						if ( m.Skills[SkillName.Spellweaving].Value > 100 )
						{
							m.Skills.Spellweaving.Base = 100;
						}

//end values
					}


				}
		}
	}
}