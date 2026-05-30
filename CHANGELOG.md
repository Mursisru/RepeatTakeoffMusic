# Changelog

## 1.4.1 — 2026-05-30

### Performance
- RepeatTakeoffMusicConfigCache: reads the Enabled flag once per frame in the Harmony prefix (no logic change).

## 1.4.0

- **Behaviour:** One takeoff-music boost per **`Unit.persistentID`** for the local aircraft. Land → take off again on the **same** unit → no replay. **New** unit (e.g. after respawn / spawn from menu) gets a fresh id → theme can play once more. No reliance on `AircraftSelectionMenu` or `CameraSelectionState`. `GetLocalAircraft` failure does not reset state.

## 1.3.0

- Reset takeoff-music “boost slot” on **`AircraftSelectionMenu.HideSelection`** (covers **Return to map** and **Fly**), per game flow in `AircraftSelectionMenu.cs`. Removed patch on **`CameraSelectionState.LeaveState`** so camera-only changes no longer clear the slot while the selection UI may still be open.

## 1.2.0

- **Behaviour:** Takeoff-theme bypass is consumed on first in-world takeoff after you last left the **aircraft selection** flow. It resets when `CameraSelectionState.LeaveState` runs (leave selection camera / return to map), not on land→takeoff cycles. Matches “update aircraft = open selection menu, then back to map.”

## 1.1.1

- Fix: takeoff theme could play again after land → immediate takeoff on the **same** aircraft. Other `CrossFadeMusic` calls (or moments when local aircraft was temporarily unresolved) cleared internal state; bookkeeping now uses `Unit.persistentID` and is **not** reset on `GetLocalAircraft` failure. Session state clears on `GameManager.SetupGame`.

## 1.1.0

- Takeoff theme plays **once per local aircraft possession**: after landing and taking off again in the **same** aircraft (same instance), the takeoff track no longer retriggers.
- Switching to a **different** aircraft resets the state so the new plane’s first takeoff can still play its theme (when the game requests `takeoffMusic`).
- Only patches `CrossFadeMusic` when the clip matches the current local aircraft’s `takeoffMusic` (avoids touching other cues that share the same bool pattern).

## 1.0.0

- Initial release: Harmony prefix on `MusicManager.CrossFadeMusic` to allow per-aircraft takeoff music on every takeoff (local player).
