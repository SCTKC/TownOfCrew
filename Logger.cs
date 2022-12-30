using System.Collections.Generic;
using System.Diagnostics;
using static TownOfCrew.TownOfCrewPlugin;
using LogLevel = BepInEx.Logging.LogLevel;

namespace TownOfCrew;
class Logger
{
    public static bool isEnable;
    public static List<string> disableList = new();
    public static List<string> sendToGameList = new();
    public static bool isDetail = false;
    public static bool isAlsoInGame = false;
    public static void Enable() => isEnable = true;
    public static void Disable() => isEnable = false;
    public static void Enable(string tag, bool toGame = false)
    {
        disableList.Remove(tag);
        if (toGame && !sendToGameList.Contains(tag)) sendToGameList.Add(tag);
        else sendToGameList.Remove(tag);
    }
    public static void Disable(string tag) { if (!disableList.Contains(tag)) disableList.Add(tag); }
    public static void SendInGame(string text)
    {
        if (!isEnable) return;
        if (DestroyableSingleton<HudManager>._instance) DestroyableSingleton<HudManager>.Instance.Notifier.AddItem(text);
    }
    private static void SendToFile(string text, LogLevel level)
    {
        var Logger = logger;
        text = text.Replace("\r", "\\r").Replace("\n", "\\n");
        string log_text = $"{text}";
        switch (level)
        {
            case LogLevel.Info:
                Logger.LogInfo(log_text);
                break;
            case LogLevel.Warning:
                Logger.LogWarning(log_text);
                break;
            case LogLevel.Error:
                Logger.LogError(log_text);
                break;
            case LogLevel.Fatal:
                Logger.LogFatal(log_text);
                break;
            case LogLevel.Message:
                Logger.LogMessage(log_text);
                break;
            case LogLevel.Debug:
                Logger.LogDebug(log_text);
                break;
            case LogLevel.None or LogLevel.All:
                Logger.LogWarning("Error:Invalid LogLevel");
                Logger.LogInfo(log_text);
                break;
        }
    }
    public static void Fatal(string text) => SendToFile(text, LogLevel.Fatal);
    public static void Error(string text) => SendToFile(text, LogLevel.Error);
    public static void Warning(string text) => SendToFile(text, LogLevel.Warning);
    public static void Info(string text) => SendToFile(text, LogLevel.Info);
    public static void Message(string text) => SendToFile(text, LogLevel.Message);
    public static void Debug(string text) => SendToFile(text, LogLevel.Debug);
    public static void CurrentMethod()
    {
        StackFrame stack = new(1);
        Message($"\"[{stack.GetMethod().Name}]{stack.GetMethod().ReflectedType.Name}\"");
    }
}
