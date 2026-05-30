using HarmonyLib;
using UnityEngine;

namespace RepeatTakeoffMusic.Patches
{
    [HarmonyPatch(
        typeof(MusicManager),
        nameof(MusicManager.CrossFadeMusic),
        new[]
        {
            typeof(AudioClip), typeof(float), typeof(float), typeof(bool), typeof(bool), typeof(bool), typeof(float)
        })]
    internal static class MusicManagerCrossFadeMusicPatch
    {
        private static bool _hasTrackedPid;
        private static PersistentID _trackedPid;
        private static bool _takeoffBoostConsumed;

        internal static void ResetForNewSession()
        {
            _hasTrackedPid = false;
            _takeoffBoostConsumed = false;
        }

        static void Prefix(AudioClip __0, ref bool __4, bool __5)
        {
            RepeatTakeoffMusicConfigCache.Refresh();
            if (!RepeatTakeoffMusicConfigCache.Enabled)
                return;
            if (__0 == null)
                return;
            if (__4 || !__5)
                return;

            if (!GameManager.GetLocalAircraft(out var local) || local == null)
                return;

            var pid = local.persistentID;
            if (!_hasTrackedPid || !pid.Equals(_trackedPid))
            {
                _hasTrackedPid = true;
                _trackedPid = pid;
                _takeoffBoostConsumed = false;
            }

            if (_takeoffBoostConsumed)
                return;

            var parameters = local.GetAircraftParameters();
            if (parameters == null || parameters.takeoffMusic != __0)
                return;

            __4 = true;
            _takeoffBoostConsumed = true;
        }
    }
}
