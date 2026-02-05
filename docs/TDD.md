# Aether Renaissance - Technical Design Document (Phase 0)

**Version:** 2.0 - Phase 0 Vertical Slice  
**Last Updated:** February 5, 2026  
**Project:** Unity RTS Prototype - Aetherpunk Theme  
**Status:** 🚧 Early Development - Phase 0

---

## ⚠️ CRITICAL: Phase 0 Scope Restrictions

This document describes **ONLY Phase 0** - the minimal vertical slice.

### ✅ INCLUDED in Phase 0:
- **Faction:** Golden Concordat ONLY
- **Units:** Servo (worker), Volt (ranged), Guard (tank)
- **Building:** Barracks (trains units)
- **Economy:** Solaris ONLY (no Quartz, no Overcharge)
- **Theme:** Aetherpunk tech/clockwork - NOT fantasy magic

### ❌ NOT in Phase 0:
- Heroes, RPG elements, abilities
- Magic, spells, enchantments
- Crystal/Biomass/Aether resources (OLD - removed)
- Multiple factions
- Quartz resource
- Day/night cycles
- Upkeep systems

---

## 1. EXECUTIVE SUMMARY

Aether Renaissance Phase 0 is a minimal Unity RTS prototype focusing on:

- **Single resource economy** (Solaris only)
- **Unit-centric RTS gameplay** (NO heroes, NO magic)
- **3 basic units + 1 building** (Servo/Volt/Guard + Barracks)
- **Aetherpunk aesthetic** (tech/clockwork/brass - NOT fantasy)
- **Auto-bootstrap** (press Play → game runs)

**Target Platform:** PC (Windows, Linux, macOS)  
**Engine:** Unity 2022.3 LTS  
**Target Audience:** RTS enthusiasts, ages 16+

---

## 2. CORE GAME SYSTEMS

### 2.1 Resource System

#### Solaris (Солярис)
- **Role:** Basic currency for everything in Phase 0
- **Gathering:** Servos gather from Solaris Deposits (placeholder cubes)
- **Uses:** Train units, build Barracks
- **Starting amount:** 500
- **Gather rate:** 10 Solaris/trip
- **No caps** in Phase 0

#### Phase 1+ (NOT Phase 0):
- Quartz (tech resource)
- Overcharge (advanced mechanics)

---

### 2.2 Units (Phase 0)

#### Unit_Servo (Worker)
- **Role:** Gather Solaris, build Barracks
- **Stats:**
  - HP: 50
  - Speed: 5
  - No combat
- **Cost:** 50 Solaris
- **Abilities:** Gather, Build
- **Visual:** Placeholder cube/capsule

#### Unit_Volt (Ranged)
- **Role:** Ranged attacker
- **Stats:**
  - HP: 80
  - Speed: 4.5
  - Damage: 15
  - Range: 8
  - Attack speed: 1.5s
- **Cost:** 100 Solaris
- **Visual:** Placeholder capsule

#### Unit_Guard (Tank)
- **Role:** Melee tank
- **Stats:**
  - HP: 200
  - Speed: 3
  - Damage: 25
  - Range: 1.5 (melee)
  - Attack speed: 2s
- **Cost:** 150 Solaris
- **Visual:** Placeholder cube

---

### 2.3 Buildings (Phase 0)

#### Building_Barracks
- **Role:** Train units (Servo/Volt/Guard)
- **Stats:**
  - HP: 500
  - Build time: 5s
- **Cost:** 200 Solaris
- **Train queue:** Up to 5 units
- **Visual:** Placeholder large cube

---

### 2.4 Faction (Phase 0)

#### Golden Concordat (Золотой Конкордат)
- **Theme:** Clockwork empire, brass & gold aesthetic
- **Philosophy:** Order through automation
- **Tech focus:** Mechanical precision, solar power
- **Units:** Servo/Volt/Guard
- **Color scheme:** Gold/brass/bronze

**Phase 1+:** Other factions (Crimson Dominion, etc.)

---

## 3. TECHNICAL ARCHITECTURE

### 3.1 Project Structure

```
Assets/
├── Scripts/
│   ├── Units/
│   │   ├── Unit_Servo.cs
│   │   ├── Unit_Volt.cs
│   │   └── Unit_Guard.cs
│   ├── Buildings/
│   │   └── Building_Barracks.cs
│   ├── Systems/
│   │   ├── ResourceSystem.cs
│   │   ├── SelectionManager.cs
│   │   └── CameraController.cs
│   └── Bootstrap/
│       └── GameBootstrap.cs
├── Prefabs/
│   ├── Units/
│   └── Buildings/
├── Scenes/
│   └── TestScene.unity
└── UI/
    └── ResourceDisplay.uxml
```

### 3.2 Core Systems

#### ResourceSystem.cs
- Track Solaris amount
- Validate costs
- Update UI

#### SelectionManager.cs
- Click to select units
- Box selection
- Right-click to move/attack

#### CameraController.cs
- WASD movement
- Mouse edge scrolling
- Zoom with scroll wheel

#### GameBootstrap.cs
- Auto-initialize on Play
- Spawn starting Servos
- Set initial Solaris

---

## 4. GAMEPLAY FLOW (Phase 0)

### Core Loop:
1. Press Play → Game auto-starts
2. Select starting Servos
3. Gather Solaris from deposits
4. Build Barracks
5. Train Volt/Guard units
6. Test combat

### Victory Condition (Phase 0):
No formal win/loss - sandbox testing mode

---

## 5. UI/UX (Phase 0)

### HUD Elements:
- Top-left: Solaris counter
- Bottom-center: Unit selection info
- Bottom-right: Build/train buttons (when applicable)

### Controls:
- **Left-click:** Select unit
- **Right-click:** Move/attack
- **Drag:** Box selection
- **WASD:** Camera movement
- **Mouse edge:** Scroll camera
- **Scroll wheel:** Zoom

---

## 6. TECHNICAL REQUIREMENTS

### Minimum:
- **OS:** Windows 10/11, macOS 10.15+, or Linux
- **Unity:** 2022.3 LTS or newer
- **RAM:** 8 GB
- **Storage:** 2 GB free space
- **GPU:** DirectX 11 compatible

### Recommended:
- **Unity:** 2022.3 LTS (tested version)
- **RAM:** 16 GB
- **GPU:** Dedicated graphics card

---

## 7. DEVELOPMENT PRIORITIES

### Phase 0 Milestones:
1. ✅ Core systems (Resource, Selection, Camera)
2. ✅ Basic units (Servo/Volt/Guard)
3. ✅ Barracks building
4. ✅ Placeholder visuals
5. ✅ Auto-bootstrap

### Phase 1 (Future):
- Quartz resource
- More units/buildings
- Combat balance
- Real 3D models
- Sound/music

---

## 8. DESIGN PHILOSOPHY

### Aetherpunk NOT Fantasy:
- Tech/clockwork aesthetic (brass, gears, solar power)
- NO magic spells, enchantments, fantasy elements
- Industrial revolution meets advanced tech

### Unit-Centric RTS:
- NO heroes with RPG progression
- Focus on army composition
- Traditional RTS mechanics (StarCraft/WC3-style)

### Solaris Economy:
- Phase 0: Solaris only (simple)
- Phase 1+: Add Quartz (complexity)

---

## 9. OUT OF SCOPE (Phase 0)

❌ Heroes, abilities, magic  
❌ Multiple factions  
❌ Quartz resource  
❌ Day/night cycles  
❌ Upkeep systems  
❌ Story/campaign  
❌ Multiplayer  
❌ Advanced AI  
❌ Polished graphics/audio  

---

## 10. TESTING CHECKLIST

### Phase 0 Must Work:
- [ ] Press Play → game starts
- [ ] Select Servo → gather Solaris
- [ ] Build Barracks
- [ ] Train Volt → attack enemy
- [ ] Train Guard → tank damage
- [ ] UI shows Solaris count
- [ ] Camera controls work

---

**Document Status:** Phase 0 Complete  
**Next Update:** After Phase 0 vertical slice completion  
**Contact:** AetherRenaissance dev team
