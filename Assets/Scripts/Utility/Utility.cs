using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class Utility
{
    static LogLevel _logLevel = LogLevel.Normal;

    public enum LogLevel
    {
        Important = 0,
        Normal,
        Verbose
    }

    public static void Log(string message, LogLevel logLevel = LogLevel.Normal)
    {
        if (logLevel > _logLevel)
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
