# Repeat Takeoff Music

BepInEx plugin for **[Nuclear Option](https://store.steampowered.com/app/2168680/Nuclear_Option/)** that adjusts **per-aircraft takeoff music** for the local player.

Vanilla only plays each aircraft’s takeoff theme **once per match** when that cue first runs. This mod forces **`allowReplay`** once per **local aircraft unit** (`Unit.persistentID`): **land and take off again in the same unit** → theme does **not** restart. **Spawn a new aircraft** (new network id) → you get **one** more takeoff theme for that unit.

**BepInEx plugin GUID:** `com.at747.repeattakeoffmusic`

## Requirements

- Nuclear Option (Windows)
- [BepInEx 5](https://docs.bepinex.dev/) (x64) with Harmony (bundled with typical installs)

## Build

1. Clone this repository.
2. Ensure the game is installed at the default Steam path, **or** copy `Directory.Build.user.props.example` to `Directory.Build.user.props` in the **repository root** and set `NuclearOptionRoot` to your install folder.

3. Open `RepeatTakeoffMusic.sln` in Visual Studio **or** run:

```text
msbuild RepeatTakeoffMusic\RepeatTakeoffMusic.csproj /p:Configuration=Release
```

4. Copy `RepeatTakeoffMusic\bin\Release\RepeatTakeoffMusic.dll` to the game’s `BepInEx\plugins\` folder.

## Install (release DLL)

1. Download `RepeatTakeoffMusic.dll` from the repository’s **Releases** page.
2. Place it in `...\Nuclear Option\BepInEx\plugins\`.

## How it works

Vanilla calls `MusicManager.CrossFadeMusic` from `Aircraft.CheckRadarAlt` with `allowReplay: false` for takeoff. This mod’s **Prefix** sets `allowReplay` to `true` only for the **first** matching `takeoffMusic` call on the current **`persistentID`**. When the local player controls a **different** unit id, the counter resets. `GameManager.SetupGame` resets everything for a new session.

## Behaviour notes (vanilla)

- Music is tied to becoming **airborne** (`CheckRadarAlt`), not to opening the aircraft menu.
- Takeoff clip is used only when **gear is deployed**; with gear up the game passes `null` and no takeoff theme plays (unchanged).

## Config

| Key | Default | Description |
|-----|---------|-------------|
| `General.Enabled` | `true` | Set `false` to disable the mod’s patching (vanilla behaviour). |

## Manual test checklist

1. **Same aircraft, second takeoff:** land, take off again **without** switching planes — takeoff theme should **not** play again.
2. **New spawn:** after you get a **new** aircraft (new unit id), first takeoff can play the theme again (gear down).
3. **Death:** confirm death sting / music still behaves normally.
4. **Multiplayer:** only local `CrossFadeMusic` calls are affected; remote players are unchanged.

## Compatibility

Patches `MusicManager.CrossFadeMusic(...)` and `GameManager.SetupGame`. Game updates that change these will require a mod update.

## License

[MIT](LICENSE)
