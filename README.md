**Developer:** Mursisru

# Repeat Takeoff Music (Nuclear Option)

[![Nuclear Option](https://img.shields.io/badge/Game-Nuclear%20Option-blue)](https://store.steampowered.com/app/2168680/Nuclear_Option/) [![BepInEx 5](https://img.shields.io/badge/Loader-BepInEx%205-orange)](https://docs.bepinex.dev/) [![Version](https://img.shields.io/badge/Version-1.4.1-green)](https://github.com/Mursisru/RepeatTakeoffMusic/releases/tag/v1.4.1)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow)](https://github.com/Mursisru/RepeatTakeoffMusic/blob/main/LICENSE)


BepInEx plugin that adjusts **per-aircraft takeoff music**: **once per local unit** (`persistentID`). Land → take off again in the **same** unit → no replay. **New** aircraft (new id) → one more takeoff theme.

## Install

> [!IMPORTANT]
> **BepInEx 5 (x64) required** - install [BepInEx](https://docs.bepinex.dev/) before this mod.

1. Build `RepeatTakeoffMusic_Engine` (Release) or copy `RepeatTakeoffMusic_Engine.dll` into `BepInEx/plugins/`.
2. Ensure BepInEx 5 and Harmony are present (standard NO install).

Override game path: optional `Directory.Build.user.props` next to `Directory.Build.props`:

```xml
<Project>
  <PropertyGroup>
    <NuclearOptionRoot>C:\Path\To\Nuclear Option</NuclearOptionRoot>
  </PropertyGroup>
</Project>
```

## How it works

Vanilla calls `MusicManager.CrossFadeMusic` with `allowReplay: false` for takeoff. This mod’s **Prefix** sets `allowReplay` true once per local `persistentID`. See `CHANGELOG.md`.

## Behaviour notes (vanilla)

- Music is tied to becoming **airborne** (`CheckRadarAlt`), not to opening the aircraft menu.
- Takeoff clip is used only when **gear is deployed**; with gear up the game passes `null` and no takeoff theme plays (unchanged).

## Config

- `General.Enabled` — default `true`. Set `false` to disable the mod’s patching (vanilla behaviour).

## Manual test checklist

1. **Same aircraft, second takeoff:** land, take off again without switching — takeoff theme should **not** play again.
2. **New unit:** spawn a new aircraft — first takeoff can play the theme again (gear down).
3. **Death:** confirm death sting / music still behaves normally.
4. **Multiplayer:** logic is unchanged for remote players; only local `CrossFadeMusic` calls are affected.

## Compatibility

Patches `MusicManager.CrossFadeMusic(...)` and `GameManager.SetupGame`.

---

## Keywords

nuclear-option, bepinex, harmony, mod, repeattakeoffmusic, csharp, unity
