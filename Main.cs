using BepInEx;
using BepInEx.Logging;
using BepInEx.IL2CPP;
using HarmonyLib;

namespace TownOfCrew
{
    [BepInAutoPlugin("com.sctkc.townofcrew", "TownOfCrew", "1.0.1")]
    [BepInIncompatibility("com.ykundesu.supernewroles")]
    [BepInIncompatibility("com.tugaru.TownOfPlus")]
    [BepInIncompatibility("com.emptybottle.townofhosdt")]
    [BepInProcess("Among Us.exe")]
    public partial class Main : BasePlugin
    {
        public Harmony Harmony { get; } = new Harmony("com.sctkc.townofcrew");

        public static Main Instance = new();

        public static ManualLogSource logger;

        public override void Load()
        {
            logger = Log;

            Logger.Message("TownOfCrewをご利用いただきありがとうございます");

            Harmony.PatchAll();
        }
    }
}