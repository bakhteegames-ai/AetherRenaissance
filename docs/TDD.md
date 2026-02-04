# Aether Renaissance - Technical Design Document
**Version:** 1.5.1
**Last Updated:** January 2025
**Project:** Real-Time Strategy Game

---

## 1. EXECUTIVE SUMMARY

Aether Renaissance is a real-time strategy game inspired by Warcraft 3, featuring:
- Three-tier resource system (Crystal, Biomass, Aether)
- Hero-centric gameplay with RPG elements
- Strategic base building and army management
- Day/Night cycle affecting gameplay
- Upkeep system balancing army sizes

**Target Platform:** PC (Windows, Linux, macOS)
**Engine:** Unity/Godot (TBD)
**Target Audience:** RTS enthusiasts, ages 16+

---

## 2. CORE GAME SYSTEMS

### 2.1 Resource System

#### Primary Resources
1. **Crystal** (Кристалл)
   - Basic resource, equivalent to Gold in WC3
   - Gathered from Crystal Mines
   - Used for: Basic units, structures, upgrades
   - Starting amount: 500
   - Gather rate: 10 Crystal/trip

2. **Biomass** (Биомасса)
   - Secondary resource, equivalent to Lumber in WC3
   - Gathered from Biomass Extractors
   - Used for: Advanced units, upgrades, special buildings
   - Starting amount: 150
   - Gather rate: 5 Biomass/trip

3. **Aether** (Эфир)
   - Tertiary resource for hero items and powerful units
   - Limited availability on map
   - Gathered from Aether Wells (neutral structures)
   - Used for: Hero items, ultimate abilities, special units
   - Starting amount: 0
   - Gather rate: 2-3 Aether/trip

### 2.2 Hero System

#### Hero Mechanics
- **Experience:** Heroes gain XP from kills and quests
- **Level Cap:** Level 10
- **Attributes:** Strength, Agility, Intelligence
- **Abilities:** 4 active abilities + 1 ultimate (unlocks at level 6)
- **Talents:** Unlock at levels 3, 5, 7, 9
- **Inventory:** 6 item slots

#### Hero Progression
- Level 1: Base stats, 1 ability point
- Level 2-5: +1 ability point per level
- Level 6: Ultimate ability unlocked
- Level 7-10: +1 ability point per level

### 2.3 Unit System

#### Unit Categories
1. **Workers** - Resource gathering, building construction
2. **Basic Units** - Cost: Crystal only
3. **Advanced Units** - Cost: Crystal + Biomass
4. **Elite Units** - Cost: Crystal + Biomass + Aether
5. **Heroes** - Special recruitment requirements

#### Supply/Upkeep System
- **No Supply:** 0-40 units = 100% resource income
- **Low Upkeep:** 41-70 units = 70% resource income
- **High Upkeep:** 71+ units = 40% resource income

### 2.4 Building System

#### Core Buildings
- **Main Structure** (Главное здание)
  - HP: 2500
  - Produces workers
  - Stores resources
  - Drop-off point

- **Barracks** (Казармы)
  - Produces basic military units
  - Unlocks unit upgrades

- **Workshop** (Мастерская)
  - Produces advanced units
  - Requires Biomass

- **Hero Altar** (Алтарь Героев)
  - Recruits heroes
  - Revives fallen heroes

### 2.5 Combat System

#### Damage Types
- **Physical Damage:** Standard melee/ranged attacks
- **Magic Damage:** Spell damage, ignores armor
- **Pure Damage:** Ignores all resistances

#### Armor Types
- **Light Armor:** +bonus vs magic, vulnerable to pierce
- **Medium Armor:** Balanced defense
- **Heavy Armor:** High physical defense, weak to magic

---

## 3. MAP & ENVIRONMENT

### 3.1 Fog of War
- **Unexplored:** Black fog
- **Explored:** Gray fog (terrain visible, units hidden)
- **Vision:** Full visibility around your units/buildings

### 3.2 Day/Night Cycle
- **Day:** 8 minutes (480 seconds)
- **Night:** 4 minutes (240 seconds)
- **Effects:**
  - Night: -40% vision range
  - Night: Stealth units gain +25% movement speed
  - Day: Markets offer better exchange rates

### 3.3 Neutral Elements

#### Creep Camps
- Small: 1-3 units, rewards: 50-100 XP, small item
- Medium: 3-5 units, rewards: 150-300 XP, medium item
- Large: 5-8 units, rewards: 400-600 XP, rare item

#### Aether Wells
- Neutral structures providing Aether
- Can be captured and controlled
- 3 Aether per gathering trip

---

## 4. VICTORY CONDITIONS

### Standard Victory
1. **Elimination:** Destroy all enemy main structures
2. **Domination:** Control 75%+ of map objectives for 5 minutes
3. **Economic:** Accumulate 10,000 Crystal + 5,000 Biomass + 500 Aether

### Alternative Modes (Future)
- King of the Hill
- Capture the Flag
- Survival/Defense
- Campaign missions

---

## 5. UI/UX REQUIREMENTS

### Core UI Elements
- **Resource Bar:** Display Crystal, Biomass, Aether, Supply
- **Mini-map:** Show explored terrain, units, objectives
- **Unit Panel:** Selected unit stats, abilities, commands
- **Hero Panel:** Quick access to hero abilities and inventory
- **Building Queue:** Show production progress

### Control Groups
- Support up to 12 control groups (1-9, 0, -, =)
- Double-tap to center camera on group

---

## 6. TECHNICAL REQUIREMENTS

### Performance Targets
- **Frame Rate:** 60 FPS minimum on medium settings
- **Unit Count:** Support 200+ units simultaneously
- **Map Size:** Up to 256x256 tiles
- **Pathfinding:** A* or Flow Field algorithm

### Network (Multiplayer)
- P2P or dedicated server architecture
- Support 2-8 players
- Replay system
- Anti-cheat measures

---

## 7. DEVELOPMENT ROADMAP

### Phase 1: Core Systems (Months 1-3)
- [ ] Resource system implementation
- [ ] Basic unit control and pathfinding
- [ ] Simple combat system
- [ ] Map editor prototype

### Phase 2: Advanced Features (Months 4-6)
- [ ] Hero system with abilities
- [ ] Building construction system
- [ ] Day/Night cycle
- [ ] Fog of War implementation

### Phase 3: Content Creation (Months 7-9)
- [ ] 3+ playable factions
- [ ] 20+ unit types
- [ ] 10+ hero characters
- [ ] 5+ maps

### Phase 4: Polish & Testing (Months 10-12)
- [ ] Balance testing
- [ ] Bug fixing
- [ ] Performance optimization
- [ ] Multiplayer testing

---

## 8. DESIGN CONSTRAINTS

### Must Have
- Clear visual feedback for all actions
- Responsive controls (< 100ms input lag)
- Readable unit silhouettes
- Color-blind friendly UI
- Hotkey customization

### Should Have
- Replay system
- Spectator mode
- Custom game lobbies
- Map editor

### Could Have
- Campaign mode
- Cinematics
- Voice acting
- Custom unit skins

---

## 9. BALANCE GUIDELINES

### Resource Balance
- 1 Crystal ≈ 2 Biomass ≈ 10 Aether (relative value)
- Workers pay for themselves in 60 seconds
- First hero available at 3:00 game time

### Unit Balance
- Basic units: 30-60 seconds build time
- Advanced units: 60-90 seconds build time
- Heroes: 120 seconds recruitment time
- Average game length: 20-30 minutes

### Economy Guardrails
- Maximum workers: 20 per player
- Resource cap: 10,000 Crystal, 5,000 Biomass, 1,000 Aether
- Gathering efficiency: Diminishing returns after 3 workers per source

---

## 10. APPENDICES

### A. Reference Games
- Warcraft 3: Reign of Chaos / The Frozen Throne
- StarCraft II
- Age of Empires II
- Command & Conquer series

### B. Art Style Direction
- Low-poly aesthetic (similar to WC3)
- Vibrant, saturated colors
- Clear unit silhouettes
- Readable from strategic camera angle

### C. Audio Design
- Directional sound for combat
- Distinct unit voice lines
- Ambient environmental sounds
- Epic orchestral soundtrack

---

**Document Status:** Living Document
**Next Review:** February 2025
**Contributors:** bakhteegames-ai team
