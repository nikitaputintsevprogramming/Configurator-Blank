using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CameraPresets
{
    public static class TestLog
    {
        public static bool enableLog = true;

        [MenuItem("Debugging/Invert logging")]
        private static void InvertLogging()
        {
            enableLog = !enableLog;
            Debug.LogFormat("Enable log to: {0}", enableLog);
        }

        [InitializeOnLoad]
        [ExecuteAlways]
        public static class Dbug
        {
            public static void Log(string message)
            {
                if (enableLog)
                {
                    Debug.Log(message);
                }
            }

            public static void LogWarning(string message)
            {
                if (enableLog)
                {
                    Debug.LogWarning(message);
                }
            }

            public static void LogError(string message)
            {
                if (enableLog)
                {
                    Debug.LogError(message);
                }
            }
        }
    }
}
