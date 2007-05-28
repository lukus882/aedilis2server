using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.ContextMenus;
using Server.Engines.Quests;
using Server.Engines.Quests.Necro;
using Server.Targeting;
using Server.Gumps;

namespace Server.Mobiles
{
	public class PetReviver : BaseQuester
	{
		public override int TalkNumber{ get{ return 489; } } // Resurrect
		public override bool ClickTitle{ get{ return true; } }
		public override bool IsActiveVendor{ get{ return false; } }
		public override bool DisallowAllMoves{ get{ return true; } }

		public ArrayList PetHolders;
		public Item exit;

		[Constructable]
		public PetReviver() : base( "the Pet Doctor" )
		{
			PetHolders = new ArrayList();
		}

		public PetReviver( Serial serial ) : base( serial )
		{
		}

		public override void InitBody()
		{
			InitStats( 100, 100, 25 );

			this.Body = 0x191;
			this.Name = NameList.RandomName( "female" );
			NameHue = 0x491; //This is my new Color for 'Unique/Custom NPCs'

			Hue = 0x3ea;

		}

		public override void OnDelete()
		{
			for (int i=0;i<PetHolders.Count;i++)
			{
				if (PetHolders != null)
				{
					if (PetHolders[i] != null)
					{
						if (PetHolders[i] is PetHoldingChamber)
						{
							PetHoldingChamber chamberdelete = PetHolders[i] as PetHoldingChamber;
							i=i-1;
							chamberdelete.Delete();
						}
					}
				}
			}
			if (exit != null)
			{
				if (exit is Item)
				{
					exit.Delete();
				}
			}
		}

		private PetHoldingChamber m_petholder;

		private const int holdrange = 24;
/*
		public PetHoldingChamber Chamber
		{
			get
			{
				if ( m_petholder == null || m_petholder.Deleted || m_petholder.Map != this.Map || !Utility.InRange( m_petholder.Location, this.Location, holdrange ) )
				{
					foreach ( Item item in GetItemsInRange( holdrange ) )
					{
						if ( item is PetHoldingChamber )
						{
							m_petholder=(PetHoldingChamber)item;
							PetHolders.Add((PetHoldingChamber)item);
							break;
						}
					}
				}

				return m_petholder;
			}
		}
*/
		public override void OnSpeech(SpeechEventArgs e)
		{
			Mobile m = e.Mobile;

			if( m.GetDistanceToSqrt(new Point3D(this.X,this.Y,this.Z)) > 5 )
			{
				return;
			}
		
			string mPhrase = e.Speech.Trim();

			if ( mPhrase.Length == 0 )
			{
				return;
			}

			if( Insensitive.Equals(mPhrase,"claim"))
			{
				for (int i=0; i< PetHolders.Count;i++)
				{
					if (PetHolders != null)
					{
						if (PetHolders[i] != null)
						{
							if (PetHolders[i] is PetHoldingChamber)
							{
								PetHoldingChamber chamber = PetHolders[i] as PetHoldingChamber;
								if((chamber.m_pet) != null)
								{
									if ((chamber.m_pet) is BaseCreature)
									{
										BaseCreature pet = (chamber.m_pet) as BaseCreature;
										if (pet.ControlMaster == m)
										{
											if ((chamber.HealCount <= 0))
											{
												if (this.exit != null)
												{
													if (this.exit is PetExitPoint)
													{
														PetExitPoint exitpoint = this.exit as PetExitPoint;
														(chamber.m_pet).Location = exitpoint.Location;
														(chamber.m_pet) = null;
														if (chamber.heal != null)
														{
															(chamber.heal).Stop();
														}
													}
												}
												else
												{
													(chamber.m_pet).Location = m.Location;
													(chamber.m_pet) = null;
													if (chamber.heal != null)
													{
														(chamber.heal).Stop();
													}
												}
												
												this.Say("Your pet has been restored!");
												break;
											}
											else
											{
												this.Say("Your pet has not fully recovered yet, Sorry! Come back soon!");
												m.SendMessage("Your pet has not fully recovered yet..");
												break;
											}
										}
									}
								}
							}
						}
					}
				}
			}
			else
			{
				return;
			}

		}

		public override void InitOutfit()
		{

			EquipItem( SetHue( new Sandals(), 0x481 ) );
			EquipItem( SetHue( new SkullCap(), 0x481 ) );
			EquipItem( SetHue( new PlainDress(), 0x481 ) );
		}

		public override bool CanTalkTo( PlayerMobile to )
		{
			return true;
		}

		public override void OnTalk( PlayerMobile player, bool contextMenu )
		{
			player.Target = new PetReviveTarget(player,this);
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			writer.WriteItemList(PetHolders, true);
			writer.Write( exit );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			PetHolders = reader.ReadItemList();
			exit = reader.ReadItem();
		}
	}

	public class PetReviveTarget : Target
	{
		private PlayerMobile m_from;
		private PetReviver m_doctor;

		public PetReviveTarget( PlayerMobile player,PetReviver doctor ) : base( 10, false, TargetFlags.None )
		{
			m_from = player;
			m_doctor=doctor;
		}

		protected override void OnTarget( Mobile from, object targeted)
		{
			bool full=true;
			bool nodupes=true;
			if (targeted is BaseCreature)
			{
				BaseCreature mount = targeted as BaseCreature;
				for (int j=0;j<(m_doctor.PetHolders).Count;j++)
				{
					if (m_doctor.PetHolders[j] is PetHoldingChamber)
					{
						PetHoldingChamber comparechamber = m_doctor.PetHolders[j] as PetHoldingChamber;
						if (comparechamber.m_pet != null)
						{
							if(comparechamber.m_pet is BaseCreature)
							{
								BaseCreature comparemount = comparechamber.m_pet as BaseCreature;
								//from.SendMessage(comparemount.ToString());
								//from.SendMessage(mount.ToString());
								if (mount == comparemount)
								{
									nodupes=false;
									break;
								}
							}
						}
					}
				}
				if (nodupes)
				{
					if (mount.IsDeadPet)
					{
						if ( mount.ControlMaster != from )
						{
							from.SendMessage("You cannot select a pet that is not yours!");
						}
						else
						{
							for (int i=0; i < (m_doctor.PetHolders).Count ;i++)
							{
								if (m_doctor.PetHolders[i] is PetHoldingChamber)
								{
									PetHoldingChamber petchamber = m_doctor.PetHolders[i] as PetHoldingChamber;
									if (petchamber.m_pet == null)
									{
										from.CloseGump(typeof(PetReviveGump));
										from.SendGump( new PetReviveGump(from, mount,petchamber,m_doctor) );
										full=false;
										break;
									}
								}
							}
							if (full)
							{
								m_doctor.Say("Sorry! We're full. We cannot accept anymore pets! Try another city!");
								from.SendMessage("We're Sorry, but the Hospital is currently FULL! Please try another city!");
							}
							
						}
					}
					else
					{
						m_doctor.Say("Are you a Hypochondriac? Your pet is fine, and doesnt require my assistance.");
						from.SendMessage("That pet is not dead!");
					}
				}
				else
				{
					from.SendMessage("You cant help a pet thats already being helped!");
				}
			}
		}
	}
}