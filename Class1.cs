using OWML.Common;
using OWML.ModHelper;
using OWML.ModHelper.Events;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ClassLibrary2
{
    enum CheatOptions
    {
        Fill_Fuel_and_Health,
        Toggle_Launch_Codes,
        Toggle_All_Frequencies,
        Toggle_Rumors,
        Teleport_To_Sun,
        Teleport_To_SunStation,
        Teleport_To_EmberTwin, // CaveTwin
        Teleport_To_AshTwin, // TowerTwin
        Teleport_To_AshTwinProject,
        Teleport_To_TimerHearth,
        Teleport_To_TimerHearth_Probe, // TimerHearth.Satillite
        Teleport_To_Attlerock, // TimerHearth.Moon
        Teleport_To_BrittleHollow,
        Teleport_To_HollowLattern, // BrittleHollow.Moon
        Teleport_To_GiantsDeep,
        Teleport_To_ProbeCannon,
        Teleport_To_DarkBramble,
        Teleport_To_Interloper, // Comet
        Teleport_To_WhiteHole,
        Teleport_To_WhiteHoleStation, // WhiteHoleTarget
        Teleport_To_Stranger, // RingWorld
        Teleport_To_DreamWorld,
        Teleport_To_QuantumMoon,
        Teleport_To_Ship,
        Toggle_Helmet,
        Toggle_Invinciblity,
        Toggle_Spacesuit,
        Toggle_Player_Collision,
        Toggle_Ship_Collision,
        Toggle_Unlimited_Boost,
        Toggle_Unlimited_Fuel,
        Toggle_Unlimited_Oxygen,
        Toggle_Unlimited_Health
    }

    class InputClass
    {
        HashSet<Key> keys;
        int pressed = 0;


        public InputClass(params Key[] keys)
        {
            this.keys = new HashSet<Key>(keys);
        }

        public void Update()
        {
            bool areAllPressed = true;
            foreach (Key key in keys)
            {
                if (!Keyboard.current[key].IsActuated())
                {
                    areAllPressed = false;
                    break;
                }
            }

            if (areAllPressed)
            {
                pressed++;
            } 
            else
            {
                pressed = 0;
            }
            
        }

        public bool isPressedThisFrame()
        {
            return pressed == 1;
        }

        public bool isPressed()
        {
            return pressed > 0;
        }

        public int frameCountPressed()
        {
            return pressed;
        }
    }

    public class MainClass : ModBehaviour
    {
        private const string verison = "0.1.1";
        private static Vector3 zeroVector = new Vector3(0f, 0f, 0f);
        private static Quaternion zeroQuaternion = new Quaternion(0f, 0f, 0f, 0f);

        bool cheatsEnabled = true;
        Dictionary<CheatOptions, InputClass> inputs = new Dictionary<CheatOptions, InputClass>();
        bool hasUnlimitedFuel = false;
        bool hasUnlimitedOxygen = false;
        bool hasUnlimitedHealth = false;
        bool isInvincible = false;
        bool hasUnlimitedBoost = false;
        float boostSeconds = 0f;

        void Start()
        {
            ModHelper.Events.Player.OnPlayerAwake += (player) => onAwake();
            ModHelper.Console.WriteLine("CheatMods ready!");
        }

        public override void Configure(IModConfig config)
        {
            cheatsEnabled = config.Enabled;

            inputs.Clear();

            inputs.Add(CheatOptions.Fill_Fuel_and_Health, new InputClass(Key.C, Key.J));
            inputs.Add(CheatOptions.Toggle_Launch_Codes, new InputClass(Key.C, Key.L));
            inputs.Add(CheatOptions.Toggle_All_Frequencies, new InputClass(Key.C, Key.F));
            inputs.Add(CheatOptions.Toggle_Rumors, new InputClass(Key.C, Key.R));
            inputs.Add(CheatOptions.Toggle_Helmet, new InputClass(Key.C, Key.H));
            inputs.Add(CheatOptions.Toggle_Invinciblity, new InputClass(Key.C, Key.I));
            inputs.Add(CheatOptions.Toggle_Spacesuit, new InputClass(Key.C, Key.G));
            inputs.Add(CheatOptions.Toggle_Player_Collision, new InputClass(Key.C, Key.N));
            inputs.Add(CheatOptions.Toggle_Ship_Collision, new InputClass(Key.C, Key.M));
            inputs.Add(CheatOptions.Toggle_Unlimited_Boost, new InputClass(Key.C, Key.T));
            inputs.Add(CheatOptions.Toggle_Unlimited_Fuel, new InputClass(Key.C, Key.Y));
            inputs.Add(CheatOptions.Toggle_Unlimited_Oxygen, new InputClass(Key.C, Key.O));
            inputs.Add(CheatOptions.Toggle_Unlimited_Health, new InputClass(Key.C, Key.U));

            inputs.Add(CheatOptions.Teleport_To_Sun, new InputClass(Key.T, Key.Digit1));
            inputs.Add(CheatOptions.Teleport_To_SunStation, new InputClass(Key.T, Key.Digit2));
            inputs.Add(CheatOptions.Teleport_To_EmberTwin, new InputClass(Key.T, Key.Digit3));
            inputs.Add(CheatOptions.Teleport_To_AshTwin, new InputClass(Key.T, Key.Digit4));
            inputs.Add(CheatOptions.Teleport_To_TimerHearth, new InputClass(Key.T, Key.Digit5));
            inputs.Add(CheatOptions.Teleport_To_Attlerock, new InputClass(Key.T, Key.Digit6));
            inputs.Add(CheatOptions.Teleport_To_BrittleHollow, new InputClass(Key.T, Key.Digit7));
            inputs.Add(CheatOptions.Teleport_To_HollowLattern, new InputClass(Key.T, Key.Digit8));
            inputs.Add(CheatOptions.Teleport_To_GiantsDeep, new InputClass(Key.T, Key.Digit9));
            inputs.Add(CheatOptions.Teleport_To_DarkBramble, new InputClass(Key.T, Key.Digit0));

            inputs.Add(CheatOptions.Teleport_To_Interloper, new InputClass(Key.T, Key.Numpad0));
            inputs.Add(CheatOptions.Teleport_To_TimerHearth_Probe, new InputClass(Key.T, Key.Numpad1));
            inputs.Add(CheatOptions.Teleport_To_ProbeCannon, new InputClass(Key.T, Key.Numpad2));
            inputs.Add(CheatOptions.Teleport_To_WhiteHole, new InputClass(Key.T, Key.Numpad3));
            inputs.Add(CheatOptions.Teleport_To_WhiteHoleStation, new InputClass(Key.T, Key.Numpad4));
            inputs.Add(CheatOptions.Teleport_To_Stranger, new InputClass(Key.T, Key.Numpad5));
            inputs.Add(CheatOptions.Teleport_To_DreamWorld, new InputClass(Key.T, Key.Numpad6));
            inputs.Add(CheatOptions.Teleport_To_QuantumMoon, new InputClass(Key.T, Key.Numpad7));
            inputs.Add(CheatOptions.Teleport_To_AshTwinProject, new InputClass(Key.T, Key.Numpad8));
            inputs.Add(CheatOptions.Teleport_To_Ship, new InputClass(Key.T, Key.Numpad9));

            ModHelper.Console.WriteLine("CheatMods Confgiured!");
        }

        void onAwake()
        {
            ModHelper.Console.WriteLine("CheatMods: Player Awakes");
        }

        void OnGUI()
        {
            GUI.Label(new Rect(((float)Screen.width) - 300f, ((float)Screen.height) - 20f, 300f, 20f), "CheatsMod v" + verison + " " + (cheatsEnabled ? "Enabled" : "Disabled"));
        }

        void Update()
        {
            foreach (InputClass input in inputs.Values)
            {
                input.Update();
            }
            if (cheatsEnabled)
            {
                if (hasUnlimitedBoost && Locator.GetPlayerTransform())
                {
                    if (Locator.GetPlayerTransform().GetComponent<PlayerResources>().GetComponent<JetpackThrusterModel>().GetValue<float>("_boostSeconds") != float.MaxValue)
                    {
                        boostSeconds = Locator.GetPlayerTransform().GetComponent<PlayerResources>().GetComponent<JetpackThrusterModel>().GetValue<float>("_boostSeconds");
                        Locator.GetPlayerTransform().GetComponent<PlayerResources>().GetComponent<JetpackThrusterModel>().SetValue("_boostSeconds", float.MaxValue);
                    }
                }

                if (hasUnlimitedFuel && Locator.GetPlayerTransform())
                {
                    Locator.GetPlayerTransform().GetComponent<PlayerResources>().SetValue("_currentFuel", 100f);
                    if (Locator.GetShipTransform())
                    {
                        Locator.GetShipTransform().GetComponent<ShipResources>().AddFuel(1000f);
                    }
                }

                if (hasUnlimitedOxygen && Locator.GetPlayerTransform())
                {
                    Locator.GetPlayerTransform().GetComponent<PlayerResources>().SetValue("_currentOxygen", 500f);
                    if (Locator.GetShipTransform())
                    {
                        Locator.GetShipTransform().GetComponent<ShipResources>().AddOxygen(1000f);
                    }
                }

                if (hasUnlimitedHealth && Locator.GetPlayerTransform())
                {
                    Locator.GetPlayerTransform().GetComponent<PlayerResources>().SetValue("_currentHealth", 100f);
                }

                if (Locator.GetPlayerTransform())
                {
                    Locator.GetPlayerTransform().GetComponent<PlayerResources>().SetValue("_invincible", isInvincible);
                    Locator.GetShipTransform().GetComponent<ShipDamageController>().SetValue("_invincible", isInvincible);
                }

                if (inputs[CheatOptions.Fill_Fuel_and_Health].isPressedThisFrame() && Locator.GetPlayerTransform())
                {
                    Locator.GetPlayerTransform().GetComponent<PlayerResources>().DebugRefillResources();
                    if (Locator.GetShipTransform())
                    {
                        ShipComponent[] componentsInChildren2 = Locator.GetShipTransform().GetComponentsInChildren<ShipComponent>();
                        for (int l = 0; l < componentsInChildren2.Length; l++)
                        {
                            componentsInChildren2[l].SetDamaged(false);
                        }
                    }
                }

                if (inputs[CheatOptions.Toggle_Launch_Codes].isPressedThisFrame() && PlayerData.IsLoaded())
                {
                    toggleLaunchCodes();
                }

                if (inputs[CheatOptions.Toggle_All_Frequencies].isPressedThisFrame() && PlayerData.IsLoaded())
                {
                    toggleFrequencies();
                }

                if (inputs[CheatOptions.Toggle_Rumors].isPressedThisFrame() && Locator.GetShipLogManager())
                {
                    toggleFacts();
                }

                if(inputs[CheatOptions.Teleport_To_Sun].isPressedThisFrame() && Locator.GetPlayerTransform() && Locator.GetAstroObject(AstroObject.Name.Sun))
                {
                    teleportObjectTo(Locator.GetPlayerBody(), Locator.GetAstroObject(AstroObject.Name.Sun).GetAttachedOWRigidbody(), new Vector3(0f, 5000f, 0f), zeroVector, zeroVector, zeroQuaternion);
                }

                if (inputs[CheatOptions.Teleport_To_SunStation].isPressedThisFrame() && Locator.GetPlayerTransform() && Locator.GetWarpReceiver(NomaiWarpPlatform.Frequency.SunStation))
                {
                    teleportObjectTo(Locator.GetPlayerBody(), Locator.GetWarpReceiver(NomaiWarpPlatform.Frequency.SunStation).GetAttachedOWRigidbody(), zeroVector, zeroVector, zeroVector, zeroQuaternion);
                }

                if (inputs[CheatOptions.Teleport_To_EmberTwin].isPressedThisFrame() && Locator.GetPlayerTransform() && Locator.GetAstroObject(AstroObject.Name.CaveTwin))
                {
                    teleportObjectTo(Locator.GetPlayerBody(), Locator.GetAstroObject(AstroObject.Name.CaveTwin).GetAttachedOWRigidbody(), new Vector3(0f, 165f, 0f), zeroVector, zeroVector, zeroQuaternion);
                }

                if (inputs[CheatOptions.Teleport_To_AshTwin].isPressedThisFrame() && Locator.GetPlayerTransform() && Locator.GetAstroObject(AstroObject.Name.TowerTwin))
                {
                    teleportObjectTo(Locator.GetPlayerBody(), Locator.GetAstroObject(AstroObject.Name.TowerTwin).GetAttachedOWRigidbody(), new Vector3(0f, 110f, 0f), zeroVector, zeroVector, zeroQuaternion);
                }

                if (inputs[CheatOptions.Teleport_To_AshTwinProject].isPressedThisFrame() && Locator.GetPlayerTransform() && Locator.GetAstroObject(AstroObject.Name.TowerTwin))
                {
                    teleportObjectTo(Locator.GetPlayerBody(), Locator.GetAstroObject(AstroObject.Name.TowerTwin).GetAttachedOWRigidbody(), new Vector3(-5f, 0f, 0f), zeroVector, zeroVector, zeroQuaternion);
                }

                if (inputs[CheatOptions.Teleport_To_TimerHearth].isPressedThisFrame() && Locator.GetPlayerTransform() && Locator.GetAstroObject(AstroObject.Name.TimberHearth))
                {
                    teleportObjectTo(Locator.GetPlayerBody(), Locator.GetAstroObject(AstroObject.Name.TimberHearth).GetAttachedOWRigidbody(), new Vector3(0f, 280f, 0f), zeroVector, zeroVector, zeroQuaternion);
                }

                if (inputs[CheatOptions.Teleport_To_TimerHearth_Probe].isPressedThisFrame() && Locator.GetPlayerTransform() && Locator.GetAstroObject(AstroObject.Name.TimberHearth))
                {
                    teleportObjectTo(Locator.GetPlayerBody(), Locator.GetAstroObject(AstroObject.Name.TimberHearth).GetSatellite().GetAttachedOWRigidbody(), -new Vector3(1f, 0f, 0f), zeroVector, zeroVector, zeroQuaternion);
                }

                if (inputs[CheatOptions.Teleport_To_Attlerock].isPressedThisFrame() && Locator.GetPlayerTransform() && Locator.GetAstroObject(AstroObject.Name.TimberHearth))
                {
                    teleportObjectTo(Locator.GetPlayerBody(), Locator.GetAstroObject(AstroObject.Name.TimberHearth).GetMoon().GetAttachedOWRigidbody(), new Vector3(0f, 80f, 0f), zeroVector, zeroVector, zeroQuaternion);
                }

                if (inputs[CheatOptions.Teleport_To_BrittleHollow].isPressedThisFrame() && Locator.GetPlayerTransform() && Locator.GetWarpReceiver(NomaiWarpPlatform.Frequency.BrittleHollowForge))
                {
                    var planet = Locator.GetAstroObject(AstroObject.Name.BrittleHollow).GetAttachedOWRigidbody();
                    var platform = Locator.GetWarpReceiver(NomaiWarpPlatform.Frequency.BrittleHollowForge).GetPlatformCenter();
                    teleportObjectTo(Locator.GetPlayerBody(), new Vector3(platform.position.x, platform.position.y - 2f, platform.position.z), planet.GetVelocity(), planet.GetAngularVelocity(), platform.rotation);
                }

                if (inputs[CheatOptions.Teleport_To_HollowLattern].isPressedThisFrame() && Locator.GetPlayerTransform() && Locator.GetAstroObject(AstroObject.Name.BrittleHollow))
                {
                    teleportObjectTo(Locator.GetPlayerBody(), Locator.GetAstroObject(AstroObject.Name.BrittleHollow).GetMoon().GetAttachedOWRigidbody(), new Vector3(0f, 150f, 0f), zeroVector, zeroVector, zeroQuaternion);
                }  

                if (inputs[CheatOptions.Teleport_To_GiantsDeep].isPressedThisFrame() && Locator.GetPlayerTransform())
                {
                    teleportObjectTo(Locator.GetPlayerBody(), Locator.GetAstroObject(AstroObject.Name.GiantsDeep).GetAttachedOWRigidbody(), new Vector3(0f, 505f, 0f), zeroVector, zeroVector, zeroQuaternion);
                }

                if (inputs[CheatOptions.Teleport_To_ProbeCannon].isPressedThisFrame() && Locator.GetPlayerTransform())
                {
                    teleportObjectTo(Locator.GetPlayerBody(), Locator.GetAstroObject(AstroObject.Name.ProbeCannon).GetAttachedOWRigidbody(), zeroVector, zeroVector, zeroVector, zeroQuaternion);
                }

                if (inputs[CheatOptions.Teleport_To_DarkBramble].isPressedThisFrame() && Locator.GetPlayerTransform())
                {
                    teleportObjectTo(Locator.GetPlayerBody(), Locator.GetAstroObject(AstroObject.Name.DarkBramble).GetAttachedOWRigidbody(), new Vector3(0f, 950f, 0f), zeroVector, zeroVector, zeroQuaternion);
                }

                if (inputs[CheatOptions.Teleport_To_Ship].isPressedThisFrame() && Locator.GetPlayerTransform() && Locator.GetShipBody())
                {
                    teleportObjectTo(Locator.GetPlayerBody(), Locator.GetShipBody(), zeroVector, zeroVector, zeroVector, zeroQuaternion);
                }

                if (inputs[CheatOptions.Teleport_To_Interloper].isPressedThisFrame() && Locator.GetPlayerTransform())
                {
                    teleportObjectTo(Locator.GetPlayerBody(), Locator.GetAstroObject(AstroObject.Name.Comet).GetAttachedOWRigidbody(), new Vector3(0f, 85f, 0f), zeroVector, zeroVector, zeroQuaternion);
                }

                if (inputs[CheatOptions.Teleport_To_WhiteHole].isPressedThisFrame() && Locator.GetPlayerTransform())
                {
                    teleportObjectTo(Locator.GetPlayerBody(), Locator.GetAstroObject(AstroObject.Name.WhiteHole).GetAttachedOWRigidbody(), new Vector3(40f, 0f, 0f), zeroVector, zeroVector, zeroQuaternion);
                }

                if (inputs[CheatOptions.Teleport_To_WhiteHoleStation].isPressedThisFrame() && Locator.GetPlayerTransform())
                {
                    teleportObjectTo(Locator.GetPlayerBody(), Locator.GetAstroObject(AstroObject.Name.WhiteHoleTarget).GetAttachedOWRigidbody(), new Vector3(0f, 0f, 0f), zeroVector, zeroVector, zeroQuaternion);
                }

                if (inputs[CheatOptions.Teleport_To_Stranger].isPressedThisFrame() && Locator.GetPlayerTransform())
                {
                    teleportObjectTo(Locator.GetPlayerBody(), Locator.GetAstroObject(AstroObject.Name.RingWorld).GetAttachedOWRigidbody(), new Vector3(-320f, -320f, -50f), zeroVector, zeroVector, zeroQuaternion);
                }

                if (inputs[CheatOptions.Teleport_To_DreamWorld].isPressedThisFrame() && Locator.GetPlayerTransform())
                {
                    teleportObjectTo(Locator.GetPlayerBody(), Locator.GetAstroObject(AstroObject.Name.DreamWorld).GetAttachedOWRigidbody(), new Vector3(0f, 100f, 0f), zeroVector, zeroVector, zeroQuaternion);
                }

                if (inputs[CheatOptions.Teleport_To_QuantumMoon].isPressedThisFrame() && Locator.GetPlayerTransform())
                {
                    teleportObjectTo(Locator.GetPlayerBody(), Locator.GetAstroObject(AstroObject.Name.QuantumMoon).GetAttachedOWRigidbody(), new Vector3(0f, -110f, 0f), zeroVector, zeroVector, zeroQuaternion);
                }

                if (inputs[CheatOptions.Toggle_Invinciblity].isPressedThisFrame())
                {
                    isInvincible = !isInvincible;
                }

                if (inputs[CheatOptions.Toggle_Helmet].isPressedThisFrame() && Locator.GetPlayerSuit())
                {
                    if (Locator.GetPlayerSuit().IsWearingHelmet())
                    {
                        Locator.GetPlayerSuit().RemoveHelmet();
                    }
                    else
                    {
                        Locator.GetPlayerSuit().PutOnHelmet();
                    }
                }

                if (inputs[CheatOptions.Toggle_Player_Collision].isPressedThisFrame() && Locator.GetPlayerBody())
                {
                    if (Locator.GetPlayerBody().GetRequiredComponent<Rigidbody>().detectCollisions)
                    {
                        Locator.GetPlayerBody().DisableCollisionDetection();
                    }
                    else
                    {
                        Locator.GetPlayerBody().EnableCollisionDetection();
                    }

                    foreach (Collider collider in Locator.GetPlayerBody().GetComponentsInChildren<Collider>())
                    {
                        if (!collider.isTrigger)
                        {
                            collider.enabled = !collider.enabled;
                        }
                    }
                }

                if (inputs[CheatOptions.Toggle_Ship_Collision].isPressedThisFrame() && Locator.GetShipBody())
                {
                    if (Locator.GetShipBody().GetRequiredComponent<Rigidbody>().detectCollisions)
                    {
                        Locator.GetShipBody().DisableCollisionDetection();
                    }
                    else
                    {
                        Locator.GetShipBody().EnableCollisionDetection();
                    }

                    foreach (Collider collider2 in Locator.GetShipTransform().GetComponentsInChildren<Collider>())
                    {
                        if (!collider2.isTrigger)
                        {
                            collider2.enabled = !collider2.enabled;
                        }
                    }
                }

                if (inputs[CheatOptions.Toggle_Spacesuit].isPressedThisFrame() && Locator.GetPlayerSuit())
                {
                    if (!Locator.GetPlayerSuit().IsWearingSuit(true))
                    {
                        Locator.GetPlayerSuit().SuitUp(false, false);
                    }
                    else
                    {
                        Locator.GetPlayerSuit().RemoveSuit(false);
                    }
                }

                if (inputs[CheatOptions.Toggle_Unlimited_Fuel].isPressedThisFrame())
                {
                    hasUnlimitedFuel = !hasUnlimitedFuel;
                }

                if (inputs[CheatOptions.Toggle_Unlimited_Boost].isPressedThisFrame())
                {
                    if (Locator.GetPlayerTransform().GetComponent<PlayerResources>().GetComponent<JetpackThrusterModel>().GetValue<float>("_boostSeconds") == float.MaxValue)
                    {
                        Locator.GetPlayerTransform().GetComponent<PlayerResources>().GetComponent<JetpackThrusterModel>().SetValue("_boostSeconds", boostSeconds);
                    }
                    hasUnlimitedBoost = !hasUnlimitedBoost;
                }
                

                if (inputs[CheatOptions.Toggle_Unlimited_Health].isPressedThisFrame())
                {
                    hasUnlimitedHealth = !hasUnlimitedHealth;
                }

                if (inputs[CheatOptions.Toggle_Unlimited_Oxygen].isPressedThisFrame())
                {
                    hasUnlimitedOxygen = !hasUnlimitedOxygen;
                }
            }
        }

        private void teleportObjectTo(OWRigidbody teleportObject, OWRigidbody teleportTo, Vector3 position, Vector3 velocity, Vector3 angularVelocity, Quaternion rotation)
        {
            if (teleportTo) 
            {
                teleportObjectTo(teleportObject, 
                    new Vector3(teleportTo.GetPosition().x + position.x, teleportTo.GetPosition().y + position.y, teleportTo.GetPosition().z + position.z),
                    new Vector3(teleportTo.GetVelocity().x + velocity.x, teleportTo.GetVelocity().y + velocity.y, teleportTo.GetVelocity().z + velocity.z),
                    new Vector3(teleportTo.GetAngularVelocity().x + angularVelocity.x, teleportTo.GetAngularVelocity().y + angularVelocity.y, teleportTo.GetAngularVelocity().z + angularVelocity.z),
                    new Quaternion(teleportTo.GetRotation().x + rotation.x, teleportTo.GetRotation().y + rotation.y, teleportTo.GetRotation().z + rotation.z, teleportTo.GetRotation().w + rotation.w));
            }
        }

        private void teleportObjectTo(OWRigidbody teleportObject, Vector3 position, Vector3 velocity, Vector3 angularVelocity, Quaternion rotation)
        {
            teleportObject.SetPosition(new Vector3(position.x, position.y, position.z));
            teleportObject.SetVelocity(new Vector3(velocity.x, velocity.y, velocity.z));
            teleportObject.SetAngularVelocity(new Vector3(angularVelocity.x, angularVelocity.y, angularVelocity.z));
            teleportObject.SetRotation(new Quaternion(rotation.x, rotation.y, rotation.z, rotation.w));
        }


        private void toggleLaunchCodes()
        {
            if (PlayerData.KnowsLaunchCodes())
            {
                DialogueConditionManager.SharedInstance.SetConditionState("TalkedToHornfels", false);
                DialogueConditionManager.SharedInstance.SetConditionState("SCIENTIST_3", false);
                DialogueConditionManager.SharedInstance.SetConditionState("LAUNCH_CODES_GIVEN", false);
                StandaloneProfileManager.SharedInstance.currentProfileGameSave.SetPersistentCondition("LAUNCH_CODES_GIVEN", false);

                ModHelper.Console.WriteLine("Cheat Mods: Forget Launch Codes!");
            }
            else
            {
                PlayerData.LearnLaunchCodes();
                DialogueConditionManager.SharedInstance.SetConditionState("TalkedToHornfels", true);
                DialogueConditionManager.SharedInstance.SetConditionState("SCIENTIST_3", true);
                DialogueConditionManager.SharedInstance.SetConditionState("LAUNCH_CODES_GIVEN", true);
                StandaloneProfileManager.SharedInstance.currentProfileGameSave.SetPersistentCondition("LAUNCH_CODES_GIVEN", true);
                GlobalMessenger.FireEvent(nameof(PlayerData.LearnLaunchCodes));

                ModHelper.Console.WriteLine("Cheat Mods: Learn Launch Codes!");
            }
        }

        private void toggleFacts()
        {
            bool knowsRumors = true;
            bool knowsFacts = true;
            foreach(ShipLogFact fact in Locator.GetShipLogManager().GetValue<List<ShipLogFact>>("_factList"))
            {
                if (!fact.IsRevealed())
                {
                    if (fact.IsRumor())
                    {
                        knowsRumors = false;
                    }
                    else
                    {
                        knowsFacts = false;
                    }
                }
            }

            if (knowsRumors && knowsFacts)
            {
                foreach (ShipLogFactSave fact in StandaloneProfileManager.SharedInstance.currentProfileGameSave.shipLogFactSaves.Values)
                {
                    fact.newlyRevealed = false;
                    fact.read = false;
                    fact.revealOrder = -1;
                }

                ModHelper.Console.WriteLine("Cheat Mods: Forget Rumors & Facts!");
            }
            else if (knowsRumors)
            {
                Locator.GetShipLogManager().RevealAllFacts(false);

                ModHelper.Console.WriteLine("Cheat Mods: Learn Facts!");
            }
            else
            {
                Locator.GetShipLogManager().RevealAllFacts(true);

                ModHelper.Console.WriteLine("Cheat Mods: Learn Rumors!");
            }

            PlayerData.SaveCurrentGame();
        }

        private void toggleFrequencies()
        {
            bool knowsFrequency = true;
            bool knowsSignal = true;
            foreach (SignalFrequency frequency in (SignalFrequency[])Enum.GetValues(typeof(SignalFrequency)))
            {
                if (frequency != SignalFrequency.Default && !PlayerData.KnowsFrequency(frequency))
                {
                    knowsFrequency = false;
                }
            }
            foreach (SignalName signal in (SignalName[])Enum.GetValues(typeof(SignalName)))
            {
                if (signal != SignalName.Default && !PlayerData.KnowsSignal(signal))
                {
                    knowsSignal = false;
                }
            }

            if (knowsFrequency && knowsSignal)
            {
                foreach (SignalName signal in (SignalName[])Enum.GetValues(typeof(SignalName)))
                {
                    StandaloneProfileManager.SharedInstance.currentProfileGameSave.knownSignals.Remove((int)signal);
                }

                PlayerData.ForgetFrequency(SignalFrequency.Quantum);
                PlayerData.ForgetFrequency(SignalFrequency.EscapePod);
                PlayerData.ForgetFrequency(SignalFrequency.Statue);
                PlayerData.ForgetFrequency(SignalFrequency.WarpCore);
                PlayerData.ForgetFrequency(SignalFrequency.HideAndSeek);
                PlayerData.ForgetFrequency(SignalFrequency.Radio);

                ModHelper.Console.WriteLine("Cheat Mods: Forget Frequencies & Signals!");
            }
            else if (knowsFrequency)
            {
                foreach (SignalName signal in (SignalName[])Enum.GetValues(typeof(SignalName)))
                {
                    StandaloneProfileManager.SharedInstance.currentProfileGameSave.knownSignals[(int)signal] = true;
                }

                ModHelper.Console.WriteLine("Cheat Mods: Learn Signals!");
            }
            else
            {
                foreach (SignalFrequency frequency in (SignalFrequency[])Enum.GetValues(typeof(SignalFrequency)))
                {
                    PlayerData.LearnFrequency(frequency);
                }

                ModHelper.Console.WriteLine("Cheat Mods: Learn Frequencies!");
            }

            PlayerData.SaveCurrentGame();
        }
    }
}
