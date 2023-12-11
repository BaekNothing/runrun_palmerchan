using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class Utility
{
    public const string TAG_PLAYER = "Player";
    public const string TAG_SUPPORT = "Support";
    public const string TAG_RESPAWN = "Respawn";
    public const string SCORE_FORMAT = "0000000";
    public const string SPEED_FORMAT = "00.0";

    public enum ObjectDrawOrder
    {
        Background = -2,
        Item,
        UI
    }

    public enum LogLevel
    {
        Important = 0,
        Normal,
        Verbose
    }

    public static void Log(string message, LogLevel logLevel = LogLevel.Normal)
    {
        if (logLevel > GameData.Instance.LogLevel)
        {
            return;
        }

        if (logLevel == LogLevel.Important)
        {
            // it not error, but important log
            Debug.Log($"<color=orange>{message}</color>");
        }
        else if (logLevel == LogLevel.Verbose)
        {
            // log with stack trace
            System.Diagnostics.StackTrace stackTrace = new();
            MethodBase methodBase = stackTrace.GetFrame(1).GetMethod();
            string methodName = methodBase.Name;
            string className = methodBase.ReflectedType.Name;
            string fileName = stackTrace.GetFrame(1).GetFileName();
            int lineNumber = stackTrace.GetFrame(1).GetFileLineNumber();

            Debug.Log(string.Format("{0}::{1}() {2}:{3} {4}",
                className, methodName, fileName, lineNumber, message));
        }
        else // Normal
        {
            Debug.Log(message);
        }
    }

    public static Color HexColor(string hexCode)
    {
        if (ColorUtility.TryParseHtmlString(hexCode, out Color color))
        {
            return color;
        }

        Debug.LogError("[UnityExtension::HexColor]invalid hex code - " + hexCode);
        return Color.white;
    }

    public static void PauseGame()
    {
        GameData.Instance.SetPausedTime(System.DateTime.Now);
        Time.timeScale = 0;
    }

    public static void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public static bool CheckGameIsPaused()
    {
        return GameData.Instance.PausedTime.AddSeconds(GameData.Instance.PauseDealy) >= System.DateTime.Now;
    }
}
