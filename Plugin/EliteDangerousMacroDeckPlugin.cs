using EliteDangerousMacroDeckPlugin.Actions;
using EliteDangerousMacroDeckPlugin.Variables;
using SuchByte.MacroDeck.Plugins;
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

        public override string Description => "Elite Dangerous Variables and Actions";

        public override Image Icon => Properties.Resources.elite_dangerous_logo;


        public override bool CanConfigure => false;

        public string JournalPath { get; private set; }

        private StatusVariableManager _statusVariableManager;
        private JournalVariableManager _journalVariableManager;

        private BindingActionManager _bindingActionManager;

        // Gets called when the plugin is loaded
        public override void Enable()
        {

            JournalPath = GetJournalPath();

            this.Actions = new List<PluginAction>();
            
            _bindingActionManager = new BindingActionManager(this);

            _statusVariableManager = new StatusVariableManager(this);
            //_journalVariableManager = new JournalVariableManager(this);

        }

        private string GetJournalPath()
        {

            int result = UnsafeNativeMethods.SHGetKnownFolderPath(new Guid("4C5C32FF-BB9D-43B0-B5B4-2D72E54EAAA4"), 0, new IntPtr(0), out IntPtr pathPtr);
            if (result != 0)
            {
                throw new InvalidOperationException("Elite Dangerous Journal Path Not Found");
            }

            var path = Marshal.PtrToStringUni(pathPtr) + @"\Frontier Developments\Elite Dangerous";
            if(!Directory.Exists(path))
            {
                throw new InvalidOperationException("Elite Dangerous Journal Path Does Not Exist: " + path);
            }

            return path;

        }

        private class UnsafeNativeMethods
        {
            [DllImport("Shell32.dll")]
            public static extern int SHGetKnownFolderPath([MarshalAs(UnmanagedType.LPStruct)] Guid rfid, uint dwFlags, IntPtr hToken, out IntPtr ppszPath);
        }

    }


}