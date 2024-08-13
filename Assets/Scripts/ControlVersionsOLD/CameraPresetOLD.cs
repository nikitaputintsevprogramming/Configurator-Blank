using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControlVersionsOLD
{
    public enum CameraPresets
    {
        RotateAround,
        FreeFly,
        Buttons
    };

    public class CameraPresetOLD : MonoBehaviour
    {
        public CameraPresets Quality = CameraPresets.FreeFly; //будет отображатся как дропдаун

        static Dictionary<string, Vector2> qualityDictionary = new Dictionary<string, Vector2>()
        {
            { "RotateAround", new Vector2(1920, 1080)},
            { "FreeFly", new Vector2(3840, 2160)},
            { "Buttons", new Vector2(3840, 2160)},
        };
    }
}