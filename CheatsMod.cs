using OWML.Common;
using OWML.ModHelper;
using OWML.ModHelper.Events;
using OWML.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using PacificEngine.OW_CommonResources;
using PacificEngine.OW_CommonResources.Game.Player;
using PacificEngine.OW_CommonResources.Game.State;
using PacificEngine.OW_CommonResources.Game.Resource;
using PacificEngine.OW_CommonResources.Game.Config;
using PacificEngine.OW_CommonResources.Game;

namespace PacificEngine.OW_CheatsMod
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
        Teleport_To_ProbeCannonCommandModule,
        Teleport_To_DarkBramble,
        Teleport_To_Vessel,
        Teleport_To_Interloper, // Comet
        Teleport_To_WhiteHole,
        Teleport_To_WhiteHoleStation, // WhiteHoleTarget
        Teleport_To_Stranger, // RingWorld
        Teleport_To_DreamWorld,
        Teleport_To_QuantumMoon,
        Teleport_To_Ship,
        Teleport_To_Probe,
        Teleport_To_Nomai_Probe,
        Teleport_To_Mapping_Satellite,
        Teleport_To_Backer_Satellite,
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
        Give_Warp_Core_Vessel,
        Give_Warp_Core_Broken,
        Give_Warp_Core_Black,
        Give_Warp_Core_White,
        Give_Warp_Core_None,
        Give_Lantern_Basic,
        Give_Lantern_Broken,
        Give_Lantern_Gen1,
        Give_Lantern_Gen2,
        Give_Lantern_Gen3,
        Give_Slide_Story_1,
        Give_Slide_Story_2,
        Give_Slide_Story_3,
        Give_Slide_Story_4,
        Give_Slide_Story_5,
        Give_Slide_Path_1,
        Give_Slide_Path_2,
        Give_Slide_Path_3,
        Give_Slide_Seal_1,
        Give_Slide_Seal_2,
        Give_Slide_Seal_3,
        Give_Slide_Rule_1,
        Give_Slide_Rule_2,
        Give_Slide_Rule_3,
        Give_Slide_Rule_4,
        Give_Slide_Burning,
        Give_Slide_Experiment,
        Give_Slide_DamageReport,
        Give_Slide_LanternSecret,
        Give_Slide_Prisoner,
        Give_Slide_PrisonerFarewell,
        Give_Slide_Tower,
        Give_Slide_SignalJammer,
        Give_Slide_Homeworld,
        Give_Slide_SupernovaEscape,
        Toggle_Fog,
        Toggle_Position_Display,
        Toggle_Planet_Position_Display,
        Toggle_Bramble_Portal_Display,
        Toggle_Warp_Pad_Display,
        Log_Fact_Reveals,
        Log_Save_Condition_Changes,
        Log_Dialogue_Condition_Changes
    }

    public class MainClass : ModBehaviour
    {
        private const string verison = "0.6.1";
        private ScreenPrompt cheatsTagger = new ScreenPrompt("");

        bool cheatsEnabled = true;
        InputMapping<CheatOptions> inputs = new InputMapping<CheatOptions>();

        void Start()
        {
            ModHelper.Events.Player.OnPlayerAwake += (player) => onAwake();

            ModHelper.Console.WriteLine("CheatMods ready!");
        }

        void Destory()
        {
            ModHelper.Console.WriteLine("CheatMods clean up!");
        }

        public override void Configure(IModConfig config)
        {
            cheatsEnabled = config.Enabled;

            Player.isInvincible = ConfigHelper.getConfigOrDefault<bool>(config, "Invincible", false);
            Ship.isInvincible = Player.isInvincible;

            Player.hasUnlimitedFuel = ConfigHelper.getConfigOrDefault<bool>(config, "Unlimited Fuel", false);
            Ship.hasUnlimitedFuel = Player.hasUnlimitedFuel;

            Player.hasUnlimitedOxygen = ConfigHelper.getConfigOrDefault<bool>(config, "Unlimited Oxygen", false);
            Ship.hasUnlimitedOxygen = Player.hasUnlimitedOxygen;

            Player.hasUnlimitedHealth = ConfigHelper.getConfigOrDefault<bool>(config, "Unlimited Health", false);
            Player.hasUnlimitedBoost = ConfigHelper.getConfigOrDefault<bool>(config, "Unlimited Boost", false);

            Anglerfish.enabledAI = ConfigHelper.getConfigOrDefault<bool>(config, "Anglerfish AI", true);
            Inhabitants.enabledAI = ConfigHelper.getConfigOrDefault<bool>(config, "Inhabitants AI", true);
            Fog.enabled = ConfigHelper.getConfigOrDefault<bool>(config, "Fog", true);

            inputs.Clear();
            inputs.addInput(config, CheatOptions.Fill_Fuel_and_Health, "C,J");
            inputs.addInput(config, CheatOptions.Toggle_Launch_Codes, "C,L");
            inputs.addInput(config, CheatOptions.Toggle_Eye_Coordinates, "C,E");
            inputs.addInput(config, CheatOptions.Toggle_All_Frequencies, "C,F");
            inputs.addInput(config, CheatOptions.Toggle_Rumors, "C,R");
            inputs.addInput(config, CheatOptions.Toggle_Helmet, "C,H");
            inputs.addInput(config, CheatOptions.Toggle_Invinciblity, "C,I");
            inputs.addInput(config, CheatOptions.Toggle_Spacesuit, "C,G");
            inputs.addInput(config, CheatOptions.Toggle_Training_Suit, "C,Digit1");
            inputs.addInput(config, CheatOptions.Toggle_Player_Collision, "C,N");
            inputs.addInput(config, CheatOptions.Toggle_Ship_Collision, "C,M");
            inputs.addInput(config, CheatOptions.Toggle_Unlimited_Boost, "C,T");
            inputs.addInput(config, CheatOptions.Toggle_Unlimited_Fuel, "C,Y");
            inputs.addInput(config, CheatOptions.Toggle_Unlimited_Oxygen, "C,O");
            inputs.addInput(config, CheatOptions.Toggle_Unlimited_Health, "C,U");

            inputs.addInput(config, CheatOptions.Teleport_To_Sun, "T,Digit1");
            inputs.addInput(config, CheatOptions.Teleport_To_SunStation, "T,Digit2");
            inputs.addInput(config, CheatOptions.Teleport_To_EmberTwin, "T,Digit3");
            inputs.addInput(config, CheatOptions.Teleport_To_AshTwin, "T,Digit4");
            inputs.addInput(config, CheatOptions.Teleport_To_TimerHearth, "T,Digit5");
            inputs.addInput(config, CheatOptions.Teleport_To_Attlerock, "T,Digit6");
            inputs.addInput(config, CheatOptions.Teleport_To_BrittleHollow, "T,Digit7");
            inputs.addInput(config, CheatOptions.Teleport_To_HollowLattern, "T,Digit8");
            inputs.addInput(config, CheatOptions.Teleport_To_GiantsDeep, "T,Digit9");
            inputs.addInput(config, CheatOptions.Teleport_To_DarkBramble, "T,Digit0");

            inputs.addInput(config, CheatOptions.Teleport_To_Interloper, "T,Numpad0");
            inputs.addInput(config, CheatOptions.Teleport_To_TimerHearth_Probe, "T,Numpad1");
            inputs.addInput(config, CheatOptions.Teleport_To_ProbeCannon, "T,Numpad2");
            inputs.addInput(config, CheatOptions.Teleport_To_WhiteHole, "T,Numpad3");
            inputs.addInput(config, CheatOptions.Teleport_To_WhiteHoleStation, "T,Numpad4");
            inputs.addInput(config, CheatOptions.Teleport_To_Stranger, "T,Numpad5");
            inputs.addInput(config, CheatOptions.Teleport_To_DreamWorld, "T,Numpad6");
            inputs.addInput(config, CheatOptions.Teleport_To_QuantumMoon, "T,Numpad7");
            inputs.addInput(config, CheatOptions.Teleport_To_AshTwinProject, "T,Numpad8");
            inputs.addInput(config, CheatOptions.Teleport_To_Ship, "T,Numpad9");
            inputs.addInput(config, CheatOptions.Teleport_Ship_To_Player, "T,NumpadDivide");
            inputs.addInput(config, CheatOptions.Teleport_To_Probe, "T,NumpadMultiply");
            inputs.addInput(config, CheatOptions.Teleport_To_Nomai_Probe, "T,NumpadMinus");
            inputs.addInput(config, CheatOptions.Teleport_To_Vessel, "T,NumpadPlus");
            inputs.addInput(config, CheatOptions.Teleport_To_ProbeCannonCommandModule, "T,NumpadPeriod");
            inputs.addInput(config, CheatOptions.Teleport_To_Mapping_Satellite, "T,M");
            inputs.addInput(config, CheatOptions.Teleport_To_Backer_Satellite, "T,B");

            inputs.addInput(config, CheatOptions.Toggle_Anglerfish_AI, "V,I");
            inputs.addInput(config, CheatOptions.Toggle_Inhabitants_AI, "V,O");
            inputs.addInput(config, CheatOptions.Toggle_Inhabitants_Hostility, "V,H");
            inputs.addInput(config, CheatOptions.Toggle_Supernova_Timer, "V,0");
            inputs.addInput(config, CheatOptions.Decrease_Supernova_Timer, "V,Minus");
            inputs.addInput(config, CheatOptions.Increase_Supernova_Timer, "V,Equals");

            inputs.addInput(config, CheatOptions.Quantum_Moon_Collapse, "Q,Digit0");
            inputs.addInput(config, CheatOptions.Decrease_Jetpack_Acceleration, "P,Minus");
            inputs.addInput(config, CheatOptions.Increase_Jetpack_Acceleration, "P,Equals");
            inputs.addInput(config, CheatOptions.Decrease_Ship_Acceleration, "O,Minus");
            inputs.addInput(config, CheatOptions.Increase_Ship_Acceleration, "O,Equals");

            inputs.addInput(config, CheatOptions.Give_Warp_Core, "G,W");
            inputs.addInput(config, CheatOptions.Give_Warp_Core_Vessel, "G,T,Digit1");
            inputs.addInput(config, CheatOptions.Give_Warp_Core_Broken, "G,T,Digit2");
            inputs.addInput(config, CheatOptions.Give_Warp_Core_Black, "G,T,Digit3");
            inputs.addInput(config, CheatOptions.Give_Warp_Core_White, "G,T,Digit4");
            inputs.addInput(config, CheatOptions.Give_Warp_Core_None, "G,T,Digit5");
            inputs.addInput(config, CheatOptions.Give_Lantern_Basic, "G,L,Digit1");
            inputs.addInput(config, CheatOptions.Give_Lantern_Broken, "G,L,Digit2");
            inputs.addInput(config, CheatOptions.Give_Lantern_Gen1, "G,L,Digit3");
            inputs.addInput(config, CheatOptions.Give_Lantern_Gen2, "G,L,Digit4");
            inputs.addInput(config, CheatOptions.Give_Lantern_Gen3, "G,L,Digit5");
            inputs.addInput(config, CheatOptions.Give_Slide_Story_1, "G,R,S,Digit1");
            inputs.addInput(config, CheatOptions.Give_Slide_Story_2, "G,R,S,Digit2");
            inputs.addInput(config, CheatOptions.Give_Slide_Story_3, "G,R,S,Digit3");
            inputs.addInput(config, CheatOptions.Give_Slide_Story_4, "G,R,S,Digit4");
            inputs.addInput(config, CheatOptions.Give_Slide_Story_5, "G,R,S,Digit5");
            inputs.addInput(config, CheatOptions.Give_Slide_Burning, "G,R,S,Digit6");
            inputs.addInput(config, CheatOptions.Give_Slide_Experiment, "G,R,S,Digit7");
            inputs.addInput(config, CheatOptions.Give_Slide_DamageReport, "G,R,S,Digit8");
            inputs.addInput(config, CheatOptions.Give_Slide_Prisoner, "G,R,S,Numpad0");
            inputs.addInput(config, CheatOptions.Give_Slide_PrisonerFarewell, "G,R,S,Numpad1");
            inputs.addInput(config, CheatOptions.Give_Slide_Tower, "G,R,S,Numpad2");
            inputs.addInput(config, CheatOptions.Give_Slide_SignalJammer, "G,R,S,Numpad3");
            inputs.addInput(config, CheatOptions.Give_Slide_Homeworld, "G,R,S,Numpad4");
            inputs.addInput(config, CheatOptions.Give_Slide_SupernovaEscape, "G,R,S,Numpad5");
            inputs.addInput(config, CheatOptions.Give_Slide_Path_1, "G,R,P,Digit1");
            inputs.addInput(config, CheatOptions.Give_Slide_Path_2, "G,R,P,Digit2");
            inputs.addInput(config, CheatOptions.Give_Slide_Path_3, "G,R,P,Digit3");
            inputs.addInput(config, CheatOptions.Give_Slide_Seal_1, "G,R,P,Digit4");
            inputs.addInput(config, CheatOptions.Give_Slide_Seal_2, "G,R,P,Digit5");
            inputs.addInput(config, CheatOptions.Give_Slide_Seal_3, "G,R,P,Digit6");
            inputs.addInput(config, CheatOptions.Give_Slide_Rule_1, "G,R,P,Digit7");
            inputs.addInput(config, CheatOptions.Give_Slide_Rule_2, "G,R,P,Digit8");
            inputs.addInput(config, CheatOptions.Give_Slide_Rule_3, "G,R,P,Digit9");
            inputs.addInput(config, CheatOptions.Give_Slide_Rule_4, "G,R,P,Digit0");

            inputs.addInput(config, CheatOptions.Toggle_Fog, "F,O,G");
            inputs.addInput(config, CheatOptions.Toggle_Position_Display, "D,P");
            inputs.addInput(config, CheatOptions.Toggle_Planet_Position_Display, "D,Z");
            inputs.addInput(config, CheatOptions.Toggle_Bramble_Portal_Display, "D,B");
            inputs.addInput(config, CheatOptions.Toggle_Warp_Pad_Display, "D,W");
            inputs.addInput(config, CheatOptions.Log_Fact_Reveals, "L,Digit1");
            inputs.addInput(config, CheatOptions.Log_Save_Condition_Changes, "L,Digit2");
            inputs.addInput(config, CheatOptions.Log_Dialogue_Condition_Changes, "L,Digit3");

            ModHelper.Console.WriteLine("CheatMods Confgiured!");
        }



        void onAwake()
        {
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
            inputs.Update();
            if (cheatsEnabled)
            {
                var currentFrame = inputs.getPressedThisFrame();
                currentFrame = currentFrame.FindAll(x => x.Item2.keyMatchCount() == currentFrame[0].Item2.keyMatchCount());

                foreach (var input in currentFrame)
                {
                    switch(input.Item1)
                    {
                        case CheatOptions.Fill_Fuel_and_Health:
                            Player.oxygenSeconds = Player.maxOxygenSeconds;
                            Player.fuelSeconds = Player.maxFuelSeconds;
                            Player.health = Player.maxHealth;
                            Player.boostSeconds = Player.maxBoostSeconds;
                            Ship.repair();
                            break;
                        case CheatOptions.Toggle_Launch_Codes:
                            Data.launchCodes = !Data.launchCodes;
                            ModHelper.Console.WriteLine("CheatsMod: Launch Codes Known " + Data.launchCodes);
                            break;
                        case CheatOptions.Toggle_Eye_Coordinates:
                            Data.eyeCoordinates = !Data.eyeCoordinates;
                            ModHelper.Console.WriteLine("CheatsMod: Eye Coordinates Known " + Data.eyeCoordinates);
                            break;
                        case CheatOptions.Toggle_All_Frequencies:
                            toggleFrequencies();
                            break;
                        case CheatOptions.Toggle_Rumors:
                            toggleFacts();
                            break;
                        case CheatOptions.Teleport_To_Sun:
                            Teleportation.teleportPlayerToSun();
                            break;
                        case CheatOptions.Teleport_To_SunStation:
                            Teleportation.teleportPlayerToSunStation();
                            break;
                        case CheatOptions.Teleport_To_EmberTwin:
                            Teleportation.teleportPlayerToEmberTwin();
                            break;
                        case CheatOptions.Teleport_To_AshTwin:
                            Teleportation.teleportPlayerToAshTwin();
                            break;
                        case CheatOptions.Teleport_To_AshTwinProject:
                            Teleportation.teleportPlayerToAshTwinProject();
                            break;
                        case CheatOptions.Teleport_To_TimerHearth:
                            Teleportation.teleportPlayerToTimberHearth();
                            break;
                        case CheatOptions.Teleport_To_TimerHearth_Probe:
                            Teleportation.teleportPlayerToTimberHearthProbe();
                            break;
                        case CheatOptions.Teleport_To_Attlerock:
                            Teleportation.teleportPlayerToAttlerock();
                            break;
                        case CheatOptions.Teleport_To_BrittleHollow:
                            Teleportation.teleportPlayerToBlackHoleForgeTeleporter();
                            break;
                        case CheatOptions.Teleport_To_HollowLattern:
                            Teleportation.teleportPlayerToHollowLattern();
                            break;
                        case CheatOptions.Teleport_To_GiantsDeep:
                            Teleportation.teleportPlayerToGiantsDeep();
                            break;
                        case CheatOptions.Teleport_To_ProbeCannon:
                            Teleportation.teleportPlayerToProbeCannon();
                            break;
                        case CheatOptions.Teleport_To_ProbeCannonCommandModule:
                            Teleportation.teleportPlayerToProbeCannonCommandModule();
                            break;
                        case CheatOptions.Teleport_To_DarkBramble:
                            Teleportation.teleportPlayerToDarkBramble();
                            break;
                        case CheatOptions.Teleport_To_Vessel:
                            Teleportation.teleportPlayerToVessel();
                            break;
                        case CheatOptions.Teleport_To_Ship:
                            Teleportation.teleportPlayerToShip();
                            break;
                        case CheatOptions.Teleport_Ship_To_Player:
                            Teleportation.teleportShipToPlayer();
                            break;
                        case CheatOptions.Teleport_To_Probe:
                            Teleportation.teleportPlayerToProbe();
                            break;
                        case CheatOptions.Teleport_To_Nomai_Probe:
                            Teleportation.teleportPlayerToNomaiProbe();
                            break;
                        case CheatOptions.Teleport_To_Interloper:
                            Teleportation.teleportPlayerToInterloper();
                            break;
                        case CheatOptions.Teleport_To_WhiteHole:
                            Teleportation.teleportPlayerToWhiteHole();
                            break;
                        case CheatOptions.Teleport_To_WhiteHoleStation:
                            Teleportation.teleportPlayerToWhiteHoleStation();
                            break;
                        case CheatOptions.Teleport_To_Stranger:
                            Teleportation.teleportPlayerToStranger();
                            break;
                        case CheatOptions.Teleport_To_DreamWorld:
                            Teleportation.teleportPlayerToDreamWorld();
                            break;
                        case CheatOptions.Teleport_To_QuantumMoon:
                            Teleportation.teleportPlayerToQuantumMoon();
                            break;
                        case CheatOptions.Teleport_To_Mapping_Satellite:
                            Teleportation.teleportPlayerToMappingSatellite();
                            break;
                        case CheatOptions.Teleport_To_Backer_Satellite:
                            Teleportation.teleportPlayerToBackerSatellite();
                            break;
                        case CheatOptions.Toggle_Helmet:
                            Player.helmet = !Player.helmet;
                            ModHelper.Console.WriteLine("CheatsMod: Player Helmet " + Player.helmet);
                            break;
                        case CheatOptions.Toggle_Player_Collision:
                            Player.collision = !Player.collision;
                            ModHelper.Console.WriteLine("CheatsMod: Player Collision " + Player.collision);
                            break;
                        case CheatOptions.Toggle_Ship_Collision:
                            Ship.collision = !Ship.collision;
                            ModHelper.Console.WriteLine("CheatsMod: Ship Collision " + Ship.collision);
                            break;
                        case CheatOptions.Toggle_Training_Suit:
                            Player.trainingSuit = !Player.trainingSuit;
                            ModHelper.Console.WriteLine("CheatsMod: Training Suit " + Player.trainingSuit);
                            break;
                        case CheatOptions.Toggle_Spacesuit:
                            Player.spaceSuit = !Player.spaceSuit;
                            ModHelper.Console.WriteLine("CheatsMod: Space Suit " + Player.spaceSuit);
                            break;
                        case CheatOptions.Toggle_Invinciblity:
                            Player.isInvincible = !Player.isInvincible;
                            Ship.isInvincible = Player.isInvincible;
                            ModHelper.Console.WriteLine("CheatsMod: Invicible " + Player.isInvincible);
                            break;
                        case CheatOptions.Toggle_Unlimited_Fuel:
                            Player.hasUnlimitedFuel = !Player.hasUnlimitedFuel;
                            Ship.hasUnlimitedFuel = Player.hasUnlimitedFuel;
                            ModHelper.Console.WriteLine("CheatsMod: Unlimited Fuel " + Player.hasUnlimitedFuel);
                            break;
                        case CheatOptions.Toggle_Unlimited_Boost:
                            Player.hasUnlimitedBoost = !Player.hasUnlimitedBoost;
                            ModHelper.Console.WriteLine("CheatsMod: Unlimited Boost " + Player.hasUnlimitedBoost);
                            break;
                        case CheatOptions.Toggle_Unlimited_Health:
                            Player.hasUnlimitedHealth = !Player.hasUnlimitedHealth;
                            ModHelper.Console.WriteLine("CheatsMod: Unlimited Health " + Player.hasUnlimitedHealth);
                            break;
                        case CheatOptions.Toggle_Unlimited_Oxygen:
                            Player.hasUnlimitedOxygen = !Player.hasUnlimitedOxygen;
                            Ship.hasUnlimitedOxygen = Player.hasUnlimitedOxygen;
                            ModHelper.Console.WriteLine("CheatsMod: Unlimited Oxygen " + Player.hasUnlimitedOxygen);
                            break;
                        case CheatOptions.Toggle_Anglerfish_AI:
                            Anglerfish.enabledAI = !Anglerfish.enabledAI;
                            ModHelper.Console.WriteLine("CheatsMod: Anglerfish AI " + Anglerfish.enabledAI);
                            break;
                        case CheatOptions.Toggle_Inhabitants_AI:
                            Inhabitants.enabledAI = !Inhabitants.enabledAI;
                            ModHelper.Console.WriteLine("CheatsMod: Inhabitants AI " + Inhabitants.enabledAI);
                            break;
                        case CheatOptions.Toggle_Inhabitants_Hostility:
                            Inhabitants.enabledHostility = !Inhabitants.enabledHostility;
                            ModHelper.Console.WriteLine("CheatsMod: Inhabitants Hostility " + Inhabitants.enabledHostility);
                            break;
                        case CheatOptions.Toggle_Supernova_Timer:
                            SuperNova.freeze = !SuperNova.freeze;
                            ModHelper.Console.WriteLine("CheatsMod: SuperNova Frozen " + SuperNova.freeze);
                            break;
                        case CheatOptions.Decrease_Supernova_Timer:
                            SuperNova.remaining -= 60f;
                            ModHelper.Console.WriteLine("CheatsMod: Remaining Time " + SuperNova.remaining);
                            break;
                        case CheatOptions.Increase_Supernova_Timer:
                            SuperNova.remaining += 60f;
                            ModHelper.Console.WriteLine("CheatsMod: Remaining Time " + SuperNova.remaining);
                            break;
                        case CheatOptions.Decrease_Jetpack_Acceleration:
                            Player.thrust = Player.thrust / 2f;
                            ModHelper.Console.WriteLine("CheatsMod: JetPack Acceleration Multiplier " + (Player.thrust / 6f));
                            break;
                        case CheatOptions.Increase_Jetpack_Acceleration:
                            Player.thrust = Player.thrust * 2f;
                            ModHelper.Console.WriteLine("CheatsMod: JetPack Acceleration Multiplier " + (Player.thrust / 6f));
                            break;
                        case CheatOptions.Decrease_Ship_Acceleration:
                            Ship.thrust = Ship.thrust / 2f;
                            ModHelper.Console.WriteLine("CheatsMod: Ship Acceleration Multiplier " + (Ship.thrust / 50f));
                            break;
                        case CheatOptions.Increase_Ship_Acceleration:
                            Ship.thrust = Ship.thrust * 2f;
                            ModHelper.Console.WriteLine("CheatsMod: Ship Acceleration Multiplier " + (Ship.thrust / 50f));
                            break;
                        case CheatOptions.Quantum_Moon_Collapse:
                            QuantumMoonHelper.collapse();
                            break;
                        case CheatOptions.Give_Warp_Core:
                            Possession.pickUpWarpCore(WarpCoreType.Vessel);
                            break;
                        case CheatOptions.Give_Warp_Core_Vessel:
                            Possession.pickUpWarpCore(WarpCoreType.Vessel);
                            break;
                        case CheatOptions.Give_Warp_Core_Broken:
                            Possession.pickUpWarpCore(WarpCoreType.VesselBroken);
                            break;
                        case CheatOptions.Give_Warp_Core_Black:
                            Possession.pickUpWarpCore(WarpCoreType.Black);
                            break;
                        case CheatOptions.Give_Warp_Core_White:
                            Possession.pickUpWarpCore(WarpCoreType.White);
                            break;
                        case CheatOptions.Give_Warp_Core_None:
                            Possession.pickUpWarpCore(WarpCoreType.SimpleBroken);
                            break;
                        case CheatOptions.Give_Lantern_Basic:
                            Possession.pickUpLantern(false, true);
                            break;
                        case CheatOptions.Give_Lantern_Broken:
                            Possession.pickUpLantern(true, false);
                            break;
                        case CheatOptions.Give_Lantern_Gen1:
                            Possession.pickUpDreamLantern(DreamLanternType.Nonfunctioning, false);
                            break;
                        case CheatOptions.Give_Lantern_Gen2:
                            Possession.pickUpDreamLantern(DreamLanternType.Malfunctioning, true);
                            break;
                        case CheatOptions.Give_Lantern_Gen3:
                            Possession.pickUpDreamLantern(DreamLanternType.Functioning, false);
                            break;
                        case CheatOptions.Give_Slide_Story_1:
                            Possession.pickUpSlideReel(Items.SlideReelStory.Story_1, false);
                            break;
                        case CheatOptions.Give_Slide_Story_2:
                            Possession.pickUpSlideReel(Items.SlideReelStory.Story_2, false);
                            break;
                        case CheatOptions.Give_Slide_Story_3:
                            Possession.pickUpSlideReel(Items.SlideReelStory.Story_3, false);
                            break;
                        case CheatOptions.Give_Slide_Story_4:
                            Possession.pickUpSlideReel(Items.SlideReelStory.Story_4, false);
                            break;
                        case CheatOptions.Give_Slide_Story_5:
                            Possession.pickUpSlideReel(Items.SlideReelStory.Story_5_Complete, false);
                            break;
                        case CheatOptions.Give_Slide_Path_1:
                            Possession.pickUpSlideReel(Items.SlideReelStory.LibraryPath_1, false);
                            break;
                        case CheatOptions.Give_Slide_Path_2:
                            Possession.pickUpSlideReel(Items.SlideReelStory.LibraryPath_2, false);
                            break;
                        case CheatOptions.Give_Slide_Path_3:
                            Possession.pickUpSlideReel(Items.SlideReelStory.LibraryPath_3, false);
                            break;
                        case CheatOptions.Give_Slide_Seal_1:
                            Possession.pickUpSlideReel(Items.SlideReelStory.Seal_1, false);
                            break;
                        case CheatOptions.Give_Slide_Seal_2:
                            Possession.pickUpSlideReel(Items.SlideReelStory.Seal_2, false);
                            break;
                        case CheatOptions.Give_Slide_Seal_3:
                            Possession.pickUpSlideReel(Items.SlideReelStory.Seal_3, false);
                            break;
                        case CheatOptions.Give_Slide_Rule_1:
                            Possession.pickUpSlideReel(Items.SlideReelStory.DreamRule_1, false);
                            break;
                        case CheatOptions.Give_Slide_Rule_2:
                            Possession.pickUpSlideReel(Items.SlideReelStory.DreamRule_2_v1, false);
                            break;
                        case CheatOptions.Give_Slide_Rule_3:
                            Possession.pickUpSlideReel(Items.SlideReelStory.DreamRule_2_v2, false);
                            break;
                        case CheatOptions.Give_Slide_Rule_4:
                            Possession.pickUpSlideReel(Items.SlideReelStory.DreamRule_3, false);
                            break;
                        case CheatOptions.Give_Slide_Burning:
                            Possession.pickUpSlideReel(Items.SlideReelStory.Burning, false);
                            break;
                        case CheatOptions.Give_Slide_Experiment:
                            Possession.pickUpSlideReel(Items.SlideReelStory.Experiment, false);
                            break;
                        case CheatOptions.Give_Slide_DamageReport:
                            Possession.pickUpSlideReel(Items.SlideReelStory.DamageReport, false);
                            break;
                        case CheatOptions.Give_Slide_LanternSecret:
                            Possession.pickUpSlideReel(Items.SlideReelStory.LanternSecret, false);
                            break;
                        case CheatOptions.Give_Slide_Prisoner:
                            Possession.pickUpSlideReel(Items.SlideReelStory.PrisonPeephole_Vision, false);
                            break;
                        case CheatOptions.Give_Slide_PrisonerFarewell:
                            Possession.pickUpSlideReel(Items.SlideReelStory.PrisonerFarewellVision, false);
                            break;
                        case CheatOptions.Give_Slide_Tower:
                            Possession.pickUpSlideReel(Items.SlideReelStory.TowerVision, false);
                            break;
                        case CheatOptions.Give_Slide_SignalJammer:
                            Possession.pickUpSlideReel(Items.SlideReelStory.SignalJammer, false);
                            break;
                        case CheatOptions.Give_Slide_Homeworld:
                            Possession.pickUpSlideReel(Items.SlideReelStory.Homeworld, false);
                            break;
                        case CheatOptions.Give_Slide_SupernovaEscape:
                            Possession.pickUpSlideReel(Items.SlideReelStory.SupernovaEscape, false);
                            break;
                        case CheatOptions.Toggle_Fog:
                            Fog.enabled = !Fog.enabled;
                            ModHelper.Console.WriteLine("CheatsMod: Fog " + Fog.enabled);
                            break;
                        case CheatOptions.Toggle_Position_Display:
                            Position.debugPlayerPosition = !Position.debugPlayerPosition;
                            break;
                        case CheatOptions.Toggle_Planet_Position_Display:
                            Planet.debugPlanetPosition = !Planet.debugPlanetPosition;
                            break;
                        case CheatOptions.Toggle_Bramble_Portal_Display:
                            BramblePortals.debugMode = !BramblePortals.debugMode;
                            break;
                        case CheatOptions.Toggle_Warp_Pad_Display:
                            WarpPad.debugMode = !WarpPad.debugMode;
                            break;
                        case CheatOptions.Log_Fact_Reveals:
                            Data.debugFacts = !Data.debugFacts;
                            ModHelper.Console.WriteLine("CheatsMod: Debug Facts " + Data.debugFacts);
                            break;
                        case CheatOptions.Log_Save_Condition_Changes:
                            Data.debugPersistentConditions = !Data.debugPersistentConditions;
                            ModHelper.Console.WriteLine("CheatsMod: Debug Saved Conditions " + Data.debugPersistentConditions);
                            break;
                        case CheatOptions.Log_Dialogue_Condition_Changes:
                            Data.debugDialogConditions = !Data.debugDialogConditions;
                            ModHelper.Console.WriteLine("CheatsMod: Debug Dialogue Conditions " + Data.debugDialogConditions);
                            break;
                        default:
                            ModHelper.Console.WriteLine("CheatsMod: Input not mapped " + input.Item1, MessageType.Warning);
                            break;
                    }
                }
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
