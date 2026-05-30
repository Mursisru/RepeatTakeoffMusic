using BepInEx;
using BepInEx.Configuration;
using System.Reflection;
using HarmonyLib;

namespace RepeatTakeoffMusic
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public sealed class RepeatTakeoffMusicPlugin : BaseUnityPlugin
    {
        public const string PluginGuid = "com.at747.repeattakeoffmusic";
        public const string PluginName = "Repeat Takeoff Music";
        public const string PluginVersion = "1.4.1";

        internal static ConfigEntry<bool> Enabled { get; private set; }

        private void Awake()
        {
            Enabled = Config.Bind(
                "General",
                "Enabled",
                true,
                "If true, takeoff theme plays once per local aircraft unit (persistentID); land and take off again in the same unit will not replay it. New spawn = new id = one more play.");

            var harmony = new Harmony(PluginGuid);
            harmony.PatchAll(typeof(RepeatTakeoffMusicPlugin).GetTypeInfo().Assembly);

            Logger.LogInfo($"{PluginName} {PluginVersion} loaded (Harmony patched {nameof(MusicManager.CrossFadeMusic)}).");
        }
    }
}
