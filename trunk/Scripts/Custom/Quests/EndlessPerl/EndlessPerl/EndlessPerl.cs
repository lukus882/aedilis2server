using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests.EndlessPerlQuest
{
	public class EndlessPerl : QuestSystem
	{
		private static Type[] m_TypeReferenceTable = new Type[]
			{
				typeof( EndlessPerlQuest.DontOfferConversation ),
				typeof( EndlessPerlQuest.DeclineConversation ),
				typeof( EndlessPerlQuest.AcceptConversation ),
				typeof( EndlessPerlQuest.DuringCollectingConversation ),
				typeof( EndlessPerlQuest.EndConversation ),
				typeof( EndlessPerlQuest.FullEndConversation ),
				typeof( EndlessPerlQuest.FindGraniteObjective ),
				typeof( EndlessPerlQuest.ReturnToClericObjective ),
				typeof( EndlessPerlQuest.MakeRoomObjective )
			};

		public override Type[] TypeReferenceTable{ get{ return m_TypeReferenceTable; } }

		public override object Name
		{
			get
			{
				return "The Quest of Endless Peril";
			}
		}

		public override object OfferMessage
		{
			get
			{
				return "<U>The Cleric starts to speak.</U><BR><BR>So the power of the church compels you? I am indeed the most fevorous Cleric in the land. Ah, you look at my shield in such a way! So you wish one of these shields for yourself do you? I have for you a VERY dangerous yet rewarding endeavor.<BR><BR><I>Noriar looks at you and smiles...</I><BR><BR>Well my child do you wish to take this Task on?";
			}
		}

		public override TimeSpan RestartDelay{ get{ return TimeSpan.FromMinutes( 30.0 ); } }
		public override bool IsTutorial{ get{ return false; } }

		public override int Picture{ get{ return 0x15A9; } }

		public EndlessPerl( PlayerMobile from ) : base( from )
		{
		}

		// Serialization
		public EndlessPerl()
		{
		}

		public override void Accept()
		{
			base.Accept();

			AddConversation( new AcceptConversation() );
		}

		public override void Decline()
		{
			base.Decline();

			AddConversation( new DeclineConversation() );
		}
	}
}