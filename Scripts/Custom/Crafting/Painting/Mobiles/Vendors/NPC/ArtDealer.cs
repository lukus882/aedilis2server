/*
 created by:Bad Karma aka Broadside

  			        BBBBB        A      DDDDD  
  			       BB  BB      AAA     DDDDDD
  			      BB  BB     AA AA    DD   DD
 			     BBBBBB	   AA   AA   DD   DD
  		    	BBBBB     AAAAAAA   DD   DD
   		       BB  BB    AAAAAAA   DD   DD
   		      BB  BB    AA   AA   DDDDDD
		     BBBBBB    AA   AA   DDDDD

			        KK   KK       A      RRRRRR    MM   MM      A
			       KK  KK       AAA     RR   RR   MMM MMM     AAA
			      KK KK       AA AA    RR   RR   MMMMMMM    AA AA
			     KKKK       AA   AA   RRRRRR    MM M MM   AA   AA
			    KKKK       AAAAAAA   RRRR      MM   MM   AAAAAAA
			   KK KK      AAAAAAA   RR RR     MM   MM   AAAAAAA
			  KK   KK    AA   AA   RR  RR    MM   MM   AA   AA
			 KK     KK  AA   AA   RR   RR   MM   MM   AA   AA
 */
using System;
using System.Collections;
using Server;
using Server.Engines.BulkOrders;

namespace Server.Mobiles
{
	public class ArtDealer : BaseVendor
	{
		private ArrayList m_SBInfos = new ArrayList();
		protected override ArrayList SBInfos{ get { return m_SBInfos; } }

		[Constructable]
		public ArtDealer() : base( "the Art Dealer" )
		{
            SetSkill(SkillName.Tailoring, 65.0, 88.0);
            SetSkill(SkillName.Cooking, 90.0, 100.0);
            SetSkill(SkillName.Inscribe, 90.0, 100.0);
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBArtDealer() );
		}

		public override VendorShoeType ShoeType
		{
			get{ return VendorShoeType.Sandals; }
		}

        public ArtDealer(Serial serial)
            : base(serial)
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