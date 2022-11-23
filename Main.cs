using BepInEx;
using BepInEx.IL2CPP;
using HarmonyLib;
using AmongUs.Data;
using UnityEngine;
using static TownOfCrew.Helpers;
using static TownOfCrew.Main;

namespace TownOfCrew
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInProcess("Among Us.exe")]
    public class Main : BasePlugin
    {
        public static BepInEx.Logging.ManualLogSource Logger;
        public Harmony Harmony { get; } = new Harmony(PluginInfo.PLUGIN_GUID);

        public static Main Instance;
        public override void Load()
        {
            Logger = Log;

            Logger.LogMessage("TownOfCrewをご利用いただきありがとうございます");

            Harmony.PatchAll();
        }
    }
}