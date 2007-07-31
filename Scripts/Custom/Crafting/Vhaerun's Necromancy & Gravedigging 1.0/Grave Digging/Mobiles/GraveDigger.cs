using System;
using System.Collections;
using Server;

namespace Server.Mobiles
{
	public class GraveDigger : BaseVendor
	{
		private ArrayList m_SBInfos = new ArrayList();
		protected override ArrayList SBInfos{ get { return m_SBInfos; } }

		[Constructable]
		public GraveDigger() : base( "the grave digger" )
		{
			SetSkill( SkillName.Mining, 50.0, 100.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBGraveDigger() );
		}

		public override void InitOutfit()
		{
			AddItem( new Server.Items.Shirt( Utility.RandomNeutralHue() ) );
			AddItem( new Server.Items.Boots() );
			AddItem( new Server.Items.Cloak( Utility.RandomGreenHue() ) );
			AddItem( new Server.Items.StrawHat() );
			AddItem( new Server.Items.LongPants( Utility.RandomBlueHue() ) );
		}

		public GraveDigger( Serial serial ) : base( serial )
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