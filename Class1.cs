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
        Teleport_To_Probe,
        Teleport_Ship_To_Player,
        Toggle_Helmet,
        Toggle_Invinciblity,
        Toggle_Spacesuit,
        Toggle_Player_Collision,
        Toggle_Ship_Collision,
        Toggle_Unlimited_Boost,
        Toggle_Unlimited_Fuel,
        Toggle_Unlimited_Oxygen,
        Toggle_Unlimited_Health,
        Toggle_Anglerfish_AI,
        Toggle_Inhabitants_AI,
        Toggle_Inhabitants_Hostility,
        Toggle_Supernova_Timer,
        Decrease_Supernova_Timer,
        Increase_Supernova_Timer,
    }

    public class MainClass : ModBehaviour
    {
        private const string verison = "0.2.5";

        bool cheatsEnabled = true;
        Dictionary<CheatOptions, MultiInputClass> inputs = new Dictionary<CheatOptions, MultiInputClass>();
        float boostSeconds = 0f;

        void Start()
        {
            ModHelper.Events.Player.OnPlayerAwake += (player) => onAwake();
            Anglerfish.Start((ModHelper)ModHelper);
            Inhabitants.Start((ModHelper)ModHelper);
            ModHelper.Console.WriteLine("CheatMods ready!");
        }

        void Destory()
        {
            Anglerfish.Destroy((ModHelper)ModHelper);
            Inhabitants.Destroy((ModHelper)ModHelper);
            ModHelper.Console.WriteLine("CheatMods clean up!");
        }

        private T getConfigOrDefault<T>(IModConfig config, string id, T defaultValue)
        {
            try
            {
                var sValue = config.GetSettingsValue<T>(id);
                if (sValue == null)
                {
                    throw new NullReferenceException(id);
                }
                if (sValue is string && ((string)(object)sValue).Length < 1)
                {
                    throw new NullReferenceException(id);
                }
                return sValue;
            }
            catch (Exception e)
            {
                config.SetSettingsValue(id, defaultValue);
                return defaultValue;
            };
        }


        private MultiInputClass getInputConfigOrDefault(IModConfig config, string id, string defaultValue)
        {
            return MultiInputClass.fromString(getConfigOrDefault<string>(config, id, defaultValue));
        }

        private void addInput(IModConfig config, CheatOptions option, string defaultValue)
        {
            var name = Enum.GetName(option.GetType(), option).Replace("_", " ");
            inputs.Add(option, getInputConfigOrDefault(config, name, defaultValue));
        }

        public override void Configure(IModConfig config)
        {
            cheatsEnabled = config.Enabled;

            Player.isInvincible = getConfigOrDefault<bool>(config, "Invincible", false);
            Ship.isInvincible = Player.isInvincible;

            Player.hasUnlimitedFuel = getConfigOrDefault<bool>(config, "Unlimited Fuel", false);
            Ship.hasUnlimitedFuel = Player.hasUnlimitedFuel;

            Player.hasUnlimitedOxygen = getConfigOrDefault<bool>(config, "Unlimited Oxygen", false);
            Ship.hasUnlimitedOxygen = Player.hasUnlimitedOxygen;

            Player.hasUnlimitedHealth = getConfigOrDefault<bool>(config, "Unlimited Health", false);
            Player.hasUnlimitedBoost = getConfigOrDefault<bool>(config, "Unlimited Boost", false);

            Anglerfish.enabledAI = getConfigOrDefault<bool>(config, "Anglerfish AI", false);
            Inhabitants.enabledAI = getConfigOrDefault<bool>(config, "Inhabitants AI", false);
            Inhabitants.enabledHostility = getConfigOrDefault<bool>(config, "Inhabitants Hostility", false);

            inputs.Clear();
            addInput(config, CheatOptions.Fill_Fuel_and_Health, "C,J");
            addInput(config, CheatOptions.Toggle_Launch_Codes, "C,L");
            addInput(config, CheatOptions.Toggle_All_Frequencies, "C,F");
            addInput(config, CheatOptions.Toggle_Rumors, "C,R");
            addInput(config, CheatOptions.Toggle_Helmet, "C,H");
            addInput(config, CheatOptions.Toggle_Invinciblity, "C,I");
            addInput(config, CheatOptions.Toggle_Spacesuit, "C,G");
            addInput(config, CheatOptions.Toggle_Player_Collision, "C,N");
            addInput(config, CheatOptions.Toggle_Ship_Collision, "C,M");
            addInput(config, CheatOptions.Toggle_Unlimited_Boost, "C,T");
            addInput(config, CheatOptions.Toggle_Unlimited_Fuel, "C,Y");
            addInput(config, CheatOptions.Toggle_Unlimited_Oxygen, "C,O");
            addInput(config, CheatOptions.Toggle_Unlimited_Health, "C,U");

            addInput(config, CheatOptions.Teleport_To_Sun, "T,Digit1");
            addInput(config, CheatOptions.Teleport_To_SunStation, "T,Digit2");
            addInput(config, CheatOptions.Teleport_To_EmberTwin, "T,Digit3");
            addInput(config, CheatOptions.Teleport_To_AshTwin, "T,Digit4");
            addInput(config, CheatOptions.Teleport_To_TimerHearth, "T,Digit5");
            addInput(config, CheatOptions.Teleport_To_Attlerock, "T,Digit6");
            addInput(config, CheatOptions.Teleport_To_BrittleHollow, "T,Digit7");
            addInput(config, CheatOptions.Teleport_To_HollowLattern, "T,Digit8");
            addInput(config, CheatOptions.Teleport_To_GiantsDeep, "T,Digit9");
            addInput(config, CheatOptions.Teleport_To_DarkBramble, "T,Digit0");

            addInput(config, CheatOptions.Teleport_To_Interloper, "T,Numpad0");
            addInput(config, CheatOptions.Teleport_To_TimerHearth_Probe, "T,Numpad1");
            addInput(config, CheatOptions.Teleport_To_ProbeCannon, "T,Numpad2");
            addInput(config, CheatOptions.Teleport_To_WhiteHole, "T,Numpad3");
            addInput(config, CheatOptions.Teleport_To_WhiteHoleStation, "T,Numpad4");
            addInput(config, CheatOptions.Teleport_To_Stranger, "T,Numpad5");
            addInput(config, CheatOptions.Teleport_To_DreamWorld, "T,Numpad6");
            addInput(config, CheatOptions.Teleport_To_QuantumMoon, "T,Numpad7");
            addInput(config, CheatOptions.Teleport_To_AshTwinProject, "T,Numpad8");
            addInput(config, CheatOptions.Teleport_To_Ship, "T,Numpad9");
            addInput(config, CheatOptions.Teleport_Ship_To_Player, "T,NumpadDivide");
            addInput(config, CheatOptions.Teleport_To_Probe, "T,NumpadMultiply");

            addInput(config, CheatOptions.Toggle_Anglerfish_AI, "V,I");
            addInput(config, CheatOptions.Toggle_Inhabitants_AI, "V,O");
            addInput(config, CheatOptions.Toggle_Inhabitants_Hostility, "V,H");
            addInput(config, CheatOptions.Toggle_Supernova_Timer, "V,0");
            addInput(config, CheatOptions.Decrease_Supernova_Timer, "V,Minus");
            addInput(config, CheatOptions.Increase_Supernova_Timer, "V,Equals");

            ModHelper.Console.WriteLine("CheatMods Confgiured!");
        }

        void onAwake()
        {
            Anglerfish.Awake();
            Inhabitants.Awake();

            ModHelper.Console.WriteLine("CheatMods: Player Awakes");
        }

        void OnGUI()
        {
            GUI.Label(new Rect(((float)Screen.width) - 300f, ((float)Screen.height) - 20f, 300f, 20f), "CheatsMod v" + verison + " " + (cheatsEnabled ? "Enabled" : "Disabled"));
        }

        void Update()
        {
            foreach (MultiInputClass input in inputs.Values)
            {
                input.Update();
            }
            if (cheatsEnabled)
            {
                Player.Update();
                Ship.Update();
                Anglerfish.Update();
                SuperNova.Update();

                if (inputs[CheatOptions.Fill_Fuel_and_Health].isPressedThisFrame() && Locator.GetPlayerTransform())
                {
                    Player.oxygenSeconds = Player.maxOxygenSeconds;
                    Player.fuelSeconds = Player.maxFuelSeconds;
                    Player.health = Player.maxHealth;
                    Player.boostSeconds = Player.maxBoostSeconds;
                    Ship.repair();
                }

                if (inputs[CheatOptions.Toggle_Launch_Codes].isPressedThisFrame() && PlayerData.IsLoaded())
                    toggleLaunchCodes();

                if (inputs[CheatOptions.Toggle_All_Frequencies].isPressedThisFrame() && PlayerData.IsLoaded())
                    toggleFrequencies();

                if (inputs[CheatOptions.Toggle_Rumors].isPressedThisFrame() && Locator.GetShipLogManager())
                    toggleFacts();

                if (inputs[CheatOptions.Teleport_To_Sun].isPressedThisFrame())
                    Teleportation.teleportPlayerToSun();

                if (inputs[CheatOptions.Teleport_To_SunStation].isPressedThisFrame())
                    Teleportation.teleportPlayerToSunStation();

                if (inputs[CheatOptions.Teleport_To_EmberTwin].isPressedThisFrame())
                    Teleportation.teleportPlayerToEmberTwin();

                if (inputs[CheatOptions.Teleport_To_AshTwin].isPressedThisFrame())
                    Teleportation.teleportPlayerToAshTwin();

                if (inputs[CheatOptions.Teleport_To_AshTwinProject].isPressedThisFrame())
                    Teleportation.teleportPlayerToAshTwinProject();

                if (inputs[CheatOptions.Teleport_To_TimerHearth].isPressedThisFrame())
                    Teleportation.teleportPlayerToTimberHearth();

                if (inputs[CheatOptions.Teleport_To_TimerHearth_Probe].isPressedThisFrame())
                    Teleportation.teleportPlayerToTimberHearthProbe();

                if (inputs[CheatOptions.Teleport_To_Attlerock].isPressedThisFrame())
                    Teleportation.teleportPlayerToAttlerock();

                if (inputs[CheatOptions.Teleport_To_BrittleHollow].isPressedThisFrame())
                    Teleportation.teleportPlayerToBlackHoleForgeTeleporter();

                if (inputs[CheatOptions.Teleport_To_HollowLattern].isPressedThisFrame())
                    Teleportation.teleportPlayerToHollowLattern();

                if (inputs[CheatOptions.Teleport_To_GiantsDeep].isPressedThisFrame())
                    Teleportation.teleportPlayerToGiantsDeep();

                if (inputs[CheatOptions.Teleport_To_ProbeCannon].isPressedThisFrame())
                    Teleportation.teleportPlayerToProbeCannon();

                if (inputs[CheatOptions.Teleport_To_DarkBramble].isPressedThisFrame())
                    Teleportation.teleportPlayerToDarkBramble();

                if (inputs[CheatOptions.Teleport_To_Ship].isPressedThisFrame())
                    Teleportation.teleportPlayerToShip();

                if (inputs[CheatOptions.Teleport_Ship_To_Player].isPressedThisFrame())
                    Teleportation.teleportShipToPlayer();

                if (inputs[CheatOptions.Teleport_To_Probe].isPressedThisFrame())
                    Teleportation.teleportPlayerToProbe();

                if (inputs[CheatOptions.Teleport_To_Interloper].isPressedThisFrame())
                    Teleportation.teleportPlayerToInterloper();

                if (inputs[CheatOptions.Teleport_To_WhiteHole].isPressedThisFrame())
                    Teleportation.teleportPlayerToWhiteHole();

                if (inputs[CheatOptions.Teleport_To_WhiteHoleStation].isPressedThisFrame())
                    Teleportation.teleportPlayerToWhiteHoleStation();

                if (inputs[CheatOptions.Teleport_To_Stranger].isPressedThisFrame())
                    Teleportation.teleportPlayerToStranger();

                if (inputs[CheatOptions.Teleport_To_DreamWorld].isPressedThisFrame())
                    Teleportation.teleportPlayerToDreamWorld();

                if (inputs[CheatOptions.Teleport_To_QuantumMoon].isPressedThisFrame())
                    Teleportation.teleportPlayerToQuantumMoon();

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

                if (inputs[CheatOptions.Toggle_Invinciblity].isPressedThisFrame())
                {
                    Player.isInvincible = !Player.isInvincible;
                    Ship.isInvincible = Player.isInvincible;
                    ModHelper.Console.WriteLine("CheatsMod: Invicible " + Player.isInvincible);
                }

                if (inputs[CheatOptions.Toggle_Unlimited_Fuel].isPressedThisFrame())
                {
                    Player.hasUnlimitedFuel = !Player.hasUnlimitedFuel;
                    Ship.hasUnlimitedFuel = Player.hasUnlimitedFuel;
                    ModHelper.Console.WriteLine("CheatsMod: Unlimited Fuel " + Player.hasUnlimitedFuel);
                }

                if (inputs[CheatOptions.Toggle_Unlimited_Boost].isPressedThisFrame())
                {
                    Player.hasUnlimitedBoost = !Player.hasUnlimitedBoost;
                    ModHelper.Console.WriteLine("CheatsMod: Unlimited Boost " + Player.hasUnlimitedBoost);
                }

                if (inputs[CheatOptions.Toggle_Unlimited_Health].isPressedThisFrame())
                {
                    Player.hasUnlimitedHealth = !Player.hasUnlimitedHealth;
                    ModHelper.Console.WriteLine("CheatsMod: Unlimited Health " + Player.hasUnlimitedHealth);
                }

                if (inputs[CheatOptions.Toggle_Unlimited_Oxygen].isPressedThisFrame())
                {
                    Player.hasUnlimitedOxygen = !Player.hasUnlimitedOxygen;
                    Ship.hasUnlimitedOxygen = Player.hasUnlimitedOxygen;
                    ModHelper.Console.WriteLine("CheatsMod: Unlimited Oxygen " + Player.hasUnlimitedOxygen);
                }

                if (inputs[CheatOptions.Toggle_Anglerfish_AI].isPressedThisFrame())
                {
                    Anglerfish.enabledAI = !Anglerfish.enabledAI;
                    ModHelper.Console.WriteLine("CheatsMod: Anglerfish AI " + Anglerfish.enabledAI);
                }

                if (inputs[CheatOptions.Toggle_Inhabitants_AI].isPressedThisFrame())
                {
                    Inhabitants.enabledAI = !Inhabitants.enabledAI;
                    ModHelper.Console.WriteLine("CheatsMod: Inhabitants AI " + Inhabitants.enabledAI);
                }

                if (inputs[CheatOptions.Toggle_Inhabitants_Hostility].isPressedThisFrame())
                {
                    Inhabitants.enabledHostility = !Inhabitants.enabledHostility;
                    ModHelper.Console.WriteLine("CheatsMod: Inhabitants Hostility " + Inhabitants.enabledHostility);
                }

                if (inputs[CheatOptions.Toggle_Supernova_Timer].isPressedThisFrame())
                {
                    SuperNova.freeze(!SuperNova.isFrozen());
                    ModHelper.Console.WriteLine("CheatsMod: SuperNova Frozen " + SuperNova.isFrozen());
                }

                if (inputs[CheatOptions.Decrease_Supernova_Timer].isPressedThisFrame())
                {
                    SuperNova.adjust(-60f);
                    ModHelper.Console.WriteLine("CheatsMod: Remaining Time " + TimeLoop.GetSecondsRemaining());
                }

                if (inputs[CheatOptions.Increase_Supernova_Timer].isPressedThisFrame())
                {
                    SuperNova.adjust(60f);
                    ModHelper.Console.WriteLine("CheatsMod: Remaining Time " + TimeLoop.GetSecondsRemaining());
                }
            }
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
