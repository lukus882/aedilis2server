/*
***********************************
*************Created By************
***********************************
**************Broadside ***********
***********************************
****************AKA****************
***********************************
**************Bad Karma************
***********************************
 */
using System;
using Server;

namespace Server.Items
{
  public class MysticTunic : LeatherChest 
   {
       public override int ArtifactRarity { get { return 50; } } 						
       public override int BasePhysicalResistance { get { return 14; } } 						  
       public override int BaseFireResistance{ get{ return 16; } }
	   public override int BaseColdResistance{ get{ return 15; } }
	   public override int BasePoisonResistance{ get{ return 15; } }
	   public override int BaseEnergyResistance{ get{ return 15; } }

      		public override int InitMinHits{ get{ return 255; } } 
      		public override int InitMaxHits{ get{ return 255; } }
      [Constructable]
      public MysticTunic()
      {
          Weight = 6.0;
          Name = "Mystic Tunic";
          Hue = 302;

          Attributes.DefendChance = 7;
          Attributes.SpellDamage = 5;
          Attributes.NightSight = 5;

          Attributes.Luck = 5;

          switch (Utility.Random(38))
          {
              case 0: SkillBonuses.SetValues(0, SkillName.Swords, 5.0); break;
              case 1: SkillBonuses.SetValues(0, SkillName.Alchemy, 5.0); break;
              case 2: SkillBonuses.SetValues(0, SkillName.Anatomy, 5.0); break;
              case 3: SkillBonuses.SetValues(0, SkillName.AnimalLore, 5.0); break;
              case 4: SkillBonuses.SetValues(0, SkillName.Parry, 5.0); break;
              case 5: SkillBonuses.SetValues(0, SkillName.Blacksmith, 5.0); break;
              case 6: SkillBonuses.SetValues(0, SkillName.Fletching, 5.0); break;
              case 7: SkillBonuses.SetValues(0, SkillName.Peacemaking, 5.0); break;
              case 8: SkillBonuses.SetValues(0, SkillName.Carpentry, 5.0); break;
              case 9: SkillBonuses.SetValues(0, SkillName.Discordance, 5.0); break;
              case 10: SkillBonuses.SetValues(0, SkillName.EvalInt, 5.0); break;
              case 11: SkillBonuses.SetValues(0, SkillName.Healing, 5.0); break;
              case 12: SkillBonuses.SetValues(0, SkillName.Fishing, 5.0); break;
              case 13: SkillBonuses.SetValues(0, SkillName.Provocation, 5.0); break;
              case 14: SkillBonuses.SetValues(0, SkillName.Inscribe, 5.0); break;
              case 15: SkillBonuses.SetValues(0, SkillName.Magery, 5.0); break;
              case 16: SkillBonuses.SetValues(0, SkillName.MagicResist, 5.0); break;
              case 17: SkillBonuses.SetValues(0, SkillName.Tactics, 5.0); break;
              case 18: SkillBonuses.SetValues(0, SkillName.Musicianship, 5.0); break;
              case 19: SkillBonuses.SetValues(0, SkillName.Archery, 5.0); break;
              case 20: SkillBonuses.SetValues(0, SkillName.SpiritSpeak, 5.0); break;
              case 21: SkillBonuses.SetValues(0, SkillName.Stealing, 5.0); break;
              case 22: SkillBonuses.SetValues(0, SkillName.Tailoring, 5.0); break;
              case 23: SkillBonuses.SetValues(0, SkillName.AnimalTaming, 5.0); break;
              case 24: SkillBonuses.SetValues(0, SkillName.Tinkering, 5.0); break;
              case 25: SkillBonuses.SetValues(0, SkillName.Veterinary, 5.0); break;
              case 26: SkillBonuses.SetValues(0, SkillName.Macing, 5.0); break;
              case 27: SkillBonuses.SetValues(0, SkillName.Fencing, 5.0); break;
              case 28: SkillBonuses.SetValues(0, SkillName.Wrestling, 5.0); break;
              case 29: SkillBonuses.SetValues(0, SkillName.Lumberjacking, 5.0); break;
              case 30: SkillBonuses.SetValues(0, SkillName.Mining, 5.0); break;
              case 31: SkillBonuses.SetValues(0, SkillName.Meditation, 5.0); break;
              case 32: SkillBonuses.SetValues(0, SkillName.Stealth, 5.0); break;
              case 33: SkillBonuses.SetValues(0, SkillName.Necromancy, 5.0); break;
              case 34: SkillBonuses.SetValues(0, SkillName.Focus, 5.0); break;
              case 35: SkillBonuses.SetValues(0, SkillName.Chivalry, 5.0); break;
              case 36: SkillBonuses.SetValues(0, SkillName.Bushido, 5.0); break;
              case 37: SkillBonuses.SetValues(0, SkillName.Ninjitsu, 5.0); break;
          }
      }

      public MysticTunic(Serial serial)
          : base(serial) 
      { 
      } 

      public override void Serialize( GenericWriter writer ) 
      { 
         base.Serialize( writer ); 

         writer.Write( (int) 0 ); 
      } 
        
      public override void Deserialize(GenericReader reader) 
      { 
         base.Deserialize( reader ); 

         int version = reader.ReadInt(); 
      }
		public override void OnDoubleClick(Mobile from)
		{
			if (from.Backpack != null && IsChildOf(from.Backpack))
			{
				if (this.ItemID == 5069) this.ItemID = 5198;
				else if (this.ItemID == 5198) this.ItemID = 10112;
				else if (this.ItemID == 10112) this.ItemID = 5136;
				else if (this.ItemID == 5136) this.ItemID = 5069;
			}
			else
			{
				from.SendMessage("You must have the item in your pack to morph it.");
			}
		}
   } 
} 


