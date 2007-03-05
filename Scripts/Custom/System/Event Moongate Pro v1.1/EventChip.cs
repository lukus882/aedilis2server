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
	public class EventChip : Item
	{
		private Mobile m_Owner;
		private EventGate m_Moongate;
		
		private int SavedStr, SavedDex, SavedInt, SavedFame, SavedKarma, SavedKills;
		private ArrayList StoredSkills;
		
		[Constructable]
		public EventChip( Mobile from, EventGate moongate ) : base( 4626 )
		{
			m_Owner = from;
			m_Moongate = moongate;
			
			Movable = false;
			Name = m_Owner.Name + "'s Event Chip";
			Hue = 1152;
			
			SaveSkills( from, this );
			
			StoredSkills = new ArrayList();
			
			for( int i = 0; i < m_Owner.Skills.Length; ++i )
				StoredSkills.Add( ( int )m_Owner.Skills[i].Base );
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			if( m_Owner == from )
			{
				m_Owner.RawStr 	= 	this.SavedStr;
				m_Owner.RawDex 	= 	this.SavedDex;
				m_Owner.RawInt 	= 	this.SavedInt;
				m_Owner.Fame 	= 	this.SavedFame;
				m_Owner.Karma 	= 	this.SavedKarma;
				m_Owner.Kills 	= 	this.SavedKills;
				
				for( int i = 0; i < StoredSkills.Count; i++ )
					m_Owner.Skills[i].Base = ( int )StoredSkills[i];

				m_Owner.SendMessage( "You have been restored." );
				this.Delete();
			}
			else
			{
				from.SendMessage( "You are not the owner of this chip!" );
				m_Owner.SendMessage( "Someone is trying to use your chip! Please report this illegal activity to a GM immediately." );
			}
		}
		
		public void SaveSkills( Mobile from, EventChip chip )
		{
			chip.SavedStr 	= from.RawStr;
			chip.SavedDex 	= from.RawDex;
			chip.SavedInt 	= from.RawInt;
			chip.SavedFame 	= from.Fame;
			chip.SavedKarma	= from.Karma;
			chip.SavedKills	= from.Kills;
		}
		
		public void Convert( Mobile from )
		{
			from.SendMessage( "Made it to convert." );
			
			if( m_Owner == from )
			{
				m_Owner.RawStr 	= 	this.SavedStr;
				m_Owner.RawDex 	= 	this.SavedDex;
				m_Owner.RawInt 	= 	this.SavedInt;
				m_Owner.Fame 	= 	this.SavedFame;
				m_Owner.Karma 	= 	this.SavedKarma;
				m_Owner.Kills 	= 	this.SavedKills;
				
				for( int i = 0; i < StoredSkills.Count; i++ )
					m_Owner.Skills[i].Base = ( int )StoredSkills[i];
				
				m_Owner.SendMessage( "You have been restored." );
				this.Delete();
			}
			else
			{
				from.SendMessage( "You are not the owner of this chip!" );
				m_Owner.SendMessage( "Someone is trying to use your chip! Please report this illegal activity to a GM immediately." );
			}
		}
		
		public EventChip( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( ( int ) 0 );  // Version
			
			//Version 0
			writer.Write( m_Owner );
			writer.Write( ( int )SavedStr );
			writer.Write( ( int )SavedDex );
			writer.Write( ( int )SavedInt );
			writer.Write( ( int )SavedFame );
			writer.Write( ( int )SavedKarma );
			writer.Write( ( int )SavedKills );
			
			writer.Write( ( int )StoredSkills.Count );
			for( int i = 0; i < StoredSkills.Count; i++ )
				writer.Write( ( int )StoredSkills[i] );
			//Version 0
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			
			StoredSkills = new ArrayList();
			
			//Version 0
			m_Owner 	= 	reader.ReadMobile();
			SavedStr 	= 	reader.ReadInt();
			SavedDex 	= 	reader.ReadInt();
			SavedInt 	= 	reader.ReadInt();
			SavedFame 	= 	reader.ReadInt();
			SavedKarma 	= 	reader.ReadInt();
			SavedKills	= 	reader.ReadInt();
			
			int Count = reader.ReadInt();
			for( int i = 0; i < Count; i++ )
				StoredSkills.Add( reader.ReadInt() );
			//Version 0
		}
	}
}

