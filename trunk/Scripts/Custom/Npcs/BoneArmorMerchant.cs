using System; 
using System.Collections; 
using Server; 

namespace Server.Mobiles 
{ 
public class BoneArmorVendor : BaseVendor
{ 
private ArrayList m_SBInfos = new ArrayList(); 
protected override ArrayList SBInfos{ get { return m_SBInfos; } } 

[Constructable] 
public BoneArmorVendor() : base( "the Bone Merchant" ) 
{ 
CantWalk=true;
} 

public override void InitSBInfo() 
{ 
m_SBInfos.Add( new SBBoneArmorVendor() ); 
} 

public override VendorShoeType ShoeType 
{ 
get{ return Utility.RandomBool() ? VendorShoeType.Shoes : VendorShoeType.Sandals; } 
} 

public override void InitOutfit() 
{ 
base.InitOutfit(); 

AddItem( new Server.Items.BoneChest() );
AddItem( new Server.Items.BoneLegs() );
AddItem( new Server.Items.BoneArms() );
AddItem( new Server.Items.BoneHelm() );
AddItem( new Server.Items.BoneGloves() );
} 

public BoneArmorVendor( Serial serial ) : base( serial ) 
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