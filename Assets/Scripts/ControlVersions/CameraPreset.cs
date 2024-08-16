using UnityEditor;
using UnityEngine;

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
        public delegate void CameraPresetHandler(CameraPresets cameraPresets);
        public static event CameraPresetHandler CameraPresetIsChange;

        public CameraPresets cameraPreset = CameraPresets.FreeFly; //будет отображаться как дропдаун
        public static CameraPresets currentPreset;

        private void OnValidate()
        {
            currentPreset = cameraPreset;
            if(TestLog.enableLog)
                Debug.LogFormat("current preset: {0}", currentPreset);
            CameraPresetIsChange?.Invoke(currentPreset); // Вызываем событие, если есть подписчики
        }
    }
}
