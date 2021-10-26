using OWML.Common;
using OWML.ModHelper;
using OWML.ModHelper.Events;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ClassLibrary2
{
    class EnabledInhabitants
    {
        private static bool updateGhosts(ref float __result)
        {
            if (Inhabitants.enabledAI)
            {
                return true;
            }
            else
            {
                __result = float.MaxValue;
                return false;
            }
        }
    }

    class HostileInhabitants
    {
        private static bool updateGhosts(ref float __result)
        {
            if (Inhabitants.enabledHostility)
            {
                return true;
            }
            else
            {
                __result = float.MinValue;
                return false;
            }
        }
    }

    public class Inhabitants
    { 
        private static bool _enabledAI = true;
        private static bool _enabledHostility = true;

        public static bool enabledAI { get { return _enabledAI; } set { _enabledAI = value; updateGhosts(); } }
        public static bool enabledHostility { get { return _enabledHostility; } set { _enabledHostility = value; updateGhosts(); } }

        public static void Start(ModHelper helper)
        {
            helper.HarmonyHelper.AddPrefix<WaitAction>("CalculateUtility", typeof(EnabledInhabitants), "updateGhosts");
            helper.HarmonyHelper.AddPrefix<ChaseAction>("CalculateUtility", typeof(HostileInhabitants), "updateGhosts");
            helper.HarmonyHelper.AddPrefix<HuntAction>("CalculateUtility", typeof(HostileInhabitants), "updateGhosts");
            helper.HarmonyHelper.AddPrefix<StalkAction>("CalculateUtility", typeof(HostileInhabitants), "updateGhosts");
            helper.HarmonyHelper.AddPrefix<GrabAction>("CalculateUtility", typeof(HostileInhabitants), "updateGhosts");
        }

        public static void Destroy(ModHelper helper)
        {

        }

        public static void Awake()
        {
            foreach (Sector sector in SectorManager.GetRegisteredSectors())
            {
                if (Sector.Name.DreamWorld.Equals(sector.GetName()))
                {
                    sector.OnSectorOccupantsUpdated += () => updateGhosts();
                }
            }
        }

        private static void updateGhosts()
        {
            
        }
    }
}
