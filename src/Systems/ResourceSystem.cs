using System;
using System.Collections.Generic;

namespace AetherRenaissance.Systems
{
    // Resource types in the three-tier economy
    public enum ResourceType
    {
        Crystal,   // Tier 1: Basic resource
        Biomass,   // Tier 2: Intermediate resource
        Aether     // Tier 3: Advanced resource
    }

    public class ResourceStorage
    {
        public int Crystal { get; set; }
        public int Biomass { get; set; }
        public int Aether { get; set; }
        
        public int CrystalCapacity { get; set; }
        public int BiomassCapacity { get; set; }
        public int AetherCapacity { get; set; }

        public ResourceStorage(int crystalCap = 1000, int biomassCap = 500, int aetherCap = 100)
        {
            Crystal = 0;
            Biomass = 0;
            Aether = 0;
            
            CrystalCapacity = crystalCap;
            BiomassCapacity = biomassCap;
            AetherCapacity = aetherCap;
        }

        public int GetResource(ResourceType type)
        {
            return type switch
            {
                ResourceType.Crystal => Crystal,
                ResourceType.Biomass => Biomass,
                ResourceType.Aether => Aether,
                _ => 0
            };
        }

        public int GetCapacity(ResourceType type)
        {
            return type switch
            {
                ResourceType.Crystal => CrystalCapacity,
                ResourceType.Biomass => BiomassCapacity,
                ResourceType.Aether => AetherCapacity,
                _ => 0
            };
        }

        public void SetResource(ResourceType type, int amount)
        {
            switch (type)
            {
                case ResourceType.Crystal:
                    Crystal = Math.Min(amount, CrystalCapacity);
                    break;
                case ResourceType.Biomass:
                    Biomass = Math.Min(amount, BiomassCapacity);
                    break;
                case ResourceType.Aether:
                    Aether = Math.Min(amount, AetherCapacity);
                    break;
            }
        }

        public bool CanStore(ResourceType type, int amount)
        {
            return GetResource(type) + amount <= GetCapacity(type);
        }

        public void DisplayResources()
        {
            Console.WriteLine("\n=== RESOURCES ===");
            Console.WriteLine($"Crystal: {Crystal}/{CrystalCapacity}");
            Console.WriteLine($"Biomass: {Biomass}/{BiomassCapacity}");
            Console.WriteLine($"Aether: {Aether}/{AetherCapacity}");
        }
    }

    public class ResourceCost
    {
        public int Crystal { get; set; }
        public int Biomass { get; set; }
        public int Aether { get; set; }

        public ResourceCost(int crystal = 0, int biomass = 0, int aether = 0)
        {
            Crystal = crystal;
            Biomass = biomass;
            Aether = aether;
        }

        public override string ToString()
        {
            var parts = new List<string>();
            if (Crystal > 0) parts.Add($"{Crystal} Crystal");
            if (Biomass > 0) parts.Add($"{Biomass} Biomass");
            if (Aether > 0) parts.Add($"{Aether} Aether");
            return string.Join(", ", parts);
        }
    }

    public class ResourceGatherer
    {
        public string Name { get; set; }
        public ResourceType ResourceType { get; set; }
        public int GatherRate { get; set; }  // Resources per tick
        public bool IsActive { get; set; }

        public ResourceGatherer(string name, ResourceType type, int rate)
        {
            Name = name;
            ResourceType = type;
            GatherRate = rate;
            IsActive = true;
        }

        public int Gather()
        {
            if (!IsActive) return 0;
            return GatherRate;
        }
    }

    public class ResourceSystem
    {
        public ResourceStorage Storage { get; set; }
        public List<ResourceGatherer> Gatherers { get; set; }
        public int GameTick { get; private set; }

        public ResourceSystem()
        {
            Storage = new ResourceStorage();
            Gatherers = new List<ResourceGatherer>();
            GameTick = 0;
        }

        public ResourceSystem(int crystalCap, int biomassCap, int aetherCap)
        {
            Storage = new ResourceStorage(crystalCap, biomassCap, aetherCap);
            Gatherers = new List<ResourceGatherer>();
            GameTick = 0;
        }

        public void AddResource(ResourceType type, int amount)
        {
            int current = Storage.GetResource(type);
            int capacity = Storage.GetCapacity(type);
            int actualAmount = Math.Min(amount, capacity - current);
            
            Storage.SetResource(type, current + actualAmount);
            
            if (actualAmount < amount)
            {
                Console.WriteLine($"Added {actualAmount} {type} (storage full, {amount - actualAmount} wasted)");
            }
            else
            {
                Console.WriteLine($"Added {actualAmount} {type}. Total: {Storage.GetResource(type)}/{capacity}");
            }
        }

        public bool SpendResources(ResourceCost cost)
        {
            if (!CanAfford(cost))
            {
                Console.WriteLine($"Not enough resources! Need: {cost}");
                DisplayCurrentResources();
                return false;
            }

            Storage.Crystal -= cost.Crystal;
            Storage.Biomass -= cost.Biomass;
            Storage.Aether -= cost.Aether;
            
            Console.WriteLine($"Spent: {cost}");
            return true;
        }

        public bool CanAfford(ResourceCost cost)
        {
            return Storage.Crystal >= cost.Crystal &&
                   Storage.Biomass >= cost.Biomass &&
                   Storage.Aether >= cost.Aether;
        }

        public void AddGatherer(string name, ResourceType type, int rate)
        {
            var gatherer = new ResourceGatherer(name, type, rate);
            Gatherers.Add(gatherer);
            Console.WriteLine($"Added {name} - gathers {rate} {type} per tick");
        }

        public void RemoveGatherer(string name)
        {
            var gatherer = Gatherers.Find(g => g.Name == name);
            if (gatherer != null)
            {
                Gatherers.Remove(gatherer);
                Console.WriteLine($"Removed gatherer: {name}");
            }
        }

        public void UpgradeStorageCapacity(ResourceType type, int additionalCapacity)
        {
            switch (type)
            {
                case ResourceType.Crystal:
                    Storage.CrystalCapacity += additionalCapacity;
                    Console.WriteLine($"Crystal storage increased to {Storage.CrystalCapacity}");
                    break;
                case ResourceType.Biomass:
                    Storage.BiomassCapacity += additionalCapacity;
                    Console.WriteLine($"Biomass storage increased to {Storage.BiomassCapacity}");
                    break;
                case ResourceType.Aether:
                    Storage.AetherCapacity += additionalCapacity;
                    Console.WriteLine($"Aether storage increased to {Storage.AetherCapacity}");
                    break;
            }
        }

        // Simulate resource gathering over time
        public void Update()
        {
            GameTick++;
            
            foreach (var gatherer in Gatherers)
            {
                int gathered = gatherer.Gather();
                if (gathered > 0)
                {
                    AddResource(gatherer.ResourceType, gathered);
                }
            }
        }

        // Convert lower tier resources to higher tier (like refining)
        public bool ConvertResources(ResourceType from, ResourceType to, int amount)
        {
            // Conversion ratios (inspired by Warcraft 3 wood/gold balance)
            // 10 Crystal -> 1 Biomass
            // 10 Biomass -> 1 Aether
            
            int conversionRatio = 10;
            int inputNeeded = amount * conversionRatio;
            
            if (from == ResourceType.Crystal && to == ResourceType.Biomass)
            {
                if (Storage.Crystal < inputNeeded)
                {
                    Console.WriteLine($"Not enough Crystal! Need {inputNeeded}, have {Storage.Crystal}");
                    return false;
                }
                
                if (!Storage.CanStore(ResourceType.Biomass, amount))
                {
                    Console.WriteLine($"Biomass storage full!");
                    return false;
                }
                
                Storage.Crystal -= inputNeeded;
                Storage.Biomass += amount;
                Console.WriteLine($"Converted {inputNeeded} Crystal -> {amount} Biomass");
                return true;
            }
            else if (from == ResourceType.Biomass && to == ResourceType.Aether)
            {
                if (Storage.Biomass < inputNeeded)
                {
                    Console.WriteLine($"Not enough Biomass! Need {inputNeeded}, have {Storage.Biomass}");
                    return false;
                }
                
                if (!Storage.CanStore(ResourceType.Aether, amount))
                {
                    Console.WriteLine($"Aether storage full!");
                    return false;
                }
                
                Storage.Biomass -= inputNeeded;
                Storage.Aether += amount;
                Console.WriteLine($"Converted {inputNeeded} Biomass -> {amount} Aether");
                return true;
            }
            
            Console.WriteLine("Invalid resource conversion!");
            return false;
        }

        public void DisplayCurrentResources()
        {
            Storage.DisplayResources();
        }

        public void DisplayGatherers()
        {
            Console.WriteLine("\n=== RESOURCE GATHERERS ===");
            foreach (var gatherer in Gatherers)
            {
                string status = gatherer.IsActive ? "Active" : "Inactive";
                Console.WriteLine($"{gatherer.Name} - {gatherer.ResourceType} ({gatherer.GatherRate}/tick) [{status}]");
            }
        }

        public void DisplayFullStatus()
        {
            Console.WriteLine($"\n========== RESOURCE SYSTEM (Tick: {GameTick}) ==========");
            DisplayCurrentResources();
            DisplayGatherers();
        }
    }
}
