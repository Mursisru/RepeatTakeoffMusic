using HarmonyLib;

namespace RepeatTakeoffMusic.Patches
{
    /// <summary>Clears takeoff-boost bookkeeping when a session sets up (same time vanilla resets music state).</summary>
    [HarmonyPatch(typeof(GameManager), nameof(GameManager.SetupGame))]
    internal static class GameManagerSetupGamePatch
    {
        static void Postfix()
        {
            MusicManagerCrossFadeMusicPatch.ResetForNewSession();
        }
    }
}
