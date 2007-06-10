/*
 * Simple Statue System 
 * by Kleanthes@eurebia.net
 * inspired by the original Statue Maker System (Ultima Online, VetReward)
 * 
 * Version: 0.51 08/24/06   
 *
 * This system allows to place a human npc as a statue that will be
 * frozen in one position, so that it will not walk away or make any
 * animations. For this to work, a timer (one timer for alle statues
 * toegether) ticks every 10 seconds and updates all statues.
 * The animation that the statues do has two "tricks":
 * - The delay of the animation is 255, so that every frame of the
 *   animation will take more than 10 seconds. As the animation is 
 *   started again every 10 seconds, this will result in only one 
 *   frame being displayed all the time. 
 * - Some animations are shown backwards with a non standard number
 *   of frames. For example:
 *   If you show animation 16 backwards with a framecount of 5, it
 *   will start with the 2nd last frame. This way it's possible to
 *   show every animation frame you want. 
 *  
 */
using System;
using System.Collections.Generic;
using System.Text;
using Server.Network;
using Server.Commands;
using Server.ContextMenus;
using Server.Targeting;
using Server.Gumps;

namespace Server.Items
{
    public enum StatuePoses
    {
        Ready = 0,
        Casting = 1,
        Salute = 2,
        Fighting = 3,
        AllPraiseMe = 4,
        HandsOnHips = 5
    }

    public enum StatueMaterial
    {
        JadeX1 = 0xB83,
        BloodStoneX1 = 0xB85,
        BronzeX1 = 0xB87,
        BronzeX2 = 0xB88,
        AlabasterX1 = 0xB89,
        MarbleX1 = 0xB8B,
        MarbleX2 = 0xB8C,
        Granite1 = 0xB8E,
        AlabasterMarbleX0 = 0xB8F,
        AlabasterMarble1 = 0xB90,
        AlabasterMarble2 = 0xB91,
        AlabasterMarble3 = 0xB92,
        Jade0 = 0xB93,
        Jade1 = 0xB94,
        Jade2 = 0xB95,
        Jade3 = 0xB96,
        Bronze0 = 0xB97,
        Bronze1 = 0xB98,
        Bronze2 = 0xB99,
        Bronze3 = 0xB9A
    }

    public enum StatueMaterialGroups
    {
        Jade = 0,
        Bronze = 1,
        Alabaster = 2,
        AlltheOther = 3
    }    

    public class Statues 
    {
        public static void Initialize() 
        {            
            Timer.DelayCall( TimeSpan.FromSeconds( 10.0 ), new TimerCallback( UpdateStatues ) );
        }

        private static List<BaseStatue> m_list = new List<BaseStatue>();

        public static List<BaseStatue> StatuesList
        {
            get
            {
                return m_list;
            }
        }

        public static void UpdateStatues()
        {
            if (NetState.Instances.Count > 0)
            {
                foreach (BaseStatue s in m_list)
                {
                    s.UpdateStatue();
                }
            }
            Timer.DelayCall(TimeSpan.FromSeconds(10.0), new TimerCallback(UpdateStatues));
        }
    }

    public class BaseStatue : Mobile
    {
        private int m_animation;
        private int m_framecount;
        private bool m_forward;

        [Constructable]
        public BaseStatue() : this(null) { }
                
        public BaseStatue(Mobile template)
            : base()
        {
            this.Name = "Statue";
            this.Owner = template;
            this.Blessed = true;
            this.Hits = this.HitsMax;
            CopyMobile(template);
            this.Hue = 0xB85;
            this.SolidHueOverride = 0xB85;
            RecalculateData();
            Statues.StatuesList.Add(this);
        }

        public BaseStatue(Serial serial) : base(serial) { }

        private void CopyMobile(Mobile toCopy)
        {
            if (toCopy == null)
            {
                return;
            }
            HairItemID = toCopy.HairItemID;
            HairHue = toCopy.HairHue;
            FacialHairItemID = toCopy.FacialHairItemID;
            FacialHairHue = toCopy.FacialHairHue;
            BodyValue = toCopy.BodyValue;
            while (this.Items.Count > 0)
            {
                this.Items.RemoveAt(0);
            }
            EquipItemFromLayer(this, toCopy, Layer.Arms);
            EquipItemFromLayer(this, toCopy, Layer.Bracelet);
            EquipItemFromLayer(this, toCopy, Layer.Cloak);
            EquipItemFromLayer(this, toCopy, Layer.Earrings);
            EquipItemFromLayer(this, toCopy, Layer.Gloves);
            EquipItemFromLayer(this, toCopy, Layer.Helm);
            EquipItemFromLayer(this, toCopy, Layer.InnerLegs);
            EquipItemFromLayer(this, toCopy, Layer.InnerTorso);
            EquipItemFromLayer(this, toCopy, Layer.MiddleTorso);
            EquipItemFromLayer(this, toCopy, Layer.Neck);
            EquipItemFromLayer(this, toCopy, Layer.OneHanded);
            EquipItemFromLayer(this, toCopy, Layer.OuterLegs);
            EquipItemFromLayer(this, toCopy, Layer.OuterTorso);
            EquipItemFromLayer(this, toCopy, Layer.Pants);
            EquipItemFromLayer(this, toCopy, Layer.Ring);
            EquipItemFromLayer(this, toCopy, Layer.Shirt);
            EquipItemFromLayer(this, toCopy, Layer.Shoes);
            EquipItemFromLayer(this, toCopy, Layer.TwoHanded);
            EquipItemFromLayer(this, toCopy, Layer.Waist);
        }

        private Item CopyItemFromLayer(Mobile from, Layer layer)
        {
            Item item = from.FindItemOnLayer(layer);
            if (item == null)
            {
                return null;
            }
            Item copy = new Item();
            copy.ItemID = item.ItemID;
            copy.Hue = item.Hue;
            copy.Layer = item.Layer;
            copy.Name = item.Name;
            return copy;
        }

        private void EquipItemFromLayer(Mobile equiper, Mobile from, Layer layer)
        {
            Item item = CopyItemFromLayer(from, layer);
            if (item != null)
            {
                equiper.AddItem(item);
            }
        }

        private Mobile m_Owner;

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Owner
        {
            get { return m_Owner; }
            set { m_Owner = value; }
        }

        private String m_Engraving = null;

        [CommandProperty(AccessLevel.GameMaster)]
        public String Engraving
        {
            get 
            {
                if (m_Engraving != null)
                {
                    if (m_Engraving.Length > 0)
                    {
                        return m_Engraving; 
                    }
                    m_Engraving = null;
                }
                return m_Engraving;
            }
            set 
            {
                if (value != null)
                {
                    if (value.Length == 0)
                    {
                        m_Engraving = null;
                        UpdateEngraving();                            
                        return;
                    }
                }
                m_Engraving = value;
                UpdateEngraving();
            }
        }

        public void UpdateEngraving()
        {
            this.InvalidateProperties();
            if (m_Plinth != null)
            {
                m_Plinth.InvalidateProperties();
            }
        }

        public override void GetContextMenuEntries(Mobile from, List<Server.ContextMenus.ContextMenuEntry> list)
        {
            if (from == Owner)
            {
                list.Add(new StatueDeedMenu(this, from));
            }
        }

        class StatueDeedMenu : ContextMenuEntry
        {
            private BaseStatue m_Statue;
            private Mobile m_Clicker;

            public StatueDeedMenu(BaseStatue statue, Mobile from)
                : base(163)
            {
                m_Statue = statue;
                m_Clicker = from;
            }

            public override void OnClick()
            {
                if (m_Statue != null)
                {
                    m_Statue.Delete();
                    if (m_Clicker == m_Statue.Owner)
                    {
                        m_Clicker.AddToBackpack(new BaseStatueDeed(m_Statue.Material));
                    }
                }
            }
        }

        public override void AddNameProperties(ObjectPropertyList list)
        {
            base.AddNameProperties(list);
            if (m_Plinth != null)
            {
                m_Plinth.InvalidateProperties();
            }
            foreach (Item item in Items)
            {
                if (item != null)
                {
                    item.Name = Name;
                }
            }
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            if (Engraving != null)
            {
                list.Add(1072305, Engraving);
            }
        }

        private Plinth m_Plinth;

        [CommandProperty(AccessLevel.GameMaster)]
        public bool HasPlinth
        {
            get { return (m_Plinth != null); }
            set
            {
                bool has = (m_Plinth != null);
                if (value != has)
                {
                    if (!value)
                    {
                        if (m_Plinth == null)
                        {
                            return;
                        }
                        if (!m_Plinth.Deleted)
                        {
                            m_Plinth.Statue = null;
                            m_Plinth.Delete();
                        }
                        m_Plinth = null;
                        Z -= 5;
                    }
                    else
                    {
                        Z += 5;
                        m_Plinth = new Plinth(this);
                        CheckLocation();
                    }
                }
            }                
        }

        private StatuePoses m_pose = StatuePoses.Ready;

        [CommandProperty(AccessLevel.GameMaster)]
        public StatuePoses Pose
        {
            get { return m_pose; }
            set 
            {
                if (m_pose != value)
                {
                    m_pose = value;
                    RecalculateData();
                    StartUpdateStatue();
                }
            }
        }

        private StatueMaterial m_material = StatueMaterial.AlabasterX1;

        [CommandProperty(AccessLevel.GameMaster)]
        public StatueMaterial Material
        {
            get { return m_material; }
            set
            {
                if (m_material != value)
                {
                    m_material = value;
                    this.SolidHueOverride = (int)m_material;
                    this.Hue = (int)m_material;

                }
            }
        }
       
        public void CheckLocation()
        {
            if (m_Plinth != null)
            {
                if (Map != m_Plinth .Map)
                {
                    m_Plinth.Map = Map;
                }                
                if ( (X != m_Plinth.X) || (Y != m_Plinth.Y) || (Z != (m_Plinth.Z + 5))) 
                {
                    m_Plinth.Location = new Point3D(X, Y, Z - 5);                    
                }                
            }
        }

        public override void OnAosSingleClick(Mobile from)
        {
            this.UpdateStatue();
        }

        public override void OnSingleClick(Mobile from)
        {
            this.UpdateStatue();
        }

        protected override void OnLocationChange(Point3D oldLocation)
        {
            base.OnLocationChange(oldLocation);
            if (m_Plinth != null)
            {
                CheckLocation();
            }           
            StartUpdateStatue();
        }

        public override void OnDelete()
        {
            base.OnDelete();
            if (m_Plinth != null)
            {
                if (!m_Plinth.Deleted)
                {
                    m_Plinth.Statue = null;
                    m_Plinth.Delete();                    
                }
            }
            Statues.StatuesList.Remove(this);
        }

        public override void Delta(MobileDelta flag)
        {
            base.Delta(flag);
            if ( (flag == MobileDelta.Direction) || (flag == MobileDelta.Hue) )
            {
                StartUpdateStatue();
            }            
        }

        public void StartUpdateStatue()
        {
            Timer.DelayCall(TimeSpan.FromSeconds(0.1), new TimerCallback(UpdateStatue));
        }

        private void RecalculateData()
        {
            switch (m_pose)
            {
                case StatuePoses.AllPraiseMe:
                    {
                        m_animation = 17;
                        m_framecount = 5;
                        m_forward = false;
                        break;
                    }
                case StatuePoses.HandsOnHips:
                    {
                        m_animation = 6;
                        m_framecount = 7;
                        m_forward = true;
                        break;
                    }
                case StatuePoses.Ready:
                    {
                        m_animation = 4;
                        m_framecount = 1;
                        m_forward = true;
                        break;
                    }
                case StatuePoses.Casting:
                    {
                        m_animation = 16;
                        m_framecount = 5;
                        m_forward = false;
                        break;
                    }
                case StatuePoses.Salute:
                    {
                        m_animation = 33;
                        m_framecount = 3;
                        m_forward = false;
                        break;
                    }
                case StatuePoses.Fighting:
                    {
                        m_animation = 9;
                        m_framecount = 5;
                        m_forward = false;
                        break;
                    }
               }
        }

        public void UpdateStatue()
        {
		if(this.Map != null)
		{
   		 Sector sect = Map.GetSector(this);
  		  if(sect.Active)
  		  {
  		      Animate(m_animation, m_framecount, 1, m_forward, false, 255);
  		  }
		}
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)3);
            writer.Write((int)m_pose);
            writer.Write((int)m_material);
            writer.Write(m_Plinth);
            writer.Write(Engraving);
            writer.Write(Owner);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            Pose = (StatuePoses)reader.ReadInt();
            Material = (StatueMaterial)reader.ReadInt();

            if (version >= 1)
            {
                m_Plinth = reader.ReadItem() as Plinth;
            }
            if (version >= 2)
            {
                Engraving = reader.ReadString();
            }
            if (version >= 3)
            {
                Owner = reader.ReadMobile();
            }
            if (!Statues.StatuesList.Contains(this))
            {
                Statues.StatuesList.Add(this);
            }
            StartUpdateStatue();
        }
    }

    public class Plinth : Item
    {
        public Plinth() : this(null) { }

        public Plinth(BaseStatue statue)
            : base(0x32F2)
        {
            m_Statue = statue;
            Name = "Statue";
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            if (m_Statue != null)
            {
                if (m_Statue.Engraving != null)
                {
                    list.Add(1072305, m_Statue.Engraving);
                }
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public string Engraving
        {
            get
            {
                if (m_Statue == null)
                {
                    return "No Statue";
                }
                return m_Statue.Engraving;
            }
            set
            {
                if (m_Statue != null)
                {
                    m_Statue.Engraving = value;
                }
            }
        }

        public Plinth(Serial serial) : base(serial) { }
        
        private BaseStatue m_Statue;

        public BaseStatue Statue
        {
            get { return m_Statue; }
            set 
            { 
                m_Statue = value;                
            }
        }

        public void CheckLocation()
        {
            if (m_Statue != null)
            {
                if (Map != m_Statue.Map)
                {
                    m_Statue.Map = Map;
                }
                if ((X != m_Statue.X) || (Y != m_Statue.Y) || (Z != (m_Statue.Z - 5)))
                {
                    m_Statue.Location = new Point3D(X, Y, Z + 5);
                }   
            }
        }

        public override void AddNameProperties(ObjectPropertyList list)
        {
            if (m_Statue != null)
            {
                if (m_Statue.Name != null)
                {
                    list.Add(m_Statue.Name);
                    return;
                }
            }
            list.Add("Statue");
        }

        public override bool OnDragLift(Mobile from)
        {
            return false;
        }

        public override void OnLocationChange(Point3D oldLocation)
        {
            base.OnLocationChange(oldLocation);
            CheckLocation();
        }

        public override void OnDelete()
        {
            base.OnDelete();
            if (m_Statue != null)
            {
                if (!m_Statue.Deleted)
                {
                    m_Statue.HasPlinth = false;
                }
            }            
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // Version
            writer.Write(m_Statue);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            m_Statue = reader.ReadMobile() as BaseStatue;
        }
    }

    public class BaseStatueDeed : Item
    {
        private StatueMaterialGroups m_MaterialGroup;

        [CommandProperty(AccessLevel.GameMaster)]
        public StatueMaterialGroups MaterialGroup
        {
            get { return m_MaterialGroup; }
            set 
            { 
                m_MaterialGroup = value;
                Hue = (int)GetMaterials(m_MaterialGroup)[0];
            }
        }

        public static StatueMaterial[] GetMaterials(StatueMaterialGroups group)
        {
            if (group == StatueMaterialGroups.Bronze)
            {
                return new StatueMaterial[]{ StatueMaterial.Bronze0, StatueMaterial.Bronze1, StatueMaterial.Bronze2, StatueMaterial.Bronze3 };
            }
            if (group == StatueMaterialGroups.Alabaster)
            {
                return new StatueMaterial[] { StatueMaterial.AlabasterMarbleX0, StatueMaterial.AlabasterMarble1, StatueMaterial.AlabasterMarble2, StatueMaterial.AlabasterMarble3 };
            }
            if (group == StatueMaterialGroups.Jade)
            {
                return new StatueMaterial[] { StatueMaterial.Jade0, StatueMaterial.Jade1, StatueMaterial.Jade2, StatueMaterial.Jade3 };
            }
            if (group == StatueMaterialGroups.AlltheOther)
            {
                return new StatueMaterial[] { StatueMaterial.BloodStoneX1, StatueMaterial.MarbleX1, StatueMaterial.MarbleX2, StatueMaterial.Granite1 };
            }
            return null;
        }

        private static StatueMaterialGroups GetGroup(StatueMaterial material)
        {
            for (int i = 0; i < 4; i++)
            {
                StatueMaterial[] ms = GetMaterials((StatueMaterialGroups)i);
                for (int j = 0; j < 4; j++)
                {
                    if (ms[j] == material)
                    {
                        return (StatueMaterialGroups)i;
                    }
                }
            }
            return StatueMaterialGroups.AlltheOther;
        }

        [Constructable]
        public BaseStatueDeed(StatueMaterial material) : this( GetGroup(material) )
        {
        }

        [Constructable]
        public BaseStatueDeed()
            : this(StatueMaterialGroups.AlltheOther)
        {
        }

        [Constructable]
        public BaseStatueDeed(StatueMaterialGroups group) : base( 0x14F0 )
        {
            MaterialGroup = group;
            Hue = (int)GetMaterials(group)[0];
        }

        public BaseStatueDeed(Serial serial) : base(serial) { }

        public override void OnDoubleClick(Mobile from)
        {
            if (!this.IsChildOf(from.Backpack))
            {
                from.SendMessage("This must be in your backpack to be used");
                return;
            }
            from.CloseGump (typeof (StatueGump));
            from.SendGump(new StatueGump(this, from, m_MaterialGroup, 0, Direction.East, StatuePoses.AllPraiseMe, true));
        }

        public virtual void PlaceStatue(Mobile owner, StatueMaterial material, StatuePoses pose, Direction dir, bool plinth) 
        {
            owner.Target = new PlaceStatueTarget(this, material, pose, dir, plinth);
        }

        class PlaceStatueTarget : Target
        {
            private BaseStatueDeed m_deed;
            private StatueMaterial m_Material;
            private StatuePoses m_Pose;
            private Direction m_Dir;
            private bool m_Plinth;

            public PlaceStatueTarget(BaseStatueDeed deed, StatueMaterial material, StatuePoses pose, Direction dir, bool plinth)
                : base(15, true, TargetFlags.None)
            {
                m_deed = deed;
                m_Material = material;
                m_Pose = pose;
                m_Dir = dir;
                m_Plinth = plinth;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted == null)
                {
                    return;
                }                            

                if (targeted is LandTarget)
                {
                    Point3D loc = ((LandTarget)targeted).Location;
                    BaseStatue statue = new BaseStatue(from);
                    statue.Material = m_Material;                    
                    statue.Pose = m_Pose;
                    statue.Direction = m_Dir;
                    statue.Map = from.Map;                    
                    statue.Location = loc;
                    if (m_Plinth)
                    {
                        statue.HasPlinth = m_Plinth;
                    }
                    m_deed.Delete();
                    return;
                }
                if (targeted is StaticTarget)
                {
                    Point3D loc = ((StaticTarget)targeted).Location;
                    BaseStatue statue = new BaseStatue(from);
                    statue.Material = m_Material;                    
                    statue.Pose = m_Pose;
                    statue.Direction = m_Dir;
                    statue.Map = from.Map;
                    statue.Location = loc;
                    if (m_Plinth)
                    {
                        statue.HasPlinth = m_Plinth;
                    }
                    m_deed.Delete();
                    return;
                }               
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
            writer.Write((int)m_MaterialGroup);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            MaterialGroup = (StatueMaterialGroups)reader.ReadInt();
        }
    }

    public class StatueGump : Gump
    {
        private Mobile m_Mobile;
        private StatueMaterialGroups m_Group;
        private int m_Material;
        private Direction m_Direction;
        private StatuePoses m_Pose;
        private BaseStatueDeed m_Deed;

        public StatueGump(BaseStatueDeed deed, Mobile from, StatueMaterialGroups group, int material, Direction dir, StatuePoses pose, bool Plinth)
            : base(0, 0)
        {
            m_Deed = deed;
            m_Mobile = from;
            m_Group = group;
            m_Material = material;
            m_Direction = dir;
            m_Pose = pose;

            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
			this.AddImage(4, 5, 39);
			this.AddLabel(90, 39, 0, @"Material");
			this.AddLabel(98, 91, 0, @"Pose");
			this.AddLabel(84, 141, 0, @"Direction");
			this.AddButton(34, 52, 4014, 4015, (int)Buttons.MaterialBack, GumpButtonType.Reply, 0);
			this.AddButton(34, 101, 4014, 4015, (int)Buttons.PoseBack, GumpButtonType.Reply, 0);
			this.AddButton(34, 153, 4014, 4015, (int)Buttons.DirectionBack, GumpButtonType.Reply, 0);
			this.AddButton(83, 244, 247, 248, (int)Buttons.Okay, GumpButtonType.Reply, 0);
			this.AddButton(189, 52, 4005, 4006, (int)Buttons.MaterialForward, GumpButtonType.Reply, 0);
			this.AddButton(189, 101, 4005, 4006, (int)Buttons.PoseForward, GumpButtonType.Reply, 0);
			this.AddButton(188, 157, 4005, 4006, (int)Buttons.DirectionForward, GumpButtonType.Reply, 0);
			this.AddLabel(75, 65, 37, BaseStatueDeed.GetMaterials(group)[m_Material].ToString());
			this.AddLabel(76, 113, 37, m_Pose.ToString());
			this.AddLabel(75, 168, 37, m_Direction.ToString());
			this.AddCheck(47, 203, 210, 211, Plinth, (int)Buttons.CheckBox1);
			this.AddLabel(78, 204, 0, @"Plinth");


        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            bool Plinth = info.IsSwitched((int)Buttons.CheckBox1);

 	        if (info.ButtonID == (int)Buttons.Okay) 
            {
                m_Deed.PlaceStatue(m_Mobile, BaseStatueDeed.GetMaterials(m_Group)[m_Material], m_Pose, m_Direction, Plinth);
                return;
            }
            if (info.ButtonID == (int)Buttons.MaterialBack) 
            {
                 int id = (m_Material == 0 ? 3 : m_Material - 1 );
                 sender.Mobile.SendGump(new StatueGump(m_Deed, m_Mobile, m_Group, id, m_Direction, m_Pose, Plinth));
                 return;
            }
            if (info.ButtonID == (int)Buttons.MaterialForward) 
            {
                int id = (m_Material == 3 ? 0 : m_Material + 1 );
                sender.Mobile.SendGump(new StatueGump(m_Deed, m_Mobile, m_Group, id, m_Direction, m_Pose, Plinth));
                return;
            }
            if (info.ButtonID == (int)Buttons.DirectionBack)
            {
                int id = (int)m_Direction;
                id = (id == 0 ? 7 : id - 1);
                sender.Mobile.SendGump(new StatueGump(m_Deed, m_Mobile, m_Group, m_Material, (Direction)id, m_Pose, Plinth));
                return;
            }
            if (info.ButtonID == (int)Buttons.DirectionForward)
            {
                int id = (int)m_Direction;
                id = (id == 7 ? 0 : id + 1);
                sender.Mobile.SendGump(new StatueGump(m_Deed, m_Mobile, m_Group, m_Material, (Direction)id, m_Pose, Plinth));
                return;
            }
            if (info.ButtonID == (int)Buttons.PoseBack)
            {
                int id = (int)m_Pose;
                id = (id == 0 ? 5 : id - 1);
                sender.Mobile.SendGump(new StatueGump(m_Deed, m_Mobile, m_Group, m_Material, m_Direction, (StatuePoses)id, Plinth));
                return;
            }
            if (info.ButtonID == (int)Buttons.PoseForward)
            {
                int id = (int)m_Pose;
                id = (id == 5 ? 0 : id + 1);
                sender.Mobile.SendGump(new StatueGump(m_Deed, m_Mobile, m_Group, m_Material, m_Direction, (StatuePoses)id, Plinth));
                return;
            }
            sender.Mobile.SendGump(new StatueGump(m_Deed, m_Mobile, m_Group, m_Material, m_Direction, m_Pose, Plinth));
        }

        public enum Buttons
        {
            MaterialBack,
            PoseBack,
            DirectionBack,
            Okay,
            MaterialForward,
            PoseForward,
            DirectionForward,
            CheckBox1
        }
    }
}
