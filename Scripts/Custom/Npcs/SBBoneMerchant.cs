using System; 
using System.Collections; 
using Server.Items; 

namespace Server.Mobiles 
{ 
public class SBBoneArmorVendor : SBInfo 
{ 
private ArrayList m_BuyInfo = new InternalBuyInfo(); 
private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

public SBBoneArmorVendor() 
{ 
} 

public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
public override ArrayList BuyInfo { get { return m_BuyInfo; } } 

public class InternalBuyInfo : ArrayList 
{ 
public InternalBuyInfo() 
{ 
if ( Core.AOS ) 
{

Add( new GenericBuyInfo( typeof( BoneGloves ), 76, 20, 0x1450, 0 ) );
Add( new GenericBuyInfo( typeof( BoneHelm ), 54, 20, 0x1451, 0 ) );
Add( new GenericBuyInfo( typeof( BoneArms ), 64, 20, 0x144E, 0 ) );
Add( new GenericBuyInfo( typeof( BoneLegs ), 87, 20, 0x1452, 0 ) );
Add( new GenericBuyInfo( typeof( BoneChest ), 91, 20, 0x144F, 0 ) );
Add( new GenericBuyInfo( typeof( Bone ), 3, 20, 0xf7e, 0 ) );
}
} 
} 

public class InternalSellInfo : GenericSellInfo 
{ 
public InternalSellInfo()
{ 

if ( Core.AOS ) 
{ 
Add( typeof( BoneGloves ), 28 );
Add( typeof( BoneArms ), 24 );
Add( typeof( BoneHelm ), 21 );
Add( typeof( BoneLegs ), 31 );
Add( typeof( BoneChest ), 34 );
Add( typeof( Bone ), 2 );
} 
} 
} 
} 
}