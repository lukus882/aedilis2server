using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Network;
using Server.Stargate;

namespace Server.Items
{
	public class StargateAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new StargateAddon( m_Combination, m_Area, m_Design.Name, m_StartingPos, m_Reversed ); } }

		public override string DefaultName{ get{ return "a stargate deed"; } }
		public override int LabelNumber{ get{ return 0; } }

		private string m_Combination;
		private string m_Area;
		private DesignType m_Design;
		private uint m_StartingPos;
		private bool m_Reversed;

		[CommandProperty( AccessLevel.GameMaster )]
		public string Combination{ get{ return m_Combination; } set{ m_Combination = value; } }
		[CommandProperty( AccessLevel.GameMaster )]
		public string Area{ get{ return m_Area; } set{ m_Area = value; } }
		[CommandProperty( AccessLevel.GameMaster )]
		public string Design
		{ 
			get{ return m_Design == null ? null : m_Design.Name; } 
			set
			{ 
				DesignType design = DesignTypes.FindDesign( value );
				if ( design != null && design != m_Design )
					m_Design = design;
			}
		}
		[CommandProperty( AccessLevel.GameMaster )]
		public uint StartingPos{ get{ return m_StartingPos; } set{ m_StartingPos = value; } }
		[CommandProperty( AccessLevel.GameMaster )]
		public bool Reversed{ get{ return m_Reversed; } set{ m_Reversed = value; } }

		[Constructable]
		public StargateAddonDeed( string combo ) : this( combo, "Ring" )
		{
		}

		[Constructable]
		public StargateAddonDeed( string combo, string design ) : this( combo, "Unknown Area", design )
		{
		}

		[Constructable]
		public StargateAddonDeed( string combo, string area, string design ) : this( combo, area, design, 0 )
		{
		}

		[Constructable]
		public StargateAddonDeed( string combo, string design, uint startingpos ) : this( combo, "Unknown Area", design, startingpos )
		{
		}

		[Constructable]
		public StargateAddonDeed( string combo, string area, string design, uint startingpos ) : this( combo, area, design, startingpos, false )
		{
		}

		[Constructable]
		public StargateAddonDeed( string combo, string area, string design, uint startingpos, bool reversed ) : base()
		{
			m_Reversed = reversed;
			m_Combination = combo;
			m_Area = area;
			Design = design;
			m_StartingPos = startingpos;
		}

		public StargateAddonDeed( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.AccessLevel <= AccessLevel.GameMaster )
				return;
			base.OnDoubleClick( from );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( m_Combination );
			writer.Write( m_Area );
			writer.Write( m_Design.Name );
			writer.Write( m_StartingPos );
			writer.Write( m_Reversed );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_Combination = reader.ReadString();
			m_Area = reader.ReadString();
			m_Design = DesignTypes.FindDesign( reader.ReadString() );
			m_StartingPos = reader.ReadUInt();
			m_Reversed = reader.ReadBool();
		}
	}

	public enum UpdateFlag
	{
		None,
		Hue,
		Design
	}

	public class StargateAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new StargateAddonDeed( m_GateDesign.Combination, m_GateDesign.Area, m_GateDesign.Design, m_GateDesign.StartingPos, m_GateDesign.Reversed ); } }
		public override bool ShareHue{ get{ return false; } }


		private StargateDesign m_GateDesign;
		private string m_CurrentCombo;
		private List<SymbolComponent> m_ActiveSymbols;
		private ExpireTimer m_Expire;
		private bool m_InUse;

		[CommandProperty( AccessLevel.GameMaster )]
		public StargateDesign GateDesign{ get{ return m_GateDesign; } set{ m_GateDesign = value; } }

		[CommandProperty( AccessLevel.GameMaster, (AccessLevel)100 )]
		public string CurrentCombo{ get{ return m_CurrentCombo; } set{ m_CurrentCombo = value; } }

		public List<SymbolComponent> ActiveSymbols{ get{ return m_ActiveSymbols; } set{ m_ActiveSymbols = value; } }
		public ExpireTimer Expire{ get{ return m_Expire; } set{ m_Expire = value; } }
		public bool InUse{ get{ return m_InUse; } set{ m_InUse = value; } }

		[Constructable]
		public StargateAddon( string combo ) : this( combo, 0 )
		{
		}

		[Constructable]
		public StargateAddon( string combo, uint startingpos ) : this( combo, "Uknown Area", startingpos )
		{
		}

		[Constructable]
		public StargateAddon( string combo, string design ) : this( combo, "Uknown Area", design )
		{
		}

		[Constructable]
		public StargateAddon( string combo, string design, uint startingpos ) : this( combo, "Unknown Area", design, startingpos )
		{
		}

		[Constructable]
		public StargateAddon( string combo, string area, string design ) : this( combo, area, design, 0 )
		{
		}

		[Constructable]
		public StargateAddon( string combo, string area, string design, uint startingpos ) : this( combo, area, design, 0, false )
		{
		}

		[Constructable]
		public StargateAddon( string combo, string area, string design, uint startingpos, bool reversed ) : base()
		{
			if ( Stargate.Stargate.FindGate( combo ) != null )
			{
				Console.WriteLine( "Warning: Duplicate stargate removed {0}.", combo );
				Delete();
				return;
			}
			ItemID = 0x1BC3; //Teleporter
			m_ActiveSymbols = new List<SymbolComponent>();
			m_CurrentCombo = String.Empty;

			AddComponent( new SymbolComponent( 'a', 0 ), 0, 0, 0 );
			AddComponent( new SymbolComponent( 'b', 1 ), 0, 0, 0 );
			AddComponent( new SymbolComponent( 'c', 2 ), 0, 0, 0 );
			AddComponent( new SymbolComponent( 'd', 3 ), 0, 0, 0 );
			AddComponent( new SymbolComponent( 'e', 4 ), 0, 0, 0 );
			AddComponent( new SymbolComponent( 'f', 5 ), 0, 0, 0 );
			AddComponent( new SymbolComponent( 'g', 6 ), 0, 0, 0 );
			AddComponent( new SymbolComponent( 'h', 7 ), 0, 0, 0 );
			AddComponent( new SymbolComponent( 'i', 8 ), 0, 0, 0 );
			AddComponent( new SymbolComponent( 'j', 9 ), 0, 0, 0 );
			AddComponent( new SymbolComponent( 'k', 10 ), 0, 0, 0 );
			AddComponent( new SymbolComponent( 'l', 11 ), 0, 0, 0 );

			m_GateDesign = new StargateDesign( combo, area, design, startingpos, reversed, this );
			Stargate.Stargate.AddGate( m_GateDesign );
			Update( UpdateFlag.Hue | UpdateFlag.Design );
		}

		public StargateAddon( Serial serial ) : base( serial )
		{
		}

		public void Update( UpdateFlag flag )
		{
			if ( flag == UpdateFlag.None )
				return;
			if ( Components.Count != 12 )
				return;
			if ( m_GateDesign == null )
				return;

			bool updatedesign = ( flag & UpdateFlag.Design ) != 0;
			bool updatehue = ( flag & UpdateFlag.Hue ) != 0;

			if ( updatedesign && m_GateDesign.DesignType == null )
				return;

			int[] offsets = null;
			int hue = 0;
			int start = 0;
			bool reversed = false;

			if ( updatedesign )
			{
				if ( m_GateDesign.DesignType == null )
					return;
				offsets = m_GateDesign.DesignType.DesignOffsets;
				start = (int)(m_GateDesign.StartingPos % 12);
				reversed = m_GateDesign.Reversed;
				start = reversed ? 11 - start : start;
			}
			if ( updatehue )
				hue = m_GateDesign.NormalHue;

			for ( int c = 0; c < 12; c++ )
			{
				int offsetvalue = ( ( c + start ) % 12 ) * 2;
				SymbolComponent symbol = (SymbolComponent)Components[c];
				if ( updatedesign )
					symbol.Offset = new Point3D( offsets[offsetvalue], offsets[offsetvalue+1], 0 );
				if ( updatehue )
					symbol.Hue = hue;
			}
		}
/*
		public override void OnChop( Mobile from )
		{
			if ( from.AccessLevel <= AccessLevel.GameMaster )
				return;
			base.OnChop( from );
		}
*/
		public void OnSymbolUsed( SymbolComponent symbol, Mobile from )
		{
			if ( !from.InRange( Location, 2 ) || !from.InLOS( this ) )
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
			else if ( m_InUse || ( m_CurrentCombo != null && m_CurrentCombo.Length >= 6 ) )
				from.SendMessage( "You notice the stargate is already activated." );
			else if ( m_ActiveSymbols.Contains( symbol ) )
				from.SendMessage( "You notice this symbol has already been activated." );
			else
			{
				m_CurrentCombo += symbol.Letter;
				m_ActiveSymbols.Add( symbol );
				symbol.PlaySymbolEffect( true );

				if ( m_CurrentCombo.Length != 6 )
				{
					if ( m_Expire != null )
						m_Expire.Stop();

					m_Expire = new ExpireTimer( this );
					m_Expire.Start();
					return;
				}
				
				StargateDesign design = Stargate.Stargate.FindGate( m_CurrentCombo );
				if ( design != null && design.Gate != null )
					ActivateGate( from, design.Gate );
				else
				{
					from.SendMessage( "The stargate could not find a destination." );
					DeactivateGate( null, null, null );
				}

				if ( m_Expire != null )
				{
					m_Expire.Stop();
					m_Expire = null;
				}
			}
		}

		public void ActivateGate( Mobile from, StargateAddon gate )
		{
			if ( gate == this )
			{
				from.SendMessage( "The stargate cannot dial to itself." );
				DeactivateGate( null, null, null );
			}
			else if ( gate.InUse )
			{
				from.SendMessage( "The stargate could not reach the destination." );
				DeactivateGate( null, null, null );

				if ( gate.Expire != null )
				{
					gate.Expire.Stop();
					gate.Expire = null;
				}
			}
			else
			{
				from.SendMessage( "The stargate activates with a gate to {0}.", gate.GateDesign.Area );

				GateComponent gatesource = new GateComponent( gate );
				gatesource.Hue = m_GateDesign.GateHue;
				gatesource.MoveToWorld( Location, Map );

				GateComponent gatedest = new GateComponent();
				gatedest.Name = String.Format( "Stargate from {0}", m_GateDesign.Area );
				gatedest.Hue = gate.GateDesign.GateHue;
				gatedest.MoveToWorld( gate.Location, gate.Map );

				m_InUse = gate.InUse = true;
				foreach( SymbolComponent s in gate.Components )
					s.PlaySymbolEffect( true );
				Timer.DelayCall( TimeSpan.FromSeconds( 45.0 ), new TimerStateCallback( DeactivateCallBack ), new object[]{ this, gatesource, gatedest, gate } );
				return;
			}
		}

		public static void DeactivateCallBack( object state )
		{
			object[] states = (object[])state;
			((StargateAddon)states[0]).DeactivateGate( (GateComponent)states[1], (GateComponent)states[2], (StargateAddon)states[3] );
		}

		public void DeactivateGate( GateComponent source, GateComponent destination, StargateAddon target )
		{
			if ( source != null )
				source.Delete();
			if ( destination != null )
				destination.Delete();

			foreach( SymbolComponent s in m_ActiveSymbols )
				s.PlaySymbolEffect( false );
			m_ActiveSymbols.Clear();
			m_InUse = false;
			m_CurrentCombo = null;

			if ( target != null )
			{
				foreach( SymbolComponent s in target.Components )
					s.PlaySymbolEffect( false );
				target.ActiveSymbols.Clear();
				target.InUse = false;
				target.CurrentCombo = null;
			}
		}

		public void SetDesign( StargateDesign design )
		{
			m_GateDesign = design;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			//writer.Write( m_GateDesign.Combination );
			m_GateDesign.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_ActiveSymbols = new List<SymbolComponent>();

			m_GateDesign = new StargateDesign( reader );
			m_GateDesign.Gate = this;
			Stargate.Stargate.AddGate( m_GateDesign );

			foreach( SymbolComponent s in Components )
				s.Hue = m_GateDesign.NormalHue;
			//Stargate.Stargate.NeedReference.Add( new StargateDesignReference( this, reader.ReadString() ) );
		}

		public override void OnDelete()
		{
			if ( m_GateDesign != null )
			{
				Stargate.Stargate.RemoveGate( m_GateDesign );
				m_GateDesign = null;
			}

			base.OnDelete();
		}

		public class ExpireTimer : Timer
		{
			private StargateAddon m_Gate;

			public ExpireTimer( StargateAddon gate ) : base( TimeSpan.FromSeconds( 45.0 ) )
			{
				m_Gate = gate;
				Priority = TimerPriority.OneSecond;
			}

			protected override void OnTick()
			{
				m_Gate.DeactivateGate( null, null, null );
			}
		}
	}

	public class SymbolComponent : AddonComponent
	{
		private char m_Letter;

		[CommandProperty( AccessLevel.GameMaster )]
		public char Letter{ get{ return m_Letter; } set{ m_Letter = value; } }

		public override int LabelNumber{ get{ return 0; } }
		public override string DefaultName{ get{ return StargateDesign.CombinationWord( m_Letter ); } }

		public SymbolComponent( char letter, int id ) : base( 6173 + id )
		{
			m_Letter = letter;
		}

		public SymbolComponent( Serial serial ) : base( serial )
		{
		}

		public void PlaySymbolEffect( bool activate )
		{
			if ( Deleted || Addon == null || Addon.Deleted )
				return;
			StargateAddon addon = (StargateAddon)Addon;
			if ( addon.GateDesign == null )
				return;
			Hue = activate ? addon.GateDesign.ActivateHue : addon.GateDesign.DeactivateHue;
			Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), new TimerCallback( EndSymbolEffect ) );
		}

		public void EndSymbolEffect()
		{
			Hue = ((StargateAddon)Addon).GateDesign.NormalHue;
		}

		public override void OnDoubleClick( Mobile from )
		{
			StargateAddon addon = (StargateAddon)Addon;
			addon.OnSymbolUsed( this, from );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			writer.Write( (char)m_Letter );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_Letter = reader.ReadChar();
		}
	}

	public class GateComponent : Moongate
	{
		public override int LabelNumber{ get{ return 0; } }
		public override string DefaultName{ get{ return "Stargate"; } }
		//public override bool NowhereMessage{ get{ return false; } }

		private bool m_Connected;
		public bool Connected{ get{ return m_Connected; } set{ m_Connected = value; InvalidateProperties(); } }

		private StargateAddon m_ConnectedGate;
		public StargateAddon ConnectedGate{ get{ return m_ConnectedGate; } set{ m_ConnectedGate = value; InvalidateProperties(); } }

		public override void AddNameProperty( ObjectPropertyList list )
		{
			if ( Connected && ConnectedGate != null && ConnectedGate.GateDesign != null )
				list.Add( String.Format( "Stargate to {0}", ConnectedGate.GateDesign.Area ) );
			else
				base.AddNameProperty( list );
		}

		public GateComponent( Point3D targetLoc, Map targetMap ) : base( targetLoc, targetMap )
		{
		}

		public GateComponent() : base()
		{
		}

		public GateComponent( StargateAddon gate ) : base( gate.Location, gate.Map )
		{
			m_Connected = gate != null;
			if ( m_Connected )
				m_ConnectedGate = gate;
		}

		public GateComponent( Serial serial ) : base( serial )
		{
		}

		public override void BeginConfirmation( Mobile from )
		{
			EndConfirmation( from );
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

			this.Delete();
		}
	}
}