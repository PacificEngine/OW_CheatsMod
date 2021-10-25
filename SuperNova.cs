using OWML.Common;
using OWML.ModHelper;
using OWML.ModHelper.Events;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace ClassLibrary2
{
    static class SuperNova
    {
        private static float supernovaTimer = -1f;
        public static void Reset()
        {
            supernovaTimer = -1f;
        }

        public static void Update()
        {
            if (supernovaTimer >= 0f)
            {
                TimeLoop.SetSecondsRemaining(supernovaTimer);
            }
        }

        public static void set(float seconds)
        {
            TimeLoop.SetSecondsRemaining(seconds);
            if (supernovaTimer >= 0f)
            {
                supernovaTimer = TimeLoop.GetSecondsRemaining();
            }
        }

        public static void adjust(float amount)
        {
            TimeLoop.SetSecondsRemaining(TimeLoop.GetSecondsRemaining() + amount);
            if (supernovaTimer >= 0f)
            {
                supernovaTimer = TimeLoop.GetSecondsRemaining();
            }
        }

        public static void freeze(bool stopTime)
        {
            if (stopTime)
            {
                supernovaTimer = TimeLoop.GetSecondsRemaining();
            }
            else
            {
                supernovaTimer = -1f;
            }
        }

        public static bool isFrozen()
        {
            return supernovaTimer >= 0f;
        }
    }
}
