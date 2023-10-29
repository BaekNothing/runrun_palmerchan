using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class Utility
{
    public const string TAG_PLAYER = "Player";
    public const string TAG_SUPPORT = "Support";
    public const string TAG_RESPAWN = "Respawn";

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
}
