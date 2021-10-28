using OWML.Common;
using OWML.ModHelper;
using OWML.ModHelper.Events;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ClassLibrary2
{
    class Teleportation
    {
        public static void teleportPlayerToSun()
        {
            if (Locator.GetPlayerBody() && Locator.GetAstroObject(AstroObject.Name.Sun))
            {
                ignoreSand(false);
                teleportPlayerTo(Locator.GetAstroObject(AstroObject.Name.Sun).GetAttachedOWRigidbody(), new Vector3(0f, 5000f, 0f), Vector3.zero, Vector3.zero, Vector3.zero, Quaternion.identity);
            }
        }

        public static void teleportPlayerToSunStation()
        {
            if (Locator.GetPlayerBody() && Locator.GetWarpReceiver(NomaiWarpPlatform.Frequency.SunStation))
            {
                ignoreSand(false);
                teleportPlayerTo(Locator.GetWarpReceiver(NomaiWarpPlatform.Frequency.SunStation).GetAttachedOWRigidbody(), Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Quaternion.identity);
            }
        }

        public static void teleportPlayerToEmberTwin()
        {
            if (Locator.GetPlayerBody() && Locator.GetAstroObject(AstroObject.Name.CaveTwin))
            {
                ignoreSand(false);
                teleportPlayerTo(Locator.GetAstroObject(AstroObject.Name.CaveTwin).GetAttachedOWRigidbody(), new Vector3(0f, 165f, 0f), Vector3.zero, Vector3.zero, Vector3.zero, Quaternion.identity);
            }
        }

        public static void teleportPlayerToAshTwin()
        {
            if (Locator.GetPlayerBody() && Locator.GetAstroObject(AstroObject.Name.TowerTwin))
            {
                ignoreSand(false);
                teleportPlayerTo(Locator.GetAstroObject(AstroObject.Name.TowerTwin).GetAttachedOWRigidbody(), new Vector3(0f, 180f, 0f), Vector3.zero, Vector3.zero, Vector3.zero, Quaternion.identity);
            }
        }

        public static void teleportPlayerToAshTwinProject()
        {
            if (Locator.GetPlayerBody() && Locator.GetAstroObject(AstroObject.Name.TowerTwin))
            {

                var planet = Locator.GetAstroObject(AstroObject.Name.TowerTwin).GetAttachedOWRigidbody();
                var platform = Locator.GetWarpReceiver(NomaiWarpPlatform.Frequency.TimeLoop).GetPlatformCenter();
                var localPosition = platform.position - planet.GetPosition();//new Vector3(platform.position.x - planet.GetPosition().x, platform.position.y - planet.GetPosition().y, platform.position.z - planet.GetPosition().z);
                float ratio = 0f;
                if (!PlayerState.IsInsideShip())
                {
                    ratio = (localPosition.magnitude - 1.85f) / localPosition.magnitude;
                }
                else
                {
                    ratio = (localPosition.magnitude - 6f) / localPosition.magnitude;
                }
                var position = new Vector3(localPosition.x * ratio + planet.GetPosition().x, localPosition.y * ratio + planet.GetPosition().y, localPosition.z * ratio + planet.GetPosition().z);

                ignoreSand(true);
                teleportPlayerTo(position, planet.GetPointVelocity(position), Vector3.zero, planet.GetAcceleration(), platform.rotation);
            }
        }

        public static void teleportPlayerToTimberHearth()
        {
            if (Locator.GetPlayerBody() && Locator.GetAstroObject(AstroObject.Name.TimberHearth))
            {
                ignoreSand(false);
                teleportPlayerTo(Locator.GetAstroObject(AstroObject.Name.TimberHearth).GetAttachedOWRigidbody(), new Vector3(0f, 280f, 0f), Vector3.zero, Vector3.zero, Vector3.zero, Quaternion.identity);
            }
        }

        public static void teleportPlayerToTimberHearthProbe()
        {
            if (Locator.GetPlayerBody() && Locator.GetAstroObject(AstroObject.Name.TimberHearth))
            {
                ignoreSand(false);
                teleportPlayerTo(Locator.GetAstroObject(AstroObject.Name.TimberHearth).GetSatellite().GetAttachedOWRigidbody(), -new Vector3(1f, 0f, 0f), Vector3.zero, Vector3.zero, Vector3.zero, Quaternion.identity);
            }
        }

        public static void teleportPlayerToAttlerock()
        {
            if (Locator.GetPlayerBody() && Locator.GetAstroObject(AstroObject.Name.TimberHearth))
            {
                ignoreSand(false);
                teleportPlayerTo(Locator.GetAstroObject(AstroObject.Name.TimberHearth).GetMoon().GetAttachedOWRigidbody(), new Vector3(0f, 85f, 0f), Vector3.zero, Vector3.zero, Vector3.zero, Quaternion.identity);
            }
        }

        public static void teleportPlayerToBlackHoleForgeTeleporter()
        {
            if (Locator.GetPlayerBody() && Locator.GetWarpReceiver(NomaiWarpPlatform.Frequency.BrittleHollowForge))
            {
                var planet = Locator.GetAstroObject(AstroObject.Name.BrittleHollow).GetAttachedOWRigidbody();
                var platform = Locator.GetWarpReceiver(NomaiWarpPlatform.Frequency.BrittleHollowForge).GetPlatformCenter();
                ignoreSand(false);
                teleportPlayerTo(new Vector3(platform.position.x, platform.position.y - 2f, platform.position.z), planet.GetVelocity(), planet.GetAngularVelocity(), planet.GetAcceleration(), platform.rotation);
            }
        }

        public static void teleportPlayerToHollowLattern()
        {
            if (Locator.GetPlayerBody() && Locator.GetAstroObject(AstroObject.Name.BrittleHollow))
            {
                ignoreSand(false);
                teleportPlayerTo(Locator.GetAstroObject(AstroObject.Name.BrittleHollow).GetMoon().GetAttachedOWRigidbody(), new Vector3(0f, 150f, 0f), Vector3.zero, Vector3.zero, Vector3.zero, Quaternion.identity);
            }
        }

        public static void teleportPlayerToGiantsDeep()
        {
            if (Locator.GetPlayerBody() && Locator.GetAstroObject(AstroObject.Name.GiantsDeep))
            {
                ignoreSand(false);
                if (!PlayerState.IsInsideShip())
                {
                    teleportPlayerTo(Locator.GetAstroObject(AstroObject.Name.GiantsDeep).GetAttachedOWRigidbody(), new Vector3(0f, 505f, 0f), Vector3.zero, Vector3.zero, Vector3.zero, Quaternion.identity);
                }
                else
                {
                    teleportPlayerTo(Locator.GetAstroObject(AstroObject.Name.GiantsDeep).GetAttachedOWRigidbody(), new Vector3(0f, 520f, 0f), Vector3.zero, Vector3.zero, Vector3.zero, Quaternion.identity);
                }
            }
        }

        public static void teleportPlayerToProbeCannon()
        {
            if (Locator.GetPlayerBody() && Locator.GetAstroObject(AstroObject.Name.ProbeCannon))
            {
                ignoreSand(false);
                teleportPlayerTo(Locator.GetAstroObject(AstroObject.Name.ProbeCannon).GetAttachedOWRigidbody(), Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Quaternion.identity);
            }
        }

        public static void teleportPlayerToDarkBramble()
        {
            if (Locator.GetPlayerBody() && Locator.GetAstroObject(AstroObject.Name.DarkBramble))
            {
                ignoreSand(false);
                teleportPlayerTo(Locator.GetAstroObject(AstroObject.Name.DarkBramble).GetAttachedOWRigidbody(), new Vector3(0f, 950f, 0f), Vector3.zero, Vector3.zero, Vector3.zero, Quaternion.identity);
            }
        }

        public static void teleportPlayerToShip()
        {
            if (Locator.GetPlayerBody() && Locator.GetShipBody() && !PlayerState.IsInsideShip())
            {
                ignoreSand(false);
                teleportObjectTo(Locator.GetPlayerBody(), Locator.GetShipBody(), Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Quaternion.identity);
            }
        }

        public static void teleportPlayerToProbe()
        {
            if (Locator.GetPlayerBody() && Locator.GetProbe())
            {
                ignoreSand(false);
                teleportPlayerTo(Locator.GetProbe().GetAttachedOWRigidbody(), Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Quaternion.identity);
            }
        }

        public static void teleportShipToPlayer()
        {
            if (Locator.GetPlayerBody() && Locator.GetShipBody() && !PlayerState.IsInsideShip())
            {
                ignoreSand(false);
                teleportObjectTo(Locator.GetShipBody(), Locator.GetPlayerBody(), Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Quaternion.identity);
            }
        }

        public static void teleportPlayerToInterloper()
        {
            if (Locator.GetPlayerBody() && Locator.GetAstroObject(AstroObject.Name.Comet))
            {
                ignoreSand(false);
                teleportPlayerTo(Locator.GetAstroObject(AstroObject.Name.Comet).GetAttachedOWRigidbody(), new Vector3(0f, 85f, 0f), Vector3.zero, Vector3.zero, Vector3.zero, Quaternion.identity);
            }
        }

        public static void teleportPlayerToWhiteHole()
        {
            if (Locator.GetPlayerBody() && Locator.GetAstroObject(AstroObject.Name.WhiteHole))
            {
                ignoreSand(false);
                teleportPlayerTo(Locator.GetAstroObject(AstroObject.Name.WhiteHole).GetAttachedOWRigidbody(), new Vector3(40f, 0f, 0f), Vector3.zero, Vector3.zero, Vector3.zero, Quaternion.identity);
            }
        }

        public static void teleportPlayerToWhiteHoleStation()
        {
            if (Locator.GetPlayerBody() && Locator.GetAstroObject(AstroObject.Name.WhiteHoleTarget))
            {
                ignoreSand(false);
                teleportPlayerTo(Locator.GetAstroObject(AstroObject.Name.WhiteHoleTarget).GetAttachedOWRigidbody(), new Vector3(0f, 0f, 0f), Vector3.zero, Vector3.zero, Vector3.zero, Quaternion.identity);
            }
        }

        public static void teleportPlayerToStranger()
        {
            if (Locator.GetPlayerBody() && Locator.GetAstroObject(AstroObject.Name.RingWorld))
            {
                ignoreSand(false);
                Teleportation.teleportPlayerTo(Locator.GetAstroObject(AstroObject.Name.RingWorld).GetAttachedOWRigidbody(), new Vector3(-320f, -320f, -50f), Vector3.zero, Vector3.zero, Vector3.zero, Quaternion.identity);
            }
        }

        public static void teleportPlayerToDreamWorld()
        {
            if (Locator.GetPlayerBody() && Locator.GetAstroObject(AstroObject.Name.DreamWorld))
            {
                ignoreSand(false);
                Teleportation.teleportPlayerTo(Locator.GetAstroObject(AstroObject.Name.DreamWorld).GetAttachedOWRigidbody(), new Vector3(0f, 100f, 0f), Vector3.zero, Vector3.zero, Vector3.zero, Quaternion.identity);
            }
        }
        public static void teleportPlayerToQuantumMoon()
        {
            if (Locator.GetPlayerBody() && Locator.GetAstroObject(AstroObject.Name.QuantumMoon))
            {
                ignoreSand(false);
                Teleportation.teleportPlayerTo(Locator.GetAstroObject(AstroObject.Name.QuantumMoon).GetAttachedOWRigidbody(), new Vector3(0f, -110f, 0f), Vector3.zero, Vector3.zero, Vector3.zero, Quaternion.identity);
            }
        }

        private static void ignoreSand(bool ignore)
        {
            if (PlayerState.IsInsideShip())
            {
                foreach (SandLevelController sandLevelController in UnityEngine.Object.FindObjectsOfType<SandLevelController>())
                    foreach (Collider componentsInChild in Locator.GetShipBody().GetComponentsInChildren<Collider>())
                        Physics.IgnoreCollision(sandLevelController.GetSandCollider(), componentsInChild, ignore);
            }
            else if (ignore)
            {
                GlobalMessenger<OWRigidbody>.FireEvent("EnterTimeLoopCentral", Locator.GetPlayerBody());
            }
            else
            {
                GlobalMessenger<OWRigidbody>.FireEvent("ExitTimeLoopCentral", Locator.GetPlayerBody());
            }
        }

        private static OWRigidbody getPlayerBody()
        {
            if (PlayerState.IsInsideShip())
            {
                return Locator.GetShipBody();
            }
            else
            {
                return Locator.GetPlayerBody();
            }
        }

        public static void teleportPlayerTo(OWRigidbody teleportTo, Vector3 position, Vector3 velocity, Vector3 angularVelocity, Vector3 acceleration, Quaternion rotation)
        {
            if (teleportTo)
            {
                teleportObjectTo(getPlayerBody(), teleportTo, position, velocity, angularVelocity, acceleration, rotation);
            }
        }

        public static void teleportPlayerTo(Vector3 position, Vector3 velocity, Vector3 angularVelocity, Vector3 acceleration, Quaternion rotation)
        {
            teleportObjectTo(getPlayerBody(), position, velocity, angularVelocity, acceleration, rotation);
        }

        public static void teleportObjectTo(OWRigidbody teleportObject, OWRigidbody teleportTo, Vector3 position, Vector3 velocity, Vector3 angularVelocity, Vector3 acceleration, Quaternion rotation)
        {
            if (teleportTo)
            {
                var newPosition = position + teleportTo.GetPosition();
                var newVelocity = velocity + teleportTo.GetVelocity();
                var newAnglarVelocity = angularVelocity + teleportTo.GetAngularVelocity();
                var newAcceleration = acceleration + teleportTo.GetAcceleration();
                var parentRotation = teleportTo.GetRotation();
                var newRotation = new Quaternion(parentRotation.x + rotation.x, parentRotation.y + rotation.y, parentRotation.z + rotation.z, parentRotation.w + rotation.w);
                teleportObjectTo(teleportObject, newPosition, newVelocity, newAnglarVelocity, newAcceleration, newRotation);
            }
        }

        public static void teleportObjectTo(OWRigidbody teleportObject, Vector3 position, Vector3 velocity, Vector3 angularVelocity, Vector3 acceleration, Quaternion rotation)
        {
            teleportObject.SetPosition(new Vector3(position.x, position.y, position.z));
            teleportObject.SetVelocity(new Vector3(velocity.x, velocity.y, velocity.z));
            teleportObject.SetAngularVelocity(new Vector3(angularVelocity.x, angularVelocity.y, angularVelocity.z));
            teleportObject.SetRotation(new Quaternion(rotation.x, rotation.y, rotation.z, rotation.w));

            teleportObject.SetValue("_lastPosition", new Vector3(position.x, position.y, position.z));
            teleportObject.SetValue("_currentVelocity", new Vector3(velocity.x, velocity.y, velocity.z));
            teleportObject.SetValue("_lastVelocity", new Vector3(velocity.x, velocity.y, velocity.z));
            teleportObject.SetValue("_currentAngularVelocity", new Vector3(angularVelocity.x, angularVelocity.y, angularVelocity.z));
            teleportObject.SetValue("_lastAngularVelocity", new Vector3(angularVelocity.x, angularVelocity.y, angularVelocity.z));
            teleportObject.SetValue("_currentAccel", new Vector3(acceleration.x, acceleration.y, acceleration.z));
            teleportObject.SetValue("_lastAccel", new Vector3(acceleration.x, acceleration.y, acceleration.z));
        }
    }
}
