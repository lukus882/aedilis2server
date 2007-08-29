///////////////////////////////////////////////////
///// Created By Bad Karma aka Broadside///////////
///////////////////////////////////////////////////

using System;
using Server;

namespace Server.Items
{
	public class BlackthorneCostume : Item, IDyable
	{

		public bool m_Transformed;
		public Timer m_TransformTimer;
		private DateTime m_End;

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Transformed
		{
			get{ return m_Transformed; }
			set{ m_Transformed = value; }
		}

		[Constructable]
		public BlackthorneCostume() : base( 0x1F03 )
		{
                        Name = "Blackthorne Costume";
			
                        Hue = 2;
                        Layer = Layer.OuterTorso;
                        ItemID = 0x1F03;

			LootType = LootType.Blessed;
			Weight = 3.0;
		}

		public BlackthorneCostume( Serial serial ) : base( serial )
		{
		}

     		public override void OnDoubleClick( Mobile from ) 
		{ 
			
                        if ( Parent != from ) 
                        { 
                                from.SendMessage( "The costume must be equiped to be used." ); 
                        } 

			else if ( from.Mounted == true )
			{
				from.SendMessage( "You cannot be mounted while wearing your costume!" );
			}
			
			else if ( from.BodyMod == 0x0 )
                        { 
				
				LootType = LootType.Blessed;
               			from.SendMessage( "You pull the mask over your head." );
				from.PlaySound( 0x440 );
				from.BodyMod = 769;
				from.DisplayGuildTitle = false; 
				ItemID = 9860;
                        
			}
			else
			{
				from.SendMessage( "You lower the mask." );
				from.PlaySound( 0x440 );
				from.BodyMod = 0x0;		
				from.DisplayGuildTitle = true;
				ItemID = 0x1F03;
			}
		}

		public virtual bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			Hue = sender.DyedHue;

			return true;
		}
			
		public override void OnRemoved( Object parent)
      		{
      			
			            base.OnRemoved(parent);

            		if (parent is Mobile)
            		{
                		Mobile from = (Mobile)parent;

				if ( from.BodyMod == 769 )
                        	{ 
				
				from.SendMessage( "You lower the mask." );
				from.PlaySound( 0x440 );
				from.BodyMod = 0x0;
				from.DisplayGuildTitle = true;
				}
                        
			}
				
      			
      		}			

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			ItemID = 0x1F03;
		}
	}
}
