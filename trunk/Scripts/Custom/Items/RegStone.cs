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
       )      Lucid's Mega Pack        )
      /~    The Mother Load v1.1     _/
    _/_______________________________/
 -=(_)_______________________________)=-

 */
using System;
using Server.Items;


namespace Server.Items
{
	public class RegStone2 : Item
	{
		private bool m_ChargeForRegs;
		private int m_PriceForRegs;
		private int m_AmountOfRegs;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public bool ChargeForRegs { get{ return m_ChargeForRegs; } set{ m_ChargeForRegs = value; } }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int PriceForRegs { get{ return m_PriceForRegs; } set{ m_PriceForRegs = value; } }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int AmountOfRegs { get{ return m_AmountOfRegs; } set{ m_AmountOfRegs = value; } }
		
		
		[Constructable]
		public RegStone2() : base( 0xED4 )
		{
			Name = "a reagent stone";
			Movable = false;
			Hue = 0x2D1;

			//Default settings.
			ChargeForRegs = true;
			PriceForRegs = 2050;
			AmountOfRegs = 50;
			//Default settings.
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			if ( ChargeForRegs )
			{
				if ( from.Backpack.ConsumeTotal( typeof( Gold ), PriceForRegs ) )
				{
					PackBag( from );
					from.SendMessage( "{0} gold has been removed from your backpack.", PriceForRegs );
				}
				else
				{
					from.SendMessage( "You do not have enough funds for that." );
				}
			}
			else
			{
				PackBag( from );
				from.SendMessage( "A bag of reagents have been placed into your backpack." );
			}
		}
		
		public void PackBag( Mobile from )
		{
			BagOfReagents bag = new BagOfReagents( AmountOfRegs );
			
			if ( !from.AddToBackpack( bag ) )
				bag.Delete();
		}
		
		public RegStone2( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( ( int ) 1 ); // version
			
			//Version 1
			writer.Write( ( bool )m_ChargeForRegs );
			writer.Write( ( int )m_PriceForRegs );
			writer.Write( ( int )m_AmountOfRegs );
			//Version 1
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			
			switch ( version )
			{
				case 1:
					{
						m_ChargeForRegs = reader.ReadBool();
						m_PriceForRegs = reader.ReadInt();
						m_AmountOfRegs = reader.ReadInt();
						goto case 0;
					}
				case 0:
					{
						break;
					}
			}
		}
	}
}
