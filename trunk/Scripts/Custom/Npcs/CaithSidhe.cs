using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a witches corpse" )]
	public class Witch : BaseCreature
	{
		private bool m_ShowHours;
		private int m_WitchBeginHour;
		private int m_WitchEndHour;

		public bool caithsidhe;
		public int lastminute = 0;

		public override bool ShowFameTitle{ get{ return false; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public bool ShowHours
		{
			get { return m_ShowHours; }
			set { m_ShowHours = value; InvalidateProperties(); }
		}
		[CommandProperty( AccessLevel.GameMaster )]
		public int WitchBeginHour
		{
			get{ return m_WitchBeginHour; }
			set{ m_WitchBeginHour = value; InvalidateProperties(); }
		}
		[CommandProperty( AccessLevel.GameMaster )]
		public int WitchEndHour
		{
			get{ return m_WitchEndHour; }
			set{ m_WitchEndHour = value; InvalidateProperties(); }
		}

		private static bool m_Talked;
		string[] WitchSay = new string[]
		{
			"Come kitty, kitty, kitty...",
			"I can't find my cat. Have you seen it?"
		};
		string[] CaithSidheSay = new string[]
		{
            "Miaow"
		};

		[Constructable]
		public Witch() : base( AIType.AI_Melee, FightMode.None, 10, 1, 0.2, 0.4 )
		{

            Name = "Agrona";
            Title = "the witch";
			Body = 401;

            SpeechHue = Utility.RandomDyedHue();
			Hue = 0x83EA;
            HairItemID = 0x2FCD;
            HairHue = 1153;

			caithsidhe = false;
			m_WitchBeginHour = 7;
			m_WitchEndHour = 20;

            this.SetStr(81, 105);
            this.SetDex(91, 115);
            this.SetInt(96, 120);

            this.SetHits(49, 63);

            this.SetDamage(5, 10);

            this.SetDamageType(ResistanceType.Physical, 100);

            this.SetResistance(ResistanceType.Physical, 15, 20);
            this.SetResistance(ResistanceType.Fire, 5, 10);
            this.SetResistance(ResistanceType.Poison, 5, 10);
            this.SetResistance(ResistanceType.Energy, 5, 10);

            this.SetSkill(SkillName.EvalInt, 75.1, 100.0);
            this.SetSkill(SkillName.Magery, 75.1, 100.0);
            this.SetSkill(SkillName.MagicResist, 75.0, 97.5);
            this.SetSkill(SkillName.Tactics, 65.0, 87.5);
            this.SetSkill(SkillName.Wrestling, 20.2, 60.0);

			Fame = 2500;
			Karma = -2500;

			VirtualArmor = 90;

			PlainDress dress = new PlainDress();
			dress.Hue = 1175;
			AddItem( dress );

			AddItem( new Sandals() );
            AddItem( new GnarledStaff());
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
		}

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			base.OnMovement( m, oldLocation );
			int hours, minutes;

			Server.Items.Clock.GetTime( m.Map, m.X, m.Y, out hours, out minutes );

			if ( (m_ShowHours) && (m.AccessLevel > AccessLevel.Player) )
			{
				if (minutes > lastminute)
				{
					m.SendMessage( "Hour: "+hours+" Minute: "+minutes);
					lastminute = minutes;
				}
			}

			if ( ((hours >= m_WitchBeginHour) && (hours <= m_WitchEndHour)) )
			{
				if ( caithsidhe )
				{
                    if (this.Combatant != null)
                        return;

                    else
                    {

                        this.SetStr(81, 105);
                        this.SetDex(91, 115);
                        this.SetInt(96, 120);

                        this.SetHits(49, 63);

                        this.SetDamage(5, 10);

                        this.SetDamageType(ResistanceType.Physical, 100);

                        this.SetResistance(ResistanceType.Physical, 15, 20);
                        this.SetResistance(ResistanceType.Fire, 5, 10);
                        this.SetResistance(ResistanceType.Poison, 5, 10);
                        this.SetResistance(ResistanceType.Energy, 5, 10);

                        this.SetSkill(SkillName.EvalInt, 75.1, 100.0);
                        this.SetSkill(SkillName.Magery, 75.1, 100.0);
                        this.SetSkill(SkillName.MagicResist, 75.0, 97.5);
                        this.SetSkill(SkillName.Tactics, 65.0, 87.5);
                        this.SetSkill(SkillName.Wrestling, 20.2, 60.0);

                        this.Fame = 2500;
                        this.Karma = -2500;

                        this.Hue = 0x83EA;

                        this.Name = "Agrona";
                        this.Title = "the witch";
                        this.Body = 401;

                        this.HairItemID = 0x2FCD;
                        this.HairHue = 1153;

                        PlainDress dress = new PlainDress();
                        dress.Hue = 1175;
                        AddItem(dress);

                        AddItem(new Sandals());

                        AddItem(new GnarledStaff());

                        this.FightMode = FightMode.None;
                        this.Kills = 0;

                        caithsidhe = false;
                    }
				}
			}
			else
			{
				if ( !caithsidhe )
				{

                    UnEquip(this, Layer.Shoes);
                    UnEquip(this, Layer.OuterTorso);
                    UnEquip(this, Layer.TwoHanded);

                    this.Hue = 1175;
                    this.Name = "a caith sidhe";
                    this.Title = "";
                    this.Body = 201;

                    SetStr(476, 505);
                    SetDex(76, 95);
                    SetInt(301, 325);

                    SetHits(286, 303);

                    SetDamage(7, 14);

                    SetDamageType(ResistanceType.Physical, 100);

                    SetResistance(ResistanceType.Physical, 45, 60);
                    SetResistance(ResistanceType.Fire, 50, 60);
                    SetResistance(ResistanceType.Cold, 30, 40);
                    SetResistance(ResistanceType.Poison, 20, 30);
                    SetResistance(ResistanceType.Energy, 30, 40);

                    SetSkill(SkillName.EvalInt, 70.1, 80.0);
                    SetSkill(SkillName.Magery, 70.1, 80.0);
                    SetSkill(SkillName.MagicResist, 85.1, 95.0);
                    SetSkill(SkillName.Tactics, 70.1, 80.0);
                    SetSkill(SkillName.Wrestling, 60.1, 80.0);

                    Fame = 15000;
                    Karma = -15000;

					this.FightMode = FightMode.Closest;
					this.Kills = 10;

					caithsidhe = true;
				}
			}

      		  	if( m_Talked == false )
        	  	{
      		      		if ( m.InRange( this, 3 ) && m is PlayerMobile)
        			{
            				m_Talked = true;

            				if ( caithsidhe )
            				{
            					SayRandom( CaithSidheSay, this );
            				}
            				else
            				{
            					SayRandom( WitchSay, this );
            				}

           				this.Move( GetDirectionTo( m.Location ) );
             				SpamTimer t = new SpamTimer();
           				t.Start();
            			}
        	  	}
		}

        public static void UnEquip(Mobile m_from, Layer layer)
        {
            if (m_from.FindItemOnLayer(layer) != null)
            {
                Item item = m_from.FindItemOnLayer(layer);
                item.Delete();
            }
        }

		public override Poison PoisonImmune{ get{ return Poison.Regular; } }

		public override bool OnBeforeDeath()
		{
			if (caithsidhe)
			{
                PackItem(new Hides()); // put here whatever you want the caith sihe to have as loot!
			}

			return true;
		}

    	  	private class SpamTimer : Timer
		{
			public SpamTimer() : base( TimeSpan.FromSeconds( 12 ) )
	       		{
          			Priority = TimerPriority.OneSecond;
       			}

         		protected override void OnTick()
        		{
           			m_Talked = false;
        		}
      		}

		private static void SayRandom( string[] say, Mobile m )
	       	{
	       		m.Say( say[Utility.Random( say.Length )] );
		}

		public Witch( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)2 ); // version

			writer.Write( (bool) m_ShowHours );
			writer.Write( (int) m_WitchBeginHour );
			writer.Write( (int) m_WitchEndHour );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			switch ( version )
			{
				case 2:
				{
					m_WitchBeginHour = reader.ReadInt();
					m_WitchEndHour = reader.ReadInt();

					goto case 1;
				}
				case 1:
				{
					m_ShowHours = reader.ReadBool();

					break;
				}
			}
		}
	}
}