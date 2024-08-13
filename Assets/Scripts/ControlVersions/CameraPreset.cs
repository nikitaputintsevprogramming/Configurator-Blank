using System.Collections;
using System.Collections.Generic;
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
        public delegate void CameraPresetHandler();
        public static event CameraPresetHandler CameraPresetIsChange;

        public CameraPresets cameraPreset = CameraPresets.FreeFly; //будет отображатся как дропдаун
        public static CameraPresets currentPreset;

        //static Dictionary<string, Vector2> cameraPresetDictionary = new Dictionary<string, Vector2>()
        //{
        //    { "RotateAround", new Vector2(1920, 1080)},
        //    { "FreeFly", new Vector2(3840, 2160)},
        //    { "Buttons", new Vector2(3840, 2160)},
        //};

        private void OnValidate()
        {
            //Debug.Log(currentPreset);
            currentPreset = cameraPreset;
            if (CameraPresetIsChange != null)
                CameraPresetIsChange();
        }
    }
}