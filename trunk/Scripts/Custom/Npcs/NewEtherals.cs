using System; 
using Server.Mobiles; 

namespace Server.Items 
{ 
	public class EtherealPolarBear : EtherealMount 
	{ 
		[Constructable] 
		public EtherealPolarBear() : base( 11676, 0x3E92 ) 
		{ 
			Name = "Ehereal Polar Bear";
			ItemID = 8417;
			MountedID = 16069;
			RegularID = 8417;
			LootType = LootType.Blessed;
		} 

		public EtherealPolarBear( Serial serial ) : base( serial ) 
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

			if ( Name != "Ethereal Polar Bear" )
				Name = "Ethereal Polar Bear";
		} 
	}

//__________________________________________________________________________________________________________________________
	
	public class EtherealUndeadSteed : EtherealMount 
	{ 

		[Constructable] 
		public EtherealUndeadSteed() : base( 11669, 0x3E90 )                            
		{ 
			Name = "Ethereal Undead Steed Statuette";
			ItemID = 9680;
 			MountedID = 16059;
			RegularID = 9680;
			LootType = LootType.Blessed;
                } 

		public EtherealUndeadSteed( Serial serial ) : base( serial ) 
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

			if ( Name != "Ethereal Undead Steed Statuette" )
				Name = "Ethereal Undead Steed Statuette";
		} 
	}

//__________________________________________________________________________________________________________________________

	public class EtherealDemon : EtherealMount 
	{ 

		[Constructable] 
		public EtherealDemon() : base( 11670, 0x3E91 )                            
		{ 
			Name = "Ethereal Demon Statuette";
			ItemID = 8403;
			MountedID = 16239;
			RegularID = 8403;
			LootType = LootType.Blessed; 
		} 

		public EtherealDemon( Serial serial ) : base( serial ) 
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

			if ( Name != "Ethereal Demon Statuette" )
				Name = "Ethereal Demon Statuette";
		} 
	}
}

