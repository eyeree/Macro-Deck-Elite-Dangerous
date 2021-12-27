using EliteJournalReader;
using EliteJournalReader.Events;
using SuchByte.MacroDeck.Plugins;
using SuchByte.MacroDeck.Variables;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace EliteDangerousMacroDeckPlugin
{

    public class VariableManager
    {

        private readonly Dictionary<string, object> _values = new Dictionary<string, object>();
        private readonly MacroDeckPlugin _plugin;
        private readonly string _prefix;

        protected VariableManager(MacroDeckPlugin plugin, string prefix)
        {
            _plugin = plugin;
            _prefix = prefix + "_";
        }

        private void _set(string name, object value, VariableType type, bool save)
        {
            var found = _values.TryGetValue(name, out object current);
            if (!found || (current != null && !current.Equals(value)) || (current == null && value != null))  
            {
                _values[name] = value;
                var full_name = _prefix + name;
                Debug.WriteLine("Setting variable {0} to {1}.", full_name, value);
                SuchByte.MacroDeck.Variables.VariableManager.SetValue(full_name, value, type, _plugin, save);
            }
        }

        protected void Set(string value, string name, bool save = false)
        {
            
            if(value == null)
            {
                value = "";
            }

            _set(name, value, VariableType.String, save);

        }

        protected void Set(int value, string name, bool save = false)
        {
            _set(name, value, VariableType.Integer, save);
        }

        protected void Set(float value, string name, bool save = false)
        {
            _set(name, value, VariableType.Float, save);
        }

        protected void Set(double value, string name, bool save = false)
        {
            _set(name, (float)value, VariableType.Float, save);
        }

        protected void Set(bool value, string name, bool save = false)
        {
            _set(name, value, VariableType.Bool, save);
        }

        protected void Set<Flags>(Flags flags, Flags flagsSet, string name, bool save = false) where Flags : System.Enum
        {
            ulong bits = Convert.ToUInt64(flags);
            ulong bitsSet = Convert.ToUInt64(flagsSet);
            Set((bits & bitsSet) == bitsSet, name, save);
        }

        protected void Set<Flags>(Flags flags, Flags flagsSet, Flags flagsClear, string name, bool save = false) where Flags : System.Enum
        {
            ulong bits = Convert.ToUInt64(flags);
            ulong bitsSet = Convert.ToUInt64(flagsSet);
            ulong bitsClear = Convert.ToUInt64(flagsClear);
            Set(((bits & bitsSet) == bitsSet) && (bits & bitsClear) == 0, name, save);
        }

        protected void Save(string value, string name)
        {
            Set(value, name, true);
        }


        protected void Save(int value, string name)
        {
            Set(value, name, true);
        }

        protected void Save(float value, string name)
        {
            Set(value, name, true);
        }

        protected void Save(double value, string name)
        {
            Set(value, name, true);
        }

        protected void Save(bool value, string name)
        {
            Set(value, name, true);
        }

        protected void Save<Flags>(Flags flags, Flags flagsSet, string name) where Flags : System.Enum
        {
            Set(flags, flagsSet, name, true);
        }

        protected void Save<Flags>(Flags flags, Flags flagsSet, Flags flagsClear, string name) where Flags : System.Enum
        {
            Set(flags, flagsSet, flagsClear, name, true);
        }

    }

    public class StatusManager : VariableManager
    {

        private StatusWatcher _statusWatcher;

        public StatusManager(EliteDangerousMacroDeckPlugin plugin) : base(plugin, "ed_status")
        {
            _statusWatcher = new StatusWatcher(plugin.JournalPath);
            _statusWatcher.StatusUpdated += handleStatusEvents;
            _statusWatcher.StartWatching();
        }

        private void handleStatusEvents(object sender, StatusFileEvent evt)
        {
            Set(evt.Altitude, "altitude");
            Set(evt.Balance, "balance");
            Set(evt.BodyName, "body_name");
            Set(evt.Cargo, "cargo");
            Set(evt.Destination.Body, "destination_body");
            Set(evt.Destination.Name, "destination_name");
            Set(evt.Destination.System, "destination_system");
            Set(evt.Firegroup, "firegroup");
            Set(evt.Flags, StatusFlags.AltitudeFromAverageRadius, "altitude_from_average_radius");
            Set(evt.Flags, StatusFlags.BeingInterdicted, "being_interdicted");
            Set(evt.Flags, StatusFlags.CargoScoopDeployed, "cargo_scoop_deployed");
            Set(evt.Flags, StatusFlags.Docked, "docked");
            Set(evt.Flags, StatusFlags.FlightAssistOff, "flight_assist_off");
            Set(evt.Flags, StatusFlags.FsdCharging, "fsd_charging");
            Set(evt.Flags, StatusFlags.FsdCooldown, "fsd_cooldown");
            Set(evt.Flags, StatusFlags.FsdJump, "fsd_jump");
            Set(evt.Flags, StatusFlags.FsdMassLocked, "fsd_mass_locked");
            Set(evt.Flags, StatusFlags.HardpointsDeployed, StatusFlags.Supercruise & StatusFlags.FsdJump, "hardpoints_deployed");
            Set(evt.Flags, StatusFlags.HasLatLong, "has_lat_long");
            Set(evt.Flags, StatusFlags.HudInAnalysisMode, "hud_in_analysis_mode");
            Set(evt.Flags, StatusFlags.InFighter, "in_fighter");
            Set(evt.Flags, StatusFlags.InMainShip, "in_main_ship");
            Set(evt.Flags, StatusFlags.InSRV, "in_srv");
            Set(evt.Flags, StatusFlags.InWing, "in_wing");
            Set(evt.Flags, StatusFlags.IsInDanger, "is_in_danger");
            Set(evt.Flags, StatusFlags.Landed, "landed");
            Set(evt.Flags, StatusFlags.LandingGearDown, "landing_gear_down");
            Set(evt.Flags, StatusFlags.LightsOn, "lights_on");
            Set(evt.Flags, StatusFlags.LowFuel, "low_fuel");
            Set(evt.Flags, StatusFlags.NightVision, "night_vision");
            Set(evt.Flags, StatusFlags.Overheating, "overheating");
            Set(evt.Flags, StatusFlags.ScoopingFuel, "scooping_fuel");
            Set(evt.Flags, StatusFlags.ShieldsUp, "shields_up");
            Set(evt.Flags, StatusFlags.SilentRunning, "silent_running");
            Set(evt.Flags, StatusFlags.SrvDriveAssist & StatusFlags.InSRV, "srv_drive_assist");
            Set(evt.Flags, StatusFlags.SrvHandbrake & StatusFlags.InSRV, "srv_handbrake");
            Set(evt.Flags, StatusFlags.SrvHighBeam & StatusFlags.InSRV, "srv_high_beam");
            Set(evt.Flags, StatusFlags.SrvTurret & StatusFlags.InSRV, "srv_turret");
            Set(evt.Flags, StatusFlags.SrvUnderShip & StatusFlags.InSRV, "srv_under_ship");
            Set(evt.Flags, StatusFlags.Supercruise, "supercruise");
            Set(evt.Flags2, MoreStatusFlags.AimDownSight, "aim_down_sight");
            Set(evt.Flags2, MoreStatusFlags.BreathableAtmosphere, "breathable_atmosphere");
            Set(evt.Flags2, MoreStatusFlags.Cold, "cold");
            Set(evt.Flags2, MoreStatusFlags.GlideMode, "glide_mode");
            Set(evt.Flags2, MoreStatusFlags.Hot, "hot");
            Set(evt.Flags2, MoreStatusFlags.InMulticrew, "in_multicrew");
            Set(evt.Flags2, MoreStatusFlags.InTaxi, "in_taxi");
            Set(evt.Flags2, MoreStatusFlags.LowHealth, "low_health");
            Set(evt.Flags2, MoreStatusFlags.LowOxygen, "low_oxygen");
            Set(evt.Flags2, MoreStatusFlags.OnFoot, "on_foot");
            Set(evt.Flags2, MoreStatusFlags.OnFootExterior, "on_foot_exterior");
            Set(evt.Flags2, MoreStatusFlags.OnFootInHangar, "on_foot_in_hangar");
            Set(evt.Flags2, MoreStatusFlags.OnFootInStation, "on_foot_in_station");
            Set(evt.Flags2, MoreStatusFlags.OnFootOnPlanet, "on_foot_on_planet");
            Set(evt.Flags2, MoreStatusFlags.OnFootSocialSpace, "on_foot_social_space");
            Set(evt.Flags2, MoreStatusFlags.VeryCold, "very_cold");
            Set(evt.Flags2, MoreStatusFlags.VeryHot, "very_hot");
            Set(evt.Gravity, "gravity");
            Set(evt.GuiFocus.ToString(), "gui_focus");
            Set(evt.Health, "health");
            Set(evt.Heading, "heading");
            Set(evt.Latitude, "latitude");
            Set(evt.LegalState, "legal_state");
            Set(evt.Longitude, "longitude");
            Set(evt.Oxygen, "oxygen");
            Set(evt.Pips.Engine, "pips_engine");
            Set(evt.Pips.System, "pips_system");
            Set(evt.Pips.Weapons, "pips_weapons");
            Set(evt.PlanetRadius, "planet_radius");
            Set(evt.SelectedWeapon, "selected_weapon");
            Set(evt.Temperature, "temperature");

            if (evt.Fuel != null)
            {
                Set(evt.Fuel.FuelMain, "fuel_main");
                Set(evt.Fuel.FuelReservoir, "fuel_reservoir");
            }

        }


    }

    public class EliteDangerousMacroDeckPlugin : MacroDeckPlugin
    {

        // A short description what the plugin can do
        public override string Description => "Elite Dangerous Variables and Actions";

        // You can add a image from your resources here
        public override Image Icon => Properties.Resources.elite_dangerous_logo;

        // Optional; If your plugin can be configured, set to "true". It'll make the "Configure" button appear in the package manager.
        public override bool CanConfigure => false;

        public string JournalPath { get; private set; }

        private StatusManager _statusManager;

        // Gets called when the plugin is loaded
        public override void Enable()
        {

            JournalPath = GetJournalPath();

            this.Actions = new List<PluginAction>
            {
            };

            _statusManager = new StatusManager(this);

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