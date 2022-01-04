using EliteDangerousMacroDeckPlugin.Actions.Bindings;
using SuchByte.MacroDeck.ActionButton;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace EliteDangerousMacroDeckPlugin.Actions
{

    public class Context
    {
        private readonly string context = VariableCache.GetString("ed_context");
        public bool Ship { get { return context == "ship"; } }
        public bool Srv { get { return context == "ship"; } }
        public bool Foot { get { return context == "ship"; } }
    }

    public delegate StandardBindingInfo GetBindingFunc();
    public delegate StandardBindingInfo GetBindingFuncWithContext(Context context);

    public abstract class StandardBindingAction : BindingAction
    {

        private readonly GetBindingFunc _getBinding;

        public StandardBindingAction(string name, string description, GetBindingFunc getBinding) : base(name, description)
        {
            _getBinding = getBinding;
        }

        public StandardBindingAction(string name, string description, GetBindingFuncWithContext getBinding)
            : this(name, description, () => getBinding(new Context()))
        {
        }

        public override void Trigger(string clientId, ActionButton actionButton)
        {

            var binding = _getBinding();
            if (binding == null)
            {
                Trace.TraceInformation($"Action {Name} triggered - no binding");
                return;
            }

            var keyboardBinding = GetKeyboardBinding(binding);
            if (keyboardBinding == null)
            {
                Trace.TraceInformation($"Action {Name} triggered - no keyboard binding");
                return;
            }

            Trace.TraceInformation($"Action {Name} triggered");

            SendKeyStrokes(keyboardBinding);

        }

        private BindingInfo GetKeyboardBinding(StandardBindingInfo binding)
        {
            if (binding.Primary?.Device?.Equals("Keyboard") == true)
            {
                return binding.Primary;
            }
            if (binding.Primary?.Device?.Equals("Keyboard") == true)
            {
                return binding.Secondary;
            }
            return null;
        }

    }

}
