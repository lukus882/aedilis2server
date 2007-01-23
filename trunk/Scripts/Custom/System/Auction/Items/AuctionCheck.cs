using System;

using Server;
using Server.Items;
using Server.Mobiles;

namespace Arya.Auction
{
	/// <summary>
	/// Defines various conditions that can apply when an auction is terminated
	/// </summary>
	public enum AuctionResult
	{
		/// <summary>
		/// The auction has been succesful and the item has been sold.
		/// </summary>
		Succesful,
		/// <summary>
		/// The auction ends and no bids have been made. The item will be returned to the owner.
		/// </summary>
		NoBids,
		/// <summary>
		/// The auction has ended, and there have been bids but the reserve hasn't been met.
		/// </summary>
		ReserveNotMet,
		/// <summary>
		/// A user has been outbid in an auction
		/// </summary>
		Outbid,
		/// <summary>
		/// The auction had pending status and both parts agreed to finalize the auction
		/// </summary>
		PendingRefused,
		/// <summary>
		/// The auction had pending status and at least one part decided to cancel the auction
		/// </summary>
		PendingAccepted,
		/// <summary>
		/// The pending period has timed out
		/// </summary>
		PendingTimedOut,
		/// <summary>
		/// The Auction System has been forced to stop and the auction ends unsuccesfully
		/// </summary>
		SystemStopped,
		/// <summary>
		/// The auctioned item has been deleted from the world
		/// </summary>
		ItemDeleted,
		/// <summary>
		/// The auction has been removed from the system by the staff
		/// </summary>
		StaffRemoved,
		/// <summary>
		/// The auction ended because a buyer used the buy now feature
		/// </summary>
		BuyNow
	}

	/// <summary>
	/// Base class for the auction system checks
	/// </summary>
	public abstract class AuctionCheck : Item
	{
		protected Guid m_Auction;
		protected string m_Message;
		protected string m_ItemName;
		protected Mobile m_Owner;

		/// <summary>
		/// Gets the message accompanying this check
		/// </summary>
		public string Message
		{
			get { return m_Message; }
		}

		/// <summary>
		/// Gets the auction that originated this check. This value might be null
		/// </summary>
		public AuctionItem Auction
		{
			get
			{
				return AuctionSystem.Find( m_Auction );
			}
		}

		/// <summary>
		/// Gets the html message used in gumps
		/// </summary>
		public string HtmlDetails
		{
			get { return string.Format( "<basefont color=#FFFFFF>{0}", m_Message ); }
		}

		/// <summary>
		/// Gets the name of the item returned by this check
		/// </summary>
		public abstract string ItemName
		{
			get;
		}

		public AuctionCheck() : base( 5360 )
		{
			LootType = LootType.Blessed;
		}

		#region Serialization

		public AuctionCheck( Serial serial ) : base ( serial )
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize (writer);

			writer.Write( 0 ); // Version

			writer.Write( m_Auction.ToString() );
			writer.Write( m_Message );
			writer.Write( m_ItemName );
			writer.Write( m_Owner );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize (reader);

			int version = reader.ReadInt();

			m_Auction = new Guid( reader.ReadString() );
			m_Message = reader.ReadString();
			m_ItemName = reader.ReadString();
			m_Owner = reader.ReadMobile();
		}

		#endregion

		/// <summary>
		/// Delivers the item carried by this check
		/// </summary>
		/// <param name="to">The mobile the check should be delivered to</param>
		/// <returns>True if the item has been delivered to the player's bank</returns>
		public virtual bool Deliver( Mobile to )
		{
			Item item = DeliveredItem;

			if ( item == null )
			{
				to.SendMessage( AuctionSystem.MessageHue, AuctionSystem.ST[ 116 ] );
				return false;
			}

			bool dropped = to.BankBox.TryDropItem( to, item, false );

			if ( dropped )
			{
				to.SendMessage( AuctionSystem.MessageHue, AuctionSystem.ST[ 117 ] );
				item.UpdateTotals();
				Delete();
			}
			else
			{
				to.SendMessage( AuctionSystem.MessageHue, AuctionSystem.ST[ 118 ] );
			}

			return dropped;
		}

		public override void OnDoubleClick(Mobile from)
		{
			bool ok = false;

			if ( from.AccessLevel > AccessLevel.Counselor )
			{
				from.SendMessage( AuctionSystem.MessageHue, AuctionSystem.ST[ 119 ] );
				ok = true;
			}
			else if ( from == m_Owner )
			{
				ok = true;
			}
			else
			{
				if ( m_Owner != null && m_Owner.Account != null && m_Owner.Account.Equals( from.Account ) )
				{
					ok = true;
				}
			}

			if ( ok )
			{
				if ( DeliveredItem is MobileStatuette )
				{
					// Send pet retrieval gump
					from.CloseGump( typeof( CreatureDeliveryGump ) );
					from.SendGump( new CreatureDeliveryGump( this ) );
				}
				else
				{
					// Send item retrieval gump
					from.CloseGump( typeof( AuctionDeliveryGump ) );
					from.SendGump( new AuctionDeliveryGump( this ) );
				}
			}
			else
			{
				from.SendMessage( AuctionSystem.MessageHue, AuctionSystem.ST[ 120 ] );
			}
		}

		/// <summary>
		/// Gets the item that should be delivered to the players bank
		/// </summary>
		public abstract Item DeliveredItem
		{
			get;
		}

		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties (list);

			list.Add( 1060658, "Message\t{0}", m_Message ); // ~1_val~: ~2_val~
		}
	}
}
