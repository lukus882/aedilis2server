/*
    _________________________________
 -=(_)_______________________________)=-
   /   .   .   . ____  . ___      _/
  /~ /    /   / /     / /   )2007 /
 (~ (____(___/ (____ / /___/     (
  \ ----------------------------- \
   \    lucidnagual@charter.net    \
    \_     ===================      \
     \   -Owner of "The Conjuring"-  \
      \_     ===================     ~\
       )  Lucid's EventMoongate Pro    )
      /~        Version v1.1         _/
    _/_______________________________/
 -=(_)_______________________________)=-

 */
using System;
using Server;
using System.IO;
using Server.Spells;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Gumps;
using System.Collections;
using System.Collections.Generic;


namespace Server.Items
{
	public enum TypeOfTeams
	{
		None = 0,
		FFA = 1,
		Duel = 2,
		ThreeTeam = 3,
		FourTeam = 4,
		Paintball = 5,
		CTF = 6
	}
	
	public class EventGate : Moongate
	{
		private AccessLevel m_AccessLevel = ( AccessLevel )2; //4 = Admin, 3 = Seer, 2 = Gm etc
		
		private List<Item> totalitems = new List<Item>();
		
		private EventBag m_EventBag;
		private EventBag[] m_ListBag;
		private static Mobile m_Owner;
		private TypeOfTeams m_Team;
		
		public int m_RawStr, m_RawDex, m_RawInt, m_Fame, m_Karma, m_Kills, m_Skills;
		
		public static bool m_PetsAllowed, m_ItemsAllowed, m_LocationOverride;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int SetStrengthTo { get{ return m_RawStr; } set{ m_RawStr = value; } }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int SetDexterityTo { get{ return m_RawDex; } set{ m_RawDex = value; } }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int SetIntelligenceTo { get{ return m_RawInt; } set{ m_RawInt = value; } }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int SetFameTo { get{ return m_Fame; } set{ m_Fame = value; } }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int SetKarmaTo { get{ return m_Karma; } set{ m_Karma = value; } }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int SetKillsTo { get{ return m_Kills; } set{ m_Kills = value; } }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int SetSkillsTo { get{ return m_Skills; } set{ m_Skills = value; } }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public bool PetsAllowed { get{ return m_PetsAllowed; } set{ m_PetsAllowed = value; } }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public bool ItemsAllowed { get{ return m_ItemsAllowed; } set{ m_ItemsAllowed = value; } }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public bool UseLocationOverride { get{ return m_LocationOverride; } set{ m_LocationOverride = value; } }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public TypeOfTeams AssignTeam { get{ return m_Team; } set{ m_Team = value; } }
		
		[Constructable]
		public EventGate() : base()
		{
			Name = "Event Gate";
			Movable = false;
			Hue = 1153;
			Light = LightType.Circle300;
			m_Team = 0;
			m_Fame = m_Karma = m_Kills = 0;
			m_RawStr = m_RawDex = m_RawInt = 100;
		}
		
		public override void CheckGate( Mobile from, int range )
		{
			base.CheckGate( from, range );
			
			if ( UseLocationOverride )
				LocationOverride( from );
		}
		
		public override void OnGateUsed( Mobile from )
		{
			Container pack = from.Backpack;
			BankBox bank = from.BankBox;
			m_Owner = from;
			
			from.Warmode = false;
			from.Hidden = false;
			
			if ( from.Spell != null )
				( ( Spell )from.Spell ).Disturb( DisturbType.Unspecified );
			
			Targeting.Target.Cancel( from );
			
			bank.AddItem( new EventChip( from, this ) );
			from.SendMessage( "Your data has been scanned and saved." );
			
			if ( !ItemsAllowed )
			{
				if ( pack != null && bank != null )
				{
					SetSkills( from, this );
					
					List<Item> BankBags = new List<Item> ();
					List<Item> BankContents = new List<Item> ();
					
					EquippedItems( from ); 	//Checks for items that might be equipped and drops them in seperate bag.
					
					//Add pre-existing event bag items to a list. Add those items to the bankbox and delete the old bags.
					CycleForBags( from );
					
					from.SendMessage( "Your belongings have been deposited in your bankbox." );
					
					EventRules( from );
					
					from.MoveToWorld( Target, TargetMap );
				}
				else
				{
					from.SendMessage( "You are missing a backpack or bankbox and need to report this issue to staff immediately." );
				}
			}
			else
			{
				EventRules( from );
				
				from.MoveToWorld( Target, TargetMap );
			}
		}
		
		public override void UseGate( Mobile from )
		{
			if ( !PetsAllowed && from.Mounted )
			{
				from.SendMessage( "You may NOT enter while mounted." );
				//Send gump that offers to shrink [and bank] or stable the pet.
			}
			else if ( Factions.Sigil.ExistsOn( from ) )
			{
				from.SendLocalizedMessage( 1061632 ); // You can't do that while carrying the sigil.
			}
			else if ( from is BaseCreature || from is BaseVendor )
			{
				return;
			}
			else if ( !PetsAllowed && from.Followers != 0 )
			{
				from.SendMessage( "Pets are not allowed!" );
			}
			else if ( from.Criminal )
			{
				from.SendLocalizedMessage( 1005561, "", 0x22 ); // Thou'rt a criminal and cannot escape so easily.
			}
			else if ( Server.Spells.SpellHelper.CheckCombat( from ) )
			{
				from.SendLocalizedMessage( 1005564, "", 0x22 ); // Wouldst thou flee during the heat of battle??
			}
			else if ( from.Spell != null )
			{
				from.SendLocalizedMessage( 1049616 ); // You are too busy to do that at the moment.
			}
			else
				OnGateUsed( from );
		}
		
		public void LocationOverride( Mobile from )
		{
			Target = new Point3D( 5377, 1082, 1 );
			TargetMap = Map.Felucca;
		}
		
		public void EquippedItems( Mobile from )
		{
			List<Item> equipitems = new List<Item> ( from.Items );
			List<Item> wornitems = new List<Item>();
			
			EventBag stuffWorn = new EventBag();
			stuffWorn.Name = "Equipped Items of " + from.Name;
			
			foreach ( Item item in equipitems )
			{
				if ( ( item.Layer != Layer.Bank ) && ( item.Layer != Layer.Backpack ) && ( item.Layer != Layer.Hair ) && ( item.Layer != Layer.FacialHair ) )
				{
					if ( Items != null )
						wornitems.Add( item );
				}
			}
			
			if ( wornitems.Count > 0 )
			{
				for ( int i = 0; i < wornitems.Count; i++ )
				{
					Item item = wornitems[i];
					stuffWorn.DropItem( item );
				}
				
				from.Backpack.DropItem( stuffWorn );
			}
		}
		
		public void CycleForBags( Mobile from ) //Mal's Version
		{
			Container pack = from.Backpack;
			BankBox bank = from.BankBox;
			
			if ( pack != null && bank != null )
			{
				Item[] bags = bank.FindItemsByType( typeof( EventBag ) );
				List<Item> baglist = new List<Item>();
				List<Item> packlist = new List<Item> ();
				
				if ( bags.Length != 0 )
				{
					for ( int i = 0; i < bags.Length; i++ )
						baglist.AddRange( ( ( Container )bags[i] ).FindItemsByType( typeof( Item ) ) );
					
					foreach ( Item item in pack.Items )
					{
						if ( item.Movable )	//Non movable pack items must remain.
						{
							packlist.Add( item );
						}
					}
					
					if( baglist.Count != 0 )
					{
						EventBag newbag = new EventBag();
						
						foreach ( Item item in baglist )
							newbag.DropItem( item );
						
						foreach ( Item item in packlist )
							newbag.DropItem( item );
						
						bank.DropItem( newbag );
					}
					
					for( int i = 0; i < bags.Length; i++ )
					{
						Item item = bags[i];
						
						item.Delete();
					}
				}
				else
					PackBag( from );
			}
		}
		
		public void PackBag( Mobile from )
		{
			Container pack = from.Backpack;
			BankBox bank = from.BankBox;
			
			List<Item> PackOfItems = new List<Item> ();
			
			//Make list of items in player's backpack.
			foreach ( Item item in pack.Items )
			{
				if ( item.Movable )	//Non movable pack items must remain.
				{
					PackOfItems.Add( item );
				}
			}
			
			EventBag newbag = new EventBag();
			
			//Add backpack items to a new event bag.
			for ( int i = 0; i < PackOfItems.Count; i++ )
			{
				Item item = PackOfItems[i];
				
				newbag.DropItem( item );
			}
			
			//Drop event bag into player's bankbox.
			bank.DropItem( newbag );
		}
		
		public void EventRules( Mobile from )
		{
			from.SendMessage( "The event rules are: " );
			
			from.SendMessage( "NO [Spectator] KILLING!" );
			from.SendMessage( "NO SUMMONS!" );
			
			if ( !ItemsAllowed )
				from.SendMessage( "NO FORGEIGN ITEMS!" );
			
			if ( !PetsAllowed )
				from.SendMessage( "NO PETS!" );
		}
		
		public void SetSkills( Mobile from, EventGate gate )
		{
			if ( m_Owner == from )
			{
				gate.m_RawStr 	= 	from.RawStr;
				gate.m_RawDex 	= 	from.RawDex;
				gate.m_RawInt 	= 	from.RawInt;
				gate.m_Fame 	= 	from.Fame;
				gate.m_Karma 	= 	from.Karma;
				gate.m_Kills 	= 	from.Kills;
				
				for( int i = 0; i < m_Owner.Skills.Length; i++ )
				{
					m_Owner.Skills[i].Base = ( int )gate.m_Skills;
				}
			}
			else
			{
				from.SendMessage( "You are missing a backpack or bankbox and need to report this issue to staff immediately." );
				return;
			}
		}
		
		public EventGate( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( ( int ) 0 ); // version
			
			//Version 0
			
			writer.Write( m_Owner );
			writer.Write( ( int )m_Skills );
			writer.Write( ( int )m_RawStr );
			writer.Write( ( int )m_RawDex );
			writer.Write( ( int )m_RawInt );
			writer.Write( ( int )m_Fame );
			writer.Write( ( int )m_Karma );
			writer.Write( ( int )m_Kills );
			writer.Write( ( bool )m_PetsAllowed );
			writer.Write( ( bool )m_ItemsAllowed );
			writer.Write( ( bool )m_LocationOverride );
			//Version 0
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			
			switch ( version )
			{
				case 0:
					{
						m_Owner = reader.ReadMobile();
						m_Skills = reader.ReadInt();
						m_RawStr = reader.ReadInt();
						m_RawDex = reader.ReadInt();
						m_RawInt = reader.ReadInt();
						m_Fame = reader.ReadInt();
						m_Karma = reader.ReadInt();
						m_Kills = reader.ReadInt();
						m_PetsAllowed = reader.ReadBool();
						m_ItemsAllowed = reader.ReadBool();
						m_LocationOverride = reader.ReadBool();
						
						break;
					}
			}
		}
	}
}

