using System;
using System.Collections.Generic;
using System.Text;
using AmongUs.GameOptions;
using UnityEngine;

namespace TownOfCrew
{
    public static class Helpers
    {
        public static class GameState
        {
            public static bool IsHost => AmongUsClient.Instance?.AmHost is true;
            public static bool IsFreePlay => AmongUsClient.Instance?.NetworkMode == NetworkModes.FreePlay;
            public static bool IsStart => AmongUsClient.Instance?.GameState != InnerNet.InnerNetClient.GameStates.Started;
        }
        public static bool IsMode(GameModes mode)
        {
            return GameOptionsManager.Instance.currentGameMode == mode;
        }
        public static Transform SetPos(this Transform t, float? x = null, float? y = null, float?z = null)
        {
            var pos = t.localPosition;
            t.localPosition = new Vector3(x ?? pos.x, y ?? pos.y, z ?? pos.z);
            return t;
        }
        public static Vector2 SetPos(this Vector2 v, float? x = null, float? y = null)
        {
            v = new Vector2(x ?? v.x, y ?? v.y);
            return v;
        }
    }
}
