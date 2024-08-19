using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

namespace CameraPreset
{
    public enum CameraPresets
    {
        RotateAround,
        FreeFly,
        Buttons
    };

    [InitializeOnLoad]
    public class CameraPreset : MonoBehaviour
    {
        // Словарь для сопоставления значений CameraPresets с типами компонентов
        public static readonly Dictionary<CameraPresets, Type> presetComponentMap = new Dictionary<CameraPresets, Type>
        {
            { CameraPresets.RotateAround, typeof(CanvasRotateAround) },
            { CameraPresets.FreeFly, typeof(CanvasFreeFly) },
            { CameraPresets.Buttons, typeof(CanvasButtons) }
        };

        public delegate void CameraPresetHandler(CameraPresets cameraPresets);
        public static event CameraPresetHandler CameraPresetIsChange;

        public CameraPresets cameraPreset = CameraPresets.FreeFly; //будет отображаться как дропдаун
        public static CameraPresets currentPreset;

        #if UNITY_EDITOR
        private void OnValidate()
        {
            currentPreset = cameraPreset;
            if(TestLog.enableLog)
                Debug.LogFormat("current preset: {0}", currentPreset);
            CameraPresetIsChange?.Invoke(currentPreset); // Вызываем событие, если есть подписчики
        }
        #endif
    }
}
