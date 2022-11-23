using HarmonyLib;
using UnityEngine;
using static TownOfCrew.Helpers;

namespace TownOfCrew.Patch
{
    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Update))]
    public static class ForcedMeetingSkip
    {
        public static void Postfix(MeetingHud __instance)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.M) && Input.GetKeyDown(KeyCode.Return) && (GameState.IsHost || GameState.IsFreePlay))
            {
                __instance.RpcClose();
            }
        }
    }
}
