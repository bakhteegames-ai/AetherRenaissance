using System;
using System.Collections.Generic;
using System.Linq;

namespace AetherRenaissance.Systems
{
    // Damage types from TDD v1.5.1
    public enum DamageType
    {
        Kinetic,        // > Light Armor
        Aether,         // > Medium Armor
        ArmorPiercing,  // > Heavy Armor
        Siege           // > Fortified - Has Friendly Fire
    }

    public enum ArmorType { Light, Medium, Heavy, Fortified }

    public interface ICombatUnit
    {
        string Name { get; }
        int Health { get; set; }
        int MaxHealth { get; }
        int BaseAttack { get; }
        DamageType AttackType { get; }
        ArmorType ArmorType { get; }
        int Armor { get; }
        bool IsAlive { get; }
        string Team { get; set; }
    }

    public class CombatUnit : ICombatUnit
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; private set; }
        public int BaseAttack { get; set; }
        public DamageType AttackType { get; set; }
        public ArmorType ArmorType { get; set; }
        public int Armor { get; set; }
        public bool IsAlive => Health > 0;
        public string Team { get; set; }

        public CombatUnit(string name, int health, int attack, DamageType damageType, 
                         ArmorType armorType, int armor, string team = "Player")
        {
            Name = name;
            Health = health;
            MaxHealth = health;
            BaseAttack = attack;
            AttackType = damageType;
            ArmorType = armorType;
            Armor = armor;
            Team = team;
        }
    }

    public class CombatSystem
    {
        private Random _random;
        private Dictionary<(DamageType, ArmorType), float> _damageMultipliers;

        public CombatSystem()
        {
            _random = new Random();
            InitializeDamageMultipliers();
        }

        private void InitializeDamageMultipliers()
        {
            _damageMultipliers = new Dictionary<(DamageType, ArmorType), float>
            {
                { (DamageType.Kinetic, ArmorType.Light), 1.5f },
                { (DamageType.Kinetic, ArmorType.Medium), 1.0f },
                { (DamageType.Kinetic, ArmorType.Heavy), 0.75f },
                { (DamageType.Aether, ArmorType.Medium), 1.5f },
                { (DamageType.ArmorPiercing, ArmorType.Heavy), 1.5f },
                { (DamageType.Siege, ArmorType.Fortified), 2.0f }
            };
        }

        public void Attack(ICombatUnit attacker, ICombatUnit defender)
        {
            float mult = _damageMultipliers.GetValueOrDefault((attacker.AttackType, defender.ArmorType), 1.0f);
            int damage = Math.Max(1, (int)(attacker.BaseAttack * mult) - defender.Armor);
            defender.Health = Math.Max(0, defender.Health - damage);
            Console.WriteLine($"{attacker.Name} -> {defender.Name}: {damage} dmg. HP: {defender.Health}/{defender.MaxHealth}");
        }
    }

    public static class UnitFactory
    {
        public static CombatUnit CreateVolt(string team = "Player") => 
            new CombatUnit("Volt", 80, 15, DamageType.Kinetic, ArmorType.Light, 2, team);
        
        public static CombatUnit CreateGuard(string team = "Player") => 
            new CombatUnit("Guard", 150, 12, DamageType.Kinetic, ArmorType.Medium, 8, team);
    }
}
