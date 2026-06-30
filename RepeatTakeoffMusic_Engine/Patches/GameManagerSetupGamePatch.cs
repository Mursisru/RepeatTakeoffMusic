using HarmonyLib;

namespace RepeatTakeoffMusic_Engine.Patches
{
    [HarmonyPatch(typeof(GameManager), nameof(GameManager.SetupGame))]
    internal static class GameManagerSetupGamePatch
    {
        static void Postfix()
        {
            MusicManagerCrossFadeMusicPatch.ResetForNewSession();
        }
    }
}
