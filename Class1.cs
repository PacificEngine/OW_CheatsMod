using OWML.Common;
using OWML.ModHelper;
using OWML.ModHelper.Events;
using OWML.Utils;
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
        Toggle_Eye_Coordinates,
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
        Teleport_To_Nomai_Probe,
        Teleport_Ship_To_Player,
        Toggle_Helmet,
        Toggle_Invinciblity,
        Toggle_Spacesuit,
        Toggle_Training_Suit,
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
        Quantum_Moon_Collapse,
        Decrease_Jetpack_Acceleration,
        Increase_Jetpack_Acceleration,
        Decrease_Ship_Acceleration,
        Increase_Ship_Acceleration,
        Give_Warp_Core,
        Toggle_Position_Display
    }

    public class MainClass : ModBehaviour
    {
        private const string verison = "0.3.0";
        private ScreenPrompt cheatsTagger = new ScreenPrompt("");

        bool cheatsEnabled = true;
        Dictionary<CheatOptions, MultiInputClass> inputs = new Dictionary<CheatOptions, MultiInputClass>();

        void Start()
        {
            ModHelper.Events.Player.OnPlayerAwake += (player) => onAwake();
            Position.Start();
            Anglerfish.Start();
            Inhabitants.Start();
            Items.Start();
            ModHelper.Console.WriteLine("CheatMods ready!");
        }

        void Destory()
        {
            Position.Destroy();
            Anglerfish.Destroy();
            Inhabitants.Destroy();
            Items.Destroy();
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
            catch (Exception)
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
            Helper.helper = (ModHelper)ModHelper;

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
            addInput(config, CheatOptions.Toggle_Eye_Coordinates, "C,E");
            addInput(config, CheatOptions.Toggle_All_Frequencies, "C,F");
            addInput(config, CheatOptions.Toggle_Rumors, "C,R");
            addInput(config, CheatOptions.Toggle_Helmet, "C,H");
            addInput(config, CheatOptions.Toggle_Invinciblity, "C,I");
            addInput(config, CheatOptions.Toggle_Spacesuit, "C,G");
            addInput(config, CheatOptions.Toggle_Training_Suit, "C,Digit1");
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
            addInput(config, CheatOptions.Teleport_To_Nomai_Probe, "T,NumpadMinus");

            addInput(config, CheatOptions.Toggle_Anglerfish_AI, "V,I");
            addInput(config, CheatOptions.Toggle_Inhabitants_AI, "V,O");
            addInput(config, CheatOptions.Toggle_Inhabitants_Hostility, "V,H");
            addInput(config, CheatOptions.Toggle_Supernova_Timer, "V,0");
            addInput(config, CheatOptions.Decrease_Supernova_Timer, "V,Minus");
            addInput(config, CheatOptions.Increase_Supernova_Timer, "V,Equals");

            addInput(config, CheatOptions.Quantum_Moon_Collapse, "Q,Digit0");
            addInput(config, CheatOptions.Decrease_Jetpack_Acceleration, "P,Minus");
            addInput(config, CheatOptions.Increase_Jetpack_Acceleration, "P,Equals");
            addInput(config, CheatOptions.Decrease_Ship_Acceleration, "O,Minus");
            addInput(config, CheatOptions.Increase_Ship_Acceleration, "O,Equals");

            addInput(config, CheatOptions.Give_Warp_Core, "G,W");

            addInput(config, CheatOptions.Toggle_Position_Display, "D,P");
            

            ModHelper.Console.WriteLine("CheatMods Confgiured!");
        }

        void onAwake()
        {
            Helper.helper = (ModHelper)ModHelper;
            Position.Awake();
            Anglerfish.Awake();
            Inhabitants.Awake();

            ModHelper.Console.WriteLine("CheatMods: Player Awakes");
        }

        void OnGUI()
        {
            cheatsTagger.SetText("CheatsMod v" + verison + ": " + (cheatsEnabled ? "Enabled" : "Disabled"));
            if (Locator.GetPromptManager()?.GetScreenPromptList(PromptPosition.LowerLeft)?.Contains(cheatsTagger) == false)
            {
                Locator.GetPromptManager().AddScreenPrompt(cheatsTagger, PromptPosition.LowerLeft, true);
            }
        }

        void Update()
        {
            Helper.helper = (ModHelper)ModHelper;
            foreach (MultiInputClass input in inputs.Values)
            {
                input.Update();
            }
            if (cheatsEnabled)
            {
                Position.Update();
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

                if (inputs[CheatOptions.Toggle_Launch_Codes].isPressedThisFrame())
                {
                    Data.launchCodes = !Data.launchCodes;
                    ModHelper.Console.WriteLine("CheatsMod: Launch Codes Known " + Data.launchCodes);
                }

                if (inputs[CheatOptions.Toggle_Eye_Coordinates].isPressedThisFrame())
                {
                    Data.eyeCoordinates = !Data.eyeCoordinates;
                    ModHelper.Console.WriteLine("CheatsMod: Eye Coordinates Known " + Data.eyeCoordinates);
                }

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

                if (inputs[CheatOptions.Teleport_To_Nomai_Probe].isPressedThisFrame())
                    Teleportation.teleportPlayerToNomaiProbe();

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

                if (inputs[CheatOptions.Toggle_Helmet].isPressedThisFrame())
                {
                    Player.helmet = !Player.helmet;
                    ModHelper.Console.WriteLine("CheatsMod: Player Helmet " + Player.helmet);
                }

                if (inputs[CheatOptions.Toggle_Player_Collision].isPressedThisFrame())
                {
                    Player.collision = !Player.collision;
                    ModHelper.Console.WriteLine("CheatsMod: Player Collision " + Player.collision);
                }

                if (inputs[CheatOptions.Toggle_Ship_Collision].isPressedThisFrame())
                {
                    Ship.collision = !Ship.collision;
                    ModHelper.Console.WriteLine("CheatsMod: Ship Collision " + Ship.collision);
                }

                if (inputs[CheatOptions.Toggle_Training_Suit].isPressedThisFrame())
                {
                    Player.trainingSuit = !Player.trainingSuit;
                    ModHelper.Console.WriteLine("CheatsMod: Training Suit " + Player.trainingSuit);
                }

                if (inputs[CheatOptions.Toggle_Spacesuit].isPressedThisFrame())
                {
                    Player.spaceSuit = !Player.spaceSuit;
                    ModHelper.Console.WriteLine("CheatsMod: Space Suit " + Player.spaceSuit);
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

                if (inputs[CheatOptions.Decrease_Jetpack_Acceleration].isPressedThisFrame())
                {
                    Player.thrust = Player.thrust / 2f;
                    ModHelper.Console.WriteLine("CheatsMod: JetPack Acceleration Multiplier " + (Player.thrust / 6f));
                }

                if (inputs[CheatOptions.Increase_Jetpack_Acceleration].isPressedThisFrame())
                {
                    Player.thrust = Player.thrust * 2f;
                    ModHelper.Console.WriteLine("CheatsMod: JetPack Acceleration Multiplier " + (Player.thrust / 6f));
                }

                if (inputs[CheatOptions.Decrease_Ship_Acceleration].isPressedThisFrame())
                {
                    Ship.thrust = Ship.thrust / 2f;
                    ModHelper.Console.WriteLine("CheatsMod: JetPack Acceleration Multiplier " + (Ship.thrust / 50f));
                }

                if (inputs[CheatOptions.Increase_Ship_Acceleration].isPressedThisFrame())
                {
                    Ship.thrust = Ship.thrust * 2f;
                    ModHelper.Console.WriteLine("CheatsMod: JetPack Acceleration Multiplier " + (Ship.thrust / 50f));
                }

                if (inputs[CheatOptions.Quantum_Moon_Collapse].isPressedThisFrame())
                    QuantumMoonHelper.collapse();

                if (inputs[CheatOptions.Give_Warp_Core].isPressedThisFrame())
                    Items.pickUpWarpCore(WarpCoreType.Vessel);

                if (inputs[CheatOptions.Toggle_Position_Display].isPressedThisFrame())
                    Position.debugMode = !Position.debugMode;
            }
        }

        private void toggleFacts()
        {
            if (Data.knowAllRumors && Data.knowAllFacts)
            {
                Data.knowAllRumors = false;
                Data.knowAllFacts = false;
            }
            else if (Data.knowAllRumors)
            {
                Data.knowAllFacts = true;
            }
            else
            {
                Data.knowAllRumors = true;
            }

            ModHelper.Console.WriteLine("Cheat Mods: All Rumors " + Data.knowAllRumors + " All Fact " + Data.knowAllFacts);
        }

        private void toggleFrequencies()
        {
            if (Data.knowAllFrequencies && Data.knowAllSignals)
            {
                Data.knowAllSignals = false;
                Data.knowAllFrequencies = false;
            }
            else if (Data.knowAllFrequencies)
            {
                Data.knowAllSignals = true;
            }
            else
            {
                Data.knowAllFrequencies = true;
            }

            ModHelper.Console.WriteLine("Cheat Mods: All Frequencies " + Data.knowAllFrequencies + " All Signals " + Data.knowAllSignals);
        }
    }
}
