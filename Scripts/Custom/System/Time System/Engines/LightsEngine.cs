using System;
using System.Collections.Generic;
using Server;
using Server.Items;

namespace Server.TimeSystem
{
    public class LightsEngine
    {
        #region Get Methods

        private static bool IsDefraggable(BaseLight baseLight)
        {
            if (baseLight == null || baseLight.Deleted)
            {
                return true;
            }

            return false;
        }

        private static bool IsControllable(BaseLight baseLight, EffectsObject eo)
        {
            if (!Data.UseAutoLighting || eo == null || eo.EffectsMap == null || !eo.EffectsMap.UseAutoLighting || (baseLight is TSBaseLight && !((TSBaseLight)baseLight).UseAutoLighting))
            {
                return false;
            }

            return true;
        }

        #endregion

        #region Check Methods

        public static void CheckLights()
        {
            if (Data.LightsList == null)
            {
                return;
            }

            bool defragged = false;

            lock (Data.LightsList)
            {
                for (int i = 0; i < Data.LightsList.Count; i++)
                {
                    BaseLight baseLight = Data.LightsList[i];

                    EffectsObject eo = EffectsEngine.GetEffects(baseLight, false);

                    if (IsDefraggable(baseLight))
                    {
                        defragged = true;

                        Data.LightsList.Remove(baseLight);

                        i--;
                    }
                    else if (!IsControllable(baseLight, eo))
                    {
                        if (!baseLight.Burning)
                        {
                            baseLight.Ignite();
                        }
                    }
                    else
                    {
                        TimeEngine.CalculateLightLevel(baseLight, eo);
                    }
                }
            }

            if (defragged)
            {
                Data.LightsList.TrimExcess();
            }
        }

        #endregion

        #region Update Methods

        public static void PopulateLightsList()
        {
            Data.LightsList = new List<BaseLight>();

            lock (Data.LightsList)
            {
                foreach (Item item in World.Items.Values)
                {
                    if (!item.Deleted && item is BaseLight)
                    {
                        for (int i = 0; i < Data.ItemLightTypes.Length; i++)
                        {
                            if (item.GetType() == Data.ItemLightTypes[i])
                            {
                                BaseLight baseLight = (BaseLight)item;

                                Data.LightsList.Add(baseLight);
                            }
                        }
                    }
                }
            }

            Support.ConsoleWriteLine(String.Format("Time System: Calculated managed lights list and found {0} light{1}.", Data.LightsList.Count, Data.LightsList.Count == 1 ? "" : "s"));
        }

        public static void UpdateManagedLight(BaseLight baseLight, int currentLevel)
        {
            if (currentLevel >= Data.LightsOnLevel && !baseLight.Burning)
            {
                baseLight.Ignite();
            }
            else if (currentLevel < Data.LightsOnLevel && baseLight.Burning)
            {
                baseLight.Douse();
            }
        }

        #endregion

        #region Calculation Methods

        public static void CalculateLightOutage(BaseLight baseLight, EffectsObject eo)
        {
            if (!Data.UseRandomLightOutage || IsDefraggable(baseLight) || !IsControllable(baseLight, eo) || !eo.EffectsMap.UseRandomLightOutage || (baseLight is TSBaseLight && !((TSBaseLight)baseLight).UseRandomLightOutage))
            {
                return;
            }

            int lowNumber = Support.GetRandom(0, (100 - eo.EffectsMap.LightOutageChancePerTick));
            int highNumber = lowNumber + eo.EffectsMap.LightOutageChancePerTick;

            int randomChance = Support.GetRandom(0, 100);

            if (randomChance >= lowNumber && randomChance <= highNumber)
            {
                if (baseLight.Burning)
                {
                    baseLight.Douse();
                }
                else
                {
                    baseLight.Ignite();
                }
            }
        }

        #endregion
    }
}