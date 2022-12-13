using HarmonyLib;
using UnityEngine;

namespace TownOfCrew.Patch.Lobby
{
    [HarmonyPatch(typeof(LobbyBehaviour),nameof(LobbyBehaviour.FixedUpdate))]
    public static class WallHack
    {
        public static void Postfix()
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                PlayerControl.LocalPlayer.Collider.offset = new Vector2(0f,127f);
            }
            else
            {
                PlayerControl.LocalPlayer.Collider.offset = new Vector2(0f, -0.3636f);
            }
        }
    }
}
