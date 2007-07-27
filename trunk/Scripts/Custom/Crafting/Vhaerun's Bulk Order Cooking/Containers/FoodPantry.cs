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
	public class FoodPantry : Item 
	{
		private int m_Apple;
		private int m_Grape;
		private int m_Peach;
		private int m_Pear;
		private int m_WithdrawIncrement;
		
		[CommandProperty(AccessLevel.GameMaster)]
		public int WithdrawIncrement { get { return m_WithdrawIncrement; } set { m_WithdrawIncrement = value; InvalidateProperties(); } }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int Apple{ get{ return m_Apple; } set{ m_Apple = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int Grape{ get{ return m_Grape; } set{ m_Grape = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int Peach{ get{ return m_Peach; } set{ m_Peach = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int Pear{ get{ return m_Pear; } set{ m_Pear = value; InvalidateProperties(); } }

		[Constructable]
		
		public FoodPantry() : base( 0xA4F )
		{
			Movable = true;
			Weight = 10.0;
			Name = "Food Pantry";
			WithdrawIncrement = 20;
		}
		
		[Constructable]
		public FoodPantry( int withdrawincrement ) : base( 0xA4F )
		{
			Movable = true;
			Weight = 10.0;
			Name = "Food Pantry";
			WithdrawIncrement = withdrawincrement;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 2 ) )
			from.LocalOverheadMessage( Network.MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
			else if ( from is PlayerMobile )
			from.SendGump( new FoodPantryGump( (PlayerMobile)from, this ) );
		}

		public void BeginCombine( Mobile from )
		{
			from.Target = new FoodPantryTarget( this );
		}

		public void EndCombine( Mobile from, object o )
		{
			if ( o is Item && ((Item)o).IsChildOf( from.Backpack ) )
			{
				if ( o is Apple )
				{
					if ( Apple >= 1000 )
					from.SendMessage( "There's not enough room for more." );
					else
					{
						Item curItem = o as Item;
						Apple += curItem.Amount;
						curItem.Delete();
						from.SendGump( new FoodPantryGump( (PlayerMobile)from, this ) );
						BeginCombine( from );
					}
				}
				else if ( o is Pear )
				{

					if ( Pear >= 1000 )
					from.SendMessage( "There's not enough room for more." );
					else
					{
						Item curItem = o as Item;
						Pear += curItem.Amount;
						curItem.Delete();
						from.SendGump( new FoodPantryGump( (PlayerMobile)from, this ) );
						BeginCombine( from );
					}
				}
				else if ( o is Peach )
				{

					if ( Peach >= 1000 )
					from.SendMessage( "There's not enough room for more." );
					else
					{
						Item curItem = o as Item;
						Peach += curItem.Amount;
						curItem.Delete();
						from.SendGump( new FoodPantryGump( (PlayerMobile)from, this ) );
						BeginCombine( from );
					}
				}
				else if (o is Grapes )
				{

					if ( Grape >= 1000 )
					from.SendMessage( "There's not enough room for more." );
					else
					{
						Item curItem = o as Item;
						Grape += curItem.Amount;
						curItem.Delete();
						from.SendGump( new FoodPantryGump( (PlayerMobile)from, this ) );
						BeginCombine( from );
					}
				}
				else
				{
					from.SendMessage( "That is not an item you can put in here." );
				}
			}
			else
			{
				from.SendLocalizedMessage( 1045158 ); // You must have the item in your backpack to target it.
			}
		}

		public FoodPantry( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) m_Apple);
			writer.Write( (int) m_Grape);
			writer.Write( (int) m_Peach);
			writer.Write( (int) m_Pear);			
			writer.Write( (int) m_WithdrawIncrement);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			m_Apple = reader.ReadInt();
			m_Grape = reader.ReadInt();
			m_Peach = reader.ReadInt();
			m_Pear = reader.ReadInt();
			m_WithdrawIncrement = reader.ReadInt();
		}
	}
}


namespace Server.Items
{
	public class FoodPantryGump : Gump
	{
		private PlayerMobile m_From;
		private FoodPantry m_Box;

		public FoodPantryGump( PlayerMobile from, FoodPantry box ) : base( 25, 25 )
		{
			m_From = from;
			m_Box = box;

			m_From.CloseGump( typeof( FoodPantryGump ) );

			AddPage( 0 );

			AddBackground( 12, 19, 210, 230, 9250);
			AddLabel( 100, 30, 13, @"Food Pantry");
			
			AddLabel( 60, 50, 32, @"Add Item");
			AddButton( 25, 50, 4005, 4007, 1, GumpButtonType.Reply, 0);

			AddLabel( 60, 75, 32, @"Close");
			AddButton( 25, 75, 4005, 4007, 0, GumpButtonType.Reply, 0);
			
			AddLabel( 60, 115, 53, @"Apples");
			AddLabel( 200, 115, 0x480, box.Apple.ToString() );
			AddButton( 25, 115, 4005, 4007, 3, GumpButtonType.Reply, 0);
			
			AddLabel( 60, 135, 53, @"Grapes");
			AddLabel( 200, 135, 0x480, box.Grape.ToString() );
			AddButton( 25, 135, 4005, 4007, 4, GumpButtonType.Reply, 0);
			
			AddLabel( 60, 155, 53, @"Peaches");
			AddLabel( 200, 155, 0x480, box.Peach.ToString() );
			AddButton( 25, 155, 4005, 4007, 5, GumpButtonType.Reply, 0);
			
			AddLabel( 60, 175, 53, @"Pears");
			AddLabel( 200, 175, 0x480, box.Pear.ToString() );
			AddButton( 25, 175, 4005, 4007, 6, GumpButtonType.Reply, 0);
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			if ( m_Box.Deleted )
			return;
			
			if ( info.ButtonID == 1)
			{
				m_From.SendGump( new FoodPantryGump( m_From, m_Box ) );
				m_Box.BeginCombine( m_From );
			}
			
			if ( info.ButtonID == 3 )
			{
				if ( m_Box.Apple > 0 )
				{
					if ( m_Box.Apple > m_Box.WithdrawIncrement )
					{
						m_From.AddToBackpack( new Apple(m_Box.WithdrawIncrement) );
						m_Box.Apple = m_Box.Apple - m_Box.WithdrawIncrement;
						m_From.SendGump( new FoodPantryGump( m_From, m_Box ) );
					}
					else if (m_Box.Apple > 0)
                				{
						m_From.AddToBackpack( new Apple(m_Box.Apple) );
						m_Box.Apple = 0;
						m_From.SendGump( new FoodPantryGump( m_From, m_Box ) );
					}
					else
					{
						m_From.SendMessage( "You do not have any of that!" );
						m_From.SendGump( new FoodPantryGump( m_From, m_Box ) );
						m_Box.BeginCombine( m_From );
					}
				}
			}
			
			if ( info.ButtonID == 4 )
			{
				if ( m_Box.Grape > 0 )
				{
					if ( m_Box.Grape > m_Box.WithdrawIncrement )
					{
						m_From.AddToBackpack( new Grapes(m_Box.WithdrawIncrement) );
						m_Box.Grape = m_Box.Grape - m_Box.WithdrawIncrement;
						m_From.SendGump( new FoodPantryGump( m_From, m_Box ) );
					}
					else if (m_Box.Grape> 0)
                				{
						m_From.AddToBackpack( new Grapes(m_Box.Grape) );
						m_Box.Grape = 0;
						m_From.SendGump( new FoodPantryGump( m_From, m_Box ) );
					}
					else
					{
						m_From.SendMessage( "You do not have any of that!" );
						m_From.SendGump( new FoodPantryGump( m_From, m_Box ) );
						m_Box.BeginCombine( m_From );
					}
				}
			}
			if ( info.ButtonID == 5 )
			{
				if ( m_Box.Peach > 0 )
				{
					if ( m_Box.Peach > m_Box.WithdrawIncrement )
					{
						m_From.AddToBackpack( new Peach(m_Box.WithdrawIncrement) );
						m_Box.Peach = m_Box.Peach - m_Box.WithdrawIncrement;
						m_From.SendGump( new FoodPantryGump( m_From, m_Box ) );
					}
					else if (m_Box.Peach> 0)
                				{
						m_From.AddToBackpack( new Peach(m_Box.Peach) );
						m_Box.Peach = 0;
						m_From.SendGump( new FoodPantryGump( m_From, m_Box ) );
					}
					else
					{
						m_From.SendMessage( "You do not have any of that!" );
						m_From.SendGump( new FoodPantryGump( m_From, m_Box ) );
						m_Box.BeginCombine( m_From );
					}
				}
			}
			if ( info.ButtonID == 6 )
			{
				if ( m_Box.Pear > 0 )
				{
					if ( m_Box.Pear > m_Box.WithdrawIncrement )
					{
						m_From.AddToBackpack( new Pear(m_Box.WithdrawIncrement) );
						m_Box.Pear = m_Box.Pear - m_Box.WithdrawIncrement;
						m_From.SendGump( new FoodPantryGump( m_From, m_Box ) );
					}
					else if (m_Box.Pear> 0)
                				{
						m_From.AddToBackpack( new Pear(m_Box.Pear) );
						m_Box.Pear = 0;
						m_From.SendGump( new FoodPantryGump( m_From, m_Box ) );
					}
					else
					{
						m_From.SendMessage( "You do not have any of that!" );
						m_From.SendGump( new FoodPantryGump( m_From, m_Box ) );
						m_Box.BeginCombine( m_From );
					}
				}
			}
		}
	}

}

namespace Server.Items
{
	public class FoodPantryTarget : Target
	{
		private FoodPantry m_Box;

		public FoodPantryTarget( FoodPantry box ) : base( 18, false, TargetFlags.None )
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
