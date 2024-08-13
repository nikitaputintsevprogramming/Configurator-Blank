using ArrowControlOLD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArrowControlOLD
{
    public enum QualityEnum
    {
        FullHD,
        UHD_4K,
    };

    public class WindowControlDataOLD : MonoBehaviour
    {
        public static WindowControlDataOLD Instance;

        public Dictionary<string, Vector2> qualityDictionary = new Dictionary<string, Vector2>()
        {
            { "FullHD", new Vector2(1920, 1080)},
            { "UHD_4K", new Vector2(3840, 2160)},
        };

        public Vector2 _resolutionScreen = Instance.qualityDictionary[QualityEnum.FullHD.ToString()];
        public QualityEnum Quality = QualityEnum.FullHD;//будет отображатся как дропдаун
                                                        //public Vector2 sizeButtons = new Vector2(100, 100);

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}