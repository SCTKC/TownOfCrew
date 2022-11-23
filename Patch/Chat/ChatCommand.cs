using System;
using System.Text;
using Assets.CoreScripts;
using System.Linq;
using HarmonyLib;
using Hazel;
using static TownOfCrew.Helpers;
using UnityEngine;
using Unity;
using TMPro;
using AmongUs.Data;
using AmongUs.Data.Legacy;
using Rewired;

namespace TownOfCrew.Patch.Chat
{
    [HarmonyPatch(typeof(ChatController), nameof(ChatController.AddChat))]
    public static class ChatCommand
    {
        public static bool Prefix(PlayerControl sourcePlayer, string chatText,ChatController __instance)
        {
            if (!GameState.IsHost || GameState.IsGameStart) { return true; }
            bool handled = true;
            string[] Commands = chatText.Split(' ');
            string text = __instance.TextArea.text;
            if (Commands[0].Equals(".name",StringComparison.OrdinalIgnoreCase))
            {
                handled = false;
                sourcePlayer.RpcSetName(Commands[1]);
                if (Commands[2].Contains("color="))
                {
                    sourcePlayer.RpcSetName($"<color={Commands[2].Replace("color=", "")}>{Commands[1]}</color>");
                }
                if(sourcePlayer.NetId == PlayerControl.LocalPlayer.NetId)
                {
                    DataManager.Player.Customization.Name = Commands[1];
                }
                __instance.AddChat(sourcePlayer, $"名前を{Commands[1]}にしました。");
                 
            }
            if (Commands[0].Equals(".color", StringComparison.OrdinalIgnoreCase))
            {
                handled = false;
                sourcePlayer.RpcSetColor(Commands[1].ToByte());
                __instance.AddChat(sourcePlayer, "色を" + Commands[1].ToByte() + "にしました。");
            }
            if (Commands[0].Equals(".help", StringComparison.OrdinalIgnoreCase))
            {
                handled = false;
                __instance.AddChat(sourcePlayer,"　.name 文字列 で名前を文字列にすることが出来る。\n" +
                    ".color 1～17の数字 色をで変更することができる。\n" +
                    ".help このヘルプをで表示できる。\n" +
                    ".platform 全員の機種が表示される。");
            }
            if (Commands[0].Equals(".kc", StringComparison.OrdinalIgnoreCase))
            {
                handled = false;
                if (!float.TryParse(text[4..], out var cooltime)) __instance.AddChat(PlayerControl.LocalPlayer, "使い方\n/kc {キルクールタイム}");
                var settime = cooltime;
                if (settime == 0) settime = 1E-45f;
                PlayerControl.GameOptions.KillCooldown = settime;
                PlayerControl.LocalPlayer.RpcSyncSettings(PlayerControl.GameOptions);
                __instance.AddChat(PlayerControl.LocalPlayer, $"キルクールタイムを{settime}秒に変更しました！");
            }
            return handled;
        }
    }
}
