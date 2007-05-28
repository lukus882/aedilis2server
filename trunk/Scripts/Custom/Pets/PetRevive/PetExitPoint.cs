using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;
using Server.Engines.Quests;

namespace Server.Items
{
	public class PetExitPoint : Item
	{
		public Mobile m_doctor;

		[Constructable]
		public PetExitPoint() : base( 0x1F14 )
		{
			Name = "Pet Holding Slot";
			Visible = false;
			Movable = false;
		}

		public PetExitPoint( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.Target=  new AssignPetExiter(this);
			from.SendMessage( "What do you wish to assign this to?" );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( (m_doctor ) );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_doctor = reader.ReadMobile();
		}
	}

	public class AssignPetExiter : Target
	{
		private PetExitPoint m_chamber;

		public AssignPetExiter( PetExitPoint chamber ) : base( 10, false, TargetFlags.None )
		{
			m_chamber=chamber;
		}

		protected override void OnTarget( Mobile from, object targeted)
		{
			if (targeted is PetReviver)
			{
				PetReviver doctor = targeted as PetReviver;
				doctor.exit = m_chamber;
				m_chamber.m_doctor = targeted as Mobile;
				from.SendMessage("Exit has been bounded to that doctor.");
			}			
		}
	}
}