using System.Collections;
using UnityEditor;
using UnityEngine;

namespace CameraPresets
{
    [InitializeOnLoad]
    [ExecuteAlways]
    static class PresetTwoButtons
    {
        static PresetTwoButtons()
        {
            EditorCameraPresetMenu.AddCameraPresetEvent += AddPreset;
        }

        static void AddPreset()
        {
            Debug.Log("Hello");
            GameObject CanvasTwoButtons = Resources.Load("Presets/Canvases/CanvasButtons") as GameObject;
            if (CanvasTwoButtons != null)
            {
                TestLog.Dbug.Log("Найден префаб: " + CanvasTwoButtons);
                CanvasTwoButtons.GetComponent<CanvasTwoButtons>().CreateSelf();
            }
            else
            {
                TestLog.Dbug.LogWarning("Не найден префаб: CanvasTwoButtons");
            }
        }
    }
}