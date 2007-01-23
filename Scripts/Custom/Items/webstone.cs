// created on 9/18/2003 at 12:00 AM 
using System; 
using Server; 

namespace Server.Items 
{ 
   public class EWebStone : Item 
   { 
      private string m_Website; 
       
      [CommandProperty( AccessLevel.GameMaster )] 
      public string Website 
      { 
         get{ return m_Website; } 
         set{ m_Website = value; } 
      } 
       
      [Constructable] 
      public EWebStone() : base( 0xED4 ) 
      { 
         Movable = false; 
         Name = "Web Stone"; 
      } 
       
      public override void OnDoubleClick( Mobile from ) 
      { 
         from.LaunchBrowser( Website ); 
      } 
       
      public EWebStone( Serial serial ) : base( serial ) 
      { 
          
      } 
       
      public override void Serialize( GenericWriter writer ) 
      { 
         base.Serialize( writer ); 
         writer.Write( (int) 1 ); // version 
         writer.Write( (string) m_Website ); 
      } 
       
      public override void Deserialize( GenericReader reader ) 
      { 
         base.Deserialize( reader ); 
         int version = reader.ReadInt(); 
          
         m_Website = reader.ReadString(); 
      } 
   } 
} 
