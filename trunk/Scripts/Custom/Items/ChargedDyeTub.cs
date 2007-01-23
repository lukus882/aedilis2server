using System;
using System.Text;
using System.Collections;
using Server;
using Server.Multis;
using Server.Targeting;
using Server.Items;
using Server.Gumps;
using Server.Network;
using Server.Mobiles;

namespace Server.Items
{
        
    public class ChargedDyeTub : Item
	{
		private bool m_Redyable;
		private int m_DyedHue;
        private int m_Charges;
        private bool m_AllowRunebooks;
        private bool m_AllowFurniture;
        private bool m_AllowStatuettes;
        private bool m_AllowLeather;
        private bool m_AllowDyables;
        private bool m_AllowSpellbooks;
    
		public virtual CustomHuePicker CustomHuePicker{ get{ return null; } }
                
        [CommandProperty( AccessLevel.GameMaster )]
		public bool Redyable
		{
			get{return m_Redyable;}
			set{m_Redyable = value;}
		}

        [CommandProperty(AccessLevel.GameMaster)]
        public int DyedHue
        {
            get{return m_DyedHue;}
            set{if (m_Redyable)
                  {
                    m_DyedHue = value;
                    Hue = value;
                  }
               }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Charges
        {
            get { return m_Charges; }
            set { m_Charges = value; InvalidateProperties(); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool AllowRunebooks
        {
            get { return m_AllowRunebooks; }
            set { m_AllowRunebooks = value; InvalidateProperties(); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool AllowFurniture
        {
            get { return m_AllowFurniture; }
            set { m_AllowFurniture = value; InvalidateProperties(); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool AllowStatuettes
        {
            get { return m_AllowStatuettes; }
            set { m_AllowStatuettes = value; InvalidateProperties(); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool AllowLeather
        {
            get { return m_AllowLeather; }
            set { m_AllowLeather = value; InvalidateProperties(); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool AllowDyables
        {
            get { return m_AllowDyables; }
            set { m_AllowDyables = value; InvalidateProperties(); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool AllowSpellbooks
        {
            get { return m_AllowSpellbooks; }
            set { m_AllowSpellbooks = value; InvalidateProperties(); }
        }

		[Constructable] 
		public ChargedDyeTub() : base( 0xFAB )
		{
			Weight = 10.0;
			m_Redyable = false;
            m_Charges = 50;
            m_AllowRunebooks = false;
            m_AllowFurniture = false;
            m_AllowStatuettes = false;
            m_AllowLeather = false;
            m_AllowDyables = true;
            m_AllowSpellbooks = false;
		}


		public ChargedDyeTub( Serial serial ) : base( serial )
		{
		}
		
		// You can not dye that.
		public virtual int FailMessage{ get{ return 1042083; } }


        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            list.Add(1060584, m_Charges.ToString()); // uses remaining: ~1_val~

            list.Add("Dyes:{0}{1}{2}{3}{4}{5}", (m_AllowSpellbooks ? " Spellbooks" : ""), (m_AllowRunebooks ? " Runebooks" : ""), (m_AllowDyables ? " Clothing" : ""), (m_AllowLeather ? " Leather" : ""), (m_AllowFurniture ? " Furniture" : ""), (m_AllowStatuettes ? " Statuettes" : ""));

        }

        public override void OnDoubleClick(Mobile from)
        {
            if (Charges == 0)
                from.SendLocalizedMessage(1019073); // This item is out of charges.
            else
            {
                if (from.InRange(this.GetWorldLocation(), 1))
                {
                    from.SendMessage("Select item to dye.");
                    from.Target = new InternalTarget(this, Charges);
                }
                else
                {
                    from.SendLocalizedMessage(500446); // That is too far away.
                }
            }
        }

        private class InternalTarget : Target
        {
            private ChargedDyeTub m_Tub;
            private int m_Charges;

            public InternalTarget(ChargedDyeTub tub, int Charges) : base(1, false, TargetFlags.None)
            {
                m_Tub = tub;
                m_Charges = Charges;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is Item)
                {
                    Item item = (Item)targeted;

                    if (item is IDyable && m_Tub.AllowDyables)
                    {
                        if (!from.InRange(m_Tub.GetWorldLocation(), 1) || !from.InRange(item.GetWorldLocation(), 1))
                            from.SendLocalizedMessage(500446); // That is too far away.
                        else if (item.Parent is Mobile)
                            from.SendLocalizedMessage(500861); // Can't Dye clothing that is being worn.
                        else
                        {
                            from.PlaySound(0x23E);
                            item.Hue = m_Tub.DyedHue;
                            m_Tub.Charges -= 1;
                            if (m_Tub.Charges == 0)
                                from.SendLocalizedMessage(1019073); // This item is out of charges.
                        }
                    }
                    else if ((FurnitureAttribute.Check(item) || (item is PotionKeg)) && m_Tub.AllowFurniture)
                    {
                        if (!from.InRange(m_Tub.GetWorldLocation(), 1) || !from.InRange(item.GetWorldLocation(), 1))
                        {
                            from.SendLocalizedMessage(500446); // That is too far away.
                        }
                        else
                        {
                            bool okay = (item.IsChildOf(from.Backpack));

                            if (!okay)
                            {
                                if (item.Parent == null)
                                {
                                    BaseHouse house = BaseHouse.FindHouseAt(item);

                                    if (house == null || !house.IsLockedDown(item))
                                        from.SendLocalizedMessage(501022); // Furniture must be locked down to paint it.
                                    else if (!house.IsCoOwner(from))
                                        from.SendLocalizedMessage(501023); // You must be the owner to use this item.
                                    else
                                        okay = true;
                                }
                                else
                                {
                                    from.SendLocalizedMessage(1048135); // The furniture must be in your backpack to be painted.
                                }
                            }

                            if (okay)
                            {
                                from.PlaySound(0x23E);
                                item.Hue = m_Tub.DyedHue;
                                m_Tub.Charges -= 1;
                                if (m_Tub.Charges == 0)
                                    from.SendLocalizedMessage(1019073); // This item is out of charges.
                            }
                        }
                    }
                    else if ((item is Runebook || item is RecallRune) && m_Tub.AllowRunebooks)
                    {
                        if (!from.InRange(m_Tub.GetWorldLocation(), 1) || !from.InRange(item.GetWorldLocation(), 1))
                        {
                            from.SendLocalizedMessage(500446); // That is too far away.
                        }
                        else if (!item.Movable)
                        {
                            from.SendLocalizedMessage(1049776); // You cannot dye runes or runebooks that are locked down.
                        }
                        else
                        {
                            from.PlaySound(0x23E);
                            item.Hue = m_Tub.DyedHue;
                            m_Tub.Charges -= 1;
                            if (m_Tub.Charges == 0)
                                from.SendLocalizedMessage(1019073); // This item is out of charges.
                        }
                    }
                    else if ((item is Spellbook) && m_Tub.AllowSpellbooks)
                    {
                        if (!from.InRange(m_Tub.GetWorldLocation(), 1) || !from.InRange(item.GetWorldLocation(), 1))
                        {
                            from.SendLocalizedMessage(500446); // That is too far away.
                        }
                        else if (!item.Movable)
                        {
                            from.SendMessage("You cannot dye spellbooks that are locked down."); // You cannot dye runes or runebooks that are locked down.
                        }
                        else
                        {
                            from.PlaySound(0x23E);
                            item.Hue = m_Tub.DyedHue;
                            m_Tub.Charges -= 1;
                            if (m_Tub.Charges == 0)
                                from.SendLocalizedMessage(1019073); // This item is out of charges.
                        }
                    }
                    else if (item is MonsterStatuette && m_Tub.AllowStatuettes)
                    {
                        if (!from.InRange(m_Tub.GetWorldLocation(), 1) || !from.InRange(item.GetWorldLocation(), 1))
                        {
                            from.SendLocalizedMessage(500446); // That is too far away.
                        }
                        else if (!item.Movable)
                        {
                            from.SendLocalizedMessage(1049779); // You cannot dye statuettes that are locked down.
                        }
                        else
                        {
                            from.PlaySound(0x23E);
                            item.Hue = m_Tub.DyedHue;
                            m_Tub.Charges -= 1;
                            if (m_Tub.Charges == 0)
                                from.SendLocalizedMessage(1019073); // This item is out of charges.
                        }
                    }
                    else if ((item is BaseArmor && (((BaseArmor)item).MaterialType == ArmorMaterialType.Leather || ((BaseArmor)item).MaterialType == ArmorMaterialType.Studded)) && m_Tub.AllowLeather)
                    {
                        if (!from.InRange(m_Tub.GetWorldLocation(), 1) || !from.InRange(item.GetWorldLocation(), 1))
                        {
                            from.SendLocalizedMessage(500446); // That is too far away.
                        }
                        else if (!item.Movable)
                        {
                            from.SendLocalizedMessage(1042419); // You may not dye leather items which are locked down.
                        }
                        else if (item.Parent is Mobile)
                        {
                            from.SendLocalizedMessage(500861); // Can't Dye clothing that is being worn.
                        }
                        else
                        {
                            from.PlaySound(0x23E);
                            item.Hue = m_Tub.DyedHue;
                            m_Tub.Charges -= 1;
                            if (m_Tub.Charges == 0)
                                from.SendLocalizedMessage(1019073); // This item is out of charges.
                        }
                    }
                    else
                    {
                        from.SendLocalizedMessage(m_Tub.FailMessage);
                    }
                }
                else
                {
                    from.SendLocalizedMessage(m_Tub.FailMessage);
                }
            }
        }

       public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 1); // version

            writer.Write((bool)m_Redyable);
            writer.Write((int)m_DyedHue);

            writer.Write((int)m_Charges);
            writer.Write((bool)m_AllowRunebooks);
            writer.Write((bool)m_AllowFurniture);
            writer.Write((bool)m_AllowStatuettes);
            writer.Write((bool)m_AllowLeather);
            writer.Write((bool)m_AllowDyables);
            writer.Write((bool)m_AllowSpellbooks);


        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 1:
                    {
                        m_Redyable = reader.ReadBool();
                        m_DyedHue = reader.ReadInt();
                        goto case 0;
                    }
                case 0:
                    {
                        m_Charges = reader.ReadInt();
                        m_AllowRunebooks = reader.ReadBool();
                        m_AllowFurniture = reader.ReadBool();
                        m_AllowStatuettes = reader.ReadBool();
                        m_AllowLeather = reader.ReadBool();
                        m_AllowDyables = reader.ReadBool();
                        m_AllowSpellbooks = reader.ReadBool();
                        break;
                    }
            }
        }
	}

   
}
    