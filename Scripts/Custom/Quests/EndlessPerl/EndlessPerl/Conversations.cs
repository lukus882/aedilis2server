using System;
using Server;

namespace Server.Engines.Quests.EndlessPerlQuest
{
	public class DontOfferConversation : QuestConversation
	{
		public override object Message
		{
			get
			{
				return "I am busy at the moment, Could you come back later?";
			}
		}

		public override bool Logged{ get{ return false; } }

		public DontOfferConversation()
		{
		}
	}

	public class DeclineConversation : QuestConversation
	{
		public override object Message
		{
			get
			{
				return "<I>That Cleric looks at you</I><BR><BR>Okay young child we are not forcing you into something you dont want to do.";
			}
		}

		public override bool Logged{ get{ return false; } }

		public DeclineConversation()
		{
		}
	}

	public class AcceptConversation : QuestConversation
	{
		public override object Message
		{
			get
			{
				return "<I>Noriar looks at you and smiles...</I><BR><BR>Ok great! here is a list of what materials I require they are very rare.<BR><BR><I>The list the Cleric handed you.</I><BR><BR><U>Ancient Granite</U><BR>Only Known place to get this is dartmoor ponies out in the mountain pass near shame.<BR><BR>Should you retrieve 35 of these shards - I shall consider you worthy and will craft you a shield.";
			}
		}

		public AcceptConversation()
		{
		}

		public override void OnRead()
		{
			System.AddObjective( new FindGraniteObjective() );
		}
	}

	public class DuringCollectingConversation : QuestConversation
	{
		public override object Message
		{
			get
			{
				return "<I>The Cleric turns around as you tap his shoulder</I><BR><BR>Hey! your back so soon, Great great! where is it??? huh? You don't have it. well i cannot craft your shield my child untell you have what i need!";
			}
		}

		public override bool Logged{ get{ return false; } }

		public DuringCollectingConversation()
		{
		}
	}

	public class EndConversation : QuestConversation
	{
		public override object Message
		{
			get
			{
				return "<I>Well done, you have collected all of the items I requested for the Shield.</I><BR><BR>Here it is, May it serve you well.";
			}
		}

		public EndConversation()
		{
		}

		public override void OnRead()
		{
			System.Complete();
		}
	}

	public class FullEndConversation : QuestConversation
	{
		private bool m_Logged;

		public override object Message
		{
			get
			{
				return "<I>The old man looks at your backpack</I><BR><BR>Where is it? How could you find anything in that mess. Go clean out your backpack and bring me back my drink.. err Medicine.";
			}
		}

		public override bool Logged{ get{ return m_Logged; } }

		public FullEndConversation( bool logged )
		{
			m_Logged = logged;
		}

		public FullEndConversation()
		{
			m_Logged = true;
		}

		public override void OnRead()
		{
			if ( m_Logged )
				System.AddObjective( new MakeRoomObjective() );
		}
	}
}