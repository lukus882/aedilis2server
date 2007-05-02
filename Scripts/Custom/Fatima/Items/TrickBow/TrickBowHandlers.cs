using System;
using System.Collections.Generic;
using Server;
using Server.Targeting;
using Server.Mobiles;
using Server.Items;

namespace Fatima.Items
{
	public enum ArrowReq : byte
	{
		NotUsable = 0,
		Usable = 1,
		NoReq = 2
	};

	public class SelectTrickArrowTypeContextMenu : Server.ContextMenus.ContextMenuEntry
	{
		private Bow m_Bow;

		public override bool NonLocalUse{ get{ return false; } }
		public SelectTrickArrowTypeContextMenu( Bow bow ) : base( 97, 12 ) //"Setup"
		{
			m_Bow = bow;
		}

		public override void OnClick()
		{
			Owner.From.SendMessage("Select the Trick Arrow type you wish to fire primarily.");
			Owner.From.Target = new InternalTarget(m_Bow);
		}

		private class InternalTarget : Target
		{
			private Bow m_Bow;

			public InternalTarget( Bow bow ) : base( 12, false, TargetFlags.None )
			{
				m_Bow = bow;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if (o == null)
					return;

				if ( o != null && o is TrickArrow )
				{
					m_Bow.ArmDifferentAmmo( (TrickArrow)o, from );
				}
				else
					from.SendMessage("That is not a valid special arrow!");
			}
		}
	}

	public class ClearTrickArrowTypeContextMenu : Server.ContextMenus.ContextMenuEntry
	{
		private Bow m_Bow;

		public override bool NonLocalUse{ get{ return false; } }

		public ClearTrickArrowTypeContextMenu( Bow bow ) : base( 154, 12 ) //"Clear"
		{
			m_Bow = bow;
		}

		public override void OnClick()
		{
			Owner.From.SendMessage("Your bow will now fire regular arrows.");
			m_Bow.ClearTrickAmmo(); //reset
		}
	}

	public interface ITrickBow
	{
		bool ArmDifferentAmmo( TrickArrow arrow, Mobile from );
	};
}