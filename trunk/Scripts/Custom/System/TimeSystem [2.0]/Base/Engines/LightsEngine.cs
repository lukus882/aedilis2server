using System;
using System.Collections.Generic;
using Server;
using Server.Items;

namespace Server.TimeSystem
{
    public class LightsEngine
    {
        #region Defrag Lights

        public static bool DefragLightsList(BaseLight baseLight)
        {
            if (baseLight == null || baseLight.Deleted || (baseLight is TSBaseLight && !((TSBaseLight)baseLight).UseAutoLighting))
            {
                Data.LightsList.Remove(baseLight);

                return true;
            }

            return false;
        }

        #endregion

        #region Check Methods

        public static void CheckLights()
        {
            if (Data.LightsList == null)
            {
                return;
            }

            if (Data.UseTimeZones)
            {
                lock (Data.LightsList)
                {
                    bool defragged = false;

                    for (int i = 0; i < Data.LightsList.Count; i++)
                    {
                        BaseLight baseLight = (BaseLight)Data.LightsList[i];

                        if (DefragLightsList(baseLight))
                        {
                            defragged = true;

                            i--;
                        }
                        else
                        {
                            TimeEngine.CalculateLightLevel(baseLight);
                        }
                    }

                    if (defragged)
                    {
                        Data.LightsList.TrimExcess();
                    }
                }
            }
            else
            {
                UpdateAllManagedLights();
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

                                bool addLight = true;

                                if (baseLight is TSBaseLight && !((TSBaseLight)baseLight).UseAutoLighting)
                                {
                                    addLight = false;
                                }

                                if (addLight)
                                {
                                    Data.LightsList.Add(baseLight);

                                    if (Data.UseTimeZones)
                                    {
                                        TimeEngine.CalculateLightLevel(baseLight);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (!Data.UseTimeZones)
            {
                UpdateAllManagedLights();
            }

            Support.ConsoleWriteLine(String.Format("Time System: Calculated managed lights list and found {0} light{1}.", Data.LightsList.Count, Data.LightsList.Count == 1 ? "" : "s"));
        }

        public static void UpdateManagedLight(BaseLight baseLight, int currentLevel)
        {
            if (DefragLightsList(baseLight))
            {
                return;
            }

            if (currentLevel >= Data.LightsOnLevel && !baseLight.Burning)
            {
                baseLight.Ignite();
            }
            else if (currentLevel < Data.LightsOnLevel && baseLight.Burning)
            {
                baseLight.Douse();
            }
        }

        public static void UpdateAllManagedLights()
        {
            if (Data.LightsList == null)
            {
                return;
            }

            TimeEngine.CalculateLightLevel(null);

            if (Data.BaseLightLevel >= Data.LightsOnLevel)
            {
                lock (Data.LightsList)
                {
                    bool defragged = false;

                    for (int i = 0; i < Data.LightsList.Count; i++)
                    {
                        BaseLight baseLight = (BaseLight)Data.LightsList[i];

                        if (DefragLightsList(baseLight))
                        {
                            defragged = true;

                            i--;
                        }
                        else
                        {
                            if (!baseLight.Burning)
                            {
                                baseLight.Ignite();
                            }
                        }
                    }

                    if (defragged)
                    {
                        Data.LightsList.TrimExcess();
                    }
                }
            }
            else if (Data.BaseLightLevel < Data.LightsOnLevel)
            {
                lock (Data.LightsList)
                {
                    bool defragged = false;

                    for (int i = 0; i < Data.LightsList.Count; i++)
                    {
                        BaseLight baseLight = (BaseLight)Data.LightsList[i];

                        if (DefragLightsList(baseLight))
                        {
                            defragged = true;

                            i--;
                        }
                        else
                        {
                            if (baseLight.Burning)
                            {
                                baseLight.Douse();
                            }
                        }
                    }

                    if (defragged)
                    {
                        Data.LightsList.TrimExcess();
                    }
                }
            }
        }

        public static void TurnOnAllWorldLights()
        {
            foreach (Item item in World.Items.Values)
            {
                if (item is BaseLight)
                {
                    for (int i = 0; i < Data.ItemLightTypes.Length; i++)
                    {
                        if (item.GetType() == Data.ItemLightTypes[i])
                        {
                            BaseLight baseLight = (BaseLight)item;

                            if (!baseLight.Deleted)
                            {
                                baseLight.Ignite();
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region Calculation Methods

        public static void CalculateLightOutage(BaseLight baseLight)
        {
            if (baseLight == null || !Data.UseAutoLighting || !Data.UseRandomLightOutage || DefragLightsList(baseLight) || (baseLight is TSBaseLight && !((TSBaseLight)baseLight).UseRandomLightOutage))
            {
                return;
            }

            int lowNumber = Support.GetRandom(0, (100 - Data.LightOutageChancePerTick));
            int highNumber = lowNumber + Data.LightOutageChancePerTick;

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