using System; 
using Server; 
using Server.Gumps;
using Server.Network;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Mobiles;
using Server.Engines.Quests;

namespace Server.Gumps
{
	public class PetReviveGump : Gump
	{
		private Mobile m_from;
		private BaseCreature m_mount;
		private PetHoldingChamber i_petchamber;
		private PetReviver m_doctor;

		public static void Initialize()
		{
		}
		
		public PetReviveGump( Mobile from, BaseCreature mount,PetHoldingChamber petchamber,PetReviver doctor ) : base( 50,50 )
		{
			from.CloseGump( typeof( PetReviveGump ) );

			m_from = from;
			m_mount = mount;
			i_petchamber = petchamber;
			m_doctor = doctor;

			AddPage( 0 );

			AddBackground( 10, 10, 265, 140, 0x242C );

			AddItem( 205, 40, 0x4 );
			AddItem( 227, 40, 0x5 );

			AddItem( 180, 78, 0xCAE );
			AddItem( 195, 90, 0xCAD );
			AddItem( 218, 95, 0xCB0 );

			//AddHtmlLocalized( 30, 30, 150, 75, 1049665, false, false ); // <div align=center>Wilt thou sanctify the resurrection of:</div>
			AddHtml(30,30,150,75, String.Format( "Would you like to spend 6000 gold to revive this pet?", 0 ), false,false);
			AddHtml( 30, 70, 150, 25, String.Format( "<div align=CENTER>{0}</div>", mount.Name ), true, false );

			AddButton( 40, 105, 0x81A, 0x81B, 1, GumpButtonType.Reply, 0 ); // Okay
			AddButton( 110, 105, 0x819, 0x818, 0, GumpButtonType.Reply, 0 ); // Cancel
		}

		public void ToPetRoom (Mobile from,BaseCreature mount,PetHoldingChamber petchamber,PetReviver m_doctor)
		{
			mount.Location = petchamber.Location;
			mount.Loyalty = 100;
			mount.CantWalk = true;
			mount.ControlOrder = OrderType.Stay;
			petchamber.m_pet=mount;
			petchamber.HealCount = 15;

			petchamber.heal = new PetHoldingChamberHeal( petchamber );
			petchamber.heal.Start();

			from.SendMessage("The doctor has put your pet in a recovery room.");

			m_doctor.Say("Emergancy! Get this animal to a Healing Room, Stat!");
		}
		
		public override void OnResponse( NetState state, RelayInfo info)
		{ //Determine what they have "chosen", its either <Yes> or <No>
		
			Mobile from = state.Mobile;
			Container cont;

			switch (info.ButtonID)
			{
				case 0:
				{
					from.SendMessage("You decide not to revive a pet.");
					break;
				}
				case 1:
				{
					if ( from.InRange( m_doctor.Location, 5 ) )
					{
						BankBox box = from.BankBox;
						if ( box != null )
						{
							Container pack = from.Backpack;
							cont = from.BankBox;
	
							if (cont != null  &&  cont.ConsumeTotal( typeof( Gold ), (6000) ))
							{
								ToPetRoom(m_from,m_mount,i_petchamber,m_doctor);
								break;
							}
							if ( pack != null && pack.ConsumeTotal( typeof( Gold ), (6000)))
							{
								ToPetRoom(m_from,m_mount,i_petchamber,m_doctor);
								break;
							}
						}
					}
					else
					{
						from.SendMessage("You are too far away from the doctor!");
					}
					break;
				}
			}
		}
	}
}