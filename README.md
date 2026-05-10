# Repeat Takeoff Music

BepInEx plugin for **[Nuclear Option](https://store.steampowered.com/app/2168680/Nuclear_Option/)** that lets **per-aircraft takeoff music** play on **every** takeoff in a match, instead of only the first time each aircraft theme is triggered.

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

Vanilla code calls `MusicManager.CrossFadeMusic` from `Aircraft.CheckRadarAlt` when the local aircraft becomes airborne, with a middle `bool` set to `false` (one-shot). Pilot death uses `true` for that same parameter. This mod applies a Harmony **Prefix** on `CrossFadeMusic` and, for calls that match the takeoff pattern (non-null clip, second bool `false`, third bool `true`), sets the second bool to `true` so the track can play again.

## Behaviour notes (vanilla)

- Music is tied to becoming **airborne** (`CheckRadarAlt`), not to opening the aircraft menu.
- Takeoff clip is used only when **gear is deployed**; with gear up the game passes `null` and no takeoff theme plays (unchanged).

## Config

| Key | Default | Description |
|-----|---------|-------------|
| `General.Enabled` | `true` | Set `false` to restore vanilla one-shot takeoff music without removing the plugin. |

## Manual test checklist

1. **Repeat takeoff:** same aircraft, land then take off again — theme should play each time (gear down).
2. **Different aircraft:** switch types in one match — each type’s theme should still play when that type takes off.
3. **Death:** confirm death sting / music still behaves normally.
4. **Multiplayer:** only local `CrossFadeMusic` calls are affected; remote players are unchanged.

## Compatibility

Patches `MusicManager.CrossFadeMusic(AudioClip, float, float, bool, bool, bool, float)`. Game updates that change this signature will require a mod update.

## License

[MIT](LICENSE)
