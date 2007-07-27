using System;
using Server.Misc;

namespace Server.Items
{
	public class CooksCap : Cap 
	{ 
		private SkillMod m_SkillMod0; 
		
		[Constructable] 
		public CooksCap() : base( 0x1715 ) 
		{ 
			Name = "Cook's Cap";
			Hue = 0x47E;
			DefineMods();
		} 

		private void DefineMods()
		{
			m_SkillMod0 = new DefaultSkillMod( SkillName.Cooking, true, 5 ); 
		}

		private void SetMods( Mobile wearer )
		{			
			wearer.AddSkillMod( m_SkillMod0 );  
		}

		public override bool OnEquip( Mobile from ) 
		{ 
			SetMods( from );
			return true;  
		} 

		public override bool Dye( Mobile from, DyeTub sender )
		{
			from.SendLocalizedMessage( 1042083 ); // You can not dye that.
			return false;
		}

		public override void OnRemoved( object parent ) 
		{ 
			if ( parent is Mobile ) 
			{ 
				Mobile m = (Mobile)parent;
//				m.RemoveStatMod( "CooksCap" ); 

				if ( m_SkillMod0 != null ) 
					m_SkillMod0.Remove(); 

			} 
		} 

		public override void OnSingleClick( Mobile from ) 
		{ 
			this.LabelTo( from, Name ); 
		} 

		public CooksCap( Serial serial ) : base( serial ) 
		{ 
			DefineMods();
			
			if ( Parent != null && this.Parent is Mobile ) 
				SetMods( (Mobile)Parent );
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
		} 
	}

	public class ChefsCap : Cap 
	{ 
		private SkillMod m_SkillMod0; 
		
		[Constructable] 
		public ChefsCap() : base( 0x1715 ) 
		{ 
			Name = "Chef's Cap";
			Hue = 0x47E;
			DefineMods();
		} 

		private void DefineMods()
		{
			m_SkillMod0 = new DefaultSkillMod( SkillName.Cooking, true, 10 ); 
		}

		private void SetMods( Mobile wearer )
		{			
			wearer.AddSkillMod( m_SkillMod0 );  
		}

		public override bool OnEquip( Mobile from ) 
		{ 
			SetMods( from );
			return true;  
		} 

		public override bool Dye( Mobile from, DyeTub sender )
		{
			from.SendLocalizedMessage( 1042083 ); // You can not dye that.
			return false;
		}

		public override void OnRemoved( object parent ) 
		{ 
			if ( parent is Mobile ) 
			{ 
				Mobile m = (Mobile)parent;
//				m.RemoveStatMod( "ChefsCap" ); 

				if ( m_SkillMod0 != null ) 
					m_SkillMod0.Remove(); 

			} 
		} 

		public override void OnSingleClick( Mobile from ) 
		{ 
			this.LabelTo( from, Name ); 
		} 

		public ChefsCap( Serial serial ) : base( serial ) 
		{ 
			DefineMods();
			
			if ( Parent != null && this.Parent is Mobile ) 
				SetMods( (Mobile)Parent );
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
		} 
	}

	public class CooksApron : HalfApron
	{ 
		private SkillMod m_SkillMod0; 
		
		[Constructable] 
		public CooksApron() : base( 0x153B ) 
		{ 
			Name = "Cook's Apron";
			Hue = 0x47E;
			DefineMods();
		} 

		private void DefineMods()
		{
			m_SkillMod0 = new DefaultSkillMod( SkillName.Cooking, true, 5 ); 
		}

		private void SetMods( Mobile wearer )
		{			
			wearer.AddSkillMod( m_SkillMod0 );  
		}

		public override bool OnEquip( Mobile from ) 
		{ 
			SetMods( from );
			return true;  
		} 

		public override bool Dye( Mobile from, DyeTub sender )
		{
			from.SendLocalizedMessage( 1042083 ); // You can not dye that.
			return false;
		}

		public override void OnRemoved( object parent ) 
		{ 
			if ( parent is Mobile ) 
			{ 
				Mobile m = (Mobile)parent;
//				m.RemoveStatMod( "CooksApron" ); 

				if ( m_SkillMod0 != null ) 
					m_SkillMod0.Remove(); 

			} 
		} 

		public override void OnSingleClick( Mobile from ) 
		{ 
			this.LabelTo( from, Name ); 
		} 

		public CooksApron( Serial serial ) : base( serial ) 
		{ 
			DefineMods();
			
			if ( Parent != null && this.Parent is Mobile ) 
				SetMods( (Mobile)Parent );
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
		} 
	}

	public class ChefsApron : HalfApron
	{ 
		private SkillMod m_SkillMod0; 
		
		[Constructable] 
		public ChefsApron() : base( 0x153B ) 
		{ 
			Name = "Chef's Apron";
			Hue = 0x47E;
			DefineMods();
		} 

		private void DefineMods()
		{
			m_SkillMod0 = new DefaultSkillMod( SkillName.Cooking, true, 10 ); 
		}

		private void SetMods( Mobile wearer )
		{			
			wearer.AddSkillMod( m_SkillMod0 );  
		}

		public override bool OnEquip( Mobile from ) 
		{ 
			SetMods( from );
			return true;  
		} 

		public override bool Dye( Mobile from, DyeTub sender )
		{
			from.SendLocalizedMessage( 1042083 ); // You can not dye that.
			return false;
		}

		public override void OnRemoved( object parent ) 
		{ 
			if ( parent is Mobile ) 
			{ 
				Mobile m = (Mobile)parent;
//				m.RemoveStatMod( "ChefsApron" ); 

				if ( m_SkillMod0 != null ) 
					m_SkillMod0.Remove(); 

			} 
		} 

		public override void OnSingleClick( Mobile from ) 
		{ 
			this.LabelTo( from, Name ); 
		} 

		public ChefsApron( Serial serial ) : base( serial ) 
		{ 
			DefineMods();
			
			if ( Parent != null && this.Parent is Mobile ) 
				SetMods( (Mobile)Parent );
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
		} 
	}
} 
