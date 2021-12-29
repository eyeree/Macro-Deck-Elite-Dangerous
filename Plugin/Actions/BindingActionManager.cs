using EliteDangerousMacroDeckPlugin.Actions.Bindings;
using SuchByte.MacroDeck.ActionButton;
using SuchByte.MacroDeck.Plugins;
using System;
using System.Collections.Generic;
using System.Text;

namespace EliteDangerousMacroDeckPlugin.Actions
{

    internal class StandardBindingAction : PluginAction
    {

        public override string Name => _name;
        public override string Description => _description;

        private readonly Func<string, ActionButton, StandardBindingInfo> _getBinding;
        private readonly string _name;
        private readonly string _description;

        public StandardBindingAction(string name, string description, Func<StandardBindingInfo> getBinding)
            : this(name, description, (clientId, actionButton) => getBinding())
        {
        }

        public StandardBindingAction(string name, string description, Func<ActionButton, StandardBindingInfo> getBinding)
            : this(name, description, (clientId, actionButton) => getBinding(actionButton))
        {
        }

        public StandardBindingAction(string name, string description, Func<string, ActionButton, StandardBindingInfo> getBinding)
        {
            _name = name;
            _description = description;
            _getBinding = getBinding;
        }

        public override void Trigger(string clientId, ActionButton actionButton)
        {
            var bindingInfo = _getBinding(clientId, actionButton);
        }

    }

    internal class BindingActionManager
    {

        private readonly EliteDangerousMacroDeckPlugin _plugin;
        private readonly Bindings.BindingsManager _bindings;

        public BindingActionManager(EliteDangerousMacroDeckPlugin plugin)
        {

            _plugin = plugin;
            _bindings = new Bindings.BindingsManager();

            // SHIP

            add("ship_landing_gear_toggle", "Raise or Lower Ship Landing Gear", 
                () => _bindings.Ship.LandingGearToggle
            );

            add("ship_lights_toggle", "Turn On or Off Ship Lights",
                () => _bindings.Ship.ShipSpotLightToggle
            );

            add("ship_orbit_lines_toggle", "Turn On or Off Ship Orbit Lines",
                () => _bindings.Ship.OrbitLinesToggle
            );

            // SRV

            add("srv_lights_toggle", "Turn On or Off SRV Lights",
                () => _bindings.Srv.HeadlightsBuggyButton
            );

            // ANY

            add("current_lights_toggle", "Turn On or Off Ship, Srv, or Backback Lights",
                () => getBool("is_main_ship") ? _bindings.Ship.ShipSpotLightToggle :
                      getBool("is_srv") ? _bindings.Srv.HeadlightsBuggyButton :
                      getBool("on_foot") ? _bindings.Foot.HumanoidToggleFlashlightButton :
                      null
            );


        }

        private void add(PluginAction action)
        {
            _plugin.Actions.Add(action);
        }

        private void add(string name, string description, Func<StandardBindingInfo> getBinding)
        {
            add(new StandardBindingAction("ed_" + name, description, getBinding));
        }

        bool getBool(string variableName)
        {
            return VariableCache.getBool(variableName);
        }

    }
}
