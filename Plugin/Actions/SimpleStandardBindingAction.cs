﻿using EliteDangerousMacroDeckPlugin.Actions.Bindings;
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
    public delegate StandardBindingInfo ContextualGetBindingFunc(Context context);

    public abstract class StandardBindingAction : BindingAction
    {

        public StandardBindingAction(string name, string description) : base(name, description)
        {
        }

        protected abstract StandardBindingInfo GetBinding();

        public override void Trigger(string clientId, ActionButton actionButton)
        {

            var binding = GetBinding();
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

    public class SimpleStandardBindingAction : StandardBindingAction
    {

        private readonly GetBindingFunc _getBinding;

        public SimpleStandardBindingAction(string name, string description, GetBindingFunc getBinding) : base(name, description)
        {
            _getBinding = getBinding;
        }

        public SimpleStandardBindingAction(string name, string description, ContextualGetBindingFunc getBinding) 
            : this(name, description, () => getBinding(new Context()))
        {
        }

        protected override StandardBindingInfo GetBinding()
        {
            return _getBinding();
        }

    }

    public class ContextualStandardBindingAction : StandardBindingAction
    {

        private readonly Dictionary<string, GetBindingFunc> _getBindings = new Dictionary<string, GetBindingFunc>();

        public ContextualStandardBindingAction(string name, string description)
            : base(name, description)
        {
        }

        public void AddContext(string context, GetBindingFunc getBinding)
        {
            _getBindings.Add(context, getBinding);
        }

        protected override StandardBindingInfo GetBinding()
        {
            var context = VariableCache.GetString("ed_context");
            _getBindings.TryGetValue(context, out var getBinding);
            return getBinding == null ? null : getBinding();
        }

    }

}
