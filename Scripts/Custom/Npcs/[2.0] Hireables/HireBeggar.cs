using System; 
using System.Collections;
using System.Collections.Generic;
using Server.Items; 
using Server.ContextMenus; 
using Server.Misc; 
using Server.Network; 

namespace Server.Mobiles 
{ 
    public class HireBeggar : BaseHire 
    { 
        [Constructable] 
        public HireBeggar()
        { 
            SpeechHue = Utility.RandomDyedHue(); 
            Hue = Utility.RandomSkinHue(); 

            if ( this.Female = Utility.RandomBool() ) 
            { 
                Body = 0x191; 
                Name = NameList.RandomName( "female" ); 

		        switch ( Utility.Random ( 2 ) )
		        {
			      case 0: AddItem( new Skirt ( Utility.RandomNeutralHue() ) ); break;
			      case 1: AddItem( new Kilt ( Utility.RandomNeutralHue() ) ); break;
		        }
            }
             
            else 
            { 
                Body = 0x190; 
                Name = NameList.RandomName( "male" ); 
                AddItem( new ShortPants( Utility.RandomNeutralHue() ) ); 
            }
            
		     Title = "the beggar";
         	
			 Utility.AssignRandomHair( this );			
			
            SetStr( 86, 100 ); 
            SetDex( 81, 100 ); 
            SetInt( 81, 100 ); 

            SetDamage( 10, 23 ); 

		SetSkill( SkillName.Begging, 66, 97 ); 
		SetSkill( SkillName.Tactics, 55, 77 ); 
		SetSkill( SkillName.Wrestling, 55, 77 ); 
		SetSkill( SkillName.Magery, 55, 77 );		

            Fame = 0; 
            Karma = 0; 

            AddItem( new Sandals( Utility.RandomNeutralHue() ) ); 

		switch ( Utility.Random( 2 ) )
		{
			case 0: AddItem( new Doublet( Utility.RandomNeutralHue() ) ); break;
			case 1: AddItem( new Shirt( Utility.RandomNeutralHue() ) ); break;
		}
		
            PackGold( 0, 25 ); 
        
        }
        
	public override bool ClickTitle{ get{ return false; } }
        public HireBeggar( Serial serial ) : base( serial ) 
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
