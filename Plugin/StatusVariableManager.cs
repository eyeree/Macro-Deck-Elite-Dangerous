using EliteJournalReader;
using EliteJournalReader.Events;
using System.Diagnostics;

namespace EliteDangerousMacroDeckPlugin
{
    public class StatusVariableManager : VariableManagerBase
    {

        private readonly StatusWatcher _statusWatcher;

        public StatusVariableManager(EliteDangerousMacroDeckPlugin plugin) : base(plugin, "ed")
        {
            _statusWatcher = new StatusWatcher(plugin.JournalPath);
            _statusWatcher.StatusUpdated += HandleEvent;
            _statusWatcher.StartWatching();
        }

        private void HandleEvent(object sender, StatusFileEvent evt)
        {
            Debug.WriteLine("-- Status -------------------------------");
            Set("aim_down_sight", evt.Flags2, MoreStatusFlags.AimDownSight);
            Set("altitude", evt.Altitude);
            Set("altitude_from_average_radius", evt.Flags, StatusFlags.AltitudeFromAverageRadius);
            Set("balance", evt.Balance);
            Set("being_interdicted", evt.Flags, StatusFlags.BeingInterdicted);
            Set("body_name", evt.BodyName);
            Set("breathable_atmosphere", evt.Flags2, MoreStatusFlags.BreathableAtmosphere);
            Set("cargo", evt.Cargo);
            Set("cargo_scoop_deployed", evt.Flags, StatusFlags.CargoScoopDeployed);
            Set("cold", evt.Flags2, MoreStatusFlags.Cold);
            Set("destination_body", evt.Destination.Body);
            Set("destination_name", evt.Destination.Name);
            Set("destination_system", evt.Destination.System);
            Set("docked", evt.Flags, StatusFlags.Docked);
            Set("firegroup", evt.Firegroup);
            Set("flight_assist_off", evt.Flags, StatusFlags.FlightAssistOff);
            Set("fsd_charging", evt.Flags, StatusFlags.FsdCharging);
            Set("fsd_cooldown", evt.Flags, StatusFlags.FsdCooldown);
            Set("fsd_jump", evt.Flags, StatusFlags.FsdJump);
            Set("fsd_mass_locked", evt.Flags, StatusFlags.FsdMassLocked);
            Set("fuel_main", (evt.Fuel == null) ? 0 : evt.Fuel.FuelMain);
            Set("fuel_reservoir", (evt.Fuel == null) ? 0 : evt.Fuel.FuelReservoir);
            Set("glide_mode", evt.Flags2, MoreStatusFlags.GlideMode);
            Set("gravity", evt.Gravity);
            Set("gui_focus", evt.GuiFocus.ToString());
            Set("hardpoints_deployed", evt.Flags, StatusFlags.HardpointsDeployed, StatusFlags.Supercruise & StatusFlags.FsdJump);
            Set("has_lat_long", evt.Flags, StatusFlags.HasLatLong);
            Set("heading", evt.Heading);
            Set("health", evt.Health);
            Set("hot", evt.Flags2, MoreStatusFlags.Hot);
            Set("hud_in_analysis_mode", evt.Flags, StatusFlags.HudInAnalysisMode);
            Set("in_fighter", evt.Flags, StatusFlags.InFighter);
            Set("in_main_ship", evt.Flags, StatusFlags.InMainShip);
            Set("in_multicrew", evt.Flags2, MoreStatusFlags.InMulticrew);
            Set("in_srv", evt.Flags, StatusFlags.InSRV);
            Set("in_taxi", evt.Flags2, MoreStatusFlags.InTaxi);
            Set("in_wing", evt.Flags, StatusFlags.InWing);
            Set("is_in_danger", evt.Flags, StatusFlags.IsInDanger);
            Set("landed", evt.Flags, StatusFlags.Landed);
            Set("landing_gear_down", evt.Flags, StatusFlags.LandingGearDown);
            Set("latitude", evt.Latitude);
            Set("legal_state", evt.LegalState);
            Set("lights_on", evt.Flags, StatusFlags.LightsOn);
            Set("longitude", evt.Longitude);
            Set("low_fuel", evt.Flags, StatusFlags.LowFuel);
            Set("low_health", evt.Flags2, MoreStatusFlags.LowHealth);
            Set("low_oxygen", evt.Flags2, MoreStatusFlags.LowOxygen);
            Set("night_vision", evt.Flags, StatusFlags.NightVision);
            Set("on_foot", evt.Flags2, MoreStatusFlags.OnFoot);
            Set("on_foot_exterior", evt.Flags2, MoreStatusFlags.OnFootExterior);
            Set("on_foot_in_hangar", evt.Flags2, MoreStatusFlags.OnFootInHangar);
            Set("on_foot_in_station", evt.Flags2, MoreStatusFlags.OnFootInStation);
            Set("on_foot_on_planet", evt.Flags2, MoreStatusFlags.OnFootOnPlanet);
            Set("on_foot_social_space", evt.Flags2, MoreStatusFlags.OnFootSocialSpace);
            Set("overheating", evt.Flags, StatusFlags.Overheating);
            Set("oxygen", evt.Oxygen);
            Set("pips_engine", evt.Pips.Engine);
            Set("pips_system", evt.Pips.System);
            Set("pips_weapons", evt.Pips.Weapons);
            Set("planet_radius", evt.PlanetRadius);
            Set("scooping_fuel", evt.Flags, StatusFlags.ScoopingFuel);
            Set("selected_weapon", evt.SelectedWeapon);
            Set("shields_up", evt.Flags, StatusFlags.ShieldsUp);
            Set("silent_running", evt.Flags, StatusFlags.SilentRunning);
            Set("srv_drive_assist", evt.Flags, StatusFlags.SrvDriveAssist & StatusFlags.InSRV);
            Set("srv_handbrake", evt.Flags, StatusFlags.SrvHandbrake & StatusFlags.InSRV);
            Set("srv_high_beam", evt.Flags, StatusFlags.SrvHighBeam & StatusFlags.InSRV);
            Set("srv_turret", evt.Flags, StatusFlags.SrvTurret & StatusFlags.InSRV);
            Set("srv_under_ship", evt.Flags, StatusFlags.SrvUnderShip & StatusFlags.InSRV);
            Set("supercruise", evt.Flags, StatusFlags.Supercruise);
            Set("temperature", evt.Temperature);
            Set("very_cold", evt.Flags2, MoreStatusFlags.VeryCold);
            Set("very_hot", evt.Flags2, MoreStatusFlags.VeryHot);
        }

    }


}