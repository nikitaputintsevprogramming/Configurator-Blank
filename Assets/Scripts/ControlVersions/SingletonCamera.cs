using System.Collections;
using UnityEngine;

namespace Assets.Scripts.ControlVersions
{
    public class SingletonCamera : MonoBehaviour
    {
        public static SingletonCamera Instance { get; private set; }

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

        public SettingsCamera settingsCamera;

        public float v;
        public float h;
    }
}