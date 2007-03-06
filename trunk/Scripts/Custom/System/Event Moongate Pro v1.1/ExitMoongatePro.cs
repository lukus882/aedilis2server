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
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Gumps;
using System.Collections;
using System.Collections.Generic;


namespace Server.Items
{
	public class ExitMoongatePro : Moongate
	{
		private bool m_Gift;
		
		private List<Item> m_GiftList = new List<Item>();
		
		//Future modification will inclue giving a participation gift.
		//[CommandProperty( AccessLevel.GameMaster)]
		//public bool GiveGift { get{ return m_Gift; } set{ m_Gift = value; } }
		
		[Constructable]
		public ExitMoongatePro() : base()
		{
			Name = "Exit Event Here";
			Movable = false;
			Hue = 1153;
			Light = LightType.Circle300;
		}
		
		public override void UseGate( Mobile from )
		{
			IntelExit( from );
			
			base.UseGate( from );
		}
		
		public override void OnGateUsed( Mobile from )
		{
			PlayerMobile pm = from as PlayerMobile;
			Container bank = pm.BankBox;
			
			from.SolidHueOverride = -1;
			from.NameHue = -1;
			from.HueMod = -1;
			from.NameMod = null;
			
			if ( m_Gift )
				ParticipantGift( from );
			
			
			if ( bank != null )
			{
				CycleForChips( from );
			}
			else
			{
				from.SendMessage( "You are missing a backpack or bankbox and need to report this issue to staff immediately." );
			}
		}
		
		public void CycleForChips( Mobile from )
		{
			int ChipCount = from.BankBox.GetAmount( typeof( EventChip ) );
			
			if ( ChipCount > 0 )
			{
				EventChip foundchip = from.BankBox.FindItemByType( typeof( EventChip ), true ) as EventChip;
				
				if ( foundchip != null )
				{
					foundchip.Convert( from );
					from.SendMessage( "Your bankbox has been searched and an event chip has been found!");
				}
				
				foundchip.Delete();
				
				CycleForChips( from );
			}
			else
			{
				IntelExit( from );
			}
		}
		
		public void IntelExit( Mobile from )
		{
			if ( from.Kills > 4 ) //Send reds to Buc's Den in Felucca.
			{
				Target = new Point3D( 2723, 2191, 1 );
				TargetMap = Map.Felucca;
				from.PlaySound( 0x1FC );
			}
			else //Send blues to Brit Bank in Felucca.
			{
				Target = new Point3D( 1417, 1695, 1 );
				TargetMap = Map.Felucca;
				from.PlaySound( 0x1FC );
			}
		}
		
		public void ParticipantGift( Mobile from )
		{
			if ( m_GiftList.Count == 0 )
			{
				return;
			}
			else
			{
				Bag bag = new Bag();
				bag.Name = "Participation Reward";
				bag.Hue = 33;
				
				for( int i = 0; i < m_GiftList.Count; i++ )
				{
					Item item = m_GiftList[i];
					
					bag.DropItem( item );
				}
				
				from.Backpack.DropItem( bag );
			}
		}
		
		public override void OnDelete()
		{
			if ( m_GiftList.Capacity > 0 )
			{
				m_GiftList.Clear();
			}
		}
		
		public ExitMoongatePro( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			
			writer.Write( ( bool )m_Gift );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			
			m_Gift = reader.ReadBool();
		}
	}
}

