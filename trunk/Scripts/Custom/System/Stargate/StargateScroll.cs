using System;
using System.Collections;
using Server;
using Server.Network;
using Server.Stargate;

namespace Server.Items
{
	public class StargateScroll : Item
	{
		public override int LabelNumber{ get{ return 0; } }
		public override string DefaultName{ get{ return "a star scroll"; } }

		private StargateDesign m_Design;
		private string m_Combination;
		private Mobile m_Decoder;

		//[CommandProperty( AccessLevel.GameMaster )]
		public StargateDesign Design
		{
			get
			{
				if ( m_Design == null )
					m_Design = Stargate.Stargate.FindGate( m_Combination );
				return m_Design;
			}
			set
			{
				m_Design = value;
				if ( m_Design != null )
				{
					m_Combination = m_Design.Combination;
					InvalidateProperties();
				}
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public string Combination
		{
			get
			{
				if ( ( m_Combination == null || m_Combination == String.Empty ) && m_Design != null )
					m_Combination = m_Design.Combination;
				return m_Combination;
			}
			set
			{
				m_Combination = value;
				m_Design = Stargate.Stargate.FindGate( value );
				InvalidateProperties();
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Decoder{ get{ return m_Decoder; } set{ m_Decoder = value; } }

		[Constructable]
		public StargateScroll( string design ) : this( Stargate.Stargate.FindGate( design ) )
		{
		}

		public StargateScroll( StargateDesign design ) : base( 0x227C )
		{
			Design = design;
		}

		public StargateScroll( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( Design != null )
			{
				string message;
				double drop = m_Design.DropChance;
				if ( m_Decoder != null )
					message = "preserved for use throughout the ages";
				else if ( drop <= 0.10 )
					message = "considerably damaged and possibly unrecoverable";
				else if ( drop <= 0.25 )
					message = "ruined, tattered and about to fall apart";
				else if ( drop <= 0.50 )
					message = "very damaged from old age";
				else if ( drop <= 0.75 )
					message = "very good condition and moderately readable";
				else if ( drop <= 0.90 )
					message = "remarkably preserved with slight damages";
				else if ( drop < 100.0 )
					message = "incredibly intact, almost perfectly";
				else
					message = "perfectly intact, not a scratch";

				list.Add( 1062613, String.Format( "<i> {0} </i>", message ) );
			}
		}

		public override void AddNameProperty( ObjectPropertyList list )
		{
			list.Add( 1060844, "a star" );
		}

		public bool HasRequiredSkill( Mobile from )
		{
			return from.Skills[SkillName.Cartography].Value >= 90 - m_Design.DropChance;
		}

		public void DisplayTo( Mobile from )
		{
			from.SendMessage( "You attempt to read the words carefully..." );
			if ( m_Decoder != from && !HasRequiredSkill( from ) )
				from.SendMessage( "...but you fail to make anything of the scroll." );
			else
			{
				from.SendMessage( "..." + TranslateCombo() );
				from.PlaySound( 0x249 );
			}

		}

		public void Decode( Mobile from )
		{
			if ( !HasRequiredSkill( from ) )
				from.SendMessage( "The scroll is too difficult to attempt to decode." );
			else
			{
				double minSkill = 90 - m_Design.DropChance;

				if ( !from.CheckSkill( SkillName.Cartography, minSkill, minSkill + 45.0 ) )
					from.LocalOverheadMessage( MessageType.Regular, 0x3B2, false, "You fail to make anything of the scroll." );
				else
				{
					m_Decoder = from;
					DisplayTo( from );
				}
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			else if ( Combination == null || Design == null )
				from.SendMessage( "This scroll has lost its text forever." );
			else if ( m_Decoder == null )
				Decode( from );
			else
				DisplayTo( from );
		}

		public string TranslateCombo()
		{
			string[] words = new string[m_Combination.Length];
			for( int i = 0; i < m_Combination.Length; i++ )
				words[i] = StargateDesign.CombinationWord( m_Combination[i] );
			return String.Join( " ", words );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( m_Decoder );
			writer.Write( m_Combination );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_Decoder = reader.ReadMobile();
			Stargate.Stargate.NeedReference.Add( new StargateDesignReference( this, reader.ReadString() ) );
		}

		public void SetDesign( StargateDesign design )
		{
			m_Design = design;
		}
	}
}