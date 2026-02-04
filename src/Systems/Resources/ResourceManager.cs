using System;
using System.Collections.Generic;

namespace AetherRenaissance.Systems.Resources
{
    /// <summary>
    /// Resource types in the game
    /// </summary>
    public enum ResourceType
    {
        Crystal,  // Primary resource (equivalent to Gold in WC3)
        Biomass,  // Secondary resource (equivalent to Lumber in WC3)
        Aether    // Tertiary premium resource
    }

    /// <summary>
    /// Player's resource storage with caps and income modifiers
    /// </summary>
    public class PlayerResources
    {
        // Current amounts
        public int Crystal { get; private set; }
        public int Biomass { get; private set; }
        public int Aether { get; private set; }
        
        // Resource caps (from TDD)
        public const int MaxCrystal = 10000;
        public const int MaxBiomass = 5000;
        public const int MaxAether = 1000;
        
        // Starting amounts (from TDD)
        public const int StartingCrystal = 500;
        public const int StartingBiomass = 150;
        public const int StartingAether = 0;
        
        // Gather rates per trip (from TDD)
        public const int CrystalGatherRate = 10;
        public const int BiomassGatherRate = 5;
        public const int AetherGatherRate = 3; // 2-3 from TDD, using 3
        
        // Upkeep modifier (affects income)
        public float UpkeepModifier { get; private set; } = 1.0f;
        
        public PlayerResources()
        {
            Crystal = StartingCrystal;
            Biomass = StartingBiomass;
            Aether = StartingAether;
        }
        
        /// <summary>
        /// Add resources with cap check
        /// </summary>
        public bool AddResource(ResourceType type, int amount)
        {
            amount = (int)(amount * UpkeepModifier); // Apply upkeep penalty
            
            switch (type)
            {
                case ResourceType.Crystal:
                    Crystal = Math.Min(Crystal + amount, MaxCrystal);
                    return true;
                case ResourceType.Biomass:
                    Biomass = Math.Min(Biomass + amount, MaxBiomass);
                    return true;
                case ResourceType.Aether:
                    Aether = Math.Min(Aether + amount, MaxAether);
                    return true;
                default:
                    return false;
            }
        }
        
        /// <summary>
        /// Spend resources (e.g., for units, buildings, upgrades)
        /// </summary>
        public bool SpendResources(int crystal, int biomass, int aether)
        {
            if (Crystal >= crystal && Biomass >= biomass && Aether >= aether)
            {
                Crystal -= crystal;
                Biomass -= biomass;
                Aether -= aether;
                return true;
            }
            return false;
        }
        
        /// <summary>
        /// Check if player can afford something
        /// </summary>
        public bool CanAfford(int crystal, int biomass, int aether)
        {
            return Crystal >= crystal && Biomass >= biomass && Aether >= aether;
        }
        
        /// <summary>
        /// Update upkeep modifier based on unit count (from TDD)
        /// No Supply: 0-40 units = 100% income
        /// Low Upkeep: 41-70 units = 70% income
        /// High Upkeep: 71+ units = 40% income
        /// </summary>
        public void UpdateUpkeep(int totalUnitCount)
        {
            if (totalUnitCount <= 40)
            {
                UpkeepModifier = 1.0f; // 100% income
            }
            else if (totalUnitCount <= 70)
            {
                UpkeepModifier = 0.7f; // 70% income
            }
            else
            {
                UpkeepModifier = 0.4f; // 40% income
            }
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
    }
    
    /// <summary>
    /// Resource source (mine, extractor, well)
    /// </summary>
    public class ResourceSource
    {
        public ResourceType Type { get; private set; }
        public int RemainingAmount { get; private set; }
        public int GatherRate { get; private set; }
        public bool IsNeutral { get; private set; } // For Aether Wells
        public int OwnerId { get; private set; } = -1; // -1 = unclaimed
        
        private int maxWorkers = 3; // Diminishing returns after 3 workers (from TDD)
        private List<int> assignedWorkers = new List<int>();
        
        public ResourceSource(ResourceType type, int amount, bool isNeutral = false)
        {
            Type = type;
            RemainingAmount = amount;
            IsNeutral = isNeutral;
            
            GatherRate = type switch
            {
                ResourceType.Crystal => PlayerResources.CrystalGatherRate,
                ResourceType.Biomass => PlayerResources.BiomassGatherRate,
                ResourceType.Aether => PlayerResources.AetherGatherRate,
                _ => 0
            };
        }
        
        /// <summary>
        /// Gather resources from this source
        /// </summary>
        public int Gather()
        {
            if (RemainingAmount <= 0) return 0;
            
            int amount = Math.Min(GatherRate, RemainingAmount);
            RemainingAmount -= amount;
            return amount;
        }
        
        /// <summary>
        /// Assign worker to this source
        /// </summary>
        public bool AssignWorker(int workerId)
        {
            if (assignedWorkers.Count >= maxWorkers)
                return false; // Diminishing returns kick in
                
            assignedWorkers.Add(workerId);
            return true;
        }
        
        public void RemoveWorker(int workerId)
        {
            assignedWorkers.Remove(workerId);
        }
        
        /// <summary>
        /// Capture neutral resource (Aether Well)
        /// </summary>
        public bool Capture(int playerId)
        {
            if (!IsNeutral) return false;
            
            OwnerId = playerId;
            return true;
        }
        
        public bool IsDepleted => RemainingAmount <= 0;
        public int WorkerCount => assignedWorkers.Count;
    }
    
    /// <summary>
    /// Main Resource Manager - handles all resource operations
    /// </summary>
    public class ResourceManager
    {
        private Dictionary<int, PlayerResources> playerResources = new Dictionary<int, PlayerResources>();
        private List<ResourceSource> sources = new List<ResourceSource>();
        
        // Resource balance values (from TDD)
        // 1 Crystal ≈ 2 Biomass ≈ 10 Aether (relative value)
        public const float CrystalValue = 1.0f;
        public const float BiomassValue = 0.5f;
        public const float AetherValue = 0.1f;
        
        public ResourceManager()
        {
            Console.WriteLine("ResourceManager initialized");
        }
        
        /// <summary>
        /// Initialize player resources
        /// </summary>
        public void InitializePlayer(int playerId)
        {
            if (!playerResources.ContainsKey(playerId))
            {
                playerResources[playerId] = new PlayerResources();
                Console.WriteLine($"Player {playerId} resources initialized: " +
                    $"{PlayerResources.StartingCrystal} Crystal, " +
                    $"{PlayerResources.StartingBiomass} Biomass, " +
                    $"{PlayerResources.StartingAether} Aether");
            }
        }
        
        /// <summary>
        /// Create a resource source on the map
        /// </summary>
        public ResourceSource CreateSource(ResourceType type, int amount, bool isNeutral = false)
        {
            var source = new ResourceSource(type, amount, isNeutral);
            sources.Add(source);
            return source;
        }
        
        /// <summary>
        /// Worker gathers from source
        /// </summary>
        public void GatherResource(int playerId, ResourceSource source)
        {
            if (!playerResources.ContainsKey(playerId)) return;
            
            int amount = source.Gather();
            if (amount > 0)
            {
                playerResources[playerId].AddResource(source.Type, amount);
            }
        }
        
        /// <summary>
        /// Get player resources
        /// </summary>
        public PlayerResources GetPlayerResources(int playerId)
        {
            if (!playerResources.ContainsKey(playerId))
            {
                InitializePlayer(playerId);
            }
            return playerResources[playerId];
        }
        
        /// <summary>
        /// Update all players' upkeep based on their unit counts
        /// </summary>
        public void UpdateAllUpkeep(Dictionary<int, int> playerUnitCounts)
        {
            foreach (var kvp in playerUnitCounts)
            {
                int playerId = kvp.Key;
                int unitCount = kvp.Value;
                
                if (playerResources.ContainsKey(playerId))
                {
                    playerResources[playerId].UpdateUpkeep(unitCount);
                }
            }
        }
        
        /// <summary>
        /// Calculate total resource value for economic victory condition
        /// From TDD: 10,000 Crystal + 5,000 Biomass + 500 Aether
        /// </summary>
        public float CalculateTotalValue(int playerId)
        {
            if (!playerResources.ContainsKey(playerId)) return 0;
            
            var resources = playerResources[playerId];
            return resources.Crystal * CrystalValue +
                   resources.Biomass * BiomassValue +
                   resources.Aether * AetherValue;
        }
        
        /// <summary>
        /// Check if player has achieved economic victory
        /// </summary>
        public bool CheckEconomicVictory(int playerId)
        {
            if (!playerResources.ContainsKey(playerId)) return false;
            
            var resources = playerResources[playerId];
            return resources.Crystal >= 10000 &&
                   resources.Biomass >= 5000 &&
                   resources.Aether >= 500;
        }
        
        public List<ResourceSource> GetAllSources() => sources;
    }
}
