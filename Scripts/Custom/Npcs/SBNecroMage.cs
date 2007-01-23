using System; 
using System.Collections; 
using Server.Items; 

namespace Server.Mobiles 
{ 
public class SBNecroMage : SBInfo 
{ 
private ArrayList m_BuyInfo = new InternalBuyInfo(); 
private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

public SBNecroMage() 
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
Add( new GenericBuyInfo( typeof( BatWing ), 3, 20, 0xF78, 0 ) ); 
Add( new GenericBuyInfo( typeof( GraveDust ), 3, 20, 0xF8F, 0 ) ); 
Add( new GenericBuyInfo( typeof( DaemonBlood ), 6, 20, 0xF7D, 0 ) ); 
Add( new GenericBuyInfo( typeof( NoxCrystal ), 6, 20, 0xF8E, 0 ) ); 
Add( new GenericBuyInfo( typeof( PigIron ), 5, 20, 0xF8A, 0 ) ); 

Add( new GenericBuyInfo( typeof( AnimateDeadScroll ), 52, 20, 0x2260, 0 ) );
Add( new GenericBuyInfo( typeof( BloodOathScroll ), 42, 20, 0x2261, 0 ) );
Add( new GenericBuyInfo( typeof( CorpseSkinScroll ), 32, 20, 0x2262, 0 ) );
Add( new GenericBuyInfo( typeof( CurseWeaponScroll ), 12, 20, 0x2263, 0 ) );
Add( new GenericBuyInfo( typeof( EvilOmenScroll ), 32, 20, 0x2264, 0 ) );
Add( new GenericBuyInfo( typeof( HorrificBeastScroll ), 52, 20, 0x2265, 0 ) );
Add( new GenericBuyInfo( typeof( LichFormScroll ), 82, 20, 0x2266, 0 ) );
Add( new GenericBuyInfo( typeof( MindRotScroll ), 42, 20, 0x2267, 0 ) );
Add( new GenericBuyInfo( typeof( PainSpikeScroll ), 32, 20, 0x2268, 0 ) );
Add( new GenericBuyInfo( typeof( PoisonStrikeScroll ), 62, 20, 0x2269, 0 ) );
Add( new GenericBuyInfo( typeof( StrangleScroll ), 72, 20, 0x226A, 0 ) );
Add( new GenericBuyInfo( typeof( SummonFamiliarScroll ), 82, 20, 0x226B, 0 ) );
Add( new GenericBuyInfo( typeof( VampiricEmbraceScroll ), 102, 20, 0x226C, 0 ) );
Add( new GenericBuyInfo( typeof( VengefulSpiritScroll ), 92, 20, 0x226D, 0 ) );
Add( new GenericBuyInfo( typeof( WitherScroll ), 72, 20, 0x226E, 0 ) );
Add( new GenericBuyInfo( typeof( WraithFormScroll ), 32, 20, 0x226F, 0 ) );
Add( new GenericBuyInfo( typeof( NecromancerSpellbook ), 115, 10, 0x2253, 0 ) ); 
} 

Add( new GenericBuyInfo( typeof( ScribesPen ), 8, 10, 0xFBF, 0 ) ); 
Add( new GenericBuyInfo( typeof( BlankScroll ), 5, 20, 0x0E34, 0 ) ); 
} 
} 

public class InternalSellInfo : GenericSellInfo 
{ 
public InternalSellInfo() 
{ 

if ( Core.AOS ) 
{ 
Add( typeof( BatWing ), 2 ); 
Add( typeof( GraveDust ), 2 ); 
Add( typeof( DaemonBlood ), 3 ); 
Add( typeof( NoxCrystal ), 3 ); 
Add( typeof( AnimateDeadScroll ), 25 );
Add( typeof( BloodOathScroll ), 25 ); 
Add( typeof( CorpseSkinScroll ), 25 ); 
Add( typeof( CurseWeaponScroll ), 25 ); 
Add( typeof( EvilOmenScroll ), 25 ); 
Add( typeof( HorrificBeastScroll ), 25 );
Add( typeof( LichFormScroll ), 25 ); 
Add( typeof( PainSpikeScroll ), 25 ); 
Add( typeof( StrangleScroll ), 25 ); 
Add( typeof( PoisonStrikeScroll ), 25 ); 
Add( typeof( SummonFamiliarScroll ), 25 ); 
Add( typeof( VampiricEmbraceScroll ), 25 ); 
Add( typeof( VengefulSpiritScroll ), 25 ); 
Add( typeof( WitherScroll ), 25 ); 
Add( typeof( WraithFormScroll ), 25 ); 
} 
} 
} 
} 
}