using HarmonyLib;

namespace TownOfCrew
{
    [HarmonyPatch(typeof(ModManager), nameof(ModManager.LateUpdate))]
    public static class ShowModStamp
    {
        public static void Prefix(ModManager __instance)
        {
            __instance.ShowModStamp();
        }
    }
}
