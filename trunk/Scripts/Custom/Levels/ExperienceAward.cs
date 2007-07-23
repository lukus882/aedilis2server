
using System; 
using Server.Misc;
using Server.Mobiles;

namespace Server.Mobiles
{
    public class ExperienceValidate
    {
        private Mobile m_Owner;

        public static void ExperienceAward(Mobile from, BaseCreature bc)
        {
            if (from.Backpack == null)
                return;

            int karma = Math.Abs(bc.Karma);
            int expbase = (/*bc.TotalGold*/ + karma + bc.Fame + ((bc.Hits + bc.Stam + bc.Mana) / 3)) / 4500;
            int maxexp = 6 + (30 * expbase);
            int minexp = (maxexp / 2);

            int amount = Utility.Random(minexp, maxexp);

            ExperienceGiven(from, amount);
        }

        public static void ExperienceGiven(Mobile from, int amount)
        {
            PlayerMobile pm = from as PlayerMobile;
            if (amount < 1)
                return;
            pm.PlayerLevel += amount;
            pm.SendMessage("You have received {0} experience points", amount);
        }
    }
}

