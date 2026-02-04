using System;
using AetherRenaissance.Systems;

namespace AetherRenaissance
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("======================================");
            Console.WriteLine("    AETHER RENAISSANCE - RTS GAME     ");
            Console.WriteLine("======================================\n");
            
            // Demo the game systems
            DemoResourceSystem();
            DemoHeroSystem();
            DemoIntegratedGameplay();
            
            Console.WriteLine("\n\n======================================");
            Console.WriteLine("      DEMO COMPLETED SUCCESSFULLY     ");
            Console.WriteLine("======================================");
        }

        static void DemoResourceSystem()
        {
            Console.WriteLine("\n\n===========================================" );
            Console.WriteLine("   DEMO 1: THREE-TIER RESOURCE SYSTEM");
            Console.WriteLine("===========================================\n");
            
            var resourceSystem = new ResourceSystem();
            
            // Add initial resources
            Console.WriteLine("--- Initial Setup ---");
            resourceSystem.AddResource(ResourceType.Crystal, 100);
            resourceSystem.AddResource(ResourceType.Biomass, 20);
            resourceSystem.AddResource(ResourceType.Aether, 5);
            resourceSystem.DisplayCurrentResources();
            
            // Add resource gatherers
            Console.WriteLine("\n--- Setting up Gatherers ---");
            resourceSystem.AddGatherer("Crystal Mine #1", ResourceType.Crystal, 10);
            resourceSystem.AddGatherer("Crystal Mine #2", ResourceType.Crystal, 10);
            resourceSystem.AddGatherer("Biomass Harvester", ResourceType.Biomass, 5);
            resourceSystem.AddGatherer("Aether Extractor", ResourceType.Aether, 2);
            
            // Simulate gathering over 3 ticks
            Console.WriteLine("\n--- Simulating 3 ticks of gathering ---");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"\n--- Tick {i + 1} ---");
                resourceSystem.Update();
            }
            
            resourceSystem.DisplayFullStatus();
            
            // Test resource conversion
            Console.WriteLine("\n--- Testing Resource Conversion ---");
            Console.WriteLine("Converting 50 Crystal to Biomass (10:1 ratio)...");
            resourceSystem.ConvertResources(ResourceType.Crystal, ResourceType.Biomass, 5);
            
            Console.WriteLine("\nConverting 30 Biomass to Aether (10:1 ratio)...");
            resourceSystem.ConvertResources(ResourceType.Biomass, ResourceType.Aether, 3);
            
            resourceSystem.DisplayCurrentResources();
            
            // Test spending resources
            Console.WriteLine("\n--- Testing Resource Spending ---");
            var buildCost = new ResourceCost(crystal: 50, biomass: 10, aether: 2);
            Console.WriteLine($"\nBuilding structure that costs: {buildCost}");
            resourceSystem.SpendResources(buildCost);
            resourceSystem.DisplayCurrentResources();
        }

        static void DemoHeroSystem()
        {
            Console.WriteLine("\n\n===========================================" );
            Console.WriteLine("      DEMO 2: HERO DEVELOPMENT SYSTEM");
            Console.WriteLine("===========================================\n");
            
            var unitSystem = new UnitSystem();
            
            // Create a hero
            Console.WriteLine("--- Creating Hero ---");
            var hero = unitSystem.CreateHero("Arthas");
            hero.DisplayStats();
            
            // Add talents
            Console.WriteLine("\n--- Learning Talents ---");
            var combatTalent = new Talent("Blade Master", TalentType.Combat, 
                "Increases attack damage by 20%", 3);
            var economyTalent = new Talent("Resource Manager", TalentType.Economy, 
                "Increases resource gathering by 15%", 3);
            var magicTalent = new Talent("Aether Channeling", TalentType.Magic, 
                "Increases mana regeneration", 3);
            
            hero.AddTalent(combatTalent);
            hero.AddTalent(economyTalent);
            hero.AddTalent(magicTalent);
            
            // Equip items
            Console.WriteLine("\n--- Equipping Items ---");
            var sword = new Equipment("Frostmourne", EquipmentSlot.Weapon, attack: 25);
            var armor = new Equipment("Plate Armor", EquipmentSlot.Armor, defense: 15, health: 50);
            var ring = new Equipment("Ring of Power", EquipmentSlot.Accessory, attack: 5, defense: 5, health: 20);
            
            hero.EquipItem(sword);
            hero.EquipItem(armor);
            hero.EquipItem(ring);
            
            // Gain experience and level up
            Console.WriteLine("\n--- Gaining Experience ---");
            hero.AddExperience(80);
            Console.WriteLine();
            hero.AddExperience(50); // Should level up
            
            // Upgrade talents
            Console.WriteLine("\n--- Upgrading Talents ---");
            hero.UpgradeTalent("Blade Master");
            hero.UpgradeTalent("Aether Channeling");
            
            // Display final stats
            hero.DisplayStats();
            
            // Test combat
            Console.WriteLine("\n--- Testing Combat ---");
            hero.TakeDamage(30);
            hero.Heal(20);
        }

        static void DemoIntegratedGameplay()
        {
            Console.WriteLine("\n\n===========================================" );
            Console.WriteLine("     DEMO 3: INTEGRATED GAMEPLAY");
            Console.WriteLine("===========================================\n");
            
            Console.WriteLine("Starting a complete game scenario...\n");
            
            // Initialize all systems
            var resourceSystem = new ResourceSystem();
            var unitSystem = new UnitSystem();
            
            // Setup starting resources
            Console.WriteLine("--- Initial Resources ---");
            resourceSystem.AddResource(ResourceType.Crystal, 200);
            resourceSystem.AddResource(ResourceType.Biomass, 50);
            resourceSystem.AddResource(ResourceType.Aether, 10);
            
            // Create player hero
            Console.WriteLine("\n--- Creating Player Hero ---");
            var playerHero = unitSystem.CreateHero("Lord Commander");
            
            // Add starting equipment
            playerHero.EquipItem(new Equipment("Starting Sword", EquipmentSlot.Weapon, attack: 10));
            playerHero.EquipItem(new Equipment("Leather Armor", EquipmentSlot.Armor, defense: 8, health: 30));
            
            // Add resource gatherers (base economy)
            Console.WriteLine("\n--- Building Economy ---");
            resourceSystem.AddGatherer("Main Base - Crystal", ResourceType.Crystal, 15);
            resourceSystem.AddGatherer("Main Base - Biomass", ResourceType.Biomass, 8);
            
            // Simulate early game (5 ticks)
            Console.WriteLine("\n--- Early Game Phase (5 ticks) ---");
            for (int i = 0; i < 5; i++)
            {
                resourceSystem.Update();
            }
            resourceSystem.DisplayCurrentResources();
            
            // Build advanced structures (spend resources)
            Console.WriteLine("\n--- Mid Game: Building Advanced Structures ---");
            var aetherExtractorCost = new ResourceCost(crystal: 100, biomass: 30, aether: 5);
            Console.WriteLine($"Building Aether Extractor (Cost: {aetherExtractorCost})...");
            
            if (resourceSystem.SpendResources(aetherExtractorCost))
            {
                resourceSystem.AddGatherer("Aether Extractor", ResourceType.Aether, 3);
                Console.WriteLine("Successfully built Aether Extractor!");
            }
            
            // Hero gains experience from quests/battles
            Console.WriteLine("\n--- Hero Progression ---");
            Console.WriteLine("Hero completes quests and battles...");
            playerHero.AddExperience(120); // Levels up
            
            // Unlock and upgrade talent
            var leadershipTalent = new Talent("Leadership", TalentType.Combat, 
                "Nearby units gain +10% attack", 5);
            playerHero.AddTalent(leadershipTalent);
            playerHero.UpgradeTalent("Leadership");
            
            // Create army units
            Console.WriteLine("\n--- Building Army ---");
            var warrior1 = unitSystem.CreateUnit("Warrior #1", "Infantry", 150, 20, 10, 5);
            var warrior2 = unitSystem.CreateUnit("Warrior #2", "Infantry", 150, 20, 10, 5);
            var archer1 = unitSystem.CreateUnit("Archer #1", "Ranged", 100, 25, 5, 7);
            
            // Simulate late game gathering
            Console.WriteLine("\n--- Late Game Phase (3 ticks) ---");
            for (int i = 0; i < 3; i++)
            {
                resourceSystem.Update();
            }
            
            // Final status
            Console.WriteLine("\n--- FINAL GAME STATE ---");
            resourceSystem.DisplayFullStatus();
            Console.WriteLine();
            unitSystem.DisplayAllHeroes();
            Console.WriteLine();
            unitSystem.DisplayAllUnits();
            
            Console.WriteLine("\n--- Game Statistics ---");
            Console.WriteLine($"Total Game Ticks: {resourceSystem.GameTick}");
            Console.WriteLine($"Heroes Created: {unitSystem.Heroes.Count}");
            Console.WriteLine($"Units Created: {unitSystem.Units.Count}");
            Console.WriteLine($"Resource Gatherers: {resourceSystem.Gatherers.Count}");
            Console.WriteLine($"\nPlayer is ready for conquest!");
        }
    }
}
