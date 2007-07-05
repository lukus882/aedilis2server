///////////////////////////////////////////////////
//						 //
// C# Exporter Generated: 3/28/2006 2:47:45 AM   //
// Designed by Ravenal of OrBSydia		 //
// Version: 2.0					 //
//						 //
// Script: [Pet Death Gump] : by Doomsday Dragon //
// Version 1.0					 //
//						 //
///////////////////////////////////////////////////

using System;
using Server;
using Server.Network;
using Server.Mobiles;

namespace Server.Gumps
{
    public class PetDeathGump : Gump
    {
        public PetDeathGump( BaseCreature bc ) :base( 0, 0 )
        {
            Closable   = true;
            Disposable = true;
            Dragable   = true;

            AddPage(0);
            AddBackground(521, 467, 275, 129, 9270);
 
            AddImage(557, 503, 111); 
            AddImage(694, 504, 111); 
            AddImage(622, 484, 9000);
 
            AddAlphaRegion(531, 477, 255, 109);
 
            AddItem(729, 532, 3795, 0); 
            AddItem(748, 520, 3809, 0); 
            AddItem(758, 503, 3800, 0);
 
            AddLabel(626, 482, 32, @"Pet Death!"); 
            AddLabel(534, 501, 32, @"Your pet:");
            AddLabel(593, 530, 32, @String.Format("[{0}]", bc.Name)); 
            AddLabel(632, 564, 32, @"Has been slain in battle!");
 
            AddImageTiled(525, 498, 266, 4, 96);
            AddImageTiled(525, 480, 266, 4, 96);
 
            AddItem(534, 557, 8707, 0); 
            AddItem(619, 584, 6883, 0); 
            AddItem(657, 584, 6882, 0); 
            AddItem(639, 584, 6884, 0); 
            AddItem(622, 585, 6881, 0); 
            AddItem(626, 584, 6882, 0); 
            AddItem(597, 584, 6883, 0); 
            AddItem(602, 584, 6884, 0); 
            AddItem(595, 585, 6880, 0); 
            AddItem(574, 584, 6883, 0); 
            AddItem(579, 584, 6884, 0); 
            AddItem(572, 585, 6880, 0); 
            AddItem(558, 585, 6881, 0); 
            AddItem(565, 584, 6884, 0); 
            AddItem(538, 584, 6883, 0); 
            AddItem(542, 585, 6880, 0); 
            AddItem(535, 584, 6884, 0); 
            AddItem(538, 584, 6882, 0); 
            AddItem(521, 585, 6880, 0); 
            AddItem(499, 584, 6883, 0); 
            AddItem(655, 586, 6880, 0); 
            AddItem(672, 584, 6882, 0); 
            AddItem(668, 586, 6881, 0); 
            AddItem(676, 584, 6884, 0); 
            AddItem(671, 584, 6883, 0); 
            AddItem(690, 586, 6881, 0); 
            AddItem(709, 584, 6882, 0); 
            AddItem(707, 586, 6881, 0); 
            AddItem(725, 584, 6882, 0); 
            AddItem(722, 584, 6884, 0); 
            AddItem(729, 585, 6881, 0); 
            AddItem(747, 584, 6882, 0); 
            AddItem(745, 585, 6881, 0); 
            AddItem(753, 584, 6884, 0); 
            AddItem(749, 584, 6883, 0); 
            AddItem(504, 466, 8708, 0); 
            AddItem(771, 466, 8708, 0); 
            AddItem(504, 507, 8708, 0); 
            AddItem(771, 506, 8708, 0); 
            AddItem(656, 464, 6882, 0); 
            AddItem(618, 464, 6883, 0); 
            AddItem(638, 464, 6884, 0); 
            AddItem(622, 466, 6881, 0); 
            AddItem(654, 466, 6880, 0); 
            AddItem(602, 465, 6883, 0); 
            AddItem(606, 466, 6880, 0); 
            AddItem(598, 465, 6884, 0); 
            AddItem(600, 465, 6882, 0); 
            AddItem(582, 465, 6880, 0); 
            AddItem(561, 464, 6883, 0); 
            AddItem(671, 465, 6882, 0); 
            AddItem(655, 465, 6883, 0); 
            AddItem(676, 465, 6880, 0); 
            AddItem(693, 465, 6882, 0);  
            AddItem(563, 465, 6881, 0); 
            AddItem(556, 465, 6880, 0); 
            AddItem(557, 465, 6882, 0); 
            AddItem(539, 465, 6884, 0); 
            AddItem(531, 465, 6881, 0); 
            AddItem(511, 465, 6883, 0); 
            AddItem(521, 465, 6882, 0); 
            AddItem(517, 466, 6880, 0); 
            AddItem(690, 465, 6884, 0); 
            AddItem(715, 465, 6882, 0); 
            AddItem(697, 466, 6881, 0); 
            AddItem(713, 466, 6880, 0); 
            AddItem(730, 465, 6882, 0); 
            AddItem(727, 465, 6884, 0); 
            AddItem(723, 465, 6883, 0); 
            AddItem(743, 466, 6880, 0); 
            AddItem(760, 465, 6882, 0); 
            AddItem(757, 465, 6884, 0); 
            AddItem(762, 466, 6881, 0); 

            AddImage(624, 440, 9804);
	}

        public override void OnResponse( NetState sender, RelayInfo info )
        {
        }
    }
}
