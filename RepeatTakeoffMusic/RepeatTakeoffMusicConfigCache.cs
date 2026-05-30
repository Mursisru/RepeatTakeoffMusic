using UnityEngine;

namespace RepeatTakeoffMusic
{
    internal static class RepeatTakeoffMusicConfigCache
    {
        private static int _frame = -1;

        internal static bool Enabled;

        internal static void Refresh()
        {
            int frame = Time.frameCount;
            if (frame == _frame)
                return;
            _frame = frame;
            Enabled = RepeatTakeoffMusicPlugin.Enabled != null && RepeatTakeoffMusicPlugin.Enabled.Value;
        }
    }
}
