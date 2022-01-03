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

    // Macro Deck serializes all actions to XML and back when they are loaded.
    // So we need an unique type for each one. Otherwise we could just create
    // instances with different name, description, and getBindings values.
    //
    // There are a lot of possible actions for Elite Dangerous, but many of them
    // don't make sense unless (until?) Macro Deck supports holding down a
    // button So many of the entries below are commented out. (e.g. PluginAction
    // could support StartTrigger and StopTrigger events, the default
    // implementation of StartTigger could call Trigger). 
    //
    // The entries are sorted by BindingInfo property name. EliteDangerous
    // supports four different sets of bindings: General, Ship, Srv, and Foot.
    // There are four complete BindingInfo objects each of which will have ALL
    // the binding entries. Each action has to decide which set of bindings it
    // should use. In some cases, there may be multiple actions, each using a
    // binding from a different set.
    //
    // Action names all start with "General", "Ship", "SRV", "Foot", or "Current"
    // to indicate which binding set they use. The "Current" actions use the
    // current "ed_context" variable value to choose the binding. All "Current"
    // actions are at the end of the list.
    // 



    /*
    public class BackwardKey : SimpleStandardBindingAction
    {
        public BackwardKey()
            : base("Throttle - Decrease", "(ship) Decrease throttle.",
                () => EDBindings.Ship.BackwardKey)
        {
        }
    }
    */

    /* 
        public class BackwardThrustButton : SimpleStandardBindingAction
        {
            public BackwardThrustButton()
                : base("BackwardThrustButton", "BackwardThrustButton",
                    () => EDBindings.Ship.BackwardThrustButton)
            {
            }
        }

        public class BackwardThrustButton_Landing : SimpleStandardBindingAction
        {
            public BackwardThrustButton_Landing()
                : base("BackwardThrustButton_Landing", "BackwardThrustButton_Landing",
                    () => EDBindings.Ship.BackwardThrustButton_Landing)
            {
            }
        }

        public class BuggyPitchDownButton : SimpleStandardBindingAction
        {
            public BuggyPitchDownButton()
                : base("BuggyPitchDownButton", "BuggyPitchDownButton",
                    () => EDBindings.Ship.BuggyPitchDownButton)
            {
            }
        }

        public class BuggyPitchUpButton : SimpleStandardBindingAction
        {
            public BuggyPitchUpButton()
                : base("BuggyPitchUpButton", "BuggyPitchUpButton",
                    () => EDBindings.Ship.BuggyPitchUpButton)
            {
            }
        }

        public class BuggyPrimaryFireButton : SimpleStandardBindingAction
        {
            public BuggyPrimaryFireButton()
                : base("BuggyPrimaryFireButton", "BuggyPrimaryFireButton",
                    () => EDBindings.Ship.BuggyPrimaryFireButton)
            {
            }
        }

        public class BuggyRollLeft : SimpleStandardBindingAction
        {
            public BuggyRollLeft()
                : base("BuggyRollLeft", "BuggyRollLeft",
                    () => EDBindings.Ship.BuggyRollLeft)
            {
            }
        }

        public class BuggyRollLeftButton : SimpleStandardBindingAction
        {
            public BuggyRollLeftButton()
                : base("BuggyRollLeftButton", "BuggyRollLeftButton",
                    () => EDBindings.Ship.BuggyRollLeftButton)
            {
            }
        }

        public class BuggyRollRight : SimpleStandardBindingAction
        {
            public BuggyRollRight()
                : base("BuggyRollRight", "BuggyRollRight",
                    () => EDBindings.Ship.BuggyRollRight)
            {
            }
        }

        public class BuggyRollRightButton : SimpleStandardBindingAction
        {
            public BuggyRollRightButton()
                : base("BuggyRollRightButton", "BuggyRollRightButton",
                    () => EDBindings.Ship.BuggyRollRightButton)
            {
            }
        }

        public class BuggySecondaryFireButton : SimpleStandardBindingAction
        {
            public BuggySecondaryFireButton()
                : base("BuggySecondaryFireButton", "BuggySecondaryFireButton",
                    () => EDBindings.Ship.BuggySecondaryFireButton)
            {
            }
        }

        public class BuggyTurretPitchDownButton : SimpleStandardBindingAction
        {
            public BuggyTurretPitchDownButton()
                : base("BuggyTurretPitchDownButton", "BuggyTurretPitchDownButton",
                    () => EDBindings.Ship.BuggyTurretPitchDownButton)
            {
            }
        }

        public class BuggyTurretPitchUpButton : SimpleStandardBindingAction
        {
            public BuggyTurretPitchUpButton()
                : base("BuggyTurretPitchUpButton", "BuggyTurretPitchUpButton",
                    () => EDBindings.Ship.BuggyTurretPitchUpButton)
            {
            }
        }

        public class BuggyTurretYawLeftButton : SimpleStandardBindingAction
        {
            public BuggyTurretYawLeftButton()
                : base("BuggyTurretYawLeftButton", "BuggyTurretYawLeftButton",
                    () => EDBindings.Ship.BuggyTurretYawLeftButton)
            {
            }
        }

        public class BuggyTurretYawRightButton : SimpleStandardBindingAction
        {
            public BuggyTurretYawRightButton()
                : base("BuggyTurretYawRightButton", "BuggyTurretYawRightButton",
                    () => EDBindings.Ship.BuggyTurretYawRightButton)
            {
            }
        }

        public class CamPitchDown : SimpleStandardBindingAction
        {
            public CamPitchDown()
                : base("CamPitchDown", "CamPitchDown",
                    () => EDBindings.Ship.CamPitchDown)
            {
            }
        }

        public class CamPitchUp : SimpleStandardBindingAction
        {
            public CamPitchUp()
                : base("CamPitchUp", "CamPitchUp",
                    () => EDBindings.Ship.CamPitchUp)
            {
            }
        }

        public class CamTranslateBackward : SimpleStandardBindingAction
        {
            public CamTranslateBackward()
                : base("CamTranslateBackward", "CamTranslateBackward",
                    () => EDBindings.Ship.CamTranslateBackward)
            {
            }
        }

        public class CamTranslateDown : SimpleStandardBindingAction
        {
            public CamTranslateDown()
                : base("CamTranslateDown", "CamTranslateDown",
                    () => EDBindings.Ship.CamTranslateDown)
            {
            }
        }

        public class CamTranslateForward : SimpleStandardBindingAction
        {
            public CamTranslateForward()
                : base("CamTranslateForward", "CamTranslateForward",
                    () => EDBindings.Ship.CamTranslateForward)
            {
            }
        }

        public class CamTranslateLeft : SimpleStandardBindingAction
        {
            public CamTranslateLeft()
                : base("CamTranslateLeft", "CamTranslateLeft",
                    () => EDBindings.Ship.CamTranslateLeft)
            {
            }
        }

        public class CamTranslateRight : SimpleStandardBindingAction
        {
            public CamTranslateRight()
                : base("CamTranslateRight", "CamTranslateRight",
                    () => EDBindings.Ship.CamTranslateRight)
            {
            }
        }

        public class CamTranslateUp : SimpleStandardBindingAction
        {
            public CamTranslateUp()
                : base("CamTranslateUp", "CamTranslateUp",
                    () => EDBindings.Ship.CamTranslateUp)
            {
            }
        }

        public class CamYawLeft : SimpleStandardBindingAction
        {
            public CamYawLeft()
                : base("CamYawLeft", "CamYawLeft",
                    () => EDBindings.Ship.CamYawLeft)
            {
            }
        }

        public class CamYawRight : SimpleStandardBindingAction
        {
            public CamYawRight()
                : base("CamYawRight", "CamYawRight",
                    () => EDBindings.Ship.CamYawRight)
            {
            }
        }

        public class CamZoomIn : SimpleStandardBindingAction
        {
            public CamZoomIn()
                : base("CamZoomIn", "CamZoomIn",
                    () => EDBindings.Ship.CamZoomIn)
            {
            }
        }

        public class CamZoomOut : SimpleStandardBindingAction
        {
            public CamZoomOut()
                : base("CamZoomOut", "CamZoomOut",
                    () => EDBindings.Ship.CamZoomOut)
            {
            }
        }

        public class ChargeECM : SimpleStandardBindingAction
        {
            public ChargeECM()
                : base("ChargeECM", "ChargeECM",
                    () => EDBindings.Ship.ChargeECM)
            {
            }
        }

        public class CommanderCreator_Redo : SimpleStandardBindingAction
        {
            public CommanderCreator_Redo()
                : base("CommanderCreator_Redo", "CommanderCreator_Redo",
                    () => EDBindings.Ship.CommanderCreator_Redo)
            {
            }
        }

        public class CommanderCreator_Rotation : SimpleStandardBindingAction
        {
            public CommanderCreator_Rotation()
                : base("CommanderCreator_Rotation", "CommanderCreator_Rotation",
                    () => EDBindings.Ship.CommanderCreator_Rotation)
            {
            }
        }

        public class CommanderCreator_Rotation_MouseToggle : SimpleStandardBindingAction
        {
            public CommanderCreator_Rotation_MouseToggle()
                : base("CommanderCreator_Rotation_MouseToggle", "CommanderCreator_Rotation_MouseToggle",
                    () => EDBindings.Ship.CommanderCreator_Rotation_MouseToggle)
            {
            }
        }

        public class CommanderCreator_Undo : SimpleStandardBindingAction
        {
            public CommanderCreator_Undo()
                : base("CommanderCreator_Undo", "CommanderCreator_Undo",
                    () => EDBindings.Ship.CommanderCreator_Undo)
            {
            }
        }
    */

    public class CycleFireGroupNext : SimpleStandardBindingAction
    {
        public CycleFireGroupNext()
            : base("Fire Group - Next", "(ship) Select next fire group.",
                () => EDBindings.Ship.CycleFireGroupNext)
        {
        }
    }

    public class CycleFireGroupPrevious : SimpleStandardBindingAction
    {
        public CycleFireGroupPrevious()
            : base("Fire Group - Previous", "(ship) Select previous fire group.",
                () => EDBindings.Ship.CycleFireGroupPrevious)
        {
        }
    }

    public class CycleNextHostileTarget : SimpleStandardBindingAction
    {
        public CycleNextHostileTarget()
            : base("Target Hostile - Next", "(ship) Select next hostile contact.",
                () => EDBindings.Ship.CycleNextHostileTarget)
        {
        }
    }

    public class CycleNextPage : SimpleStandardBindingAction
    {
        public CycleNextPage()
            : base("UI Page - Next", "Select next UI page.",
                () => EDBindings.General.CycleNextPage)
        {
        }
    }

    public class CycleNextPanel : SimpleStandardBindingAction
    {
        public CycleNextPanel()
            : base("UI Panel - Next", "Select next UI panel.",
                () => EDBindings.General.CycleNextPanel)
        {
        }
    }

    public class CycleNextSubsystem : SimpleStandardBindingAction
    {
        public CycleNextSubsystem()
            : base("Target Subsystem - Next", "(ship) Select the next subsystem of current target.",
                () => EDBindings.Ship.CycleNextSubsystem)
        {
        }
    }

    public class CycleNextTarget : SimpleStandardBindingAction
    {
        public CycleNextTarget()
            : base("Target Contact - Next", "(ship) Target the next contact.",
                () => EDBindings.Ship.CycleNextTarget)
        {
        }
    }

    public class CyclePreviousHostileTarget : SimpleStandardBindingAction
    {
        public CyclePreviousHostileTarget()
            : base("Target Hostile - Previous", "(ship) Select previous hostile contact.",
                () => EDBindings.Ship.CyclePreviousHostileTarget)
        {
        }
    }

    public class CyclePreviousPage : SimpleStandardBindingAction
    {
        public CyclePreviousPage()
            : base("UI Page - Previous", "Select previous UI page.",
                () => EDBindings.Ship.CyclePreviousPage)
        {
        }
    }

    public class CyclePreviousPanel : SimpleStandardBindingAction
    {
        public CyclePreviousPanel()
            : base("UI Panel - Previous", "Select previous UI panel.",
                () => EDBindings.Ship.CyclePreviousPanel)
        {
        }
    }

    public class CyclePreviousSubsystem : SimpleStandardBindingAction
    {
        public CyclePreviousSubsystem()
            : base("Target Subsystem - Previous", "(ship) Select the previous system of current target.",
                () => EDBindings.Ship.CyclePreviousSubsystem)
        {
        }
    }

    public class CyclePreviousTarget : SimpleStandardBindingAction
    {
        public CyclePreviousTarget()
            : base("Target Contact - Previous", "(ship) Target the previous contact.",
                () => EDBindings.Ship.CyclePreviousTarget)
        {
        }
    }

    /*
        public class DecreaseSpeedButtonMax : SimpleStandardBindingAction
        {
            public DecreaseSpeedButtonMax()
                : base("DecreaseSpeedButtonMax", "DecreaseSpeedButtonMax",
                    () => EDBindings.Ship.DecreaseSpeedButtonMax)
            {
            }
        }

        public class DecreaseSpeedButtonPartial : SimpleStandardBindingAction
        {
            public DecreaseSpeedButtonPartial()
                : base("DecreaseSpeedButtonPartial", "DecreaseSpeedButtonPartial",
                    () => EDBindings.Ship.DecreaseSpeedButtonPartial)
            {
            }
        }
    */

    public class DeployHardpointToggle : SimpleStandardBindingAction
    {
        public DeployHardpointToggle()
            : base("Hardpoints - Toggle", "(ship) Toggle hardpoints deployment.",
                () => EDBindings.Ship.DeployHardpointToggle)
        {
        }
    }

    public class DeployHeatSink : SimpleStandardBindingAction
    {
        public DeployHeatSink()
            : base("Heatsink - Deploy", "(ship) Deploy a heatsink.",
                () => EDBindings.Ship.DeployHeatSink)
        {
        }
    }

    /*
    public class DownThrustButton : SimpleStandardBindingAction
    {
        public DownThrustButton()
            : base("DownThrustButton", "DownThrustButton",
                () => EDBindings.Ship.DownThrustButton)
        {
        }
    }

    public class DownThrustButton_Landing : SimpleStandardBindingAction
    {
        public DownThrustButton_Landing()
            : base("DownThrustButton_Landing", "DownThrustButton_Landing",
                () => EDBindings.Ship.DownThrustButton_Landing)
        {
        }
    }
    */

    /*
    public class EjectAllCargo : SimpleStandardBindingAction
    {
        public EjectAllCargo()
            : base("Cargo Eject", "(ship) Eject all ship cargo.",
                () => EDBindings.Ship.EjectAllCargo)
        {
        }
    }

    public class EjectAllCargo_Buggy : SimpleStandardBindingAction
    {
        public EjectAllCargo_Buggy()
            : base("Cargo Eject (srv)", "Eject all SRV cargo.",
                () => EDBindings.Srv.EjectAllCargo_Buggy)
        {
        }
    }
    */

    public class EjectAllCargo_Context : ContextualStandardBindingAction
    {
        public EjectAllCargo_Context()
            : base("Cargo - Eject", "(ship, srv) Eject all cargo.")
        {
            AddContext("ship", () => EDBindings.Ship.EjectAllCargo);
            AddContext("srv", () => EDBindings.Srv.EjectAllCargo_Buggy);
        }
    }

    public class EngineColourToggle : SimpleStandardBindingAction
    {
        public EngineColourToggle()
            : base("Engine Colour - Toggle", "(ship) Turn on/off engine colour display.",
                () => EDBindings.Ship.EngineColourToggle)
        {
        }
    }

    /*
    public class ExplorationFSSCameraPitchDecreaseButton : SimpleStandardBindingAction
    {
        public ExplorationFSSCameraPitchDecreaseButton()
            : base("ExplorationFSSCameraPitchDecreaseButton", "ExplorationFSSCameraPitchDecreaseButton",
                () => EDBindings.Ship.ExplorationFSSCameraPitchDecreaseButton)
        {
        }
    }

    public class ExplorationFSSCameraPitchIncreaseButton : SimpleStandardBindingAction
    {
        public ExplorationFSSCameraPitchIncreaseButton()
            : base("ExplorationFSSCameraPitchIncreaseButton", "ExplorationFSSCameraPitchIncreaseButton",
                () => EDBindings.Ship.ExplorationFSSCameraPitchIncreaseButton)
        {
        }
    }

    public class ExplorationFSSCameraYawDecreaseButton : SimpleStandardBindingAction
    {
        public ExplorationFSSCameraYawDecreaseButton()
            : base("ExplorationFSSCameraYawDecreaseButton", "ExplorationFSSCameraYawDecreaseButton",
                () => EDBindings.Ship.ExplorationFSSCameraYawDecreaseButton)
        {
        }
    }

    public class ExplorationFSSCameraYawIncreaseButton : SimpleStandardBindingAction
    {
        public ExplorationFSSCameraYawIncreaseButton()
            : base("ExplorationFSSCameraYawIncreaseButton", "ExplorationFSSCameraYawIncreaseButton",
                () => EDBindings.Ship.ExplorationFSSCameraYawIncreaseButton)
        {
        }
    }
    */

    public class ExplorationFSSDiscoveryScan : SimpleStandardBindingAction
    {
        public ExplorationFSSDiscoveryScan()
            : base("FSS - Discovery Scan", "(ship) Perform an FSS discovery scan.",
                () => EDBindings.Ship.ExplorationFSSDiscoveryScan)
        {
        }
    }

    public class ExplorationFSSEnter : SimpleStandardBindingAction
    {
        public ExplorationFSSEnter()
            : base("FSS - Enter", "(ship) Enter FSS mode.",
                () => EDBindings.Ship.ExplorationFSSEnter)
        {
        }
    }

    /*
    public class ExplorationFSSMiniZoomIn : SimpleStandardBindingAction
    {
        public ExplorationFSSMiniZoomIn()
            : base("ExplorationFSSMiniZoomIn", "ExplorationFSSMiniZoomIn",
                () => EDBindings.Ship.ExplorationFSSMiniZoomIn)
        {
        }
    }

    public class ExplorationFSSMiniZoomOut : SimpleStandardBindingAction
    {
        public ExplorationFSSMiniZoomOut()
            : base("ExplorationFSSMiniZoomOut", "ExplorationFSSMiniZoomOut",
                () => EDBindings.Ship.ExplorationFSSMiniZoomOut)
        {
        }
    }
    */

    public class ExplorationFSSQuit : SimpleStandardBindingAction
    {
        public ExplorationFSSQuit()
            : base("FSS - Exit", "(ship) Leave FSS mode.",
                () => EDBindings.Ship.ExplorationFSSQuit)
        {
        }
    }

    /*
    public class ExplorationFSSRadioTuningX_Decrease : SimpleStandardBindingAction
    {
        public ExplorationFSSRadioTuningX_Decrease()
            : base("ExplorationFSSRadioTuningX_Decrease", "ExplorationFSSRadioTuningX_Decrease",
                () => EDBindings.Ship.ExplorationFSSRadioTuningX_Decrease)
        {
        }
    }

    public class ExplorationFSSRadioTuningX_Increase : SimpleStandardBindingAction
    {
        public ExplorationFSSRadioTuningX_Increase()
            : base("ExplorationFSSRadioTuningX_Increase", "ExplorationFSSRadioTuningX_Increase",
                () => EDBindings.Ship.ExplorationFSSRadioTuningX_Increase)
        {
        }
    }
    */

    public class ExplorationFSSShowHelp : SimpleStandardBindingAction
    {
        public ExplorationFSSShowHelp()
            : base("FSS - Help", "(ship) Show FSS help.",
                () => EDBindings.Ship.ExplorationFSSShowHelp)
        {
        }
    }

    public class ExplorationFSSTarget : SimpleStandardBindingAction
    {
        public ExplorationFSSTarget()
            : base("FSS - Target", "(ship) Target current FSS signal.",
                () => EDBindings.Ship.ExplorationFSSTarget)
        {
        }
    }

    /*
    public class ExplorationFSSZoomIn : SimpleStandardBindingAction
    {
        public ExplorationFSSZoomIn()
            : base("ExplorationFSSZoomIn", "ExplorationFSSZoomIn",
                () => EDBindings.Ship.ExplorationFSSZoomIn)
        {
        }
    }

    public class ExplorationFSSZoomOut : SimpleStandardBindingAction
    {
        public ExplorationFSSZoomOut()
            : base("ExplorationFSSZoomOut", "ExplorationFSSZoomOut",
                () => EDBindings.Ship.ExplorationFSSZoomOut)
        {
        }
    }
    */

    /*
    public class ExplorationSAAExitThirdPerson : SimpleStandardBindingAction
    {
        public ExplorationSAAExitThirdPerson()
            : base("ExplorationSAAExitThirdPerson", "ExplorationSAAExitThirdPerson",
                () => EDBindings.Ship.ExplorationSAAExitThirdPerson)
        {
        }
    }
    */

    public class ExplorationSAANextGenus : SimpleStandardBindingAction
    {
        public ExplorationSAANextGenus()
            : base("DSS Filter - Next", "(ship) Select next filter in DSS.",
                () => EDBindings.Ship.ExplorationSAANextGenus)
        {
        }
    }

    public class ExplorationSAAPreviousGenus : SimpleStandardBindingAction
    {
        public ExplorationSAAPreviousGenus()
            : base("DSS Filter - Previous", "(ship) Select previous filter in DSS.",
                () => EDBindings.Ship.ExplorationSAAPreviousGenus)
        {
        }
    }

    /* Camera increase/decrease blur? 
    public class FStopDec : SimpleStandardBindingAction
    {
        public FStopDec()
            : base("FStopDec", "FStopDec",
                () => EDBindings.Ship.FStopDec)
        {
        }
    }

    public class FStopInc : SimpleStandardBindingAction
    {
        public FStopInc()
            : base("FStopInc", "FStopInc",
                () => EDBindings.Ship.FStopInc)
        {
        }
    }
    */

    public class FireChaffLauncher : SimpleStandardBindingAction
    {
        public FireChaffLauncher()
            : base("Chaff - Launch", "(ship) Fire chaff launcher.",
                () => EDBindings.Ship.FireChaffLauncher)
        {
        }
    }

    public class FixCameraRelativeToggle : SimpleStandardBindingAction
    {
        public FixCameraRelativeToggle()
            : base("Camera Control - Toggle", "Toggle between control of the camera and ship.", // TODO: or srv?
                () => EDBindings.General.FixCameraRelativeToggle)
        {
        }
    }

    public class FixCameraWorldToggle : SimpleStandardBindingAction
    {
        public FixCameraWorldToggle()
            : base("Camera Connect - Toggle", "Connect/disconnect camera from ship.", // TOOD: or srv?
                () => EDBindings.General.FixCameraWorldToggle)
        {
        }
    }

    public class FocusCommsPanel_Context : ContextualStandardBindingAction
    {
        public FocusCommsPanel_Context()
            : base("Panel - Comms", "(ship, srv, foot) Select the comms panel.")
        {
            AddContext("ship", () => EDBindings.Ship.FocusCommsPanel);
            AddContext("srv", () => EDBindings.Ship.FocusCommsPanel_Buggy);
            AddContext("foot", () => EDBindings.Ship.FocusCommsPanel_Humanoid);
        }
    }

    /* not present in binds file
    public class FocusDistanceDec : SimpleStandardBindingAction
    {
        public FocusDistanceDec()
            : base("FocusDistanceDec", "FocusDistanceDec",
                () => EDBindings.Ship.FocusDistanceDec)
        {
        }
    }

    public class FocusDistanceInc : SimpleStandardBindingAction
    {
        public FocusDistanceInc()
            : base("FocusDistanceInc", "FocusDistanceInc",
                () => EDBindings.Ship.FocusDistanceInc)
        {
        }
    }
    */

    public class FocusLeftPanel_Context : ContextualStandardBindingAction
    {
        public FocusLeftPanel_Context()
            : base("Panel - External", "(ship, srv) Select the external panel.")
        {
            AddContext("ship", () => EDBindings.Ship.FocusLeftPanel);
            AddContext("srv", () => EDBindings.Srv.FocusLeftPanel_Buggy);
        }
    }

    public class FocusRadarPanel_Context : ContextualStandardBindingAction
    {
        public FocusRadarPanel_Context()
            : base("Panel - Role", "(ship, srv) Select the role panel.")
        {
            AddContext("ship", () => EDBindings.Ship.FocusRadarPanel);
            AddContext("srv", () => EDBindings.Srv.FocusRadarPanel_Buggy);
        }
    }

    public class FocusRightPanel_Context : ContextualStandardBindingAction
    {
        public FocusRightPanel_Context()
            : base("Panel - Internal", "(ship, srv) Select the internal panel.")
        {
            AddContext("ship", () => EDBindings.Ship.FocusRightPanel);
            AddContext("srv", () => EDBindings.Srv.FocusRightPanel_Buggy);
        }
    }

    /*
    public class ForwardKey : SimpleStandardBindingAction
    {
        public ForwardKey()
            : base("Throttle - Increase", "(ship) Increase throttle.", // TODO: srv too?
                () => EDBindings.Ship.ForwardKey)
        {
        }
    }
    */

    /*
    public class ForwardThrustButton : SimpleStandardBindingAction
    {
        public ForwardThrustButton()
            : base("ForwardThrustButton", "ForwardThrustButton",
                () => EDBindings.Ship.ForwardThrustButton)
        {
        }
    }

    public class ForwardThrustButton_Landing : SimpleStandardBindingAction
    {
        public ForwardThrustButton_Landing()
            : base("ForwardThrustButton_Landing", "ForwardThrustButton_Landing",
                () => EDBindings.Ship.ForwardThrustButton_Landing)
        {
        }
    }

    public class FreeCamSpeedDec : SimpleStandardBindingAction
    {
        public FreeCamSpeedDec()
            : base("FreeCamSpeedDec", "FreeCamSpeedDec",
                () => EDBindings.Ship.FreeCamSpeedDec)
        {
        }
    }

    public class FreeCamSpeedInc : SimpleStandardBindingAction
    {
        public FreeCamSpeedInc()
            : base("FreeCamSpeedInc", "FreeCamSpeedInc",
                () => EDBindings.Ship.FreeCamSpeedInc)
        {
        }
    }
    */

    public class FreeCamToggleHUD : SimpleStandardBindingAction
    {
        public FreeCamToggleHUD()
            : base("Camera HUD - Toggle", "Turn on/off the HUD when in free camera mode.",
                () => EDBindings.General.FreeCamToggleHUD)
        {
        }
    }

    /*
    public class FreeCamZoomIn : SimpleStandardBindingAction
    {
        public FreeCamZoomIn()
            : base("FreeCamZoomIn", "FreeCamZoomIn",
                () => EDBindings.Ship.FreeCamZoomIn)
        {
        }
    }

    public class FreeCamZoomOut : SimpleStandardBindingAction
    {
        public FreeCamZoomOut()
            : base("FreeCamZoomOut", "FreeCamZoomOut",
                () => EDBindings.Ship.FreeCamZoomOut)
        {
        }
    }
    */

    public class FriendsMenu_Context : ContextualStandardBindingAction
    {
        public FriendsMenu_Context()
            : base("Panel - Friends", "(ship, srv) Open friends menu.")
        {
            // NOTE: ship config used for both
            AddContext("ship", () => EDBindings.Ship.FriendsMenu);
            AddContext("srv", () => EDBindings.Ship.FriendsMenu);
            // TODO: on foot too?
        }
    }

    public class GalaxyMapOpen_Context : ContextualStandardBindingAction
    {
        public GalaxyMapOpen_Context()
            : base("Panel - Galaxy Map", "(ship, srv, foot) Open Galaxy Map or on foot.")
        {
            AddContext("ship", () => EDBindings.Ship.GalaxyMapOpen);
            AddContext("srv", () => EDBindings.Srv.GalaxyMapOpen_Buggy);
            AddContext("foot", () => EDBindings.Ship.GalaxyMapOpen_Humanoid);
        }
    }

    public class GalnetAudio_ClearQueue : SimpleStandardBindingAction
    {
        public GalnetAudio_ClearQueue()
            : base("Galnet - Clear Queue", "Clear Galnet play queue.",
                () => EDBindings.Ship.GalnetAudio_ClearQueue)
        {
        }
    }

    public class GalnetAudio_Play_Pause : SimpleStandardBindingAction
    {
        public GalnetAudio_Play_Pause()
            : base("Galnet - Play/Pause", "Pause or play Galnet audio.",
                () => EDBindings.Ship.GalnetAudio_Play_Pause)
        {
        }
    }

    public class GalnetAudio_SkipBackward : SimpleStandardBindingAction
    {
        public GalnetAudio_SkipBackward()
            : base("Galnet - Backward", "Skip backward in Galnet audio.",
                () => EDBindings.Ship.GalnetAudio_SkipBackward)
        {
        }
    }

    public class GalnetAudio_SkipForward : SimpleStandardBindingAction
    {
        public GalnetAudio_SkipForward()
            : base("Galnet - Forward", "Skip forward in Galnet audio.",
                () => EDBindings.Ship.GalnetAudio_SkipForward)
        {
        }
    }

    // TODO: is this for SRV and on foot too?
    public class HMDReset : SimpleStandardBindingAction
    {
        public HMDReset()
            : base("HMD - Reset", "Reset VR Headset.",
                () => EDBindings.Ship.HMDReset)
        {
        }
    }

    /*
    public class HeadLookPitchDown : SimpleStandardBindingAction
    {
        public HeadLookPitchDown()
            : base("HeadLookPitchDown", "HeadLookPitchDown",
                () => EDBindings.Ship.HeadLookPitchDown)
        {
        }
    }

    public class HeadLookPitchUp : SimpleStandardBindingAction
    {
        public HeadLookPitchUp()
            : base("HeadLookPitchUp", "HeadLookPitchUp",
                () => EDBindings.Ship.HeadLookPitchUp)
        {
        }
    }

    public class HeadLookReset : SimpleStandardBindingAction
    {
        public HeadLookReset()
            : base("HeadLookReset", "HeadLookReset",
                () => EDBindings.Ship.HeadLookReset)
        {
        }
    }

    public class HeadLookYawLeft : SimpleStandardBindingAction
    {
        public HeadLookYawLeft()
            : base("HeadLookYawLeft", "HeadLookYawLeft",
                () => EDBindings.Ship.HeadLookYawLeft)
        {
        }
    }

    public class HeadLookYawRight : SimpleStandardBindingAction
    {
        public HeadLookYawRight()
            : base("HeadLookYawRight", "HeadLookYawRight",
                () => EDBindings.Ship.HeadLookYawRight)
        {
        }
    }
    */

    /*
    public class HumanoidBackwardButton : SimpleStandardBindingAction
    {
        public HumanoidBackwardButton()
            : base("Move - Backward", "(foot) Move backward.",
                () => EDBindings.Foot.HumanoidBackwardButton)
        {
        }
    }
    */

    public class HumanoidBattery : SimpleStandardBindingAction
    {
        public HumanoidBattery()
            : base("Energy Cell - Use", "(foot) Use energy cell.",
                () => EDBindings.Foot.HumanoidBattery)
        {
        }
    }

    public class HumanoidClearAuthorityLevel : SimpleStandardBindingAction
    {
        public HumanoidClearAuthorityLevel()
            : base("Authority Level - Clear", "(foot) Clear stolen authority level.",
                () => EDBindings.Foot.HumanoidClearAuthorityLevel)
        {
        }
    }

    public class HumanoidConflictContextualUIButton : SimpleStandardBindingAction
    {
        public HumanoidConflictContextualUIButton()
            : base("Panel - Battle Stats", "(foot) Open battle stats at conclusion of a conflict zone.",
                () => EDBindings.Foot.HumanoidConflictContextualUIButton)
        {
        }
    }

    public class HumanoidCrouchButton : SimpleStandardBindingAction
    {
        public HumanoidCrouchButton()
            : base("Crouch - Toggle", "(foot) Toggle crouching. Requires toggle button mode.",
                () => EDBindings.Foot.HumanoidCrouchButton)
        {
        }
    }

    public class HumanoidEmoteSlot1 : SimpleStandardBindingAction
    {
        public HumanoidEmoteSlot1()
            : base("Emote - Point", "(foot) Play point emote.",
                () => EDBindings.Foot.HumanoidEmoteSlot1)
        {
        }
    }

    public class HumanoidEmoteSlot2 : SimpleStandardBindingAction
    {
        public HumanoidEmoteSlot2()
            : base("Emote - Wave", "(foot) Play wave emote.",
                () => EDBindings.Foot.HumanoidEmoteSlot2)
        {
        }
    }

    public class HumanoidEmoteSlot3 : SimpleStandardBindingAction
    {
        public HumanoidEmoteSlot3()
            : base("Emote - Agree", "(foot) Play agree emote.",
                () => EDBindings.Foot.HumanoidEmoteSlot3)
        {
        }
    }

    public class HumanoidEmoteSlot4 : SimpleStandardBindingAction
    {
        public HumanoidEmoteSlot4()
            : base("Emote - Disagree", "(foot) Play disagree emote.",
                () => EDBindings.Foot.HumanoidEmoteSlot4)
        {
        }
    }

    public class HumanoidEmoteSlot5 : SimpleStandardBindingAction
    {
        public HumanoidEmoteSlot5()
            : base("Emote - Go", "(foot) Play go emote.",
                () => EDBindings.Foot.HumanoidEmoteSlot5)
        {
        }
    }

    public class HumanoidEmoteSlot6 : SimpleStandardBindingAction
    {
        public HumanoidEmoteSlot6()
            : base("Emote - Stop", "(foot) Play stop emote.",
                () => EDBindings.Foot.HumanoidEmoteSlot6)
        {
        }
    }

    public class HumanoidEmoteSlot7 : SimpleStandardBindingAction
    {
        public HumanoidEmoteSlot7()
            : base("Emote - Applaud", "(foot) Play applaud emote.",
                () => EDBindings.Foot.HumanoidEmoteSlot7)
        {
        }
    }

    public class HumanoidEmoteSlot8 : SimpleStandardBindingAction
    {
        public HumanoidEmoteSlot8()
            : base("Emote - Salute", "(foot) Play salute emote.",
                () => EDBindings.Foot.HumanoidEmoteSlot8)
        {
        }
    }

    /*
    public class HumanoidEmoteWheelButton : SimpleStandardBindingAction
    {
        public HumanoidEmoteWheelButton()
            : base("Emote Wheel", "(foot) Open emote wheel. REQUIRES TOGGLE BUTTON MODE",
                () => EDBindings.Foot.HumanoidEmoteWheelButton)
        {
        }
    }
    */

    /*
    public class HumanoidForwardButton : SimpleStandardBindingAction
    {
        public HumanoidForwardButton()
            : base("Move - Forward", "(foot) Move forward.",
                () => EDBindings.Ship.HumanoidForwardButton)
        {
        }
    }
    */

    public class HumanoidHealthPack : SimpleStandardBindingAction
    {
        public HumanoidHealthPack()
            : base("Health Pack - Use", "(foot) Use health pack.",
                () => EDBindings.Foot.HumanoidHealthPack)
        {
        }
    }

    public class HumanoidHideWeaponButton : SimpleStandardBindingAction
    {
        public HumanoidHideWeaponButton()
            : base("Weapon - Holster", "(foot) Put away weapon or tool.",
                () => EDBindings.Foot.HumanoidHideWeaponButton)
        {
        }
    }

    /*
    public class HumanoidItemWheelButton : SimpleStandardBindingAction
    {
        public HumanoidItemWheelButton()
            : base("Item Wheel", "(foot) Open item wheel. REQUIRES TOGGLE BUTTON MODE",
                () => EDBindings.Foot.HumanoidItemWheelButton)
        {
        }
    }
    */

    /*
    public class HumanoidJumpButton : SimpleStandardBindingAction
    {
        public HumanoidJumpButton()
            : base("Move - Jump", "(foot) How high?",
                () => EDBindings.Ship.HumanoidJumpButton)
        {
        }
    }
    */

    /*
    public class HumanoidMeleeButton : SimpleStandardBindingAction
    {
        public HumanoidMeleeButton()
            : base("Melee", "(foot) Perform melee attack.",
                () => EDBindings.Ship.HumanoidMeleeButton)
        {
        }
    }
    */

    public class HumanoidOpenAccessPanelButton : SimpleStandardBindingAction
    {
        public HumanoidOpenAccessPanelButton()
            : base("Panel - Insight Hub", "(foot) Open insight hub.",
                () => EDBindings.Foot.HumanoidOpenAccessPanelButton)
        {
        }
    }

    /* TODO: What is this? Is it configurable in Elite Dangerous UI?
    public class HumanoidPing : SimpleStandardBindingAction
    {
        public HumanoidPing()
            : base("HumanoidPing", "HumanoidPing",
                () => EDBindings.Ship.HumanoidPing)
        {
        }
    }
    */

    /*
    public class HumanoidPitchDownButton : SimpleStandardBindingAction
    {
        public HumanoidPitchDownButton()
            : base("HumanoidPitchDownButton", "HumanoidPitchDownButton",
                () => EDBindings.Ship.HumanoidPitchDownButton)
        {
        }
    }

    public class HumanoidPitchUpButton : SimpleStandardBindingAction
    {
        public HumanoidPitchUpButton()
            : base("HumanoidPitchUpButton", "HumanoidPitchUpButton",
                () => EDBindings.Ship.HumanoidPitchUpButton)
        {
        }
    }
    */

    /*
    public class HumanoidPrimaryFireButton : SimpleStandardBindingAction
    {
        public HumanoidPrimaryFireButton()
            : base("HumanoidPrimaryFireButton", "HumanoidPrimaryFireButton",
                () => EDBindings.Ship.HumanoidPrimaryFireButton)
        {
        }
    }

    public class HumanoidPrimaryInteractButton : SimpleStandardBindingAction
    {
        public HumanoidPrimaryInteractButton()
            : base("HumanoidPrimaryInteractButton", "HumanoidPrimaryInteractButton",
                () => EDBindings.Ship.HumanoidPrimaryInteractButton)
        {
        }
    }
    */

    public class HumanoidReloadButton : SimpleStandardBindingAction
    {
        public HumanoidReloadButton()
            : base("Weapon - Reload", "(foot) Reload weapon.",
                () => EDBindings.Foot.HumanoidReloadButton)
        {
        }
    }

    /*
    public class HumanoidRotateLeftButton : SimpleStandardBindingAction
    {
        public HumanoidRotateLeftButton()
            : base("HumanoidRotateLeftButton", "HumanoidRotateLeftButton",
                () => EDBindings.Ship.HumanoidRotateLeftButton)
        {
        }
    }

    public class HumanoidRotateRightButton : SimpleStandardBindingAction
    {
        public HumanoidRotateRightButton()
            : base("HumanoidRotateRightButton", "HumanoidRotateRightButton",
                () => EDBindings.Ship.HumanoidRotateRightButton)
        {
        }
    }

    public class HumanoidSecondaryInteractButton : SimpleStandardBindingAction
    {
        public HumanoidSecondaryInteractButton()
            : base("HumanoidSecondaryInteractButton", "HumanoidSecondaryInteractButton",
                () => EDBindings.Ship.HumanoidSecondaryInteractButton)
        {
        }
    }
    */

    public class HumanoidSelectEMPGrenade : SimpleStandardBindingAction
    {
        public HumanoidSelectEMPGrenade()
            : base("Grenade - EMP", "(foot) Select EMP grenades.",
                () => EDBindings.Ship.HumanoidSelectEMPGrenade)
        {
        }
    }

    public class HumanoidSelectFragGrenade : SimpleStandardBindingAction
    {
        public HumanoidSelectFragGrenade()
            : base("Grenade - Frag", "(foot) Select frag grenades.",
                () => EDBindings.Ship.HumanoidSelectFragGrenade)
        {
        }
    }

    public class HumanoidSelectNextGrenadeTypeButton : SimpleStandardBindingAction
    {
        public HumanoidSelectNextGrenadeTypeButton()
            : base("Grenade - Next", "(foot) Select next grenade type.",
                () => EDBindings.Ship.HumanoidSelectNextGrenadeTypeButton)
        {
        }
    }

    public class HumanoidSelectNextWeaponButton : SimpleStandardBindingAction
    {
        public HumanoidSelectNextWeaponButton()
            : base("Weapon - Next", "(foot) Select next weapon.",
                () => EDBindings.Ship.HumanoidSelectNextWeaponButton)
        {
        }
    }

    public class HumanoidSelectPreviousGrenadeTypeButton : SimpleStandardBindingAction
    {
        public HumanoidSelectPreviousGrenadeTypeButton()
            : base("Grenade - Previous", "(foot) Select previous grenade type.",
                () => EDBindings.Ship.HumanoidSelectPreviousGrenadeTypeButton)
        {
        }
    }

    public class HumanoidSelectPreviousWeaponButton : SimpleStandardBindingAction
    {
        public HumanoidSelectPreviousWeaponButton()
            : base("Weapon - Previous", "(foot) Select previous weapon.",
                () => EDBindings.Ship.HumanoidSelectPreviousWeaponButton)
        {
        }
    }

    public class HumanoidSelectPrimaryWeaponButton : SimpleStandardBindingAction
    {
        public HumanoidSelectPrimaryWeaponButton()
            : base("Weapon - Primary", "(foot) Select primary weapon.",
                () => EDBindings.Ship.HumanoidSelectPrimaryWeaponButton)
        {
        }
    }

    public class HumanoidSelectSecondaryWeaponButton : SimpleStandardBindingAction
    {
        public HumanoidSelectSecondaryWeaponButton()
            : base("Weapon - Secondary", "(foot) Select secondary weapon.",
                () => EDBindings.Ship.HumanoidSelectSecondaryWeaponButton)
        {
        }
    }

    public class HumanoidSelectShieldGrenade : SimpleStandardBindingAction
    {
        public HumanoidSelectShieldGrenade()
            : base("Grenade - Shield", "(foot) Select shield grenades.",
                () => EDBindings.Ship.HumanoidSelectShieldGrenade)
        {
        }
    }

    public class HumanoidSelectUtilityWeaponButton : SimpleStandardBindingAction
    {
        public HumanoidSelectUtilityWeaponButton()
            : base("Weapon - Tool", "(foot) Select tool.",
                () => EDBindings.Ship.HumanoidSelectUtilityWeaponButton)
        {
        }
    }

    public class HumanoidSprintButton : SimpleStandardBindingAction
    {
        public HumanoidSprintButton()
            : base("Sprint - Toggle", "(foot) Toggle sprinting. REQUIRES TOGGLE BUTTON MODE",
                () => EDBindings.Foot.HumanoidSprintButton)
        {
        }
    }

    /*
    public class HumanoidStrafeLeftButton : SimpleStandardBindingAction
    {
        public HumanoidStrafeLeftButton()
            : base("HumanoidStrafeLeftButton", "HumanoidStrafeLeftButton",
                () => EDBindings.Ship.HumanoidStrafeLeftButton)
        {
        }
    }

    public class HumanoidStrafeRightButton : SimpleStandardBindingAction
    {
        public HumanoidStrafeRightButton()
            : base("HumanoidStrafeRightButton", "HumanoidStrafeRightButton",
                () => EDBindings.Ship.HumanoidStrafeRightButton)
        {
        }
    }
    */

    public class HumanoidSwitchToCompAnalyser : SimpleStandardBindingAction
    {
        public HumanoidSwitchToCompAnalyser()
            : base("Tool - Profile Analyser", "(foot) Select profile analyser.",
                () => EDBindings.Foot.HumanoidSwitchToCompAnalyser)
        {
        }
    }

    public class HumanoidSwitchToRechargeTool : SimpleStandardBindingAction
    {
        public HumanoidSwitchToRechargeTool()
            : base("Tool - Energy Link", "(foot) Select energy link.",
                () => EDBindings.Foot.HumanoidSwitchToRechargeTool)
        {
        }
    }

    public class HumanoidSwitchToSuitTool : SimpleStandardBindingAction
    {
        public HumanoidSwitchToSuitTool()
            : base("Tool - Suit Specific", "(foot) Select suit specific tool.",
                () => EDBindings.Foot.HumanoidSwitchToSuitTool)
        {
        }
    }

    public class HumanoidSwitchWeapon : SimpleStandardBindingAction
    {
        public HumanoidSwitchWeapon()
            : base("Weapon - Switch", "(foot) Switch between primary and secondary weapons.",
                () => EDBindings.Foot.HumanoidSwitchWeapon)
        {
        }
    }

    /*
    public class HumanoidThrowGrenadeButton : SimpleStandardBindingAction
    {
        public HumanoidThrowGrenadeButton()
            : base("Grenade - Throw", "(foot) Throw grenade.",
                () => EDBindings.Ship.HumanoidThrowGrenadeButton)
        {
        }
    }
    */

    public class HumanoidToggleMissionHelpPanelButton : SimpleStandardBindingAction
    {
        public HumanoidToggleMissionHelpPanelButton()
            : base("Panel - Mission Help", "(foot) Toggle mission help panel.",
                () => EDBindings.Foot.HumanoidToggleMissionHelpPanelButton)
        {
        }
    }

    public class HumanoidToggleShieldsButton : SimpleStandardBindingAction
    {
        public HumanoidToggleShieldsButton()
            : base("Shields - Toggle", "(foot) Enable/disable shields.",
                () => EDBindings.Foot.HumanoidToggleShieldsButton)
        {
        }
    }

    public class HumanoidToggleToolModeButton : SimpleStandardBindingAction
    {
        public HumanoidToggleToolModeButton()
            : base("Tool - Toggle Mode", "(foot) Toggle tool mode.",
                () => EDBindings.Foot.HumanoidToggleToolModeButton)
        {
        }
    }

    public class HumanoidUtilityWheelCycleMode : SimpleStandardBindingAction
    {
        public HumanoidUtilityWheelCycleMode()
            : base("Utility Wheel Mode - Cycle", "(foot) Cycle utility wheel mode.",
                () => EDBindings.Foot.HumanoidUtilityWheelCycleMode)
        {
        }
    }

    /* Aim down sights
    public class HumanoidZoomButton : SimpleStandardBindingAction
    {
        public HumanoidZoomButton()
            : base("HumanoidZoomButton", "HumanoidZoomButton",
                () => EDBindings.Ship.HumanoidZoomButton)
        {
        }
    }
    */

    public class HyperSuperCombination : SimpleStandardBindingAction
    {
        public HyperSuperCombination()
            : base("FSD - Toggle", "(ship) Perform hyperspace jump or enter/exit supercruse.",
                () => EDBindings.Ship.HyperSuperCombination)
        {
        }
    }

    public class Hyperspace : SimpleStandardBindingAction
    {
        public Hyperspace()
            : base("FSD - Hyperspace Jump", "(ship) Perform hyperspace jump.",
                () => EDBindings.Ship.Hyperspace)
        {
        }
    }

    public class IncreaseEnginesPower_Context : ContextualStandardBindingAction
    {
        public IncreaseEnginesPower_Context()
            : base("Pips - Engine", "(ship, srv) Increase engine power.")
        {
            AddContext("ship", () => EDBindings.Ship.IncreaseEnginesPower);
            AddContext("srv", () => EDBindings.Ship.IncreaseEnginesPower_Buggy);
        }
    }

    /*
    public class IncreaseSpeedButtonMax : SimpleStandardBindingAction
    {
        public IncreaseSpeedButtonMax()
            : base("IncreaseSpeedButtonMax", "IncreaseSpeedButtonMax",
                () => EDBindings.Ship.IncreaseSpeedButtonMax)
        {
        }
    }

    public class IncreaseSpeedButtonPartial : SimpleStandardBindingAction
    {
        public IncreaseSpeedButtonPartial()
            : base("IncreaseSpeedButtonPartial", "IncreaseSpeedButtonPartial",
                () => EDBindings.Ship.IncreaseSpeedButtonPartial)
        {
        }
    }
    */

    public class IncreaseSystemsPower_Context : ContextualStandardBindingAction
    {
        public IncreaseSystemsPower_Context()
            : base("Pips - Systems", "(ship, srv) Increase systems power.")
        {
            AddContext("ship", () => EDBindings.Ship.IncreaseSystemsPower);
            AddContext("srv", () => EDBindings.Ship.IncreaseSystemsPower_Buggy);
        }
    }

    public class IncreaseWeaponsPower_Context : ContextualStandardBindingAction
    {
        public IncreaseWeaponsPower_Context()
            : base("Pips - Weapons", "(ship, srv) Increase weapons power.")
        {
            AddContext("ship", () => EDBindings.Ship.IncreaseWeaponsPower);
            AddContext("srv", () => EDBindings.Ship.IncreaseWeaponsPower_Buggy);
        }
    }

    public class LandingGearToggle : SimpleStandardBindingAction
    {
        public LandingGearToggle()
            : base("Landing Gear - Toggle", "(ship) Raise/lower landing gear.",
                () => EDBindings.Ship.LandingGearToggle)
        {
        }
    }

    /*
    public class LeftThrustButton : SimpleStandardBindingAction
    {
        public LeftThrustButton()
            : base("LeftThrustButton", "LeftThrustButton",
                () => EDBindings.Ship.LeftThrustButton)
        {
        }
    }

    public class LeftThrustButton_Landing : SimpleStandardBindingAction
    {
        public LeftThrustButton_Landing()
            : base("LeftThrustButton_Landing", "LeftThrustButton_Landing",
                () => EDBindings.Ship.LeftThrustButton_Landing)
        {
        }
    }
    */

    public class LightsToggle_Context : ContextualStandardBindingAction
    {

        public LightsToggle_Context()
            : base("Lights - Toggle", "(ship, srv, foot) Turn on/off lights.")
        {
            AddContext("ship", () => EDBindings.Ship.ShipSpotLightToggle);
            AddContext("srv", () => EDBindings.Srv.HeadlightsBuggyButton);
            AddContext("foot", () => EDBindings.Foot.HumanoidToggleFlashlightButton);
        }

    }

    public class MouseReset : SimpleStandardBindingAction
    {
        public MouseReset()
            : base("Mouse - Reset", "(ship) Reset the mouse.", // TODO: srv and foot too?
                () => EDBindings.Ship.MouseReset)
        {
        }
    }

    /*
    public class MoveFreeCamBackwards : SimpleStandardBindingAction
    {
        public MoveFreeCamBackwards()
            : base("MoveFreeCamBackwards", "MoveFreeCamBackwards",
                () => EDBindings.Ship.MoveFreeCamBackwards)
        {
        }
    }

    public class MoveFreeCamDown : SimpleStandardBindingAction
    {
        public MoveFreeCamDown()
            : base("MoveFreeCamDown", "MoveFreeCamDown",
                () => EDBindings.Ship.MoveFreeCamDown)
        {
        }
    }

    public class MoveFreeCamForward : SimpleStandardBindingAction
    {
        public MoveFreeCamForward()
            : base("MoveFreeCamForward", "MoveFreeCamForward",
                () => EDBindings.Ship.MoveFreeCamForward)
        {
        }
    }

    public class MoveFreeCamLeft : SimpleStandardBindingAction
    {
        public MoveFreeCamLeft()
            : base("MoveFreeCamLeft", "MoveFreeCamLeft",
                () => EDBindings.Ship.MoveFreeCamLeft)
        {
        }
    }

    public class MoveFreeCamRight : SimpleStandardBindingAction
    {
        public MoveFreeCamRight()
            : base("MoveFreeCamRight", "MoveFreeCamRight",
                () => EDBindings.Ship.MoveFreeCamRight)
        {
        }
    }

    public class MoveFreeCamUp : SimpleStandardBindingAction
    {
        public MoveFreeCamUp()
            : base("MoveFreeCamUp", "MoveFreeCamUp",
                () => EDBindings.Ship.MoveFreeCamUp)
        {
        }
    }
    */

    public class MultiCrewCockpitUICycleBackward : SimpleStandardBindingAction
    {
        public MultiCrewCockpitUICycleBackward()
            : base("MultiCrew Cockpit - Backward", "(ship) Cycle multicrew cockpit UI backward.",
                () => EDBindings.Ship.MultiCrewCockpitUICycleBackward)
        {
        }
    }

    public class MultiCrewCockpitUICycleForward : SimpleStandardBindingAction
    {
        public MultiCrewCockpitUICycleForward()
            : base("MultiCrew Cockpit - Forward", "(ship) Cycle multicrew cockpit UI forward.",
                () => EDBindings.Ship.MultiCrewCockpitUICycleForward)
        {
        }
    }

    /*
    public class MultiCrewPrimaryFire : SimpleStandardBindingAction
    {
        public MultiCrewPrimaryFire()
            : base("MultiCrewPrimaryFire", "MultiCrewPrimaryFire",
                () => EDBindings.Ship.MultiCrewPrimaryFire)
        {
        }
    }

    public class MultiCrewPrimaryUtilityFire : SimpleStandardBindingAction
    {
        public MultiCrewPrimaryUtilityFire()
            : base("MultiCrewPrimaryUtilityFire", "MultiCrewPrimaryUtilityFire",
                () => EDBindings.Ship.MultiCrewPrimaryUtilityFire)
        {
        }
    }

    public class MultiCrewSecondaryFire : SimpleStandardBindingAction
    {
        public MultiCrewSecondaryFire()
            : base("MultiCrewSecondaryFire", "MultiCrewSecondaryFire",
                () => EDBindings.Ship.MultiCrewSecondaryFire)
        {
        }
    }

    public class MultiCrewSecondaryUtilityFire : SimpleStandardBindingAction
    {
        public MultiCrewSecondaryUtilityFire()
            : base("MultiCrewSecondaryUtilityFire", "MultiCrewSecondaryUtilityFire",
                () => EDBindings.Ship.MultiCrewSecondaryUtilityFire)
        {
        }
    }

    public class MultiCrewThirdPersonFovInButton : SimpleStandardBindingAction
    {
        public MultiCrewThirdPersonFovInButton()
            : base("MultiCrewThirdPersonFovInButton", "MultiCrewThirdPersonFovInButton",
                () => EDBindings.Ship.MultiCrewThirdPersonFovInButton)
        {
        }
    }

    public class MultiCrewThirdPersonFovOutButton : SimpleStandardBindingAction
    {
        public MultiCrewThirdPersonFovOutButton()
            : base("MultiCrewThirdPersonFovOutButton", "MultiCrewThirdPersonFovOutButton",
                () => EDBindings.Ship.MultiCrewThirdPersonFovOutButton)
        {
        }
    }

    public class MultiCrewThirdPersonPitchDownButton : SimpleStandardBindingAction
    {
        public MultiCrewThirdPersonPitchDownButton()
            : base("MultiCrewThirdPersonPitchDownButton", "MultiCrewThirdPersonPitchDownButton",
                () => EDBindings.Ship.MultiCrewThirdPersonPitchDownButton)
        {
        }
    }

    public class MultiCrewThirdPersonPitchUpButton : SimpleStandardBindingAction
    {
        public MultiCrewThirdPersonPitchUpButton()
            : base("MultiCrewThirdPersonPitchUpButton", "MultiCrewThirdPersonPitchUpButton",
                () => EDBindings.Ship.MultiCrewThirdPersonPitchUpButton)
        {
        }
    }

    public class MultiCrewThirdPersonYawLeftButton : SimpleStandardBindingAction
    {
        public MultiCrewThirdPersonYawLeftButton()
            : base("MultiCrewThirdPersonYawLeftButton", "MultiCrewThirdPersonYawLeftButton",
                () => EDBindings.Ship.MultiCrewThirdPersonYawLeftButton)
        {
        }
    }

    public class MultiCrewThirdPersonYawRightButton : SimpleStandardBindingAction
    {
        public MultiCrewThirdPersonYawRightButton()
            : base("MultiCrewThirdPersonYawRightButton", "MultiCrewThirdPersonYawRightButton",
                () => EDBindings.Ship.MultiCrewThirdPersonYawRightButton)
        {
        }
    }
    */

    public class MultiCrewToggleMode : SimpleStandardBindingAction
    {
        public MultiCrewToggleMode()
            : base("MultiCrew Mode - Toggle", "(ship) Toggle multicrew mode.",
                () => EDBindings.Ship.MultiCrewToggleMode)
        {
        }
    }

    public class NightVisionToggle_Context : ContextualStandardBindingAction
    {
        public NightVisionToggle_Context()
            : base("Night Vision - Toggle", "(ship, srv, foot) Toggle Night Vision")
        {
            AddContext("ship", () => EDBindings.Ship.NightVisionToggle);
            AddContext("srv", () => EDBindings.Ship.NightVisionToggle); // TODO: is not configurable vis SRV controls, does it use ship?
            AddContext("foot", () => EDBindings.Foot.HumanoidToggleNightVisionButton);
        }
    }

    /* TODO: is not in binds file. Is this exposed in the ED config if there is an Oculus headset?
    public class OculusReset : SimpleStandardBindingAction
    {
        public OculusReset()
            : base("OculusReset", "OculusReset",
                () => EDBindings.Ship.OculusReset)
        {
        }
    }
    */

    public class OpenCodexGoToDiscovery : SimpleStandardBindingAction
    {
        public OpenCodexGoToDiscovery()
            : base("Panel - Codex Discovery", "(ship) Open codex discovery panel.",
                () => EDBindings.Ship.OpenCodexGoToDiscovery)
        {
        }
    }

    public class OpenOrders : SimpleStandardBindingAction
    {
        public OpenOrders()
            : base("Panel - Fighter Orders", "(ship) Open fighter orders panel.",
                () => EDBindings.Ship.OpenOrders)
        {
        }
    }

    public class OrbitLinesToggle : SimpleStandardBindingAction
    {
        public OrbitLinesToggle()
            : base("Orbit Lines - Toggle", "(ship) Turn on/off orbit line display.",
                () => EDBindings.Ship.OrbitLinesToggle)
        {
        }
    }

    public class OrderAggressiveBehaviour : SimpleStandardBindingAction
    {
        public OrderAggressiveBehaviour()
            : base("Fighter Order - Engage At Will", "(ship) Order fighters to engage at will.",
                () => EDBindings.Ship.OrderAggressiveBehaviour)
        {
        }
    }

    public class OrderDefensiveBehaviour : SimpleStandardBindingAction
    {
        public OrderDefensiveBehaviour()
            : base("Fighter Order - Defend", "(ship) Order fighters to defend.",
                () => EDBindings.Ship.OrderDefensiveBehaviour)
        {
        }
    }

    public class OrderFocusTarget : SimpleStandardBindingAction
    {
        public OrderFocusTarget()
            : base("Fighter Order - Attack Target", "(ship) Order fighters to attack target.",
                () => EDBindings.Ship.OrderFocusTarget)
        {
        }
    }

    public class OrderFollow : SimpleStandardBindingAction
    {
        public OrderFollow()
            : base("Fighter Order - Follow Me", "(ship) Order fighters to follow ship.",
                () => EDBindings.Ship.OrderFollow)
        {
        }
    }

    public class OrderHoldFire : SimpleStandardBindingAction
    {
        public OrderHoldFire()
            : base("Fighter Order - Maintain Formation", "(ship) Order fighters to maintain formation.",
                () => EDBindings.Ship.OrderHoldFire)
        {
        }
    }

    public class OrderHoldPosition : SimpleStandardBindingAction
    {
        public OrderHoldPosition()
            : base("Fighter Order - Hold Position", "(ship) Order fighters to hold position.",
                () => EDBindings.Ship.OrderHoldPosition)
        {
        }
    }

    public class OrderRequestDock : SimpleStandardBindingAction
    {
        public OrderRequestDock()
            : base("Fighter Order - Recall", "(ship) Order fighters to dock.",
                () => EDBindings.Ship.OrderRequestDock)
        {
        }
    }

    /* TODO: what is this?
    public class Pause : SimpleStandardBindingAction
    {
        public Pause()
            : base("Pause", "Pause",
                () => EDBindings.Ship.Pause)
        {
        }
    }
    */

    public class PhotoCameraToggle_Context : ContextualStandardBindingAction
    {
        public PhotoCameraToggle_Context()
            : base("Camera Suite - Toggle", "Toggles camera suite.")
        {
            AddContext("ship", () => EDBindings.General.PhotoCameraToggle);
            AddContext("srv", () => EDBindings.General.PhotoCameraToggle_Buggy);
            AddContext("foot", () => EDBindings.General.PhotoCameraToggle_Humanoid);
        }
    }

    /*
    public class PitchCameraUp : SimpleStandardBindingAction
    {
        public PitchCameraUp()
            : base("PitchCameraUp", "PitchCameraUp",
                () => EDBindings.Ship.PitchCameraUp)
        {
        }
    }

    public class PitchDownButton : SimpleStandardBindingAction
    {
        public PitchDownButton()
            : base("PitchDownButton", "PitchDownButton",
                () => EDBindings.Ship.PitchDownButton)
        {
        }
    }

    public class PitchDownButton_Landing : SimpleStandardBindingAction
    {
        public PitchDownButton_Landing()
            : base("PitchDownButton_Landing", "PitchDownButton_Landing",
                () => EDBindings.Ship.PitchDownButton_Landing)
        {
        }
    }

    public class PitchUpButton : SimpleStandardBindingAction
    {
        public PitchUpButton()
            : base("PitchUpButton", "PitchUpButton",
                () => EDBindings.Ship.PitchUpButton)
        {
        }
    }

    public class PitchUpButton_Landing : SimpleStandardBindingAction
    {
        public PitchUpButton_Landing()
            : base("PitchUpButton_Landing", "PitchUpButton_Landing",
                () => EDBindings.Ship.PitchUpButton_Landing)
        {
        }
    }
    */

    public class PlayerHUDModeToggle : SimpleStandardBindingAction
    {
        public PlayerHUDModeToggle()
            : base("Cockpit Mode - Switch", "(ship) Switch cockpit between analysis and combat modes.",
                () => EDBindings.Ship.PlayerHUDModeToggle)
        {
        }
    }

    public class PlayerHUDModeAnalysis : SimpleStandardBindingAction
    {
        public PlayerHUDModeAnalysis()
            : base("Cockpit Mode - Analysis", "(ship) Switch cockpit to analysis mode.",
                () => getBool("ed_hud_in_analysis_mode") ? null : EDBindings.Ship.PlayerHUDModeToggle)
        {
        }
    }

    public class PlayerHUDModeCombat : SimpleStandardBindingAction
    {
        public PlayerHUDModeCombat()
            : base("Cockpit Mode - Combat", "(ship) Switch cockpit to combat mode.",
                () => getBool("ed_hud_in_analysis_mode") ? EDBindings.Ship.PlayerHUDModeToggle : null)
        {
        }
    }

    /*
    public class PrimaryFire : SimpleStandardBindingAction
    {
        public PrimaryFire()
            : base("PrimaryFire", "PrimaryFire",
                () => EDBindings.Ship.PrimaryFire)
        {
        }
    }
    */

    public class QuickCommsPanel : ContextualStandardBindingAction
    {
        public QuickCommsPanel()
            : base("Panel - Quick Comms", "(ship, srv, foot) Show quick comms panel.")
        {
            AddContext("ship", () => EDBindings.Ship.QuickCommsPanel);
            AddContext("srv", () => EDBindings.Ship.QuickCommsPanel_Buggy);
            AddContext("foot", () => EDBindings.Ship.QuickCommsPanel_Humanoid);
        }
    }

    public class QuitCamera : SimpleStandardBindingAction
    {
        public QuitCamera()
            : base("Camera - Exit", "",
                () => EDBindings.Ship.QuitCamera)
        {
        }
    }

    public class RadarDecreaseRange : SimpleStandardBindingAction
    {
        public RadarDecreaseRange()
            : base("Sensor Zoom - Decrease", "(ship)",
                () => EDBindings.Ship.RadarDecreaseRange)
        {
        }
    }

    public class RadarIncreaseRange : SimpleStandardBindingAction
    {
        public RadarIncreaseRange()
            : base("Sensor Zoom - Increase", "(ship)",
                () => EDBindings.Srv.RadarIncreaseRange)
        {
        }
    }

    public class RecallDismissShip : SimpleStandardBindingAction
    {
        public RecallDismissShip()
            : base("Ship - Recall/Dismiss", "(srv)",
                () => EDBindings.Srv.RecallDismissShip)
        {
        }
    }

    public class ResetPowerDistribution : ContextualStandardBindingAction
    {
        public ResetPowerDistribution()
            : base("Pips - Reset", "(ship, srv)")
        {
            AddContext("ship", () => EDBindings.Ship.ResetPowerDistribution);
            AddContext("srv", () => EDBindings.Srv.ResetPowerDistribution_Buggy);
        }
    }

    /*
    public class RightThrustButton : SimpleStandardBindingAction
    {
        public RightThrustButton()
            : base("RightThrustButton", "RightThrustButton",
                () => EDBindings.Ship.RightThrustButton)
        {
        }
    }

    public class RightThrustButton_Landing : SimpleStandardBindingAction
    {
        public RightThrustButton_Landing()
            : base("RightThrustButton_Landing", "RightThrustButton_Landing",
                () => EDBindings.Ship.RightThrustButton_Landing)
        {
        }
    }

    public class RollCameraLeft : SimpleStandardBindingAction
    {
        public RollCameraLeft()
            : base("RollCameraLeft", "RollCameraLeft",
                () => EDBindings.Ship.RollCameraLeft)
        {
        }
    }

    public class RollCameraRight : SimpleStandardBindingAction
    {
        public RollCameraRight()
            : base("RollCameraRight", "RollCameraRight",
                () => EDBindings.Ship.RollCameraRight)
        {
        }
    }

    public class RollLeftButton : SimpleStandardBindingAction
    {
        public RollLeftButton()
            : base("RollLeftButton", "RollLeftButton",
                () => EDBindings.Ship.RollLeftButton)
        {
        }
    }

    public class RollLeftButton_Landing : SimpleStandardBindingAction
    {
        public RollLeftButton_Landing()
            : base("RollLeftButton_Landing", "RollLeftButton_Landing",
                () => EDBindings.Ship.RollLeftButton_Landing)
        {
        }
    }

    public class RollRightButton : SimpleStandardBindingAction
    {
        public RollRightButton()
            : base("RollRightButton", "RollRightButton",
                () => EDBindings.Ship.RollRightButton)
        {
        }
    }

    public class RollRightButton_Landing : SimpleStandardBindingAction
    {
        public RollRightButton_Landing()
            : base("RollRightButton_Landing", "RollRightButton_Landing",
                () => EDBindings.Ship.RollRightButton_Landing)
        {
        }
    }

    public class SAAThirdPersonFovInButton : SimpleStandardBindingAction
    {
        public SAAThirdPersonFovInButton()
            : base("SAAThirdPersonFovInButton", "SAAThirdPersonFovInButton",
                () => EDBindings.Ship.SAAThirdPersonFovInButton)
        {
        }
    }

    public class SAAThirdPersonFovOutButton : SimpleStandardBindingAction
    {
        public SAAThirdPersonFovOutButton()
            : base("SAAThirdPersonFovOutButton", "SAAThirdPersonFovOutButton",
                () => EDBindings.Ship.SAAThirdPersonFovOutButton)
        {
        }
    }

    public class SAAThirdPersonPitchDownButton : SimpleStandardBindingAction
    {
        public SAAThirdPersonPitchDownButton()
            : base("SAAThirdPersonPitchDownButton", "SAAThirdPersonPitchDownButton",
                () => EDBindings.Ship.SAAThirdPersonPitchDownButton)
        {
        }
    }

    public class SAAThirdPersonPitchUpButton : SimpleStandardBindingAction
    {
        public SAAThirdPersonPitchUpButton()
            : base("SAAThirdPersonPitchUpButton", "SAAThirdPersonPitchUpButton",
                () => EDBindings.Ship.SAAThirdPersonPitchUpButton)
        {
        }
    }

    public class SAAThirdPersonYawLeftButton : SimpleStandardBindingAction
    {
        public SAAThirdPersonYawLeftButton()
            : base("SAAThirdPersonYawLeftButton", "SAAThirdPersonYawLeftButton",
                () => EDBindings.Ship.SAAThirdPersonYawLeftButton)
        {
        }
    }

    public class SAAThirdPersonYawRightButton : SimpleStandardBindingAction
    {
        public SAAThirdPersonYawRightButton()
            : base("SAAThirdPersonYawRightButton", "SAAThirdPersonYawRightButton",
                () => EDBindings.Ship.SAAThirdPersonYawRightButton)
        {
        }
    }

    public class SecondaryFire : SimpleStandardBindingAction
    {
        public SecondaryFire()
            : base("SecondaryFire", "SecondaryFire",
                () => EDBindings.Ship.SecondaryFire)
        {
        }
    }
    */

    public class SelectHighestThreat : SimpleStandardBindingAction
    {
        public SelectHighestThreat()
            : base("Target Hostile - Highest", "(ship)",
                () => EDBindings.Ship.SelectHighestThreat)
        {
        }
    }

    public class SelectTarget : SimpleStandardBindingAction
    {
        public SelectTarget()
            : base("Target Ahead", "(ship, srv)", context => 
                context.Ship ? EDBindings.Ship.SelectTarget :
                context.Srv ? EDBindings.Srv.SelectTarget_Buggy :
                null)
        {
        }
    }

    public class SelectTargetsTarget : SimpleStandardBindingAction
    {
        public SelectTargetsTarget()
            : base("Target Target", "(ship) Select targeted teammate's target.",
                () => EDBindings.Ship.SelectTargetsTarget)
        {
        }
    }

    public class SetSpeed100 : SimpleStandardBindingAction
    {
        public SetSpeed100()
            : base("Thrust - Forward 100%", "(ship)",
                () => EDBindings.Ship.SetSpeed100)
        {
        }
    }

    public class SetSpeed25 : SimpleStandardBindingAction
    {
        public SetSpeed25()
            : base("Thrust - Forward 25%", "(ship)",
                () => EDBindings.Ship.SetSpeed25)
        {
        }
    }

    public class SetSpeed50 : SimpleStandardBindingAction
    {
        public SetSpeed50()
            : base("Thrust - Forward 50%", "(ship)",
                () => EDBindings.Ship.SetSpeed50)
        {
        }
    }

    public class SetSpeed75 : SimpleStandardBindingAction
    {
        public SetSpeed75()
            : base("Thrust - Forward 75%", "(ship)",
                () => EDBindings.Ship.SetSpeed75)
        {
        }
    }

    public class SetSpeedMinus100 : SimpleStandardBindingAction
    {
        public SetSpeedMinus100()
            : base("Thrust - Reverse 100%", "(ship)",
                () => EDBindings.Ship.SetSpeedMinus100)
        {
        }
    }

    public class SetSpeedMinus25 : SimpleStandardBindingAction
    {
        public SetSpeedMinus25()
            : base("Thrust - Reverse 25%", "(ship)",
                () => EDBindings.Ship.SetSpeedMinus25)
        {
        }
    }

    public class SetSpeedMinus50 : SimpleStandardBindingAction
    {
        public SetSpeedMinus50()
            : base("Thrust - Reverse 50%", "(ship)",
                () => EDBindings.Ship.SetSpeedMinus50)
        {
        }
    }

    public class SetSpeedMinus75 : SimpleStandardBindingAction
    {
        public SetSpeedMinus75()
            : base("Thrust - Reverse 75%", "(ship)",
                () => EDBindings.Ship.SetSpeedMinus75)
        {
        }
    }

    public class SetSpeedZero : SimpleStandardBindingAction
    {
        public SetSpeedZero()
            : base("Thrust - None (0%)", "(ship)",
                () => EDBindings.Ship.SetSpeedZero)
        {
        }
    }

    /*
    public class SteerLeftButton : SimpleStandardBindingAction
    {
        public SteerLeftButton()
            : base("SteerLeftButton", "SteerLeftButton",
                () => EDBindings.Ship.SteerLeftButton)
        {
        }
    }

    public class SteerRightButton : SimpleStandardBindingAction
    {
        public SteerRightButton()
            : base("SteerRightButton", "SteerRightButton",
                () => EDBindings.Ship.SteerRightButton)
        {
        }
    }

    public class StoreCamZoomIn : SimpleStandardBindingAction
    {
        public StoreCamZoomIn()
            : base("StoreCamZoomIn", "StoreCamZoomIn",
                () => EDBindings.Ship.StoreCamZoomIn)
        {
        }
    }

    public class StoreCamZoomOut : SimpleStandardBindingAction
    {
        public StoreCamZoomOut()
            : base("StoreCamZoomOut", "StoreCamZoomOut",
                () => EDBindings.Ship.StoreCamZoomOut)
        {
        }
    }

    public class StoreEnableRotation : SimpleStandardBindingAction
    {
        public StoreEnableRotation()
            : base("StoreEnableRotation", "StoreEnableRotation",
                () => EDBindings.Ship.StoreEnableRotation)
        {
        }
    }

    public class StorePitchCameraDown : SimpleStandardBindingAction
    {
        public StorePitchCameraDown()
            : base("StorePitchCameraDown", "StorePitchCameraDown",
                () => EDBindings.Ship.StorePitchCameraDown)
        {
        }
    }

    public class StorePitchCameraUp : SimpleStandardBindingAction
    {
        public StorePitchCameraUp()
            : base("StorePitchCameraUp", "StorePitchCameraUp",
                () => EDBindings.Ship.StorePitchCameraUp)
        {
        }
    }

    public class StoreToggle : SimpleStandardBindingAction
    {
        public StoreToggle()
            : base("StoreToggle", "StoreToggle",
                () => EDBindings.Ship.StoreToggle)
        {
        }
    }

    public class StoreYawCameraLeft : SimpleStandardBindingAction
    {
        public StoreYawCameraLeft()
            : base("StoreYawCameraLeft", "StoreYawCameraLeft",
                () => EDBindings.Ship.StoreYawCameraLeft)
        {
        }
    }

    public class StoreYawCameraRight : SimpleStandardBindingAction
    {
        public StoreYawCameraRight()
            : base("StoreYawCameraRight", "StoreYawCameraRight",
                () => EDBindings.Ship.StoreYawCameraRight)
        {
        }
    }
    */

    public class Supercruise : SimpleStandardBindingAction
    {
        public Supercruise()
            : base("FSD - Supercruise", "(ship)",
                () => EDBindings.Ship.Supercruise)
        {
        }
    }

    public class SystemMapOpen : SimpleStandardBindingAction
    {
        public SystemMapOpen()
            : base("Panel - System Map", "SystemMapOpen",
                context => 
                    context.Ship ? EDBindings.Ship.SystemMapOpen :
                    context.Srv ? EDBindings.Ship.SystemMapOpen_Buggy :
                    context.Foot ? EDBindings.Ship.SystemMapOpen_Humanoid :
                    null)
        {
        }
    }

    public class TargetNextRouteSystem : SimpleStandardBindingAction
    {
        public TargetNextRouteSystem()
            : base("Target Route", "(ship) Target next system in route.",
                () => EDBindings.Ship.TargetNextRouteSystem)
        {
        }
    }

    public class TargetWingman0 : SimpleStandardBindingAction
    {
        public TargetWingman0()
            : base("Target Teammate - 1", "(ship)",
                () => EDBindings.Ship.TargetWingman0)
        {
        }
    }

    public class TargetWingman1 : SimpleStandardBindingAction
    {
        public TargetWingman1()
            : base("Target Teammate - 2", "(ship)",
                () => EDBindings.Ship.TargetWingman1)
        {
        }
    }

    public class TargetWingman2 : SimpleStandardBindingAction
    {
        public TargetWingman2()
            : base("Target Teammate - 3", "(ship)",
                () => EDBindings.Ship.TargetWingman2)
        {
        }
    }

    /* TODO: what is this?
    public class ToggleAdvanceMode : SimpleStandardBindingAction
    {
        public ToggleAdvanceMode()
            : base("ToggleAdvanceMode", "ToggleAdvanceMode",
                () => EDBindings.Ship.ToggleAdvanceMode)
        {
        }
    }
    */

    public class ToggleBuggyTurretButton : SimpleStandardBindingAction
    {
        public ToggleBuggyTurretButton()
            : base("Turret - Toggle", "(srv)",
                () => EDBindings.Srv.ToggleBuggyTurretButton)
        {
        }
    }

    public class ToggleFreeCam : SimpleStandardBindingAction
    {
        public ToggleFreeCam()
            : base("Camera - Free", "", // TODO srv? foot?
                () => EDBindings.General.ToggleFreeCam)
        {
        }
    }

    public class ToggleRotationLock : SimpleStandardBindingAction
    {
        public ToggleRotationLock()
            : base("Camera Stabilizer - Toggle", "(free camera)",
                () => EDBindings.General.ToggleRotationLock)
        {
        }
    }

    public class UIFocus : SimpleStandardBindingAction
    {
        public UIFocus()
            : base("UI Focus", "(ship, srv) Note: UI FOCUS MODE must be set to CYCLE.",
                context => 
                    context.Ship ? EDBindings.Ship.UIFocus :
                    context.Srv ? EDBindings.Srv.UIFocus_Buggy :
                    null)
        {
        }
    }

    public class UI_Back : SimpleStandardBindingAction
    {
        public UI_Back()
            : base("UI - Back", "",
                () => EDBindings.General.UI_Back)
        {
        }
    }

    public class UI_Down : SimpleStandardBindingAction
    {
        public UI_Down()
            : base("UI - Down", "",
                () => EDBindings.Ship.UI_Down)
        {
        }
    }

    public class UI_Left : SimpleStandardBindingAction
    {
        public UI_Left()
            : base("UI - Left", "",
                () => EDBindings.Ship.UI_Left)
        {
        }
    }

    public class UI_Right : SimpleStandardBindingAction
    {
        public UI_Right()
            : base("UI - Right", "",
                () => EDBindings.Ship.UI_Right)
        {
        }
    }

    public class UI_Select : SimpleStandardBindingAction
    {
        public UI_Select()
            : base("UI - Select", "",
                () => EDBindings.Ship.UI_Select)
        {
        }
    }

    public class UI_Toggle : SimpleStandardBindingAction
    {
        public UI_Toggle()
            : base("UI - Toggle", "",
                () => EDBindings.Ship.UI_Toggle)
        {
        }
    }

    public class UI_Up : SimpleStandardBindingAction
    {
        public UI_Up()
            : base("UI - Up", "",
                () => EDBindings.Ship.UI_Up)
        {
        }
    }

    /*
    public class UpThrustButton : SimpleStandardBindingAction
    {
        public UpThrustButton()
            : base("UpThrustButton", "UpThrustButton",
                () => EDBindings.Ship.UpThrustButton)
        {
        }
    }

    public class UpThrustButton_Landing : SimpleStandardBindingAction
    {
        public UpThrustButton_Landing()
            : base("UpThrustButton_Landing", "UpThrustButton_Landing",
                () => EDBindings.Ship.UpThrustButton_Landing)
        {
        }
    }

    */

    public class UseBoostJuice : SimpleStandardBindingAction
    {
        public UseBoostJuice()
            : base("Engine Boost - Use", "(ship)", // TODO: does this require hold?
                () => EDBindings.Ship.UseBoostJuice)
        {
        }
    }

    public class UseShieldCell : SimpleStandardBindingAction
    {
        public UseShieldCell()
            : base("Shield Cell - Use", "(ship)",
                () => EDBindings.Ship.UseShieldCell)
        {
        }
    }

    public class VanityCameraOne : SimpleStandardBindingAction
    {
        public VanityCameraOne()
            : base("Camera Select - Cockpit Front", "", // TODO: srv and foot too?
                () => EDBindings.General.VanityCameraOne)
        {
        }
    }

    public class VanityCameraTwo : SimpleStandardBindingAction
    {
        public VanityCameraTwo()
            : base("Camera Select - Cockpit Back", "",
                () => EDBindings.General.VanityCameraTwo)
        {
        }
    }

    public class VanityCameraThree : SimpleStandardBindingAction
    {
        public VanityCameraThree()
            : base("Camera Select - Commander 1", "",
                () => EDBindings.General.VanityCameraThree)
        {
        }
    }

    public class VanityCameraFour : SimpleStandardBindingAction
    {
        public VanityCameraFour()
            : base("Camera Select - Commander 2", "",
                () => EDBindings.General.VanityCameraFour)
        {
        }
    }

    public class VanityCameraFive : SimpleStandardBindingAction
    {
        public VanityCameraFive()
            : base("Camera Select - Copilot 1", "",
                () => EDBindings.General.VanityCameraFive)
        {
        }
    }

    public class VanityCameraSix : SimpleStandardBindingAction
    {
        public VanityCameraSix()
            : base("Camera Select - Copilot 2", "",
                () => EDBindings.General.VanityCameraSix)
        {
        }
    }

    public class VanityCameraSeven : SimpleStandardBindingAction
    {
        public VanityCameraSeven()
            : base("Camera Select - Copilot 3", "",
                () => EDBindings.General.VanityCameraSeven)
        {
        }
    }

    public class VanityCameraEight : SimpleStandardBindingAction
    {
        public VanityCameraEight()
            : base("Camera Select - Front", "",
                () => EDBindings.General.VanityCameraEight)
        {
        }
    }

    public class VanityCameraNine : SimpleStandardBindingAction
    {
        public VanityCameraNine()
            : base("Camera Select - Back", "",
                () => EDBindings.General.VanityCameraNine)
        {
        }
    }

    public class VanityCameraTen : SimpleStandardBindingAction
    {
        public VanityCameraTen()
            : base("Camera Select - Back (Again)", "", // TODO ???
                () => EDBindings.General.VanityCameraTen)
        {
        }
    }

    /*
    public class VanityCameraScrollLeft : SimpleStandardBindingAction
    {
        public VanityCameraScrollLeft()
            : base("VanityCameraScrollLeft", "VanityCameraScrollLeft",
                () => EDBindings.Ship.VanityCameraScrollLeft)
        {
        }
    }

    public class VanityCameraScrollRight : SimpleStandardBindingAction
    {
        public VanityCameraScrollRight()
            : base("VanityCameraScrollRight", "VanityCameraScrollRight",
                () => EDBindings.Ship.VanityCameraScrollRight)
        {
        }
    }
    */

    public class WeaponColourToggle : SimpleStandardBindingAction
    {
        public WeaponColourToggle()
            : base("Weapon Colour - Toggle", "(ship)",
                () => EDBindings.Ship.WeaponColourToggle)
        {
        }
    }

    public class WingNavLock : SimpleStandardBindingAction
    {
        public WingNavLock()
            : base("Teammate - Nav-Lock", "Hyperspace jump or supercruse with selected teammate.",
                () => EDBindings.Ship.WingNavLock)
        {
        }
    }

    /*
    public class YawCameraLeft : SimpleStandardBindingAction
    {
        public YawCameraLeft()
            : base("YawCameraLeft", "YawCameraLeft",
                () => EDBindings.Ship.YawCameraLeft)
        {
        }
    }

    public class YawCameraRight : SimpleStandardBindingAction
    {
        public YawCameraRight()
            : base("YawCameraRight", "YawCameraRight",
                () => EDBindings.Ship.YawCameraRight)
        {
        }
    }

    public class YawLeftButton : SimpleStandardBindingAction
    {
        public YawLeftButton()
            : base("YawLeftButton", "YawLeftButton",
                () => EDBindings.Ship.YawLeftButton)
        {
        }
    }

    public class YawLeftButton_Landing : SimpleStandardBindingAction
    {
        public YawLeftButton_Landing()
            : base("YawLeftButton_Landing", "YawLeftButton_Landing",
                () => EDBindings.Ship.YawLeftButton_Landing)
        {
        }
    }

    public class YawRightButton : SimpleStandardBindingAction
    {
        public YawRightButton()
            : base("YawRightButton", "YawRightButton",
                () => EDBindings.Ship.YawRightButton)
        {
        }
    }

    public class YawRightButton_Landing : SimpleStandardBindingAction
    {
        public YawRightButton_Landing()
            : base("YawRightButton_Landing", "YawRightButton_Landing",
                () => EDBindings.Ship.YawRightButton_Landing)
        {
        }
    }
    */

    internal class BindingActionManager
    {

        private static readonly HashSet<Type> _baseBindingActionTypes = new HashSet<Type>
        {
            typeof(BindingAction),
            typeof(StandardBindingAction),
            typeof(SimpleStandardBindingAction),
            typeof(ContextualStandardBindingAction)
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
                    .OrderBy(i => i.Name, new StrCmpLogicalComparer()) // Macro Deck does not do this for you...
            );

        }

    }

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
