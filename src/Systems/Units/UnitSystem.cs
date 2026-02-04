using System;
using System.Collections.Generic;

namespace AetherRenaissance.Systems.Units
{
    /// <summary>
    /// Base class for all units (Workers, Soldiers, Heroes)
    /// </summary>
    public abstract class Unit
    {
        public int Id { get; protected set; }
        public int PlayerId { get; protected set; }
        public string Name { get; protected set; }
        
        // Core Stats
        public float MaxHP { get; protected set; }
        public float CurrentHP { get; protected set; }
        public float MovementSpeed { get; protected set; }
        public float VisionRange { get; protected set; }
        
        // Combat Stats
        public float AttackDamage { get; protected set; }
        public float AttackRange { get; protected set; }
        public float AttackSpeed { get; protected set; }
        public ArmorType ArmorType { get; protected set; }
        public DamageType DamageType { get; protected set; }
        
        // State
        public bool IsAlive => CurrentHP > 0;
        public UnitState State { get; protected set; }
        
        public Unit(int id, int playerId, string name)
        {
            Id = id;
            PlayerId = playerId;
            Name = name;
            State = UnitState.Idle;
        }
        
        public virtual void TakeDamage(float amount, DamageType type)
        {
            // TODO: Calculate damage reduction based on armor
            CurrentHP -= amount;
            if (CurrentHP <= 0) Die();
        }
        
        protected virtual void Die()
        {
            CurrentHP = 0;
            State = UnitState.Dead;
            Console.WriteLine($"{Name} (ID: {Id}) has died.");
        }
    }

    /// <summary>
    /// Worker unit for resource gathering and building
    /// </summary>
    public class Worker : Unit
    {
        public int CarryingAmount { get; private set; }
        public Resources.ResourceType? CarryingType { get; private set; }
        
        public Worker(int id, int playerId) : base(id, playerId, "Worker")
        {
            MaxHP = 250;
            CurrentHP = MaxHP;
            MovementSpeed = 3.0f;
            VisionRange = 8.0f;
            AttackDamage = 5;
            AttackRange = 1.0f;
            AttackSpeed = 1.5f;
            ArmorType = ArmorType.Light;
            DamageType = DamageType.Physical;
        }
        
        public void Gather(Resources.ResourceSource source)
        {
            if (CarryingAmount > 0) return; // Must drop off first
            
            State = UnitState.Gathering;
            // Logic for gathering handled by ResourceManager
        }
    }

    /// <summary>
    /// Hero unit with levels, attributes, and inventory
    /// </summary>
    public class Hero : Unit
    {
        public int Level { get; private set; } = 1;
        public float Experience { get; private set; } = 0;
        
        // Attributes (from TDD)
        public int Strength { get; private set; }
        public int Agility { get; private set; }
        public int Intelligence { get; private set; }
        
        public List<Ability> Abilities { get; private set; } = new List<Ability>();
        public Item[] Inventory { get; private set; } = new Item[6];
        
        public Hero(int id, int playerId, string name) : base(id, playerId, name)
        {
            MaxHP = 700;
            CurrentHP = MaxHP;
            MovementSpeed = 4.0f;
            VisionRange = 12.0f;
            Level = 1;
        }
        
        public void GainXP(float amount)
        {
            Experience += amount;
            if (Experience >= GetXPForNextLevel()) LevelUp();
        }
        
        private void LevelUp()
        {
            if (Level >= 10) return;
            Level++;
            // TODO: Increase stats and grant ability point
            Console.WriteLine($"{Name} leveled up to {Level}!");
        }
        
        private float GetXPForNextLevel() => Level * 200; // Simplified
    }

    public enum UnitState { Idle, Moving, Attacking, Gathering, Constructing, Dead }
    public enum ArmorType { Light, Medium, Heavy, Fortified, Hero }
    public enum DamageType { Physical, Magic, Pure, Siege, Chaos }
    
    public class Ability { /* Placeholder */ }
    public class Item { /* Placeholder */ }
}
