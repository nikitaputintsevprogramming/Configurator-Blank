using System;
using System.ComponentModel.Design;
using UnityEditor;
using UnityEngine;

namespace CameraPresets
{
    public class EditorCameraPresetMenu
    {
        public delegate void AddCameraPresetHandler();
        public static event AddCameraPresetHandler AddCameraPresetEvent;

        [MenuItem("Tools/Add camera preset")]
        static void AddTool()
        {
            if (AddCameraPresetEvent != null)
                AddCameraPresetEvent();
        }
    }
}