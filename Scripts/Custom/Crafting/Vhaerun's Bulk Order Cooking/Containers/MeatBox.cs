// Original Ingot Box Author Unknown
// Scripted by Karmageddon

// Re-scripted by Vhaerun for CRL Homestead 2.0

using System;
using System.Collections;
using Server;
using Server.Prompts;
using Server.Mobiles;
using Server.ContextMenus;
using Server.Gumps;
using Server.Items;
using Server.Network;
using Server.Targeting;
using Server.Multis;
using Server.Regions;
using Server.Engines.Craft;

namespace Server.Items
{	
	public class MeatBox : Item 
	{
		private int m_Bird;
		private int m_Lamb;
		private int m_Ribs;
		private int m_Fish;
		private int m_WithdrawIncrement;
		
		[CommandProperty(AccessLevel.GameMaster)]
		public int WithdrawIncrement { get { return m_WithdrawIncrement; } set { m_WithdrawIncrement = value; InvalidateProperties(); } }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int Bird{ get{ return m_Bird; } set{ m_Bird = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int Lamb{ get{ return m_Lamb; } set{ m_Lamb = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int Ribs{ get{ return m_Ribs; } set{ m_Ribs = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int Fish{ get{ return m_Fish; } set{ m_Fish = value; InvalidateProperties(); } }

		[Constructable]
		
		public MeatBox() : base( 0x2813 )
		{
			Movable = true;
			Weight = 10.0;
			Hue = 0x47E;
			Name = "Meat Box";
			WithdrawIncrement = 20;
		}
		
		[Constructable]
		public MeatBox( int withdrawincrement ) : base( 0x2813 )
		{
			Movable = true;
			Weight = 10.0;
			Hue = 0x47E;
			Name = "Meat Box";
			WithdrawIncrement = withdrawincrement;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 2 ) )
			from.LocalOverheadMessage( Network.MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
			else if ( from is PlayerMobile )
			from.SendGump( new MeatBoxGump( (PlayerMobile)from, this ) );
		}

		public void BeginCombine( Mobile from )
		{
			from.Target = new MeatBoxTarget( this );
		}

		public void EndCombine( Mobile from, object o )
		{
			if ( o is Item && ((Item)o).IsChildOf( from.Backpack ) )
			{
				if (!( o is RawBird || o is RawLambLeg || o is RawRibs || o is RawFishSteak ))
				{
					from.SendMessage( "That is not an item you can put in here." );
				}
				if ( o is RawBird )
				{

					if ( Bird >= 5000 )
					from.SendMessage( "There's not enough room for more." );
					else
					{
						Item curItem = o as Item;
						Bird += curItem.Amount;
						curItem.Delete();
						from.SendGump( new MeatBoxGump( (PlayerMobile)from, this ) );
						BeginCombine( from );
					}
				}
				if ( o is RawLambLeg )
				{

					if ( Lamb >= 5000 )
					from.SendMessage( "There's not enough room for more." );
					else
					{
						Item curItem = o as Item;
						Lamb += curItem.Amount;
						curItem.Delete();
						from.SendGump( new MeatBoxGump( (PlayerMobile)from, this ) );
						BeginCombine( from );
					}
				}
				if ( o is RawRibs )
				{

					if ( Ribs >= 5000 )
					from.SendMessage( "There's not enough room for more." );
					else
					{
						Item curItem = o as Item;
						Ribs += curItem.Amount;
						curItem.Delete();
						from.SendGump( new MeatBoxGump( (PlayerMobile)from, this ) );
						BeginCombine( from );
					}
				}
				if (o is RawFishSteak )
				{

					if ( Fish >= 5000 )
					from.SendMessage( "There's not enough room for more." );
					else
					{
						Item curItem = o as Item;
						Fish += curItem.Amount;
						curItem.Delete();
						from.SendGump( new MeatBoxGump( (PlayerMobile)from, this ) );
						BeginCombine( from );
					}
				}
			}
			else
			{
				from.SendLocalizedMessage( 1045158 ); // You must have the item in your backpack to target it.
			}
		}

		public MeatBox( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) m_Bird);
			writer.Write( (int) m_Lamb);
			writer.Write( (int) m_Ribs);
			writer.Write( (int) m_Fish);			
			writer.Write( (int) m_WithdrawIncrement);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			m_Bird = reader.ReadInt();
			m_Lamb = reader.ReadInt();
			m_Ribs = reader.ReadInt();
			m_Fish = reader.ReadInt();
			m_WithdrawIncrement = reader.ReadInt();
		}
	}
}


namespace Server.Items
{
	public class MeatBoxGump : Gump
	{
		private PlayerMobile m_From;
		private MeatBox m_Box;

		public MeatBoxGump( PlayerMobile from, MeatBox box ) : base( 25, 25 )
		{
			m_From = from;
			m_Box = box;

			m_From.CloseGump( typeof( MeatBoxGump ) );

			AddPage( 0 );

			AddBackground( 12, 19, 210, 230, 9250);
			AddLabel( 100, 30, 32, @"Meat Box");
			
			AddLabel( 60, 50, 32, @"Add Item");
			AddButton( 25, 50, 4005, 4007, 1, GumpButtonType.Reply, 0);

			AddLabel( 60, 75, 32, @"Close");
			AddButton( 25, 75, 4005, 4007, 0, GumpButtonType.Reply, 0);
			
			AddLabel( 60, 115, 0, @"Raw Bird");
			AddLabel( 200, 115, 0x480, box.Bird.ToString() );
			AddButton( 25, 115, 4005, 4007, 3, GumpButtonType.Reply, 0);
			
			AddLabel( 60, 135, 2223, @"Raw Lamb");
			AddLabel( 200, 135, 0x480, box.Lamb.ToString() );
			AddButton( 25, 135, 4005, 4007, 4, GumpButtonType.Reply, 0);
			
			AddLabel( 60, 155, 2128, @"Raw Ribs");
			AddLabel( 200, 155, 0x480, box.Ribs.ToString() );
			AddButton( 25, 155, 4005, 4007, 5, GumpButtonType.Reply, 0);
			
			AddLabel( 60, 175, 1644, @"Raw Fish Steak");
			AddLabel( 200, 175, 0x480, box.Fish.ToString() );
			AddButton( 25, 175, 4005, 4007, 6, GumpButtonType.Reply, 0);
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			if ( m_Box.Deleted )
			return;
			
			if ( info.ButtonID == 1)
			{
				m_From.SendGump( new MeatBoxGump( m_From, m_Box ) );
				m_Box.BeginCombine( m_From );
			}
			
			if ( info.ButtonID == 3 )
			{
				if ( m_Box.Bird > 0 )
				{
					if ( m_Box.Bird > m_Box.WithdrawIncrement )
					{
						m_From.AddToBackpack( new RawBird(m_Box.WithdrawIncrement) );
						m_Box.Bird = m_Box.Bird - m_Box.WithdrawIncrement;
						m_From.SendGump( new MeatBoxGump( m_From, m_Box ) );
					}
					else if (m_Box.Bird > 0)
                				{
						m_From.AddToBackpack( new RawBird(m_Box.Bird) );
						m_Box.Bird = 0;
						m_From.SendGump( new MeatBoxGump( m_From, m_Box ) );
					}
					else
					{
						m_From.SendMessage( "You do not have any of that meat!" );
						m_From.SendGump( new MeatBoxGump( m_From, m_Box ) );
						m_Box.BeginCombine( m_From );
					}
				}
			}
			
			if ( info.ButtonID == 4 )
			{
				if ( m_Box.Lamb > 0 )
				{
					if ( m_Box.Lamb > m_Box.WithdrawIncrement )
					{
						m_From.AddToBackpack( new RawLambLeg(m_Box.WithdrawIncrement) );
						m_Box.Lamb = m_Box.Lamb - m_Box.WithdrawIncrement;
						m_From.SendGump( new MeatBoxGump( m_From, m_Box ) );
					}
					else if (m_Box.Lamb> 0)
                				{
						m_From.AddToBackpack( new RawLambLeg(m_Box.Lamb) );
						m_Box.Lamb = 0;
						m_From.SendGump( new MeatBoxGump( m_From, m_Box ) );
					}
					else
					{
						m_From.SendMessage( "You do not have any of that meat!" );
						m_From.SendGump( new MeatBoxGump( m_From, m_Box ) );
						m_Box.BeginCombine( m_From );
					}
				}
			}
			if ( info.ButtonID == 5 )
			{
				if ( m_Box.Ribs > 0 )
				{
					if ( m_Box.Ribs > m_Box.WithdrawIncrement )
					{
						m_From.AddToBackpack( new RawRibs(m_Box.WithdrawIncrement) );
						m_Box.Ribs = m_Box.Ribs - m_Box.WithdrawIncrement;
						m_From.SendGump( new MeatBoxGump( m_From, m_Box ) );
					}
					else if (m_Box.Ribs> 0)
                				{
						m_From.AddToBackpack( new RawRibs(m_Box.Ribs) );
						m_Box.Ribs = 0;
						m_From.SendGump( new MeatBoxGump( m_From, m_Box ) );
					}
					else
					{
						m_From.SendMessage( "You do not have any of that meat!" );
						m_From.SendGump( new MeatBoxGump( m_From, m_Box ) );
						m_Box.BeginCombine( m_From );
					}
				}
			}
			if ( info.ButtonID == 6 )
			{
				if ( m_Box.Fish > 0 )
				{
					if ( m_Box.Fish > m_Box.WithdrawIncrement )
					{
						m_From.AddToBackpack( new RawFishSteak(m_Box.WithdrawIncrement) );
						m_Box.Fish = m_Box.Fish - m_Box.WithdrawIncrement;
						m_From.SendGump( new MeatBoxGump( m_From, m_Box ) );
					}
					else if (m_Box.Fish> 0)
                				{
						m_From.AddToBackpack( new RawFishSteak(m_Box.Fish) );
						m_Box.Fish = 0;
						m_From.SendGump( new MeatBoxGump( m_From, m_Box ) );
					}
					else
					{
						m_From.SendMessage( "You do not have any of that meat!" );
						m_From.SendGump( new MeatBoxGump( m_From, m_Box ) );
						m_Box.BeginCombine( m_From );
					}
				}
			}
		}
	}

}

namespace Server.Items
{
	public class MeatBoxTarget : Target
	{
		private MeatBox m_Box;

		public MeatBoxTarget( MeatBox box ) : base( 18, false, TargetFlags.None )
		{
			m_Box = box;
		}

		protected override void OnTarget( Mobile from, object targeted )
		{
			if ( m_Box.Deleted )
			return;

			m_Box.EndCombine( from, targeted );
		}
	}
}
