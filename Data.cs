using OWML.Common;
using OWML.ModHelper;
using OWML.ModHelper.Events;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ClassLibrary2
{
    static class Data
    {
        public static bool launchCodes
        {
            get
            { 
                return PlayerData.IsLoaded() && PlayerData.KnowsLaunchCodes();
            }
            set
            {
                if (PlayerData.IsLoaded() && value != launchCodes)
                {
                    if (value)
                    {
                        PlayerData.LearnLaunchCodes();
                        DialogueConditionManager.SharedInstance.SetConditionState("TalkedToHornfels", true);
                        DialogueConditionManager.SharedInstance.SetConditionState("SCIENTIST_3", true);
                        DialogueConditionManager.SharedInstance.SetConditionState("LAUNCH_CODES_GIVEN", true);
                        StandaloneProfileManager.SharedInstance.currentProfileGameSave.SetPersistentCondition("LAUNCH_CODES_GIVEN", true);
                        GlobalMessenger.FireEvent(nameof(PlayerData.LearnLaunchCodes));
                    }
                    else
                    {
                        DialogueConditionManager.SharedInstance.SetConditionState("TalkedToHornfels", false);
                        DialogueConditionManager.SharedInstance.SetConditionState("SCIENTIST_3", false);
                        DialogueConditionManager.SharedInstance.SetConditionState("LAUNCH_CODES_GIVEN", false);
                        StandaloneProfileManager.SharedInstance.currentProfileGameSave.SetPersistentCondition("LAUNCH_CODES_GIVEN", false);
                    }
                    PlayerData.SaveCurrentGame();
                }
            }
        }

        public static bool knowAllSignals
        {
            get
            {
                if (!PlayerData.IsLoaded())
                {
                    return false;
                }

                foreach (SignalName signal in (SignalName[])Enum.GetValues(typeof(SignalName)))
                {
                    if (signal != SignalName.Default && !PlayerData.KnowsSignal(signal))
                    {
                        return false;
                    }
                }
                return true;
            }
            set
            {
                if (!PlayerData.IsLoaded())
                {
                    return;
                }

                if (value)
                {
                    foreach (SignalName signal in (SignalName[])Enum.GetValues(typeof(SignalName)))
                    {
                        StandaloneProfileManager.SharedInstance.currentProfileGameSave.knownSignals[(int)signal] = true;
                    }
                }
                else
                {
                    foreach (SignalName signal in (SignalName[])Enum.GetValues(typeof(SignalName)))
                    {
                        StandaloneProfileManager.SharedInstance.currentProfileGameSave.knownSignals.Remove((int)signal);
                    }
                }
                PlayerData.SaveCurrentGame();
            }
        }

        public static bool knowAllFrequencies
        {
            get
            {
                if (!PlayerData.IsLoaded())
                {
                    return false;
                }

                foreach (SignalFrequency frequency in (SignalFrequency[])Enum.GetValues(typeof(SignalFrequency)))
                {
                    if (frequency != SignalFrequency.Default && !PlayerData.KnowsFrequency(frequency))
                    {
                        return false;
                    }
                }
                return true;
            }
            set
            {
                if (!PlayerData.IsLoaded())
                {
                    return;
                }

                if (value)
                {
                    foreach (SignalFrequency frequency in (SignalFrequency[])Enum.GetValues(typeof(SignalFrequency)))
                    {
                        PlayerData.LearnFrequency(frequency);
                    }
                }
                else
                {
                    PlayerData.ForgetFrequency(SignalFrequency.Quantum);
                    PlayerData.ForgetFrequency(SignalFrequency.EscapePod);
                    PlayerData.ForgetFrequency(SignalFrequency.Statue);
                    PlayerData.ForgetFrequency(SignalFrequency.WarpCore);
                    PlayerData.ForgetFrequency(SignalFrequency.HideAndSeek);
                    PlayerData.ForgetFrequency(SignalFrequency.Radio);
                }
                PlayerData.SaveCurrentGame();
            }
        }

        public static bool knowAllRumors
        {
            get
            {
                if (!Locator.GetShipLogManager())
                {
                    return false;
                }

                foreach (ShipLogFact fact in Locator.GetShipLogManager().GetValue<List<ShipLogFact>>("_factList"))
                {
                    if (!fact.IsRevealed())
                    {
                        if (fact.IsRumor())
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            set
            {
                if (!Locator.GetShipLogManager())
                {
                    return;
                }

                if (value)
                {
                    Locator.GetShipLogManager().RevealAllFacts(false);
                }
                else
                {
                    foreach (ShipLogFact fact in Locator.GetShipLogManager().GetValue<List<ShipLogFact>>("_factList"))
                    {
                        if (!fact.IsRevealed())
                        {
                            if (fact.IsRumor())
                            {
                                var savedFact = StandaloneProfileManager.SharedInstance.currentProfileGameSave.shipLogFactSaves[fact.GetEntryID()];
                                savedFact.newlyRevealed = false;
                                savedFact.read = false;
                                savedFact.revealOrder = -1;
                            }
                        }
                    }
                }
                PlayerData.SaveCurrentGame();
            }
        }

        public static bool knowAllFacts
        {
            get
            {
                if (!Locator.GetShipLogManager())
                {
                    return false;
                }

                foreach (ShipLogFact fact in Locator.GetShipLogManager().GetValue<List<ShipLogFact>>("_factList"))
                {
                    if (!fact.IsRevealed())
                    {
                        if (!fact.IsRumor())
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            set
            {
                if (!Locator.GetShipLogManager())
                {
                    return;
                }

                if (value)
                {
                    Locator.GetShipLogManager().RevealAllFacts(true);
                }
                else
                {
                    foreach (ShipLogFact fact in Locator.GetShipLogManager().GetValue<List<ShipLogFact>>("_factList"))
                    {
                        if (!fact.IsRevealed())
                        {
                            if (!fact.IsRumor())
                            {
                                var savedFact = StandaloneProfileManager.SharedInstance.currentProfileGameSave.shipLogFactSaves[fact.GetEntryID()];
                                savedFact.newlyRevealed = false;
                                savedFact.read = false;
                                savedFact.revealOrder = -1;
                            }
                        }
                    }
                }
                PlayerData.SaveCurrentGame();
            }
        }
    }
}
