# Migration Guide: .NET Prototype → Unity Phase 0

**Date**: February 5, 2026  
**Status**: Repository restructure in progress

---

## 📋 What Happened?

The Aether Renaissance repository underwent a major restructuring to align with **MASTER CANON v2.1** and focus on Unity development for Phase 0.

### Old Structure (Before Feb 5, 2026)
```
AetherRenaissance/
├── Assets/Scripts/     (Unity scripts - incomplete)
├── src/                (.NET standalone demo)
│   ├── Systems/
│   │   ├── ResourceSystem.cs   (Crystal/Biomass/Aether)
│   │   └── UnitSystem.cs       (Heroes with talents)
│   └── Program.cs
├── AetherRenaissance.csproj  (.NET project file)
└── README.md           (Described .NET demo with dotnet run)
```

### Problems with Old Structure
1. ❌ **Mixed Unity + .NET**: Confusing for AI and developers
2. ❌ **Wrong game design**: Crystal/Biomass/Aether (should be Solaris/Quartz)
3. ❌ **Heroes and magic**: Phase 0 has NO heroes, NO magic
4. ❌ **Documentation mismatch**: README described different game
5. ❌ **Not playable in Unity**: Couldn't just "Press Play"

---

## ✅ New Structure (After Migration)

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
│   │   │   ├── ResourceSystem.cs  (Solaris only)
│   │   │   └── SelectionManager.cs
│   │   └── Bootstrap/
│   │       └── GameBootstrap.cs   (Auto-init)
│   ├── Prefabs/
│   ├── Scenes/
│   └── UI/
├── ProjectSettings/
├── Packages/
├── docs/
│   ├── Aether_Renaissance_Master_Context_v2.txt  (MASTER CANON)
│   └── TDD.md  (Updated to Phase 0)
├── archive/
│   └── dotnet-prototype/  (Old .NET code moved here)
├── README.md  (Unity Phase 0 instructions)
└── MIGRATION.md  (This file)
```

---

## 🗂️ What Was Archived?

The following files were moved to `archive/dotnet-prototype/`:

### Archived Files
- `src/` folder (entire .NET standalone demo)
- `AetherRenaissance.csproj` (.NET project file)
- Old `README.md` sections about `dotnet build` / `dotnet run`

### Why Archive Instead of Delete?
- **Reference**: Good architectural examples for systems design
- **History**: Shows evolution of game design
- **Learning**: Demonstrates RTS mechanics in pure C#
- **Prototyping**: May be useful for quick logic testing

---

## 🎯 What Changed in Game Design?

### Resources
| Old (Archived) | New (Phase 0) | Future (Phase 1+) |
|----------------|---------------|-------------------|
| Crystal        | Solaris       | Solaris           |
| Biomass        | ❌ Removed     | Quartz            |
| Aether         | ❌ Removed     | (Faction mechanic)|

**Phase 0 uses Solaris ONLY**. Quartz and Overcharge are planned for Phase 1.

### Units
| Old (Archived) | New (Phase 0) |
|----------------|---------------|
| Generic "Unit" with heroes | Unit_Servo (worker) |
| Talent trees (Magic/Combat) | Unit_Volt (ranged DPS) |
| Equipment system | Unit_Guard (tank) |
| Experience/leveling | **NO heroes in Phase 0** |

### Theme
| Old (Archived) | New (MASTER CANON) |
|----------------|--------------------|
| "Mystical resource" (Aether) | Tech/clockwork energy |
| Magic specializations | NO magic allowed |
| Fantasy RPG elements | Aetherpunk RTS |

---

## 🔄 How to Access Archived Code

If you need to reference the old .NET prototype:

```bash
# View archived files
cd archive/dotnet-prototype/

# Run old demo (if .NET SDK installed)
dotnet run --project AetherRenaissance.csproj
```

**Note**: The archived code will NOT be maintained. It's for reference only.

---

## 📖 What to Read Next

### For Developers
1. **README.md** - How to run Unity Phase 0
2. **docs/Aether_Renaissance_Master_Context_v2.txt** - Full design canon
3. **docs/TDD.md** - Updated technical design (Phase 0 scope)

### For AI Assistants
1. **ALWAYS read MASTER CANON first** before making changes
2. **Phase 0 scope is STRICT**: No heroes, no magic, Solaris only
3. **Use proper naming**: `Unit_Volt`, not `VoltUnit` or `RangedUnit`
4. **Aetherpunk theme**: Tech/clockwork, NO fantasy elements

---

## ⚠️ Breaking Changes

If you had local work based on the old structure:

### Old Code Won't Work
```csharp
// ❌ OLD (Archived)
var resources = new ResourceSystem();
resources.AddResource(ResourceType.Crystal, 100);
resources.AddResource(ResourceType.Biomass, 50);
resources.AddResource(ResourceType.Aether, 10);

var hero = unitSystem.CreateHero("Commander");
hero.AddTalent(new Talent("Magic", TalentType.Magic));
```

### New Phase 0 Code
```csharp
// ✅ NEW (Unity Phase 0)
var resources = new ResourceSystem();
resources.AddSolaris(500);  // Solaris only in Phase 0

var servo = GameObject.Instantiate(servoPrefab);
var volt = GameObject.Instantiate(voltPrefab);
var guard = GameObject.Instantiate(guardPrefab);

// No heroes, no talents, no magic in Phase 0
```

---

## 🚀 Migration Checklist

If you're updating your local branch:

- [ ] Pull latest `main` branch
- [ ] Delete local `src/` folder (now in `archive/`)
- [ ] Delete `AetherRenaissance.csproj` (now in `archive/`)
- [ ] Read new README.md
- [ ] Read MASTER CANON in `docs/`
- [ ] Open project in Unity 2022.3+
- [ ] Test: Press Play → Should auto-init and run

---

## 🆘 Need Help?

### "I can't find the old resource system"
→ Check `archive/dotnet-prototype/src/Systems/ResourceSystem.cs`

### "Why did the game design change?"
→ Read `docs/Aether_Renaissance_Master_Context_v2.txt` (MASTER CANON)

### "Can I still use heroes?"
→ Not in Phase 0. Heroes are planned for Phase 2+

### "Where's the magic system?"
→ **There is NO magic**. Aetherpunk uses tech/clockwork/cosmic energy (scientific, not mystical)

### "The repo says Solaris, but I want Crystal/Biomass/Aether"
→ That was the old design (archived). Phase 0 uses Solaris only. Quartz comes in Phase 1.

---

## 📞 Questions?

Open an issue: https://github.com/bakhteegames-ai/AetherRenaissance/issues

Reference Issue #1 for the full migration plan.

---

**This migration ensures clean Unity development aligned with MASTER CANON v2.1.**

**Last Updated**: February 5, 2026  
**Related Issue**: #1
