using System;
using System.Collections.Generic;

namespace AetherRenaissance.Systems
{
    // Hero talent types
    public enum TalentType
    {
        Combat,
        Economy,
        Magic
    }

    // Equipment slots
    public enum EquipmentSlot
    {
        Weapon,
        Armor,
        Accessory
    }

    public class Equipment
    {
        public string Name { get; set; }
        public EquipmentSlot Slot { get; set; }
        public int BonusAttack { get; set; }
        public int BonusDefense { get; set; }
        public int BonusHealth { get; set; }

        public Equipment(string name, EquipmentSlot slot, int attack = 0, int defense = 0, int health = 0)
        {
            Name = name;
            Slot = slot;
            BonusAttack = attack;
            BonusDefense = defense;
            BonusHealth = health;
        }
    }

    public class Talent
    {
        public string Name { get; set; }
        public TalentType Type { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public int MaxLevel { get; set; }

        public Talent(string name, TalentType type, string description, int maxLevel = 3)
        {
            Name = name;
            Type = type;
            Description = description;
            Level = 0;
            MaxLevel = maxLevel;
        }

        public bool CanUpgrade() => Level < MaxLevel;
        
        public void Upgrade()
        {
            if (CanUpgrade())
            {
                Level++;
                Console.WriteLine($"{Name} upgraded to level {Level}");
            }
        }
    }

    public class Hero
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public int ExperienceToNextLevel { get; private set; }
        
        // Base stats
        public int BaseHealth { get; set; }
        public int BaseAttack { get; set; }
        public int BaseDefense { get; set; }
        
        // Current stats (including equipment bonuses)
        public int Health { get; private set; }
        public int MaxHealth { get; private set; }
        public int Attack { get; private set; }
        public int Defense { get; private set; }
        
        public List<Talent> Talents { get; set; }
        public Dictionary<EquipmentSlot, Equipment?> EquippedItems { get; set; }
        public int TalentPoints { get; set; }

        public Hero(string name)
        {
            Name = name;
            Level = 1;
            Experience = 0;
            ExperienceToNextLevel = 100;
            
            BaseHealth = 100;
            BaseAttack = 10;
            BaseDefense = 5;
            
            TalentPoints = 0;
            Talents = new List<Talent>();
            EquippedItems = new Dictionary<EquipmentSlot, Equipment?>
            {
                { EquipmentSlot.Weapon, null },
                { EquipmentSlot.Armor, null },
                { EquipmentSlot.Accessory, null }
            };
            
            RecalculateStats();
        }

        public void AddExperience(int amount)
        {
            Experience += amount;
            Console.WriteLine($"{Name} gained {amount} experience ({Experience}/{ExperienceToNextLevel})");
            
            while (Experience >= ExperienceToNextLevel)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            Experience -= ExperienceToNextLevel;
            Level++;
            ExperienceToNextLevel = (int)(ExperienceToNextLevel * 1.5);
            
            // Increase base stats on level up
            BaseHealth += 20;
            BaseAttack += 2;
            BaseDefense += 1;
            
            TalentPoints++;
            
            RecalculateStats();
            Health = MaxHealth; // Restore health on level up
            
            Console.WriteLine($"*** {Name} reached level {Level}! ***");
            Console.WriteLine($"Health: {MaxHealth}, Attack: {Attack}, Defense: {Defense}");
            Console.WriteLine($"Talent points available: {TalentPoints}");
        }

        public void EquipItem(Equipment equipment)
        {
            if (EquippedItems[equipment.Slot] != null)
            {
                Console.WriteLine($"Unequipped {EquippedItems[equipment.Slot]!.Name}");
            }
            
            EquippedItems[equipment.Slot] = equipment;
            RecalculateStats();
            Console.WriteLine($"{Name} equipped {equipment.Name}");
            Console.WriteLine($"Stats: Health: {MaxHealth}, Attack: {Attack}, Defense: {Defense}");
        }

        public void UnequipItem(EquipmentSlot slot)
        {
            if (EquippedItems[slot] != null)
            {
                Console.WriteLine($"{Name} unequipped {EquippedItems[slot]!.Name}");
                EquippedItems[slot] = null;
                RecalculateStats();
            }
        }

        public void AddTalent(Talent talent)
        {
            Talents.Add(talent);
            Console.WriteLine($"{Name} learned talent: {talent.Name}");
        }

        public bool UpgradeTalent(string talentName)
        {
            var talent = Talents.Find(t => t.Name == talentName);
            if (talent == null)
            {
                Console.WriteLine($"Talent {talentName} not found!");
                return false;
            }

            if (TalentPoints <= 0)
            {
                Console.WriteLine($"No talent points available!");
                return false;
            }

            if (!talent.CanUpgrade())
            {
                Console.WriteLine($"{talentName} is already at max level!");
                return false;
            }

            talent.Upgrade();
            TalentPoints--;
            Console.WriteLine($"Talent points remaining: {TalentPoints}");
            return true;
        }

        private void RecalculateStats()
        {
            // Calculate total stats from base + equipment
            int totalHealth = BaseHealth;
            int totalAttack = BaseAttack;
            int totalDefense = BaseDefense;

            foreach (var equippedItem in EquippedItems.Values)
            {
                if (equippedItem != null)
                {
                    totalHealth += equippedItem.BonusHealth;
                    totalAttack += equippedItem.BonusAttack;
                    totalDefense += equippedItem.BonusDefense;
                }
            }

            MaxHealth = totalHealth;
            Attack = totalAttack;
            Defense = totalDefense;
            
            // If health is not initialized, set it to max
            if (Health == 0)
            {
                Health = MaxHealth;
            }
        }

        public void TakeDamage(int damage)
        {
            int actualDamage = Math.Max(1, damage - Defense);
            Health = Math.Max(0, Health - actualDamage);
            Console.WriteLine($"{Name} took {actualDamage} damage. Health: {Health}/{MaxHealth}");
        }

        public void Heal(int amount)
        {
            Health = Math.Min(MaxHealth, Health + amount);
            Console.WriteLine($"{Name} healed {amount} HP. Health: {Health}/{MaxHealth}");
        }

        public bool IsAlive() => Health > 0;

        public void DisplayStats()
        {
            Console.WriteLine($"\n=== {Name} (Level {Level}) ===");
            Console.WriteLine($"Experience: {Experience}/{ExperienceToNextLevel}");
            Console.WriteLine($"Health: {Health}/{MaxHealth}");
            Console.WriteLine($"Attack: {Attack}");
            Console.WriteLine($"Defense: {Defense}");
            Console.WriteLine($"Talent Points: {TalentPoints}");
            
            Console.WriteLine($"\nEquipped Items:");
            foreach (var slot in EquippedItems)
            {
                if (slot.Value != null)
                {
                    Console.WriteLine($"  {slot.Key}: {slot.Value.Name}");
                }
                else
                {
                    Console.WriteLine($"  {slot.Key}: (empty)");
                }
            }
            
            if (Talents.Count > 0)
            {
                Console.WriteLine($"\nTalents:");
                foreach (var talent in Talents)
                {
                    Console.WriteLine($"  {talent.Name} (Level {talent.Level}/{talent.MaxLevel}) - {talent.Description}");
                }
            }
        }
    }

    public class Unit
    {
        public string Name { get; set; }
        public string UnitType { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Speed { get; set; }

        public Unit(string name, string unitType, int health, int attack, int defense, int speed)
        {
            Name = name;
            UnitType = unitType;
            Health = health;
            MaxHealth = health;
            Attack = attack;
            Defense = defense;
            Speed = speed;
        }

        public bool IsAlive() => Health > 0;

        public void TakeDamage(int damage)
        {
            int actualDamage = Math.Max(1, damage - Defense);
            Health = Math.Max(0, Health - actualDamage);
            Console.WriteLine($"{Name} took {actualDamage} damage. Health: {Health}/{MaxHealth}");
        }
    }

    public class UnitSystem
    {
        public List<Hero> Heroes { get; set; }
        public List<Unit> Units { get; set; }

        public UnitSystem()
        {
            Heroes = new List<Hero>();
            Units = new List<Unit>();
        }

        public Hero CreateHero(string name)
        {
            var hero = new Hero(name);
            Heroes.Add(hero);
            Console.WriteLine($"Hero {name} has been created!");
            return hero;
        }

        public Unit CreateUnit(string name, string unitType, int health, int attack, int defense, int speed)
        {
            var unit = new Unit(name, unitType, health, attack, defense, speed);
            Units.Add(unit);
            Console.WriteLine($"Unit {name} ({unitType}) has been created!");
            return unit;
        }

        public void DisplayAllHeroes()
        {
            Console.WriteLine("\n===== ALL HEROES =====");
            foreach (var hero in Heroes)
            {
                hero.DisplayStats();
            }
        }

        public void DisplayAllUnits()
        {
            Console.WriteLine("\n===== ALL UNITS =====");
            foreach (var unit in Units)
            {
                Console.WriteLine($"{unit.Name} ({unit.UnitType}) - HP: {unit.Health}/{unit.MaxHealth}, ATK: {unit.Attack}, DEF: {unit.Defense}");
            }
        }
    }
}
