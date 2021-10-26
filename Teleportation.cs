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
                teleportObjectTo(teleportObject,
                    new Vector3(teleportTo.GetPosition().x + position.x, teleportTo.GetPosition().y + position.y, teleportTo.GetPosition().z + position.z),
                    new Vector3(teleportTo.GetVelocity().x + velocity.x, teleportTo.GetVelocity().y + velocity.y, teleportTo.GetVelocity().z + velocity.z),
                    new Vector3(teleportTo.GetAngularVelocity().x + angularVelocity.x, teleportTo.GetAngularVelocity().y + angularVelocity.y, teleportTo.GetAngularVelocity().z + angularVelocity.z),
                    new Vector3(teleportTo.GetAcceleration().x + acceleration.x, teleportTo.GetAcceleration().y + acceleration.y, teleportTo.GetAcceleration().z + acceleration.z),
                    new Quaternion(teleportTo.GetRotation().x + rotation.x, teleportTo.GetRotation().y + rotation.y, teleportTo.GetRotation().z + rotation.z, teleportTo.GetRotation().w + rotation.w));
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
