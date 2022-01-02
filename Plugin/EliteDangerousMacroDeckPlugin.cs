using EliteDangerousMacroDeckPlugin.Actions;
using EliteDangerousMacroDeckPlugin.Variables;
using SuchByte.MacroDeck.Plugins;
using SuchByte.MacroDeck.Profiles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace EliteDangerousMacroDeckPlugin
{

    public class EliteDangerousMacroDeckPlugin : MacroDeckPlugin
    {

        public override string Name => "Elite Dangerous";
        public override string Description => "Provides Variables and Actions for Elite Dangerous";
        public override string Author => "eyeree";

        public override Image Icon => Properties.Resources.elite_dangerous_logo;

        public override bool CanConfigure => false;

        private StatusVariableManager _statusVariableManager;

        private BindingActionManager _bindingActionManager;

        // Gets called when the plugin is loaded
        public override void Enable()
        {

            this.Actions = new List<PluginAction>();
            
            _bindingActionManager = new BindingActionManager(this);

            _statusVariableManager = new StatusVariableManager(this);

        }


    }


}