//
// Story Teller v0.5
// jm (aka x-ray aka ¢¥¤ìŒ›˜) 
// jm99[at]mail333.com
//

using System; 
using System.IO;
using System.Text;
using System.Collections; 
using Server; 
using Server.Items; 
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "story teller corpse" )]
	public class StoryTeller : BaseCreature
	{
		public bool active;

		private string path;
		[CommandProperty( AccessLevel.GameMaster )]
		public string Path
		{
			get	{ return path; }
			set	{ path = value; }
		}
		private DateTime nextAbilityTime;

		private StreamReader text;

		private string curspeech;

		public override bool InitialInnocent{ get{ return true; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Active
		{
			get
			{
				return active;
			}
			set
			{
				if ( !value )
				{
					CloseStream();
				}

				active = value;
			}
		}

		[Constructable]
		public StoryTeller() : base( AIType.AI_Mage, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			InitBody();
			InitProps();
			InitOutfit();

			active = true;
		}

		public void InitProps()
		{
			SetHits( 13, 18 );

			SetDamage( 9, 15 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 80, 90 );
			SetResistance( ResistanceType.Fire, 40, 50 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.EvalInt, 90.1, 100.0 );
			SetSkill( SkillName.Magery, 90.1, 100.0 );
			SetSkill( SkillName.Meditation, 90.1, 100.0 );
			SetSkill( SkillName.MagicResist, 100.5, 150.0 );
			SetSkill( SkillName.Tactics, 10.1, 20.0 );
			SetSkill( SkillName.Wrestling, 10.1, 12.5 );

			Fame = 2000;
			Karma = 2000;

			VirtualArmor = 100;

			Tamable = false;
		}

		public void InitBody()
		{
			SetStr( 80, 100 );
			SetDex( 80, 100 );
			SetInt( 80, 100 );

			Hue = Utility.RandomSkinHue();

			if ( Female = Utility.RandomBool() )
			{
				Body = 401;
				Name = NameList.RandomName( "female" );
			}
			else
			{
				Body = 400;
				Name = NameList.RandomName( "male" );
			}
		}

		public void InitOutfit()
		{
			if ( Female )
				AddItem( new FancyDress() );
			else
				AddItem( new FancyShirt( GetRandomHue() ) );

			int lowHue = GetRandomHue();

			AddItem( new ShortPants( lowHue ) );

			if ( Female )
				AddItem( new ThighBoots( lowHue ) );
			else
				AddItem( new Boots( lowHue ) );

			if ( !Female )
				AddItem( new BodySash( lowHue ) );

			AddItem( new Cloak( GetRandomHue() ) );

			switch ( Utility.Random( 4 ) )
			{
				case 0: AddItem( new ShortHair( Utility.RandomHairHue() ) ); break;
				case 1: AddItem( new TwoPigTails( Utility.RandomHairHue() ) ); break;
				case 2: AddItem( new ReceedingHair( Utility.RandomHairHue() ) ); break;
				case 3: AddItem( new KrisnaHair( Utility.RandomHairHue() ) ); break;
			}

			PackGold( 200, 250 );
		}

		private static int GetRandomHue()
		{
			switch ( Utility.Random( 6 ) )
			{
				default:
				case 0: return 0;
				case 1: return Utility.RandomBlueHue();
				case 2: return Utility.RandomGreenHue();
				case 3: return Utility.RandomRedHue();
				case 4: return Utility.RandomYellowHue();
				case 5: return Utility.RandomNeutralHue();
			}
		}

		public override bool BardImmune{ get{ return true; } }

		public override void GenerateLoot()
		{
			AddLoot( LootPack.LowScrolls );
			AddLoot( LootPack.Gems, 2 );
		}

		public StoryTeller( Serial serial ) : base( serial )
		{
		}

		public void Emote()
		{
			switch(Utility.Random( 85 ))
			{
			case 1:
				PlaySound( Female ? 785 : 1056 );
				Say( "*cough!*" );					
				break;
			case 2:
				PlaySound( Female ? 818 : 1092 );
				Say( "*sniff*" );
				break;
			default:
				break;
			}
		}

		public void CloseStream()
		{
			if (text != null)
			{
				try { text.Close(); text = null; } catch {};
			}
		}

		public void Talk()
		{
			if (text == null) return;

			try
			{
				curspeech = text.ReadLine();

				if (curspeech == null) throw (new ArgumentNullException());

				Say( curspeech );					
			}
			catch
			{
				CloseStream();
			}
		}

		public override void OnThink()
		{
			if ( DateTime.Now >= nextAbilityTime && Combatant == null && active == true )
			{
				nextAbilityTime = DateTime.Now + TimeSpan.FromSeconds( Utility.RandomMinMax( 4, 6 ) );

				if (text == null)
				{
					try 
					{
						text = new StreamReader ( "Data/StoryTeller/" + path, System.Text.Encoding.Default, false );
					}
					catch {}
				}

				Talk();
				
				Emote();
			}
		}

		public override void OnDeath( Container c )
		{
			CloseStream();
			base.OnDeath( c );
		}

		public override void OnDelete()
		{
			CloseStream();
			base.OnDelete();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
			writer.Write( (bool) active );
			writer.Write( (string) path );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			active = reader.ReadBool();
			path = reader.ReadString();
		}
	}
}
