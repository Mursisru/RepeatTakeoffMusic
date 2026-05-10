using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace RepeatTakeoffMusic
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public sealed class RepeatTakeoffMusicPlugin : BaseUnityPlugin
    {
        public const string PluginGuid = "com.at747.repeattakeoffmusic";
        public const string PluginName = "Repeat Takeoff Music";
        public const string PluginVersion = "1.0.0";

        internal static ConfigEntry<bool> Enabled { get; private set; }

        private void Awake()
        {
            Enabled = Config.Bind(
                "General",
                "Enabled",
                true,
                "If true, per-aircraft takeoff music can play on every takeoff, not only the first time per match.");

            var harmony = new Harmony(PluginGuid);
            harmony.PatchAll(typeof(RepeatTakeoffMusicPlugin).GetTypeInfo().Assembly);

            Logger.LogInfo($"{PluginName} {PluginVersion} loaded (Harmony patched {nameof(MusicManager.CrossFadeMusic)}).");
        }
    }
}
