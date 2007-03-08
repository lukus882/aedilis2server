using System;
using System.Collections.Generic;
using Server;
using Server.Network;

namespace Server.TimeSystem
{
    public class EffectsEngine
    {
        #region Set Methods

        public static void SetNightSightOn(Mobile mobile, int level)
        {
            MobileObject mo = Support.GetMobileObject(mobile);

            new LightCycle.NightSightTimer(mobile).Start();

            mo.IsNightSightOn = true;
            mo.OldLightLevel = level;
        }

        public static void SetNightSightOff(Mobile mobile)
        {
            MobileObject mo = Support.GetMobileObject(mobile);

            SetNightSightOff(mo);
        }

        public static void SetNightSightOff(MobileObject mo)
        {
            if (mo != null)
            {
                mo.IsNightSightOn = false;
                mo.OldLightLevel = 0;
            }
        }

        #endregion

        #region Get Methods

        public static EffectsObject GetEffects(object o, bool getSeason)
        {
            Map map = Support.GetMap(o);
            int x = Support.GetXAxis(o);
            int y = Support.GetYAxis(o);

            return GetEffects(map, x, y, getSeason);
        }

        public static EffectsObject GetEffects(Map map, int x, int y, bool getSeason)
        {
            EffectsObject eo = new EffectsObject();

            eo.EffectsMap = null;
            eo.EffectsExclusionMap = null;

            List<EffectsMapObject> emoArray = GetEffectsMapArray(map, x, y);

            if (emoArray.Count > 0)
            {
                eo.EffectsMap = emoArray[0];
            }

            List<EffectsExclusionMapObject> eemoArray = GetEffectsExclusionMapArray(map, x, y);

            if (eemoArray.Count > 0)
            {
                eo.EffectsExclusionMap = eemoArray[0];
            }

            EffectsMapObject emoParentSeason = null;

            for (int i = 0; i < emoArray.Count; i++)
            {
                EffectsMapObject emo = emoArray[i];

                if (emo.UseSeasons)
                {
                    emoParentSeason = emo;

                    break;
                }
            }

            if (eo.EffectsMap != null && eo.EffectsExclusionMap != null && eo.EffectsExclusionMap.Priority >= eo.EffectsMap.Priority)
            {
                eo.EffectsMap = null;

                return eo;
            }

            if (eo.EffectsMap != null)
            {
                eo.EffectsExclusionMap = null;

                if (getSeason && emoParentSeason != null)
                {
                    eo.LateralSeason = emoParentSeason.GetLateralSeason(map, x, y);
                    eo.Season = emoParentSeason.GetSeason(map, x, y);
                }
            }

            return eo;
        }

        public static int GetNightSightLevel(Mobile mobile, int level)
        {
            if (!Data.Enabled)
            {
                return level;
            }

            EffectsObject eo = GetEffects(mobile, false);
            MobileObject mo = Support.GetMobileObject(mobile);

            if (Data.UseNightSightDarkestHourOverride)
            {
                if (eo.EffectsMap != null && eo.EffectsMap.UseNightSightDarkestHourOverride)
                {
                    if (mo != null && mo.IsDarkestHour)
                    {
                        if (eo.EffectsMap.NightSightDarkestHourReduction < 100)
                        {
                            level = (int)((double)level * ((double)(100 - eo.EffectsMap.NightSightDarkestHourReduction) / 100));

                            if (level < 0)
                            {
                                level = 0;
                            }
                        }
                        else
                        {
                            level = -1;
                        }
                    }
                }
            }

            if (level > -1 && Data.UseNightSightOverride)
            {
                if (eo.EffectsMap != null && eo.EffectsMap.UseNightSightOverride)
                {
                    if (eo.EffectsMap.NightSightLevelReduction < 100)
                    {
                        level = (int)((double)level * ((double)(100 - eo.EffectsMap.NightSightLevelReduction) / 100));

                        if (level < 0)
                        {
                            level = 0;
                        }
                    }
                    else
                    {
                        level = -1;
                    }
                }
            }

            return level;
        }

        public static List<EffectsMapObject> GetEffectsMapArray(Map map, int x, int y)
        {
            List<EffectsMapObject> emoArray = new List<EffectsMapObject>();

            for (int i = 0; i < Data.EffectsMapArray.Count; i++)
            {
                EffectsMapObject emo = Data.EffectsMapArray[i];

                if (emo.IsIn(map, x, y))
                {
                    emoArray.Add(emo);
                }
            }

            emoArray.Sort(SortEffectsMapArray);

            return emoArray;
        }

        public static List<EffectsExclusionMapObject> GetEffectsExclusionMapArray(Map map, int x, int y)
        {
            List<EffectsExclusionMapObject> eemoArray = new List<EffectsExclusionMapObject>();

            for (int i = 0; i < Data.EffectsExclusionMapArray.Count; i++)
            {
                EffectsExclusionMapObject eemo = Data.EffectsExclusionMapArray[i];

                if (eemo.IsIn(map, x, y))
                {
                    eemoArray.Add(eemo);
                }
            }

            eemoArray.Sort(SortEffectsExclusionMapArray);

            return eemoArray;
        }

        private static int SortEffectsMapArray(EffectsMapObject emoOne, EffectsMapObject emoTwo)
        {
            if (emoOne == emoTwo)
            {
                return 0;
            }

            if (emoOne.Priority > emoTwo.Priority)
            {
                return -1;
            }
            else if (emoOne.Priority == emoTwo.Priority)
            {
                if (emoOne.Index < emoTwo.Index)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return 1;
            }
        }

        private static int SortEffectsExclusionMapArray(EffectsExclusionMapObject eemoOne, EffectsExclusionMapObject eemoTwo)
        {
            if (eemoOne == eemoTwo)
            {
                return 0;
            }

            if (eemoOne.Priority > eemoTwo.Priority)
            {
                return 1;
            }
            else if (eemoOne.Priority == eemoTwo.Priority)
            {
                if (eemoOne.Index < eemoTwo.Index)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }

        #endregion

        #region Check Methods

        public static void CheckEffects(MobileObject mo, EffectsObject eo, bool checkSeason, bool checkNightSight)
        {
            Mobile mobile = mo.Mobile;

            if (eo.EffectsMap != null)
            {
                if (checkSeason && Data.UseSeasons && eo.EffectsMap.UseSeasons)
                {
                    if (mo.Season != eo.Season)
                    {
                        mo.Season = eo.Season;

                        if (eo.Season == Season.Winter)
                        {
                            mobile.Send(new SeasonChange(4)); // Send Desolation packet for Winter instead of Winter due to Winter adding blocky snow tiles which don't look right.
                        }
                        else
                        {
                            mobile.Send(new SeasonChange((int)eo.Season - 1));
                        }

                        Support.SendLightLevelUpdate(mo);
                    }
                }

                if (checkNightSight)
                {
                    if (mo.IsNightSightOn)
                    {
                        if (Data.UseNightSightDarkestHourOverride && eo.EffectsMap.UseNightSightDarkestHourOverride)
                        {
                            if (mo.IsDarkestHour)
                            {
                                if (eo.EffectsMap.NightSightDarkestHourReduction < 100)
                                {
                                    int adjustment = (int)((double)mo.OldLightLevel * ((double)(100 - eo.EffectsMap.NightSightDarkestHourReduction) / 100));

                                    if (adjustment >= 0 && mobile.LightLevel != adjustment)
                                    {
                                        mobile.LightLevel = adjustment;
                                    }
                                }
                                else
                                {
                                    mobile.EndAction(typeof(LightCycle));
                                    mobile.LightLevel = 0;
                                    BuffInfo.RemoveBuff(mobile, BuffIcon.NightSight);

                                    SetNightSightOff(mo);

                                    mobile.SendMessage("The evil in the air draws energy from your nightsight effect!");
                                }
                            }
                        }

                        if (!mo.IsDarkestHour && Data.UseNightSightOverride && mo.IsNightSightOn)
                        {
                            if (eo.EffectsMap.UseNightSightOverride)
                            {
                                if (eo.EffectsMap.NightSightLevelReduction < 100)
                                {
                                    int adjustment = (int)((double)mo.OldLightLevel * ((double)(100 - eo.EffectsMap.NightSightLevelReduction) / 100));

                                    if (adjustment >= 0 && mobile.LightLevel != adjustment)
                                    {
                                        mobile.LightLevel = adjustment;
                                    }
                                }
                                else
                                {
                                    mobile.EndAction(typeof(LightCycle));
                                    mobile.LightLevel = 0;
                                    BuffInfo.RemoveBuff(mobile, BuffIcon.NightSight);

                                    SetNightSightOff(mo);

                                    mobile.SendMessage("An unknown magical disturbance has consumed your nightsight effect!");
                                }
                            }
                            else
                            {
                                if (mobile.LightLevel < mo.OldLightLevel)
                                {
                                    mobile.LightLevel = mo.OldLightLevel;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (checkNightSight && mo.IsNightSightOn && mobile.LightLevel < mo.OldLightLevel)
                {
                    mobile.LightLevel = mo.OldLightLevel;
                }
            }
        }

        #endregion
    }
}