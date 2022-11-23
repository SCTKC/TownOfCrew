using HarmonyLib;
using UnityEngine;
using static TownOfCrew.Helpers;

namespace TownOfCrew.Patch
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.FixedUpdate))]
    public static class ForcedMeetingSkip
    {
        public static void Postfix()
        {
            if (GameState.IsMeeting && GameState.IsHost && Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.M) && Input.GetKeyDown(KeyCode.Return))
            {
                MeetingHud.Instance.RpcClose();
            }
        }
    }
}
