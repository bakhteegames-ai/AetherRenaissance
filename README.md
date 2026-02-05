# Aether Renaissance

> **Phase 0 Vertical Slice** - Unity RTS Prototype
> 
> **Status**: 🏗️ Early Development  
> **Engine**: Unity 2022.3+  
> **Genre**: Competitive RTS (Aetherpunk)

---

## 🎯 Overview

**Aether Renaissance** is a competitive RTS inspired by Warcraft 3 and StarCraft, set in an aetherpunk world where technology, clockwork, and cosmic energy replace traditional magic.

**Current Phase**: Phase 0 Vertical Slice  
**Goal**: Playable single-faction demo with core RTS mechanics

---

## ⚙️ Phase 0 Scope

### Golden Concordat (Only Faction)
- **Theme**: Italian Renaissance + Clockwork Engineering
- **Style**: Brass, marble, optics, steam-powered mechanisms
- **Philosophy**: Order through engineering excellence

### Units (Phase 0)
| Unit | Role | HP | Damage | Range | Speed | Cost |
|------|------|----|----|-------|-------|------|
| **Servo** | Worker | 50 | - | - | 3.0 | 50 Solaris |
| **Volt** | Ranged DPS | 70 | 9 | 6.0 | 3.5 | 60 Solaris |
| **Guard** | Tank Melee | 160 | 13 | 1.6 | 3.0 | 90 Solaris |

### Buildings (Phase 0)
- **Barracks**: Trains Volt and Guard units
- **Depot** (planned): Resource storage and worker spawn

### Economy (Phase 0)
- **Solaris**: Main resource (mining, training, building)
- **Starting amount**: 500 Solaris
- **Harvest rate**: +5 Solaris/second per worker
- **Worker carry capacity**: 20 Solaris

> **Note**: Quartz and Overcharge mechanics are planned for Phase 1+

---

## 🚀 Quick Start

### Requirements
- Unity 2022.3 LTS or newer
- Git

### Installation

```bash
# Clone repository
git clone https://github.com/bakhteegames-ai/AetherRenaissance.git
cd AetherRenaissance

# Open in Unity
# File → Open Project → Select AetherRenaissance folder

# Press Play ▶️
```

### First Play
1. **Open Unity** and load the project
2. **Open Scene**: `Assets/Scenes/Phase0_TestMap.unity`
3. **Press Play** ▶️
4. **Controls**:
   - `LMB`: Select units
   - `Drag`: Marquee selection
   - `RMB`: Move/Attack
   - `WASD` or `Edge Scroll`: Camera movement
   - `Mouse Wheel`: Zoom
   - `Ctrl+1-9`: Create control groups
   - `1-9`: Select control groups

---

## 📋 Game Design (MASTER CANON)

### Core Pillars
1. **Decisions > Reactions**: Strategic thinking over APM
2. **High Lethality**: Combat is fast and decisive
3. **Clear Counters**: Rock-paper-scissors unit relationships
4. **Macro > Micro**: Economy and positioning matter most

### Aetherpunk Theme
- ❌ **NO traditional magic** (no wizards, no mana, no spells)
- ✅ **Tech-based powers**: Clockwork, optics, steam, electricity
- ✅ **Cosmic energy**: Aether as scientific phenomenon, not mysticism
- ✅ **Da Vinci engineering**: Brass, gears, Renaissance aesthetics

### Factions (Full Game)
1. **Golden Concordat** (Order/Humans) - Phase 0 ✅
2. **Ferrum** (Industry/Dieselpunk) - Planned
3. **Viridian Coil** (Biotech/Growth) - Planned
4. **Entropy** (Void/Cosmic Horror) - Planned

---

## 📁 Project Structure

```
AetherRenaissance/
├── Assets/
│   ├── Scripts/
│   │   ├── Units/
│   │   │   ├── Unit_Servo.cs
│   │   │   ├── Unit_Volt.cs
│   │   │   └── Unit_Guard.cs
│   │   ├── Buildings/
│   │   │   └── Building_Barracks.cs
│   │   ├── Systems/
│   │   │   ├── ResourceSystem.cs
│   │   │   └── SelectionManager.cs
│   │   └── Bootstrap/
│   │       └── GameBootstrap.cs
│   ├── Prefabs/
│   ├── Scenes/
│   └── UI/
├── ProjectSettings/
├── Packages/
├── docs/
│   └── Aether_Renaissance_Master_Context_v2.txt
└── README.md (this file)
```

---

## 🎮 Controls

### Unit Control (Warcraft 3 Style)
- **Select**: Left Click (LMB)
- **Multi-Select**: Drag box
- **Add to Selection**: Shift + LMB
- **Move**: Right Click (RMB) on ground
- **Attack**: RMB on enemy unit
- **Stop**: S key
- **Hold Position**: H key

### Camera
- **Pan**: WASD or Edge Scroll
- **Zoom**: Mouse Wheel
- **Rotate**: Middle Mouse Button (planned)

### Groups
- **Create Group**: Ctrl + [1-9]
- **Select Group**: [1-9]
- **Add to Group**: Shift + [1-9]
- **Center on Group**: Double-tap [1-9]

---

## 🛠️ Tech Stack

- **Engine**: Unity 2022.3 LTS
- **Language**: C#
- **Navigation**: Unity NavMesh
- **Input**: New Input System (planned)
- **Architecture**: ECS-inspired (modular systems)

---

## 📖 Development Phases

### ✅ Phase 0: Vertical Slice (Current)
- [x] Golden Concordat faction
- [x] 3 unit types (Servo, Volt, Guard)
- [x] Barracks building
- [x] Solaris economy
- [ ] Selection and movement
- [ ] Basic combat
- [ ] Auto-bootstrap on Play

### 🔜 Phase 1: Core Gameplay
- [ ] All Golden Concordat units (12 slots)
- [ ] Quartz resource + Overcharge
- [ ] Damage types (Kinetic, Aether, AP, Siege)
- [ ] Building construction
- [ ] Fog of War

### 🔮 Phase 2: Multi-Faction
- [ ] Ferrum faction
- [ ] Faction-specific mechanics
- [ ] Map variety
- [ ] AI opponent

### 🚀 Phase 3: Competitive
- [ ] All 4 factions
- [ ] Multiplayer
- [ ] Balance patches
- [ ] Ranked mode

---

## 🧠 Design Philosophy

This project follows **MASTER CANON v2.1** - a comprehensive design document that ensures:

1. **Consistency**: All features align with core vision
2. **AI-Friendly**: Clear rules for AI-assisted development
3. **No Scope Creep**: Phase 0 scope is strictly enforced
4. **Tech Over Magic**: Aetherpunk aesthetic maintained

See `docs/Aether_Renaissance_Master_Context_v2.txt` for full design document.

---

## 🤝 Contributing

This is an active development project. If you want to contribute:

1. Read **MASTER CANON** in `docs/`
2. Check open Issues for current tasks
3. Follow Phase 0 scope strictly
4. All code must follow naming convention:
   - `Unit_[Name].cs` for units
   - `Building_[Name].cs` for buildings
   - No generic/abstract class names

---

## 📄 License

MIT License - See [LICENSE](LICENSE) file

---

## 📞 Links

- **Repository**: https://github.com/bakhteegames-ai/AetherRenaissance
- **Issues**: [Report bugs or request features](https://github.com/bakhteegames-ai/AetherRenaissance/issues)
- **Master Canon**: See `docs/Aether_Renaissance_Master_Context_v2.txt`

---

## ⚠️ Important Notes

### For AI Assistants
If you're an AI helping with this project:
- **ALWAYS read MASTER CANON first**
- **Phase 0 scope is STRICT** - no heroes, no magic, no Quartz
- **Use proper naming**: `Unit_Volt`, not `RangedUnit` or `VoltUnit`
- **Aetherpunk only**: No fantasy elements
- **Auto-bootstrap required**: Game must run on Play button

### For Developers
This repository is structured for:
- ✅ Unity development (primary)
- ❌ NOT .NET standalone (legacy code archived)

Legacy .NET prototype has been moved to `archive/dotnet-prototype/`

---

**Last Updated**: February 5, 2026  
**Version**: Phase 0 - Vertical Slice  
**Status**: 🏗️ Active Development
