// Created By Lucid Nagual - Admin of The Conjuring
// I'd like to thank all the wonderful people for sharing they're scripts & support.
// I hope by submitting this I can at least partially pay back the Runuo Community.
// Special thanks to _A_Li_N_ for his stasis chamber & chip. I combined his script with my EventGate.   

using System;
using Server;
using System.IO;
using System.Collections;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Gumps;


namespace Server.Items
{
	public class EventGate : Item
	{
		private AccessLevel m_AccessLevel = (AccessLevel)2;//4 = Admin, 3 = Seer, 2 = Gm etc

		private ArrayList m_Destinations = new ArrayList();
		private ArrayList m_DestX = new ArrayList();
		private ArrayList m_DestY = new ArrayList();
		private ArrayList m_DestZ = new ArrayList();
		private ArrayList m_DestMaps = new ArrayList();

		public bool RawStrMem, RawDexMem, RawIntMem, FameMem, KarmaMem, KillsMem;
        private Mobile m_Owner;

		[CommandProperty( AccessLevel.GameMaster )]
		public bool RawStr
		{
			get{ return RawStrMem; }
			set{ RawStrMem = value; }
		}
		[CommandProperty( AccessLevel.GameMaster )]
		public bool RawDex
		{
			get{ return RawDexMem; }
			set{ RawDexMem = value; }
		}
		[CommandProperty( AccessLevel.GameMaster )]
		public bool Int
		{
			get{ return RawIntMem; }
			set{ RawIntMem = value; }
		}
		[CommandProperty( AccessLevel.GameMaster )]
		public bool Fame
		{
			get{ return FameMem; }
			set{ FameMem = value; }
		}
		[CommandProperty( AccessLevel.GameMaster )]
		public bool Karma
		{
			get{ return KarmaMem; }
			set{ KarmaMem = value; }
		}
		[CommandProperty( AccessLevel.GameMaster )]
		public bool Kills
		{
			get{ return KillsMem; }
			set{ KillsMem = value; }
		}

		[Constructable]
		public EventGate() : base( 0xF6C )
		{
			Movable = false;
			Hue = 1153;
			Name = "Event Gate";
			Light = LightType.Circle300;
			RawStrMem = RawDexMem = RawIntMem = FameMem = KarmaMem = KillsMem = true;
		}

		public EventGate( Serial serial ) : base( serial )
		{
		}

		public override bool OnMoveOver( Mobile m)
		{
			if ( m != null && m.Player && m.Mounted && m.Followers != 0 || m is BaseCreature )
			{
				m.SendMessage("You may NOT enter while mounted."); // You may not enter while mounted.
				return false;
			}
			if ( m.Followers != 0 )
			{	
				return false;
			}
			else
			{
//--<Banks Items In Backpack>----------------------------------<Start>
				Backpack bag = new Backpack();
				Container pack = m.Backpack;
				BankBox box = m.BankBox;
				ArrayList equipitems = new ArrayList(m.Items);
				ArrayList bagitems = new ArrayList( pack.Items );
				foreach (Item item in equipitems)
				{
					if ((item.Layer != Layer.Bank) && (item.Layer != Layer.Backpack) && (item.Layer != Layer.Hair) && (item.Layer != Layer.FacialHair))
					{
						pack.DropItem( item );
		
					}
				}
				Container pouch = m.Backpack;
				ArrayList finalitems = new ArrayList( pouch.Items );
				foreach (Item items in finalitems)
				{
					bag.DropItem(items);
				}
				box.DropItem(bag);
//--<Banks Items In Backpack>----------------------------------<End>
//
//--<Saves Info on Chip>-------------------------------------<Start>
				BankBox bb = m.BankBox;
				if( bb != null )
				{
					m_Owner = m;
					bb.AddItem( new EventChip( m, this ) );
				}
			      AddItem( new EventChip( m, this ) );
				m.SendMessage( "Your skills & stats have been stored on chip" );
//--<Saves Info on Chip>---------------------------------------<End>
//
//--<Event Rules>--------------------------------------------<Start>
				m.SendMessage( "The Event Rules: NO KILLING, NO FOREIGN ITEMS, NO SUMMONS, NO MOUNTS!!!  Failure to obey to these rules will result in jail time!!" );
				m.SendMessage("We advise you to place all items in your bank before entering next time.");
				m.SendMessage("We hope you enjoy this custom event...all comments/suggestions are welcome!");
				m.X = 5381;
				m.Y = 1086;
				m.Z = 0;
//--<Event Rules>----------------------------------------------<End>
			return false;
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version

			writer.Write( (int)m_Destinations.Count );
			for ( int i = 0; i < m_Destinations.Count; ++i )
			{
				writer.Write( (string)m_Destinations[i] );
				writer.Write( (int)m_DestX[i] );
				writer.Write( (int)m_DestY[i] );
				writer.Write( (int)m_DestZ[i] );
				writer.Write( (int)m_DestMaps[i] );
			}
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					int count = reader.ReadInt();
					for ( int i = 0; i < count; ++i )
					{
						m_Destinations.Add( reader.ReadString() );
						m_DestX.Add( reader.ReadInt() );
						m_DestY.Add( reader.ReadInt() );
						m_DestZ.Add( reader.ReadInt() );
						m_DestMaps.Add( reader.ReadInt() );
					}
					break;
				}
			}
		}
	}
}

//--Event Chip-----------------------------------------------------------------------------

namespace Server.Items
{
	public class EventChip : Item
	{
		private Mobile m_Owner;
		private EventGate m_Chamber;

		private int RawStrMem, RawDexMem, RawIntMem, FameMem, KarmaMem, KillsMem;
		private int RawStr, RawDex, RawInt, Fame, Karma, Kills;
		private ArrayList StoredSkills;

		[Constructable]
		public EventChip( Mobile from, EventGate chamber ) : base( 4626 )
		{
			m_Owner = from;
			m_Chamber = chamber;

			Movable = false;
            Name = m_Owner.Name + "'s Event Chip";
			Hue = 1152;

			if( m_Chamber.RawStrMem )
			{
				RawStr = m_Owner.RawStr;
				//from.SendMessage( "Storing your Str at " + Str.ToString() );
				RawStrMem = 1;
			}
			if( m_Chamber.RawDexMem )
			{
				RawDex = m_Owner.RawDex;
				//from.SendMessage( "Storing your Dex at " + Dex.ToString() );
				RawDexMem = 1;
			}
			if( m_Chamber.RawIntMem )
			{
				RawInt = m_Owner.RawInt;
				//from.SendMessage( "Storing your Int at " + Int.ToString() );
				RawIntMem = 1;
			}
			if( m_Chamber.FameMem )
			{
				Fame = m_Owner.Fame;
				//from.SendMessage( "Storing your Fame at " + Fame.ToString() );
				FameMem = 1;
			}
			if( m_Chamber.KarmaMem )
			{
				Karma = m_Owner.Karma;
				//from.SendMessage( "Storing your Karma at " + Karma.ToString() );
				KarmaMem = 1;
			}
			if( m_Chamber.KillsMem )
			{
				Kills = m_Owner.Kills;
				//from.SendMessage( "Storing your Kills at " + Kills.ToString() );
				KillsMem = 1;
			}
			;

			StoredSkills = new ArrayList();
			for( int i = 0; i < PowerScroll.Skills.Length; ++i )
				StoredSkills.Add( (int)m_Owner.Skills[PowerScroll.Skills[i]].Base );
		}

		public EventChip( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 );  // Version

			writer.Write( m_Owner );

			writer.Write( (int)RawStrMem );
			writer.Write( (int)RawDexMem );
			writer.Write( (int)RawIntMem );
			writer.Write( (int)FameMem );
			writer.Write( (int)KarmaMem );
			writer.Write( (int)KillsMem );

			if( RawStrMem == 1 )
			{
				writer.Write( RawStr );
			}
			if( RawDexMem == 1 )
			{
				writer.Write( RawDex );
			}
			if( RawIntMem == 1 )
			{
				writer.Write( RawInt );
			}
			if( FameMem == 1 )
			{
				writer.Write( Fame );
			}
			if( KarmaMem == 1 )
			{
				writer.Write( Karma );
			}
			if( KillsMem == 1 )
			{
				writer.Write( Kills );
			}

			writer.Write( (int)StoredSkills.Count );
			for( int i = 0; i < StoredSkills.Count; i++ )
				writer.Write( (int)StoredSkills[i] );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			m_Owner = reader.ReadMobile();
			RawStrMem = reader.ReadInt();
			RawDexMem = reader.ReadInt();
			RawIntMem = reader.ReadInt();
			FameMem = reader.ReadInt();
			KarmaMem = reader.ReadInt();
			KillsMem = reader.ReadInt();

			if( RawStrMem == 1 )
			{
				RawStr = reader.ReadInt();
			}
			if( RawDexMem == 1 )
			{
				RawDex = reader.ReadInt();
			}
			if( RawIntMem == 1 )
			{
				RawInt = reader.ReadInt();
			}
			if( FameMem == 1 )
			{
				Fame = reader.ReadInt();
			}
			if( KarmaMem == 1 )
			{
				Karma = reader.ReadInt();
			}
			if( KillsMem == 1 )
			{
				Kills = reader.ReadInt();
			}

			int Count = reader.ReadInt();
			for( int i = 0; i < Count; i++ )
				StoredSkills.Add( reader.ReadInt() );
		}

		public override void OnDoubleClick( Mobile from )
		{
            if( m_Owner == from )
            {
				if( RawStrMem == 1 )
				{
					m_Owner.RawStr = RawStr;
					//m_Owner.SendMessage( "Restoring your Str to " + Str.ToString() );
				}
				if( RawDexMem == 1 )
				{
					m_Owner.RawDex = RawDex;
					//m_Owner.SendMessage( "Restoring your Dex to " + Dex.ToString() );
				}
				if( RawIntMem == 1 )
				{
					m_Owner.RawInt = RawInt;
					//m_Owner.SendMessage( "Restoring your Int to " + Int.ToString() );
				}
				if( FameMem == 1 )
				{
					m_Owner.Fame = Fame;
					//m_Owner.SendMessage( "Restoring your Fame to " + Fame.ToString() );
				}
				if( KarmaMem == 1 )
				{
					m_Owner.Karma = Karma;
					//m_Owner.SendMessage( "Restoring your Karma to " + Karma.ToString() );
				}
				if( KillsMem == 1 )
				{
					m_Owner.Kills = Kills;
					//m_Owner.SendMessage( "Restoring your Kills to " + Kills.ToString() );
				}
				
				for( int i = 0; i < StoredSkills.Count; i++ )
					m_Owner.Skills[PowerScroll.Skills[i]].Base = (int)StoredSkills[i];

				m_Owner.SendMessage( "You have been restored." );
				this.Delete();
			}
			else
			{
				from.SendMessage( "You are not the owner of this chip!" );
				m_Owner.SendMessage( "Someone is trying to use your chip! Please report this illegal activity to a GM immediately." );
			}
			
		}
	}
}


