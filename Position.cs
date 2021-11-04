using OWML.Common;
using OWML.ModHelper;
using OWML.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace ClassLibrary2
{
    static class Position
    {
        private static float lastUpdate = 0f;
        public static bool debugMode { get; set; } = false;

        private static ScreenPrompt parentPrompt = new ScreenPrompt("");
        private static ScreenPrompt positionPrompt = new ScreenPrompt("");
        private static ScreenPrompt velocityPrompt = new ScreenPrompt("");

        private delegate OWRigidbody body();
        public delegate Vector3 vector();

        public enum HeavenlyBodies {
            Sun,
            SunStation,
            AshTwin,
            EmberTwin,
            TimberHearth,
            TimberHearthProbe,
            Attlerock,
            BrittleHollow,
            HollowLantern,
            GiantsDeep,
            ProbeCannon,
            NomaiProbe,
            DarkBramble,
            InnerDarkBramble_Hub,
            InnerDarkBramble_EscapePod,
            InnerDarkBramble_Nest,
            InnerDarkBramble_Feldspar,
            InnerDarkBramble_Gutter,
            InnerDarkBramble_Vessel,
            InnerDarkBramble_Maze,
            InnerDarkBramble_SmallNest,
            Interloper,
            WhiteHole,
            WhiteHoleStation,
            Stranger,
            DreamWorld,
            QuantumMoon,
            EyeOfTheUniverse,
            EyeOfTheUniverse_Vessel
        }

        private static Dictionary<HeavenlyBodies, body> bodies = new Dictionary<HeavenlyBodies, body>();

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

        public static void Start()
        {
            bodies.Clear();
            bodies.Add(HeavenlyBodies.Sun, () => Locator.GetAstroObject(AstroObject.Name.Sun)?.GetAttachedOWRigidbody());
            bodies.Add(HeavenlyBodies.SunStation, () => Locator.GetWarpReceiver(NomaiWarpPlatform.Frequency.SunStation)?.GetAttachedOWRigidbody());
            bodies.Add(HeavenlyBodies.AshTwin, () => Locator.GetAstroObject(AstroObject.Name.TowerTwin)?.GetAttachedOWRigidbody());
            bodies.Add(HeavenlyBodies.EmberTwin, () => Locator.GetAstroObject(AstroObject.Name.CaveTwin)?.GetAttachedOWRigidbody());
            bodies.Add(HeavenlyBodies.TimberHearth, () => Locator.GetAstroObject(AstroObject.Name.TimberHearth)?.GetAttachedOWRigidbody());
            bodies.Add(HeavenlyBodies.TimberHearthProbe, () => Locator.GetAstroObject(AstroObject.Name.TimberHearth)?.GetSatellite()?.GetAttachedOWRigidbody());
            bodies.Add(HeavenlyBodies.Attlerock, () => Locator.GetAstroObject(AstroObject.Name.TimberHearth)?.GetMoon()?.GetAttachedOWRigidbody());
            bodies.Add(HeavenlyBodies.BrittleHollow, () => Locator.GetAstroObject(AstroObject.Name.BrittleHollow)?.GetAttachedOWRigidbody());
            bodies.Add(HeavenlyBodies.HollowLantern, () => Locator.GetAstroObject(AstroObject.Name.BrittleHollow)?.GetMoon()?.GetAttachedOWRigidbody());
            bodies.Add(HeavenlyBodies.GiantsDeep, () => Locator.GetAstroObject(AstroObject.Name.GiantsDeep)?.GetAttachedOWRigidbody());
            bodies.Add(HeavenlyBodies.ProbeCannon, () => Locator.GetAstroObject(AstroObject.Name.ProbeCannon)?.GetAttachedOWRigidbody());
            bodies.Add(HeavenlyBodies.NomaiProbe, () => Locator.GetAstroObject(AstroObject.Name.ProbeCannon)?.GetComponent<OrbitalProbeLaunchController>()?.GetValue<OWRigidbody>("_probeBody"));
            bodies.Add(HeavenlyBodies.DarkBramble, () => Locator.GetAstroObject(AstroObject.Name.DarkBramble)?.GetAttachedOWRigidbody());
            bodies.Add(HeavenlyBodies.InnerDarkBramble_Hub, () => Helper.getSector(Sector.Name.BrambleDimension).Find(body => OuterFogWarpVolume.Name.Hub.Equals(body?.GetComponentInChildren<OuterFogWarpVolume>()?.GetName()))?.GetAttachedOWRigidbody());
            bodies.Add(HeavenlyBodies.InnerDarkBramble_EscapePod, () => Helper.getSector(Sector.Name.BrambleDimension).Find(body => OuterFogWarpVolume.Name.EscapePod.Equals(body?.GetComponentInChildren<OuterFogWarpVolume>()?.GetName()))?.GetAttachedOWRigidbody());
            bodies.Add(HeavenlyBodies.InnerDarkBramble_Nest, () => Helper.getSector(Sector.Name.BrambleDimension).Find(body => OuterFogWarpVolume.Name.AnglerNest.Equals(body?.GetComponentInChildren<OuterFogWarpVolume>()?.GetName()))?.GetAttachedOWRigidbody());
            bodies.Add(HeavenlyBodies.InnerDarkBramble_Feldspar, () => Helper.getSector(Sector.Name.BrambleDimension).Find(body => OuterFogWarpVolume.Name.Pioneer.Equals(body?.GetComponentInChildren<OuterFogWarpVolume>()?.GetName()))?.GetAttachedOWRigidbody());
            bodies.Add(HeavenlyBodies.InnerDarkBramble_Gutter, () => Helper.getSector(Sector.Name.BrambleDimension).Find(body => OuterFogWarpVolume.Name.ExitOnly.Equals(body?.GetComponentInChildren<OuterFogWarpVolume>()?.GetName()))?.GetAttachedOWRigidbody());
            bodies.Add(HeavenlyBodies.InnerDarkBramble_Vessel, () => Helper.getSector(Sector.Name.VesselDimension).Find(body => OuterFogWarpVolume.Name.Vessel.Equals(body?.GetComponentInChildren<OuterFogWarpVolume>()?.GetName()))?.GetAttachedOWRigidbody());
            bodies.Add(HeavenlyBodies.InnerDarkBramble_Maze, () => Helper.getSector(Sector.Name.BrambleDimension).Find(body => OuterFogWarpVolume.Name.Cluster.Equals(body?.GetComponentInChildren<OuterFogWarpVolume>()?.GetName()))?.GetAttachedOWRigidbody());
            bodies.Add(HeavenlyBodies.InnerDarkBramble_SmallNest, () => Helper.getSector(Sector.Name.BrambleDimension).Find(body => OuterFogWarpVolume.Name.SmallNest.Equals(body?.GetComponentInChildren<OuterFogWarpVolume>()?.GetName()))?.GetAttachedOWRigidbody());
            bodies.Add(HeavenlyBodies.Interloper, () => Locator.GetAstroObject(AstroObject.Name.Comet)?.GetAttachedOWRigidbody());
            bodies.Add(HeavenlyBodies.WhiteHole, () => Locator.GetAstroObject(AstroObject.Name.WhiteHole)?.GetAttachedOWRigidbody());
            bodies.Add(HeavenlyBodies.WhiteHoleStation, () => Locator.GetAstroObject(AstroObject.Name.WhiteHoleTarget)?.GetAttachedOWRigidbody());
            bodies.Add(HeavenlyBodies.Stranger, () => Locator.GetAstroObject(AstroObject.Name.RingWorld)?.GetAttachedOWRigidbody());
            bodies.Add(HeavenlyBodies.DreamWorld, () => Locator.GetAstroObject(AstroObject.Name.DreamWorld)?.GetAttachedOWRigidbody());
            bodies.Add(HeavenlyBodies.QuantumMoon, () => Locator.GetAstroObject(AstroObject.Name.QuantumMoon)?.GetAttachedOWRigidbody());
        }

        public static void Awake()
        {
        }

        public static void Destroy()
        {
        }

        public static void Update()
        {
            if (debugMode && Locator.GetPlayerBody())
            {
                if (Locator.GetPromptManager()?.GetScreenPromptList(PromptPosition.LowerLeft)?.Contains(parentPrompt) == false)
                {
                    Locator.GetPromptManager().AddScreenPrompt(parentPrompt, PromptPosition.LowerLeft, true);
                    Locator.GetPromptManager().AddScreenPrompt(positionPrompt, PromptPosition.LowerLeft, true);
                    Locator.GetPromptManager().AddScreenPrompt(velocityPrompt, PromptPosition.LowerLeft, true);
                }

                if (Time.time - lastUpdate > 0.2f)
                {
                    lastUpdate = Time.time;
                    var list = Position.getClosest(getPlayerBody().GetPosition());
                    var item = list[0] == Position.HeavenlyBodies.TimberHearthProbe ? list[1] : list[0];
                    var parent = Position.getBody(item);
                    if (parent)
                    {
                        parentPrompt.SetText("Parent: " + item);
                        positionPrompt.SetText("Position: " + (parent.transform.InverseTransformPoint(getPlayerBody().GetPosition())));
                        velocityPrompt.SetText("Velocity: " + (getPlayerBody().GetVelocity() - parent.GetVelocity()));
                    }
                    parentPrompt?.SetVisibility(parent != null);
                    positionPrompt?.SetVisibility(parent != null);
                    velocityPrompt?.SetVisibility(parent != null);
                }
            }
            else
            {
                parentPrompt?.SetVisibility(false);
                positionPrompt?.SetVisibility(false);
                velocityPrompt?.SetVisibility(false);
            }
        }

        public static OWRigidbody getBody(HeavenlyBodies body)
        {
            return bodies[body].Invoke();
        }

        public static List<HeavenlyBodies> getClosest(Vector3 position)
        {
            List<HeavenlyBodies> keys = new List<HeavenlyBodies>(bodies.Keys);
            keys.Sort((v1, v2) =>
            {
                var distanceV1 = bodies[v1]?.Invoke()?.transform?.InverseTransformPoint(position).magnitude;
                var distanceV2 = bodies[v2]?.Invoke()?.transform?.InverseTransformPoint(position).magnitude;
                if (distanceV1.HasValue && distanceV2.HasValue)
                {
                    return (int)(distanceV1.GetValueOrDefault(0f) - distanceV2.GetValueOrDefault(0f));
                }
                if (distanceV1.HasValue)
                {
                    return -1;
                }
                if (distanceV2.HasValue)
                {
                    return 1;
                }
                return 0;
            });
            return keys;
        }
    }
}
