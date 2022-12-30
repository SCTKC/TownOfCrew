using BepInEx;
using BepInEx.IL2CPP;
using BepInEx.Logging;
using BepInEx.Configuration;
using HarmonyLib;

namespace TownOfCrew
{
    [BepInAutoPlugin(GUID, NAME, VERSION)]
    [HarmonyPatch]
    public partial class TownOfCrewPlugin : BasePlugin
    {
        public const string GUID = "com.sctkc.townofcrew";
        public const string NAME = "TownOfCrew";
        public const string VERSION = "1.0.0";
        public static ManualLogSource logger;
        public static Harmony Harmony = new Harmony(GUID);
        public static TownOfCrewPlugin Instance;
        public static ConfigEntry<bool> IsAutoUpdate;
        public override void Load()
        {
            Instance = this;

            logger = Log;

            Log.LogInfo($"TownOfCrewをご利用いただきありがとうございます。");

            Harmony.PatchAll();

            IsAutoUpdate = Config.Bind("Custom", "IsAutoUpdate", false, "変更したい名前を指定します");
        }
        [HarmonyPatch(typeof(ModManager),nameof(ModManager.LateUpdate))]
        public static void Postfix(ModManager __instance)
        {
            __instance.ShowModStamp();
        }
    }
}
