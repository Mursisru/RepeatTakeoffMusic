# Changelog

## [0.0.0] - 2026-06-30

### Changed
- Documentation refresh: Developer header, badges, GitHub Alerts, Keywords, gitignore hygiene.


## 1.4.1 — 2026-05-30

### Performance
- RepeatTakeoffMusicConfigCache: reads the Enabled flag once per frame in the Harmony prefix (no logic change).

## 1.4.0

- One takeoff-music boost per local `persistentID`; removed `AircraftSelectionMenu` patch.

## 1.3.0

- Reset boost on `AircraftSelectionMenu.HideSelection` (return-to-map and fly). Removed `CameraSelectionState.LeaveState` patch.

## 1.2.0

- Takeoff boost resets when leaving `CameraSelectionState` (aircraft selection at airbase), not on persistentId / land cycles.

## 1.1.1

- Fix: takeoff theme replay after land → immediate takeoff on same aircraft (state was cleared when local aircraft was briefly unavailable or on unrelated `CrossFadeMusic` calls). Use `persistentID`; no reset on `GetLocalAircraft` failure; reset on `GameManager.SetupGame`.

## 1.1.0

- Takeoff theme plays **once per local aircraft possession**: landing and taking off again in the **same** aircraft does not replay the theme.
- Switching to a **different** aircraft resets so the new plane’s first takeoff can still play its theme.
- Only patches when the clip matches the current local aircraft’s `takeoffMusic`.

## 1.0.0

- Initial release.
