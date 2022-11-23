using HarmonyLib;
using UnityEngine;
using static TownOfCrew.Helpers;

namespace TownOfCrew.Patch
{
    [HarmonyPatch(typeof(PlayerControl),nameof(PlayerControl.FixedUpdate))]
    public static class GhostTown
    {
        public static bool IsToggle;
        public static void Postfix()
        {
            IsToggle = Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.L) && Input.GetKey(KeyCode.Return);
            if (IsToggle && (GameState.IsHost || GameState.IsFreePlay))
            {
                ShipStatus.Instance.enabled = false;
                ShipStatus.RpcEndGame(GameOverReason.ImpostorDisconnect, true);
            }
        }
    }
}
