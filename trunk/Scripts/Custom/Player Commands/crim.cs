// Created by adobab to understand for player criminal or not. (Especially for Player Killers)
// 06 July 2004
using System; 
using Server; 
using Server.Mobiles; 

namespace Server.Commands
{ 
  
  public class crim 
  { 
   public static void Initialize() 
   { 
     CommandSystem.Register( "criminal", AccessLevel.Player, new CommandEventHandler( crim_OnCommand ) ); 
   } 

   [Usage( "criminal" )] 
   public static void crim_OnCommand( CommandEventArgs e ) 
   { 
Mobile from = e.Mobile;
    
    if (from.Criminal) 
    { 
      from.SendMessage("Yes sir or madam you are a criminal !!"); 
      return; 
    } 
else
      from.SendMessage("No sir or madam you are not a criminal"); 
      return; 

   } 
  } 
} 
