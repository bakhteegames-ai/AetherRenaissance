# Aether Renaissance

A real-time strategy (RTS) game inspired by Warcraft 3, featuring strategic resource management, hero development, and tactical combat.

## 🎮 Overview

Aether Renaissance combines classic RTS mechanics with innovative resource and hero management systems. Players must balance economic development, military strategy, and hero progression to achieve victory.

## ✨ Key Features

### Core Gameplay

**Three-Tier Resource System**
- **Crystal** (Tier 1): Basic resource gathered from mines
- **Biomass** (Tier 2): Intermediate resource from biological sources
- **Aether** (Tier 3): Advanced mystical resource
- Resource conversion system (10:1 ratio between tiers)
- Dynamic resource gathering and storage management

**Hero Development System**
- Experience-based leveling with progressive stat increases
- Talent tree with Combat, Economy, and Magic specializations
- Equipment system with weapons, armor, and accessories
- Dynamic stat calculation based on base stats + equipment bonuses

**Unit Control**
- Traditional RTS unit control with formations
- Unit creation and army management
- Tactical abilities based on unit types

**Base Building**
- Strategic placement of structures and defenses
- Building upgrades and technology progression

### Game Mechanics

**Resource Management**
- Automatic resource gathering through collectors
- Storage capacity limits requiring strategic upgrades
- Resource costs for units, buildings, and upgrades

**Hero Progression**
- Gain experience through combat and quests
- Unlock and upgrade talents with talent points
- Equip items to enhance hero capabilities
- Level-based stat scaling

**Combat System**
- Damage calculation with attack and defense stats
- Unit health and healing mechanics
- Tactical positioning and formations

## 🚀 Getting Started

### Prerequisites

- .NET 6.0 SDK or later
- C# development environment (Visual Studio, VS Code, or Rider)

### Installation

1. Clone the repository:
```bash
git clone https://github.com/bakhteegames-ai/AetherRenaissance.git
cd AetherRenaissance
```

2. Build the project:
```bash
dotnet build
```

3. Run the demo:
```bash
dotnet run --project .
```

## 📁 Project Structure

```
AetherRenaissance/
├── src/
│   ├── Systems/
│   │   ├── UnitSystem.cs       # Hero and unit management
│   │   └── ResourceSystem.cs   # Three-tier resource economy
│   └── Program.cs              # Game demo and examples
├── docs/
│   └── TDD.md                  # Technical Design Document
├── AetherRenaissance.csproj    # Project configuration
└── README.md                   # This file
```

## 🎯 Game Systems

### Resource System

The resource system manages the three-tier economy:

```csharp
var resourceSystem = new ResourceSystem();
resourceSystem.AddResource(ResourceType.Crystal, 100);
resourceSystem.AddGatherer("Crystal Mine", ResourceType.Crystal, 10);
resourceSystem.Update(); // Simulate gathering
resourceSystem.ConvertResources(ResourceType.Crystal, ResourceType.Biomass, 5);
```

### Hero System

Heroes are the core of your strategy:

```csharp
var hero = unitSystem.CreateHero("Commander");
hero.AddExperience(120); // Gain levels

var sword = new Equipment("Legendary Sword", EquipmentSlot.Weapon, attack: 25);
hero.EquipItem(sword);

var talent = new Talent("Blade Master", TalentType.Combat, "Increases damage", 3);
hero.AddTalent(talent);
hero.UpgradeTalent("Blade Master");
```

## 🎬 Demo Examples

The program includes three comprehensive demos:

1. **Resource System Demo**: Showcases resource gathering, conversion, and spending
2. **Hero System Demo**: Demonstrates hero progression, equipment, and talents
3. **Integrated Gameplay**: Simulates a complete game scenario from early to late game

Run the program to see all systems in action!

## 🛠️ Technical Stack

- **Engine**: C# / .NET 6.0
- **Architecture**: Object-oriented with modular systems
- **Design**: Inspired by Warcraft 3 and modern RTS games

## 📋 Roadmap

- [x] Core resource system with three-tier economy
- [x] Hero development with experience and leveling
- [x] Equipment and talent systems
- [x] Unit creation and management
- [ ] Building system with construction and upgrades
- [ ] Combat system with tactical abilities
- [ ] Map system with fog of war
- [ ] Multiplayer support
- [ ] Campaign mode
- [ ] Visual UI and graphics engine integration

## 🤝 Contributing

Contributions are welcome! This is an active development project.

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🙏 Acknowledgments

- Inspired by Warcraft 3's game mechanics
- Based on classic RTS design principles
- Created as a demonstration of RTS game architecture

## 📞 Contact

For questions or feedback, please open an issue on GitHub.

---

**Status**: 🏗️ In Active Development

The game currently features working resource and hero systems with comprehensive demos. Combat and building systems are planned for upcoming releases.
