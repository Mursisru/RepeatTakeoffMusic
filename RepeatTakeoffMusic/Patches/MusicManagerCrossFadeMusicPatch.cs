using HarmonyLib;
using UnityEngine;

namespace RepeatTakeoffMusic.Patches
{
    /// <summary>
    /// Vanilla takeoff calls <c>CrossFadeMusic(clip, 2f, 0f, false, false, true, 0f)</c>; pilot death uses
    /// <c>..., false, true, true, 0f)</c>. The middle bool (Harmony __4) gates one-shot-per-match behaviour for
    /// takeoff themes; forcing it true matches the death path and allows repeats.
    /// </summary>
    [HarmonyPatch(
        typeof(MusicManager),
        nameof(MusicManager.CrossFadeMusic),
        new[]
        {
            typeof(AudioClip), typeof(float), typeof(float), typeof(bool), typeof(bool), typeof(bool), typeof(float)
        })]
    internal static class MusicManagerCrossFadeMusicPatch
    {
        static void Prefix(AudioClip __0, ref bool __4, bool __5)
        {
            if (!RepeatTakeoffMusicPlugin.Enabled.Value)
                return;
            if (__0 == null)
                return;
            if (__4 || !__5)
                return;
            __4 = true;
        }
    }
}
