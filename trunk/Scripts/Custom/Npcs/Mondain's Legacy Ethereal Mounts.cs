using System; 
using Server.Mobiles; 

namespace Server.Items 
{ 
	public class EtherealChargeroftheFallen : EtherealMount 
	{ 
		[Constructable] 
		public EtherealChargeroftheFallen() : base( 11676, 0x3E92 ) 
		{ 
			Name = "Charger of the Fallen";
			ItemID = 11676;
			LootType = LootType.Blessed;
		} 

		public EtherealChargeroftheFallen( Serial serial ) : base( serial ) 
		{ 
		} 

		

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 

			writer.Write( (int) 0 ); 
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 

			int version = reader.ReadInt(); 

			if ( Name != "Charger of the Fallen" )
				Name = "Charger of the Fallen";
		} 
	}

//__________________________________________________________________________________________________________________________

	public class EtherealChimera : EtherealMount 
	{ 

		[Constructable] 
		public EtherealChimera() : base( 11669, 0x3E90 )                            
		{ 
			Name = "Ethereal Chimera Statuette";
			ItemID = 11669;
 			LootType = LootType.Blessed;
                } 

		public EtherealChimera( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 

			writer.Write( (int) 0 ); 
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 

			int version = reader.ReadInt();

			if ( Name != "Ethereal Chimera Statuette" )
				Name = "Ethereal Chimera Statuette";
		} 
	}

//__________________________________________________________________________________________________________________________

	public class EtherealCuSidhe : EtherealMount 
	{ 

		[Constructable] 
		public EtherealCuSidhe() : base( 11670, 0x3E91 )                            
		{ 
			Name = "Ethereal Cu Sidhe Statuette";
			ItemID = 11670;
			LootType = LootType.Blessed; 
		} 

		public EtherealCuSidhe( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 

			writer.Write( (int) 0 ); 
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 

			int version = reader.ReadInt();

			if ( Name != "Ethereal Cu Sidhe Statuette" )
				Name = "Ethereal Cu Sidhe Statuette";
		} 
	}
}
