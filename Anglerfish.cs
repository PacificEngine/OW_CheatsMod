using OWML.Common;
using OWML.ModHelper;
using OWML.ModHelper.Events;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace ClassLibrary2
{
    static class Anglerfish
    {
        private static bool _enabledAI = true;
        private static float _acceleration = 2f;
        private static float _investigateSpeed = 20f;
        private static float _chaseSpeed = 42f;
        private static float _turnSpeed = 180f;
        private static float _quickTurnSpeed = 360f;
        private static float _mouthOpenDistance = 100f;
        private static float _pursueDistance = 200f;
        private static float _escapeDistance = 500f;

        public static bool enabledAI { get { return _enabledAI; } set { _enabledAI = value; updateAnglerfish(); } }
        public static float acceleration { get { return _acceleration; } set { _acceleration = value; updateAnglerfish(); } }
        public static float investigateSpeed { get { return _investigateSpeed; } set { _investigateSpeed = value; updateAnglerfish(); } }
        public static float chaseSpeed { get { return _chaseSpeed; } set { _chaseSpeed = value; updateAnglerfish(); } }
        public static float turnSpeed { get { return _turnSpeed; } set { _turnSpeed = value; updateAnglerfish(); } }
        public static float quickTurnSpeed { get { return _quickTurnSpeed; } set { _quickTurnSpeed = value; updateAnglerfish(); } }
        public static float mouthOpenDistance { get { return _mouthOpenDistance; } set { _mouthOpenDistance = value; updateAnglerfish(); } }
        public static float pursueDistance { get { return _pursueDistance; } set { _pursueDistance = value; updateAnglerfish(); } }
        public static float escapeDistance { get { return _escapeDistance; } set { _escapeDistance = value; updateAnglerfish(); } }

        public static void Start()
        {
        }

        public static void Awake()
        {
            foreach (Sector sector in SectorManager.GetRegisteredSectors())
            {
                if (Sector.Name.BrambleDimension.Equals(sector.GetName()))
                {
                    sector.OnSectorOccupantsUpdated += () => updateAnglerfish();
                }
            }
        }

        public static void Destory()
        {
        }

        public static void Update()
        {
        }

        public static void onEnter()
        {
        }

        public static void onLeave()
        {
        }

        private static void updateAnglerfish()
        {
            foreach (AnglerfishController anglerfishController in UnityEngine.Object.FindObjectsOfType<AnglerfishController>())
            {
                anglerfishController.enabled = enabledAI;
               /* anglerfishController.SetValue("_acceleration", acceleration);
                anglerfishController.SetValue("_investigateSpeed", investigateSpeed);
                anglerfishController.SetValue("_chaseSpeed", chaseSpeed);
                anglerfishController.SetValue("_turnSpeed", turnSpeed);
                anglerfishController.SetValue("_quickTurnSpeed", quickTurnSpeed);
                anglerfishController.SetValue("_arrivalDistance", mouthOpenDistance);
                anglerfishController.SetValue("_pursueDistance", pursueDistance);
                anglerfishController.SetValue("_escapeDistance", escapeDistance);
               */
            }
        }
    }
}
