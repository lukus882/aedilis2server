using System;

namespace Server.Items
{
	public class GraveItem : Item
	{
      		public override bool ForceShowProperties{ get{ return ObjectPropertyList.Enabled; } }

		//Grave ItemIDs
		public static int[] m_GraveID = new int[]
		{
			4550, 4551, 3088, 3089, 3090, 3091, 3092, 3093, 3097, 3098, 3099, 3100, 3101, 3102, 
			3108, 3109, 3811, 3812, 3813, 3814, 4495, 4497, 4596, 4597, 4598, 4599, 4600, 4601, 
			4602, 4603, 6421, 7712, 1065, 1056, 2462, 2518, 3773, 3774, 3775, 3776, 4775, 4776
		};

		public GraveItem() : base( 1 )
		{
			ItemID = m_GraveID[Utility.Random(m_GraveID.Length)];
			Weight = 10;
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1049644, "Stolen from a grave" );
		}

		public GraveItem( Serial serial ) : base( serial )
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