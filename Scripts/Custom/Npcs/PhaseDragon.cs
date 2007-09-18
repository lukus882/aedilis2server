//Made by Alex21 shard owner of Forestia : A New Beginning
using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a dragon corpse" )]
	public class PhaseDragon : BaseCreature
	{
		[Constructable]
		public PhaseDragon () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a phase dragon";
            		Body = 106;
            		Hue = 0;
			BaseSoundID = 362;

			SetStr( 1000 );
			SetDex( 150 );
			SetInt( 600 );

			SetHits( 1200 );

			SetDamage( 20 );

			SetDamageType( ResistanceType.Physical, 80 );
            		SetDamageType(ResistanceType.Poison, 10);
            		SetDamageType(ResistanceType.Fire, 10);

			SetResistance( ResistanceType.Physical, 60 );
			SetResistance( ResistanceType.Fire, 70 );
			SetResistance( ResistanceType.Cold, 20 );
			SetResistance( ResistanceType.Poison, 40 );
			SetResistance( ResistanceType.Energy, 40 );

			
			SetSkill( SkillName.Tactics, 120 );
			SetSkill( SkillName.Wrestling, 120 );
            		SetSkill(SkillName.Anatomy, 120);
            		SetSkill(SkillName.Poisoning, 100);
            		SetSkill(SkillName.Hiding, 100);
            		SetSkill(SkillName.Stealth, 100);
            		SetSkill(SkillName.Healing, 100);

			Fame = 2000;
			Karma = -2000;

			VirtualArmor = 60;


            AddItem(new Bandage(60));
            

		}

        public override void OnDamage(int amount, Mobile from, bool willKill)
        {
            if (this.Hits <= 299)
            {
                if (from is BaseCreature)
                {
                    BaseCreature pet = (BaseCreature)from;
                    if (pet.ControlMaster != null)
                    {
                        from = pet.ControlMaster;
                        this.FocusMob = pet.ControlMaster;
                    }
                }

                this.Hidden = true;

                switch (Utility.Random(8))
                {
                    case 0:

                        this.X = from.X;
                        this.Y = from.Y;
                        this.Hits = this.Hits + Utility.RandomMinMax(20, 27);

                        break;
                    case 1:

                        this.X = from.X;
                        this.Y = from.Y;
                        this.Hits = this.Hits + Utility.RandomMinMax(50, 90);

                        break;
                    case 2:

                        this.X = from.X;
                        this.Y = from.Y;
                        this.Hits = this.Hits + Utility.RandomMinMax(50, 90);

                        break;
                    case 3:

                        this.X = from.X;
                        this.Y = from.Y;
                        this.Hits = this.Hits + Utility.RandomMinMax(4, 12);

                        break;
                    case 4:

                        this.X = from.X;
                        this.Y = from.Y;
                        this.Hits = this.Hits + Utility.RandomMinMax(5, 10);

                        break;
                    case 5:

                        this.X = from.X;
                        this.Y = from.Y;
                        this.Hits = this.Hits + Utility.RandomMinMax(18, 28);

                        break;
                    case 6:

                        this.X = from.X;
                        this.Y = from.Y;
                        this.Hits = this.Hits + Utility.RandomMinMax(2, 3);

                        break;
                    case 7:

                        this.X = from.X;
                        this.Y = from.Y;
                        this.Hits = this.Hits + Utility.RandomMinMax(10, 20);

                        break;
                }

            }
            else if (this.Hits >= 300)
            {
                this.CantWalk = false;
                this.Hidden = false;
            }
        }

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 8 );
            AddLoot(LootPack.Rich, 6);
			AddLoot( LootPack.Gems, 12 );
		}

        private Item wand;

        public override void OnDeath(Container c)
		{
			base.OnDeath(c);

			if (0 == Utility.Random(20)) // or better yet is: if ( Utility.RandomDouble() < 0.05) -- this is for 5% or 1 out of 20 chance
			{
				switch (Utility.Random(11))
				{
					case 0: wand = new ClumsyWand(); break;
					case 1: wand = new FeebleWand(); break;
					case 2: wand = new FireballWand(); break;
					case 3: wand = new GreaterHealWand(); break;
					case 4: wand = new HarmWand(); break;
					case 5: wand = new HealWand(); break;
					case 6: wand = new IDWand(); break;
					case 7: wand = new LightningWand(); break;
					case 8: wand = new MagicArrowWand(); break;
					case 9: wand = new ManaDrainWand(); break;
					case 10: wand = new WeaknessWand(); break;
				}
				c.AddItem(wand);
			}
		}


		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } } // fire breath enabled
		public override bool AutoDispel{ get{ return !Controlled; } }
		//public override int TreasureMapLevel{ get{ return 4; } }
		public override int Meat{ get{ return 40; } }
		public override int Hides{ get{ return 160; } }
		public override HideType HideType{ get{ return HideType.Barbed; } }
		public override int Scales{ get{ return 18; } }
		public override ScaleType ScaleType{ get{ return ( Body == 12 ? ScaleType.Yellow : ScaleType.Red ); } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		

		public PhaseDragon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}
