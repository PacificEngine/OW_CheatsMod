using OWML.Common;
using OWML.ModHelper;
using OWML.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ClassLibrary2
{
    static class QuantumMoonHelper
    {
        public static AstroObject.Name getState()
        {
            switch (Locator.GetAstroObject(AstroObject.Name.QuantumMoon).GetComponentInChildren<QuantumMoon>().GetStateIndex())
            {
                case 0:
                    return AstroObject.Name.HourglassTwins;
                case 1:
                    return AstroObject.Name.TimberHearth;
                case 2:
                    return AstroObject.Name.BrittleHollow;
                case 3:
                    return AstroObject.Name.GiantsDeep;
                case 4:
                    return AstroObject.Name.DarkBramble;
                case 5:
                    return AstroObject.Name.Eye;
                default:
                    return AstroObject.Name.None;
            }
        }

        public static void setState(AstroObject.Name state)
        {
            int index = -1;
            switch (state)
            {
                case AstroObject.Name.HourglassTwins:
                case AstroObject.Name.TowerTwin:
                case AstroObject.Name.CaveTwin:
                    index = 0; break;
                case AstroObject.Name.TimberHearth:
                    index = 1; break;
                case AstroObject.Name.BrittleHollow:
                    index = 2; break;
                case AstroObject.Name.GiantsDeep:
                    index = 3; break;
                case AstroObject.Name.DarkBramble:
                    index = 4; break;
                case AstroObject.Name.Eye:
                    index = 5; break;
                default:
                    index = -1; break;
            }
            Locator.GetAstroObject(AstroObject.Name.QuantumMoon).GetComponentInChildren<QuantumMoon>().Invoke("SetSurfaceState", index);
        }

        public static void nextState()
        {
            var index = Locator.GetAstroObject(AstroObject.Name.QuantumMoon).GetComponentInChildren<QuantumMoon>().GetStateIndex();
            Locator.GetAstroObject(AstroObject.Name.QuantumMoon).GetComponentInChildren<QuantumMoon>().Invoke("SetSurfaceState", (index + 1) % 5);
        }

        public static void previousState()
        {
            var index = Locator.GetAstroObject(AstroObject.Name.QuantumMoon).GetComponentInChildren<QuantumMoon>().GetStateIndex();
            Locator.GetAstroObject(AstroObject.Name.QuantumMoon).GetComponentInChildren<QuantumMoon>().Invoke("SetSurfaceState", (index - 1) % 5);
        }

        public static void collapse()
        {
            Locator.GetAstroObject(AstroObject.Name.QuantumMoon).GetComponentInChildren<QuantumMoon>().Invoke("Collapse", true);
        }
    }
}
