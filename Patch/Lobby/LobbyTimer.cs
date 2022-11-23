using HarmonyLib;
using UnityEngine;
using static TownOfCrew.Helpers;

namespace TownOfCrew.Patch.Lobby
{
    class LobbyTimer
    {
        public static float Timer = 600f;
        [HarmonyPatch(typeof(GameStartManager),nameof(GameStartManager.Start))]
        public static class GameStartManagerStartPatch
        {
            public static void Postfix() 
            {
                Timer = 600f;
            }
        }
        [HarmonyPatch(typeof(GameStartManager),nameof(GameStartManager.Update))]
        public static class GameStartManagerUpdatePatch
        {
            private static bool update = false;
            private static string currentText = "";
            public static void Prefix(GameStartManager __instance)
            {
                if (!GameState.IsHost || !GameData.Instance || GameState.IsLocalGame) return;
                update = GameData.Instance.PlayerCount != __instance.LastPlayerCount;
            }
            public static void Postfix(GameStartManager __instance)
            {
                if (!GameState.IsHost || !GameData.Instance || GameState.IsLocalGame) return;
                if (update) currentText = __instance.PlayerCounter.text;
                Timer = Mathf.Max(0f,Timer -= Time.deltaTime);
                int m = (int)Timer / 60;
                int s = (int)Timer % 60;
                string suffix = $" {m:00}:{s:00}";
                __instance.PlayerCounter.text = currentText + suffix;
                __instance.PlayerCounter.autoSizeTextContainer = true;
            }
        }
    }
}
