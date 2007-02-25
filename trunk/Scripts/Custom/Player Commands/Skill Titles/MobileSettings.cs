using System;
using System.Collections.Generic;
using System.Text;
using Server;

namespace Server.Mobiles
{
    [PropertyObject]
    public class MobileSettings
    {
        /// <summary>
        /// PlayerMobile Instance to be able to Invalidate the Properties if there are any settings to be added
        /// </summary>
        protected PlayerMobile x_Player;

        private int x_LegendaryHue;
        private int x_ElderHue;
        private int x_GrandmasterHue;
        private int x_NormalHue;
        private bool x_DisplaySkillTitles;
        private SkillName x_SkillTitle;

        [CommandProperty( AccessLevel.GameMaster )]
        public SkillName SkillTitle
        {
            get { return x_SkillTitle; }
            set { x_SkillTitle = value; }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public int LegendaryHue
        {
            get { return x_LegendaryHue; }
            set { x_LegendaryHue = value; }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public int ElderHue
        {
            get { return x_ElderHue; }
            set { x_ElderHue = value; }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public int GrandmasterHue
        {
            get { return x_GrandmasterHue; }
            set { x_GrandmasterHue = value; }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public int NormalHue
        {
            get { return x_NormalHue; }
            set { x_NormalHue = value; }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public bool DisplaySkillTitles
        {
            get { return x_DisplaySkillTitles; }
            set { x_DisplaySkillTitles = value; }
        }

        /// <summary>
        /// Creates a new Instance of the Mobile Settings
        /// </summary>
        public MobileSettings( PlayerMobile m )
        {
            x_Player = m;

            x_SkillTitle = SkillName.Alchemy;
            x_LegendaryHue = SkillTitles.DefaultLegendaryHue;
            x_ElderHue = SkillTitles.DefaultElderHue;
            x_GrandmasterHue = SkillTitles.DefaultGrandmasterHue;
            x_NormalHue = SkillTitles.DefaultNormalHue;
            x_DisplaySkillTitles = true;
        }

        /// <summary>
        /// The Settings String to let the GameMasters or higher know there is Additional Settings
        /// </summary>
        /// <returns></returns>
        public override string  ToString()
        {
 	         return "...";
        }

        /// <summary>
        /// This is before the Get Properties is fired, but it is after the base.GetProperties on <see cref="PlayerMobile"/> is shown
        /// </summary>
        /// <param name="list">ObjectPropertyList</param>
        public virtual void OnBeforeGetProperties(ObjectPropertyList list)
        {
        }
        
        /// <summary>
        /// This is at the very end of the GetProperties in <see cref="PlayerMobile"/>
        /// </summary>
        /// <param name="list"></param>
        public virtual void OnAfterGetProperties(ObjectPropertyList list)
        {
        }

        /// <summary>
        /// Deserializes the Mobile Settings
        /// </summary>
        /// <param name="reader">the Generic Reader to read the Settings from</param>
        public virtual void Deserialize( GenericReader reader )
        {
            int version = (int)reader.ReadInt();

            switch (version)
            {
                case 0:
                    {
                        x_SkillTitle = (SkillName)reader.ReadInt();
                        x_LegendaryHue = (int)reader.ReadInt();
                        x_ElderHue = (int)reader.ReadInt();
                        x_GrandmasterHue = (int)reader.ReadInt();
                        x_NormalHue = (int)reader.ReadInt();
                        x_DisplaySkillTitles = (bool)reader.ReadBool();
                        break;
                    }
            }
        }

        /// <summary>
        /// To Serialize the Mobile Settings
        /// </summary>
        /// <param name="writer">the Generic Writer to write to the Settings to</param>
        public virtual void Serialize(GenericWriter writer)
        {
            writer.Write((int)0); // Version

            writer.Write((int)x_SkillTitle);
            writer.Write((int)x_LegendaryHue);
            writer.Write((int)x_ElderHue);
            writer.Write((int)x_GrandmasterHue);
            writer.Write((int)x_NormalHue);
            writer.Write((bool)x_DisplaySkillTitles);
        }
    }
}
