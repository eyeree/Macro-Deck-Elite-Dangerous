using EliteDangerousMacroDeckPlugin.Actions.Bindings;
using SuchByte.MacroDeck.Plugins;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace EliteDangerousMacroDeckPlugin.Actions
{

    // There are a lot of possible actions for Elite Dangerous that don't make
    // sense, require a hold button support from macro deck (or alternatively 
    // require configuration for "how long" so they can be used in macros with
    // fixed requirements at least.) See the end of a file for a comment that 
    // contains unfinished code for most of these.
    //
    // EliteDangerous supports four different sets of bindings: General, Ship,
    // Srv, and Foot. There are four complete BindingInfo objects, each of which
    // will have ALL the binding entries. Each action has to decide which set of
    // bindings it should use. Some actions use the ed_context variable value to
    // determine which bindings should be used when they are triggered.
    //
    // The entries are sorted by PluginAction property name, both here and when
    // passed to Macro Deck. The class names from come from the BindingsInfo 
    // property names, which don't match the names used for the actions in most
    // cases.Action names try to follow a consistent naming patterns so they are
    // grouped nicely when displayed in Macro Deck. Macro Deck only provides 
    // limited space for displaying these names.

    public class HumanoidClearAuthorityLevel : StandardBindingAction
    {
        public HumanoidClearAuthorityLevel()
            : base("Authority Level - Clear", "(foot)",
                () => EDBindings.Foot.HumanoidClearAuthorityLevel)
        {
        }
    }

    public class PhotoCameraToggle : StandardBindingAction
    {
        public PhotoCameraToggle()
            : base("Camera - Enable", "",
                context => // Note: all values are in general
                    context.Ship ? EDBindings.General.PhotoCameraToggle : 
                    context.Srv ? EDBindings.General.PhotoCameraToggle_Buggy :
                    context.Foot ? EDBindings.General.PhotoCameraToggle_Humanoid :
                    null)
        {
        }
    }

    public class QuitCamera : StandardBindingAction
    {
        public QuitCamera()
            : base("Camera - Disable", "",
                () => EDBindings.General.QuitCamera)
        {
        }
    }

    public class ToggleFreeCam : StandardBindingAction
    {
        public ToggleFreeCam()
            : base("Camera - Select Free", "", // TODO srv? foot?
                () => EDBindings.General.ToggleFreeCam)
        {
        }
    }

    public class VanityCameraOne : StandardBindingAction
    {
        public VanityCameraOne()
            : base("Camera - Select Cockpit Front", "", // TODO: srv and foot too?
                () => EDBindings.General.VanityCameraOne)
        {
        }
    }

    public class VanityCameraTwo : StandardBindingAction
    {
        public VanityCameraTwo()
            : base("Camera - Select Cockpit Back", "",
                () => EDBindings.General.VanityCameraTwo)
        {
        }
    }

    public class VanityCameraThree : StandardBindingAction
    {
        public VanityCameraThree()
            : base("Camera - Select Commander 1", "",
                () => EDBindings.General.VanityCameraThree)
        {
        }
    }

    public class VanityCameraFour : StandardBindingAction
    {
        public VanityCameraFour()
            : base("Camera - Select Commander 2", "",
                () => EDBindings.General.VanityCameraFour)
        {
        }
    }

    public class VanityCameraFive : StandardBindingAction
    {
        public VanityCameraFive()
            : base("Camera - Select Copilot 1", "",
                () => EDBindings.General.VanityCameraFive)
        {
        }
    }

    public class VanityCameraSix : StandardBindingAction
    {
        public VanityCameraSix()
            : base("Camera - Select Copilot 2", "",
                () => EDBindings.General.VanityCameraSix)
        {
        }
    }

    public class VanityCameraSeven : StandardBindingAction
    {
        public VanityCameraSeven()
            : base("Camera - Select Copilot 3", "",
                () => EDBindings.General.VanityCameraSeven)
        {
        }
    }

    public class VanityCameraEight : StandardBindingAction
    {
        public VanityCameraEight()
            : base("Camera - Select Front", "",
                () => EDBindings.General.VanityCameraEight)
        {
        }
    }

    public class VanityCameraNine : StandardBindingAction
    {
        public VanityCameraNine()
            : base("Camera - Select Back", "",
                () => EDBindings.General.VanityCameraNine)
        {
        }
    }

    public class VanityCameraTen : StandardBindingAction
    {
        public VanityCameraTen()
            : base("Camera - Select Back (Again)", "", // TODO ???
                () => EDBindings.General.VanityCameraTen)
        {
        }
    }

    public class FixCameraRelativeToggle : StandardBindingAction
    {
        public FixCameraRelativeToggle()
            : base("Camera - Toggle Control", "(free camera)", // TODO: or srv?
                () => EDBindings.General.FixCameraRelativeToggle)
        {
        }
    }

    public class FixCameraWorldToggle : StandardBindingAction
    {
        public FixCameraWorldToggle()
            : base("Camera - Toggle Connect", "(free camera)", // TOOD: or srv?
                () => EDBindings.General.FixCameraWorldToggle)
        {
        }
    }

    public class FreeCamToggleHUD : StandardBindingAction
    {
        public FreeCamToggleHUD()
            : base("Camera - Toggle HUD", "(free camera)",
                () => EDBindings.General.FreeCamToggleHUD)
        {
        }
    }

    public class ToggleRotationLock : StandardBindingAction
    {
        public ToggleRotationLock()
            : base("Camera - Toggle Stabilizer", "(free camera)",
                () => EDBindings.General.ToggleRotationLock)
        {
        }
    }

    public class EjectAllCargo : StandardBindingAction
    {
        public EjectAllCargo()
            : base("Cargo - Eject", "(ship, srv)",
                context =>
                    context.Ship ? EDBindings.Ship.EjectAllCargo :
                    context.Srv ? EDBindings.Srv.EjectAllCargo_Buggy :
                    null)
        {
        }
    }

    public class FireChaffLauncher : StandardBindingAction
    {
        public FireChaffLauncher()
            : base("Chaff - Launch", "(ship)",
                () => EDBindings.Ship.FireChaffLauncher)
        {
        }
    }

    public class HumanoidCrouchButton : StandardBindingAction
    {
        public HumanoidCrouchButton()
            : base("Crouch - Toggle", "(foot)",
                () => EDBindings.Foot.HumanoidCrouchButton)
        {
        }
    }

    public class PlayerHUDModeAnalysis : StandardBindingAction
    {
        public PlayerHUDModeAnalysis()
            : base("Cockpit Mode - Select Analysis", "(ship)",
                () => getBool("ed_hud_in_analysis_mode") ? null : EDBindings.Ship.PlayerHUDModeToggle)
        {
        }
    }

    public class PlayerHUDModeCombat : StandardBindingAction
    {
        public PlayerHUDModeCombat()
            : base("Cockpit Mode - Select Combat", "(ship)",
                () => getBool("ed_hud_in_analysis_mode") ? EDBindings.Ship.PlayerHUDModeToggle : null)
        {
        }
    }

    public class PlayerHUDModeToggle : StandardBindingAction
    {
        public PlayerHUDModeToggle()
            : base("Cockpit Mode - Switch", "(ship)",
                () => EDBindings.Ship.PlayerHUDModeToggle)
        {
        }
    }

    public class ExplorationSAANextGenus : StandardBindingAction
    {
        public ExplorationSAANextGenus()
            : base("DSS Filter - Next", "(ship)",
                () => EDBindings.Ship.ExplorationSAANextGenus)
        {
        }
    }

    public class ExplorationSAAPreviousGenus : StandardBindingAction
    {
        public ExplorationSAAPreviousGenus()
            : base("DSS Filter - Previous", "(ship)",
                () => EDBindings.Ship.ExplorationSAAPreviousGenus)
        {
        }
    }

    public class HumanoidBattery : StandardBindingAction
    {
        public HumanoidBattery()
            : base("Energy Cell - Use", "(foot)",
                () => EDBindings.Foot.HumanoidBattery)
        {
        }
    }

    public class UseBoostJuice : StandardBindingAction
    {
        public UseBoostJuice()
            : base("Engine Boost - Use", "(ship)", // TODO: does this require hold?
                () => EDBindings.Ship.UseBoostJuice)
        {
        }
    }

    public class EngineColourToggle : StandardBindingAction
    {
        public EngineColourToggle()
            : base("Engine Colour - Toggle", "(ship)",
                () => EDBindings.Ship.EngineColourToggle)
        {
        }
    }

    public class HumanoidEmoteSlot1 : StandardBindingAction
    {
        public HumanoidEmoteSlot1()
            : base("Emote - Point", "(foot)",
                () => EDBindings.Foot.HumanoidEmoteSlot1)
        {
        }
    }

    public class HumanoidEmoteSlot2 : StandardBindingAction
    {
        public HumanoidEmoteSlot2()
            : base("Emote - Wave", "(foot)",
                () => EDBindings.Foot.HumanoidEmoteSlot2)
        {
        }
    }

    public class HumanoidEmoteSlot3 : StandardBindingAction
    {
        public HumanoidEmoteSlot3()
            : base("Emote - Agree", "(foot)",
                () => EDBindings.Foot.HumanoidEmoteSlot3)
        {
        }
    }

    public class HumanoidEmoteSlot4 : StandardBindingAction
    {
        public HumanoidEmoteSlot4()
            : base("Emote - Disagree", "(foot)",
                () => EDBindings.Foot.HumanoidEmoteSlot4)
        {
        }
    }

    public class HumanoidEmoteSlot5 : StandardBindingAction
    {
        public HumanoidEmoteSlot5()
            : base("Emote - Go", "(foot)",
                () => EDBindings.Foot.HumanoidEmoteSlot5)
        {
        }
    }

    public class HumanoidEmoteSlot6 : StandardBindingAction
    {
        public HumanoidEmoteSlot6()
            : base("Emote - Stop", "(foot)",
                () => EDBindings.Foot.HumanoidEmoteSlot6)
        {
        }
    }

    public class HumanoidEmoteSlot7 : StandardBindingAction
    {
        public HumanoidEmoteSlot7()
            : base("Emote - Applaud", "(foot)",
                () => EDBindings.Foot.HumanoidEmoteSlot7)
        {
        }
    }

    public class HumanoidEmoteSlot8 : StandardBindingAction
    {
        public HumanoidEmoteSlot8()
            : base("Emote - Salute", "(foot)",
                () => EDBindings.Foot.HumanoidEmoteSlot8)
        {
        }
    }

    public class CycleFireGroupNext : StandardBindingAction
    {
        public CycleFireGroupNext()
            : base("Fire Group - Next", "(ship)",
                () => EDBindings.Ship.CycleFireGroupNext)
        {
        }
    }

    public class CycleFireGroupPrevious : StandardBindingAction
    {
        public CycleFireGroupPrevious()
            : base("Fire Group - Previous", "(ship)",
                () => EDBindings.Ship.CycleFireGroupPrevious)
        {
        }
    }

    public class Hyperspace : StandardBindingAction
    {
        public Hyperspace()
            : base("FSD - Select Hyperspace Jump", "(ship)",
                () => EDBindings.Ship.Hyperspace)
        {
        }
    }

    public class HyperSuperCombination : StandardBindingAction
    {
        public HyperSuperCombination()
            : base("FSD - Toggle", "(ship)",
                () => EDBindings.Ship.HyperSuperCombination)
        {
        }
    }

    public class Supercruise : StandardBindingAction
    {
        public Supercruise()
            : base("FSD - Select Supercruise", "(ship)",
                () => EDBindings.Ship.Supercruise)
        {
        }
    }

    public class ExplorationFSSDiscoveryScan : StandardBindingAction
    {
        public ExplorationFSSDiscoveryScan()
            : base("FSS - Discovery Scan", "(ship)",
                () => EDBindings.Ship.ExplorationFSSDiscoveryScan)
        {
        }
    }

    public class ExplorationFSSEnter : StandardBindingAction
    {
        public ExplorationFSSEnter()
            : base("FSS - Enter", "(ship)",
                () => EDBindings.Ship.ExplorationFSSEnter)
        {
        }
    }

    public class ExplorationFSSQuit : StandardBindingAction
    {
        public ExplorationFSSQuit()
            : base("FSS - Exit", "(ship)",
                () => EDBindings.Ship.ExplorationFSSQuit)
        {
        }
    }

    public class ExplorationFSSShowHelp : StandardBindingAction
    {
        public ExplorationFSSShowHelp()
            : base("FSS - Help", "(ship)",
                () => EDBindings.Ship.ExplorationFSSShowHelp)
        {
        }
    }

    public class ExplorationFSSTarget : StandardBindingAction
    {
        public ExplorationFSSTarget()
            : base("FSS - Target", "(ship)",
                () => EDBindings.Ship.ExplorationFSSTarget)
        {
        }
    }

    public class GalnetAudio_ClearQueue : StandardBindingAction
    {
        public GalnetAudio_ClearQueue()
            : base("Galnet - Clear Queue", "(ship)", // TODO: ship only?
                () => EDBindings.Ship.GalnetAudio_ClearQueue)
        {
        }
    }

    public class GalnetAudio_Play_Pause : StandardBindingAction
    {
        public GalnetAudio_Play_Pause()
            : base("Galnet - Play/Pause", "(ship)",
                () => EDBindings.Ship.GalnetAudio_Play_Pause)
        {
        }
    }

    public class GalnetAudio_SkipBackward : StandardBindingAction
    {
        public GalnetAudio_SkipBackward()
            : base("Galnet - Backward", "(ship)",
                () => EDBindings.Ship.GalnetAudio_SkipBackward)
        {
        }
    }

    public class GalnetAudio_SkipForward : StandardBindingAction
    {
        public GalnetAudio_SkipForward()
            : base("Galnet - Forward", "(ship)",
                () => EDBindings.Ship.GalnetAudio_SkipForward)
        {
        }
    }

    public class HumanoidSelectNextGrenadeTypeButton : StandardBindingAction
    {
        public HumanoidSelectNextGrenadeTypeButton()
            : base("Grenade - Next", "(foot)",
                () => EDBindings.Foot.HumanoidSelectNextGrenadeTypeButton)
        {
        }
    }

    public class HumanoidSelectPreviousGrenadeTypeButton : StandardBindingAction
    {
        public HumanoidSelectPreviousGrenadeTypeButton()
            : base("Grenade - Previous", "(foot)",
                () => EDBindings.Foot.HumanoidSelectPreviousGrenadeTypeButton)
        {
        }
    }

    public class HumanoidSelectEMPGrenade : StandardBindingAction
    {
        public HumanoidSelectEMPGrenade()
            : base("Grenade - Select EMP", "(foot)",
                () => EDBindings.Foot.HumanoidSelectEMPGrenade)
        {
        }
    }

    public class HumanoidSelectFragGrenade : StandardBindingAction
    {
        public HumanoidSelectFragGrenade()
            : base("Grenade - Select Frag", "(foot)",
                () => EDBindings.Foot.HumanoidSelectFragGrenade)
        {
        }
    }

    public class HumanoidSelectShieldGrenade : StandardBindingAction
    {
        public HumanoidSelectShieldGrenade()
            : base("Grenade - Select Shield", "(foot)",
                () => EDBindings.Foot.HumanoidSelectShieldGrenade)
        {
        }
    }

    public class DeployHardpointToggle : StandardBindingAction
    {
        public DeployHardpointToggle()
            : base("Hardpoints - Toggle", "(ship)",
                () => EDBindings.Ship.DeployHardpointToggle)
        {
        }
    }

    public class HumanoidHealthPack : StandardBindingAction
    {
        public HumanoidHealthPack()
            : base("Health Pack - Use", "(foot)",
                () => EDBindings.Foot.HumanoidHealthPack)
        {
        }
    }

    public class DeployHeatSink : StandardBindingAction
    {
        public DeployHeatSink()
            : base("Heatsink - Deploy", "(ship)",
                () => EDBindings.Ship.DeployHeatSink)
        {
        }
    }
    
    public class HMDReset : StandardBindingAction
    {
        public HMDReset()
            : base("HMD - Reset", "Reset VR Headset.",
                () => EDBindings.Ship.HMDReset) // TODO: is this for SRV and on foot too?
        {
        }
    }

    public class LandingGearToggle : StandardBindingAction
    {
        public LandingGearToggle()
            : base("Landing Gear - Toggle", "(ship)",
                () => EDBindings.Ship.LandingGearToggle)
        {
        }
    }

    public class LightsToggle : StandardBindingAction
    {

        public LightsToggle()
            : base("Lights - Toggle", "(ship, srv, foot)",
                context =>
                    context.Ship ? EDBindings.Ship.ShipSpotLightToggle :
                    context.Srv ? EDBindings.Srv.HeadlightsBuggyButton :
                    context.Foot ? EDBindings.Foot.HumanoidToggleFlashlightButton :
                    null)
        {
        }

    }

    public class MouseReset : StandardBindingAction
    {
        public MouseReset()
            : base("Mouse - Reset", "(ship)", // TODO: srv and foot too?
                () => EDBindings.Ship.MouseReset)
        {
        }
    }

    public class OrbitLinesToggle : StandardBindingAction
    {
        public OrbitLinesToggle()
            : base("Orbit Lines - Toggle", "(ship)",
                () => EDBindings.Ship.OrbitLinesToggle)
        {
        }
    }

    public class HumanoidConflictContextualUIButton : StandardBindingAction
    {
        public HumanoidConflictContextualUIButton()
            : base("Panel - Battle Stats", "(foot)",
                () => EDBindings.Foot.HumanoidConflictContextualUIButton)
        {
        }
    }

    public class OpenCodexGoToDiscovery : StandardBindingAction
    {
        public OpenCodexGoToDiscovery()
            : base("Panel - Codex Discovery", "(ship)",
                () => EDBindings.Ship.OpenCodexGoToDiscovery)
        {
        }
    }

    public class FocusCommsPanel : StandardBindingAction
    {
        public FocusCommsPanel()
            : base("Panel - Comms", "(ship, srv, foot)",
                context =>
                    context.Ship ? EDBindings.Ship.FocusCommsPanel :
                    context.Srv ? EDBindings.Srv.FocusCommsPanel_Buggy :
                    context.Foot ? EDBindings.Foot.FocusCommsPanel_Humanoid :
                    null)
        {
        }
    }

    public class FocusLeftPanel : StandardBindingAction
    {
        public FocusLeftPanel()
            : base("Panel - External", "(ship, srv)",
                context =>
                    context.Ship ? EDBindings.Ship.FocusLeftPanel :
                    context.Srv ? EDBindings.Srv.FocusLeftPanel_Buggy :
                    null)
        {
        }
    }

    public class FriendsMenu : StandardBindingAction
    {
        public FriendsMenu()
            : base("Panel - Friends", "(ship, srv)",
                context =>
                    context.Ship ? EDBindings.Ship.FriendsMenu :
                    context.Srv ? EDBindings.Ship.FriendsMenu : // NOTE: ship config used for both
                                                                // TODO: on foot too?
                    null)
        {
        }
    }

    public class OpenOrders : StandardBindingAction
    {
        public OpenOrders()
            : base("Panel - Fighter Orders", "(ship)",
                () => EDBindings.Ship.OpenOrders)
        {
        }
    }

    public class GalaxyMapOpen : StandardBindingAction
    {
        public GalaxyMapOpen()
            : base("Panel - Galaxy Map", "(ship, srv, foot)",
                context =>
                    context.Ship ? EDBindings.Ship.GalaxyMapOpen :
                    context.Srv ? EDBindings.Srv.GalaxyMapOpen_Buggy :
                    context.Foot ? EDBindings.Foot.GalaxyMapOpen_Humanoid :
                    null)
        {
        }
    }

    public class HumanoidOpenAccessPanelButton : StandardBindingAction
    {
        public HumanoidOpenAccessPanelButton()
            : base("Panel - Insight Hub", "(foot)",
                () => EDBindings.Foot.HumanoidOpenAccessPanelButton)
        {
        }
    }

    public class QuickCommsPanel : StandardBindingAction
    {
        public QuickCommsPanel()
            : base("Panel - Quick Comms", "(ship, srv, foot)",
                  context =>
                    context.Ship ? EDBindings.Ship.QuickCommsPanel :
                    context.Srv ? EDBindings.Srv.QuickCommsPanel_Buggy :
                    context.Foot ? EDBindings.Foot.QuickCommsPanel_Humanoid :
                    null)
        {
        }
    }

    public class FocusRightPanel : StandardBindingAction
    {
        public FocusRightPanel()
            : base("Panel - Internal", "(ship, srv)",
                context =>
                    context.Ship ? EDBindings.Ship.FocusRightPanel :
                    context.Srv ? EDBindings.Srv.FocusRightPanel_Buggy :
                    null)
        {
        }
    }

    public class HumanoidToggleMissionHelpPanelButton : StandardBindingAction
    {
        public HumanoidToggleMissionHelpPanelButton()
            : base("Panel - Mission Help", "(foot)",
                () => EDBindings.Foot.HumanoidToggleMissionHelpPanelButton)
        {
        }
    }

    public class FocusRadarPanel : StandardBindingAction
    {
        public FocusRadarPanel()
            : base("Panel - Role", "(ship, srv)",
                context =>
                    context.Ship ? EDBindings.Ship.FocusRadarPanel :
                    context.Srv ? EDBindings.Srv.FocusRadarPanel_Buggy :
                    null)
        {
        }
    }

    public class SystemMapOpen : StandardBindingAction
    {
        public SystemMapOpen()
            : base("Panel - System Map", "(ship, srv, foot)",
                context =>
                    context.Ship ? EDBindings.Ship.SystemMapOpen :
                    context.Srv ? EDBindings.Srv.SystemMapOpen_Buggy :
                    context.Foot ? EDBindings.Foot.SystemMapOpen_Humanoid :
                    null)
        {
        }
    }

    public class IncreaseEnginesPower : StandardBindingAction
    {
        public IncreaseEnginesPower()
            : base("Pips - Increase Engine", "(ship, srv)",
                context =>
                    context.Ship ? EDBindings.Ship.IncreaseEnginesPower :
                    context.Srv ? EDBindings.Srv.IncreaseEnginesPower_Buggy :
                    null)
        {
        }
    }

    public class IncreaseSystemsPower : StandardBindingAction
    {
        public IncreaseSystemsPower()
            : base("Pips - Increase Systems", "(ship, srv)",
                context =>
                    context.Ship ? EDBindings.Ship.IncreaseSystemsPower :
                    context.Srv ? EDBindings.Srv.IncreaseSystemsPower_Buggy :
                    null)
        {
        }
    }

    public class IncreaseWeaponsPower : StandardBindingAction
    {
        public IncreaseWeaponsPower()
            : base("Pips - Increase Weapons", "(ship, srv)",
                context =>
                    context.Ship ? EDBindings.Ship.IncreaseWeaponsPower :
                    context.Srv ? EDBindings.Srv.IncreaseWeaponsPower_Buggy :
                    null)
        {
        }
    }

    public class ResetPowerDistribution : StandardBindingAction
    {
        public ResetPowerDistribution()
            : base("Pips - Reset", "(ship, srv)",
                  context =>
                    context.Ship ? EDBindings.Ship.ResetPowerDistribution :
                    context.Srv ? EDBindings.Srv.ResetPowerDistribution_Buggy :
                    null)
        {
        }
    }
    public class MultiCrewCockpitUICycleBackward : StandardBindingAction
    {
        public MultiCrewCockpitUICycleBackward()
            : base("MultiCrew Cockpit - Backward", "(ship)",
                () => EDBindings.Ship.MultiCrewCockpitUICycleBackward)
        {
        }
    }

    public class MultiCrewCockpitUICycleForward : StandardBindingAction
    {
        public MultiCrewCockpitUICycleForward()
            : base("MultiCrew Cockpit - Forward", "(ship)",
                () => EDBindings.Ship.MultiCrewCockpitUICycleForward)
        {
        }
    }

    public class MultiCrewToggleMode : StandardBindingAction
    {
        public MultiCrewToggleMode()
            : base("MultiCrew Mode - Toggle", "(ship)",
                () => EDBindings.Ship.MultiCrewToggleMode)
        {
        }
    }

    public class NightVisionToggle : StandardBindingAction
    {
        public NightVisionToggle()
            : base("Night Vision - Toggle", "(ship, srv, foot)",
                  context =>
                    context.Ship ? EDBindings.Ship.NightVisionToggle :
                    context.Srv ? EDBindings.Ship.NightVisionToggle : // TODO: is not configurable vis SRV controls, does it use ship?
                    context.Foot ? EDBindings.Foot.HumanoidToggleNightVisionButton :
                    null)
        {
        }
    }

    public class RadarDecreaseRange : StandardBindingAction
    {
        public RadarDecreaseRange()
            : base("Sensor Zoom - Decrease", "(ship)",
                () => EDBindings.Ship.RadarDecreaseRange)
        {
        }
    }

    public class RadarIncreaseRange : StandardBindingAction
    {
        public RadarIncreaseRange()
            : base("Sensor Zoom - Increase", "(ship)",
                () => EDBindings.Ship.RadarIncreaseRange)
        {
        }
    }

    public class UseShieldCell : StandardBindingAction
    {
        public UseShieldCell()
            : base("Shield Cell - Use", "(ship)",
                () => EDBindings.Ship.UseShieldCell)
        {
        }
    }

    public class HumanoidToggleShieldsButton : StandardBindingAction
    {
        public HumanoidToggleShieldsButton()
            : base("Shields - Toggle", "(foot)",
                () => EDBindings.Foot.HumanoidToggleShieldsButton)
        {
        }
    }

    public class RecallDismissShip : StandardBindingAction
    {
        public RecallDismissShip()
            : base("Ship - Recall/Dismiss", "(srv)",
                () => EDBindings.Srv.RecallDismissShip)
        {
        }
    }

    public class HumanoidSprintButton : StandardBindingAction
    {
        public HumanoidSprintButton()
            : base("Sprint - Toggle", "(foot) NOTE: requires TOGGLE button mode",
                () => EDBindings.Foot.HumanoidSprintButton)
        {
        }
    }

    public class CycleNextTarget : StandardBindingAction
    {
        public CycleNextTarget()
            : base("Target Contact - Next", "(ship)",
                () => EDBindings.Ship.CycleNextTarget)
        {
        }
    }

    public class SelectTarget : StandardBindingAction
    {
        public SelectTarget()
            : base("Target Ahead", "(ship, srv)", 
                context =>
                    context.Ship ? EDBindings.Ship.SelectTarget :
                    context.Srv ? EDBindings.Srv.SelectTarget_Buggy :
                    null)
        {
        }
    }

    public class CyclePreviousTarget : StandardBindingAction
    {
        public CyclePreviousTarget()
            : base("Target Contact - Previous", "(ship)",
                () => EDBindings.Ship.CyclePreviousTarget)
        {
        }
    }

    public class CycleNextHostileTarget : StandardBindingAction
    {
        public CycleNextHostileTarget()
            : base("Target Hostile - Next", "(ship)",
                () => EDBindings.Ship.CycleNextHostileTarget)
        {
        }
    }

    public class CyclePreviousHostileTarget : StandardBindingAction
    {
        public CyclePreviousHostileTarget()
            : base("Target Hostile - Previous", "(ship)",
                () => EDBindings.Ship.CyclePreviousHostileTarget)
        {
        }
    }

    public class SelectHighestThreat : StandardBindingAction
    {
        public SelectHighestThreat()
            : base("Target Hostile - Highest Threat", "(ship)",
                () => EDBindings.Ship.SelectHighestThreat)
        {
        }
    }

    public class TargetNextRouteSystem : StandardBindingAction
    {
        public TargetNextRouteSystem()
            : base("Target Route", "(ship)",
                () => EDBindings.Ship.TargetNextRouteSystem)
        {
        }
    }

    public class SelectTargetsTarget : StandardBindingAction
    {
        public SelectTargetsTarget()
            : base("Target Target", "(ship)",
                () => EDBindings.Ship.SelectTargetsTarget)
        {
        }
    }

    public class TargetWingman0 : StandardBindingAction
    {
        public TargetWingman0()
            : base("Target Teammate - 1", "(ship)",
                () => EDBindings.Ship.TargetWingman0)
        {
        }
    }

    public class TargetWingman1 : StandardBindingAction
    {
        public TargetWingman1()
            : base("Target Teammate - 2", "(ship)",
                () => EDBindings.Ship.TargetWingman1)
        {
        }
    }

    public class TargetWingman2 : StandardBindingAction
    {
        public TargetWingman2()
            : base("Target Teammate - 3", "(ship)",
                () => EDBindings.Ship.TargetWingman2)
        {
        }
    }

    public class CycleNextSubsystem : StandardBindingAction
    {
        public CycleNextSubsystem()
            : base("Target Subsystem - Next", "(ship)",
                () => EDBindings.Ship.CycleNextSubsystem)
        {
        }
    }

    public class CyclePreviousSubsystem : StandardBindingAction
    {
        public CyclePreviousSubsystem()
            : base("Target Subsystem - Previous", "(ship)",
                () => EDBindings.Ship.CyclePreviousSubsystem)
        {
        }
    }

    public class WingNavLock : StandardBindingAction
    {
        public WingNavLock()
            : base("Teammate - Nav-Lock", "(ship)",
                () => EDBindings.Ship.WingNavLock)
        {
        }
    }

    public class SetSpeed25 : StandardBindingAction
    {
        public SetSpeed25()
            : base("Thrust - Forward 25%", "(ship)",
                () => EDBindings.Ship.SetSpeed25)
        {
        }
    }

    public class SetSpeed50 : StandardBindingAction
    {
        public SetSpeed50()
            : base("Thrust - Forward 50%", "(ship)",
                () => EDBindings.Ship.SetSpeed50)
        {
        }
    }

    public class SetSpeed75 : StandardBindingAction
    {
        public SetSpeed75()
            : base("Thrust - Forward 75%", "(ship)",
                () => EDBindings.Ship.SetSpeed75)
        {
        }
    }


    public class SetSpeed100 : StandardBindingAction
    {
        public SetSpeed100()
            : base("Thrust - Forward 100%", "(ship)",
                () => EDBindings.Ship.SetSpeed100)
        {
        }
    }

    public class SetSpeedZero : StandardBindingAction
    {
        public SetSpeedZero()
            : base("Thrust - None (0%)", "(ship)",
                () => EDBindings.Ship.SetSpeedZero)
        {
        }
    }

    public class SetSpeedMinus25 : StandardBindingAction
    {
        public SetSpeedMinus25()
            : base("Thrust - Reverse 25%", "(ship)",
                () => EDBindings.Ship.SetSpeedMinus25)
        {
        }
    }

    public class SetSpeedMinus50 : StandardBindingAction
    {
        public SetSpeedMinus50()
            : base("Thrust - Reverse 50%", "(ship)",
                () => EDBindings.Ship.SetSpeedMinus50)
        {
        }
    }

    public class SetSpeedMinus75 : StandardBindingAction
    {
        public SetSpeedMinus75()
            : base("Thrust - Reverse 75%", "(ship)",
                () => EDBindings.Ship.SetSpeedMinus75)
        {
        }
    }

    public class SetSpeedMinus100 : StandardBindingAction
    {
        public SetSpeedMinus100()
            : base("Thrust - Reverse 100%", "(ship)",
                () => EDBindings.Ship.SetSpeedMinus100)
        {
        }
    }

    public class HumanoidSwitchToCompAnalyser : StandardBindingAction
    {
        public HumanoidSwitchToCompAnalyser()
            : base("Tool - Select Profile Analyser", "(foot)",
                () => EDBindings.Foot.HumanoidSwitchToCompAnalyser)
        {
        }
    }

    public class HumanoidSwitchToRechargeTool : StandardBindingAction
    {
        public HumanoidSwitchToRechargeTool()
            : base("Tool - Select Energy Link", "(foot)",
                () => EDBindings.Foot.HumanoidSwitchToRechargeTool)
        {
        }
    }

    public class HumanoidSwitchToSuitTool : StandardBindingAction
    {
        public HumanoidSwitchToSuitTool()
            : base("Tool - Select Suit Specific", "(foot)",
                () => EDBindings.Foot.HumanoidSwitchToSuitTool)
        {
        }
    }

    public class HumanoidToggleToolModeButton : StandardBindingAction
    {
        public HumanoidToggleToolModeButton()
            : base("Tool - Toggle Mode", "(foot)",
                () => EDBindings.Foot.HumanoidToggleToolModeButton)
        {
        }
    }


    public class ToggleBuggyTurretButton : StandardBindingAction
    {
        public ToggleBuggyTurretButton()
            : base("Turret - Toggle", "(srv)",
                () => EDBindings.Srv.ToggleBuggyTurretButton)
        {
        }
    }

    public class UI_Back : StandardBindingAction
    {
        public UI_Back()
            : base("UI - Item Back", "",
                () => EDBindings.General.UI_Back)
        {
        }
    }

    public class UI_Down : StandardBindingAction
    {
        public UI_Down()
            : base("UI - Item Down", "",
                () => EDBindings.General.UI_Down)
        {
        }
    }

    public class UI_Left : StandardBindingAction
    {
        public UI_Left()
            : base("UI - Item Left", "",
                () => EDBindings.General.UI_Left)
        {
        }
    }

    public class UI_Right : StandardBindingAction
    {
        public UI_Right()
            : base("UI - Item Right", "",
                () => EDBindings.General.UI_Right)
        {
        }
    }

    public class UI_Up : StandardBindingAction
    {
        public UI_Up()
            : base("UI - Item Up", "",
                () => EDBindings.General.UI_Up)
        {
        }
    }

    public class CycleNextPage : StandardBindingAction
    {
        public CycleNextPage()
            : base("UI - Page Next", "",
                () => EDBindings.General.CycleNextPage)
        {
        }
    }

    public class CyclePreviousPage : StandardBindingAction
    {
        public CyclePreviousPage()
            : base("UI - Page Previous", "",
                () => EDBindings.General.CyclePreviousPage)
        {
        }
    }

    public class CycleNextPanel : StandardBindingAction
    {
        public CycleNextPanel()
            : base("UI - Panel Next", "",
                () => EDBindings.General.CycleNextPanel)
        {
        }
    }

    public class CyclePreviousPanel : StandardBindingAction
    {
        public CyclePreviousPanel()
            : base("UI - Panel Previous", "",
                () => EDBindings.General.CyclePreviousPanel)
        {
        }
    }

    public class UI_Select : StandardBindingAction
    {
        public UI_Select()
            : base("UI - Select", "",
                () => EDBindings.General.UI_Select)
        {
        }
    }

    public class UI_Toggle : StandardBindingAction
    {
        public UI_Toggle()
            : base("UI - Toggle", "",
                () => EDBindings.General.UI_Toggle)
        {
        }
    }

    public class UIFocus : StandardBindingAction
    {
        public UIFocus()
            : base("UI Focus - Switch", "(ship, srv) Note: UI FOCUS MODE must be set to CYCLE.",
                context =>
                    context.Ship ? EDBindings.Ship.UIFocus :
                    context.Srv ? EDBindings.Srv.UIFocus_Buggy :
                    null)
        {
        }
    }

    public class HumanoidUtilityWheelCycleMode : StandardBindingAction
    {
        public HumanoidUtilityWheelCycleMode()
            : base("Utility Wheel Mode - Cycle", "(foot)",
                () => EDBindings.Foot.HumanoidUtilityWheelCycleMode)
        {
        }
    }

    public class HumanoidHideWeaponButton : StandardBindingAction
    {
        public HumanoidHideWeaponButton()
            : base("Weapon - Holster", "(foot)",
                () => EDBindings.Foot.HumanoidHideWeaponButton)
        {
        }
    }

    public class HumanoidSelectNextWeaponButton : StandardBindingAction
    {
        public HumanoidSelectNextWeaponButton()
            : base("Weapon - Next", "(foot)",
                () => EDBindings.Foot.HumanoidSelectNextWeaponButton)
        {
        }
    }

    public class HumanoidSelectPreviousWeaponButton : StandardBindingAction
    {
        public HumanoidSelectPreviousWeaponButton()
            : base("Weapon - Previous", "(foot)",
                () => EDBindings.Foot.HumanoidSelectPreviousWeaponButton)
        {
        }
    }

    public class HumanoidReloadButton : StandardBindingAction
    {
        public HumanoidReloadButton()
            : base("Weapon - Reload", "(foot)",
                () => EDBindings.Foot.HumanoidReloadButton)
        {
        }
    }

    public class HumanoidSelectPrimaryWeaponButton : StandardBindingAction
    {
        public HumanoidSelectPrimaryWeaponButton()
            : base("Weapon - Select Primary", "(foot)",
                () => EDBindings.Foot.HumanoidSelectPrimaryWeaponButton)
        {
        }
    }

    public class HumanoidSelectSecondaryWeaponButton : StandardBindingAction
    {
        public HumanoidSelectSecondaryWeaponButton()
            : base("Weapon - Select Secondary", "(foot)",
                () => EDBindings.Foot.HumanoidSelectSecondaryWeaponButton)
        {
        }
    }

    public class HumanoidSelectUtilityWeaponButton : StandardBindingAction
    {
        public HumanoidSelectUtilityWeaponButton()
            : base("Weapon - Select Tool", "(foot)",
                () => EDBindings.Foot.HumanoidSelectUtilityWeaponButton)
        {
        }
    }

    public class HumanoidSwitchWeapon : StandardBindingAction
    {
        public HumanoidSwitchWeapon()
            : base("Weapon - Switch", "(foot)",
                () => EDBindings.Foot.HumanoidSwitchWeapon)
        {
        }
    }

    public class WeaponColourToggle : StandardBindingAction
    {
        public WeaponColourToggle()
            : base("Weapon - Toggle Colour", "(ship)",
                () => EDBindings.Ship.WeaponColourToggle)
        {
        }
    }

    internal class BindingActionManager
    {

        private static readonly HashSet<Type> _baseBindingActionTypes = new HashSet<Type>
        {
            typeof(BindingAction),
            typeof(StandardBindingAction),
        };

        private readonly EliteDangerousMacroDeckPlugin _plugin;

        public BindingActionManager(EliteDangerousMacroDeckPlugin plugin)
        {

            _plugin = plugin;

            _plugin.Actions.AddRange(
                Assembly
                    .GetExecutingAssembly()
                    .GetTypes()
                    .Where(t => typeof(BindingAction).IsAssignableFrom(t) && !_baseBindingActionTypes.Contains(t))
                    .Select(t => (BindingAction)Activator.CreateInstance(t))
                    .OrderBy(i => i.Name, new StrCmpLogicalComparer())
            );

        }

    }

    // Sorts numbers by numeric value instead of lexically, e.g. so 75% comes before 100%.
    public class StrCmpLogicalComparer : Comparer<string>
    {
        [DllImport("Shlwapi.dll", CharSet = CharSet.Unicode)]
        private static extern int StrCmpLogicalW(string x, string y);

        public override int Compare(string x, string y)
        {
            return StrCmpLogicalW(x, y);
        }
    }

}


/*
 *  Below are actions for bindings that don't make sense for use via Macro Deck unless your
 *  trying to use it to fly/drive/walk. Most of them we can't support unless Macro Deck 
 *  supports separate events for button press and button release. We could allow "how long"
 *  config for each action and simulate holding the button down for just that long. At least
 *  you could use them to implement macros in some cases. Maybe in a future update....
 
    public class BackwardKey : StandardBindingAction
    {
        public BackwardKey()
            : base("Throttle - Decrease", "(ship) Decrease throttle.",
                () => EDBindings.Ship.BackwardKey)
        {
        }
    }

    public class BackwardThrustButton : StandardBindingAction
    {
        public BackwardThrustButton()
            : base("BackwardThrustButton", "BackwardThrustButton",
                () => EDBindings.Ship.BackwardThrustButton)
        {
        }
    }

    public class BackwardThrustButton_Landing : StandardBindingAction
    {
        public BackwardThrustButton_Landing()
            : base("BackwardThrustButton_Landing", "BackwardThrustButton_Landing",
                () => EDBindings.Ship.BackwardThrustButton_Landing)
        {
        }
    }

    public class BuggyPitchDownButton : StandardBindingAction
    {
        public BuggyPitchDownButton()
            : base("BuggyPitchDownButton", "BuggyPitchDownButton",
                () => EDBindings.Ship.BuggyPitchDownButton)
        {
        }
    }

    public class BuggyPitchUpButton : StandardBindingAction
    {
        public BuggyPitchUpButton()
            : base("BuggyPitchUpButton", "BuggyPitchUpButton",
                () => EDBindings.Ship.BuggyPitchUpButton)
        {
        }
    }

    public class BuggyPrimaryFireButton : StandardBindingAction
    {
        public BuggyPrimaryFireButton()
            : base("BuggyPrimaryFireButton", "BuggyPrimaryFireButton",
                () => EDBindings.Ship.BuggyPrimaryFireButton)
        {
        }
    }

    public class BuggyRollLeft : StandardBindingAction
    {
        public BuggyRollLeft()
            : base("BuggyRollLeft", "BuggyRollLeft",
                () => EDBindings.Ship.BuggyRollLeft)
        {
        }
    }

    public class BuggyRollLeftButton : StandardBindingAction
    {
        public BuggyRollLeftButton()
            : base("BuggyRollLeftButton", "BuggyRollLeftButton",
                () => EDBindings.Ship.BuggyRollLeftButton)
        {
        }
    }

    public class BuggyRollRight : StandardBindingAction
    {
        public BuggyRollRight()
            : base("BuggyRollRight", "BuggyRollRight",
                () => EDBindings.Ship.BuggyRollRight)
        {
        }
    }

    public class BuggyRollRightButton : StandardBindingAction
    {
        public BuggyRollRightButton()
            : base("BuggyRollRightButton", "BuggyRollRightButton",
                () => EDBindings.Ship.BuggyRollRightButton)
        {
        }
    }

    public class BuggySecondaryFireButton : StandardBindingAction
    {
        public BuggySecondaryFireButton()
            : base("BuggySecondaryFireButton", "BuggySecondaryFireButton",
                () => EDBindings.Ship.BuggySecondaryFireButton)
        {
        }
    }

    public class BuggyTurretPitchDownButton : StandardBindingAction
    {
        public BuggyTurretPitchDownButton()
            : base("BuggyTurretPitchDownButton", "BuggyTurretPitchDownButton",
                () => EDBindings.Ship.BuggyTurretPitchDownButton)
        {
        }
    }

    public class BuggyTurretPitchUpButton : StandardBindingAction
    {
        public BuggyTurretPitchUpButton()
            : base("BuggyTurretPitchUpButton", "BuggyTurretPitchUpButton",
                () => EDBindings.Ship.BuggyTurretPitchUpButton)
        {
        }
    }

    public class BuggyTurretYawLeftButton : StandardBindingAction
    {
        public BuggyTurretYawLeftButton()
            : base("BuggyTurretYawLeftButton", "BuggyTurretYawLeftButton",
                () => EDBindings.Ship.BuggyTurretYawLeftButton)
        {
        }
    }

    public class BuggyTurretYawRightButton : StandardBindingAction
    {
        public BuggyTurretYawRightButton()
            : base("BuggyTurretYawRightButton", "BuggyTurretYawRightButton",
                () => EDBindings.Ship.BuggyTurretYawRightButton)
        {
        }
    }

    public class CamPitchDown : StandardBindingAction
    {
        public CamPitchDown()
            : base("CamPitchDown", "CamPitchDown",
                () => EDBindings.Ship.CamPitchDown)
        {
        }
    }

    public class CamPitchUp : StandardBindingAction
    {
        public CamPitchUp()
            : base("CamPitchUp", "CamPitchUp",
                () => EDBindings.Ship.CamPitchUp)
        {
        }
    }

    public class CamTranslateBackward : StandardBindingAction
    {
        public CamTranslateBackward()
            : base("CamTranslateBackward", "CamTranslateBackward",
                () => EDBindings.Ship.CamTranslateBackward)
        {
        }
    }

    public class CamTranslateDown : StandardBindingAction
    {
        public CamTranslateDown()
            : base("CamTranslateDown", "CamTranslateDown",
                () => EDBindings.Ship.CamTranslateDown)
        {
        }
    }

    public class CamTranslateForward : StandardBindingAction
    {
        public CamTranslateForward()
            : base("CamTranslateForward", "CamTranslateForward",
                () => EDBindings.Ship.CamTranslateForward)
        {
        }
    }

    public class CamTranslateLeft : StandardBindingAction
    {
        public CamTranslateLeft()
            : base("CamTranslateLeft", "CamTranslateLeft",
                () => EDBindings.Ship.CamTranslateLeft)
        {
        }
    }

    public class CamTranslateRight : StandardBindingAction
    {
        public CamTranslateRight()
            : base("CamTranslateRight", "CamTranslateRight",
                () => EDBindings.Ship.CamTranslateRight)
        {
        }
    }

    public class CamTranslateUp : StandardBindingAction
    {
        public CamTranslateUp()
            : base("CamTranslateUp", "CamTranslateUp",
                () => EDBindings.Ship.CamTranslateUp)
        {
        }
    }

    public class CamYawLeft : StandardBindingAction
    {
        public CamYawLeft()
            : base("CamYawLeft", "CamYawLeft",
                () => EDBindings.Ship.CamYawLeft)
        {
        }
    }

    public class CamYawRight : StandardBindingAction
    {
        public CamYawRight()
            : base("CamYawRight", "CamYawRight",
                () => EDBindings.Ship.CamYawRight)
        {
        }
    }

    public class CamZoomIn : StandardBindingAction
    {
        public CamZoomIn()
            : base("CamZoomIn", "CamZoomIn",
                () => EDBindings.Ship.CamZoomIn)
        {
        }
    }

    public class CamZoomOut : StandardBindingAction
    {
        public CamZoomOut()
            : base("CamZoomOut", "CamZoomOut",
                () => EDBindings.Ship.CamZoomOut)
        {
        }
    }

    public class ChargeECM : StandardBindingAction
    {
        public ChargeECM()
            : base("ChargeECM", "ChargeECM",
                () => EDBindings.Ship.ChargeECM)
        {
        }
    }

    public class CommanderCreator_Redo : StandardBindingAction
    {
        public CommanderCreator_Redo()
            : base("CommanderCreator_Redo", "CommanderCreator_Redo",
                () => EDBindings.Ship.CommanderCreator_Redo)
        {
        }
    }

    public class CommanderCreator_Rotation : StandardBindingAction
    {
        public CommanderCreator_Rotation()
            : base("CommanderCreator_Rotation", "CommanderCreator_Rotation",
                () => EDBindings.Ship.CommanderCreator_Rotation)
        {
        }
    }

    public class CommanderCreator_Rotation_MouseToggle : StandardBindingAction
    {
        public CommanderCreator_Rotation_MouseToggle()
            : base("CommanderCreator_Rotation_MouseToggle", "CommanderCreator_Rotation_MouseToggle",
                () => EDBindings.Ship.CommanderCreator_Rotation_MouseToggle)
        {
        }
    }

    public class CommanderCreator_Undo : StandardBindingAction
    {
        public CommanderCreator_Undo()
            : base("CommanderCreator_Undo", "CommanderCreator_Undo",
                () => EDBindings.Ship.CommanderCreator_Undo)
        {
        }
    }

    public class DecreaseSpeedButtonMax : StandardBindingAction
    {
        public DecreaseSpeedButtonMax()
            : base("DecreaseSpeedButtonMax", "DecreaseSpeedButtonMax",
                () => EDBindings.Ship.DecreaseSpeedButtonMax)
        {
        }
    }

    public class DecreaseSpeedButtonPartial : StandardBindingAction
    {
        public DecreaseSpeedButtonPartial()
            : base("DecreaseSpeedButtonPartial", "DecreaseSpeedButtonPartial",
                () => EDBindings.Ship.DecreaseSpeedButtonPartial)
        {
        }
    }

    public class DownThrustButton : StandardBindingAction
    {
        public DownThrustButton()
            : base("DownThrustButton", "DownThrustButton",
                () => EDBindings.Ship.DownThrustButton)
        {
        }
    }

    public class DownThrustButton_Landing : StandardBindingAction
    {
        public DownThrustButton_Landing()
            : base("DownThrustButton_Landing", "DownThrustButton_Landing",
                () => EDBindings.Ship.DownThrustButton_Landing)
        {
        }
    }

    public class ExplorationFSSCameraPitchDecreaseButton : StandardBindingAction
    {
        public ExplorationFSSCameraPitchDecreaseButton()
            : base("ExplorationFSSCameraPitchDecreaseButton", "ExplorationFSSCameraPitchDecreaseButton",
                () => EDBindings.Ship.ExplorationFSSCameraPitchDecreaseButton)
        {
        }
    }

    public class ExplorationFSSCameraPitchIncreaseButton : StandardBindingAction
    {
        public ExplorationFSSCameraPitchIncreaseButton()
            : base("ExplorationFSSCameraPitchIncreaseButton", "ExplorationFSSCameraPitchIncreaseButton",
                () => EDBindings.Ship.ExplorationFSSCameraPitchIncreaseButton)
        {
        }
    }

    public class ExplorationFSSCameraYawDecreaseButton : StandardBindingAction
    {
        public ExplorationFSSCameraYawDecreaseButton()
            : base("ExplorationFSSCameraYawDecreaseButton", "ExplorationFSSCameraYawDecreaseButton",
                () => EDBindings.Ship.ExplorationFSSCameraYawDecreaseButton)
        {
        }
    }

    public class ExplorationFSSCameraYawIncreaseButton : StandardBindingAction
    {
        public ExplorationFSSCameraYawIncreaseButton()
            : base("ExplorationFSSCameraYawIncreaseButton", "ExplorationFSSCameraYawIncreaseButton",
                () => EDBindings.Ship.ExplorationFSSCameraYawIncreaseButton)
        {
        }
    }

    public class ExplorationFSSMiniZoomIn : StandardBindingAction
    {
        public ExplorationFSSMiniZoomIn()
            : base("ExplorationFSSMiniZoomIn", "ExplorationFSSMiniZoomIn",
                () => EDBindings.Ship.ExplorationFSSMiniZoomIn)
        {
        }
    }

    public class ExplorationFSSMiniZoomOut : StandardBindingAction
    {
        public ExplorationFSSMiniZoomOut()
            : base("ExplorationFSSMiniZoomOut", "ExplorationFSSMiniZoomOut",
                () => EDBindings.Ship.ExplorationFSSMiniZoomOut)
        {
        }
    }

    public class ExplorationFSSRadioTuningX_Decrease : StandardBindingAction
    {
        public ExplorationFSSRadioTuningX_Decrease()
            : base("ExplorationFSSRadioTuningX_Decrease", "ExplorationFSSRadioTuningX_Decrease",
                () => EDBindings.Ship.ExplorationFSSRadioTuningX_Decrease)
        {
        }
    }

    public class ExplorationFSSRadioTuningX_Increase : StandardBindingAction
    {
        public ExplorationFSSRadioTuningX_Increase()
            : base("ExplorationFSSRadioTuningX_Increase", "ExplorationFSSRadioTuningX_Increase",
                () => EDBindings.Ship.ExplorationFSSRadioTuningX_Increase)
        {
        }
    }

    public class ExplorationFSSZoomIn : StandardBindingAction
    {
        public ExplorationFSSZoomIn()
            : base("ExplorationFSSZoomIn", "ExplorationFSSZoomIn",
                () => EDBindings.Ship.ExplorationFSSZoomIn)
        {
        }
    }

    public class ExplorationFSSZoomOut : StandardBindingAction
    {
        public ExplorationFSSZoomOut()
            : base("ExplorationFSSZoomOut", "ExplorationFSSZoomOut",
                () => EDBindings.Ship.ExplorationFSSZoomOut)
        {
        }
    }

    public class ExplorationSAAExitThirdPerson : StandardBindingAction
    {
        public ExplorationSAAExitThirdPerson()
            : base("ExplorationSAAExitThirdPerson", "ExplorationSAAExitThirdPerson",
                () => EDBindings.Ship.ExplorationSAAExitThirdPerson)
        {
        }
    }

    // Camera increase/decrease blur? 
    public class FStopDec : StandardBindingAction
    {
        public FStopDec()
            : base("FStopDec", "FStopDec",
                () => EDBindings.Ship.FStopDec)
        {
        }
    }

    public class FStopInc : StandardBindingAction
    {
        public FStopInc()
            : base("FStopInc", "FStopInc",
                () => EDBindings.Ship.FStopInc)
        {
        }
    }

    // not present in binds file
    public class FocusDistanceDec : StandardBindingAction
    {
        public FocusDistanceDec()
            : base("FocusDistanceDec", "FocusDistanceDec",
                () => EDBindings.Ship.FocusDistanceDec)
        {
        }
    }

    public class FocusDistanceInc : StandardBindingAction
    {
        public FocusDistanceInc()
            : base("FocusDistanceInc", "FocusDistanceInc",
                () => EDBindings.Ship.FocusDistanceInc)
        {
        }
    }

    public class ForwardKey : StandardBindingAction
    {
        public ForwardKey()
            : base("Throttle - Increase", "(ship) Increase throttle.", // TODO: srv too?
                () => EDBindings.Ship.ForwardKey)
        {
        }
    }

    public class ForwardThrustButton : StandardBindingAction
    {
        public ForwardThrustButton()
            : base("ForwardThrustButton", "ForwardThrustButton",
                () => EDBindings.Ship.ForwardThrustButton)
        {
        }
    }

    public class ForwardThrustButton_Landing : StandardBindingAction
    {
        public ForwardThrustButton_Landing()
            : base("ForwardThrustButton_Landing", "ForwardThrustButton_Landing",
                () => EDBindings.Ship.ForwardThrustButton_Landing)
        {
        }
    }

    public class FreeCamSpeedDec : StandardBindingAction
    {
        public FreeCamSpeedDec()
            : base("FreeCamSpeedDec", "FreeCamSpeedDec",
                () => EDBindings.Ship.FreeCamSpeedDec)
        {
        }
    }

    public class FreeCamSpeedInc : StandardBindingAction
    {
        public FreeCamSpeedInc()
            : base("FreeCamSpeedInc", "FreeCamSpeedInc",
                () => EDBindings.Ship.FreeCamSpeedInc)
        {
        }
    }

    public class ForwardThrustButton : StandardBindingAction
    {
        public ForwardThrustButton()
            : base("ForwardThrustButton", "ForwardThrustButton",
                () => EDBindings.Ship.ForwardThrustButton)
        {
        }
    }

    public class ForwardThrustButton_Landing : StandardBindingAction
    {
        public ForwardThrustButton_Landing()
            : base("ForwardThrustButton_Landing", "ForwardThrustButton_Landing",
                () => EDBindings.Ship.ForwardThrustButton_Landing)
        {
        }
    }

    public class FreeCamSpeedDec : StandardBindingAction
    {
        public FreeCamSpeedDec()
            : base("FreeCamSpeedDec", "FreeCamSpeedDec",
                () => EDBindings.Ship.FreeCamSpeedDec)
        {
        }
    }

    public class FreeCamSpeedInc : StandardBindingAction
    {
        public FreeCamSpeedInc()
            : base("FreeCamSpeedInc", "FreeCamSpeedInc",
                () => EDBindings.Ship.FreeCamSpeedInc)
        {
        }
    }

    public class FreeCamZoomIn : StandardBindingAction
    {
        public FreeCamZoomIn()
            : base("FreeCamZoomIn", "FreeCamZoomIn",
                () => EDBindings.Ship.FreeCamZoomIn)
        {
        }
    }

    public class FreeCamZoomOut : StandardBindingAction
    {
        public FreeCamZoomOut()
            : base("FreeCamZoomOut", "FreeCamZoomOut",
                () => EDBindings.Ship.FreeCamZoomOut)
        {
        }
    }

    public class HeadLookPitchDown : StandardBindingAction
    {
        public HeadLookPitchDown()
            : base("HeadLookPitchDown", "HeadLookPitchDown",
                () => EDBindings.Ship.HeadLookPitchDown)
        {
        }
    }

    public class HeadLookPitchUp : StandardBindingAction
    {
        public HeadLookPitchUp()
            : base("HeadLookPitchUp", "HeadLookPitchUp",
                () => EDBindings.Ship.HeadLookPitchUp)
        {
        }
    }

    public class HeadLookReset : StandardBindingAction
    {
        public HeadLookReset()
            : base("HeadLookReset", "HeadLookReset",
                () => EDBindings.Ship.HeadLookReset)
        {
        }
    }

    public class HeadLookYawLeft : StandardBindingAction
    {
        public HeadLookYawLeft()
            : base("HeadLookYawLeft", "HeadLookYawLeft",
                () => EDBindings.Ship.HeadLookYawLeft)
        {
        }
    }

    public class HeadLookYawRight : StandardBindingAction
    {
        public HeadLookYawRight()
            : base("HeadLookYawRight", "HeadLookYawRight",
                () => EDBindings.Ship.HeadLookYawRight)
        {
        }
    }

    public class HumanoidBackwardButton : StandardBindingAction
    {
        public HumanoidBackwardButton()
            : base("Move - Backward", "(foot) Move backward.",
                () => EDBindings.Foot.HumanoidBackwardButton)
        {
        }
    }

    public class HumanoidEmoteWheelButton : StandardBindingAction
    {
        public HumanoidEmoteWheelButton()
            : base("Emote Wheel", "(foot) Open emote wheel. REQUIRES TOGGLE BUTTON MODE",
                () => EDBindings.Foot.HumanoidEmoteWheelButton)
        {
        }
    }

    public class HumanoidForwardButton : StandardBindingAction
    {
        public HumanoidForwardButton()
            : base("Move - Forward", "(foot) Move forward.",
                () => EDBindings.Ship.HumanoidForwardButton)
        {
        }
    }

    public class HumanoidItemWheelButton : StandardBindingAction
    {
        public HumanoidItemWheelButton()
            : base("Item Wheel", "(foot) Open item wheel. REQUIRES TOGGLE BUTTON MODE",
                () => EDBindings.Foot.HumanoidItemWheelButton)
        {
        }
    }

    public class HumanoidJumpButton : StandardBindingAction
    {
        public HumanoidJumpButton()
            : base("Move - Jump", "(foot) How high?",
                () => EDBindings.Ship.HumanoidJumpButton)
        {
        }
    }

    public class HumanoidMeleeButton : StandardBindingAction
    {
        public HumanoidMeleeButton()
            : base("Melee", "(foot) Perform melee attack.",
                () => EDBindings.Ship.HumanoidMeleeButton)
        {
        }
    }

    // TODO: What is this? Is it configurable in Elite Dangerous UI?
    public class HumanoidPing : StandardBindingAction
    {
        public HumanoidPing()
            : base("HumanoidPing", "HumanoidPing",
                () => EDBindings.Ship.HumanoidPing)
        {
        }
    }

    public class HumanoidPitchDownButton : StandardBindingAction
    {
        public HumanoidPitchDownButton()
            : base("HumanoidPitchDownButton", "HumanoidPitchDownButton",
                () => EDBindings.Ship.HumanoidPitchDownButton)
        {
        }
    }

    public class HumanoidPitchUpButton : StandardBindingAction
    {
        public HumanoidPitchUpButton()
            : base("HumanoidPitchUpButton", "HumanoidPitchUpButton",
                () => EDBindings.Ship.HumanoidPitchUpButton)
        {
        }
    }

    public class HumanoidPrimaryFireButton : StandardBindingAction
    {
        public HumanoidPrimaryFireButton()
            : base("HumanoidPrimaryFireButton", "HumanoidPrimaryFireButton",
                () => EDBindings.Ship.HumanoidPrimaryFireButton)
        {
        }
    }

    public class HumanoidPrimaryInteractButton : StandardBindingAction
    {
        public HumanoidPrimaryInteractButton()
            : base("HumanoidPrimaryInteractButton", "HumanoidPrimaryInteractButton",
                () => EDBindings.Ship.HumanoidPrimaryInteractButton)
        {
        }
    }

    public class HumanoidRotateLeftButton : StandardBindingAction
    {
        public HumanoidRotateLeftButton()
            : base("HumanoidRotateLeftButton", "HumanoidRotateLeftButton",
                () => EDBindings.Ship.HumanoidRotateLeftButton)
        {
        }
    }

    public class HumanoidRotateRightButton : StandardBindingAction
    {
        public HumanoidRotateRightButton()
            : base("HumanoidRotateRightButton", "HumanoidRotateRightButton",
                () => EDBindings.Ship.HumanoidRotateRightButton)
        {
        }
    }

    public class HumanoidSecondaryInteractButton : StandardBindingAction
    {
        public HumanoidSecondaryInteractButton()
            : base("HumanoidSecondaryInteractButton", "HumanoidSecondaryInteractButton",
                () => EDBindings.Ship.HumanoidSecondaryInteractButton)
        {
        }
    }

    public class HumanoidStrafeLeftButton : StandardBindingAction
    {
        public HumanoidStrafeLeftButton()
            : base("HumanoidStrafeLeftButton", "HumanoidStrafeLeftButton",
                () => EDBindings.Ship.HumanoidStrafeLeftButton)
        {
        }
    }

    public class HumanoidStrafeRightButton : StandardBindingAction
    {
        public HumanoidStrafeRightButton()
            : base("HumanoidStrafeRightButton", "HumanoidStrafeRightButton",
                () => EDBindings.Ship.HumanoidStrafeRightButton)
        {
        }
    }

    public class HumanoidThrowGrenadeButton : StandardBindingAction
    {
        public HumanoidThrowGrenadeButton()
            : base("Grenade - Throw", "(foot) Throw grenade.",
                () => EDBindings.Ship.HumanoidThrowGrenadeButton)
        {
        }
    }

    public class HumanoidZoomButton : StandardBindingAction
    {
        public HumanoidZoomButton()
            : base("HumanoidZoomButton", "HumanoidZoomButton",
                () => EDBindings.Ship.HumanoidZoomButton)
        {
        }
    }

    public class IncreaseSpeedButtonMax : StandardBindingAction
    {
        public IncreaseSpeedButtonMax()
            : base("IncreaseSpeedButtonMax", "IncreaseSpeedButtonMax",
                () => EDBindings.Ship.IncreaseSpeedButtonMax)
        {
        }
    }

    public class IncreaseSpeedButtonPartial : StandardBindingAction
    {
        public IncreaseSpeedButtonPartial()
            : base("IncreaseSpeedButtonPartial", "IncreaseSpeedButtonPartial",
                () => EDBindings.Ship.IncreaseSpeedButtonPartial)
        {
        }
    }

    public class LeftThrustButton : StandardBindingAction
    {
        public LeftThrustButton()
            : base("LeftThrustButton", "LeftThrustButton",
                () => EDBindings.Ship.LeftThrustButton)
        {
        }
    }

    public class LeftThrustButton_Landing : StandardBindingAction
    {
        public LeftThrustButton_Landing()
            : base("LeftThrustButton_Landing", "LeftThrustButton_Landing",
                () => EDBindings.Ship.LeftThrustButton_Landing)
        {
        }
    }

    public class MoveFreeCamBackwards : StandardBindingAction
    {
        public MoveFreeCamBackwards()
            : base("MoveFreeCamBackwards", "MoveFreeCamBackwards",
                () => EDBindings.Ship.MoveFreeCamBackwards)
        {
        }
    }

    public class MoveFreeCamDown : StandardBindingAction
    {
        public MoveFreeCamDown()
            : base("MoveFreeCamDown", "MoveFreeCamDown",
                () => EDBindings.Ship.MoveFreeCamDown)
        {
        }
    }

    public class MoveFreeCamForward : StandardBindingAction
    {
        public MoveFreeCamForward()
            : base("MoveFreeCamForward", "MoveFreeCamForward",
                () => EDBindings.Ship.MoveFreeCamForward)
        {
        }
    }

    public class MoveFreeCamLeft : StandardBindingAction
    {
        public MoveFreeCamLeft()
            : base("MoveFreeCamLeft", "MoveFreeCamLeft",
                () => EDBindings.Ship.MoveFreeCamLeft)
        {
        }
    }

    public class MoveFreeCamRight : StandardBindingAction
    {
        public MoveFreeCamRight()
            : base("MoveFreeCamRight", "MoveFreeCamRight",
                () => EDBindings.Ship.MoveFreeCamRight)
        {
        }
    }

    public class MoveFreeCamUp : StandardBindingAction
    {
        public MoveFreeCamUp()
            : base("MoveFreeCamUp", "MoveFreeCamUp",
                () => EDBindings.Ship.MoveFreeCamUp)
        {
        }
    }

    public class MultiCrewPrimaryFire : StandardBindingAction
    {
        public MultiCrewPrimaryFire()
            : base("MultiCrewPrimaryFire", "MultiCrewPrimaryFire",
                () => EDBindings.Ship.MultiCrewPrimaryFire)
        {
        }
    }

    public class MultiCrewPrimaryUtilityFire : StandardBindingAction
    {
        public MultiCrewPrimaryUtilityFire()
            : base("MultiCrewPrimaryUtilityFire", "MultiCrewPrimaryUtilityFire",
                () => EDBindings.Ship.MultiCrewPrimaryUtilityFire)
        {
        }
    }

    public class MultiCrewSecondaryFire : StandardBindingAction
    {
        public MultiCrewSecondaryFire()
            : base("MultiCrewSecondaryFire", "MultiCrewSecondaryFire",
                () => EDBindings.Ship.MultiCrewSecondaryFire)
        {
        }
    }

    public class MultiCrewSecondaryUtilityFire : StandardBindingAction
    {
        public MultiCrewSecondaryUtilityFire()
            : base("MultiCrewSecondaryUtilityFire", "MultiCrewSecondaryUtilityFire",
                () => EDBindings.Ship.MultiCrewSecondaryUtilityFire)
        {
        }
    }

    public class MultiCrewThirdPersonFovInButton : StandardBindingAction
    {
        public MultiCrewThirdPersonFovInButton()
            : base("MultiCrewThirdPersonFovInButton", "MultiCrewThirdPersonFovInButton",
                () => EDBindings.Ship.MultiCrewThirdPersonFovInButton)
        {
        }
    }

    public class MultiCrewThirdPersonFovOutButton : StandardBindingAction
    {
        public MultiCrewThirdPersonFovOutButton()
            : base("MultiCrewThirdPersonFovOutButton", "MultiCrewThirdPersonFovOutButton",
                () => EDBindings.Ship.MultiCrewThirdPersonFovOutButton)
        {
        }
    }

    public class MultiCrewThirdPersonPitchDownButton : StandardBindingAction
    {
        public MultiCrewThirdPersonPitchDownButton()
            : base("MultiCrewThirdPersonPitchDownButton", "MultiCrewThirdPersonPitchDownButton",
                () => EDBindings.Ship.MultiCrewThirdPersonPitchDownButton)
        {
        }
    }

    public class MultiCrewThirdPersonPitchUpButton : StandardBindingAction
    {
        public MultiCrewThirdPersonPitchUpButton()
            : base("MultiCrewThirdPersonPitchUpButton", "MultiCrewThirdPersonPitchUpButton",
                () => EDBindings.Ship.MultiCrewThirdPersonPitchUpButton)
        {
        }
    }

    public class MultiCrewThirdPersonYawLeftButton : StandardBindingAction
    {
        public MultiCrewThirdPersonYawLeftButton()
            : base("MultiCrewThirdPersonYawLeftButton", "MultiCrewThirdPersonYawLeftButton",
                () => EDBindings.Ship.MultiCrewThirdPersonYawLeftButton)
        {
        }
    }

    public class MultiCrewThirdPersonYawRightButton : StandardBindingAction
    {
        public MultiCrewThirdPersonYawRightButton()
            : base("MultiCrewThirdPersonYawRightButton", "MultiCrewThirdPersonYawRightButton",
                () => EDBindings.Ship.MultiCrewThirdPersonYawRightButton)
        {
        }
    }

    // TODO: is not in binds file. Is this exposed in the ED config if there is an Oculus headset?
    public class OculusReset : StandardBindingAction
    {
        public OculusReset()
            : base("OculusReset", "OculusReset",
                () => EDBindings.Ship.OculusReset)
        {
        }
    }

    // TODO: what is this?
    public class Pause : StandardBindingAction
    {
        public Pause()
            : base("Pause", "Pause",
                () => EDBindings.Ship.Pause)
        {
        }
    }

    public class PitchCameraUp : StandardBindingAction
    {
        public PitchCameraUp()
            : base("PitchCameraUp", "PitchCameraUp",
                () => EDBindings.Ship.PitchCameraUp)
        {
        }
    }

    public class PitchDownButton : StandardBindingAction
    {
        public PitchDownButton()
            : base("PitchDownButton", "PitchDownButton",
                () => EDBindings.Ship.PitchDownButton)
        {
        }
    }

    public class PitchDownButton_Landing : StandardBindingAction
    {
        public PitchDownButton_Landing()
            : base("PitchDownButton_Landing", "PitchDownButton_Landing",
                () => EDBindings.Ship.PitchDownButton_Landing)
        {
        }
    }

    public class PitchUpButton : StandardBindingAction
    {
        public PitchUpButton()
            : base("PitchUpButton", "PitchUpButton",
                () => EDBindings.Ship.PitchUpButton)
        {
        }
    }

    public class PitchUpButton_Landing : StandardBindingAction
    {
        public PitchUpButton_Landing()
            : base("PitchUpButton_Landing", "PitchUpButton_Landing",
                () => EDBindings.Ship.PitchUpButton_Landing)
        {
        }
    }

    public class PrimaryFire : StandardBindingAction
    {
        public PrimaryFire()
            : base("PrimaryFire", "PrimaryFire",
                () => EDBindings.Ship.PrimaryFire)
        {
        }
    }

    public class RightThrustButton : StandardBindingAction
    {
        public RightThrustButton()
            : base("RightThrustButton", "RightThrustButton",
                () => EDBindings.Ship.RightThrustButton)
        {
        }
    }

    public class RightThrustButton_Landing : StandardBindingAction
    {
        public RightThrustButton_Landing()
            : base("RightThrustButton_Landing", "RightThrustButton_Landing",
                () => EDBindings.Ship.RightThrustButton_Landing)
        {
        }
    }

    public class RollCameraLeft : StandardBindingAction
    {
        public RollCameraLeft()
            : base("RollCameraLeft", "RollCameraLeft",
                () => EDBindings.Ship.RollCameraLeft)
        {
        }
    }

    public class RollCameraRight : StandardBindingAction
    {
        public RollCameraRight()
            : base("RollCameraRight", "RollCameraRight",
                () => EDBindings.Ship.RollCameraRight)
        {
        }
    }

    public class RollLeftButton : StandardBindingAction
    {
        public RollLeftButton()
            : base("RollLeftButton", "RollLeftButton",
                () => EDBindings.Ship.RollLeftButton)
        {
        }
    }

    public class RollLeftButton_Landing : StandardBindingAction
    {
        public RollLeftButton_Landing()
            : base("RollLeftButton_Landing", "RollLeftButton_Landing",
                () => EDBindings.Ship.RollLeftButton_Landing)
        {
        }
    }

    public class RollRightButton : StandardBindingAction
    {
        public RollRightButton()
            : base("RollRightButton", "RollRightButton",
                () => EDBindings.Ship.RollRightButton)
        {
        }
    }

    public class RollRightButton_Landing : StandardBindingAction
    {
        public RollRightButton_Landing()
            : base("RollRightButton_Landing", "RollRightButton_Landing",
                () => EDBindings.Ship.RollRightButton_Landing)
        {
        }
    }

    public class SAAThirdPersonFovInButton : StandardBindingAction
    {
        public SAAThirdPersonFovInButton()
            : base("SAAThirdPersonFovInButton", "SAAThirdPersonFovInButton",
                () => EDBindings.Ship.SAAThirdPersonFovInButton)
        {
        }
    }

    public class SAAThirdPersonFovOutButton : StandardBindingAction
    {
        public SAAThirdPersonFovOutButton()
            : base("SAAThirdPersonFovOutButton", "SAAThirdPersonFovOutButton",
                () => EDBindings.Ship.SAAThirdPersonFovOutButton)
        {
        }
    }

    public class SAAThirdPersonPitchDownButton : StandardBindingAction
    {
        public SAAThirdPersonPitchDownButton()
            : base("SAAThirdPersonPitchDownButton", "SAAThirdPersonPitchDownButton",
                () => EDBindings.Ship.SAAThirdPersonPitchDownButton)
        {
        }
    }

    public class SAAThirdPersonPitchUpButton : StandardBindingAction
    {
        public SAAThirdPersonPitchUpButton()
            : base("SAAThirdPersonPitchUpButton", "SAAThirdPersonPitchUpButton",
                () => EDBindings.Ship.SAAThirdPersonPitchUpButton)
        {
        }
    }

    public class SAAThirdPersonYawLeftButton : StandardBindingAction
    {
        public SAAThirdPersonYawLeftButton()
            : base("SAAThirdPersonYawLeftButton", "SAAThirdPersonYawLeftButton",
                () => EDBindings.Ship.SAAThirdPersonYawLeftButton)
        {
        }
    }

    public class SAAThirdPersonYawRightButton : StandardBindingAction
    {
        public SAAThirdPersonYawRightButton()
            : base("SAAThirdPersonYawRightButton", "SAAThirdPersonYawRightButton",
                () => EDBindings.Ship.SAAThirdPersonYawRightButton)
        {
        }
    }

    public class SecondaryFire : StandardBindingAction
    {
        public SecondaryFire()
            : base("SecondaryFire", "SecondaryFire",
                () => EDBindings.Ship.SecondaryFire)
        {
        }
    }

    public class SteerLeftButton : StandardBindingAction
    {
        public SteerLeftButton()
            : base("SteerLeftButton", "SteerLeftButton",
                () => EDBindings.Ship.SteerLeftButton)
        {
        }
    }

    public class SteerRightButton : StandardBindingAction
    {
        public SteerRightButton()
            : base("SteerRightButton", "SteerRightButton",
                () => EDBindings.Ship.SteerRightButton)
        {
        }
    }

    public class StoreCamZoomIn : StandardBindingAction
    {
        public StoreCamZoomIn()
            : base("StoreCamZoomIn", "StoreCamZoomIn",
                () => EDBindings.Ship.StoreCamZoomIn)
        {
        }
    }

    public class StoreCamZoomOut : StandardBindingAction
    {
        public StoreCamZoomOut()
            : base("StoreCamZoomOut", "StoreCamZoomOut",
                () => EDBindings.Ship.StoreCamZoomOut)
        {
        }
    }

    public class StoreEnableRotation : StandardBindingAction
    {
        public StoreEnableRotation()
            : base("StoreEnableRotation", "StoreEnableRotation",
                () => EDBindings.Ship.StoreEnableRotation)
        {
        }
    }

    public class StorePitchCameraDown : StandardBindingAction
    {
        public StorePitchCameraDown()
            : base("StorePitchCameraDown", "StorePitchCameraDown",
                () => EDBindings.Ship.StorePitchCameraDown)
        {
        }
    }

    public class StorePitchCameraUp : StandardBindingAction
    {
        public StorePitchCameraUp()
            : base("StorePitchCameraUp", "StorePitchCameraUp",
                () => EDBindings.Ship.StorePitchCameraUp)
        {
        }
    }

    public class StoreToggle : StandardBindingAction
    {
        public StoreToggle()
            : base("StoreToggle", "StoreToggle",
                () => EDBindings.Ship.StoreToggle)
        {
        }
    }

    public class StoreYawCameraLeft : StandardBindingAction
    {
        public StoreYawCameraLeft()
            : base("StoreYawCameraLeft", "StoreYawCameraLeft",
                () => EDBindings.Ship.StoreYawCameraLeft)
        {
        }
    }

    public class StoreYawCameraRight : StandardBindingAction
    {
        public StoreYawCameraRight()
            : base("StoreYawCameraRight", "StoreYawCameraRight",
                () => EDBindings.Ship.StoreYawCameraRight)
        {
        }
    }

    // TODO: what is this?
    public class ToggleAdvanceMode : StandardBindingAction
    {
        public ToggleAdvanceMode()
            : base("ToggleAdvanceMode", "ToggleAdvanceMode",
                () => EDBindings.Ship.ToggleAdvanceMode)
        {
        }
    }

    public class UpThrustButton : StandardBindingAction
    {
        public UpThrustButton()
            : base("UpThrustButton", "UpThrustButton",
                () => EDBindings.Ship.UpThrustButton)
        {
        }
    }

    public class UpThrustButton_Landing : StandardBindingAction
    {
        public UpThrustButton_Landing()
            : base("UpThrustButton_Landing", "UpThrustButton_Landing",
                () => EDBindings.Ship.UpThrustButton_Landing)
        {
        }
    }

    public class VanityCameraScrollLeft : StandardBindingAction
    {
        public VanityCameraScrollLeft()
            : base("VanityCameraScrollLeft", "VanityCameraScrollLeft",
                () => EDBindings.Ship.VanityCameraScrollLeft)
        {
        }
    }

    public class VanityCameraScrollRight : StandardBindingAction
    {
        public VanityCameraScrollRight()
            : base("VanityCameraScrollRight", "VanityCameraScrollRight",
                () => EDBindings.Ship.VanityCameraScrollRight)
        {
        }
    }

    public class YawCameraLeft : StandardBindingAction
    {
        public YawCameraLeft()
            : base("YawCameraLeft", "YawCameraLeft",
                () => EDBindings.Ship.YawCameraLeft)
        {
        }
    }

    public class YawCameraRight : StandardBindingAction
    {
        public YawCameraRight()
            : base("YawCameraRight", "YawCameraRight",
                () => EDBindings.Ship.YawCameraRight)
        {
        }
    }

    public class YawLeftButton : StandardBindingAction
    {
        public YawLeftButton()
            : base("YawLeftButton", "YawLeftButton",
                () => EDBindings.Ship.YawLeftButton)
        {
        }
    }

    public class YawLeftButton_Landing : StandardBindingAction
    {
        public YawLeftButton_Landing()
            : base("YawLeftButton_Landing", "YawLeftButton_Landing",
                () => EDBindings.Ship.YawLeftButton_Landing)
        {
        }
    }

    public class YawRightButton : StandardBindingAction
    {
        public YawRightButton()
            : base("YawRightButton", "YawRightButton",
                () => EDBindings.Ship.YawRightButton)
        {
        }
    }

    public class YawRightButton_Landing : StandardBindingAction
    {
        public YawRightButton_Landing()
            : base("YawRightButton_Landing", "YawRightButton_Landing",
                () => EDBindings.Ship.YawRightButton_Landing)
        {
        }
    }

 */

