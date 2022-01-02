using EliteJournalReader;
using EliteJournalReader.Events;
using System.Diagnostics;

namespace EliteDangerousMacroDeckPlugin.Variables
{
    public class StatusVariableManager : VariableManagerBase
    {

        private readonly StatusWatcher _statusWatcher;

        public StatusVariableManager(EliteDangerousMacroDeckPlugin plugin) : base(plugin)
        {
            _statusWatcher = new StatusWatcher();
            _statusWatcher.StatusUpdated += HandleEvent;
            _statusWatcher.StartWatching();
        }

        private void HandleEvent(object sender, StatusFileEvent evt)
        {
            Debug.WriteLine("-- Status -------------------------------");
            Set("ed_aim_down_sight", evt.Flags2, MoreStatusFlags.AimDownSight);
            Set("ed_altitude", evt.Altitude);
            Set("ed_altitude_from_average_radius", evt.Flags, StatusFlags.AltitudeFromAverageRadius);
            Set("ed_balance", evt.Balance);
            Set("ed_being_interdicted", evt.Flags, StatusFlags.BeingInterdicted);
            Set("ed_body_name", evt.BodyName);
            Set("ed_breathable_atmosphere", evt.Flags2, MoreStatusFlags.BreathableAtmosphere);
            Set("ed_cargo", evt.Cargo);
            Set("ed_cargo_scoop_deployed", evt.Flags, StatusFlags.CargoScoopDeployed);
            Set("ed_cold", evt.Flags2, MoreStatusFlags.Cold);
            Set("ed_context", GetContext(evt));
            Set("ed_destination_body", evt.Destination.Body);
            Set("ed_destination_name", evt.Destination.Name);
            Set("ed_destination_system", evt.Destination.System);
            Set("ed_docked", evt.Flags, StatusFlags.Docked);
            Set("ed_firegroup", evt.Firegroup);
            Set("ed_flight_assist_off", evt.Flags, StatusFlags.FlightAssistOff);
            Set("ed_fsd_charging", evt.Flags, StatusFlags.FsdCharging);
            Set("ed_fsd_cooldown", evt.Flags, StatusFlags.FsdCooldown);
            Set("ed_fsd_jump", evt.Flags, StatusFlags.FsdJump);
            Set("ed_fsd_mass_locked", evt.Flags, StatusFlags.FsdMassLocked);
            Set("ed_fuel_main", evt.Fuel == null ? 0 : evt.Fuel.FuelMain);
            Set("ed_fuel_reservoir", evt.Fuel == null ? 0 : evt.Fuel.FuelReservoir);
            Set("ed_glide_mode", evt.Flags2, MoreStatusFlags.GlideMode);
            Set("ed_gravity", evt.Gravity);
            Set("ed_gui_focus", evt.GuiFocus.ToString());
            Set("ed_hardpoints_deployed", evt.Flags, StatusFlags.HardpointsDeployed, StatusFlags.Supercruise & StatusFlags.FsdJump);
            Set("ed_has_lat_long", evt.Flags, StatusFlags.HasLatLong);
            Set("ed_heading", evt.Heading);
            Set("ed_health", evt.Health);
            Set("ed_hot", evt.Flags2, MoreStatusFlags.Hot);
            Set("ed_hud_in_analysis_mode", evt.Flags, StatusFlags.HudInAnalysisMode);
            Set("ed_in_fighter", evt.Flags, StatusFlags.InFighter);
            Set("ed_in_main_ship", evt.Flags, StatusFlags.InMainShip);
            Set("ed_in_multicrew", evt.Flags2, MoreStatusFlags.InMulticrew);
            Set("ed_in_srv", evt.Flags, StatusFlags.InSRV);
            Set("ed_in_taxi", evt.Flags2, MoreStatusFlags.InTaxi);
            Set("ed_in_wing", evt.Flags, StatusFlags.InWing);
            Set("ed_is_in_danger", evt.Flags, StatusFlags.IsInDanger);
            Set("ed_landed", evt.Flags, StatusFlags.Landed);
            Set("ed_landing_gear_down", evt.Flags, StatusFlags.LandingGearDown);
            Set("ed_latitude", evt.Latitude);
            Set("ed_legal_state", evt.LegalState);
            Set("ed_lights_on", evt.Flags, StatusFlags.LightsOn);
            Set("ed_longitude", evt.Longitude);
            Set("ed_low_fuel", evt.Flags, StatusFlags.LowFuel);
            Set("ed_low_health", evt.Flags2, MoreStatusFlags.LowHealth);
            Set("ed_low_oxygen", evt.Flags2, MoreStatusFlags.LowOxygen);
            Set("ed_night_vision", evt.Flags, StatusFlags.NightVision);
            Set("ed_on_foot", evt.Flags2, MoreStatusFlags.OnFoot);
            Set("ed_on_foot_exterior", evt.Flags2, MoreStatusFlags.OnFootExterior);
            Set("ed_on_foot_in_hangar", evt.Flags2, MoreStatusFlags.OnFootInHangar);
            Set("ed_on_foot_in_station", evt.Flags2, MoreStatusFlags.OnFootInStation);
            Set("ed_on_foot_on_planet", evt.Flags2, MoreStatusFlags.OnFootOnPlanet);
            Set("ed_on_foot_social_space", evt.Flags2, MoreStatusFlags.OnFootSocialSpace);
            Set("ed_overheating", evt.Flags, StatusFlags.Overheating);
            Set("ed_oxygen", evt.Oxygen);
            Set("ed_pips_engine", evt.Pips.Engine);
            Set("ed_pips_system", evt.Pips.System);
            Set("ed_pips_weapons", evt.Pips.Weapons);
            Set("ed_planet_radius", evt.PlanetRadius);
            Set("ed_scooping_fuel", evt.Flags, StatusFlags.ScoopingFuel);
            Set("ed_selected_weapon", evt.SelectedWeapon);
            Set("ed_shields_up", evt.Flags, StatusFlags.ShieldsUp);
            Set("ed_silent_running", evt.Flags, StatusFlags.SilentRunning);
            Set("ed_srv_drive_assist", evt.Flags, StatusFlags.SrvDriveAssist & StatusFlags.InSRV);
            Set("ed_srv_handbrake", evt.Flags, StatusFlags.SrvHandbrake & StatusFlags.InSRV);
            Set("ed_srv_high_beam", evt.Flags, StatusFlags.SrvHighBeam & StatusFlags.InSRV);
            Set("ed_srv_turret", evt.Flags, StatusFlags.SrvTurret & StatusFlags.InSRV);
            Set("ed_srv_under_ship", evt.Flags, StatusFlags.SrvUnderShip & StatusFlags.InSRV);
            Set("ed_supercruise", evt.Flags, StatusFlags.Supercruise);
            Set("ed_temperature", evt.Temperature);
            Set("ed_very_cold", evt.Flags2, MoreStatusFlags.VeryCold);
            Set("ed_very_hot", evt.Flags2, MoreStatusFlags.VeryHot);
        }

        private string GetContext(StatusFileEvent evt)
        {
            if (AreFlagsSet(evt.Flags, StatusFlags.InMainShip))
                if (AreFlagsSet(evt.Flags, StatusFlags.Docked))
                    return "docked";
                else
                    return "ship";
            else if (AreFlagsSet(evt.Flags, StatusFlags.InSRV))
                return "srv";
            else if (AreFlagsSet(evt.Flags2, MoreStatusFlags.OnFoot))
                if (AreFlagsSet(evt.Flags2, MoreStatusFlags.OnFootExterior))
                    return "exterior";
                else if (AreFlagsSet(evt.Flags2, MoreStatusFlags.OnFootInHangar))
                    return "hanger";
                else if (AreFlagsSet(evt.Flags2, MoreStatusFlags.OnFootInStation))
                    return "station";
                else if (AreFlagsSet(evt.Flags2, MoreStatusFlags.OnFootOnPlanet))
                    return "planet";
                else if (AreFlagsSet(evt.Flags2, MoreStatusFlags.OnFootSocialSpace))
                    return "social";
                else
                    return "unknown (on foot)";
            else if (AreFlagsSet(evt.Flags2, MoreStatusFlags.InTaxi))
                return "taxi";
            else if (AreFlagsSet(evt.Flags2, MoreStatusFlags.InMulticrew))
                return "crew";
            else
                return "unknown";
        }

    }


}