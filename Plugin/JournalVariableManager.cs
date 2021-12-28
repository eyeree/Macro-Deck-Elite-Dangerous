using EliteJournalReader;
using EliteJournalReader.Events;
using SuchByte.MacroDeck.Plugins;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace EliteDangerousMacroDeckPlugin
{
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

        private class AfmuRepairsVariableManager : VariableManagerBase
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

        private class AppliedToSquadronVariableManager : VariableManagerBase
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

        private class ApproachBodyEventVariableManager : VariableManagerBase
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

        private class ApproachSettlementVariableManager : VariableManagerBase
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

        private class AsteroidCrackedVariableManager : VariableManagerBase
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

        private class BackpackVariableManager : VariableManagerBase
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

        private class BookDropshipVariableManager : VariableManagerBase
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

        private class BookTaxiVariableManager : VariableManagerBase
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

        private class BountyVariableManager : VariableManagerBase
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

        private class BuyAmmoVariableManager : VariableManagerBase
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

        private class BuyDronesVariableManager : VariableManagerBase
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

        private class BuyExplorationDataVariableManager : VariableManagerBase
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

        private class BuyMicroResourcesVariableManager : VariableManagerBase
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

        private class BuySuitVariableManager : VariableManagerBase
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

        private class BuyTradeDataVariableManager : VariableManagerBase
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

        private class BuyWeaponVariableManager : VariableManagerBase
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

        private class CancelDropshipVariableManager : VariableManagerBase
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

        private class CancelTaxiVariableManager : VariableManagerBase
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

        private class CapShipBondVariableManager : VariableManagerBase
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

        private class CargoDepotVariableManager : VariableManagerBase
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

        private class CargoVariableManager : VariableManagerBase
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

        private class LoadGameVariableManager : VariableManagerBase
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


}