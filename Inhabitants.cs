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

    class InhabitantsHelper
    {
        private static bool Awake(ref GhostController __instance)
        {
            Inhabitants.inhabitants.Add(__instance);
            return true;
        }

        private static bool OnDestroy(ref GhostController __instance)
        {
            Inhabitants.inhabitants.Remove(__instance);
            return true;
        }
    }

    public class Inhabitants
    {
        public static HashSet<GhostController> inhabitants = new HashSet<GhostController>();

        private static bool _enabledAI = true;
        private static bool _enabledHostility = true;

        public static bool enabledAI { get { return _enabledAI; } set { _enabledAI = value; updateGhosts(); } }
        public static bool enabledHostility { get { return _enabledHostility; } set { _enabledHostility = value; updateGhosts(); } }

        public static void Start()
        {
            Helper.helper.HarmonyHelper.AddPrefix<WaitAction>("CalculateUtility", typeof(EnabledInhabitants), "updateGhosts");
            Helper.helper.HarmonyHelper.AddPrefix<ChaseAction>("CalculateUtility", typeof(HostileInhabitants), "updateGhosts");
            Helper.helper.HarmonyHelper.AddPrefix<HuntAction>("CalculateUtility", typeof(HostileInhabitants), "updateGhosts");
            Helper.helper.HarmonyHelper.AddPrefix<StalkAction>("CalculateUtility", typeof(HostileInhabitants), "updateGhosts");
            Helper.helper.HarmonyHelper.AddPrefix<GrabAction>("CalculateUtility", typeof(HostileInhabitants), "updateGhosts");
        }

        public static void Destroy()
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
