using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class TestLog
{
    //public static TestLog Instance;

    public static bool enableLog;

    [MenuItem("Debugging/Invert logging")]
    private static void InvertLogging()
    {
        enableLog = !enableLog;
        Debug.LogFormat("Enable log to: {0}", enableLog);
    }
}
