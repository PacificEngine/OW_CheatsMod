using OWML.Common;
using OWML.ModHelper;
using OWML.Utils;
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
            var parent = Position.getBody(Position.HeavenlyBodies.Sun);
            if (Locator.GetPlayerBody() && parent)
            {
                ignoreSand(false);
                teleportPlayerTo(parent, new Vector3(0, 5000f, 0), Vector3.zero, Vector3.zero, Vector3.zero, Quaternion.identity);
            }
        }

        public static void teleportPlayerToSunStation()
        {
            var parent = Position.getBody(Position.HeavenlyBodies.SunStation);
            if (Locator.GetPlayerBody() && parent)
            {
                ignoreSand(false);
                teleportPlayerTo(parent, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Quaternion.identity);
            }
        }

        public static void teleportPlayerToEmberTwin()
        {
            var parent = Position.getBody(Position.HeavenlyBodies.EmberTwin);
            if (Locator.GetPlayerBody() && parent)
            {
                ignoreSand(false);
                teleportPlayerTo(parent, new Vector3(0, 165f, 0), Vector3.zero, Vector3.zero, Vector3.zero, Quaternion.identity);
            }
        }

        public static void teleportPlayerToAshTwin()
        {
            var parent = Position.getBody(Position.HeavenlyBodies.AshTwin);
            if (Locator.GetPlayerBody() && parent)
            {
                ignoreSand(false);
                teleportPlayerTo(parent, new Vector3(0, 180f, 0), Vector3.zero, Vector3.zero, Vector3.zero, Quaternion.identity);
            }
        }

        public static void teleportPlayerToAshTwinProject()
        {
            var planet = Position.getBody(Position.HeavenlyBodies.AshTwin);
            if (Locator.GetPlayerBody() && planet)
            {
                var platform = Locator.GetWarpReceiver(NomaiWarpPlatform.Frequency.TimeLoop).GetPlatformCenter();
                var localPosition = platform.position - planet.GetPosition();
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
            var parent = Position.getBody(Position.HeavenlyBodies.TimberHearth);
            if (Locator.GetPlayerBody() && parent)
            {
                ignoreSand(false);
                teleportPlayerTo(parent, new Vector3(0, 280f, 0), Vector3.zero, Vector3.zero, Vector3.zero, Quaternion.identity);
            }
        }

        public static void teleportPlayerToTimberHearthProbe()
        {
            var parent = Position.getBody(Position.HeavenlyBodies.TimberHearthProbe);
            if (Locator.GetPlayerBody() && parent)
            {
                ignoreSand(false);
                if (!PlayerState.IsInsideShip())
                {
                    teleportPlayerTo(parent, new Vector3(0, 0, -1f), Vector3.zero, Vector3.zero, Vector3.zero, Quaternion.identity);
                }
                else
                {
                    teleportPlayerTo(parent, new Vector3(0, 0, -8f), Vector3.zero, Vector3.zero, Vector3.zero, Quaternion.identity);
                }
                
            }
        }

        public static void teleportPlayerToAttlerock()
        {
            var parent = Position.getBody(Position.HeavenlyBodies.Attlerock);
            if (Locator.GetPlayerBody() && parent)
            {
                ignoreSand(false);
                teleportPlayerTo(parent, new Vector3(0f, 85f, 0f), Vector3.zero, Vector3.zero, Vector3.zero, Quaternion.identity);
            }
        }

        public static void teleportPlayerToBlackHoleForgeTeleporter()
        {
            var planet = Position.getBody(Position.HeavenlyBodies.BrittleHollow);
            if (Locator.GetPlayerBody() && planet)
            {
                var platform = Locator.GetWarpReceiver(NomaiWarpPlatform.Frequency.BrittleHollowForge).GetPlatformCenter();
                ignoreSand(false);
                teleportPlayerTo(new Vector3(platform.position.x, platform.position.y - 2f, platform.position.z), planet.GetVelocity(), planet.GetAngularVelocity(), planet.GetAcceleration(), platform.rotation);
            }
        }

        public static void teleportPlayerToHollowLattern()
        {
            var parent = Position.getBody(Position.HeavenlyBodies.HollowLantern);
            if (Locator.GetPlayerBody() && parent)
            {
                ignoreSand(false);
                if (!PlayerState.IsInsideShip())
                {
                    teleportPlayerTo(parent, new Vector3(30.3f, 92.8f, 34.2f), Vector3.zero, Vector3.zero, Vector3.zero, Quaternion.identity);
                }
                else
                {
                    teleportPlayerTo(parent, new Vector3(27.9f, 98.6f, 34.7f), Vector3.zero, Vector3.zero, Vector3.zero, Quaternion.identity);
                }
            }
        }

        public static void teleportPlayerToGiantsDeep()
        {
            var parent = Position.getBody(Position.HeavenlyBodies.GiantsDeep);
            if (Locator.GetPlayerBody() && parent)
            {
                ignoreSand(false);
                GlobalMessenger.FireEvent("PlayerEnterGiantsDeep");
                if (!PlayerState.IsInsideShip())
                {
                    teleportPlayerTo(parent, new Vector3(0f, 505f, 0f), Vector3.zero, Vector3.zero, Vector3.zero, Quaternion.identity);
                }
                else
                {
                    teleportPlayerTo(parent, new Vector3(0f, 520f, 0f), Vector3.zero, Vector3.zero, Vector3.zero, Quaternion.identity);
                }
            }
        }

        public static void teleportPlayerToProbeCannon()
        {
            var parent = Position.getBody(Position.HeavenlyBodies.ProbeCannon);
            if (Locator.GetPlayerBody() && parent)
            {
                ignoreSand(false);
                teleportPlayerTo(parent, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Quaternion.identity);
            }
        }

        public static void teleportPlayerToDarkBramble()
        {
            var parent = Position.getBody(Position.HeavenlyBodies.DarkBramble);
            if (Locator.GetPlayerBody())
            {
                ignoreSand(false);
                teleportPlayerTo(parent, new Vector3(0f, 950f, 0f), Vector3.zero, Vector3.zero, Vector3.zero, Quaternion.identity);
            }
        }

        public static void teleportPlayerToVessel()
        {
            var parent = Position.getBody(Position.HeavenlyBodies.InnerDarkBramble_Vessel);
            if (Locator.GetPlayerBody() && parent)
            {
                ignoreSand(false);
                Teleportation.teleportPlayerTo(parent, new Vector3(149.1f, 11.9f, -8.6f), Vector3.zero, Vector3.zero, Vector3.zero, Quaternion.identity);
                if (PlayerState.IsInsideShip())
                {
                    GlobalMessenger.FireEvent("WarpPlayer");
                    GlobalMessenger.FireEvent("EnterShip");
                }
                else
                {
                    GlobalMessenger.FireEvent("WarpPlayer");
                }
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

        public static void teleportPlayerToNomaiProbe()
        {
            var parent = Position.getBody(Position.HeavenlyBodies.NomaiProbe);
            if (Locator.GetPlayerBody() && parent)
            {
                ignoreSand(false);
                teleportPlayerTo(parent, new Vector3(0f, 0f, -25f), Vector3.zero, Vector3.zero, Vector3.zero, Quaternion.identity);
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
            var parent = Position.getBody(Position.HeavenlyBodies.Interloper);
            if (Locator.GetPlayerBody() && parent)
            {
                ignoreSand(false);
                teleportPlayerTo(parent, new Vector3(0f, 85f, 0f), Vector3.zero, Vector3.zero, Vector3.zero, Quaternion.identity);
            }
        }

        public static void teleportPlayerToWhiteHole()
        {
            var parent = Position.getBody(Position.HeavenlyBodies.WhiteHole);
            if (Locator.GetPlayerBody() && parent)
            {
                ignoreSand(false);
                teleportPlayerTo(parent, new Vector3(0f, 0f, 40f), Vector3.zero, Vector3.zero, Vector3.zero, Quaternion.identity);
            }
        }

        public static void teleportPlayerToWhiteHoleStation()
        {
            var parent = Position.getBody(Position.HeavenlyBodies.WhiteHoleStation);
            if (Locator.GetPlayerBody() && parent)
            {
                ignoreSand(false);
                teleportPlayerTo(parent, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Quaternion.identity);
            }
        }

        public static void teleportPlayerToStranger()
        {
            var parent = Position.getBody(Position.HeavenlyBodies.Stranger);
            if (Locator.GetPlayerBody() && parent)
            {
                ignoreSand(false);
                Teleportation.teleportPlayerTo(parent, new Vector3(45.5f, -169f, -290f), Vector3.zero, Vector3.zero, Vector3.zero, Quaternion.identity);
            }
        }

        public static void teleportPlayerToDreamWorld()
        {
            var parent = Position.getBody(Position.HeavenlyBodies.DreamWorld);
            if (Locator.GetPlayerBody() && parent)
            {
                ignoreSand(false);
                Teleportation.teleportPlayerTo(parent, new Vector3(0f, 100f, 0f), Vector3.zero, Vector3.zero, Vector3.zero, Quaternion.identity);
            }
        }

        public static void teleportPlayerToQuantumMoon()
        {
            var parent = Position.getBody(Position.HeavenlyBodies.QuantumMoon);
            if (Locator.GetPlayerBody() && parent)
            {
                ignoreSand(false);
                Teleportation.teleportPlayerTo(parent, new Vector3(0f, 80f, 0f), Vector3.zero, Vector3.zero, Vector3.zero, Quaternion.identity);
                if (PlayerState.IsInsideShip())
                {
                    GlobalMessenger.FireEvent("WarpPlayer");
                    GlobalMessenger.FireEvent("EnterShip");
                }
                else
                {
                    GlobalMessenger.FireEvent("WarpPlayer");
                }
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
            GlobalMessenger<OWRigidbody>.FireEvent(ignore ? "EnterTimeLoopCentral" : "ExitTimeLoopCentral", Locator.GetPlayerBody());
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
                var newPosition = teleportTo.transform.TransformPoint(position);
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
