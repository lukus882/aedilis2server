using System;
using Server;
using Server.Mobiles;

namespace Server.Engines.Quests.EndlessPerlQuest
{
	public class FindGraniteObjective : QuestObjective
	{
		public override object Message
		{
			get
			{
				return "<U>Ancient Granite</U> Only Known place to get this is dartmoor ponies out in the mountain pass near shame.";
			}
		}

		public FindGraniteObjective()
		{
		}

		public override void OnComplete()
		{
			System.AddObjective( new ReturnToClericObjective() );
		}
	}

	public class ReturnToClericObjective : QuestObjective
	{
		public override object Message
		{
			get
			{
				return "Return to the Cleric, and give him Ancient Granite to collect your reward.";
			}
		}

		public ReturnToClericObjective()
		{
		}
	}

	public class MakeRoomObjective : QuestObjective
	{
		public override object Message
		{
			get
			{
				return "Your backpack is to full to carry any more items. Please clean it out before you return here to collect your reward.";
			}
		}

		public MakeRoomObjective()
		{
		}
	}
}