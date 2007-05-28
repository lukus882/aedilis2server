using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;
using Server.Engines.Quests;

namespace Server.Items
{
	public class PetHoldingChamber : Item
	{
		public Mobile m_pet;
		public Mobile m_doctor;
		public int HealCount;
		public Timer heal;

		[Constructable]
		public PetHoldingChamber() : base( 0x1F14 )
		{
			Name = "Pet Holding Slot";
			Visible = false;
			Movable = false;
		}

		public PetHoldingChamber( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.Target=  new AssignPetHolder(this);
			from.SendMessage( "What do you wish to assign this to?" );
		}

		public override void OnDelete()
		{
			if (m_doctor != null)
			{
				if (m_doctor is PetReviver)
				{
					PetReviver doctor = m_doctor as PetReviver;
					if (doctor.PetHolders != null)
					{
						for (int i=0;i<(doctor.PetHolders).Count;i++)
						{
							if (doctor.PetHolders[i] == this)
							{
								(doctor.PetHolders).Remove(doctor.PetHolders[i]);
							}
						}
					}
				}
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			writer.Write( m_pet );
			writer.Write( m_doctor );
			writer.Write( HealCount );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_pet = reader.ReadMobile();
			m_doctor = reader.ReadMobile();
			HealCount = reader.ReadInt();

			if (m_pet != null)
			{
				if (m_pet is BaseCreature)
				{
					BaseCreature pet = m_pet as BaseCreature;
					this.heal = new PetHoldingChamberHeal( this );
					this.heal.Start();
				}
			}
		}
	}

	public class PetHoldingChamberHeal : Timer 
	{ 
		private PetHoldingChamber m_chamber; 
		
		public PetHoldingChamberHeal( PetHoldingChamber chamber ) : base( TimeSpan.FromMinutes( 1 ) , TimeSpan.FromMinutes( 1 ) ) 
		{ 
			m_chamber = chamber; 
		} 
		
		protected override void OnTick()
		{
			if ((m_chamber.m_doctor) != null && (m_chamber.m_doctor) is PetReviver)
			{
				PetReviver doctor = (m_chamber.m_doctor) as PetReviver;
				if (m_chamber.m_pet != null)
				{
					if ((m_chamber.m_pet) is BaseCreature)
					{
						BaseCreature pet = (m_chamber.m_pet) as BaseCreature;
						if (m_chamber.HealCount - 1 >= 0)
						{
							(m_chamber.HealCount)--;
						}
						else
						{
							if (m_chamber.HealCount <= -5)
							{
								if (pet.ControlMaster != null)
								{
									(pet.ControlMaster).SendMessage("Your creature has been revived! Your pet has now been moved outside. You must go get it before it decides to disown you!");
								}

								if (doctor.exit == null)
								{
									pet.Location = doctor.Location;
									(m_chamber.m_pet) = null;
									if (m_chamber.heal != null)
									{
										(m_chamber.heal).Stop();
									}
								}
								else
								{
									if (doctor.exit is PetExitPoint)
									{
										PetExitPoint exitpoint = doctor.exit as PetExitPoint;
										pet.Location = exitpoint.Location;
										(m_chamber.m_pet) = null;
										if (m_chamber.heal != null)
										{
											(m_chamber.heal).Stop();
										}
									}
									this.Stop();
								}
							}
							else
							{
								if (pet.IsDeadPet)
								{
									pet.ResurrectPet();
									pet.CantWalk = false;
									pet.ControlOrder = OrderType.Stay;
									for ( int i = 0; i < pet.Skills.Length; ++i )	//Decrease all skills on pet.
									{
										pet.Skills[i].Base -= 0.2;
									}
								}
								int x = 5 - (m_chamber.HealCount * -1);

								if (pet.ControlMaster != null)
								{
									(pet.ControlMaster).SendMessage("Your creature has been revived! Return to the doctor and 'claim' it. You have " + x + " minute(s) to claim it, or it will be dismissed from the hospital automatically.");
								}

								(m_chamber.HealCount)--;
							}	
						}
						pet.Loyalty = 100;
					}
				}
			}
		}
	}

	public class AssignPetHolder : Target
	{
		private PetHoldingChamber m_chamber;

		public AssignPetHolder( PetHoldingChamber chamber ) : base( 10, false, TargetFlags.None )
		{
			m_chamber=chamber;
		}

		protected override void OnTarget( Mobile from, object targeted)
		{
			bool FoundItem = false;
			PetHoldingChamber petslot;

			if (targeted is PetReviver)
			{
				PetReviver doctor = targeted as PetReviver;

				if (doctor.PetHolders != null)
				{
					for (int i=0;i<(doctor.PetHolders).Count;i++)
					{
						if (doctor.PetHolders[i] is PetHoldingChamber )
						{
							petslot = doctor.PetHolders[i] as PetHoldingChamber;
							if (petslot == m_chamber)
							{
								from.SendMessage("You cannot add this item twice!");
								FoundItem = true;
								break;
							}
						}
					}
					if (FoundItem == false)
					{
						from.SendMessage("Pet Holder has been added to the Doctor.");
						(doctor.PetHolders).Add(m_chamber);
						m_chamber.m_doctor=doctor as Mobile;
					}
				}
				else
				{
					from.SendMessage("Pet Holder has been added to the Doctor.");
					from.SendMessage(m_chamber.ToString());
					m_chamber.m_doctor=doctor;
					(doctor.PetHolders).Add(m_chamber);
				}
			}			
		}
	}
}