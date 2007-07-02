using System; 
using System.Collections; 
using Server; 
using Server.Mobiles; 
using Server.Items; 
using Server.Engines.Craft; 
using Server.Network; 
using Server.Targeting; 

namespace Server.Items 
{ 
   public class LeatherShaper : Item, IUsesRemaining 
   { 
      private int m_UsesRemaining; 

      [CommandProperty( AccessLevel.GameMaster )] 
      public int UsesRemaining 
      { 
         get { return m_UsesRemaining; } 
         set { m_UsesRemaining = value; InvalidateProperties(); } 
      } 

      public bool ShowUsesRemaining{ get{ return true; } set{} } 
       
      [Constructable] 
      public LeatherShaper() : this( 50 ) 
      { 
      } 
       
      [Constructable] 
      public LeatherShaper( int charges ) : base(0x1096) 
      { 
         Weight = 2.0; 
         Stackable = false;
	 Hue = 738; 
         Name = "Leather Shaper"; 
         UsesRemaining = charges; 
      } 
       
      public override void OnDoubleClick( Mobile from ) 
      { 
         if ( (from.Skills[SkillName.Inscribe].Value <= 90.0 ) ) 
         { 
            from.SendMessage( "You don't know how to use that."); 
         } 
         else if ( !IsChildOf( from.Backpack ) ) 
         { 
            from.SendMessage( "The LeatherShaper must be in your backpack to use it." ); 
         } 
         else if ( from.Target is InternalTarget ) 
         { 
            from.SendMessage( "This LeatherShaper is already being used." ); 
         } 
         else 
         { 
            from.Target = new InternalTarget( this ); 
         } 
      } 
       
      public override void GetProperties( ObjectPropertyList list ) 
      { 
         base.GetProperties( list ); 

         list.Add( 1060584, m_UsesRemaining.ToString() ); // uses remaining: ~1_val~ 
      } 
       
      public virtual void DisplayDurabilityTo( Mobile m ) 
      { 
         LabelToAffix( m, 1017323, AffixType.Append, ": " + m_UsesRemaining.ToString() ); // Durability 
      } 

      public override void OnSingleClick( Mobile from ) 
      { 
         DisplayDurabilityTo( from ); 

         base.OnSingleClick( from ); 
      } 
       
       
          
      private class InternalTarget : Target 
      { 
         private LeatherShaper m_LeatherShaper; 
          

         public InternalTarget( LeatherShaper LeatherShaper ) : base( 1, false, TargetFlags.None ) 
         { 
            m_LeatherShaper = LeatherShaper; 
         } 
          
         protected override void OnTarget( Mobile from, object targeted ) 
         { 
            if ( m_LeatherShaper.Deleted ) 
               return; 
             
            if ( targeted is Item ) 
            { 
               Item m = targeted as Item; 
               if ( !m.IsChildOf (from.Backpack)) 
               { 
                  from.SendMessage( "The leather mus be in your pack!" ); 
               } 
               else if ( m is BaseHides || m is BaseLeather  ) 
               { 
                  double inscription = from.Skills[SkillName.Inscribe].Value;
                  double chance = ((inscription) / 100.0);
                  double reussite = Utility.RandomDouble();
                  
                  if ( chance >= reussite ) 
                  { 
                     BlankScroll cblankscroll = new BlankScroll(); 
                     cblankscroll.Amount = m.Amount;
		     cblankscroll.Hue = 738;
                     from.AddToBackpack( cblankscroll ); 
                     m.Delete(); 
                     from.PlaySound( 583 );
                      
                     --m_LeatherShaper.UsesRemaining; 
                     if ( m_LeatherShaper.UsesRemaining <= 0 ) 
                     { 
                        from.SendMessage( "Your pumise break in two parts." ); 
                        m_LeatherShaper.Delete(); 
                     } 
                  } 
                  else 
                  { 
                        if ( m.Amount >= 3 )
                             m.Amount = m.Amount - 2 ; 
                        else
                             m.Consume();
                                      
                        from.SendMessage( "You fail to create scroll and you loose some hide." ); 
                        --m_LeatherShaper.UsesRemaining; 
                        
                        if ( m_LeatherShaper.UsesRemaining <= 0 ) 
                        { 
                           from.SendMessage( "Your pumise break in two parts." ); 
                           m_LeatherShaper.Delete(); 
                        }

                     } 
               } 
               else 
               { 
                  from.SendMessage( "You cannot create scroll from that." ); 
               } 
            } 
         } 
      } 
       
      public LeatherShaper(Serial serial) : base(serial) 
      { 
      } 
       
      public override void Serialize( GenericWriter writer ) 
      { 
         base.Serialize( writer ); 

         writer.Write( (int) 0 ); // version 
         writer.Write( (int) m_UsesRemaining ); 
      } 

      public override void Deserialize( GenericReader reader ) 
      { 
         base.Deserialize( reader ); 

         int version = reader.ReadInt(); 
          
         switch ( version ) 
         { 
            case 0: 
            { 
               m_UsesRemaining = reader.ReadInt(); 
               break; 
            } 
         } 
      } 
   } 
} 
