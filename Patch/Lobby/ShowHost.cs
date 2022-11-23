using HarmonyLib;
using TMPro;
using UnityEngine;

namespace TownOfCrew.Patch.Lobby
{
    class ShowHostPatch
    {
        private static TextMeshPro HostText;

        [HarmonyPatch(typeof(LobbyBehaviour), nameof(LobbyBehaviour.Start))]
        public static class ShowHostNameText
        {
            public static void Prefix(LobbyBehaviour __instance)
            {
                var p = DestroyableSingleton<PlayerControl>.Instance.cosmetics;

                HostText = Object.Instantiate(p.nameText, __instance.transform);
                HostText.fontSize = HostText.fontSizeMin = HostText.fontSizeMax = 0.75f;
                HostText.transform.SetPos(y: 4.2f);
                HostText.name = "HostNameText";
                HostText.gameObject.SetActive(true);
                HostText.enabled = true;
            }
        }

        [HarmonyPatch(typeof(LobbyBehaviour), nameof(LobbyBehaviour.FixedUpdate))]
        public static class SetHostNameText
        {
            public static void Postfix()
            {
                if (HostText is null) return;
                var Host = AmongUsClient.Instance?.GetHost();
                HostText.text = Host.PlayerName;
            }
        }
    }
}
