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
	public class HerbJars : Item 
	{
		private int m_Anise;
		private int m_Basil;
		private int m_BayLeaf;
		private int m_Chamomile;
		private int m_Caraway;
		private int m_Cilantro;
		private int m_Cinnamon;
		private int m_Clove;
		private int m_Copal;
		private int m_Coriander;
		private int m_Dill;
		private int m_Lavender;
		private int m_Marjoram;
		private int m_Mint;
		private int m_Mugwort;
		private int m_Mustard;
		private int m_Olive;
		private int m_Oregano;
		private int m_Peppercorn;
		private int m_RoseHerb;
		private int m_Rosemary;
		private int m_Saffron;
		private int m_Sage;
		private int m_Thyme;
		private int m_Valerian;
		private int m_WillowBark;
		private int m_WithdrawIncrement;
		
		[CommandProperty(AccessLevel.GameMaster)]
		public int WithdrawIncrement { get { return m_WithdrawIncrement; } set { m_WithdrawIncrement = value; InvalidateProperties(); } }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int Anise{ get{ return m_Anise; } set{ m_Anise = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int Basil{ get{ return m_Basil; } set{ m_Basil = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int BayLeaf{ get{ return m_BayLeaf; } set{ m_BayLeaf = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int Chamomile{ get{ return m_Chamomile; } set{ m_Chamomile = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int Caraway{ get{ return m_Caraway; } set{ m_Caraway = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int Cilantro{ get{ return m_Cilantro; } set{ m_Cilantro = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int Cinnamon{ get{ return m_Cinnamon; } set{ m_Cinnamon = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int Clove{ get{ return m_Clove; } set{ m_Clove = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int Copal{ get{ return m_Copal; } set{ m_Copal = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int Coriander{ get{ return m_Coriander; } set{ m_Coriander = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int Dill{ get{ return m_Dill; } set{ m_Dill = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int Lavender{ get{ return m_Lavender; } set{ m_Lavender = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int Marjoram{ get{ return m_Marjoram; } set{ m_Marjoram = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int Mint{ get{ return m_Mint; } set{ m_Mint = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int Mugwort{ get{ return m_Mugwort; } set{ m_Mugwort = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int Mustard{ get{ return m_Mustard; } set{ m_Mustard = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int Olive{ get{ return m_Olive; } set{ m_Olive = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int Oregano{ get{ return m_Oregano; } set{ m_Oregano = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int Peppercorn{ get{ return m_Peppercorn; } set{ m_Peppercorn = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int RoseHerb{ get{ return m_RoseHerb; } set{ m_RoseHerb = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int Rosemary{ get{ return m_Rosemary; } set{ m_Rosemary = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int Saffron{ get{ return m_Saffron; } set{ m_Saffron = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int Sage{ get{ return m_Sage; } set{ m_Sage = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int Thyme{ get{ return m_Thyme; } set{ m_Thyme = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int Valerian{ get{ return m_Valerian; } set{ m_Valerian = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int WillowBark{ get{ return m_WillowBark; } set{ m_WillowBark = value; InvalidateProperties(); } }

		[Constructable]
		
		public HerbJars() : base( 0xE4B )
		{
			Movable = true;
			Weight = 10.0;
			Name = "Food Pantry";
			Hue = 0x311;
			WithdrawIncrement = 5;
		}
		
		[Constructable]
		public HerbJars( int withdrawincrement ) : base( 0xE4B )
		{
			Movable = true;
			Weight = 10.0;
			Name = "Food Pantry";
			Hue = 0x311;
			WithdrawIncrement = withdrawincrement;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 2 ) )
			from.LocalOverheadMessage( Network.MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
			else if ( from is PlayerMobile )
			from.SendGump( new HerbJarsGump( (PlayerMobile)from, this ) );
		}

		public void BeginCombine( Mobile from )
		{
			from.Target = new HerbJarsTarget( this );
		}

		public void EndCombine( Mobile from, object o )
		{
			if ( o is Item && ((Item)o).IsChildOf( from.Backpack ) )
			{
				if ( o is Anise )
				{
					if ( Anise >= 100 )
					from.SendMessage( "There's not enough room for more." );
					else
					{
						Item curItem = o as Item;
						Anise += curItem.Amount;
						curItem.Delete();
						from.SendGump( new HerbJarsGump( (PlayerMobile)from, this ) );
						BeginCombine( from );
					}
				}
				else if ( o is Peppercorn )
				{

					if ( Peppercorn >= 100 )
					from.SendMessage( "There's not enough room for more." );
					else
					{
						Item curItem = o as Item;
						Peppercorn += curItem.Amount;
						curItem.Delete();
						from.SendGump( new HerbJarsGump( (PlayerMobile)from, this ) );
						BeginCombine( from );
					}
				}
				else if ( o is Oregano )
				{

					if ( Oregano >= 100 )
					from.SendMessage( "There's not enough room for more." );
					else
					{
						Item curItem = o as Item;
						Oregano += curItem.Amount;
						curItem.Delete();
						from.SendGump( new HerbJarsGump( (PlayerMobile)from, this ) );
						BeginCombine( from );
					}
				}
				else if (o is Copal )
				{

					if ( Copal >= 100 )
					from.SendMessage( "There's not enough room for more." );
					else
					{
						Item curItem = o as Item;
						Copal += curItem.Amount;
						curItem.Delete();
						from.SendGump( new HerbJarsGump( (PlayerMobile)from, this ) );
						BeginCombine( from );
					}
				}
				else if (o is Basil )
				{

					if ( Basil >= 100 )
					from.SendMessage( "There's not enough room for more." );
					else
					{
						Item curItem = o as Item;
						Basil += curItem.Amount;
						curItem.Delete();
						from.SendGump( new HerbJarsGump( (PlayerMobile)from, this ) );
						BeginCombine( from );
					}
				}
				else if (o is BayLeaf )
				{

					if ( BayLeaf >= 100 )
					from.SendMessage( "There's not enough room for more." );
					else
					{
						Item curItem = o as Item;
						BayLeaf += curItem.Amount;
						curItem.Delete();
						from.SendGump( new HerbJarsGump( (PlayerMobile)from, this ) );
						BeginCombine( from );
					}
				}
				else if (o is Chamomile )
				{

					if ( Chamomile >= 100 )
					from.SendMessage( "There's not enough room for more." );
					else
					{
						Item curItem = o as Item;
						Chamomile += curItem.Amount;
						curItem.Delete();
						from.SendGump( new HerbJarsGump( (PlayerMobile)from, this ) );
						BeginCombine( from );
					}
				}
				else if (o is Cilantro )
				{

					if ( Cilantro >= 100 )
					from.SendMessage( "There's not enough room for more." );
					else
					{
						Item curItem = o as Item;
						Cilantro += curItem.Amount;
						curItem.Delete();
						from.SendGump( new HerbJarsGump( (PlayerMobile)from, this ) );
						BeginCombine( from );
					}
				}
				else if (o is Cinnamon )
				{

					if ( Cinnamon >= 100 )
					from.SendMessage( "There's not enough room for more." );
					else
					{
						Item curItem = o as Item;
						Cinnamon += curItem.Amount;
						curItem.Delete();
						from.SendGump( new HerbJarsGump( (PlayerMobile)from, this ) );
						BeginCombine( from );
					}
				}
				else if (o is Clove )
				{

					if ( Clove >= 100 )
					from.SendMessage( "There's not enough room for more." );
					else
					{
						Item curItem = o as Item;
						Clove += curItem.Amount;
						curItem.Delete();
						from.SendGump( new HerbJarsGump( (PlayerMobile)from, this ) );
						BeginCombine( from );
					}
				}
				else if (o is Caraway )
				{

					if ( Caraway >= 100 )
					from.SendMessage( "There's not enough room for more." );
					else
					{
						Item curItem = o as Item;
						Caraway += curItem.Amount;
						curItem.Delete();
						from.SendGump( new HerbJarsGump( (PlayerMobile)from, this ) );
						BeginCombine( from );
					}
				}
				else if (o is Coriander )
				{

					if ( Coriander >= 100 )
					from.SendMessage( "There's not enough room for more." );
					else
					{
						Item curItem = o as Item;
						Coriander += curItem.Amount;
						curItem.Delete();
						from.SendGump( new HerbJarsGump( (PlayerMobile)from, this ) );
						BeginCombine( from );
					}
				}
				else if (o is Dill )
				{

					if ( Dill >= 100 )
					from.SendMessage( "There's not enough room for more." );
					else
					{
						Item curItem = o as Item;
						Dill += curItem.Amount;
						curItem.Delete();
						from.SendGump( new HerbJarsGump( (PlayerMobile)from, this ) );
						BeginCombine( from );
					}
				}
				else if (o is Mint )
				{

					if ( Mint >= 100 )
					from.SendMessage( "There's not enough room for more." );
					else
					{
						Item curItem = o as Item;
						Mint += curItem.Amount;
						curItem.Delete();
						from.SendGump( new HerbJarsGump( (PlayerMobile)from, this ) );
						BeginCombine( from );
					}
				}
				else if (o is Mugwort )
				{

					if ( Mugwort >= 100 )
					from.SendMessage( "There's not enough room for more." );
					else
					{
						Item curItem = o as Item;
						Mugwort += curItem.Amount;
						curItem.Delete();
						from.SendGump( new HerbJarsGump( (PlayerMobile)from, this ) );
						BeginCombine( from );
					}
				}
				else if (o is Lavender )
				{

					if ( Lavender >= 100 )
					from.SendMessage( "There's not enough room for more." );
					else
					{
						Item curItem = o as Item;
						Lavender += curItem.Amount;
						curItem.Delete();
						from.SendGump( new HerbJarsGump( (PlayerMobile)from, this ) );
						BeginCombine( from );
					}
				}
				else if (o is Marjoram )
				{

					if ( Marjoram >= 100 )
					from.SendMessage( "There's not enough room for more." );
					else
					{
						Item curItem = o as Item;
						Marjoram += curItem.Amount;
						curItem.Delete();
						from.SendGump( new HerbJarsGump( (PlayerMobile)from, this ) );
						BeginCombine( from );
					}
				}
				else if (o is Olive )
				{

					if ( Olive >= 100 )
					from.SendMessage( "There's not enough room for more." );
					else
					{
						Item curItem = o as Item;
						Olive += curItem.Amount;
						curItem.Delete();
						from.SendGump( new HerbJarsGump( (PlayerMobile)from, this ) );
						BeginCombine( from );
					}
				}
				else if (o is Mustard )
				{

					if ( Mustard >= 100 )
					from.SendMessage( "There's not enough room for more." );
					else
					{
						Item curItem = o as Item;
						Mustard += curItem.Amount;
						curItem.Delete();
						from.SendGump( new HerbJarsGump( (PlayerMobile)from, this ) );
						BeginCombine( from );
					}
				}
				else if (o is RoseHerb )
				{

					if ( RoseHerb >= 100 )
					from.SendMessage( "There's not enough room for more." );
					else
					{
						Item curItem = o as Item;
						RoseHerb += curItem.Amount;
						curItem.Delete();
						from.SendGump( new HerbJarsGump( (PlayerMobile)from, this ) );
						BeginCombine( from );
					}
				}
				else if (o is Rosemary )
				{

					if ( Rosemary >= 100 )
					from.SendMessage( "There's not enough room for more." );
					else
					{
						Item curItem = o as Item;
						Rosemary += curItem.Amount;
						curItem.Delete();
						from.SendGump( new HerbJarsGump( (PlayerMobile)from, this ) );
						BeginCombine( from );
					}
				}
				else if (o is Saffron )
				{

					if ( Saffron >= 100 )
					from.SendMessage( "There's not enough room for more." );
					else
					{
						Item curItem = o as Item;
						Saffron += curItem.Amount;
						curItem.Delete();
						from.SendGump( new HerbJarsGump( (PlayerMobile)from, this ) );
						BeginCombine( from );
					}
				}
				else if (o is Sage )
				{

					if ( Sage >= 100 )
					from.SendMessage( "There's not enough room for more." );
					else
					{
						Item curItem = o as Item;
						Sage += curItem.Amount;
						curItem.Delete();
						from.SendGump( new HerbJarsGump( (PlayerMobile)from, this ) );
						BeginCombine( from );
					}
				}
				else if (o is Thyme )
				{

					if ( Thyme >= 100 )
					from.SendMessage( "There's not enough room for more." );
					else
					{
						Item curItem = o as Item;
						Thyme += curItem.Amount;
						curItem.Delete();
						from.SendGump( new HerbJarsGump( (PlayerMobile)from, this ) );
						BeginCombine( from );
					}
				}
				else if (o is Valerian )
				{

					if ( Valerian >= 100 )
					from.SendMessage( "There's not enough room for more." );
					else
					{
						Item curItem = o as Item;
						Valerian += curItem.Amount;
						curItem.Delete();
						from.SendGump( new HerbJarsGump( (PlayerMobile)from, this ) );
						BeginCombine( from );
					}
				}
				else if (o is WillowBark )
				{

					if ( WillowBark >= 100 )
					from.SendMessage( "There's not enough room for more." );
					else
					{
						Item curItem = o as Item;
						WillowBark += curItem.Amount;
						curItem.Delete();
						from.SendGump( new HerbJarsGump( (PlayerMobile)from, this ) );
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

		public HerbJars( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) m_Anise);
			writer.Write( (int) m_Basil);
			writer.Write( (int) m_BayLeaf);
			writer.Write( (int) m_Chamomile);
			writer.Write( (int) m_Caraway);
			writer.Write( (int) m_Cilantro);
			writer.Write( (int) m_Cinnamon);
			writer.Write( (int) m_Clove);
			writer.Write( (int) m_Copal);
			writer.Write( (int) m_Coriander);
			writer.Write( (int) m_Dill);
			writer.Write( (int) m_Lavender);
			writer.Write( (int) m_Marjoram);
			writer.Write( (int) m_Mint);
			writer.Write( (int) m_Mugwort);
			writer.Write( (int) m_Mustard);
			writer.Write( (int) m_Olive);
			writer.Write( (int) m_Oregano);
			writer.Write( (int) m_Peppercorn);
			writer.Write( (int) m_RoseHerb);
			writer.Write( (int) m_Rosemary);
			writer.Write( (int) m_Saffron);
			writer.Write( (int) m_Sage);
			writer.Write( (int) m_Thyme);
			writer.Write( (int) m_Valerian);
			writer.Write( (int) m_WillowBark);			
			writer.Write( (int) m_WithdrawIncrement);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			m_Anise = reader.ReadInt();
			m_Basil = reader.ReadInt();
			m_BayLeaf = reader.ReadInt();
			m_Chamomile = reader.ReadInt();
			m_Caraway = reader.ReadInt();
			m_Cilantro = reader.ReadInt();
			m_Cinnamon = reader.ReadInt();
			m_Clove = reader.ReadInt();
			m_Copal = reader.ReadInt();
			m_Coriander = reader.ReadInt();
			m_Dill = reader.ReadInt();
			m_Lavender = reader.ReadInt();
			m_Marjoram = reader.ReadInt();
			m_Mint = reader.ReadInt();
			m_Mugwort = reader.ReadInt();
			m_Mustard = reader.ReadInt();
			m_Olive = reader.ReadInt();
			m_Oregano = reader.ReadInt();
			m_Peppercorn = reader.ReadInt();
			m_RoseHerb = reader.ReadInt();
			m_Rosemary = reader.ReadInt();
			m_Saffron = reader.ReadInt();
			m_Sage = reader.ReadInt();
			m_Thyme = reader.ReadInt();
			m_Valerian = reader.ReadInt();
			m_WillowBark = reader.ReadInt();
			m_WithdrawIncrement = reader.ReadInt();
		}
	}
}


namespace Server.Items
{
	public class HerbJarsGump : Gump
	{
		private PlayerMobile m_From;
		private HerbJars m_Box;

		public HerbJarsGump( PlayerMobile from, HerbJars box ) : base( 25, 25 )
		{
			m_From = from;
			m_Box = box;

			m_From.CloseGump( typeof( HerbJarsGump ) );

			AddPage( 0 );

			AddBackground( 12, 19, 425, 380, 9250);
			AddLabel( 100, 30, 173, @"Herb and Spice Jars");
			
			AddLabel( 60, 50, 32, @"Add Item");
			AddButton( 25, 50, 4005, 4007, 1, GumpButtonType.Reply, 0);

			AddLabel( 60, 75, 32, @"Close");
			AddButton( 25, 75, 4005, 4007, 0, GumpButtonType.Reply, 0);
			
			AddLabel( 60, 115, 53, @"Anise");
			AddLabel( 200, 115, 0x480, box.Anise.ToString() );
			AddButton( 25, 115, 4005, 4007, 3, GumpButtonType.Reply, 0);

			AddLabel( 60, 135, 53, @"Basil");
			AddLabel( 200, 135, 0x480, box.Basil.ToString() );
			AddButton( 25, 135, 4005, 4007, 4, GumpButtonType.Reply, 0);

			AddLabel( 60, 155, 53, @"Bay Leaves");
			AddLabel( 200, 155, 0x480, box.BayLeaf.ToString() );
			AddButton( 25, 155, 4005, 4007, 5, GumpButtonType.Reply, 0);

			AddLabel( 60, 175, 53, @"Chamomile");
			AddLabel( 200, 175, 0x480, box.Chamomile.ToString() );
			AddButton( 25, 175, 4005, 4007, 6, GumpButtonType.Reply, 0);

			AddLabel( 60, 195, 53, @"Caraway");
			AddLabel( 200, 195, 0x480, box.Caraway.ToString() );
			AddButton( 25, 195, 4005, 4007, 7, GumpButtonType.Reply, 0);

			AddLabel( 60, 215, 53, @"Cilantro");
			AddLabel( 200, 215, 0x480, box.Cilantro.ToString() );
			AddButton( 25, 215, 4005, 4007, 8, GumpButtonType.Reply, 0);

			AddLabel( 60, 235, 53, @"Cinnamon");
			AddLabel( 200, 235, 0x480, box.Cinnamon.ToString() );
			AddButton( 25, 235, 4005, 4007, 9, GumpButtonType.Reply, 0);

			AddLabel( 60, 255, 53, @"Clove");
			AddLabel( 200, 255, 0x480, box.Clove.ToString() );
			AddButton( 25, 255, 4005, 4007, 10, GumpButtonType.Reply, 0);

			AddLabel( 60, 275, 53, @"Copal");
			AddLabel( 200, 275, 0x480, box.Copal.ToString() );
			AddButton( 25, 275, 4005, 4007, 11, GumpButtonType.Reply, 0);

			AddLabel( 60, 295, 53, @"Coriander");
			AddLabel( 200, 295, 0x480, box.Coriander.ToString() );
			AddButton( 25, 295, 4005, 4007, 12, GumpButtonType.Reply, 0);

			AddLabel( 60, 315, 53, @"Dill");
			AddLabel( 200, 315, 0x480, box.Dill.ToString() );
			AddButton( 25, 315, 4005, 4007, 13, GumpButtonType.Reply, 0);

			AddLabel( 60, 335, 53, @"Lavender");
			AddLabel( 200, 335, 0x480, box.Lavender.ToString() );
			AddButton( 25, 335, 4005, 4007, 14, GumpButtonType.Reply, 0);

			AddLabel( 60, 355, 53, @"Marjoram");
			AddLabel( 200, 355, 0x480, box.Marjoram.ToString() );
			AddButton( 25, 355, 4005, 4007, 15, GumpButtonType.Reply, 0);

			AddLabel( 260, 115, 53, @"Mint");
			AddLabel( 400, 115, 0x480, box.Mint.ToString() );
			AddButton( 225, 115, 4005, 4007, 16, GumpButtonType.Reply, 0);

			AddLabel( 260, 135, 53, @"Mugwort");
			AddLabel( 400, 135, 0x480, box.Mugwort.ToString() );
			AddButton( 225, 135, 4005, 4007, 17, GumpButtonType.Reply, 0);

			AddLabel( 260, 155, 53, @"Mustard");
			AddLabel( 400, 155, 0x480, box.Mustard.ToString() );
			AddButton( 225, 155, 4005, 4007, 18, GumpButtonType.Reply, 0);

			AddLabel( 260, 175, 53, @"Olive");
			AddLabel( 400, 175, 0x480, box.Olive.ToString() );
			AddButton( 225, 175, 4005, 4007, 19, GumpButtonType.Reply, 0);

			AddLabel( 260, 195, 53, @"Oregano");
			AddLabel( 400, 195, 0x480, box.Oregano.ToString() );
			AddButton( 225, 195, 4005, 4007, 20, GumpButtonType.Reply, 0);
			
			AddLabel( 260, 215, 53, @"Peppercorn");
			AddLabel( 400, 215, 0x480, box.Peppercorn.ToString() );
			AddButton( 225, 215, 4005, 4007, 21, GumpButtonType.Reply, 0);

			AddLabel( 260, 235, 53, @"Rose");
			AddLabel( 400, 235, 0x480, box.RoseHerb.ToString() );
			AddButton( 225, 235, 4005, 4007, 22, GumpButtonType.Reply, 0);

			AddLabel( 260, 255, 53, @"Rosemary");
			AddLabel( 400, 255, 0x480, box.Rosemary.ToString() );
			AddButton( 225, 255, 4005, 4007, 23, GumpButtonType.Reply, 0);

			AddLabel( 260, 275, 53, @"Saffron");
			AddLabel( 400, 275, 0x480, box.Saffron.ToString() );
			AddButton( 225, 275, 4005, 4007, 24, GumpButtonType.Reply, 0);

			AddLabel( 260, 295, 53, @"Sage");
			AddLabel( 400, 295, 0x480, box.Sage.ToString() );
			AddButton( 225, 295, 4005, 4007, 25, GumpButtonType.Reply, 0);

			AddLabel( 260, 315, 53, @"Thyme");
			AddLabel( 400, 315, 0x480, box.Thyme.ToString() );
			AddButton( 225, 315, 4005, 4007, 26, GumpButtonType.Reply, 0);

			AddLabel( 260, 335, 53, @"Valerian");
			AddLabel( 400, 335, 0x480, box.Valerian.ToString() );
			AddButton( 225, 335, 4005, 4007, 27, GumpButtonType.Reply, 0);

			AddLabel( 260, 355, 53, @"Willow Bark");
			AddLabel( 400, 355, 0x480, box.WillowBark.ToString() );
			AddButton( 225, 355, 4005, 4007, 28, GumpButtonType.Reply, 0);

		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			if ( m_Box.Deleted )
			return;
			
			if ( info.ButtonID == 1)
			{
				m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
				m_Box.BeginCombine( m_From );
			}
			
			if ( info.ButtonID == 3 )
			{
				if ( m_Box.Anise > 0 )
				{
					if ( m_Box.Anise > m_Box.WithdrawIncrement )
					{
						m_From.AddToBackpack( new Anise(m_Box.WithdrawIncrement) );
						m_Box.Anise = m_Box.Anise - m_Box.WithdrawIncrement;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else if (m_Box.Anise > 0)
                				{
						m_From.AddToBackpack( new Anise(m_Box.Anise) );
						m_Box.Anise = 0;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else
					{
						m_From.SendMessage( "You do not have any of that!" );
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
						m_Box.BeginCombine( m_From );
					}
				}
			}
			if ( info.ButtonID == 4 )
			{
				if ( m_Box.Basil > 0 )
				{
					if ( m_Box.Basil > m_Box.WithdrawIncrement )
					{
						m_From.AddToBackpack( new Basil(m_Box.WithdrawIncrement) );
						m_Box.Basil = m_Box.Basil - m_Box.WithdrawIncrement;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else if (m_Box.Basil > 0)
                				{
						m_From.AddToBackpack( new Basil(m_Box.Basil) );
						m_Box.Basil = 0;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else
					{
						m_From.SendMessage( "You do not have any of that!" );
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
						m_Box.BeginCombine( m_From );
					}
				}
			}
			if ( info.ButtonID == 5 )
			{
				if ( m_Box.BayLeaf > 0 )
				{
					if ( m_Box.BayLeaf > m_Box.WithdrawIncrement )
					{
						m_From.AddToBackpack( new BayLeaf(m_Box.WithdrawIncrement) );
						m_Box.BayLeaf = m_Box.BayLeaf - m_Box.WithdrawIncrement;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else if (m_Box.BayLeaf > 0)
                				{
						m_From.AddToBackpack( new BayLeaf(m_Box.BayLeaf) );
						m_Box.BayLeaf = 0;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else
					{
						m_From.SendMessage( "You do not have any of that!" );
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
						m_Box.BeginCombine( m_From );
					}
				}
			}
			if ( info.ButtonID == 6 )
			{
				if ( m_Box.Chamomile > 0 )
				{
					if ( m_Box.Chamomile > m_Box.WithdrawIncrement )
					{
						m_From.AddToBackpack( new Chamomile(m_Box.WithdrawIncrement) );
						m_Box.Chamomile = m_Box.Chamomile - m_Box.WithdrawIncrement;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else if (m_Box.Chamomile > 0)
                				{
						m_From.AddToBackpack( new Chamomile(m_Box.Chamomile) );
						m_Box.Chamomile = 0;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else
					{
						m_From.SendMessage( "You do not have any of that!" );
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
						m_Box.BeginCombine( m_From );
					}
				}
			}
			if ( info.ButtonID == 7 )
			{
				if ( m_Box.Caraway > 0 )
				{
					if ( m_Box.Caraway > m_Box.WithdrawIncrement )
					{
						m_From.AddToBackpack( new Caraway(m_Box.WithdrawIncrement) );
						m_Box.Caraway = m_Box.Caraway - m_Box.WithdrawIncrement;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else if (m_Box.Caraway > 0)
                				{
						m_From.AddToBackpack( new Caraway(m_Box.Caraway) );
						m_Box.Caraway = 0;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else
					{
						m_From.SendMessage( "You do not have any of that!" );
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
						m_Box.BeginCombine( m_From );
					}
				}
			}
			if ( info.ButtonID == 8 )
			{
				if ( m_Box.Cilantro > 0 )
				{
					if ( m_Box.Cilantro > m_Box.WithdrawIncrement )
					{
						m_From.AddToBackpack( new Cilantro(m_Box.WithdrawIncrement) );
						m_Box.Cilantro = m_Box.Cilantro - m_Box.WithdrawIncrement;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else if (m_Box.Cilantro > 0)
                				{
						m_From.AddToBackpack( new Cilantro(m_Box.Cilantro) );
						m_Box.Cilantro = 0;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else
					{
						m_From.SendMessage( "You do not have any of that!" );
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
						m_Box.BeginCombine( m_From );
					}
				}
			}
			if ( info.ButtonID == 9 )
			{
				if ( m_Box.Cinnamon > 0 )
				{
					if ( m_Box.Cinnamon > m_Box.WithdrawIncrement )
					{
						m_From.AddToBackpack( new Cinnamon(m_Box.WithdrawIncrement) );
						m_Box.Cinnamon = m_Box.Cinnamon - m_Box.WithdrawIncrement;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else if (m_Box.Cinnamon > 0)
                				{
						m_From.AddToBackpack( new Cinnamon(m_Box.Cinnamon) );
						m_Box.Cinnamon = 0;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else
					{
						m_From.SendMessage( "You do not have any of that!" );
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
						m_Box.BeginCombine( m_From );
					}
				}
			}
			if ( info.ButtonID == 10 )
			{
				if ( m_Box.Clove > 0 )
				{
					if ( m_Box.Clove > m_Box.WithdrawIncrement )
					{
						m_From.AddToBackpack( new Clove(m_Box.WithdrawIncrement) );
						m_Box.Clove = m_Box.Clove - m_Box.WithdrawIncrement;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else if (m_Box.Clove > 0)
                				{
						m_From.AddToBackpack( new Clove(m_Box.Clove) );
						m_Box.Clove = 0;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else
					{
						m_From.SendMessage( "You do not have any of that!" );
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
						m_Box.BeginCombine( m_From );
					}
				}
			}
			if ( info.ButtonID == 11 )
			{
				if ( m_Box.Copal > 0 )
				{
					if ( m_Box.Copal > m_Box.WithdrawIncrement )
					{
						m_From.AddToBackpack( new Copal(m_Box.WithdrawIncrement) );
						m_Box.Copal = m_Box.Copal - m_Box.WithdrawIncrement;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else if (m_Box.Copal> 0)
                				{
						m_From.AddToBackpack( new Copal(m_Box.Copal) );
						m_Box.Copal = 0;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else
					{
						m_From.SendMessage( "You do not have any of that!" );
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
						m_Box.BeginCombine( m_From );
					}
				}
			}
			if ( info.ButtonID == 12 )
			{
				if ( m_Box.Coriander > 0 )
				{
					if ( m_Box.Coriander > m_Box.WithdrawIncrement )
					{
						m_From.AddToBackpack( new Coriander(m_Box.WithdrawIncrement) );
						m_Box.Coriander = m_Box.Coriander - m_Box.WithdrawIncrement;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else if (m_Box.Coriander > 0)
                				{
						m_From.AddToBackpack( new Coriander(m_Box.Coriander) );
						m_Box.Coriander = 0;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else
					{
						m_From.SendMessage( "You do not have any of that!" );
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
						m_Box.BeginCombine( m_From );
					}
				}
			}
			if ( info.ButtonID == 13 )
			{
				if ( m_Box.Dill > 0 )
				{
					if ( m_Box.Dill > m_Box.WithdrawIncrement )
					{
						m_From.AddToBackpack( new Dill(m_Box.WithdrawIncrement) );
						m_Box.Dill = m_Box.Dill - m_Box.WithdrawIncrement;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else if (m_Box.Dill > 0)
                				{
						m_From.AddToBackpack( new Dill(m_Box.Dill) );
						m_Box.Dill = 0;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else
					{
						m_From.SendMessage( "You do not have any of that!" );
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
						m_Box.BeginCombine( m_From );
					}
				}
			}
			if ( info.ButtonID == 14 )
			{
				if ( m_Box.Lavender > 0 )
				{
					if ( m_Box.Lavender > m_Box.WithdrawIncrement )
					{
						m_From.AddToBackpack( new Lavender(m_Box.WithdrawIncrement) );
						m_Box.Lavender = m_Box.Lavender - m_Box.WithdrawIncrement;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else if (m_Box.Lavender > 0)
                				{
						m_From.AddToBackpack( new Lavender(m_Box.Lavender) );
						m_Box.Lavender = 0;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else
					{
						m_From.SendMessage( "You do not have any of that!" );
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
						m_Box.BeginCombine( m_From );
					}
				}
			}
			if ( info.ButtonID == 15 )
			{
				if ( m_Box.Marjoram > 0 )
				{
					if ( m_Box.Marjoram > m_Box.WithdrawIncrement )
					{
						m_From.AddToBackpack( new Marjoram(m_Box.WithdrawIncrement) );
						m_Box.Marjoram = m_Box.Marjoram - m_Box.WithdrawIncrement;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else if (m_Box.Marjoram > 0)
                				{
						m_From.AddToBackpack( new Marjoram(m_Box.Marjoram) );
						m_Box.Marjoram = 0;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else
					{
						m_From.SendMessage( "You do not have any of that!" );
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
						m_Box.BeginCombine( m_From );
					}
				}
			}
			if ( info.ButtonID == 16 )
			{
				if ( m_Box.Mint > 0 )
				{
					if ( m_Box.Mint > m_Box.WithdrawIncrement )
					{
						m_From.AddToBackpack( new Mint(m_Box.WithdrawIncrement) );
						m_Box.Mint = m_Box.Mint - m_Box.WithdrawIncrement;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else if (m_Box.Mint > 0)
                				{
						m_From.AddToBackpack( new Mint(m_Box.Mint) );
						m_Box.Mint = 0;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else
					{
						m_From.SendMessage( "You do not have any of that!" );
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
						m_Box.BeginCombine( m_From );
					}
				}
			}
			if ( info.ButtonID == 17 )
			{
				if ( m_Box.Mugwort > 0 )
				{
					if ( m_Box.Mugwort > m_Box.WithdrawIncrement )
					{
						m_From.AddToBackpack( new Mugwort(m_Box.WithdrawIncrement) );
						m_Box.Mugwort = m_Box.Mugwort - m_Box.WithdrawIncrement;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else if (m_Box.Mugwort > 0)
                				{
						m_From.AddToBackpack( new Mugwort(m_Box.Mugwort) );
						m_Box.Mugwort = 0;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else
					{
						m_From.SendMessage( "You do not have any of that!" );
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
						m_Box.BeginCombine( m_From );
					}
				}
			}
			if ( info.ButtonID == 18 )
			{
				if ( m_Box.Mustard > 0 )
				{
					if ( m_Box.Mustard > m_Box.WithdrawIncrement )
					{
						m_From.AddToBackpack( new Mustard(m_Box.WithdrawIncrement) );
						m_Box.Mustard = m_Box.Mustard - m_Box.WithdrawIncrement;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else if (m_Box.Mustard > 0)
                				{
						m_From.AddToBackpack( new Mustard(m_Box.Mustard) );
						m_Box.Mustard = 0;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else
					{
						m_From.SendMessage( "You do not have any of that!" );
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
						m_Box.BeginCombine( m_From );
					}
				}
			}
			if ( info.ButtonID == 19 )
			{
				if ( m_Box.Olive > 0 )
				{
					if ( m_Box.Olive > m_Box.WithdrawIncrement )
					{
						m_From.AddToBackpack( new Olive(m_Box.WithdrawIncrement) );
						m_Box.Olive = m_Box.Olive - m_Box.WithdrawIncrement;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else if (m_Box.Olive > 0)
                				{
						m_From.AddToBackpack( new Olive(m_Box.Olive) );
						m_Box.Olive = 0;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else
					{
						m_From.SendMessage( "You do not have any of that!" );
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
						m_Box.BeginCombine( m_From );
					}
				}
			}
			if ( info.ButtonID == 20 )
			{
				if ( m_Box.Oregano > 0 )
				{
					if ( m_Box.Oregano > m_Box.WithdrawIncrement )
					{
						m_From.AddToBackpack( new Oregano(m_Box.WithdrawIncrement) );
						m_Box.Oregano = m_Box.Oregano - m_Box.WithdrawIncrement;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else if (m_Box.Oregano> 0)
                				{
						m_From.AddToBackpack( new Oregano(m_Box.Oregano) );
						m_Box.Oregano = 0;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else
					{
						m_From.SendMessage( "You do not have any of that!" );
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
						m_Box.BeginCombine( m_From );
					}
				}
			}
			if ( info.ButtonID == 21 )
			{
				if ( m_Box.Peppercorn > 0 )
				{
					if ( m_Box.Peppercorn > m_Box.WithdrawIncrement )
					{
						m_From.AddToBackpack( new Peppercorn(m_Box.WithdrawIncrement) );
						m_Box.Peppercorn = m_Box.Peppercorn - m_Box.WithdrawIncrement;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else if (m_Box.Peppercorn> 0)
                				{
						m_From.AddToBackpack( new Peppercorn(m_Box.Peppercorn) );
						m_Box.Peppercorn = 0;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else
					{
						m_From.SendMessage( "You do not have any of that!" );
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
						m_Box.BeginCombine( m_From );
					}
				}
			}
			if ( info.ButtonID == 22 )
			{
				if ( m_Box.RoseHerb > 0 )
				{
					if ( m_Box.RoseHerb > m_Box.WithdrawIncrement )
					{
						m_From.AddToBackpack( new RoseHerb(m_Box.WithdrawIncrement) );
						m_Box.RoseHerb = m_Box.RoseHerb - m_Box.WithdrawIncrement;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else if (m_Box.RoseHerb > 0)
                				{
						m_From.AddToBackpack( new RoseHerb(m_Box.RoseHerb) );
						m_Box.RoseHerb = 0;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else
					{
						m_From.SendMessage( "You do not have any of that!" );
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
						m_Box.BeginCombine( m_From );
					}
				}
			}
			if ( info.ButtonID == 23 )
			{
				if ( m_Box.Rosemary > 0 )
				{
					if ( m_Box.Rosemary > m_Box.WithdrawIncrement )
					{
						m_From.AddToBackpack( new Rosemary(m_Box.WithdrawIncrement) );
						m_Box.Rosemary = m_Box.Rosemary - m_Box.WithdrawIncrement;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else if (m_Box.Rosemary > 0)
                				{
						m_From.AddToBackpack( new Rosemary(m_Box.Rosemary) );
						m_Box.Rosemary = 0;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else
					{
						m_From.SendMessage( "You do not have any of that!" );
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
						m_Box.BeginCombine( m_From );
					}
				}
			}
			if ( info.ButtonID == 24 )
			{
				if ( m_Box.Saffron > 0 )
				{
					if ( m_Box.Saffron > m_Box.WithdrawIncrement )
					{
						m_From.AddToBackpack( new Saffron(m_Box.WithdrawIncrement) );
						m_Box.Saffron = m_Box.Saffron - m_Box.WithdrawIncrement;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else if (m_Box.Saffron > 0)
                				{
						m_From.AddToBackpack( new Saffron(m_Box.Saffron) );
						m_Box.Saffron = 0;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else
					{
						m_From.SendMessage( "You do not have any of that!" );
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
						m_Box.BeginCombine( m_From );
					}
				}
			}
			if ( info.ButtonID == 25 )
			{
				if ( m_Box.Sage > 0 )
				{
					if ( m_Box.Sage > m_Box.WithdrawIncrement )
					{
						m_From.AddToBackpack( new Sage(m_Box.WithdrawIncrement) );
						m_Box.Sage = m_Box.Sage - m_Box.WithdrawIncrement;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else if (m_Box.Sage > 0)
                				{
						m_From.AddToBackpack( new Sage(m_Box.Sage) );
						m_Box.Sage = 0;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else
					{
						m_From.SendMessage( "You do not have any of that!" );
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
						m_Box.BeginCombine( m_From );
					}
				}
			}
			if ( info.ButtonID == 26 )
			{
				if ( m_Box.Thyme > 0 )
				{
					if ( m_Box.Thyme > m_Box.WithdrawIncrement )
					{
						m_From.AddToBackpack( new Thyme(m_Box.WithdrawIncrement) );
						m_Box.Thyme = m_Box.Thyme - m_Box.WithdrawIncrement;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else if (m_Box.Thyme > 0)
                				{
						m_From.AddToBackpack( new Thyme(m_Box.Thyme) );
						m_Box.Thyme = 0;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else
					{
						m_From.SendMessage( "You do not have any of that!" );
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
						m_Box.BeginCombine( m_From );
					}
				}
			}
			if ( info.ButtonID == 27 )
			{
				if ( m_Box.Valerian > 0 )
				{
					if ( m_Box.Valerian > m_Box.WithdrawIncrement )
					{
						m_From.AddToBackpack( new Valerian(m_Box.WithdrawIncrement) );
						m_Box.Valerian = m_Box.Valerian - m_Box.WithdrawIncrement;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else if (m_Box.Valerian > 0)
                				{
						m_From.AddToBackpack( new Valerian(m_Box.Valerian) );
						m_Box.Valerian = 0;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else
					{
						m_From.SendMessage( "You do not have any of that!" );
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
						m_Box.BeginCombine( m_From );
					}
				}
			}
			if ( info.ButtonID == 28 )
			{
				if ( m_Box.WillowBark > 0 )
				{
					if ( m_Box.WillowBark > m_Box.WithdrawIncrement )
					{
						m_From.AddToBackpack( new WillowBark(m_Box.WithdrawIncrement) );
						m_Box.WillowBark = m_Box.WillowBark - m_Box.WithdrawIncrement;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else if (m_Box.WillowBark > 0)
                				{
						m_From.AddToBackpack( new WillowBark(m_Box.WillowBark) );
						m_Box.WillowBark = 0;
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
					}
					else
					{
						m_From.SendMessage( "You do not have any of that!" );
						m_From.SendGump( new HerbJarsGump( m_From, m_Box ) );
						m_Box.BeginCombine( m_From );
					}
				}
			}
		}
	}

}

namespace Server.Items
{
	public class HerbJarsTarget : Target
	{
		private HerbJars m_Box;

		public HerbJarsTarget( HerbJars box ) : base( 18, false, TargetFlags.None )
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
