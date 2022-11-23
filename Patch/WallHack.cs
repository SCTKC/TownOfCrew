using HarmonyLib;
using UnityEngine;

namespace TownOfCrew.Patch
{
    [HarmonyPatch(typeof(PlayerControl),nameof(PlayerControl.FixedUpdate))]
    public static class WallHack
    {
        public static bool IsToggle;
        public static void Postfix()
        {
            IsToggle = Input.GetKey(KeyCode.LeftControl);
            if (PlayerControl.LocalPlayer?.Collider?.offset != null)
            {
                if (IsToggle)
                {
                    PlayerControl.LocalPlayer.Collider.offset = new Vector2(0f, 127f);
                }
                else
                {
                    PlayerControl.LocalPlayer.Collider.offset = new Vector2(0f, -0.3636f);
                }
            }
        }
    }
}
