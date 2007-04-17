using System;
using System.Collections;
using Server;
using Server.Engines.BulkOrders;

namespace Server.Mobiles
{
	public class UndeadStichingMistress : BaseVendor
	{
		private ArrayList m_SBInfos = new ArrayList();
		protected override ArrayList SBInfos{ get { return m_SBInfos; } }

		[Constructable]
		public UndeadStichingMistress() : base( "the undead stiching mistress" )
		{
			SetSkill( SkillName.Tailoring, 64.0, 100.0 );
			BodyValue = 50;
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBStichingMistress() );
		}

		public UndeadStichingMistress( Serial serial ) : base( serial )
		{
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
		}
	}
}