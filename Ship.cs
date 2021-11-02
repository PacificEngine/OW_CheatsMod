using OWML.Common;
using OWML.ModHelper;
using OWML.ModHelper.Events;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ClassLibrary2
{
    class Ship
    {
        public static ShipResources getResources()
        {
            ShipResources resources = null;
            if (Locator.GetShipTransform() && Locator.GetShipTransform().TryGetComponent<ShipResources>(out resources))
            {
                return resources;
            }
            return null;
        }

        public static ShipDamageController getDamageController()
        {
            ShipDamageController controller = null;
            if (Locator.GetShipTransform() && Locator.GetShipTransform().TryGetComponent<ShipDamageController>(out controller))
            {
                return controller;
            }
            return null;
        }

        public static bool hasUnlimitedFuel { get; set; }
        public static bool hasUnlimitedOxygen { get; set; }
        public static bool isInvincible { get; set; }

        public static float fuelSeconds
        {
            get
            {
                if (getResources() != null)
                    return getResources().GetFuel();
                else
                    return maxFuelSeconds;
            }
            set
            {
                if (getResources() != null)
                {
                    getResources().SetFuel(value);
                }
            }
        }
        public static float maxFuelSeconds { get; set; } = 10000f;
        public static float oxygenSeconds
        {
            get
            {
                if (getResources() != null)
                    return getResources().GetOxygen();
                else
                    return maxOxygenSeconds;
            }
            set
            {
                if (getResources() != null)
                {
                    getResources().SetOxygen(value);
                }
            }
        }
        public static float maxOxygenSeconds { get; set; } = 6000f;
        public static bool invincible
        {
            get
            {
                if (getDamageController() != null)
                    return getDamageController().GetValue<bool>("_invincible");
                else
                    return false;
            }
            set
            {
                if (getDamageController() != null)
                {
                    getDamageController().SetValue("_invincible", value);
                }
            }
        }
        public static bool collision
        {
            get
            {
                if (Locator.GetShipBody())
                {
                    if (!Locator.GetShipBody().GetRequiredComponent<Rigidbody>().detectCollisions)
                    {
                        return false;
                    }
                }
                return true;
            }
            set
            {
                if (Locator.GetShipBody())
                {
                    if (!value)
                    {
                        Locator.GetShipBody().DisableCollisionDetection();
                    }
                    else
                    {
                        Locator.GetShipBody().EnableCollisionDetection();
                    }

                    foreach (Collider collider in Locator.GetShipBody().GetComponentsInChildren<Collider>())
                    {
                        if (!collider.isTrigger)
                        {
                            collider.enabled = value;
                        }
                    }
                }
            }
        }

        public static void Update()
        {
            if (hasUnlimitedFuel)
                fuelSeconds = maxFuelSeconds;
            if (hasUnlimitedOxygen)
                oxygenSeconds = maxOxygenSeconds;
            invincible = isInvincible;

            if (fuelSeconds > maxFuelSeconds)
                fuelSeconds = maxFuelSeconds;
            if (oxygenSeconds > maxOxygenSeconds)
                oxygenSeconds = maxOxygenSeconds;
        }

        public static void repair()
        {
            if (Locator.GetShipTransform())
            {
                foreach (ShipHull hull in Locator.GetShipTransform().GetComponentsInChildren<ShipHull>())
                {
                    hull.SetValue("_damaged", false);
                    hull.SetValue("_integrity", 1f);
                }
                foreach (ShipComponent component in Locator.GetShipTransform().GetComponentsInChildren<ShipComponent>())
                {
                    component.SetDamaged(false);
                }
            }
        }
    }
}
