using OWML.Common;
using OWML.ModHelper;
using OWML.ModHelper.Events;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ClassLibrary2
{
    class Player
    {
        public static PlayerResources getResources()
        {
            PlayerResources resources = null;
            if (Locator.GetPlayerTransform() && Locator.GetPlayerTransform().TryGetComponent<PlayerResources>(out resources))
            {
                return resources;
            }
            return null;
        }

        public static JetpackThrusterModel getJetpack()
        {
            JetpackThrusterModel model = null;
            if (getResources() && getResources().TryGetComponent<JetpackThrusterModel>(out model))
            {
                return model;
            }
            return null;
        }

        public static bool hasUnlimitedBoost { get; set; }
        public static bool hasUnlimitedFuel { get; set; }
        public static bool hasUnlimitedOxygen { get; set; }
        public static bool hasUnlimitedHealth { get; set; }
        public static bool isInvincible { get; set; }
        public static float boostSeconds
        {
            get
            {
                if (getJetpack() != null)
                    return getJetpack().GetValue<float>("_boostChargeFraction") * maxBoostSeconds;
                else
                    return maxBoostSeconds;
            }
            set
            {
                if (getJetpack() != null)
                {
                    getJetpack().SetValue("_boostChargeFraction", value / maxBoostSeconds);
                }
            }
        }
        public static float maxBoostSeconds
        {
            get
            {
                if (getJetpack() != null)
                    return getJetpack().GetValue<float>("_boostSeconds");
                else
                    return 1f;
            }
            set
            {
                if (getJetpack() != null)
                {
                    getJetpack().SetValue("_boostSeconds", value);
                }
            }
        }
        public static float health
        {
            get
            {
                if (getResources() != null)
                    return getResources().GetValue<float>("_currentHealth");
                else
                    return maxHealth;
            }
            set
            {
                if (getResources() != null)
                {
                    getResources().SetValue("_currentHealth", value);
                }
            }
        }
        public static float maxHealth { get; set; } = 100f;
        public static float fuelSeconds
        {
            get
            {
                if (getResources() != null)
                    return getResources().GetValue<float>("_currentFuel");
                else
                    return maxFuelSeconds;
            }
            set
            {
                if (getResources() != null)
                {
                    getResources().SetValue("_currentFuel", value);
                }
            }
        }
        public static float maxFuelSeconds { get; set; } = 100f;
        public static float oxygenSeconds
        {
            get
            {
                if (getResources() != null)
                    return getResources().GetValue<float>("_currentOxygen");
                else
                    return maxOxygenSeconds;
            }
            set
            {
                if (getResources() != null)
                {
                    getResources().SetValue("_currentOxygen", value);
                }
            }
        }
        public static float maxOxygenSeconds { get; set; } = 450f;
        public static bool invincible
        {
            get
            {
                if (getResources() != null)
                    return getResources().GetValue<bool>("_invincible");
                else
                    return false;
            }
            set
            {
                if (getResources() != null)
                {
                    getResources().SetValue("_invincible", value);
                }
            }
        }

        public static void Update()
        {
            if (hasUnlimitedBoost)
                boostSeconds = maxBoostSeconds;
            if (hasUnlimitedFuel)
                fuelSeconds = maxFuelSeconds;
            if (hasUnlimitedOxygen)
                oxygenSeconds = maxOxygenSeconds;
            if (hasUnlimitedHealth)
                health = maxHealth;
            invincible = isInvincible;

            if (boostSeconds > maxBoostSeconds)
                boostSeconds = maxBoostSeconds;
            if (fuelSeconds > maxFuelSeconds)
                fuelSeconds = maxFuelSeconds;
            if (oxygenSeconds > maxOxygenSeconds)
                oxygenSeconds = maxOxygenSeconds;
            if (health > maxHealth)
                health = maxHealth;
        }
    }
}
