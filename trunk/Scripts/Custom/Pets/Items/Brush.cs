using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	public class TamingBrush : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefTamingCraft.CraftSystem; } }

		[Constructable]
		public TamingBrush() : base( 0x1373 )
		{
			Weight = 2.0;
		}

		[Constructable]
		public TamingBrush( int uses ) : base( uses, 0x1373 )
		{
			Weight = 2.0;
		}

      		public override void OnDoubleClick( Mobile from ) 
     	 	{ 
			if ( FSATS.EnableTamingCraft == false )
			{
				from.SendMessage( "Taming craft has been disabled, Contact your server administrator for details." );
			}
			else
			{
				base.OnDoubleClick( from );
			}
      		}

		public TamingBrush( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( Weight == 1.0 )
				Weight = 2.0;
		}
	}
}