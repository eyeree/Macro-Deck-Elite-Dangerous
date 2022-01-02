using EliteDangerousMacroDeckPlugin.Actions.Bindings;
using SuchByte.MacroDeck.Plugins;
using System.Collections.Generic;

namespace EliteDangerousMacroDeckPlugin.Actions
{

    // Macro Deck serializes all actions to XML and back when they are loaded. So we need
    // an unique type for each one. Otherwise we could just create instances with different
    // name, description, and getBindings values.

    // SHIP

    public class ShipLandingGearToggle : SimpleStandardBindingAction
    {
        public ShipLandingGearToggle() 
            : base("Ship Landing Gear Toggle", "Raise or Lower Ship Landing Gear", 
                  () => EDBindings.Ship.LandingGearToggle)
        {
        }
    }

    public class ShipLightsToggle : SimpleStandardBindingAction
    {
        public ShipLightsToggle()
            : base("Ship Lights Toggle", "Turn On or Off Ship Spot Lights",
                () => EDBindings.Ship.ShipSpotLightToggle)
        {
        }
    }

    public class ShipOrbitLinesToggle : SimpleStandardBindingAction
    {
        public ShipOrbitLinesToggle()
            : base("Ship Orbit Lines Toggle", "Turn On or Off Ship Orbit Lines",
                () => EDBindings.Ship.OrbitLinesToggle)
        {
        }

    }

    // SRV

    public class SrvLightsToggle : SimpleStandardBindingAction
    {
        public SrvLightsToggle()
            : base("SRV Lights Toggle", "Turn On or Off SRV Headlights",
                () => EDBindings.Srv.HeadlightsBuggyButton)
        {
        }
    }

    // FOOT

    public class FootLightsToggle : SimpleStandardBindingAction
    {

        public FootLightsToggle()
            : base("Foot Lights Toggle", "Turn On or Off Flashlight.",
                () => EDBindings.Foot.HumanoidToggleFlashlightButton)
        {
        }

    }

    // CURRENT

    public class CurrentLightsToggle : ContextualStandardBindingAction
    {

        public CurrentLightsToggle()
            : base("Current Lights Toggle", "Turn On or Off Ship, SRV, or Backpack Lights")
        {
            AddContext("ship", () => EDBindings.Ship.ShipSpotLightToggle);
            AddContext("srv", () => EDBindings.Srv.HeadlightsBuggyButton);
            AddContext("foot", () => EDBindings.Foot.HumanoidToggleFlashlightButton);
        }

    }

    internal class BindingActionManager
    {

        private readonly EliteDangerousMacroDeckPlugin _plugin;

        public BindingActionManager(EliteDangerousMacroDeckPlugin plugin)
        {

            _plugin = plugin;

            // SHIP

            Add<ShipLandingGearToggle>();
            Add<ShipLightsToggle>();
            Add<ShipOrbitLinesToggle>();

            // SRV

            Add<SrvLightsToggle>();

            // FOOT

            Add<FootLightsToggle>();

            // CURRENT

            Add<CurrentLightsToggle>();

        }

        private void Add<T>() where T : PluginAction, new()
        {
            _plugin.Actions.Add(new T());
        }

    }
}
