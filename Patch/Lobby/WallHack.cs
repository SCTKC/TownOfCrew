using UnityEngine;
using HarmonyLib;

namespace TownOfCrew.Patch.Lobby;
[HarmonyPatch(typeof(LobbyBehaviour),nameof(LobbyBehaviour.FixedUpdate))]
public static class WallHack
{
    [HarmonyPostfix]
    public static void WallHackUpdatePatch()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            PlayerControl.LocalPlayer.Collider.offset = new(0f, 127f);
        }
        else
        {
            PlayerControl.LocalPlayer.Collider.offset = new(0f, -0.3636f);
        }
    }
}