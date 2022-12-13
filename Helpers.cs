using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using InnerNet;
using AmongUs.GameOptions;

namespace TownOfCrew
{
    public static class Helpers
    {
        public static Dictionary<byte, SpriteRenderer> MyRendCache = new();
        public static class GameState
        {
            public static bool IsGameStart => AmongUsClient.Instance?.IsGameStarted is true;
            public static bool IsHost => AmongUsClient.Instance?.AmHost is true;
            public static bool IsMeeting => MeetingHud.Instance != null;
            public static bool IsShip => ShipStatus.Instance != null;
            public static bool IsChatOpen => HudManager.Instance?.Chat?.IsOpen is true;
            public static bool IsChatActive => HudManager.Instance?.Chat?.isActiveAndEnabled is true;
            public static bool IsCanSendChat => HudManager.Instance?.Chat?.TimeSinceLastMessage >= 3f;
            public static bool IsFocusChatArea => HudManager.Instance?.Chat?.TextArea?.hasFocus is true;
            public static bool IsCanMove => PlayerControl.LocalPlayer?.CanMove is true;
            public static bool IsCountDown => GameStartManager.Instance?.startState is GameStartManager.StartingStates.Countdown;
            public static bool IsDead => PlayerControl.LocalPlayer?.Data?.IsDead is true;
            public static bool IsImpostor => PlayerControl.LocalPlayer?.Data?.Role?.IsImpostor is true;
        }

        public static void SetPos(this Transform transform, float? x = null, float? y = null, float? z = null)
        {
            var pos = transform.localPosition;
            transform.localPosition = new Vector3(x ?? pos.x, y ?? pos.y, z ?? pos.z);
        }
        public static void SetSc(this Transform transform, float? x = null, float? y = null, float? z = null)
        {
            var scale = transform.localScale;
            transform.localScale = new Vector3(x ?? scale.x, y ?? scale.y, z ?? scale.z);
        }
        public static KeyCode GetKey()
        {
            foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(code))
                {
                    return code;
                }
            }
            return KeyCode.None;
        }
        public static byte ToByte(this string s)
        {
            return (byte)int.Parse(s);
        }
        public static int ToInt(this string s)
        {
            return int.Parse(s);
        }
        public static string GetName(this PlayerControl p)
        {
            return p.Data.PlayerName;
        }
        public static string ColorString(Color c, string s)
        {
            return $"<color=#{ToByte(c.r):X2}{ToByte(c.g):X2}{ToByte(c.b):X2}{ToByte(c.a):X2}>{s}</color>";
        }
        public static string ColorString(Color32 c, string s)
        {
            return $"<color=#{ToByte(c.r):X2}{ToByte(c.g):X2}{ToByte(c.b):X2}{ToByte(c.a):X2}>{s}</color>";
        }
        private static byte ToByte(this float f)
        {
            f = Mathf.Clamp01(f);
            return (byte)(f * 255);
        }
        public static byte ToByte(this int i)
        {
            return (byte)i;
        }

        public static HatParent HatSlot(this PoolablePlayer player)
        {
            return player.transform.FindChild("HatSlot").GetComponent<HatParent>();
        }
        public static VisorLayer VisorSlot(this PoolablePlayer player)
        {
            return player.transform.FindChild("Visor").GetComponent<VisorLayer>();
        }
        public static string SetSize(this string text, float size) => $"<size={size}>" + text + "</size>";
    }
}
