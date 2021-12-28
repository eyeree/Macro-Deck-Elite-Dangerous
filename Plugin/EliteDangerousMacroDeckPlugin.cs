using EliteJournalReader;
using EliteJournalReader.Events;
using SuchByte.MacroDeck.Plugins;
using SuchByte.MacroDeck.Variables;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
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
        private readonly HashSet<string> _marked = new HashSet<string>();

        protected VariableManager(MacroDeckPlugin plugin, string prefix)
        {
            _plugin = plugin;
            _prefix = prefix + "_";
        }

        protected void Mark()
        {
            Debug.WriteLine(">>>>> Mark");
            _marked.Clear();
        }

        protected void Sweep()
        {
            Debug.WriteLine(">>>>> Sweep Started");
            string[] keys = new string[_values.Keys.Count];
            _values.Keys.CopyTo(keys, 0);
            foreach (var name in keys)
            {
                if(!_marked.Contains(name))
                {
                    Clear(name);
                }
            }
            Debug.WriteLine(">>>>> Sweep Finished");
        }

        protected void Clear(string name)
        {
            // TODO: can Macro Deck support deleting variables?
            if(_values.TryGetValue(name, out object value))
            {
                var type = _variableType[value.GetType()];
                switch(type)
                {
                    case VariableType.String:
                        Set(name, "");
                        break;

                    case VariableType.Bool:
                        Set(name, false);
                        break;

                    case VariableType.Integer:
                        Set(name, 0);
                        break;

                    case VariableType.Float:
                        Set(name, 0.0f);
                        break;

                    default:
                        throw new NotSupportedException();
                }
            }
        }

        private readonly Dictionary<Type, VariableType> _variableType = new Dictionary<Type, VariableType>
        {
            { typeof(string), VariableType.String },
            { typeof(int), VariableType.Integer },
            { typeof(float), VariableType.Float },
            { typeof(bool), VariableType.Bool }
        };

        private void InternalSet(string name, object value, bool save)
        {
            Debug.Assert(_variableType.ContainsKey(value.GetType()), $"Unsupported variable type: {value.GetType().Name}");
            var full_name = _prefix + name;
            _marked.Add(name);
            var type = _variableType[value.GetType()];
            var found = _values.TryGetValue(name, out object current);
            if (!found || (current != null && !current.Equals(value)) || (current == null && value != null))  
            {
                _values[name] = value;
                Debug.WriteLine(">>>>> Changed variable {0} to {1}.", full_name, value);
                // SuchByte.MacroDeck.Variables.VariableManager.SetValue(full_name, value, type, _plugin, save);
            } else
            {
                Debug.WriteLine(">>>>> Unchanged variable {0} is {1}.", full_name, value);
            }
        }

        protected string SnakeCase(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return "";
            }

            var builder = new StringBuilder(text.Length + Math.Min(2, text.Length / 5));
            var previousCategory = default(UnicodeCategory?);

            for (var currentIndex = 0; currentIndex < text.Length; currentIndex++)
            {
                var currentChar = text[currentIndex];
                if (currentChar == '_')
                {
                    builder.Append('_');
                    previousCategory = null;
                    continue;
                }

                var currentCategory = char.GetUnicodeCategory(currentChar);
                switch (currentCategory)
                {

                    case UnicodeCategory.UppercaseLetter:
                    case UnicodeCategory.TitlecaseLetter:
                        if (previousCategory == UnicodeCategory.SpaceSeparator ||
                            previousCategory == UnicodeCategory.LowercaseLetter ||
                            previousCategory != UnicodeCategory.DecimalDigitNumber &&
                            previousCategory != null &&
                            currentIndex > 0 &&
                            currentIndex + 1 < text.Length &&
                            char.IsLower(text[currentIndex + 1]))
                        {
                            builder.Append('_');
                        }

                        currentChar = char.ToLower(currentChar, CultureInfo.InvariantCulture);
                        break;

                    case UnicodeCategory.LowercaseLetter:
                    case UnicodeCategory.DecimalDigitNumber:
                        if (previousCategory == UnicodeCategory.SpaceSeparator)
                        {
                            builder.Append('_');
                        }
                        break;

                    default:
                        if (previousCategory != null)
                        {
                            previousCategory = UnicodeCategory.SpaceSeparator;
                        }
                        continue;
                }

                builder.Append(currentChar);
                previousCategory = currentCategory;
            }

            return builder.ToString();
        }

        protected string TitleCase(string text)
        {
            var textInfo = CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(text);
        }

        protected void Set(string name, string value, bool save = false)
        {
            
            if(value == null)
            {
                value = "";
            }

            InternalSet(name, value, save);

        }

        protected void Set(string name, int value, bool save = false)
        {
            InternalSet(name, value, save);
        }
        protected void Set(string name, long value, bool save = false)
        {
            // TODO: change Macro Deck to use long instead of int for VariableType.Integer?
            Set(name, value.ToString(), save);
        }

        protected void Set(string name, float value, bool save = false)
        {
            InternalSet(name, value, save);
        }

        protected void Set(string name, double value, bool save = false)
        {
            // TODO: change Macro Deck to use double instead of float for VariableType.Float?
            Set(name, value.ToString(), save);
        }

        protected void Set(string name, bool value, bool save = false)
        {
            InternalSet(name, value, save);
        }

        protected void Set<Flags>(string name, Flags flags, Flags flagsSet, bool save = false) where Flags : System.Enum
        {
            ulong bits = Convert.ToUInt64(flags);
            ulong bitsSet = Convert.ToUInt64(flagsSet);
            Set(name, (bits & bitsSet) == bitsSet, save);
        }

        protected void Set<Flags>(string name, Flags flags, Flags flagsSet, Flags flagsClear, bool save = false) where Flags : System.Enum
        {
            ulong bits = Convert.ToUInt64(flags);
            ulong bitsSet = Convert.ToUInt64(flagsSet);
            ulong bitsClear = Convert.ToUInt64(flagsClear);
            Set(name, ((bits & bitsSet) == bitsSet) && (bits & bitsClear) == 0, save);
        }

        protected void Set(string name, DateTime value, bool save = false)
        {
            // TODO: change Macro Deck to use long instead of int for VariableType.Integer and
            // then use ticks instead?
            Set(name, value.ToString(), save);
        }

        protected void Save(string name, string value)
        {
            Set(name, value, true);
        }


        protected void Save(string name, int value)
        {
            Set(name, value, true);
        }

        protected void Save(string name, long value)
        {
            Set(name, value, true);
        }

        protected void Save(string name, float value)
        {
            Set(name, value, true);
        }

        protected void Save(string name, double value)
        {
            Set(name, value, true);
        }

        protected void Save(string name, bool value)
        {
            Set(name, value, true);
        }

        protected void Save<Flags>(string name, Flags flags, Flags flagsSet) where Flags : System.Enum
        {
            Set(name, flags, flagsSet, true);
        }

        protected void Save<Flags>(string name, Flags flags, Flags flagsSet, Flags flagsClear) where Flags : System.Enum
        {
            Set(name, flags, flagsSet, flagsClear, true);
        }

        protected void Save(string name, DateTime value)
        {
            Set(name, value, true);
        }

    }

    public class StatusVariableManager : VariableManager
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


    public class JournalVariableManager 
    {

        private readonly JournalWatcher _journalWatcher;

        public JournalVariableManager(EliteDangerousMacroDeckPlugin plugin)
        {
            _journalWatcher = new JournalWatcher(plugin.JournalPath);
            new AfmuRepairsVariableManager(plugin, _journalWatcher);
            new AppliedToSquadronVariableManager(plugin, _journalWatcher);
            new ApproachBodyEventVariableManager(plugin, _journalWatcher);
            new ApproachSettlementVariableManager(plugin, _journalWatcher);
            new AsteroidCrackedVariableManager(plugin, _journalWatcher);
            new BackpackVariableManager(plugin, _journalWatcher);
            new BookDropshipVariableManager(plugin, _journalWatcher);
            new BookTaxiVariableManager(plugin, _journalWatcher);
            new BountyVariableManager(plugin, _journalWatcher);
            new BuyAmmoVariableManager(plugin, _journalWatcher);
            new BuyDronesVariableManager(plugin, _journalWatcher);
            new BuyExplorationDataVariableManager(plugin, _journalWatcher);
            new BuyMicroResourcesVariableManager(plugin, _journalWatcher);
            new BuySuitVariableManager(plugin, _journalWatcher);
            new BuyTradeDataVariableManager(plugin, _journalWatcher);
            new BuyWeaponVariableManager(plugin, _journalWatcher);
            new CancelDropshipVariableManager(plugin, _journalWatcher);
            new CancelTaxiVariableManager(plugin, _journalWatcher);
            new CapShipBondVariableManager(plugin, _journalWatcher);
            new CargoDepotVariableManager(plugin, _journalWatcher);
            new CargoVariableManager(plugin, _journalWatcher);

            new LoadGameVariableManager(plugin, _journalWatcher);

            _journalWatcher.StartWatching();
        }

        private class AfmuRepairsVariableManager : VariableManager
        {

            public AfmuRepairsVariableManager(EliteDangerousMacroDeckPlugin plugin, JournalWatcher watcher) : base(plugin, "ed_admu")
            {
                watcher.GetEvent<AfmuRepairsEvent>().Fired += HandleEvent;
            }

            private void HandleEvent(object sender, AfmuRepairsEvent.AfmuRepairsEventArgs e)
            {
                string name = SnakeCase(e.Module);
                Set($"{name}_fully_repaired", e.FullyRepaired);
                Set($"{name}_health", e.Health);
                Set($"{name}_timestamp", e.Timestamp);
                Set("module", e.Module);
                Set("timestamp", e.Timestamp);
            }

        }

        private class AppliedToSquadronVariableManager : VariableManager
        {

            public AppliedToSquadronVariableManager(MacroDeckPlugin plugin, JournalWatcher watcher) : base(plugin, "ed_applied_to_squadron")
            {
                watcher.GetEvent<AppliedToSquadronEvent>().Fired += HandleEvent;
            }

            private void HandleEvent(object sender, AppliedToSquadronEvent.AppliedToSquadronEventArgs e)
            {
                Set("squadron_name", e.SquadronName);
                Set("timestamp", e.Timestamp);
            }

        }

        private class ApproachBodyEventVariableManager : VariableManager
        {

            public ApproachBodyEventVariableManager(MacroDeckPlugin plugin, JournalWatcher watcher) : base(plugin, "ed_approach_body")
            {
                watcher.GetEvent<ApproachBodyEvent>().Fired += HandleEvent;
            }

            public void HandleEvent(object sender, ApproachBodyEvent.ApproachBodyEventArgs e)
            {
                Set("body", e.Body);
                Set("body_id", e.BodyID.ToString());
                Set("star_system", e.StarSystem);
                Set("system_address", e.SystemAddress.ToString());
                Set("timestamp", e.Timestamp);
            }

        }

        private class ApproachSettlementVariableManager : VariableManager
        {

            public ApproachSettlementVariableManager(MacroDeckPlugin plugin, JournalWatcher watcher) : base(plugin, "ed_approach_settlement")
            {
                watcher.GetEvent<ApproachSettlementEvent>().Fired += HandleEvent;
            }

            public void HandleEvent(object sender, ApproachSettlementEvent.ApproachSettlementEventArgs e)
            {
                Set("body_id", e.BodyID.ToString());
                Set("body_name", e.BodyName);
                Set("latitude", e.Latitude);
                Set("longitude", e.Longitude);
                Set("market_id", e.MarketID.ToString());
                Set("name", e.Name);
                Set("system_address", e.SystemAddress.ToString());
                Set("timestamp", e.Timestamp);
            }

        }

        private class AsteroidCrackedVariableManager : VariableManager
        {

            public AsteroidCrackedVariableManager(MacroDeckPlugin plugin, JournalWatcher watcher) : base(plugin, "ed_asteroid_cracked")
            {
                watcher.GetEvent<AsteroidCrackedEvent>().Fired += HandleEvent;
            }

            public void HandleEvent(object sender, AsteroidCrackedEvent.AsteroidCrackedEventArgs e)
            {
                Set("body", e.Body);
                Set("timestamp", e.Timestamp);
            }

        }

        private class BackpackVariableManager : VariableManager
        {

            private readonly Dictionary<string, int> _counts = new Dictionary<string, int>();

            public BackpackVariableManager(MacroDeckPlugin plugin, JournalWatcher watcher) : base(plugin, "ed_backpack")
            {
                watcher.GetEvent<BackpackChangeEvent>().Fired += HandleEvent;
                watcher.GetEvent<BackpackEvent>().Fired += HandleEvent;
                watcher.GetEvent<BackpackMaterialsEvent>().Fired += HandleEvent;
            }

            private string GetKey(string type, string name)
            {
                return $"{SnakeCase(type)}_{SnakeCase(name)}";
            }

            private string SetCount(string type, string name, int count)
            {
                var key = GetKey(type, name);
                SetCount(key, count);
                return key;
            }

            private void SetCount(string key, int count)
            {
                _counts[key] = count;
                Debug.WriteLine(">>>>>>>>> Backpack SetCount {0} {1}", key, count);
                Set(key, count);
            }

            private int GetCount(string type, string name)
            {
                var key = GetKey(type, name);
                _counts.TryGetValue(key, out var count);
                Debug.WriteLine(">>>>>>>>> Backpack GetCount {0} {1}", key, count);
                return count;
            }

            public void HandleEvent(object sender, BackpackChangeEvent.BackpackChangeEventArgs e)
            {
                // TODO: do we need to handle this event? What is Type?
                foreach (var item in e.Added)
                {
                    var count = GetCount(item.Type, item.Name);
                    count += item.Count;
                    SetCount(item.Type, item.Name, count);
                    Set("added", item.Name);
                }
                foreach (var item in e.Removed)
                {
                    var count = GetCount(item.Type, item.Name);
                    count -= item.Count;
                    SetCount(item.Type, item.Name, Math.Max(count, 0));
                    Set("removed", item.Name);
                }
            }

            public void HandleEvent(object sender, BackpackEvent.BackpackEventArgs e)
            {

                Mark();

                foreach (var item in e.Items)
                {
                    SetCount("item", item.Name, item.Count);
                }

                foreach (var component in e.Components)
                {
                    SetCount("component", component.Name, component.Count);
                }

                foreach (var consumable in e.Consumables)
                {
                    SetCount("consumable", consumable.Name, consumable.Count);
                }

                foreach (var data in e.Data)
                {
                    SetCount("data", data.Name, data.Count);
                }

                Sweep();

            }

            public void HandleEvent(object sender, BackpackMaterialsEvent.BackpackMaterialsEventArgs e)
            {
                // TODO: How does this relate to the above event?
            }

        }

        private class BookDropshipVariableManager : VariableManager
        {

            public BookDropshipVariableManager(MacroDeckPlugin plugin, JournalWatcher watcher) : base(plugin, "ed_book_dropship")
            {
                watcher.GetEvent<BookDropshipEvent>().Fired += HandleEvent;
            }

            public void HandleEvent(object sender, BookDropshipEvent.BookDropshipEventArgs e)
            {
                Set("cost", e.Cost);
                Set("destination_system", e.DestinationSystem);
                Set("destination_location", e.DestinationLocation);
                Set("timestamp", e.Timestamp);
            }

        }

        private class BookTaxiVariableManager : VariableManager
        {

            public BookTaxiVariableManager(MacroDeckPlugin plugin, JournalWatcher watcher) : base(plugin, "ed_book_taxi")
            {
                watcher.GetEvent<BookTaxiEvent>().Fired += HandleEvent;
            }

            public void HandleEvent(object sender, BookTaxiEvent.BookTaxiEventArgs e)
            {
                Set("cost", e.Cost);
                Set("destination_system", e.DestinationSystem);
                Set("destination_location", e.DestinationLocation);
                Set("timestamp", e.Timestamp);
            }

        }

        private class BountyVariableManager : VariableManager
        {

            public BountyVariableManager(MacroDeckPlugin plugin, JournalWatcher watcher) : base(plugin, "ed_add_bounty")
            {
                watcher.GetEvent<BountyEvent>().Fired += HandleEvent;
            }

            public void HandleEvent(object sender, BountyEvent.BountyEventArgs e)
            {
                Set("target", e.Target);
                Set("target_localized", e.Target_Localised);
                Set("victim_faction", e.VictimFaction);
                Set("total_reward", e.TotalReward);
                Set("shared_with_others", e.SharedWithOthers);
                Set("timestamp", e.Timestamp);
            }

        }

        private class BuyAmmoVariableManager : VariableManager
        {

            public BuyAmmoVariableManager(MacroDeckPlugin plugin, JournalWatcher watcher) : base(plugin, "ed_buy_ammo")
            {
                watcher.GetEvent<BuyAmmoEvent>().Fired += HandleEvent;
            }

            public void HandleEvent(object sender, BuyAmmoEvent.BuyAmmoEventArgs e)
            {
                Set("cost", e.Cost);
                Set("timestamp", e.Timestamp);
            }

        }

        private class BuyDronesVariableManager : VariableManager
        {

            public BuyDronesVariableManager(MacroDeckPlugin plugin, JournalWatcher watcher) : base(plugin, "ed_buy_drone")
            {
                watcher.GetEvent<BuyDronesEvent>().Fired += HandleEvent;
            }

            public void HandleEvent(object sender, BuyDronesEvent.BuyDronesEventArgs e)
            {
                var name = SnakeCase(e.Type);
                Set($"{name}_count", e.Count);
                Set($"{name}_price", e.BuyPrice);
                Set($"{name}_cost", e.TotalCost);
                Set($"{name}_timestamp", e.Timestamp);
                Set("type", e.Type);
                Set("type_localized", e.Type_Localised);
                Set("count", e.Count);
                Set("price", e.BuyPrice);
                Set("cost", e.TotalCost);
                Set("timestamp", e.Timestamp);
            }

        }

        private class BuyExplorationDataVariableManager : VariableManager
        {

            public BuyExplorationDataVariableManager(MacroDeckPlugin plugin, JournalWatcher watcher) : base(plugin, "ed_buy_exploration_data")
            {
                watcher.GetEvent<BuyExplorationDataEvent>().Fired += HandleEvent;
            }

            public void HandleEvent(object sender, BuyExplorationDataEvent.BuyExplorationDataEventArgs e)
            {
                Set("system", e.System);
                Set("cost", e.Cost);
                Set("timestamp", e.Timestamp);
            }

        }

        private class BuyMicroResourcesVariableManager : VariableManager
        {

            public BuyMicroResourcesVariableManager(MacroDeckPlugin plugin, JournalWatcher watcher) : base(plugin, "ed_buy_micro_resources")
            {
                watcher.GetEvent<BuyMicroResourcesEvent>().Fired += HandleEvent;
            }

            public void HandleEvent(object sender, BuyMicroResourcesEvent.BuyMicroResourcesEventArgs e)
            {
                var name = SnakeCase(e.Name);
                Set($"{name}_name_localized", e.Name_Localised);
                Set($"{name}_count", e.Count);
                Set($"{name}_category", e.Category);
                Set($"{name}_category_localized", e.Category_Localised);
                Set($"{name}_price", e.Price);
                Set($"{name}_market_id", e.MarketID.ToString());
                Set($"{name}_timestamp", e.Timestamp);
                Set("name", e.Name);
                Set("name_localized", e.Name_Localised);
                Set("category", e.Category);
                Set("category_localized", e.Category_Localised);
                Set("count", e.Count);
                Set("price", e.Price);
                Set("market_id", e.MarketID.ToString());
                Set("timestamp", e.Timestamp);
            }

        }

        private class BuySuitVariableManager : VariableManager
        {

            public BuySuitVariableManager(MacroDeckPlugin plugin, JournalWatcher watcher) : base(plugin, "ed_buy_suit")
            {
                watcher.GetEvent<BuySuitEvent>().Fired += HandleEvent;
            }

            public void HandleEvent(object sender, BuySuitEvent.BuySuitEventArgs e)
            {
                Set("name", e.Name);
                Set("name_localized", e.Name_Localised);
                Set("price", e.Price);
                Set("id", e.SuitID.ToString());
                Set("mods", String.Join(",", e.SuitMods));
                Set("timestamp", e.Timestamp);
            }

        }

        private class BuyTradeDataVariableManager : VariableManager
        {

            public BuyTradeDataVariableManager(MacroDeckPlugin plugin, JournalWatcher watcher) : base(plugin, "ed_buy_trade_data")
            {
                watcher.GetEvent<BuyTradeDataEvent>().Fired += HandleEvent;
            }

            public void HandleEvent(object sender, BuyTradeDataEvent.BuyTradeDataEventArgs e)
            {
                Set("system", e.System);
                Set("cost", e.Cost);
                Set("timestamp", e.Timestamp);
            }

        }

        private class BuyWeaponVariableManager : VariableManager
        {

            public BuyWeaponVariableManager(MacroDeckPlugin plugin, JournalWatcher watcher) : base(plugin, "ed_buy_weapon")
            {
                watcher.GetEvent<BuyWeaponEvent>().Fired += HandleEvent;
            }

            public void HandleEvent(object sender, BuyWeaponEvent.BuyWeaponEventArgs e)
            {
                Set("name", e.Name);
                Set("name_localized", e.Name_Localised);
                Set("price", e.Price);
                Set("module_id", e.SuitModuleID.ToString());
                Set("class", e.Class);
                Set("mods", string.Join(",", e.WeaponMods));
                Set("timestamp", e.Timestamp);
            }

        }

        private class CancelDropshipVariableManager : VariableManager
        {

            public CancelDropshipVariableManager(MacroDeckPlugin plugin, JournalWatcher watcher) : base(plugin, "ed_CancelDropship")
            {
                watcher.GetEvent<CancelDropshipEvent>().Fired += HandleEvent;
            }

            public void HandleEvent(object sender, CancelDropshipEvent.CancelDropshipEventArgs e)
            {
                Set("refund", e.Refund);
                Set("timestamp", e.Timestamp);
            }

        }

        private class CancelTaxiVariableManager : VariableManager
        {

            public CancelTaxiVariableManager(MacroDeckPlugin plugin, JournalWatcher watcher) : base(plugin, "ed_cancel_taxi")
            {
                watcher.GetEvent<CancelTaxiEvent>().Fired += HandleEvent;
            }

            public void HandleEvent(object sender, CancelTaxiEvent.CancelTaxiEventArgs e)
            {
                Set("refund", e.Refund);
                Set("timestamp", e.Timestamp);
            }

        }

        private class CapShipBondVariableManager : VariableManager
        {

            public CapShipBondVariableManager(MacroDeckPlugin plugin, JournalWatcher watcher) : base(plugin, "ed_cap_ship_bond")
            {
                watcher.GetEvent<CapShipBondEvent>().Fired += HandleEvent;
            }

            public void HandleEvent(object sender, CapShipBondEvent.CapShipBondEventArgs e)
            {
                Set("awarding_faction", e.AwardingFaction);
                Set("victim_faction", e.VictimFaction);
                Set("reward", e.Reward);
                Set("timestamp", e.Timestamp);
            }

        }

        private class CargoDepotVariableManager : VariableManager
        {

            public CargoDepotVariableManager(MacroDeckPlugin plugin, JournalWatcher watcher) : base(plugin, "ed_CargoDepot")
            {
                watcher.GetEvent<CargoDepotEvent>().Fired += HandleEvent;
            }

            public void HandleEvent(object sender, CargoDepotEvent.CargoDepotEventArgs e)
            {
                Set("mission_id", e.MissionID.ToString());
                Set("update_type", e.UpdateType);
                Set("cargo_type", e.CargoType);
                Set("cargo_type_localized", e.CargoType_Localised);
                Set("count", e.Count);
                Set("start_market_id", e.StartMarketID.ToString());
                Set("end_market_id", e.EndMarketID.ToString());
                Set("items_collected", e.ItemsCollected);
                Set("items_delivered", e.ItemsDelivered);
                Set("total_items_to_deliver", e.TotalItemsToDeliver);
                Set("progress", (float)e.Progress);
                Set("timestamp", e.Timestamp);
            }

        }

        private class CargoVariableManager : VariableManager
        {

            private readonly JournalWatcher _watcher;

            public CargoVariableManager(MacroDeckPlugin plugin, JournalWatcher watcher) : base(plugin, "ed_cargo")
            {
                _watcher = watcher;
                watcher.GetEvent<CargoEvent>().Fired += HandleEvent;
            }

            public void HandleEvent(object sender, CargoEvent.CargoEventArgs e)
            {
                Debug.WriteLine("-- Cargo -------------------------------");
                Mark();
                var cargo = _watcher.ReadCargoJson();
                if (cargo.Inventory != null)
                {
                    foreach (var commodity in cargo.Inventory)
                    {
                        var name = SnakeCase(commodity.Name);
                        Set($"inventory_{name}_count", commodity.Count);
                        Set($"inventory_{name}_stolen", commodity.Stolen);
                        Set($"inventory_{name}_name_localized", commodity.Name_Localised ?? TitleCase(commodity.Name));
                    }
                }
                Set("count", cargo.Count);
                Set("vessel", cargo.Vessel);
                Set("timestamp", cargo.Timestamp);
                Sweep();
            }

        }

        //private class TEMPLATEVariableManager : VariableManager
        //{

        //    public TEMPLATEVariableManager(MacroDeckPlugin plugin, JournalWatcher watcher) : base(plugin, "ed_TEMPLATE")
        //    {
        //        watcher.GetEvent<TEMPLATEEvent>().Fired += HandleEvent;
        //    }

        //    public void HandleEvent(object sender, TEMPLATEEvent.TEMPLATEEventArgs e)
        //    {
        //        Set("timestamp", e.Timestamp);
        //    }

        //}

        private class LoadGameVariableManager : VariableManager
        {

            public LoadGameVariableManager(MacroDeckPlugin plugin, JournalWatcher watcher) : base(plugin, "ed")
            {
                watcher.GetEvent<LoadGameEvent>().Fired += HandleEvent;
            }

            public void HandleEvent(object sender, LoadGameEvent.LoadGameEventArgs e)
            {
                Debug.WriteLine("-- LoadGame -------------------------------");
                Set("commander", e.Commander);
                Set("fid", e.FID);
                Set("fuel_capacity", e.FuelCapacity);
                Set("fuel_level", e.FuelLevel);
                Set("game_build", e.Build);
                Set("game_credits", e.Credits);
                Set("game_language", e.Language);
                Set("game_mode", e.GameMode.ToString());
                Set("game_version", e.GameVersion);
                Set("group", e.Group);
                Set("is_horizons", e.Horizons);
                Set("is_odyssey", e.Odyssey);
                Set("loan", e.Loan);
                Set("ship", e.Ship);
                Set("ship_id", e.ShipID.ToString());
                Set("ship_ident", e.ShipIdent);
                Set("ship_name", e.ShipName);
                Set("started_dead", e.StartDead);
                Set("started_landed", e.StartLanded);
                Set("load_game_timestamp", e.Timestamp);
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

        private StatusVariableManager _statusVariableManager;
        private JournalVariableManager _journalVariableManager;

        // Gets called when the plugin is loaded
        public override void Enable()
        {

            JournalPath = GetJournalPath();

            this.Actions = new List<PluginAction>
            {
            };

            _statusVariableManager = new StatusVariableManager(this);
            _journalVariableManager = new JournalVariableManager(this);

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